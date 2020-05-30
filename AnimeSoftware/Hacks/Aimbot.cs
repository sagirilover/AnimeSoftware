using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using AnimeSoftware.Injections;
using AnimeSoftware.Objects;

namespace AnimeSoftware.Hacks
{
    class Aimbot
    {
        public static Vector3 oldPunchAngle = new Vector3();
        public static void Start()
        {
            while (Properties.Settings.Default.aimbot)
            {
                Thread.Sleep(1);

                if (!LocalPlayer.InGame)
                    continue;
                if (LocalPlayer.Health <= 0)
                    continue;
                if (LocalPlayer.Dormant)
                    continue;
                if (!((DllImport.GetAsyncKeyState(0x01) & 0x8000) != 0))
                    continue;

                Entity target = BestFOV(Properties.Settings.Default.fov, Properties.Settings.Default.boneid);

                if (target.Index == -1)
                    continue;

                LocalPlayer.ViewAngle = NormalizedAngle(Smooth(LocalPlayer.ViewAngle,RSC(CalcAngle(LocalPlayer.ViewPosition, target.BonePosition(Properties.Settings.Default.boneid)))));


            }
        }
        public static Vector3 CalcAngle(Vector3 src, Vector3 dst)
        {
            Vector3 angles = new Vector3 { x = 0, y = 0, z = 0 };
            double[] delta = { (src.x - dst.x), (src.y - dst.y), (src.z - dst.z) };
            float hyp = (float)Math.Sqrt(delta[0] * delta[0] + delta[1] * delta[1]);
            angles.x = (float)(Math.Atan(delta[2] / hyp) * 180.0f / Math.PI);
            angles.y = (float)(Math.Atan(delta[1] / delta[0]) * 180.0f / Math.PI);
            if (delta[0] >= 0.0f)
            {
                angles.y += 180.0f;
            }
            return angles;
        }
        public static Vector3 Smooth(Vector3 src, Vector3 dst)
        {
            Vector3 smoothed = dst - src;

            smoothed = src + smoothed/100*Properties.Settings.Default.smooth;

            return smoothed;
        }
        public static Vector3 RSC(Vector3 src)
        {
            src -= LocalPlayer.PunchAngle * 2.0f;
            oldPunchAngle = LocalPlayer.PunchAngle * 2.0f;
            return NormalizedAngle(src);
        }
        public static Entity BestDistance()
        {
            int Index=-1;
            float bestDistance = 999999f, tmpDistance;
            foreach(Entity x in Entity.List())
            {
                if (x.Health <= 0)
                    continue;
                if ((tmpDistance = x.DistanceToPlayer) < bestDistance)
                {
                    bestDistance = tmpDistance;
                    Index = x.Index;
                }
            }

               return new Entity(Index);
        }

        public static Entity BestFOV(float FOV, int boneID = 6)
        {
            int Index = -1;
            float bestFOV = 180f, tmpFOV;
            foreach(Entity x in Entity.List())
            {
                if (x.Health <= 0)
                    continue;
                if (x.Dormant)
                    continue;
                if (!Properties.Settings.Default.friendlyfire && x.isTeam)
                    continue;

                if ((tmpFOV = NormalizedAngle(LocalPlayer.ViewAngle - CalcAngle(LocalPlayer.ViewPosition, x.BonePosition(boneID))).Length) < FOV)
                {
                    if (tmpFOV < bestFOV)
                    {
                        Index = x.Index;
                        bestFOV = tmpFOV;
                    }
                }
            }
            return new Entity(Index);
        }

        public static Vector3 NormalizedAngle(Vector3 src)
        {
            while (src.x > 89.0f)
                src.x -= 180.0f;

            while (src.x < -89.0f)
                src.x += 180.0f;

            while (src.y > 180.0f)
                src.y -= 360.0f;

            while (src.y < -180.0f)
                src.y += 360.0f;

            if (src.y < -180.0f || src.y > 180.0f)
                src.y = 0.0f;

            if (src.x < -89.0f || src.x > 89.0f)
                src.x = 0.0f;

            return src;
        }
    }
}
