using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class RequestFileReload
    {
        internal static long GetReloadPtr()
        {
            var GetReloadPtr_ = IntPtr.Add(Memory.BaseAddress, 0x4768E78);
            GetReloadPtr_ = new IntPtr(Memory.ReadInt64(GetReloadPtr_));
            return (long)GetReloadPtr_;
        }

        public static void RequestReloadParts()
        {
            var PartsPtr = (IntPtr)GetReloadPtr();

            Memory.WriteFloat(PartsPtr + 0x3048, (float)10);
            Memory.WriteBoolean(PartsPtr + 0x3044, true);
        }

        public static void RequestReloadChr(string ChrName)
        {
            Memory.WriteBoolean(Memory.BaseAddress + 0x4768F7F, true);

            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x48, 0xA1, 0x78, 0x8E, 0x76, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144768E78]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0x10, 0x1E, 0x8D, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,00000001408D1E10
                0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x28, //add rsp,28
                0xC3 //ret
            };

            byte[] ExtraArgument = Encoding.Unicode.GetBytes(ChrName);

            Memory.ExecuteBufferFunction(buffer, ExtraArgument);
        }

        public static void RequestReloadObj(string ObjName)
        {

            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x48, 0xA1, 0xC8, 0x51, 0x74, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[1447451C8]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0x10, 0x1E, 0x8D, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014067FFF0
                0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x28, //add rsp,28
                0xC3 //ret
            };

            byte[] ExtraArgument = Encoding.Unicode.GetBytes(ObjName);

            Memory.ExecuteBufferFunction(buffer, ExtraArgument);
        }
    }
}
