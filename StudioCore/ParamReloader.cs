using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using StudioCore.MsbEditor;
using PARAM = SoulsMemory.PARAM;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ProcessMemoryUtilities.Managed;
using ProcessMemoryUtilities.Native;

namespace StudioCore
{
    class ParamReloader
    {
        public static StreamWriter log;
        public static IntPtr memoryHandlerPtr = (IntPtr)0;
        public static void ReloadMemoryParamsDS3()
        {
            var processArray = Process.GetProcessesByName("DarkSoulsIII");
            if (processArray.Any())
            {
                memoryHandlerPtr = NativeWrapper.OpenProcess(ProcessAccessFlags.ReadWrite, processArray.First().Id);
                SoulsMemory.Memory.ProcessHandle = SoulsMemory.Memory.AttachProc("DarkSoulsIII");
                Stopwatch meme = new Stopwatch();
                meme.Start();

                log = new StreamWriter("log.txt"); ;
                log.WriteLine("Below here are the fields edited in the last ParamMemoryReloadDS3 Operation");
                log.WriteLine("---------------------------------------------------------------------------");
                List<Thread> threads = new List<Thread>();
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ActionButtonParam"], PARAM.ParamBaseOffset.ActionButtonParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["AiSoundParam"], PARAM.ParamBaseOffset.AiSoundParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["AtkParam_Npc"], PARAM.ParamBaseOffset.AtkParam_Npc)));
                //threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["AtkParam_Pc"], PARAM.ParamBaseOffset.AtkParam_Pc))); //Currently Broken look into this:
                //threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["AttackElementCorrectParam"], PARAM.ParamBaseOffset.AttackElementCorrectParam))); //Currently Broken look into this:
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BehaviorParam"], PARAM.ParamBaseOffset.BehaviorParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BehaviorParam_PC"], PARAM.ParamBaseOffset.BehaviorParam_PC)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BonfireWarpParam"], PARAM.ParamBaseOffset.BonfireWarpParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BudgetParam"], PARAM.ParamBaseOffset.BudgetParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["Bullet"], PARAM.ParamBaseOffset.Bullet)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BulletCreateLimitParam"], PARAM.ParamBaseOffset.BulletCreateLimitParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CalcCorrectGraph"], PARAM.ParamBaseOffset.CalcCorrectGraph)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["Ceremony"], PARAM.ParamBaseOffset.Ceremony)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CharaInitParam"], PARAM.ParamBaseOffset.CharaInitParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CharMakeMenuListItemParam"], PARAM.ParamBaseOffset.CharMakeMenuListItemParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CharMakeMenuTopParam"], PARAM.ParamBaseOffset.CharMakeMenuTopParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ClearCountCorrectParam"], PARAM.ParamBaseOffset.ClearCountCorrectParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CoolTimeParam"], PARAM.ParamBaseOffset.CoolTimeParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CultSettingParam"], PARAM.ParamBaseOffset.CultSettingParam)));
                //threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["DecalParam"], PARAM.ParamBaseOffset.DecalParam))); //Currently Broken look into this:
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["DirectionCameraParam"], PARAM.ParamBaseOffset.DirectionCameraParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipMtrlSetParam"], PARAM.ParamBaseOffset.EquipMtrlSetParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipParamAccessory"], PARAM.ParamBaseOffset.EquipParamAccessory)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipParamGoods"], PARAM.ParamBaseOffset.EquipParamGoods)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipParamProtector"], PARAM.ParamBaseOffset.EquipParamProtector)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipParamWeapon"], PARAM.ParamBaseOffset.EquipParamWeapon)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["FaceGenParam"], PARAM.ParamBaseOffset.FaceGenParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["FaceParam"], PARAM.ParamBaseOffset.FaceParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["FaceRangeParam"], PARAM.ParamBaseOffset.FaceRangeParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["FootSfxParam"], PARAM.ParamBaseOffset.FootSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GameAreaParam"], PARAM.ParamBaseOffset.GameAreaParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GameProgressParam"], PARAM.ParamBaseOffset.GameProgressParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemCategoryParam"], PARAM.ParamBaseOffset.GemCategoryParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemDropDopingParam"], PARAM.ParamBaseOffset.GemDropDopingParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemDropModifyParam"], PARAM.ParamBaseOffset.GemDropModifyParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemeffectParam"], PARAM.ParamBaseOffset.GemeffectParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemGenParam"], PARAM.ParamBaseOffset.GemGenParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["HitEffectSeParam"], PARAM.ParamBaseOffset.HitEffectSeParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["HitEffectSfxConceptParam"], PARAM.ParamBaseOffset.HitEffectSfxConceptParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["HitEffectSfxParam"], PARAM.ParamBaseOffset.HitEffectSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["HPEstusFlaskRecoveryParam"], PARAM.ParamBaseOffset.HPEstusFlaskRecoveryParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ItemLotParam"], PARAM.ParamBaseOffset.ItemLotParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["KnockBackParam"], PARAM.ParamBaseOffset.KnockBackParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["KnowledgeLoadScreenItemParam"], PARAM.ParamBaseOffset.KnowledgeLoadScreenItemParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["LoadBalancerDrawDistScaleParam"], PARAM.ParamBaseOffset.LoadBalancerDrawDistScaleParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["LockCamParam"], PARAM.ParamBaseOffset.LockCamParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["Magic"], PARAM.ParamBaseOffset.Magic)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MapMimicryEstablishmentParam"], PARAM.ParamBaseOffset.MapMimicryEstablishmentParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MenuOffscrRendParam"], PARAM.ParamBaseOffset.MenuOffscrRendParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MenuPropertyLayoutParam"], PARAM.ParamBaseOffset.MenuPropertyLayoutParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MenuPropertySpecParam"], PARAM.ParamBaseOffset.MenuPropertySpecParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MenuValueTableParam"], PARAM.ParamBaseOffset.MenuValueTableParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ModelSfxParam"], PARAM.ParamBaseOffset.ModelSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MoveParam"], PARAM.ParamBaseOffset.MoveParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MPEstusFlaskRecoveryParam"], PARAM.ParamBaseOffset.MPEstusFlaskRecoveryParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MultiHPEstusFlaskBonusParam"], PARAM.ParamBaseOffset.MultiHPEstusFlaskBonusParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MultiMPEstusFlaskBonusParam"], PARAM.ParamBaseOffset.MultiMPEstusFlaskBonusParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MultiPlayCorrectionParam"], PARAM.ParamBaseOffset.MultiPlayCorrectionParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NetworkMsgParam"], PARAM.ParamBaseOffset.NetworkMsgParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NetworkParam"], PARAM.ParamBaseOffset.NetworkParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NewMenuColorTableParam"], PARAM.ParamBaseOffset.NewMenuColorTableParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NpcAiActionParam"], PARAM.ParamBaseOffset.NpcAiActionParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NpcParam"], PARAM.ParamBaseOffset.NpcParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NpcThinkParam"], PARAM.ParamBaseOffset.NpcThinkParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ObjActParam"], PARAM.ParamBaseOffset.ObjActParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ObjectMaterialSfxParam"], PARAM.ParamBaseOffset.ObjectMaterialSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ObjectParam"], PARAM.ParamBaseOffset.ObjectParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["PhantomParam"], PARAM.ParamBaseOffset.PhantomParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["PlayRegionParam"], PARAM.ParamBaseOffset.PlayRegionParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ProtectorGenParam"], PARAM.ParamBaseOffset.ProtectorGenParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["RagdollParam"], PARAM.ParamBaseOffset.RagdollParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ReinforceParamProtector"], PARAM.ParamBaseOffset.ReinforceParamProtector)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ReinforceParamWeapon"], PARAM.ParamBaseOffset.ReinforceParamWeapon)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["RoleParam"], PARAM.ParamBaseOffset.RoleParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SeMaterialConvertParam"], PARAM.ParamBaseOffset.SeMaterialConvertParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ShopLineupParam"], PARAM.ParamBaseOffset.ShopLineupParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SkeletonParam"], PARAM.ParamBaseOffset.SkeletonParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SpEffectParam"], PARAM.ParamBaseOffset.SpEffectParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SpEffectVfxParam"], PARAM.ParamBaseOffset.SpEffectVfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SwordArtsParam"], PARAM.ParamBaseOffset.SwordArtsParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["TalkParam"], PARAM.ParamBaseOffset.TalkParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ThrowDirectionSfxParam"], PARAM.ParamBaseOffset.ThrowDirectionSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ToughnessParam"], PARAM.ParamBaseOffset.ToughnessParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["UpperArmParam"], PARAM.ParamBaseOffset.UpperArmParam)));
                //threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["WeaponGenParam"], PARAM.ParamBaseOffset.WeaponGenParam))); //Currently Broken look into this:
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["WepAbsorpPosParam"], PARAM.ParamBaseOffset.WepAbsorpPosParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["WetAspectParam"], PARAM.ParamBaseOffset.WeaponGenParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["Wind"], PARAM.ParamBaseOffset.Wind)));

                foreach (var thread in threads)
                {
                    thread.Start();
                }
                foreach (var thread in threads)
                {
                    thread.Join();
                }
                NativeWrapper.CloseHandle(memoryHandlerPtr);
                memoryHandlerPtr = (IntPtr)0;
                meme.Stop();
                log.WriteLine("---------------------------------------------------------------------------");
                log.WriteLine(meme.ElapsedMilliseconds + "ms elapsed while writing those fields to memory.");
                log.Close();
            }
        }
        private static void WriteMemoryPARAM(SoulsFormats.PARAM param, PARAM.ParamBaseOffset enumOffset)
        {
            var BasePtr = SoulsMemory.PARAM.GetParamPtr(enumOffset);
            var BaseDataPtr = SoulsMemory.PARAM.GetToRowPtr(enumOffset);
            var RowCount = SoulsMemory.PARAM.GetRowCount(enumOffset);

            IntPtr DataSectionPtr;

            int RowId;
            int rowPtr;

            //log.WriteLine(param.AppliedParamdef.ParamType);
            for (int i = 0; i < RowCount; i++)
            {
                RowId = SoulsMemory.Memory.ReadInt32(BaseDataPtr);
                rowPtr = SoulsMemory.Memory.ReadInt32(BaseDataPtr + 0x8);

                DataSectionPtr = IntPtr.Add(BasePtr, rowPtr);

                BaseDataPtr = BaseDataPtr + 0x18;

                SoulsFormats.PARAM.Row row = param[RowId];
                if (row != null)
                {
                    WriteMemoryRow(row, DataSectionPtr);
                }
            }
        }
        private static void WriteMemoryRow(SoulsFormats.PARAM.Row row, IntPtr RowDataSectionPtr)
        {
            int offset = 0;
            int bitFieldPos = 0;
            BitArray bits = null;

            foreach (var cell in row.Cells)
            {
                offset += WriteMemoryCell(cell, RowDataSectionPtr + offset, ref bitFieldPos, ref bits);
            }
        }
        private static int WriteMemoryCell(SoulsFormats.PARAM.Cell cell, IntPtr CellDataPtr, ref int bitFieldPos, ref BitArray bits)
        {
            string dataTypeString = cell.Def.InternalType;
            if (dataTypeString == "f32")
            {
                float valueRead = 0f;
                NativeWrapper.ReadProcessMemory<float>(memoryHandlerPtr, CellDataPtr, ref valueRead);

                float value = Convert.ToSingle(cell.Value);
                if (valueRead != value)
                {
                    log.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    NativeWrapper.WriteProcessMemory<float>(memoryHandlerPtr, CellDataPtr, ref value);
                }
                return sizeof(float);
            }
            else if (dataTypeString == "s32")
            {
                Int32 valueRead = 0;
                NativeWrapper.ReadProcessMemory<Int32>(memoryHandlerPtr, CellDataPtr, ref valueRead);

                Int32 value = Convert.ToInt32(cell.Value);
                if (valueRead != value)
                {
                    log.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    NativeWrapper.WriteProcessMemory<Int32>(memoryHandlerPtr, CellDataPtr, ref value);
                }
                return sizeof(Int32);
            }
            else if (dataTypeString == "s16")
            {
                Int16 valueRead = 0;
                NativeWrapper.ReadProcessMemory<Int16>(memoryHandlerPtr, CellDataPtr, ref valueRead);

                Int16 value = Convert.ToInt16(cell.Value);
                if (valueRead != value)
                {
                    log.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    NativeWrapper.WriteProcessMemory<Int16>(memoryHandlerPtr, CellDataPtr, ref value);
                }
                return sizeof(Int16);
            }
            else if (dataTypeString == "s8")
            {
                sbyte valueRead = 0;
                NativeWrapper.ReadProcessMemory<sbyte>(memoryHandlerPtr, CellDataPtr, ref valueRead);

                sbyte value = Convert.ToSByte(cell.Value);
                if (valueRead != value)
                {
                    log.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    NativeWrapper.WriteProcessMemory<sbyte>(memoryHandlerPtr, CellDataPtr, ref value);
                }
                return sizeof(sbyte);
            }
            else if (dataTypeString == "u32")
            {
                UInt32 valueRead = 0;
                NativeWrapper.ReadProcessMemory<UInt32>(memoryHandlerPtr, CellDataPtr, ref valueRead);

                UInt32 value = Convert.ToUInt32(cell.Value);
                if (valueRead != value)
                {
                    log.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    NativeWrapper.WriteProcessMemory<UInt32>(memoryHandlerPtr, CellDataPtr, ref value);
                }
                return sizeof(UInt32);
            }
            else if (dataTypeString == "u16")
            {
                UInt16 valueRead = 0;
                NativeWrapper.ReadProcessMemory<UInt16>(memoryHandlerPtr, CellDataPtr, ref valueRead);

                UInt16 value = Convert.ToUInt16(cell.Value);
                if (valueRead != value)
                {
                    log.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    NativeWrapper.WriteProcessMemory<UInt16>(memoryHandlerPtr, CellDataPtr, ref value);
                }
                return sizeof(UInt16);
            }
            else if (dataTypeString == "u8")
            {
                if (cell.Def.BitSize != -1)
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
                        NativeWrapper.ReadProcessMemory<byte>(memoryHandlerPtr, CellDataPtr, ref valueRead);

                        byte[] bitFieldByte = new byte[1];
                        bits.CopyTo(bitFieldByte, 0);
                        bitFieldPos = 0;
                        byte bitbuffer = bitFieldByte[0];
                        if (valueRead != bitbuffer)
                        {
                            log.WriteLine($"Field={cell.Def.DisplayName} OldValue={new BitArray(valueRead).ToString()} NewValue={new BitArray(bitbuffer).ToString()}");
                            NativeWrapper.WriteProcessMemory<byte>(memoryHandlerPtr, CellDataPtr, ref bitbuffer);
                        }
                        return sizeof(byte);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    byte valueRead = 0;
                    NativeWrapper.ReadProcessMemory<byte>(memoryHandlerPtr, CellDataPtr, ref valueRead);

                    byte value = Convert.ToByte(cell.Value);
                    if (valueRead != value)
                    {
                        log.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                        NativeWrapper.WriteProcessMemory<byte>(memoryHandlerPtr, CellDataPtr, ref value);
                    }
                    return sizeof(byte);
                }
            }
            else if (dataTypeString == "dummy8" || dataTypeString == "fixstr" || dataTypeString == "fixstrW")
            {
                return cell.Def.ArrayLength;
            }
            else
            {
                throw new Exception("Yer code is dumb.");
            }
        }
    }
}
