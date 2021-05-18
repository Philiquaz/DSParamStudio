using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class RUMBLE
    {

        internal static long GetRumblePtr()
        {

            var GetRumblePtr_ = IntPtr.Add(Memory.BaseAddress, 0x4752328);
            GetRumblePtr_ = new IntPtr(Memory.ReadInt64(GetRumblePtr_));
            return (long)GetRumblePtr_;
        }

        public static void PlayRumble(int RumbleId)
        {
            var RumblePtr = (IntPtr)GetRumblePtr();


            Memory.WriteInt32(RumblePtr + 0x4228, RumbleId);

            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x48, 0xA1, 0x28, 0x23, 0x75, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144752328]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0x50, 0xFF, 0x7B, 0x40, 0x01, 0x00, 0x00, 0x00,  //mov r14,00000001407BFF50
                0x45, 0x31, 0xC0, //xor r8d,r8d
                0x48, 0x83, 0xEC, 0x38, //sub rsp,38
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x38, //add rsp,38
                0xC3 //ret
            };

            var ExtraArgument = new byte[0x40];

            ExtraArgument[0x00] = 0xFF;
            ExtraArgument[0x18] = 0xFF;
            ExtraArgument[0x19] = 0xFF;
            ExtraArgument[0x1A] = 0xFF;
            ExtraArgument[0x1B] = 0xFF;

            Memory.ExecuteBufferFunction(buffer, ExtraArgument);
        }
    }
}
