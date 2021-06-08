using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnimeSoftware.Hack.Models;
using AnimeSoftware.Injections;
using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;
using AnimeSoftware.Utils;

namespace AnimeSoftware.Hacks
{
    internal class PerfectNade
    {
        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(10);

                if (!Properties.Settings.Default.perfectnade)
                    continue;
                if (!Engine.InGame)
                    continue;
                
                var lp = new LocalPlayer();
                if(lp.Ptr == IntPtr.Zero)
                    continue;
                
                if (lp.Health <= 0)
                    continue;
                
                if (lp.ViewAngle.X != -89f)
                    continue;
                
                if (lp.ActiveWeapon.Id != 44)
                    continue;
                
                if (lp.Velocity.Length > 3)
                    continue;
                
                if ((WinAPI.GetAsyncKeyState(0x02) & 0x8000) != 0)
                {
                    Thread.Sleep(800);
                    if (!((WinAPI.GetAsyncKeyState(0x02) & 0x8000) != 0))
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