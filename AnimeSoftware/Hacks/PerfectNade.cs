using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnimeSoftware.Injections;
using AnimeSoftware.Objects;
using hazedumper;

namespace AnimeSoftware.Hacks
{
    class PerfectNade
    {
        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(10);

                if (!Properties.Settings.Default.perfectnade)
                    continue;
                if (!LocalPlayer.InGame)
                    continue;
                if (LocalPlayer.Health <= 0)
                    continue;
                if (LocalPlayer.ViewAngle.x != -89)
                    continue;
                if (LocalPlayer.ActiveWeapon != 44)
                    continue;
                if (LocalPlayer.Speed > 3)
                    continue;
                if ((DllImport.GetAsyncKeyState(0x02) & 0x8000) != 0)
                {
                    Thread.Sleep(800);
                    if (!((DllImport.GetAsyncKeyState(0x02) & 0x8000) != 0))
                        continue;
                    ClientCMD.Exec("+attack");
                    Thread.Sleep(80);
                    ClientCMD.Exec("-attack2");
                    Thread.Sleep(1);
                    ClientCMD.Exec("-attack");
                }
            }
        }
    }
}
