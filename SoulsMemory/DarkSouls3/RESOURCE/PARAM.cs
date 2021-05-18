using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
