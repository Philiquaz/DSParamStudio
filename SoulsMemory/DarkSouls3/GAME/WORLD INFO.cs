using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class WORLD_INFO
    {
        internal static IntPtr GetWorldInfoPtr()
        {
            var WorldInfoPtr = IntPtr.Add(Memory.BaseAddress, 0x4743A80);
            WorldInfoPtr = new IntPtr(Memory.ReadInt64(WorldInfoPtr));
            WorldInfoPtr = IntPtr.Add(Memory.BaseAddress, 0x08);
            WorldInfoPtr = new IntPtr(Memory.ReadInt64(WorldInfoPtr));
            return WorldInfoPtr;
        }

        public static int GetAreaNo()
        {
            var BasePtr = GetWorldInfoPtr();

            return Memory.ReadInt32(BasePtr + 0x8);
        }

        public static int GetBlockNo()
        {
            var BasePtr = GetWorldInfoPtr();

            return Memory.ReadInt32(BasePtr + 0x18);
        }

        public static byte GetIsLock()
        {
            var BasePtr = GetWorldInfoPtr();

            return Memory.ReadInt8(BasePtr + 0x28);
        }
    }
}
