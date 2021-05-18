using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class WORLD_SOUND
    {
        public static void EnableSoundDisplay(bool State)
        {
            Memory.WriteBoolean(Memory.BaseAddress + 0x4745233, State);
        }
    }
}
