using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class GAME_REND
    {
        public class GROUP_MASK
        {
            public static void ShowMapParts(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4555CF0, State);
            }

            public static void ShowObjects(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4555CF1, State);
            }

            public static void ShowCharacter(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4555CF2, State);
            }

            public static void ShowSFX(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4555CF3, State);
            }

            public static void ShowRemo(bool State)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4555CF4, State);
            }
        }
    }
}
