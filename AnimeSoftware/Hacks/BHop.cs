using AnimeSoftware.Injections;
using AnimeSoftware.Objects;
using System;
using System.Threading;

namespace AnimeSoftware.Hacks
{
    internal class BHop
    {
        public static bool strafe = false;
        private static readonly Random rnd = new Random();
        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!Properties.Settings.Default.bhop)
                {
                    continue;
                }

                if (!LocalPlayer.InGame)
                {
                    continue;
                }

                if (LocalPlayer.Health <= 0)
                {
                    continue;
                }

                if (LocalPlayer.Speed <= 0)
                {
                    continue;
                }

                Vector oldAngle = LocalPlayer.ViewAngle;

                while ((DllImport.GetAsyncKeyState(0x20) & 0x8000) != 0)
                {
                    Thread.Sleep(1);

                    if (Properties.Settings.Default.autostrafe)
                    {
                        strafe = true;
                        Vector cuurentAngle = LocalPlayer.ViewAngle;
                        if (cuurentAngle.y > oldAngle.y)
                        {
                            LocalPlayer.MoveLeft();
                        }
                        else if (cuurentAngle.y < oldAngle.y)
                        {
                            LocalPlayer.MoveRight();
                        }
                    }

                    if (LocalPlayer.Flags == 257 || LocalPlayer.Flags == 263)
                    {
                        if (rnd.Next(100) < Properties.Settings.Default.bhopChoke)
                        {
                            Thread.Sleep(20);
                        }

                        LocalPlayer.Jump();

                    }
                    oldAngle = LocalPlayer.ViewAngle;
                }
                if (strafe)
                {
                    LocalPlayer.MoveClearY();
                    strafe = false;
                }


            }
        }
    }
}
