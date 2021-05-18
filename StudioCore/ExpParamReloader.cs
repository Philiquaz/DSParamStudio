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
using ProcessMemoryUtilities.Managed;

namespace StudioCore
{
    class ExpParamReloader
    {
        private static IntPtr memoryHandle = (IntPtr)0;
        public static List<string> meme = null;
        public static void MemoryReadWrite()
        {
            ImGui.Begin("Test");
            if (ImGui.Button("do meme"))
            {
                var meme2 = Process.GetProcessesByName("DarkSoulsIII");
                Debug.WriteLine("idklolb4");
                if (meme2.Any())
                {
                    Debug.WriteLine("idklol");
                    memoryHandle = NativeWrapper.OpenProcess(ProcessMemoryUtilities.Native.ProcessAccessFlags.Write, meme2.First().Id);

                    SoulsMemory.Memory.ProcessHandle = SoulsMemory.Memory.AttachProc("DarkSoulsIII");
                    Stopwatch meme = new Stopwatch();
                    meme.Start();

                    WriteMemoryPARAM(ParamBank.Params["ClearCountCorrectParam"], PARAM.ParamBaseOffset.ClearCountCorrectParam);
                    WriteMemoryPARAM(ParamBank.Params["EquipParamProtector"], PARAM.ParamBaseOffset.EquipParamProtector);
                    WriteMemoryPARAM(ParamBank.Params["EquipParamWeapon"], PARAM.ParamBaseOffset.EquipParamWeapon);
                    WriteMemoryPARAM(ParamBank.Params["RoleParam"], PARAM.ParamBaseOffset.RoleParam);
                    WriteMemoryPARAM(ParamBank.Params["SkeletonParam"], PARAM.ParamBaseOffset.SkeletonParam);
                    WriteMemoryPARAM(ParamBank.Params["SpEffectParam"], PARAM.ParamBaseOffset.SpEffectParam);
                    meme.Stop();
                    Debug.WriteLine("It took " + meme.ElapsedMilliseconds + " milliseconds to run this operation");

                    NativeWrapper.CloseHandle(memoryHandle);
                    memoryHandle = (IntPtr)0;
                }
            }
            ImGui.SameLine();
            if (ImGui.Button("read meme"))
            {
                var meme24 = new PARAMSMemory("Assets/Paramdex/DS3/Defs").ClearCountCorrectParam.Rows;
                foreach (var item in meme24)
                {
                    if (item.ID == 0)
                    {
                        foreach (var item2 in item.Fields)
                        {
                            if (item2.dataType == typeof(float))
                            {
                                Debug.WriteLine($"DATATYPE={item2.dataType} BitFieldIndex={item2.bitFieldIndex} FieldName={item2.fieldName} " + SoulsMemory.Memory.ReadFloat(item2.FieldDataPointer).ToString("0.####"));
                            }
                            else if (item2.dataType == typeof(Int32))
                            {
                                Debug.WriteLine($"DATATYPE={item2.dataType} BitFieldIndex={item2.bitFieldIndex} FieldName={item2.fieldName} " + SoulsMemory.Memory.ReadInt32(item2.FieldDataPointer).ToString("0.####"));
                            }
                            else if (item2.dataType == typeof(UInt32))
                            {
                                Debug.WriteLine($"DATATYPE={item2.dataType} BitFieldIndex={item2.bitFieldIndex} FieldName={item2.fieldName} " + SoulsMemory.Memory.ReadUInt32(item2.FieldDataPointer).ToString("0.####"));
                            }
                            else if (item2.dataType == typeof(Int16))
                            {
                                Debug.WriteLine($"DATATYPE={item2.dataType} BitFieldIndex={item2.bitFieldIndex} FieldName={item2.fieldName} " + SoulsMemory.Memory.ReadInt16(item2.FieldDataPointer).ToString("0.####"));
                            }
                            else if (item2.dataType == typeof(UInt16))
                            {
                                Debug.WriteLine($"DATATYPE={item2.dataType} BitFieldIndex={item2.bitFieldIndex} FieldName={item2.fieldName} " + SoulsMemory.Memory.ReadUInt16(item2.FieldDataPointer).ToString("0.####"));
                            }
                            else if (item2.dataType == typeof(SByte))
                            {
                                Debug.WriteLine($"DATATYPE={item2.dataType} BitFieldIndex={item2.bitFieldIndex} FieldName={item2.fieldName} " + ((SByte)SoulsMemory.Memory.ReadInt8(item2.FieldDataPointer)).ToString("0.####"));
                            }
                            else if (item2.dataType == typeof(Byte))
                            {
                                if (item2.isBitField)
                                {
                                    var bits = new BitArray(new byte[] { SoulsMemory.Memory.ReadInt8(item2.FieldDataPointer) });
                                    Debug.WriteLine($"DATATYPE={item2.dataType} BitFieldIndex={item2.bitFieldIndex} FieldName={item2.fieldName} " + bits.Get(item2.bitFieldIndex).ToString());
                                }
                                else
                                {
                                    Debug.WriteLine($"DATATYPE={item2.dataType} BitFieldIndex={item2.bitFieldIndex} FieldName={item2.fieldName} " + SoulsMemory.Memory.ReadInt8(item2.FieldDataPointer).ToString("0.####"));
                                }
                            }
                        }
                    }
                }
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

                SoulsFormats.PARAM.Row row = param[RowId];

                if (row != null)
                {
                    WriteMemoryRow(row, DataSectionPtr);
                }
            }
        }
        public static void WriteMemoryRow(SoulsFormats.PARAM.Row row, IntPtr RowDataSectionPtr)
        {
            int offset = 0;
            int bitFieldPos = 0;
            BitArray bits = null;
            List<FIELDMemory> fieldsList = new List<FIELDMemory>();
            foreach (var cell in row.Cells)
            {
                offset += WriteMemoryCell(cell, RowDataSectionPtr + offset, ref bitFieldPos, ref bits);
            }
        }
        public static int WriteMemoryCell(SoulsFormats.PARAM.Cell cell, IntPtr CellDataPtr, ref int bitFieldPos, ref BitArray bits)
        {
            string dataTypeString = cell.Def.InternalType;
            Type dataType;
            if (dataTypeString == "f32")
            {
                float value = Convert.ToSingle(cell.Value);
                NativeWrapper.WriteProcessMemory<float>(memoryHandle, CellDataPtr, ref value);
                return sizeof(float);
            }
            else if (dataTypeString == "s32")
            {
                Int32 value = Convert.ToInt32(cell.Value);
                NativeWrapper.WriteProcessMemory<Int32>(memoryHandle, CellDataPtr, ref value);
                return sizeof(Int32);
            }
            else if (dataTypeString == "s16")
            {
                Int16 value = Convert.ToInt16(cell.Value);
                NativeWrapper.WriteProcessMemory<Int16>(memoryHandle, CellDataPtr, ref value);
                return sizeof(Int16);
            }
            else if (dataTypeString == "s8")
            {
                sbyte value = Convert.ToSByte(cell.Value);
                NativeWrapper.WriteProcessMemory<sbyte>(memoryHandle, CellDataPtr, ref value);
                return sizeof(sbyte);
            }
            else if (dataTypeString == "u32")
            {
                UInt32 value = Convert.ToUInt32(cell.Value);
                NativeWrapper.WriteProcessMemory<UInt32>(memoryHandle, CellDataPtr, ref value);
                return sizeof(UInt32);
            }
            else if (dataTypeString == "u16")
            {
                UInt16 value = Convert.ToUInt16(cell.Value);
                NativeWrapper.WriteProcessMemory<UInt16>(memoryHandle, CellDataPtr, ref value);
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
                        NativeWrapper.WriteProcessMemoryArray<byte>(memoryHandle, CellDataPtr, bitFieldByte);
                        return sizeof(byte);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    byte value = 0;
                    NativeWrapper.WriteProcessMemory<byte>(memoryHandle, CellDataPtr, ref value);
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
    }
    public class PARAMSMemory
    {
        private string ParamDexFolderPath;
        public PARAMMemory EquipParamWeapon { get; }
        public PARAMMemory ClearCountCorrectParam { get; }
        public PARAMMemory SpEffectParam { get; }
        public PARAMMemory RoleParam { get; }

        public PARAMSMemory(string ParamDexFolderPath)
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
