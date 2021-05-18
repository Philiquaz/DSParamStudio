using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class MapLayer
    {
        internal static long GetMapLayerPtr()
        {

            var MapLayerPtr = IntPtr.Add(Memory.BaseAddress, 0x4775D70);
            MapLayerPtr = new IntPtr(Memory.ReadInt64(MapLayerPtr));
            return (long)MapLayerPtr;
        }

        public class Read
        {
            public static int GetMapLayerMask()
            {
                var MapLayerPtr = (IntPtr)GetMapLayerPtr();
                MapLayerPtr = IntPtr.Add(MapLayerPtr, 0x18);

                int ItemCount = Memory.ReadInt32(MapLayerPtr);
                return ItemCount;
            }
        }

        public class Write
        {
            public static void SetMapLayerID(byte MapLayerID)
            {
                var MapLayerPtr = (IntPtr)GetMapLayerPtr();
                Memory.WriteInt32(MapLayerPtr + 0x20, MapLayerID);
            }

            public static void ReloadMapLayer(bool state)
            {
                var MapLayerPtr = (IntPtr)GetMapLayerPtr();
                Memory.WriteBoolean(MapLayerPtr + 0x28, state);
            }
        }
    }
}
