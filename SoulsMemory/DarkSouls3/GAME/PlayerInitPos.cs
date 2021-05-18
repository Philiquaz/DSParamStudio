using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class PlayerInitPos
    {
        internal static IntPtr GetInitPosPtr()
        {
            var InitPosPtr = IntPtr.Add(Memory.BaseAddress, 0x4743AB0);
            InitPosPtr = new IntPtr(Memory.ReadInt64(InitPosPtr));
            return InitPosPtr;
        }

        public static int GetNextMapId()
        {
            var BasePtr = GetInitPosPtr();

            return Memory.ReadInt32(BasePtr + 0xC);
        }

        public static int GetEventIdOfInitPos()
        {
            var BasePtr = GetInitPosPtr();

            return Memory.ReadInt32(BasePtr + 0x14);
        }

        public static int GetInvasionChrType()
        {
            var BasePtr = GetInitPosPtr();

            return Memory.ReadInt32(BasePtr + 0xC54);
        }

        public static byte GetIsStartFromSummonSign()
        {
            var BasePtr = GetInitPosPtr();

            return Memory.ReadInt8(BasePtr + 0xA64);
        }

        public static byte GetIsSummoned()
        {
            var BasePtr = GetInitPosPtr();

            return Memory.ReadInt8(BasePtr + 0xAC7);
        }
    }
}
