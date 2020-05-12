using System;
using hazedumper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AnimeSoftware.Objects;
using AnimeSoftware.Injections;

namespace AnimeSoftware.Hacks
{
    class BlockBot
    {
        public static bool blocking = false;
        public static bool bb = false;
        public static bool hb = false;
        public static float distanceFactor = 1.5f;
        public static float trajFactor = 0.3f;
        

        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!Properties.Settings.Default.blockbot)
                    continue;

                if (!LocalPlayer.InGame)
                    continue;

                if (LocalPlayer.Health <= 0)
                    continue;

                Entity target = null;

                while ((DllImport.GetAsyncKeyState(Properties.Hotkey.Default.blockbotKey) & 0x8000) != 0)
                {
                    Thread.Sleep(1);
                    if (target == null)
                    {
                        target = Aimbot.BestDistance();
                    }

                    blocking = true;
                    float speed = target.Speed;
                    if ((LocalPlayer.Position - target.BonePosition(8)).Length < 30)
                    {
                        if (LocalPlayer.Flags == 256)
                            continue;

                        if (bb)
                        {
                            LocalPlayer.MoveClearY();
                            bb = false;
                        }

                        hb = true;

                        if (target.Speed == 0 && (LocalPlayer.Position - target.ViewPosition).Length < 10)
                        {
                            LocalPlayer.MoveClearX();
                            continue;
                        }

                        LocalPlayer.ViewAngleY = Aimbot.NormalizedAngle(Aimbot.CalcAngle(LocalPlayer.ViewPosition, target.Position)).y;


                        LocalPlayer.MoveForward();

                    }
                    else
                    {
                        bb = true;
                        Vector3 angle = Aimbot.CalcAngle(LocalPlayer.ViewPosition, target.Position);
                        angle.y -= LocalPlayer.ViewAngle.y;
                        angle = Aimbot.NormalizedAngle(angle);


                        if (speed > 1 || Math.Abs(angle.y) > 1)
                        {
                            if (angle.y < 0.0f)
                            {
                                LocalPlayer.MoveRight();

                            }

                            else if (angle.y > 0.0f)
                            {
                                LocalPlayer.MoveLeft();
                            }
                        }
                        else
                        {
                            LocalPlayer.MoveClearY();
                        }
                    }


                }
                if (blocking || hb || bb)
                {
                    if (hb)
                    {
                        LocalPlayer.MoveClearX();
                        hb = false;
                    }
                    Thread.Sleep(1);
                    if (bb)
                    {
                        LocalPlayer.MoveClearY();
                        bb = false;
                    }
                    blocking = false;
                }


            }
        }
    }
}
