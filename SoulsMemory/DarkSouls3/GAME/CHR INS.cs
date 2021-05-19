using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoulsMemory
{
    public class CHR_INS
    {
        public class CHRDBG
        {

            internal static IntPtr GetChrDbgBase()
            {
                var ChrDbgBase_ = IntPtr.Add(Memory.BaseAddress, 0x4768F98);
                ChrDbgBase_ = new IntPtr(Memory.ReadInt64(ChrDbgBase_));
                return ChrDbgBase_;
            }

            public static void SetPlayerNoDead(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F68, state);
            }

            public static void SetPlayerExterminate(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F69, state);
            }

            public static void SetAllNoDead(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F70, state);
            }

            public static void SetAllNoDamage(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F71, state);
            }

            public static void SetAllNoHit(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F72, state);
            }

            public static void SetAllNoAttack(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F73, state);
            }

            public static void SetAllNoMove(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F74, state);
            }

            public static void SetAllNoUpdateAi(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F75, state);
            }

            public static void SetAllNoStaminaConsume(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F6A, state);
            }

            public static void SetAllNoMpConsume(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F6B, state);
            }

            public static void SetAllNoArrowConsume(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F6C, state);
            }

            public static void SetAllNoArtsPointConsume(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F77, state);
            }

            public static void SetAllNoMagicQtyConsume(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F6D, state);
            }

            public static void SetAllNoWepProtDurabilityDamage(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F76, state);
            }

            public static void SetPlayerHide(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F6E, state);
            }

            public static void SetPlayerSilence(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F6F, state);
            }

            public static void SetNewKnockBackMode(bool state)
            {
                var ChrDbg_ = GetChrDbgBase();
                Memory.WriteBoolean(ChrDbg_ + 0x135, state);
            }

            public static void DrawFootIK(bool state)
            {
                var ChrDbg_ = GetChrDbgBase();
                Memory.WriteBoolean(ChrDbg_ + 0x6B, state);
            }

            public static void SetForceParryMode(bool state)
            {
                Memory.WriteBoolean(Memory.BaseAddress + 0x4768F78, state);
            }

            public static void EnableIgnitionFunction(bool state)
            {
                var ChrDbg_ = GetChrDbgBase();
                Memory.WriteBoolean(ChrDbg_ + 0x18A, state);
            }

            public static void SwitchAllNoUpdate()
            {
                var buffer = new byte[]
                {
                0x31, 0xD2, //xor edx,edx
                0x49, 0xBE, 0x50, 0x87, 0x8D, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,00000001408D8750
                0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x28, //add rsp,28
                0xC3 //ret
                };

                Memory.ExecuteFunction(buffer);
            }

            public static void AllOmisionMode(byte FramerateDivider)
            {
                Memory.WriteUInt8(Memory.BaseAddress + 0x4768F7A, FramerateDivider);
            }

            public static void SetLv1(int Lv1)
            {
                var ChrDbg_ = GetChrDbgBase();
                Memory.WriteInt32(ChrDbg_ + 0x14, Lv1);
            }

            public static void SetLv2(int Lv2)
            {
                var ChrDbg_ = GetChrDbgBase();
                Memory.WriteInt32(ChrDbg_ + 0x18, Lv2);
            }

            public static void SetLv5(int Lv5)
            {
                var ChrDbg_ = GetChrDbgBase();
                Memory.WriteInt32(ChrDbg_ + 0x1C, Lv5);
            }

            public static void SetLv30(int Lv30)
            {
                var ChrDbg_ = GetChrDbgBase();
                Memory.WriteInt32(ChrDbg_ + 0x20, Lv30);
            }

            public class DbgTeleport
            {

                public static void ForceTeleport(Vector3 Position)
                {
                    var ChrLocal_ = WorldChrMan.WorldChrManPtr.GetPlayerEzState1Ptr();

                    Memory.WriteFloat(ChrLocal_ + 0x190, Position.X);
                    Memory.WriteFloat(ChrLocal_ + 0x194, Position.Y);
                    Memory.WriteFloat(ChrLocal_ + 0x198, Position.Z);

                    Memory.WriteUInt8(ChrLocal_ + 0x18A, 1);
                }
            }
        }

        public class CreateDebugChr
        {
            public enum ChrId : int
            {
                Human = 0,
                EventEntity = 100,
                DialogueEntity = 1000,
                Skeleton1 = 1070,
                Skeleton2 = 1071,
                PusOfMan = 1090,
                HollowDeserter1 = 1100,
                HollowDeserter2 = 1102,
                HollowDeserter3 = 1105,
                GiantSlug = 1130,
                SkeletonElite = 1170,
                ShotelSkeleton = 1180,
                CathedralKnight = 1190,
                Thrall1 = 1200,
                Thrall2 = 1201,
                Ghru1 = 1200,
                Ghru2 = 1201,
                Hollow1 = 1220,
                Evangelist = 1230,
                UndeadSettler1 = 1240,
                UndeadSettler2 = 1241,
                GraveWarden = 1250,
                UndeadJailer = 1260,
                LothricKnight1 = 1280,
                LothricKnight2 = 1281,
                LothricKnight3 = 1282,
                LothricKnight_DregHeap = 1283,
                WingedKnight = 1290,
                BlackKnight = 1300,
                Outrider = 1310,
                CrystalSage = 1320,
                CrystalSage_Archives = 1321,
                Scholar = 1340,
                IrithyllHollow = 1350,
                CrucifiedHollow = 1360,
                SwampHollow = 1370,
                SnakeShaman = 1380,
                SnakeAssassin1 = 1390,
                SnakeAssassin2 = 1391,
                FireKeeper = 1400,
                SilverKnight = 1410,
                Hollow2 = 1430,
                Hollow3 = 1440,
                Hollow4 = 1441,
                Hollow5 = 1442,
                Hollow6 = 1445,
                Hollow7 = 1446,
                Ludleth = 1450,
                Bonewheel = 1470,
                PhantomOutrider = 1480,
                PhantomDancer = 1490,
                Dog1 = 2020,
                Dog2 = 2021,
                PontiffKnight = 2030,
                MonstrosityOfSin = 2040,
                LeechMonster = 2060,
                Wretch = 2070,
                Dog3 = 2080,
                Oceiros = 2090,
                SewerCentipede = 2100,
                Rat = 2110,
                Mimic = 2120,
                Slime1 = 2130,
                Slime2 = 2131,
                Slime3 = 2132,
                Basilisk = 2140,
                CrystalLizard = 2150,
                Pilgrim = 2160,
                Yorsha = 2170,
                Mangrub = 2180,
                Gargoyle1 = 2190,
                Gargoyle2 = 2191,
                Sandworm = 2200,
                Corvian = 2210,
                Jailer = 2230,
                Vordt = 2240,
                SulyvahnBeast = 2250,
                Ballista = 2260,
                GiantCrab = 2270,
                Crab = 2271,
                GiantRat = 2280,
                IrithyllDog = 2290,
                Giant = 3020,
                GreaterGiant = 3021,
                AbyssWatcher = 3040,
                OldDemonKing = 3050,
                FireDemon = 3060,
                DemonGhru1 = 3070,
                DemonGhru2 = 3071,
                PilgrimButterfly = 3080,
                CagedHollow = 3090,
                GiantCrystalLizard = 3100,
                DeepAccursed = 3110,
                ElderGhru = 3120,
                AncientWyvern1 = 3140,
                AncientWyvern2 = 3141,
                DragonslayerArmour = 3160,
                Darkwraith = 3170,
                Andre = 3190,
                ShrineHndmaid = 3200,
                HornBeetle = 3210,
                RockLizard = 3220,
                DemonStatue = 3230,
                ShrineHandmaid = 3250,
                NamelessKing = 5010,
                DemonPrince = 5020,
                DemonPrinceFlame1 = 5021,
                DemonPrinceFlame2 = 5022,
                KingOfTheStorm = 5030,
                Gundyr = 5110,
                PontiffSulyvahn = 5140,
                Aldrich = 5150,
                Wolnir = 5160,
                Greatwood = 5180,
                StrayDemon = 5200,
                Rosaria = 5210,
                Archdeacon = 5220,
                FatDeacon = 5221,
                TallDeacon = 5222,
                Deacon = 5223,
                IrithyllDeacon1 = 5255,
                IrithyllDeacon2 = 5256,
                IrithyllDeacon3 = 5257,
                FireWitch = 5240,
                Lorian_and_Lothric = 5250,
                UnusedLothric = 5251,
                Yhorm = 5260,
                Dancer = 5270,
                SoulOfCinder = 5280,
                FarronFollower = 6000,
                Ariandel = 6010,
                Friede = 6020,
                Greatwold = 6030,
                Snowwolf = 6040,
                SnowwolfSmall = 6050,
                TreeHollow = 6060,
                CorvianKnight = 6070,
                CorvianVillager1 = 6080,
                CorvianVillager2 = 6081,
                GiantFly = 6090,
                MillwoodKnight = 6100,
                Painter1 = 6120,
                Painter2 = 6121,
                IceCrab = 6130,
                GaelFinalPhase = 6200,
                SlaveKnightGael = 6201,
                DarkeaterMidir_Bridge = 6210,
                DarkeaterMidir = 6211,
                Murkman1 = 6230,
                Murkman2 = 6231,
                Murkman3 = 6232,
                Angel = 6240,
                AngelLarva = 6250,
                RingedKnight = 6260,
                HollowPilgrim = 6270,
                Judicator1 = 6280,
                Judicator2 = 6281,
                HollowCleric = 6290,
                Pygmy = 6300,
                Filianore = 6310,
                HaraldLegionnaire = 6320,
                Locust = 6330,
                LocustSmall = 6331
            }
            //ChrCreatorPtr   
            internal static IntPtr GetChrCreatorPtr()
            {
                var LocalPlayer_ = WorldChrMan.GetWorldChrManBase();

                var ChrCreatorPtr = IntPtr.Add(LocalPlayer_, 0x3018);
                ChrCreatorPtr = new IntPtr(Memory.ReadInt64(ChrCreatorPtr));
                return ChrCreatorPtr;
            }

            public static void CreateChr(ChrId chrId, float Length, float Height, float Angle, int NpcParamId, int NpcThinkParamId)
            {
                var ChrPtr = GetChrCreatorPtr();

                Memory.WriteInt32(ChrPtr + 0x1B0, (int)chrId);
                Memory.WriteFloat(ChrPtr + 0x1B4, Length);
                Memory.WriteFloat(ChrPtr + 0x1B8, Height);
                Memory.WriteFloat(ChrPtr + 0x1BC, Angle);
                Memory.WriteInt32(ChrPtr + 0x128, NpcParamId);
                Memory.WriteInt32(ChrPtr + 0x124, NpcThinkParamId);

                var buffer = new byte[]
                {
                0x48, 0xA1, 0x78, 0x8E, 0x76, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144768E78]
                0x48, 0x8B, 0x80, 0x18, 0x30, 0x00, 0x00, //mov rax,[rax+00003018]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0x60, 0x7E, 0xA0, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0x0000000140A07E60
                0xBA, 0x01, 0x00, 0x00, 0x00, //mov edx,00000001
                0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x28, //add rsp,28
                0xC3 //ret
                };

                Memory.ExecuteFunction(buffer);
            }

            public static void EraseChr()
            {
                var buffer = new byte[]
                {
                0x48, 0xA1, 0x78, 0x8E, 0x76, 0x44, 0x01, 0x00, 0x00, 0x00, //mov rax,[144768E78]
                0x48, 0x8B, 0x80, 0x18, 0x30, 0x00, 0x00, //mov rax,[rax+00003018]
                0x48, 0x8B, 0xC8, //mov rcx,rax
                0x49, 0xBE, 0x60, 0x7E, 0xA0, 0x40, 0x01, 0x00, 0x00, 0x00, //mov r14,0x0000000140A07E60
                0xBA, 0x02, 0x00, 0x00, 0x00, //mov edx,00000001
                0x48, 0x83, 0xEC, 0x28, //sub rsp,28
                0x41, 0xFF, 0xD6, //call r14
                0x48, 0x83, 0xC4, 0x28, //add rsp,28
                0xC3 //ret
                };

                Memory.ExecuteFunction(buffer);
            }
        }

        public class WorldChrMan
        {
            internal class WorldChrManPtr
            {
                //base offset for local player
                public static IntPtr GetPlayerBasePtr()
                {
                    var WorldChrMan_ = WorldChrMan.GetWorldChrManBase();

                    var LocalPlayer_ = IntPtr.Add(WorldChrMan_, 0x80);
                    LocalPlayer_ = new IntPtr(Memory.ReadInt64(LocalPlayer_));
                    return LocalPlayer_;
                }
                //Main Offset for a lot of chr structures
                public static IntPtr GetPlayerExtraPtr()
                {
                    var LocalPlayer_ = GetPlayerBasePtr();

                    var LocalPlayerExtra_ = IntPtr.Add(LocalPlayer_, 0x1F90);
                    LocalPlayerExtra_ = new IntPtr(Memory.ReadInt64(LocalPlayerExtra_));
                    return LocalPlayerExtra_;
                }
                //Main Offset for Human Character Data
                public static IntPtr GetPlayerHumanPtr()
                {
                    var LocalPlayer_ = GetPlayerBasePtr();

                    var LocalPlayerHuman_ = IntPtr.Add(LocalPlayer_, 0x1FA0);
                    LocalPlayerHuman_ = new IntPtr(Memory.ReadInt64(LocalPlayerHuman_));
                    return LocalPlayerHuman_;
                }
                //SpecialEffect
                public static IntPtr GetPlayerSpEffectPtr()
                {
                    var LocalPlayer_ = GetPlayerBasePtr();

                    var SpEffect_ = IntPtr.Add(LocalPlayer_, 0x11C8);
                    SpEffect_ = new IntPtr(Memory.ReadInt64(SpEffect_));
                    return SpEffect_;
                }
                //SpecialEffect - List
                public static IntPtr GetPlayerSpEffectListPtr()
                {
                    var LocalPlayer_ = GetPlayerSpEffectPtr();

                    var SpEffectList_ = IntPtr.Add(LocalPlayer_, 0x08);
                    SpEffectList_ = new IntPtr(Memory.ReadInt64(SpEffectList_));
                    return SpEffectList_;
                }
                //Event System Offset
                public static IntPtr GetPlayerEventSystemPtr()
                {
                    var LocalPlayer_ = GetPlayerBasePtr();

                    var LocalPlayerEvent_ = IntPtr.Add(LocalPlayer_, 0x2090);
                    LocalPlayerEvent_ = new IntPtr(Memory.ReadInt64(LocalPlayerEvent_));
                    return LocalPlayerEvent_;
                }
                //Stats and Flags
                public static IntPtr GetPlayerStatsPtr()
                {
                    var LocalPlayerExtra_ = GetPlayerExtraPtr();

                    var LocalPlayerStats_ = IntPtr.Add(LocalPlayerExtra_, 0x18);
                    LocalPlayerStats_ = new IntPtr(Memory.ReadInt64(LocalPlayerStats_));
                    return LocalPlayerStats_;
                }

                //Position and Flags related to position
                public static IntPtr GetPlayerPositionPtr()
                {
                    var LocalPlayerExtra_ = GetPlayerExtraPtr();

                    var LocalPlayerPosition_ = IntPtr.Add(LocalPlayerExtra_, 0x68);
                    LocalPlayerPosition_ = new IntPtr(Memory.ReadInt64(LocalPlayerPosition_));
                    return LocalPlayerPosition_;
                }

                //Wet Control
                public static IntPtr GetPlayerWetControlPtr()
                {
                    var LocalPlayerExtra_ = GetPlayerExtraPtr();

                    var LocalPlayerWetControl_ = IntPtr.Add(LocalPlayerExtra_, 0x108);
                    LocalPlayerWetControl_ = new IntPtr(Memory.ReadInt64(LocalPlayerWetControl_));
                    return LocalPlayerWetControl_;
                }

                //EzState_ActionPtr1
                public static IntPtr GetPlayerEzState1Ptr()
                {
                    var LocalPlayerBase_ = GetPlayerBasePtr();

                    var EzState1_ = IntPtr.Add(LocalPlayerBase_, 0x50);
                    EzState1_ = new IntPtr(Memory.ReadInt64(EzState1_));
                    return EzState1_;
                }

                //EzState_ActionPtr2
                public static IntPtr GetPlayerEzState2Ptr()
                {
                    var LocalPlayerEzState1_ = GetPlayerEzState1Ptr();

                    var EzState2_ = IntPtr.Add(LocalPlayerEzState1_, 0x48);
                    EzState2_ = new IntPtr(Memory.ReadInt64(EzState2_));
                    return EzState2_;
                }

                //Toughness
                public static IntPtr GetPlayerToughnessPtr()
                {
                    var LocalPlayerExtra_ = GetPlayerExtraPtr();

                    var Toughness_ = IntPtr.Add(LocalPlayerExtra_, 0x48);
                    Toughness_ = new IntPtr(Memory.ReadInt64(Toughness_));
                    return Toughness_;
                }

                //SuperArmor
                public static IntPtr GetPlayerSuperArmorPtr()
                {
                    var LocalPlayerExtra_ = GetPlayerExtraPtr();

                    var SuperArmor_ = IntPtr.Add(LocalPlayerExtra_, 0x40);
                    SuperArmor_ = new IntPtr(Memory.ReadInt64(SuperArmor_));
                    return SuperArmor_;
                }
            }

            public static IntPtr GetWorldChrManBase()
            {
                var WorldChrMan = IntPtr.Add(Memory.BaseAddress, 0x4768E78);
                WorldChrMan = new IntPtr(Memory.ReadInt64(WorldChrMan));
                return WorldChrMan;
            }

            public static void SetDeathCam(bool State)
            {
                var DeathCamPtr = GetWorldChrManBase();

                Memory.WriteBoolean(DeathCamPtr + 0x90, State);
            }

            public static void SetMaxValidateNum(int num)
            {
                var MaxValidPtr = GetWorldChrManBase();

                Memory.WriteInt32(MaxValidPtr + 0x2FF0, num);
            }

            public class Draw
            {
                public enum CompulsionType : int
                {
                    Normal = 0,
                    Hollow = 2
                }

                public class Write
                {
                    //Toggle Draw Flag
                    public static void ToggleDraw(bool State)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFlags8(LocalPlayer_ + 0x1EE9, State, Memory.Startbit.Bit6);
                    }
                    //Play Region ParamID
                    public static void SetPlayRegionId(int regionId)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteInt32(LocalPlayer_ + 0x1ABC, regionId);
                    }
                    //Chr Ghost
                    public static void ChrFade(float fadeVal)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x1A44, fadeVal);
                    }
                    //Phantom Param Id (For Debug)
                    public static void SetPhantomParamId(int PhantomParamId)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteInt32(LocalPlayer_ + 0x1F38, PhantomParamId);
                    }
                    //WetParam Id (For Debug)
                    public static void SetWetAspectParamId(int WetAspectParamId)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteInt32(LocalPlayer_ + 0x1F3C, WetAspectParamId);
                    }
                    //WaterWetRate
                    public static void SetWaterWetRate(float WaterWetRate)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x1F40, WaterWetRate);
                    }
                    //WaterWetRate(Metal)
                    public static void SetWaterWetRateMetal(float WaterWetRate_Metal)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x1F44, WaterWetRate_Metal);
                    }
                    //WaterWetRate(NonMetal)
                    public static void SetWaterWetRateNonMetal(float WaterWetRateNonMetal)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x1F48, WaterWetRateNonMetal);
                    }



                    //Wet Control Class Inside Draw Option
                    public class WetControl
                    {
                        public static void SetHeightKneePercent(float HeightKneePercent)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteFloat(LocalPlayer_ + 0x40, HeightKneePercent);
                        }

                        public static void SetHeightWaistPercent(float HeightWaistPercent)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteFloat(LocalPlayer_ + 0x44, HeightWaistPercent);
                        }

                        public static void SetWetRateForKnees(float WetRateForKnees)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteFloat(LocalPlayer_ + 0x48, WetRateForKnees);
                        }

                        public static void SetWetRateForWaist(float WetRateForWaist)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteFloat(LocalPlayer_ + 0x4C, WetRateForWaist);
                        }

                        public static void SetHeight(float Height)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteFloat(LocalPlayer_ + 0x10, Height);
                        }

                        public static void SetWeaponWetRate(float WeaponWetRate)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteFloat(LocalPlayer_ + 0x50, WeaponWetRate);
                        }

                        public static void SetWeaponWetTime(float WeaponWetTime)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteFloat(LocalPlayer_ + 0x54, WeaponWetTime);
                        }

                        public static void SetWetFootprintDecalId(int DecalId)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteInt32(LocalPlayer_ + 0x58, DecalId);
                        }

                        public static void SetPoisonWetFootprintDecalId(int DecalId)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteInt32(LocalPlayer_ + 0x5C, DecalId);
                        }

                        public static void SetMarshWetFootprintDecalId1(int DecalId)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteInt32(LocalPlayer_ + 0x60, DecalId);
                        }

                        public static void SetMarshWetFootprintDecalId2(int DecalId)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteInt32(LocalPlayer_ + 0x64, DecalId);
                        }

                        public static void SetOilWetFootprintDecalId(int DecalId)
                        {
                            var LocalPlayer_ = WorldChrManPtr.GetPlayerWetControlPtr();

                            Memory.WriteInt32(LocalPlayer_ + 0x68, DecalId);
                        }
                    }

                    public static void SetCombustible(byte Combustible)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteUInt8(LocalPlayer_ + 0x1F50, Combustible);
                    }

                    public static void SetMaxBurnRate(float MaxBurnRate)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x1F54, MaxBurnRate);
                    }

                    public static void SetMinCombustionRate(float MinCombustionRate)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x1F5C, MinCombustionRate);
                    }

                    public static void SetMaxCombustionRate(float MaxCombustionRate)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x1F60, MaxCombustionRate);
                    }

                    public static void SetEmberEmissiveIntensity(float EmberEmissiveIntensity)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x1F64, EmberEmissiveIntensity);
                    }
                    //Reload Character Materials
                    public static void SetPhantomType(int PhantomType)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteInt32(LocalPlayer_ + 0x1A3C, PhantomType);
                    }

                    public static void IsDropShadow(bool State)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteBoolean(LocalPlayer_ + 0x1A54, State);
                    }

                    public static void IsMotionBlur(bool State)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteBoolean(LocalPlayer_ + 0x1A55, State);
                    }

                    public static void IsTAEfeedbackblur(bool State)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteBoolean(LocalPlayer_ + 0x1A56, State);
                    }

                    public static void SetPlayerOverlookedCompulsion(CompulsionType state)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerBasePtr();

                        Memory.WriteInt32(LocalPlayer_ + 0x2198, (int)state);
                    }
                }
            }


            public class EzStateRequest
            {
                public enum EzStateLadder : int
                {
                    LadderStart_Bottom = 0,
                    LadderStart_Top = 1,
                    Climb_up = 2,
                    Climb_down = 3,
                    Ladder_end_top = 4,
                    Ladder_end_bottom = 5
                }

                public class Write
                {
                    public static void SetEzStateLadder(EzStateLadder State)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerEzState2Ptr();

                        Memory.WriteInt32(LocalPlayer_ + 0x55C, (int)State);
                    }
                    //
                    //missing 3 methods
                    //
                    public static void SetEzStateMessage(int StateMessageId)
                    {
                        var LocalPlayer_ = WorldChrManPtr.GetPlayerEzState2Ptr();

                        Memory.WriteInt32(LocalPlayer_ + 0x56C, StateMessageId);
                    }
                }
            }

            public class ChrBasicInfo
            {
                internal static long GetPosBase()
                {
                    var ChrDbgBase_ = IntPtr.Add(Memory.BaseAddress, 0x4768F98);
                    ChrDbgBase_ = new IntPtr(Memory.ReadInt64(ChrDbgBase_));
                    return (long)ChrDbgBase_;
                }

                public static Vector3 GetPosition()
                {
                    var PosPtr = WorldChrMan.WorldChrManPtr.GetPlayerPositionPtr();

                    Vector3 Pos;

                    Pos.X = Memory.ReadFloat(PosPtr + 0x80);
                    Pos.Y = Memory.ReadFloat(PosPtr + 0x84);
                    Pos.Z = Memory.ReadFloat(PosPtr + 0x88);

                    return Pos;
                }

                public static void SetPosition(Vector3 Position)
                {
                    var PosPtr = WorldChrMan.WorldChrManPtr.GetPlayerPositionPtr();

                    Memory.WriteFloat(PosPtr + 0x80, Position.X);
                    Memory.WriteFloat(PosPtr + 0x84, Position.Y);
                    Memory.WriteFloat(PosPtr + 0x88, Position.Z);
                }

                public static bool isOnGround()
                {
                    var PosPtr = WorldChrMan.WorldChrManPtr.GetPlayerPositionPtr();

                    return Memory.ReadBoolean(PosPtr + 0xA2);
                }

                public static int GetHandleID()
                {
                    var PosPtr = WorldChrMan.WorldChrManPtr.GetPlayerBasePtr();

                    return Memory.ReadInt32(PosPtr + 0x08);
                }

                public static int GetChrType()
                {
                    var PosPtr = WorldChrMan.WorldChrManPtr.GetPlayerBasePtr();

                    return Memory.ReadInt32(PosPtr + 0x70);
                }

                public static int GetTeamType()
                {
                    var PosPtr = WorldChrMan.WorldChrManPtr.GetPlayerBasePtr();

                    return Memory.ReadInt32(PosPtr + 0x74);
                }

                public static void SetChrType(int ChrType)
                {
                    var PosPtr = WorldChrMan.WorldChrManPtr.GetPlayerBasePtr();

                    Memory.WriteInt32(PosPtr + 0x70, ChrType);
                }

                public static void SetTeamType(int TeamType)
                {
                    var PosPtr = WorldChrMan.WorldChrManPtr.GetPlayerBasePtr();

                    Memory.WriteInt32(PosPtr + 0x74, TeamType);
                }
            }

            public class Other
            {
                
            }

            public class Resist
            {
                public class Write
                {

                }
            }

            public class SpecialEffect
            {
                public static List<int> GetSpEffectIds()
                {
                    var LocalPlayer_ = (IntPtr)WorldChrManPtr.GetPlayerSpEffectListPtr();
                    var SpEffectIdPtr = IntPtr.Add(LocalPlayer_, 0x60);

                    List<int> SpEffectIds = new List<int>();


                    for (int i = 0; i < 256; i++)
                    {
                        SpEffectIds.Add(Memory.ReadInt32(SpEffectIdPtr));

                        LocalPlayer_ = IntPtr.Add(LocalPlayer_, 0x78);

                        if ((IntPtr)Memory.ReadInt64(LocalPlayer_) != IntPtr.Zero)
                            LocalPlayer_ = (IntPtr)Memory.ReadInt64(LocalPlayer_);
                        else
                            break;

                        SpEffectIdPtr = IntPtr.Add(LocalPlayer_, 0x60);
                    }

                    return SpEffectIds;
                }
            }

            public class SuperArmor
            {
                public class Read
                {
                    public static float GetRecoverTime()
                    {
                        var LocalPlayer_ = (IntPtr)WorldChrManPtr.GetPlayerSuperArmorPtr();

                        LocalPlayer_ = IntPtr.Add(LocalPlayer_, 0x34);

                        return Memory.ReadFloat(LocalPlayer_);
                    }
                }

                public class Write
                {
                    public static void SetEventSuperArmor(bool state)
                    {
                        var LocalPlayer_ = (IntPtr)WorldChrManPtr.GetPlayerSuperArmorPtr();

                        Memory.WriteBoolean(LocalPlayer_ + 0x10, state);
                    }

                    public static void SetRecoverTime(float RecoverVal)
                    {
                        var LocalPlayer_ = (IntPtr)WorldChrManPtr.GetPlayerSuperArmorPtr();

                        Memory.WriteFloat(LocalPlayer_ + 0x34, RecoverVal);
                    }
                }
            }

            public class Stats
            {

            }

            public class Toughness
            {
                public class Read
                {
                    public static int GetCurrentToughnessId()
                    {
                        var ToughnessPtr = (IntPtr)WorldChrManPtr.GetPlayerToughnessPtr();
                        ToughnessPtr = IntPtr.Add(ToughnessPtr, 0x3C);

                        int ToughnessParamId = Memory.ReadInt32(ToughnessPtr);
                        return ToughnessParamId;
                    }
                }

                public class Write
                {
                    public static void SetSmallCap(float EquipLoadHardcap)
                    {
                        Memory.WriteFloat(Memory.BaseAddress + 0x459EBF4, EquipLoadHardcap);
                    }

                    public static void SetMediumCap(float EquipLoadHardcap)
                    {
                        Memory.WriteFloat(Memory.BaseAddress + 0x459EBF8, EquipLoadHardcap);
                    }

                    public static void SetBigCap(float EquipLoadHardcap)
                    {
                        Memory.WriteFloat(Memory.BaseAddress + 0x459EBFC, EquipLoadHardcap);
                    }

                    public static void SetOversizedCap(float EquipLoadHardcap)
                    {
                        Memory.WriteFloat(Memory.BaseAddress + 0x459EC00, EquipLoadHardcap);
                    }
                }
            }

        }
    }
}
