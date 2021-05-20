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

                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ActionButtonParam"], SoulsParamMemory.ParamBaseOffset.ActionButtonParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["AiSoundParam"], SoulsParamMemory.ParamBaseOffset.AiSoundParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["AtkParam_Npc"], SoulsParamMemory.ParamBaseOffset.AtkParam_Npc)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["AtkParam_Pc"], SoulsParamMemory.ParamBaseOffset.AtkParam_Pc))); //Watchlist:
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["AttackElementCorrectParam"], SoulsParamMemory.ParamBaseOffset.AttackElementCorrectParam))); //Watchlist:
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BehaviorParam"], SoulsParamMemory.ParamBaseOffset.BehaviorParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BehaviorParam_PC"], SoulsParamMemory.ParamBaseOffset.BehaviorParam_PC)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BonfireWarpParam"], SoulsParamMemory.ParamBaseOffset.BonfireWarpParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BudgetParam"], SoulsParamMemory.ParamBaseOffset.BudgetParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["Bullet"], SoulsParamMemory.ParamBaseOffset.Bullet)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["BulletCreateLimitParam"], SoulsParamMemory.ParamBaseOffset.BulletCreateLimitParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CalcCorrectGraph"], SoulsParamMemory.ParamBaseOffset.CalcCorrectGraph)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["Ceremony"], SoulsParamMemory.ParamBaseOffset.Ceremony)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CharaInitParam"], SoulsParamMemory.ParamBaseOffset.CharaInitParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CharMakeMenuListItemParam"], SoulsParamMemory.ParamBaseOffset.CharMakeMenuListItemParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CharMakeMenuTopParam"], SoulsParamMemory.ParamBaseOffset.CharMakeMenuTopParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ClearCountCorrectParam"], SoulsParamMemory.ParamBaseOffset.ClearCountCorrectParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CoolTimeParam"], SoulsParamMemory.ParamBaseOffset.CoolTimeParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["CultSettingParam"], SoulsParamMemory.ParamBaseOffset.CultSettingParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["DecalParam"], SoulsParamMemory.ParamBaseOffset.DecalParam))); //Watchlist:
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["DirectionCameraParam"], SoulsParamMemory.ParamBaseOffset.DirectionCameraParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipMtrlSetParam"], SoulsParamMemory.ParamBaseOffset.EquipMtrlSetParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipParamAccessory"], SoulsParamMemory.ParamBaseOffset.EquipParamAccessory)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipParamGoods"], SoulsParamMemory.ParamBaseOffset.EquipParamGoods)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipParamProtector"], SoulsParamMemory.ParamBaseOffset.EquipParamProtector)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["EquipParamWeapon"], SoulsParamMemory.ParamBaseOffset.EquipParamWeapon)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["FaceGenParam"], SoulsParamMemory.ParamBaseOffset.FaceGenParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["FaceParam"], SoulsParamMemory.ParamBaseOffset.FaceParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["FaceRangeParam"], SoulsParamMemory.ParamBaseOffset.FaceRangeParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["FootSfxParam"], SoulsParamMemory.ParamBaseOffset.FootSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GameAreaParam"], SoulsParamMemory.ParamBaseOffset.GameAreaParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GameProgressParam"], SoulsParamMemory.ParamBaseOffset.GameProgressParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemCategoryParam"], SoulsParamMemory.ParamBaseOffset.GemCategoryParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemDropDopingParam"], SoulsParamMemory.ParamBaseOffset.GemDropDopingParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemDropModifyParam"], SoulsParamMemory.ParamBaseOffset.GemDropModifyParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemeffectParam"], SoulsParamMemory.ParamBaseOffset.GemeffectParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["GemGenParam"], SoulsParamMemory.ParamBaseOffset.GemGenParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["HitEffectSeParam"], SoulsParamMemory.ParamBaseOffset.HitEffectSeParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["HitEffectSfxConceptParam"], SoulsParamMemory.ParamBaseOffset.HitEffectSfxConceptParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["HitEffectSfxParam"], SoulsParamMemory.ParamBaseOffset.HitEffectSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["HPEstusFlaskRecoveryParam"], SoulsParamMemory.ParamBaseOffset.HPEstusFlaskRecoveryParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ItemLotParam"], SoulsParamMemory.ParamBaseOffset.ItemLotParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["KnockBackParam"], SoulsParamMemory.ParamBaseOffset.KnockBackParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["KnowledgeLoadScreenItemParam"], SoulsParamMemory.ParamBaseOffset.KnowledgeLoadScreenItemParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["LoadBalancerDrawDistScaleParam"], SoulsParamMemory.ParamBaseOffset.LoadBalancerDrawDistScaleParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["LockCamParam"], SoulsParamMemory.ParamBaseOffset.LockCamParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["Magic"], SoulsParamMemory.ParamBaseOffset.Magic)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MapMimicryEstablishmentParam"], SoulsParamMemory.ParamBaseOffset.MapMimicryEstablishmentParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MenuOffscrRendParam"], SoulsParamMemory.ParamBaseOffset.MenuOffscrRendParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MenuPropertyLayoutParam"], SoulsParamMemory.ParamBaseOffset.MenuPropertyLayoutParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MenuPropertySpecParam"], SoulsParamMemory.ParamBaseOffset.MenuPropertySpecParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MenuValueTableParam"], SoulsParamMemory.ParamBaseOffset.MenuValueTableParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ModelSfxParam"], SoulsParamMemory.ParamBaseOffset.ModelSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MoveParam"], SoulsParamMemory.ParamBaseOffset.MoveParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MPEstusFlaskRecoveryParam"], SoulsParamMemory.ParamBaseOffset.MPEstusFlaskRecoveryParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MultiHPEstusFlaskBonusParam"], SoulsParamMemory.ParamBaseOffset.MultiHPEstusFlaskBonusParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MultiMPEstusFlaskBonusParam"], SoulsParamMemory.ParamBaseOffset.MultiMPEstusFlaskBonusParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["MultiPlayCorrectionParam"], SoulsParamMemory.ParamBaseOffset.MultiPlayCorrectionParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NetworkMsgParam"], SoulsParamMemory.ParamBaseOffset.NetworkMsgParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NetworkParam"], SoulsParamMemory.ParamBaseOffset.NetworkParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NewMenuColorTableParam"], SoulsParamMemory.ParamBaseOffset.NewMenuColorTableParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NpcAiActionParam"], SoulsParamMemory.ParamBaseOffset.NpcAiActionParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NpcParam"], SoulsParamMemory.ParamBaseOffset.NpcParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["NpcThinkParam"], SoulsParamMemory.ParamBaseOffset.NpcThinkParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ObjActParam"], SoulsParamMemory.ParamBaseOffset.ObjActParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ObjectMaterialSfxParam"], SoulsParamMemory.ParamBaseOffset.ObjectMaterialSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ObjectParam"], SoulsParamMemory.ParamBaseOffset.ObjectParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["PhantomParam"], SoulsParamMemory.ParamBaseOffset.PhantomParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["PlayRegionParam"], SoulsParamMemory.ParamBaseOffset.PlayRegionParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ProtectorGenParam"], SoulsParamMemory.ParamBaseOffset.ProtectorGenParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["RagdollParam"], SoulsParamMemory.ParamBaseOffset.RagdollParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ReinforceParamProtector"], SoulsParamMemory.ParamBaseOffset.ReinforceParamProtector)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ReinforceParamWeapon"], SoulsParamMemory.ParamBaseOffset.ReinforceParamWeapon)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["RoleParam"], SoulsParamMemory.ParamBaseOffset.RoleParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SeMaterialConvertParam"], SoulsParamMemory.ParamBaseOffset.SeMaterialConvertParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ShopLineupParam"], SoulsParamMemory.ParamBaseOffset.ShopLineupParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SkeletonParam"], SoulsParamMemory.ParamBaseOffset.SkeletonParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SpEffectParam"], SoulsParamMemory.ParamBaseOffset.SpEffectParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SpEffectVfxParam"], SoulsParamMemory.ParamBaseOffset.SpEffectVfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["SwordArtsParam"], SoulsParamMemory.ParamBaseOffset.SwordArtsParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["TalkParam"], SoulsParamMemory.ParamBaseOffset.TalkParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ThrowDirectionSfxParam"], SoulsParamMemory.ParamBaseOffset.ThrowDirectionSfxParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["ToughnessParam"], SoulsParamMemory.ParamBaseOffset.ToughnessParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["UpperArmParam"], SoulsParamMemory.ParamBaseOffset.UpperArmParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["WeaponGenParam"], SoulsParamMemory.ParamBaseOffset.WeaponGenParam))); //Watchlist:
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["WepAbsorpPosParam"], SoulsParamMemory.ParamBaseOffset.WepAbsorpPosParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["WetAspectParam"], SoulsParamMemory.ParamBaseOffset.WeaponGenParam)));
                threads.Add(new Thread(() => WriteMemoryPARAM(ParamBank.Params["Wind"], SoulsParamMemory.ParamBaseOffset.Wind)));

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
        private static void WriteMemoryPARAM(SoulsFormats.PARAM param, SoulsParamMemory.ParamBaseOffset enumOffset)
        {
            var BasePtr = memoryHandler.GetParamPtr(enumOffset);
            var BaseDataPtr = memoryHandler.GetToRowPtr(enumOffset);
            var RowCount = memoryHandler.GetRowCount(enumOffset);

            IntPtr DataSectionPtr;

            int RowId = 0;
            int rowPtr = 0;

            for (int i = 0; i < RowCount; i++)
            {
                memoryHandler.ReadProcessMemory(BaseDataPtr, ref RowId);
                memoryHandler.ReadProcessMemory(BaseDataPtr + 0x8, ref rowPtr);

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
            SoulsFormats.PARAMDEF.DefType displayType = cell.Def.DisplayType;
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
                        memoryHandler.ReadProcessMemory<byte>(CellDataPtr, ref valueRead);

                        byte[] bitField = new byte[1];
                        bits.CopyTo(bitField, 0);
                        bitFieldPos = 0;
                        byte bitbuffer = bitField[0];
                        if (valueRead != bitbuffer)
                        {
                            memoryHandler.WriteProcessMemory<byte>(CellDataPtr, ref bitbuffer);
                        }
                        return sizeof(byte);
                    }
                    else
                    {
                        return 0;
                    }
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
                        UInt16 valueRead = 0;
                        memoryHandler.ReadProcessMemory<UInt16>(CellDataPtr, ref valueRead);

                        UInt16[] bitField = new UInt16[1];
                        bits.CopyTo(bitField, 0);
                        bitFieldPos = 0;
                        UInt16 bitbuffer = bitField[0];
                        if (valueRead != bitbuffer)
                        {
                            memoryHandler.WriteProcessMemory<UInt16>(CellDataPtr, ref bitbuffer);
                        }
                        return sizeof(UInt16);
                    }
                    else
                    {
                        return 0;
                    }
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
                        UInt32 valueRead = 0;
                        memoryHandler.ReadProcessMemory<UInt32>(CellDataPtr, ref valueRead);

                        UInt32[] bitField = new UInt32[1];
                        bits.CopyTo(bitField, 0);
                        bitFieldPos = 0;
                        UInt32 bitbuffer = bitField[0];
                        if (valueRead != bitbuffer)
                        {
                            memoryHandler.WriteProcessMemory<UInt32>(CellDataPtr, ref bitbuffer);
                        }
                        return sizeof(UInt32);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            if (displayType == SoulsFormats.PARAMDEF.DefType.f32)
            {
                float valueRead = 0f;
                memoryHandler.ReadProcessMemory<float>(CellDataPtr, ref valueRead);

                float value = Convert.ToSingle(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory<float>(CellDataPtr, ref value);
                }
                return sizeof(float);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.s32)
            {
                Int32 valueRead = 0;
                memoryHandler.ReadProcessMemory<Int32>(CellDataPtr, ref valueRead);

                Int32 value = Convert.ToInt32(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory<Int32>(CellDataPtr, ref value);
                }
                return sizeof(Int32);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.s16)
            {
                Int16 valueRead = 0;
                memoryHandler.ReadProcessMemory<Int16>(CellDataPtr, ref valueRead);

                Int16 value = Convert.ToInt16(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory<Int16>(CellDataPtr, ref value);
                }
                return sizeof(Int16);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.s8)
            {
                sbyte valueRead = 0;
                memoryHandler.ReadProcessMemory<sbyte>(CellDataPtr, ref valueRead);

                sbyte value = Convert.ToSByte(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory<sbyte>(CellDataPtr, ref value);
                }
                return sizeof(sbyte);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.u32)
            {
                UInt32 valueRead = 0;
                memoryHandler.ReadProcessMemory<UInt32>(CellDataPtr, ref valueRead);

                UInt32 value = Convert.ToUInt32(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory<UInt32>(CellDataPtr, ref value);
                }
                return sizeof(UInt32);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.u16)
            {
                UInt16 valueRead = 0;
                memoryHandler.ReadProcessMemory<UInt16>(CellDataPtr, ref valueRead);

                UInt16 value = Convert.ToUInt16(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory<UInt16>(CellDataPtr, ref value);
                }
                return sizeof(UInt16);
            }
            else if (displayType == SoulsFormats.PARAMDEF.DefType.u8)
            {
                byte valueRead = 0;
                memoryHandler.ReadProcessMemory<byte>(CellDataPtr, ref valueRead);

                byte value = Convert.ToByte(cell.Value);
                if (valueRead != value)
                {
                    memoryHandler.WriteProcessMemory<byte>(CellDataPtr, ref value);
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
        private Process gameProcess;
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
        public enum ParamBaseOffset : int
        {
            ActionButtonParam = 0xAD8,
            AiSoundParam = 0xD60,
            AtkParam_Npc = 0x268,
            AtkParam_Pc = 0x2B0,
            AttackElementCorrectParam = 0x1660,
            BehaviorParam = 0x3D0,
            BehaviorParam_PC = 0x418,
            BonfireWarpParam = 0xF10,
            BudgetParam = 0xEC8,
            Bullet = 0x388,
            BulletCreateLimitParam = 0x1780,
            CalcCorrectGraph = 0x8E0,
            Ceremony = 0x1078,
            CharaInitParam = 0x658,
            CharMakeMenuListItemParam = 0x1150,
            CharMakeMenuTopParam = 0x1108,
            ClearCountCorrectParam = 0x17C8,
            CoolTimeParam = 0x1A98,
            CultSettingParam = 0x1468,
            DecalParam = 0xA90,
            DirectionCameraParam = 0x1390,
            EquipMtrlSetParam = 0x6A0,
            EquipParamAccessory = 0x100,
            EquipParamGoods = 0x148,
            EquipParamProtector = 0xB8,
            EquipParamWeapon = 0x70,
            FaceGenParam = 0x6E8,
            FaceParam = 0x730,
            FaceRangeParam = 0x778,
            FootSfxParam = 0x16F0,
            GameAreaParam = 0x850,
            GameProgressParam = 0x1810,
            GemCategoryParam = 0xC40,
            GemDropDopingParam = 0xC88,
            GemDropModifyParam = 0xCD0,
            GemeffectParam = 0xBF8,
            GemGenParam = 0xBB0,
            HitEffectSeParam = 0x1270,
            HitEffectSfxConceptParam = 0x11E0,
            HitEffectSfxParam = 0x1228,
            HPEstusFlaskRecoveryParam = 0x14F8,
            ItemLotParam = 0x5C8,
            KnockBackParam = 0xA00,
            KnowledgeLoadScreenItemParam = 0x18E8,
            LoadBalancerDrawDistScaleParam = 0x1A50,
            LoadBalancerParam = 0x1858,
            LockCamParam = 0x928,
            Magic = 0x460,
            MapMimicryEstablishmentParam = 0x15D0,
            MenuOffscrRendParam = 0x1930,
            MenuPropertyLayoutParam = 0xFA0,
            MenuPropertySpecParam = 0xF58,
            MenuValueTableParam = 0xFE8,
            ModelSfxParam = 0xD18,
            MoveParam = 0x610,
            MPEstusFlaskRecoveryParam = 0x1540,
            MultiHPEstusFlaskBonusParam = 0x1978,
            MultiMPEstusFlaskBonusParam = 0x19C0,
            MultiPlayCorrectionParam = 0x1588,
            NetWorkAreaParam = 0xDF0,
            NetworkMsgParam = 0xE80,
            NetworkParam = 0xE38,
            NewMenuColorTableParam = 0x1198,
            NpcAiActionParam = 0x1738,
            NpcParam = 0x220,
            NpcThinkParam = 0x2F8,
            ObjActParam = 0x970,
            ObjectMaterialSfxParam = 0x18A0,
            ObjectParam = 0x340,
            PhantomParam = 0x10C0,
            PlayRegionParam = 0xDA8,
            ProtectorGenParam = 0xB68,
            RagdollParam = 0x7C0,
            ReinforceParamProtector = 0x1D8,
            ReinforceParamWeapon = 0x190,
            RoleParam = 0x13D8,
            SeMaterialConvertParam = 0x1348,
            ShopLineupParam = 0x808,
            SkeletonParam = 0x898,
            SpEffectParam = 0x4A8,
            SpEffectVfxParam = 0x4F0,
            SwordArtsParam = 0x14B0,
            TalkParam = 0x538,
            ThrowDirectionSfxParam = 0x16A8,
            ToughnessParam = 0x1300,
            UpperArmParam = 0x1618,
            WeaponGenParam = 0xB20,
            WepAbsorpPosParam = 0x12B8,
            WetAspectParam = 0x1420,
            Wind = 0xA48
        }

        public bool ReadProcessMemory<T>(IntPtr baseAddress, ref T buffer) where T : unmanaged
        {
            return NativeWrapper.ReadProcessMemory(memoryHandle, baseAddress, ref buffer);
        }

        public bool WriteProcessMemory<T>(IntPtr baseAddress, ref T buffer) where T : unmanaged
        {
            return NativeWrapper.WriteProcessMemory(memoryHandle, baseAddress, ref buffer);
        }

        public IntPtr GetParamPtr(ParamBaseOffset Offset)
        {
            IntPtr ParamPtr = IntPtr.Add(gameProcess.MainModule.BaseAddress, 0x4782838);
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr, ref ParamPtr);
            ParamPtr = IntPtr.Add(ParamPtr, (int)Offset);
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr, ref ParamPtr);
            ParamPtr = IntPtr.Add(ParamPtr, 0x68);
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr, ref ParamPtr);
            ParamPtr = IntPtr.Add(ParamPtr, 0x68);
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr, ref ParamPtr);

            return ParamPtr;
        }

        public short GetRowCount(ParamBaseOffset Offset)
        {
            IntPtr ParamPtr = GetParamPtr(Offset);

            Int16 buffer = 0;
            NativeWrapper.ReadProcessMemory(memoryHandle, ParamPtr + 0xA, ref buffer);

            return buffer;
        }

        public IntPtr GetToRowPtr(ParamBaseOffset Offset)
        {
            var ParamPtr = GetParamPtr(Offset);
            ParamPtr = IntPtr.Add(ParamPtr, 0x40);

            return ParamPtr;
        }
    }
}
