using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class HIT_INS
    {
        public class WORLD_HIT_MAN
        {
            public static void EnableLowPolyColDisplay(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4766C6C, State);
            }

            public static void EnableHighPolyColDisplay(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4766C6D, State);
            }

            public static void EnableDrawGroupValidColDisplay(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4766C71, State);
            }

            public static void EnableDispGroupValidColDisplay(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4766C72, State);
            }

            public static void EnableBackreadGroupValidColDisplay(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4766C73, State);
            }

            public static void EnableObjColDisplay(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4766C6E, State);
            }

            public static void EnableRagdollColDisplay(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4766C6F, State);
            }
        }
    }
}
