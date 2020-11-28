using System.Threading;
using AnimeSoftware.Injections;
using AnimeSoftware.Objects;

namespace AnimeSoftware.Hacks
{
    class RunboostBot
    {
        public static bool boosting = false;
        public static void Start() //disabled
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!Properties.Settings.Default.runboostbot)
                    continue;

                if (!LocalPlayer.InGame)
                    continue;

                if (LocalPlayer.Health <= 0)
                    continue;

                if (LocalPlayer.Dormant)
                    continue;

                Entity target = null;

                while ((DllImport.GetAsyncKeyState(Properties.Hotkey.Default.runboostbotKey) & 0x8000) != 0)
                {
                    Thread.Sleep(1);

                    if (target == null)
                    {
                        target = Aimbot.BestDistance();
                    }

                    boosting = true;

                    Vector position = target.Position;

                    if (target.Speed <= 0)
                    {
                        LocalPlayer.MoveClearX();
                    }
                    else
                    {
                        LocalPlayer.ViewAngleY = Aimbot.NormalizedAngle(Aimbot.CalcAngle(LocalPlayer.ViewPosition, position)).y;

                        LocalPlayer.MoveForward();
                    }
                    
                }

                if (boosting)
                {
                    LocalPlayer.MoveClearX();
                    boosting = false;
                }

                
            }
        }
    }
}
