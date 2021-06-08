using System;
using System.Threading;
using AnimeSoftware.Hack.Models;
using AnimeSoftware.Utils;

namespace AnimeSoftware.Hack.Features
{
    public static class Aimbot
    {
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!Properties.Settings.Default.aimbot || !Engine.InGame)
                    continue;

                if (!Input.KeyDown(0x01))
                    continue;

                var lp = new LocalPlayer();

                if (lp.Ptr == IntPtr.Zero || lp.Health <= 0 || lp.Dormant)
                    continue;

                var weapon = lp.ActiveWeapon;

                if (weapon.IsBomb() || weapon.IsGrenade() || weapon.IsKnife())
                    continue;

                // if (!weapon.CanFire)
                //     continue;

                var target = GetBestTarget(out var aim, Properties.Settings.Default.friendlyfire);
                var punch = lp.PunchAngle * 2;
                var va = lp.ViewAngle;
                var untouched = va + punch;
                untouched.NormalizeAngle();

                if (target.Ptr == lp.Ptr || CalcFov(untouched, aim) > Properties.Settings.Default.fov)
                    continue;

                if (weapon.IsPistol() && lp.ShotsFired > 0)
                    continue;

                if (!weapon.IsPistol() && !weapon.IsShotgun() & !weapon.IsSniper())
                    aim = aim - punch;

                // aim.NormalizeAngle();

                aim = LinearSmooth(va, aim, Properties.Settings.Default.smooth);

                aim.NormalizeAngle();
                aim.Clamp();

                lp.ViewAngle = aim;
            }
        }

        private static int[] _boneIds = new[]
        {
            8, // head
            7, // neck
            11, // left shoulder
            41, // right shoulder
            6, // body
            78, // left leg
            71, // right leg
        };

        public static Player GetBestTarget(out Vector dst, bool ff = false)
        {
            var lp = new LocalPlayer();
            var result = (Player) lp;
            var team = lp.Team;
            var viewPos = lp.EyePosition;
            float bestFov = float.MaxValue;

            var angle = lp.ViewAngle;
            dst = angle;
            var calcAngle = lp.ViewAngle + lp.PunchAngle * 2f;
            calcAngle.NormalizeAngle();

            foreach (var e in EntityList.GetPlayers())
            {
                if (e.Dormant || e.Health <= 0)
                    continue;

                if (!ff && e.Team == team)
                    continue;

                for (var i = 0; i < _boneIds.Length; i++)
                {
                    var bonePos = e.GetBonePosition(_boneIds[i]);

                    var aim = CalcAngles(viewPos, bonePos); // (bonePos - viewPos).ToAngle();
                    aim.NormalizeAngle();

                    var fov = CalcFov(calcAngle, aim);

                    if (fov < bestFov)
                    {
                        dst = aim;
                        result = e;
                        bestFov = fov;
                    }
                }
            }

            return result;
        }

        public static Vector CalcAngles(Vector src, Vector dst)
        {
            Vector delta = dst - src;
            Vector angles = new Vector()
            {
                X = ExtraMath.RadianToDegrees((float) Math.Atan2(-delta.Z, Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y))),
                Y = ExtraMath.RadianToDegrees((float) Math.Atan2(delta.Y, delta.X)),
                Z = 0
            };
            return angles;
        }

        public static float CalcFov(Vector src, Vector dst)
        {
            var delta = dst - src;
            delta.NormalizeAngle();
            return delta.Length2D;
        }

        public static Vector LinearSmooth(Vector src, Vector dst, float speed)
        {
            if (speed == 0)
                return dst;

            var delta = dst - src;

            if (delta.Length == 0f)
                return src;

            delta.NormalizeAngle();
            delta.Normalize();

            var result = src + delta * speed;

            delta = dst - src;
            delta.NormalizeAngle();

            if (delta.Length - (result - src).Length < 0)
                return dst;

            return result;
        }
    }
}