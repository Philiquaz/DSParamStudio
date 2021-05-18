using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class Event
    {
        public static void StopPlayer()
        {
            var buffer = new byte[]
            {
                0x49, 0xBE, 0xD0, 0x60, 0x48, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,00000001404860D0
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
                0xC3 //ret
            };

            Memory.ExecuteFunction(buffer);
        }

        public static void CloseMenu()
        {
            var buffer = new byte[]
            {
                0x49, 0xBE, 0x20, 0xDF, 0x47, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014047DF20
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
                0xC3 //ret
            };

            Memory.ExecuteFunction(buffer);
        }

        public static void RevivePlayer()
        {
            var buffer = new byte[]
            {
                0x49, 0xBE, 0x90, 0x52, 0x48, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0000000140485290
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
                0xC3 //ret
            };

            Memory.ExecuteFunction(buffer);
        }

        public static void SetChrTypeDataGreyNext()
        {
            var buffer = new byte[]
            {
                0x49, 0xBE, 0xE0, 0x54, 0x48, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,1404854E0
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
                0xC3 //ret
            };

            Memory.ExecuteFunction(buffer);
        }
    }
}
