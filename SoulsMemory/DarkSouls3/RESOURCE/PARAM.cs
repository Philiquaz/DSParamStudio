using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SoulsMemory
{
    public class PARAM
    {
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

        public static IntPtr GetParamPtr(ParamBaseOffset Offset)
        {
            var ParamPtr = IntPtr.Add(Memory.BaseAddress, 0x4782838);
            ParamPtr = new IntPtr(Memory.ReadInt64(ParamPtr));
            ParamPtr = IntPtr.Add(ParamPtr, (int)Offset);
            ParamPtr = new IntPtr(Memory.ReadInt64(ParamPtr));
            ParamPtr = IntPtr.Add(ParamPtr, 0x68);
            ParamPtr = new IntPtr(Memory.ReadInt64(ParamPtr));
            ParamPtr = IntPtr.Add(ParamPtr, 0x68);
            ParamPtr = new IntPtr(Memory.ReadInt64(ParamPtr));

            return ParamPtr;
        }

        public static short GetRowCount(ParamBaseOffset Offset)
        {
            var ParamPtr = GetParamPtr(Offset);

            return Memory.ReadInt16(ParamPtr + 0xA);
        }

        public static IntPtr GetToRowPtr(ParamBaseOffset Offset)
        {
            var ParamPtr = GetParamPtr(Offset);
            ParamPtr = IntPtr.Add(ParamPtr, 0x40);

            return ParamPtr;
        }
        public class PARAMPointers
        {
            private string ParamDexFolderPath;
            public PARAMMemory EquipParamWeapon { get; }
            public PARAMMemory ClearCountCorrectParam { get; }
            public PARAMMemory SpEffectParam { get; }
            public PARAMMemory RoleParam { get; }

            public PARAMPointers(string ParamDexFolderPath)
            {
                this.ParamDexFolderPath = ParamDexFolderPath;
                this.EquipParamWeapon = new PARAMMemory(PARAM.ParamBaseOffset.EquipParamWeapon, XDocument.Load(ParamDexFolderPath + "/" + "EQUIP_PARAM_WEAPON_ST.xml"));
                this.ClearCountCorrectParam = new PARAMMemory(PARAM.ParamBaseOffset.ClearCountCorrectParam, XDocument.Load(ParamDexFolderPath + "/" + "CLEAR_COUNT_CORRECT_PARAM_ST.xml"));
                this.SpEffectParam = new PARAMMemory(PARAM.ParamBaseOffset.SpEffectParam, XDocument.Load(ParamDexFolderPath + "/" + "SP_EFFECT_PARAM_ST.xml"));
                this.RoleParam = new PARAMMemory(PARAM.ParamBaseOffset.RoleParam, XDocument.Load(ParamDexFolderPath + "/" + "ROLE_PARAM_ST.xml"));
            }
        }
        public class PARAMMemory
        {
            public List<ROWMemory> Rows { get; }
            public PARAMMemory(PARAM.ParamBaseOffset paramEnum, XDocument ParamDeXML)
            {
                var ParamDeXMLFields = ParamDeXML.Root.Element("Fields").Elements();

                List<ROWMemory> rowList = new List<ROWMemory>();

                var BasePtr = SoulsMemory.PARAM.GetParamPtr(paramEnum);
                var BaseDataPtr = SoulsMemory.PARAM.GetToRowPtr(paramEnum);
                var RowCount = SoulsMemory.PARAM.GetRowCount(paramEnum);

                IntPtr DataSectionPtr;

                int RowId;
                int rowPtr;


                for (int i = 0; i < RowCount; i++)
                {
                    RowId = SoulsMemory.Memory.ReadInt32(BaseDataPtr);
                    rowPtr = SoulsMemory.Memory.ReadInt32(BaseDataPtr + 0x8);

                    DataSectionPtr = IntPtr.Add(BasePtr, rowPtr);

                    BaseDataPtr = BaseDataPtr + 0x18;

                    rowList.Add(new ROWMemory(RowId, DataSectionPtr, ParamDeXMLFields));
                }
                Rows = rowList;
            }
        }
        public class ROWMemory
        {
            public int ID { get; }
            public List<FIELDMemory> Fields { get; }
            public ROWMemory(int rowID, IntPtr dataPointer, IEnumerable<XElement> paramDeXMLPath)
            {
                this.ID = rowID;

                int offset = 0;
                int bitFieldPos = 0;
                List<FIELDMemory> fieldsList = new List<FIELDMemory>();
                foreach (var element in paramDeXMLPath)
                {
                    string[] Def = element.Attribute("Def").Value.Split(' ');
                    string dataTypeString = Def[0];
                    string fieldName = Def[1];
                    Type dataType;
                    if (dataTypeString == "f32")
                    {
                        fieldsList.Add(new FIELDMemory(fieldName, typeof(float), dataPointer + offset));
                        offset += sizeof(float);
                    }
                    else if (dataTypeString == "s32")
                    {
                        fieldsList.Add(new FIELDMemory(fieldName, typeof(Int32), dataPointer + offset));
                        offset += sizeof(Int32);
                    }
                    else if (dataTypeString == "s16")
                    {
                        fieldsList.Add(new FIELDMemory(fieldName, typeof(Int16), dataPointer + offset));
                        offset += sizeof(Int16);
                    }
                    else if (dataTypeString == "s8")
                    {
                        fieldsList.Add(new FIELDMemory(fieldName, typeof(sbyte), dataPointer + offset));
                        offset += sizeof(sbyte);
                    }
                    else if (dataTypeString == "u32")
                    {
                        fieldsList.Add(new FIELDMemory(fieldName, typeof(UInt32), dataPointer + offset));
                        offset += sizeof(UInt32);
                    }
                    else if (dataTypeString == "u16")
                    {
                        fieldsList.Add(new FIELDMemory(fieldName, typeof(UInt16), dataPointer + offset));
                        offset += sizeof(UInt16);
                    }
                    else if (dataTypeString == "u8")
                    {
                        if (fieldName.Contains(":"))
                        {
                            fieldsList.Add(new FIELDMemory(fieldName, typeof(byte), dataPointer + offset, bitFieldPos));
                            bitFieldPos++;
                            if (bitFieldPos == 8)
                            {
                                bitFieldPos = 0;
                                offset += sizeof(byte);
                            }
                        }
                        else
                        {
                            fieldsList.Add(new FIELDMemory(fieldName, typeof(byte), dataPointer + offset));
                            offset += sizeof(byte);
                        }
                    }
                    else if (dataTypeString == "dummy8")
                    {
                        offset += sizeof(byte) * Convert.ToInt32(fieldName.Split('[', ']')[1]);
                    }
                }
                Fields = fieldsList;
            }
        }
        public class FIELDMemory
        {
            public string fieldName { get; }
            public Type dataType { get; }
            public IntPtr FieldDataPointer { get; }
            public bool isBitField { get; }

            public int bitFieldIndex { get; }
            public FIELDMemory(string fieldName, Type dataType, IntPtr dataPointer)
            {
                this.fieldName = fieldName;
                this.dataType = dataType;
                this.FieldDataPointer = dataPointer;
                isBitField = false;
                bitFieldIndex = -1;
            }

            public FIELDMemory(string fieldName, Type dataType, IntPtr dataPointer, int bitFieldIndex)
            {
                if (bitFieldIndex <= 8)
                    this.bitFieldIndex = bitFieldIndex;
                else
                    this.bitFieldIndex = 8;
                this.fieldName = fieldName;
                this.dataType = dataType;
                isBitField = true;
                this.FieldDataPointer = dataPointer;
            }
        }
    }
}
