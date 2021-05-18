using System;
using System.Collections.Generic;
using System.Text;
using SoulsMemory;
using ImGuiNET;
using SoulsMemory;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Collections;
using StudioCore;
using StudioCore.MsbEditor;
using SoulsFormats;
using PARAM = SoulsMemory.PARAM;
using System.Diagnostics;

namespace StudioCore
{
    class ExpParamReloader
    {
        public static List<string> meme = null;
        public static void MemoryReadWrite()
        {
            ImGui.Begin("Test");
            if (ImGui.Button("domeme"))
            {
                SoulsMemory.Memory.ProcessHandle = SoulsMemory.Memory.AttachProc("DarkSoulsIII");

                WriteMemoryPARAM(ParamBank.Params["EquipParamProtector"], PARAM.ParamBaseOffset.EquipParamProtector);
            }
            if (meme != null)
            {
                ImGui.Text("test");
                foreach (var item in meme)
                {
                    ImGui.Text(item);
                }
            }
            ImGui.End();
        }
        public static void WriteMemoryPARAM(SoulsFormats.PARAM param, PARAM.ParamBaseOffset enumOffset)
        {
            //var ParamDeXMLFields = ParamDeXML.Root.Element("Fields").Elements();

            //List<ROWMemory> rowList = new List<ROWMemory>();

            var BasePtr = SoulsMemory.PARAM.GetParamPtr(enumOffset);
            var BaseDataPtr = SoulsMemory.PARAM.GetToRowPtr(enumOffset);
            var RowCount = SoulsMemory.PARAM.GetRowCount(enumOffset);

            IntPtr DataSectionPtr;

            int RowId;
            int rowPtr;


            for (int i = 0; i < RowCount; i++)
            {
                RowId = SoulsMemory.Memory.ReadInt32(BaseDataPtr);
                rowPtr = SoulsMemory.Memory.ReadInt32(BaseDataPtr + 0x8);

                DataSectionPtr = IntPtr.Add(BasePtr, rowPtr);

                BaseDataPtr = BaseDataPtr + 0x18;

                //rowList.Add(new ROWMemory(RowId, DataSectionPtr, ParamDeXMLFields));

                SoulsFormats.PARAM.Row row = param[RowId];

                Debug.WriteLine(RowId);
                if (row != null)
                {
                    WriteMemoryRow(row, DataSectionPtr);
                }
            }
        }
        public static void WriteMemoryRow(SoulsFormats.PARAM.Row row ,IntPtr RowDataSectionPtr)
        {
            //this.ID = rowID;

            int offset = 0;
            int bitFieldPos = 0;
            BitArray bits = null;
            List<FIELDMemory> fieldsList = new List<FIELDMemory>();
            foreach (var cell in row.Cells)
            {
                offset += WriteMemoryCell(cell, RowDataSectionPtr + offset, ref bitFieldPos, ref bits);
            }
            //Fields = fieldsList;
        }
        public static int WriteMemoryCell(SoulsFormats.PARAM.Cell cell, IntPtr CellDataPtr, ref int bitFieldPos, ref BitArray bits)
        {
            Debug.WriteLine(cell.Def.DisplayName);
            string dataTypeString = cell.Def.InternalType;
            Type dataType;
            if (dataTypeString == "f32")
            {
                SoulsMemory.Memory.WriteFloat(CellDataPtr, 0f);
                return sizeof(float);
            }
            else if (dataTypeString == "s32")
            {
                SoulsMemory.Memory.WriteInt32(CellDataPtr, 0);
                return sizeof(Int32);
            }
            else if (dataTypeString == "s16")
            {
                SoulsMemory.Memory.WriteInt16(CellDataPtr, 0);
                return sizeof(Int16);
            }
            else if (dataTypeString == "s8")
            {
                WriteInt8(CellDataPtr, 0);
                return sizeof(sbyte);
            }
            else if (dataTypeString == "u32")
            {
                SoulsMemory.Memory.WriteUInt32(CellDataPtr, 0);
                return sizeof(UInt32);
            }
            else if (dataTypeString == "u16")
            {
                SoulsMemory.Memory.WriteUInt16(CellDataPtr, 0);
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
                        byte[] bitFieldByte = new byte[1];
                        bits.CopyTo(bitFieldByte, 0);
                        bitFieldPos = 0;
                        WriteUint8(CellDataPtr, bitFieldByte[0]);
                        return sizeof(byte);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    WriteUint8(CellDataPtr, 0);
                    return sizeof(byte);
                }
            }
            else if (dataTypeString == "dummy8")
            {
                
                return cell.Def.ArrayLength;
            }
            else
            {
                throw new Exception("Yer code is dumb.");
                return 0;
            }
        }
        public static bool WriteInt8(IntPtr address, sbyte value)
        {
            return Kernel32.WriteProcessMemory(SoulsMemory.Memory.ProcessHandle, address, BitConverter.GetBytes(value), (UIntPtr)1, UIntPtr.Zero);
        }

        public static bool WriteUint8(IntPtr address, byte value)
        {
            return Kernel32.WriteProcessMemory(SoulsMemory.Memory.ProcessHandle, address, BitConverter.GetBytes(value), (UIntPtr)1, UIntPtr.Zero);
        }
    }
    public class PARAMSMemory
    {
        private string ParamDexFolderPath;
        public PARAMMemory ClearCountCorrectParam { get; }
        public PARAMMemory SpEffectParam { get; }
        public PARAMMemory RoleParam { get; }

        public PARAMSMemory(string ParamDexFolderPath)
        {
            this.ParamDexFolderPath = ParamDexFolderPath;
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
                string[] Def = element.Attribute("Def").Value.Split(" ");
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
