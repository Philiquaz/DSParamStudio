using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class CUTSCENE
    {

        internal static long GetRemoPtr()
        {

            var GetRemoPtr_ = IntPtr.Add(Memory.BaseAddress, 0x477B978);
            GetRemoPtr_ = new IntPtr(Memory.ReadInt64(GetRemoPtr_));
            return (long)GetRemoPtr_;
        }

        public static void RequestCutscene(int AreaNo, int BlockNo, int CutsceneSubId)
        {
            var RemoPtr = (IntPtr)GetRemoPtr();


            Memory.WriteInt32(RemoPtr + 0x18, AreaNo);
            Memory.WriteInt32(RemoPtr + 0x1C, BlockNo);
            Memory.WriteInt32(RemoPtr + 0x28, CutsceneSubId);

            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x48, 0xA1, 0x78, 0xB9, 0x77, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[14477B978]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x41, 0xB8, 0xFF, 0xFF, 0xFF, 0xFF, //mov r8d,FFFFFFFF
                0x49, 0xBE, 0x70, 0x62, 0xCD, 0x40, 0x01, 0x00, 0x00, 0x00,  //mov r14,0000000140CD6270
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

            ExtraArgument[0x3C] = 0xFF;
            ExtraArgument[0x3D] = 0xFF;
            ExtraArgument[0x3E] = 0xFF;
            ExtraArgument[0x3F] = 0xFF;

            Memory.ExecuteBufferFunction(buffer, ExtraArgument);
        }

        public static void EnableLoop(bool state)
        {
            var RemoPtr = (IntPtr)GetRemoPtr();

            RemoPtr = IntPtr.Add(RemoPtr, 0x70);
            RemoPtr = new IntPtr(Memory.ReadInt64(RemoPtr));
            RemoPtr = IntPtr.Add(RemoPtr, 0x18);
            RemoPtr = new IntPtr(Memory.ReadInt64(RemoPtr));
            RemoPtr = IntPtr.Add(RemoPtr, 0x00);
            RemoPtr = new IntPtr(Memory.ReadInt64(RemoPtr));

            Memory.WriteBoolean(RemoPtr + 0x370, state);
        }
    }
}
