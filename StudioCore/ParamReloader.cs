using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using StudioCore.MsbEditor;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ProcessMemoryUtilities.Managed;
using ProcessMemoryUtilities.Native;
using SoulsFormats;

namespace StudioCore
{
    class ParamReloader
    {
        public static SoulsParamMemory memoryHandler;

        public static void ReloadMemoryParamsDS3()
        {
            var processArray = Process.GetProcessesByName("DarkSoulsIII");
            if (processArray.Any())
            {
                memoryHandler = new SoulsParamMemory(processArray.First());
                List<Thread> threads = new List<Thread>();

                foreach (var (paramFileName, param) in ParamBank.Params)
                {
                    if (SoulsParamMemory.paramOffsetsDS3.ContainsKey(paramFileName))
                    {
                        threads.Add(new Thread(() => WriteMemoryPARAM(param, SoulsParamMemory.paramOffsetsDS3[paramFileName])));
                    }
                }

                foreach (var thread in threads)
                {
                    thread.Start();
                }
                foreach (var thread in threads)
                {
                    thread.Join();
                }

                memoryHandler.Terminate();
                memoryHandler = null;
            }
        }
        private static void WriteMemoryPARAM(PARAM param, int paramOffset)
        {
            var BasePtr = memoryHandler.GetParamPtrDS3(paramOffset);
            var BaseDataPtr = memoryHandler.GetToRowPtrDS3(paramOffset);
            var RowCount = memoryHandler.GetRowCountDS3(paramOffset);

            IntPtr DataSectionPtr;

            int RowId = 0;
            int rowPtr = 0;

            for (int i = 0; i < RowCount; i++)
            {
                memoryHandler.ReadProcessMemory(BaseDataPtr, ref RowId);
                memoryHandler.ReadProcessMemory(BaseDataPtr + 0x8, ref rowPtr);

                DataSectionPtr = IntPtr.Add(BasePtr, rowPtr);

                BaseDataPtr += 0x18;

                PARAM.Row row = param[RowId];
                if (row != null)
                {
                    WriteMemoryRow(row, DataSectionPtr);
                }
            }
        }
        private static void WriteMemoryRow(PARAM.Row row, IntPtr RowDataSectionPtr)
        {
            int offset = 0;
            int bitFieldPos = 0;
            BitArray bits = null;

            foreach (var cell in row.Cells)
            {
                offset += WriteMemoryCell(cell, RowDataSectionPtr + offset, ref bitFieldPos, ref bits);
            }
        }
        private static int WriteMemoryCell(PARAM.Cell cell, IntPtr CellDataPtr, ref int bitFieldPos, ref BitArray bits)
        {
            PARAMDEF.DefType displayType = cell.Def.DisplayType;
            // If this can be simplified, that would be ideal. Currently we have to reconcile DefType, a numerical size in bits, and the Type used for the bitField array
            if (cell.Def.BitSize != -1)
            {
                if (displayType == SoulsFormats.PARAMDEF.DefType.u8)
                {
                    if (bitFieldPos == 0)
                    {
                        bits = new BitArray(8);
                    }
                    bits.Set(bitFieldPos, Convert.ToBoolean(cell.Value));
                    bitFieldPos++;
                    if (bitFieldPos == 8)
                    {
                        byte valueRead = 0;
                        memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                        byte[] bitField = new byte[1];
                        bits.CopyTo(bitField, 0);
                        bitFieldPos = 0;
                        byte bitbuffer = bitField[0];
                        if (valueRead != bitbuffer)
                        {
                            memoryHandler.WriteProcessMemory(CellDataPtr, ref bitbuffer);
                        }
                        return sizeof(byte);
                    }
                    return 0;
                }
                else if (displayType == SoulsFormats.PARAMDEF.DefType.u16)
                {
                    if (bitFieldPos == 0)
                    {
                        bits = new BitArray(16);
                    }
                    bits.Set(bitFieldPos, Convert.ToBoolean(cell.Value));
                    bitFieldPos++;
                    if (bitFieldPos == 16)
                    {
                        ushort valueRead = 0;
                        memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                        ushort[] bitField = new ushort[1];
                        bits.CopyTo(bitField, 0);
                        bitFieldPos = 0;
                        ushort bitbuffer = bitField[0];
                        if (valueRead != bitbuffer)
                        {
                            memoryHandler.WriteProcessMemory(CellDataPtr, ref bitbuffer);
                        }
                        return sizeof(UInt16);
                    }
                    return 0;
                }
                else if (displayType == SoulsFormats.PARAMDEF.DefType.u32)
                {
                    if (bitFieldPos == 0)
                    {
                        bits = new BitArray(32);
                    }
                    bits.Set(bitFieldPos, Convert.ToBoolean(cell.Value));
                    bitFieldPos++;
                    if (bitFieldPos == 32)
                    {
                        uint valueRead = 0;
                        memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                        uint[] bitField = new uint[1];
                        bits.CopyTo(bitField, 0);
                        bitFieldPos = 0;
                        uint bitbuffer = bitField[0];
                        if (valueRead != bitbuffer)
                        {
                            memoryHandler.WriteProcessMemory(CellDataPtr, ref bitbuffer);
                        }
                        return sizeof(UInt32);
                    }
                    return 0;
                }
            }
            if (displayType == SoulsFormats.PARAMDEF.DefType.f32)
            {
                float valueRead = 0f;
                memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                float value = Convert.ToSingle(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory(CellDataPtr, ref value);
                }
                return sizeof(float);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.s32)
            {
                int valueRead = 0;
                memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                int value = Convert.ToInt32(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory(CellDataPtr, ref value);
                }
                return sizeof(Int32);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.s16)
            {
                short valueRead = 0;
                memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                short value = Convert.ToInt16(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory(CellDataPtr, ref value);
                }
                return sizeof(Int16);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.s8)
            {
                sbyte valueRead = 0;
                memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                sbyte value = Convert.ToSByte(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory(CellDataPtr, ref value);
                }
                return sizeof(sbyte);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.u32)
            {
                uint valueRead = 0;
                memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                uint value = Convert.ToUInt32(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory(CellDataPtr, ref value);
                }
                return sizeof(UInt32);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.u16)
            {
                ushort valueRead = 0;
                memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                ushort value = Convert.ToUInt16(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory(CellDataPtr, ref value);
                }
                return sizeof(UInt16);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.u8)
            {
                byte valueRead = 0;
                memoryHandler.ReadProcessMemory(CellDataPtr, ref valueRead);

                byte value = Convert.ToByte(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory(CellDataPtr, ref value);
                }
                return sizeof(byte);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.dummy8 || displayType == SoulsFormats.PARAMDEF.DefType.fixstr || displayType == SoulsFormats.PARAMDEF.DefType.fixstrW)
            {
                return cell.Def.ArrayLength;
            }
            else
            {
                throw new Exception("Unexpected Field Type");
            }
        }
    }
    public class SoulsParamMemory
    {
        public IntPtr memoryHandle;
        private readonly Process gameProcess;
        public SoulsParamMemory(Process gameProcess)
        {
            this.gameProcess = gameProcess;
            this.memoryHandle = NativeWrapper.OpenProcess(ProcessAccessFlags.ReadWrite, gameProcess.Id);
        }
        public void Terminate()
        {
            NativeWrapper.CloseHandle(memoryHandle);
            memoryHandle = (IntPtr)0;
        }
        public static Dictionary<string, int> paramOffsetsDS3 = new Dictionary<string, int>()
        {
                {"ActionButtonParam", 0xAD8},
                {"AiSoundParam", 0xD60},
                {"AtkParam_Npc", 0x268},
                {"AtkParam_Pc", 0x2B0},
                {"AttackElementCorrectParam", 0x1660},
                {"BehaviorParam", 0x3D0},
                {"BehaviorParam_PC", 0x418},
                {"BonfireWarpParam", 0xF10},
                {"BudgetParam", 0xEC8},
                {"Bullet", 0x388},
                {"BulletCreateLimitParam", 0x1780},
                {"CalcCorrectGraph", 0x8E0},
                {"Ceremony", 0x1078},
                {"CharaInitParam", 0x658},
                {"CharMakeMenuListItemParam", 0x1150},
                {"CharMakeMenuTopParam", 0x1108},
                {"ClearCountCorrectParam", 0x17C8},
                {"CoolTimeParam", 0x1A98},
                {"CultSettingParam", 0x1468},
                {"DecalParam", 0xA90},
                {"DirectionCameraParam", 0x1390},
                {"EquipMtrlSetParam", 0x6A0},
                {"EquipParamAccessory", 0x100},
                {"EquipParamGoods", 0x148},
                {"EquipParamProtector", 0xB8},
                {"EquipParamWeapon", 0x70},
                {"FaceGenParam", 0x6E8},
                {"FaceParam", 0x730},
                {"FaceRangeParam", 0x778},
                {"FootSfxParam", 0x16F0},
                {"GameAreaParam", 0x850},
                {"GameProgressParam", 0x1810},
                {"GemCategoryParam", 0xC40},
                {"GemDropDopingParam", 0xC88},
                {"GemDropModifyParam", 0xCD0},
                {"GemeffectParam", 0xBF8},
                {"GemGenParam", 0xBB0},
                {"HitEffectSeParam", 0x1270},
                {"HitEffectSfxConceptParam", 0x11E0},
                {"HitEffectSfxParam", 0x1228},
                {"HPEstusFlaskRecoveryParam", 0x14F8},
                {"ItemLotParam", 0x5C8},
                {"KnockBackParam", 0xA00},
                {"KnowledgeLoadScreenItemParam", 0x18E8},
                {"LoadBalancerDrawDistScaleParam", 0x1A50},
                {"LoadBalancerParam", 0x1858},
                {"LockCamParam", 0x928},
                {"Magic", 0x460},
                {"MapMimicryEstablishmentParam", 0x15D0},
                {"MenuOffscrRendParam", 0x1930},
                {"MenuPropertyLayoutParam", 0xFA0},
                {"MenuPropertySpecParam", 0xF58},
                {"MenuValueTableParam", 0xFE8},
                {"ModelSfxParam", 0xD18},
                {"MoveParam", 0x610},
                {"MPEstusFlaskRecoveryParam", 0x1540},
                {"MultiHPEstusFlaskBonusParam", 0x1978},
                {"MultiMPEstusFlaskBonusParam", 0x19C0},
                {"MultiPlayCorrectionParam", 0x1588},
                {"NetWorkAreaParam", 0xDF0},
                {"NetworkMsgParam", 0xE80},
                {"NetworkParam", 0xE38},
                {"NewMenuColorTableParam", 0x1198},
                {"NpcAiActionParam", 0x1738},
                {"NpcParam", 0x220},
                {"NpcThinkParam", 0x2F8},
                {"ObjActParam", 0x970},
                {"ObjectMaterialSfxParam", 0x18A0},
                {"ObjectParam", 0x340},
                {"PhantomParam", 0x10C0},
                {"PlayRegionParam", 0xDA8},
                {"ProtectorGenParam", 0xB68},
                {"RagdollParam", 0x7C0},
                {"ReinforceParamProtector", 0x1D8},
                {"ReinforceParamWeapon", 0x190},
                {"RoleParam", 0x13D8},
                {"SeMaterialConvertParam", 0x1348},
                {"ShopLineupParam", 0x808},
                {"SkeletonParam", 0x898},
                {"SpEffectParam", 0x4A8},
                {"SpEffectVfxParam", 0x4F0},
                {"SwordArtsParam", 0x14B0},
                {"TalkParam", 0x538},
                {"ThrowDirectionSfxParam", 0x16A8},
                {"ToughnessParam", 0x1300},
                {"UpperArmParam", 0x1618},
                {"WeaponGenParam", 0xB20},
                {"WepAbsorpPosParam", 0x12B8},
                {"WetAspectParam", 0x1420},
                {"Wind", 0xA48},
        };
        public bool ReadProcessMemory<T>(IntPtr baseAddress, ref T buffer) where T : unmanaged
        {
            return NativeWrapper.ReadProcessMemory(memoryHandle, baseAddress, ref buffer);
        }

        public bool WriteProcessMemory<T>(IntPtr baseAddress, ref T buffer) where T : unmanaged
        {
            return NativeWrapper.WriteProcessMemory(memoryHandle, baseAddress, ref buffer);
        }

        public IntPtr GetParamPtrDS3(int Offset)
        {
            IntPtr ParamPtr = IntPtr.Add(gameProcess.MainModule.BaseAddress, 0x4782838);
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr, ref ParamPtr);
            ParamPtr = IntPtr.Add(ParamPtr, Offset);
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr, ref ParamPtr);
            ParamPtr = IntPtr.Add(ParamPtr, 0x68);
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr, ref ParamPtr);
            ParamPtr = IntPtr.Add(ParamPtr, 0x68);
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr, ref ParamPtr);

            return ParamPtr;
        }

        public short GetRowCountDS3(int Offset)
        {
            IntPtr ParamPtr = GetParamPtrDS3(Offset);

            Int16 buffer = 0;
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr + 0xA, ref buffer);

            return buffer;
        }

        public IntPtr GetToRowPtrDS3(int Offset)
        {
            var ParamPtr = GetParamPtrDS3(Offset);
            ParamPtr = IntPtr.Add(ParamPtr, 0x40);

            return ParamPtr;
        }
    }
}
