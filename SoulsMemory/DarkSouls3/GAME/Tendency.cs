using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class Tendency
    {
        public static void WhiteTendecy()
        {
            var buffer = new byte[]
            {
                        0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                        0x48, 0xA1, 0x50, 0xBA, 0x73, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[14473BA50]   
                        0x48, 0x8B, 0xC8, //mov rcx,rax
                        0x49, 0xBE, 0xF0, 0xD3, 0x4A, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,00000001404AD3F0
                        0x41, 0xB8, 0x00, 0x00, 0x00, 0x00, //mov r8d,00
                        0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                        0x41, 0xFF, 0xD6, //call r14
                        0x48, 0x83, 0xC4, 0x28, //add rsp,28
                        0xC3 //ret
            };

            var ExtraArgument = new byte[0x40];

            ExtraArgument[0x00] = 0xFF;
            ExtraArgument[0x3C] = 0xFF;
            ExtraArgument[0x3D] = 0xFF;
            ExtraArgument[0x3E] = 0xFF;
            ExtraArgument[0x3F] = 0xFF;

            Memory.ExecuteBufferFunction(buffer, ExtraArgument);
        }

        public static void BlackTendecy()
        {
            var buffer = new byte[]
            {
                        0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                        0x48, 0xA1, 0x50, 0xBA, 0x73, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[14473BA50]   
                        0x48, 0x8B, 0xC8, //mov rcx,rax
                        0x49, 0xBE, 0xF0, 0xD3, 0x4A, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,00000001404AD3F0
                        0x41, 0xB8, 0x01, 0x00, 0x00, 0x00, //mov r8d,01
                        0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                        0x41, 0xFF, 0xD6, //call r14
                        0x48, 0x83, 0xC4, 0x28, //add rsp,28
                        0xC3 //ret
            };

            var ExtraArgument = new byte[0x40];

            ExtraArgument[0x00] = 0xFF;
            ExtraArgument[0x3C] = 0xFF;
            ExtraArgument[0x3D] = 0xFF;
            ExtraArgument[0x3E] = 0xFF;
            ExtraArgument[0x3F] = 0xFF;

            Memory.ExecuteBufferFunction(buffer, ExtraArgument);
        }
    }
}
