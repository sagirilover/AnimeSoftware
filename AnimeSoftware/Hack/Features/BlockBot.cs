using System;
using AnimeSoftware.Offsets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AnimeSoftware.Hack.Features;
using AnimeSoftware.Hack.Models;
using AnimeSoftware.Objects;
using AnimeSoftware.Injections;
using AnimeSoftware.Utils;

namespace AnimeSoftware.Hacks
{
    internal class BlockBot
    {
        public static bool blocking = false;
        public static bool bb = false;
        public static bool hb = false;
        public static float distanceFactor = 2f;
        public static float trajFactor = 0.45f;

        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!Properties.Settings.Default.blockbot)
                    continue;

                if (!Engine.InGame)
                    continue;

                var lp = new LocalPlayer();

                if (lp.Ptr == IntPtr.Zero || lp.Health <= 0)
                    continue;

                Player target = null;

                var blocked = false;

                while ((WinAPI.GetAsyncKeyState(Properties.Hotkey.Default.blockbotKey) & 0x8000) != 0)
                {
                    if (target == null)
                    {
                        var bestDistance = float.MaxValue;
                        Player result = null;
                        foreach (var player in EntityList.GetPlayers())
                        {
                            if (player.Dormant || player.Health <= 0)
                                continue;

                            var distance = (player.Position - lp.Position).Length2D;
                            if (distance < bestDistance)
                            {
                                result = player;
                                bestDistance = distance;
                            }
                        }

                        target = result;
                        Memory.Write<int>(Memory.Client + Signatures.dwForceRight, 5);
                        Memory.Write<int>(Memory.Client + Signatures.dwForceForward, 5);
                        blocked = true;
                    }

                    if (target == null)
                        continue;

                    if ((lp.Position - target.GetBonePosition(8)).Length < 43)
                    {
                        var targetOrigin = target.Position + target.Velocity * trajFactor;
                        targetOrigin.Z = 0;

                        var localOrigin = lp.Position;
                        localOrigin.Z = 0;

                        var distance = (targetOrigin - localOrigin).Length;
                        distance *= distanceFactor;

                        if (distance > 10)
                            distance = 10;

                        var angle = lp.ViewAngle.Y -
                            Aimbot.CalcAngles(lp.Position, target.Position).Y + 360.0f;

                        lp.SideSpeed = (float) Math.Sin(angle * 0.0174533) * 45 * distance;
                        lp.ForwardSpeed = (float) Math.Cos(angle * 0.0174533) * 45 * distance;
                    }
                    else
                    {
                        if ((WinAPI.GetAsyncKeyState(0x57) & 0x8000) != 0)
                            lp.ForwardSpeed = 450;
                        else
                            lp.ForwardSpeed = 0;
                        var angle = Aimbot.CalcAngles(lp.EyePosition, target.Position);
                        angle.Y -= lp.ViewAngle.Y;
                        angle.NormalizeAngle();

                        var sidemove = -angle.Y * 25;

                        lp.SideSpeed = sidemove > 450 ? 450 : sidemove < -450 ? -450 : sidemove;
                    }
                }

                if (blocked)
                {
                    Memory.Write<int>(Memory.Client + Signatures.dwForceRight, 6);
                    Memory.Write<int>(Memory.Client + Signatures.dwForceForward, 6);
                    lp.SideSpeed = 450;
                    lp.ForwardSpeed = 450;
                }
            }
        }
    }
}