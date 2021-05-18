using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoulsMemory
{
    public class ChrCam
    {
        internal static IntPtr GetChrCamPtr()
        {
            var ChrCamPtr_ = (IntPtr)CHR_INS.WorldChrMan.GetWorldChrManBase();

            ChrCamPtr_ = IntPtr.Add(ChrCamPtr_, 0x31A0);
            ChrCamPtr_ = new IntPtr(Memory.ReadInt64(ChrCamPtr_));
            return ChrCamPtr_;
        }

        public static void SetLerpTime(float LerpTime)
        {
            var ChrCamPtr = GetChrCamPtr();

            Memory.WriteFloat(ChrCamPtr + 0x8C, LerpTime);
        }

        public class ChrFollowCam
        {
            internal static IntPtr GetFollowCamPtr()
            {
                var ChrFollowCam = GetChrCamPtr();

                ChrFollowCam = IntPtr.Add(ChrFollowCam, 0x60);
                ChrFollowCam = new IntPtr(Memory.ReadInt64(ChrFollowCam));
                return ChrFollowCam;
            }

            public static void SetFovY(float FovY)
            {
                var ChrFollowCam = GetFollowCamPtr();

                Memory.WriteFloat(ChrFollowCam + 0x50, FovY);
            }

            public static void SetPlane(float nearPlane, float farPlane)
            {
                var ChrFollowCam = GetFollowCamPtr();

                Memory.WriteFloat(ChrFollowCam + 0x58, nearPlane);
                Memory.WriteFloat(ChrFollowCam + 0x5C, farPlane);
            }

            public static void SetChrOrgOffsetVector3(float ChrOrgOffsetX, float ChrOrgOffsetY, float ChrOrgOffsetZ)
            {
                var ChrFollowCam = GetFollowCamPtr();

                Memory.WriteFloat(ChrFollowCam + 0x170, ChrOrgOffsetX);
                Memory.WriteFloat(ChrFollowCam + 0x174, ChrOrgOffsetY);
                Memory.WriteFloat(ChrFollowCam + 0x178, ChrOrgOffsetZ);
            }

            public static void SetFulcrumParameters(float FulcrumDistRateByLrMoveMaxDist, float FulcrumDistRateByLrMoveMinDist, float FulcrumDistRateByLrMove, float FulcrumDistRate, float FulcrumDist)
            {
                var ChrFollowCam = GetFollowCamPtr();

                Memory.WriteFloat(ChrFollowCam + 0x180, FulcrumDistRateByLrMoveMaxDist);
                Memory.WriteFloat(ChrFollowCam + 0x184, FulcrumDistRateByLrMoveMinDist);
                Memory.WriteFloat(ChrFollowCam + 0x188, FulcrumDistRateByLrMove);
                Memory.WriteFloat(ChrFollowCam + 0x18C, FulcrumDistRate);
                Memory.WriteFloat(ChrFollowCam + 0x190, FulcrumDist);
            }

            public static void SetCamParameters(float CamDist, float CamDistTarget, float CamCastSphereRadius, float CamSafeMarginRate, float CamSafeMarginMax)
            {
                var ChrFollowCam = GetFollowCamPtr();

                Memory.WriteFloat(ChrFollowCam + 0x194, CamDist);
                Memory.WriteFloat(ChrFollowCam + 0x198, CamDistTarget);
                Memory.WriteFloat(ChrFollowCam + 0x19C, CamCastSphereRadius);
                Memory.WriteFloat(ChrFollowCam + 0x1A0, CamSafeMarginRate);
                Memory.WriteFloat(ChrFollowCam + 0x1A4, CamSafeMarginMax);
            }
        }

        public class ChrAimCam
        {
            internal static IntPtr GetAimCamPtr()
            {
                var AimCamPtr = GetChrCamPtr();

                AimCamPtr = IntPtr.Add(AimCamPtr, 0x68);
                AimCamPtr = new IntPtr(Memory.ReadInt64(AimCamPtr));
                return AimCamPtr;
            }

            public static void SetFovY(float FovY)
            {
                var ChrAimCam = GetAimCamPtr();

                Memory.WriteFloat(ChrAimCam + 0x50, FovY);
            }

            public static void SetPlane(float nearPlane, float farPlane)
            {
                var ChrAimCam = GetAimCamPtr();

                Memory.WriteFloat(ChrAimCam + 0x58, nearPlane);
                Memory.WriteFloat(ChrAimCam + 0x5C, farPlane);
            }

            public static void SetZoomParameters(float ZoomInFovY, float ZoomOutFovY, float ZoomRate, float ZoomRateNormalMin, float ZoomSpeed, Vector4 ZoomInOrg, Vector4 ZoomOutOrg)
            {
                var ChrAimCam = GetAimCamPtr();

                Memory.WriteFloat(ChrAimCam + 0x130, ZoomInFovY);
                Memory.WriteFloat(ChrAimCam + 0x134, ZoomOutFovY);
                Memory.WriteFloat(ChrAimCam + 0x138, ZoomRate);
                Memory.WriteFloat(ChrAimCam + 0x13C, ZoomRateNormalMin);
                Memory.WriteFloat(ChrAimCam + 0x140, ZoomSpeed);

                Memory.WriteFloat(ChrAimCam + 0xB0, ZoomInOrg.X);
                Memory.WriteFloat(ChrAimCam + 0xB4, ZoomInOrg.Y);
                Memory.WriteFloat(ChrAimCam + 0xB8, ZoomInOrg.Z);
                Memory.WriteFloat(ChrAimCam + 0xBC, ZoomInOrg.W);

                Memory.WriteFloat(ChrAimCam + 0xC0, ZoomOutOrg.X);
                Memory.WriteFloat(ChrAimCam + 0xC4, ZoomOutOrg.Y);
                Memory.WriteFloat(ChrAimCam + 0xC8, ZoomOutOrg.Z);
                Memory.WriteFloat(ChrAimCam + 0xCC, ZoomOutOrg.W);

            }

            public static void SetBlendRate(Vector4 BlendRateP, Vector4 BlendRateM)
            {
                var ChrAimCam = GetAimCamPtr();

                Memory.WriteFloat(ChrAimCam + 0xD0, BlendRateP.X);
                Memory.WriteFloat(ChrAimCam + 0xD4, BlendRateP.Y);
                Memory.WriteFloat(ChrAimCam + 0xD8, BlendRateP.Z);
                Memory.WriteFloat(ChrAimCam + 0xDC, BlendRateP.W);

                Memory.WriteFloat(ChrAimCam + 0xE0, BlendRateM.X);
                Memory.WriteFloat(ChrAimCam + 0xE4, BlendRateM.Y);
                Memory.WriteFloat(ChrAimCam + 0xE8, BlendRateM.Z);
                Memory.WriteFloat(ChrAimCam + 0xEC, BlendRateM.W);

            }

            public static void SetTmpAngle(Vector2 TmpTgtAng, Vector2 TmpChrAng, Vector2 TmpCamAng)
            {
                var ChrAimCam = GetAimCamPtr();

                Memory.WriteFloat(ChrAimCam + 0x18C, TmpTgtAng.X);
                Memory.WriteFloat(ChrAimCam + 0x190, TmpTgtAng.Y);

                Memory.WriteFloat(ChrAimCam + 0x19C, TmpChrAng.X);
                Memory.WriteFloat(ChrAimCam + 0x1A0, TmpChrAng.Y);

                Memory.WriteFloat(ChrAimCam + 0x1AC, TmpCamAng.X);
                Memory.WriteFloat(ChrAimCam + 0x1B0, TmpCamAng.Y);

            }
        }
    }
}
