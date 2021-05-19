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
            SkeletonParam = 0x898,
            SpEffectParam = 0x4A8,
            ClearCountCorrectParam = 0x17C8,
            RoleParam = 0x13D8,
            EquipParamWeapon = 0x70,
            EquipParamProtector = 0xB8
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
