using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using StudioCore.MsbEditor;
using PARAM = SoulsMemory.PARAM;
using System.Diagnostics;

namespace StudioCore
{
    class ParamReloader
    {
        public static void ReloadMemoryParamsDS3()
        {
            var processArray = Process.GetProcessesByName("DarkSoulsIII");
            if (processArray.Any())
            {
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
            Type dataType;
            if (dataTypeString == "f32")
            {
                float valueRead = SoulsMemory.Memory.ReadFloat(CellDataPtr);
                float value = Convert.ToSingle(cell.Value);
                if (valueRead != value)
                {
                    Debug.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    SoulsMemory.Memory.WriteFloat(CellDataPtr, value);
                }
                return sizeof(float);
            }
            else if (dataTypeString == "s32")
            {
                Int32 valueRead = SoulsMemory.Memory.ReadInt32(CellDataPtr);
                Int32 value = Convert.ToInt32(cell.Value);
                if (valueRead != value)
                {
                    Debug.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    SoulsMemory.Memory.WriteInt32(CellDataPtr, value);
                }
                return sizeof(Int32);
            }
            else if (dataTypeString == "s16")
            {
                Int16 valueRead = SoulsMemory.Memory.ReadInt16(CellDataPtr);
                Int16 value = Convert.ToInt16(cell.Value);
                if (valueRead != value)
                {
                    Debug.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    SoulsMemory.Memory.WriteInt16(CellDataPtr, value);
                }
                return sizeof(Int16);
            }
            else if (dataTypeString == "s8")
            {
                sbyte valueRead = SoulsMemory.Memory.ReadInt8(CellDataPtr);
                sbyte value = Convert.ToSByte(cell.Value);
                if (valueRead != value)
                {
                    Debug.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    SoulsMemory.Memory.WriteInt8(CellDataPtr, value);
                }
                return sizeof(sbyte);
            }
            else if (dataTypeString == "u32")
            {
                UInt32 valueRead = SoulsMemory.Memory.ReadUInt32(CellDataPtr);
                UInt32 value = Convert.ToUInt32(cell.Value);
                if (valueRead != value)
                {
                    Debug.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    SoulsMemory.Memory.WriteUInt32(CellDataPtr, value);
                }
                return sizeof(UInt32);
            }
            else if (dataTypeString == "u16")
            {
                UInt16 valueRead = SoulsMemory.Memory.ReadUInt16(CellDataPtr);
                UInt16 value = Convert.ToUInt16(cell.Value);
                if (valueRead != value)
                {
                    Debug.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                    SoulsMemory.Memory.WriteUInt16(CellDataPtr, value);
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
                        byte valueRead = SoulsMemory.Memory.ReadUInt8(CellDataPtr);

                        byte[] bitFieldByte = new byte[1];
                        bits.CopyTo(bitFieldByte, 0);
                        bitFieldPos = 0;
                        byte bitbuffer = bitFieldByte[0];
                        if (valueRead != bitbuffer)
                        {
                            Debug.WriteLine($"Field={cell.Def.DisplayName} OldValue={new BitArray(valueRead).ToString()} NewValue={new BitArray(bitbuffer).ToString()}");
                            SoulsMemory.Memory.WriteUInt8(CellDataPtr, bitbuffer);
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
                    byte valueRead = SoulsMemory.Memory.ReadUInt8(CellDataPtr);
                    byte value = Convert.ToByte(cell.Value);
                    if (valueRead != value)
                    {
                        Debug.WriteLine($"Field={cell.Def.DisplayName} OldValue={valueRead} NewValue={value}");
                        SoulsMemory.Memory.WriteUInt8(CellDataPtr, value);
                    }
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
}
