using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsMemory
{
    public class GameData
    {
        public enum Gender : byte
        {

        }

        public enum VoiceType : byte
        {

        }

        internal static IntPtr GetGameDataPtr()
        {

            var GetGameDataPtr_ = IntPtr.Add(Memory.BaseAddress, 0x4740178);
            GetGameDataPtr_ = new IntPtr(Memory.ReadInt64(GetGameDataPtr_));
            return GetGameDataPtr_;
        }

        public static float GetPlayTime()
        {
            var GameDataPtr = GetGameDataPtr();

            GameDataPtr = IntPtr.Add(GameDataPtr, 0xA4);

            TimeSpan PlayTime = TimeSpan.FromTicks(Memory.ReadInt32(GameDataPtr));

            var PlayTimeRet = Convert.ToSingle(PlayTime);

            return PlayTimeRet;
        }

        public static int GetNewGameLvl()
        {
            var GameDataPtr = GetGameDataPtr();

            GameDataPtr = IntPtr.Add(GameDataPtr, 0x78);
            var NewGameLvl = Memory.ReadInt32(GameDataPtr);

            return NewGameLvl;
        }

        public static void SetNewGameLvl(int NGLVL)
        {
            var GameDataPtr = GetGameDataPtr();

            Memory.WriteInt32(GameDataPtr + 0x78, NGLVL);
        }

        public static int GetDeathCount()
        {
            var GameDataPtr = GetGameDataPtr();

            GameDataPtr = IntPtr.Add(GameDataPtr, 0x98);
            var DeathCount = Memory.ReadInt32(GameDataPtr);

            return DeathCount;
        }

        public static void TEST_DropItemThatExceedsWeightLimit()
        {
            var buffer = new byte[]
            {
                0x49, 0xBE, 0x20, 0xAE, 0x59, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014059AE20
                0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x28, //add rsp,28
                0xC3 //ret
            };

            Memory.ExecuteFunction(buffer);
        }

        public static bool GetIsBossFight()
        {
            var GameDataPtr = GetGameDataPtr();

            GameDataPtr = IntPtr.Add(GameDataPtr, 0xC0);
            bool IsBossFight = Memory.ReadBoolean(GameDataPtr);

            return IsBossFight;
        }

        public static void ChangeCharaInitParam(int CharacterInitParamId)
        {

            var buffer = new byte[]
            {
                0x48, 0xBA, 0, 0, 0, 0, 0, 0, 0, 0, //mov rdx,Alloc
                0x8B, 0x12, //mov edx,[rdx]
                0x49, 0xBE, 0x40, 0xAE, 0x59, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,000000014059AE40
                0x48, 0xA1, 0x78, 0x01, 0x74, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144740178]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x28, //add rsp,28
                0xC3 //ret
            };

            byte[] ExtraArgument = BitConverter.GetBytes(CharacterInitParamId);

            Memory.ExecuteBufferFunction(buffer, ExtraArgument);
        }

        public class PlayerGameData
        {
            public static void AddInventoryData()
            {

            }

            public static void AddAllInventoryData()
            {

            }

            public class PlayerParam
            {
                internal static IntPtr GetPlayerParamPtr()
                {
                    var PlayerParamPtr = GetGameDataPtr();

                    PlayerParamPtr = IntPtr.Add(PlayerParamPtr, 0x10);
                    PlayerParamPtr = new IntPtr(Memory.ReadInt64(PlayerParamPtr));
                    return PlayerParamPtr;
                }

                public static void SetGender(Gender value)
                {
                    var PlayerParamPtr = GetPlayerParamPtr();

                    Memory.WriteInt8(PlayerParamPtr + 0xAA, (byte)value);
                }

                public static void SetVoiceType(VoiceType value)
                {
                    var PlayerParamPtr = GetPlayerParamPtr();

                    Memory.WriteInt8(PlayerParamPtr + 0xAB, (byte)value);
                }

                public static void SetYoelLvLUpRemainCount(byte value)
                {
                    var PlayerParamPtr = GetPlayerParamPtr();

                    Memory.WriteInt8(PlayerParamPtr + 0x10F, value);
                }

                public static bool GetEmberState()
                {
                    var PlayerParamPtr = GetPlayerParamPtr();

                    PlayerParamPtr = IntPtr.Add(PlayerParamPtr, 0x100);
                    return Memory.ReadBoolean(PlayerParamPtr);
                }

                public static void SetEmberState(bool state)
                {
                    var PlayerParamPtr = GetPlayerParamPtr();

                    Memory.WriteBoolean(PlayerParamPtr + 0x100, state);
                }

                public static void SetGiantTreeLottery(byte value)
                {
                    var PlayerParamPtr = GetPlayerParamPtr();

                    Memory.WriteInt8(PlayerParamPtr + 0x10E, value);
                }
            }
        }
    }
}
