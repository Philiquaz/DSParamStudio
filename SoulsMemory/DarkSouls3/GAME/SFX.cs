using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class SFX
    {
        internal static long GetSfxPtr()
        {
            var GetSfxPtr_ = IntPtr.Add(Memory.BaseAddress, 0x47843C8);
            GetSfxPtr_ = new IntPtr(Memory.ReadInt64(GetSfxPtr_));
            return (long)GetSfxPtr_;
        }

        public static void CreateSfx(int SfxId, float SfxDist, bool IsUseCameraDistOnly, bool ReplaceShByPlayer, bool MoveToLookAtPos)
        {
            var SfxPtr = (IntPtr)GetSfxPtr();


            Memory.WriteInt32(SfxPtr + 0x78, SfxId);
            Memory.WriteFloat(SfxPtr + 0x7C, SfxDist);
            Memory.WriteBoolean(SfxPtr + 0x75, IsUseCameraDistOnly);
            Memory.WriteBoolean(SfxPtr + 0x76, ReplaceShByPlayer);
            Memory.WriteBoolean(SfxPtr + 0x74, MoveToLookAtPos);

            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x41, 0xB8, 0x01, 0x00, 0x00, 0x00, //mov r8d,01
                0x48, 0xA1, 0xC8, 0x43, 0x78, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[1447843C8]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0xA0, 0x0E, 0xE6, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0000000140E60EA0
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

        public static void EraseAllSfx()
        {

            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x41, 0xB8, 0x00, 0x00, 0x00, 0x00, //mov r8d,00
                0x48, 0xA1, 0xC8, 0x43, 0x78, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[1447843C8]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0xA0, 0x0E, 0xE6, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0000000140E60EA0
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

        public class SFX_MOVE
        {
            internal static long GetSfxMovePtr()
            {
                var GetSfxMovePtr_ = (IntPtr)GetSfxPtr();

                GetSfxMovePtr_ = IntPtr.Add(GetSfxMovePtr_, 0xE0);
                GetSfxMovePtr_ = new IntPtr(Memory.ReadInt64(GetSfxMovePtr_));
                return (long)GetSfxMovePtr_;
            }

            public static void AttachTestEffect(bool state)
            {
                var SfxMovePtr = (IntPtr)GetSfxMovePtr();

                Memory.WriteBoolean(SfxMovePtr + 0x461, state);
            }

            public static void SetMovementType(int Type)
            {
                var SfxMovePtr = (IntPtr)GetSfxMovePtr();

                Memory.WriteInt32(SfxMovePtr + 0x464, Type);
            }

            public static void SetSpeed(float Speed)
            {
                var SfxMovePtr = (IntPtr)GetSfxMovePtr();

                Memory.WriteFloat(SfxMovePtr + 0x468, Speed);
            }

            public static void SetRadius(float Radius)
            {
                var SfxMovePtr = (IntPtr)GetSfxMovePtr();

                Memory.WriteFloat(SfxMovePtr + 0x46C, Radius);
            }
        }

    }
}
