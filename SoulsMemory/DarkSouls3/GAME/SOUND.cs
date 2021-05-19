using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class SOUND
    {
        public class SoundTest
        {
            internal static long GetSoundTestPtr()
            {
                var GetSoundTestPtr_ = IntPtr.Add(Memory.BaseAddress, 0x4784B78);
                GetSoundTestPtr_ = new IntPtr(Memory.ReadInt64(GetSoundTestPtr_));
                GetSoundTestPtr_ = IntPtr.Add(GetSoundTestPtr_, 0x60);
                GetSoundTestPtr_ = new IntPtr(Memory.ReadInt64(GetSoundTestPtr_));
                return (long)GetSoundTestPtr_;
            }

            public static void Play3DSound(byte PlayMode, Vector3 Position, int SoundType, int SoundId)
            {
                var SoundTestPtr = (IntPtr)GetSoundTestPtr();

                Memory.WriteUInt8(SoundTestPtr + 0x10, PlayMode);
                Memory.WriteFloat(SoundTestPtr + 0x14, Position.X);
                Memory.WriteFloat(SoundTestPtr + 0x18, Position.Y);
                Memory.WriteFloat(SoundTestPtr + 0x1C, Position.Z);
                Memory.WriteInt32(SoundTestPtr + 0x08, SoundType);
                Memory.WriteInt32(SoundTestPtr + 0x0C, SoundId);

                var buffer = new byte[]
                {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x41, 0xB8, 0x0A, 0x00, 0x00, 0x00, //mov r8d,0A
                0x48, 0xA1, 0x78, 0x4B, 0x78, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144784B78]
                0x48, 0x8B, 0x40, 0x60, //mov rax,[rax+60]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0x70, 0x47, 0xE6, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0000000140E64770
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

            public static void Stop3DSound()
            {
                var SoundTestPtr = (IntPtr)GetSoundTestPtr();

                var buffer = new byte[]
                {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x41, 0xB8, 0x14, 0x00, 0x00, 0x00, //mov r8d,14
                0x48, 0xA1, 0x78, 0x4B, 0x78, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144784B78]
                0x48, 0x8B, 0x40, 0x60, //mov rax,[rax+60]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0x70, 0x47, 0xE6, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0000000140E64770
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
        }
    }
}
