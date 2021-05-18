using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class OBJ_INS
    {
        public static void CreateObj(int areaId, int modelId, int createNum, float createHeight, float createLength, float createAngle)
        {
            Memory.WriteInt32(Memory.BaseAddress + 0x47451D0, areaId);
            Memory.WriteInt32(Memory.BaseAddress + 0x47451D4, modelId);
            Memory.WriteInt32(Memory.BaseAddress + 0x4558D8C, createNum);

            Memory.WriteFloat(Memory.BaseAddress + 0x47451EC, createHeight);
            Memory.WriteFloat(Memory.BaseAddress + 0x47451F0, createLength);
            Memory.WriteFloat(Memory.BaseAddress + 0x47451FC, createAngle);

            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x49, 0xBE, 0xF0, 0xE0, 0x67, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014067E0F0
                0x41, 0xB8, 0x4C, 0x04, 0x00, 0x00, //mov r8d,0000044C
                0x48, 0xA1, 0xC8, 0x51, 0x74, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[1447451C8]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
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

        public static void DeleteAllObj()
        {
            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x49, 0xBE, 0xF0, 0xE0, 0x67, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014067E0F0
                0x41, 0xB8, 0x4D, 0x04, 0x00, 0x00, //mov r8d,0000044D
                0x48, 0xA1, 0xC8, 0x51, 0x74, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[1447451C8]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
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

        public static void RestoreAllObj()
        {
            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x49, 0xBE, 0xF0, 0xE0, 0x67, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014067E0F0
                0x41, 0xB8, 0xE9, 0x03, 0x00, 0x00, //mov r8d,000003E9
                0x48, 0xA1, 0xC8, 0x51, 0x74, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[1447451C8]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
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

        public static void DestroyAllObj()
        {
            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x49, 0xBE, 0xF0, 0xE0, 0x67, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014067E0F0
                0x41, 0xB8, 0xE8, 0x03, 0x00, 0x00, //mov r8d,000003E8
                0x48, 0xA1, 0xC8, 0x51, 0x74, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[1447451C8]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
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

        public static void ExtinctionAllObj()
        {
            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x49, 0xBE, 0xF0, 0xE0, 0x67, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014067E0F0
                0x41, 0xB8, 0xEA, 0x03, 0x00, 0x00, //mov r8d,000003EA
                0x48, 0xA1, 0xC8, 0x51, 0x74, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[1447451C8]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x48, 0x83, 0xEC, 0x58, //sub rsp,58
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x58, //add rsp,58
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
