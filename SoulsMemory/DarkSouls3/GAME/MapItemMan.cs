using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoulsMemory
{
    public class MapItemMan
    {
        internal static long GetMapItemBase()
        {
            var ChrDbgBase_ = IntPtr.Add(Memory.BaseAddress, 0x4752300);
            ChrDbgBase_ = new IntPtr(Memory.ReadInt64(ChrDbgBase_));
            return (long)ChrDbgBase_;
        }

        public class Read
        {
            public static int GetDropItemCount()
            {
                var MapItem_ = (IntPtr)GetMapItemBase();
                MapItem_ = IntPtr.Add(MapItem_, 0x14);

                int ItemCount = Memory.ReadInt32(MapItem_);
                return ItemCount;
            }

            public static Dictionary<int, Vector3> GetDropItemIdAndPos()
            {
                var MapItemPtr = (IntPtr)GetMapItemBase();
                MapItemPtr = IntPtr.Add(MapItemPtr, 0x18);
                MapItemPtr = (IntPtr)Memory.ReadInt64(MapItemPtr);

                Vector3 Position;

                Dictionary<int, Vector3> DictionaryList = new Dictionary<int, Vector3>();


                for (int i = 0; i < 30; i++)
                {
                    Position.X = Memory.ReadFloat(MapItemPtr + 0x30);
                    Position.Y = Memory.ReadFloat(MapItemPtr + 0x34);
                    Position.Z = Memory.ReadFloat(MapItemPtr + 0x38);

                    DictionaryList.Add(Memory.ReadInt32(MapItemPtr + 0x58), Position);

                    MapItemPtr = IntPtr.Add(MapItemPtr, 0x0);

                    if ((IntPtr)Memory.ReadInt64(MapItemPtr) != IntPtr.Zero)
                        MapItemPtr = (IntPtr)Memory.ReadInt64(MapItemPtr);
                    else
                        break;

                }

                return DictionaryList;
            }
        }

        public class Write
        {
            public static void DbgDispAllItem(bool state)
            {
                var MapItem_ = (IntPtr)GetMapItemBase();
                Memory.WriteBoolean(MapItem_ + 0x100, state);
            }

        }

        public class MapItemDropChanger
        {
            public class AreaInfo
            {
                public static void AreaId(int AreaId)
                {
                    var MapItemPtr = (IntPtr)GetMapItemBase();
                    MapItemPtr = IntPtr.Add(MapItemPtr, 0x88);
                    MapItemPtr = (IntPtr)Memory.ReadInt64(MapItemPtr);

                    Memory.WriteInt32(MapItemPtr + 0x60, AreaId);
                }

                public static void AddSettingArea()
                {
                    var buffer = new byte[]
                    {
                0x48, 0xA1, 0x00, 0x23, 0x75, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144752300]
                0x48, 0x8B, 0x80, 0x88, 0x00, 0x00, 0x00, //mov rax,[rax+00000088]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0x30, 0x11, 0x60, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0x0000000140601130
                0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x28, //add rsp,28
                0xC3 //ret
                    };

                    Memory.ExecuteFunction(buffer);
                }
            }

            public class ExchangeInfo
            {
                public static void SetItemType(int ItemType)
                {
                    var MapItemPtr = (IntPtr)GetMapItemBase();
                    MapItemPtr = IntPtr.Add(MapItemPtr, 0x88);
                    MapItemPtr = (IntPtr)Memory.ReadInt64(MapItemPtr);

                    Memory.WriteInt32(MapItemPtr + 0x4C, ItemType);
                }

                public static void SetItemId(int ItemId)
                {
                    var MapItemPtr = (IntPtr)GetMapItemBase();
                    MapItemPtr = IntPtr.Add(MapItemPtr, 0x88);
                    MapItemPtr = (IntPtr)Memory.ReadInt64(MapItemPtr);

                    Memory.WriteInt32(MapItemPtr + 0x50, ItemId);
                }

                public static void SetItemLotteryId(int ItemLotteryId)
                {
                    var MapItemPtr = (IntPtr)GetMapItemBase();
                    MapItemPtr = IntPtr.Add(MapItemPtr, 0x88);
                    MapItemPtr = (IntPtr)Memory.ReadInt64(MapItemPtr);

                    Memory.WriteInt32(MapItemPtr + 0x54, ItemLotteryId);
                }

                public static void SetDeterminationFlagId(int DeterminationFlagId)
                {
                    var MapItemPtr = (IntPtr)GetMapItemBase();
                    MapItemPtr = IntPtr.Add(MapItemPtr, 0x88);
                    MapItemPtr = (IntPtr)Memory.ReadInt64(MapItemPtr);

                    Memory.WriteInt32(MapItemPtr + 0x58, DeterminationFlagId);
                }

                public static void SetItemTypeJudgementId(int ItemTypeJudgementId)
                {
                    var MapItemPtr = (IntPtr)GetMapItemBase();
                    MapItemPtr = IntPtr.Add(MapItemPtr, 0x88);
                    MapItemPtr = (IntPtr)Memory.ReadInt64(MapItemPtr);

                    Memory.WriteInt32(MapItemPtr + 0x5C, ItemTypeJudgementId);
                }

                public static void ExecuteExchange()
                {
                    var buffer = new byte[]
                    {
                        0x48, 0xA1, 0x00, 0x23, 0x75, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144752300]
                        0x48, 0x8B, 0x80, 0x88, 0x00, 0x00, 0x00, //mov rax,[rax+00000088]
                        0x48, 0x8B, 0xC8, //mov rcx,rax
                        0x49, 0xBE, 0xF0, 0x10, 0x60, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0x00000001406010F0
                        0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                        0x41, 0xFF, 0xD6, //call r14
                        0x48, 0x83, 0xC4, 0x28, //add rsp,28
                        0xC3 //ret
                    };

                    Memory.ExecuteFunction(buffer);
                }
            }
        }
    }
}
