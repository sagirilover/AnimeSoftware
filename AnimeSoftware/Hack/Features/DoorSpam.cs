using AnimeSoftware.Injections;
using AnimeSoftware.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimeSoftware.Hack.Models;
using AnimeSoftware.Utils;

namespace AnimeSoftware.Hacks
{
    internal class DoorSpam
    {
        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(50);

                if (!Properties.Settings.Default.doorspammer)
                    continue;
                if (!Engine.InGame)
                    continue;

                var lp = new LocalPlayer();
                if(lp.Ptr == IntPtr.Zero)
                    continue;
                
                if (lp.Health <= 0)
                    continue;

                while ((WinAPI.GetAsyncKeyState(Properties.Hotkey.Default.doorspammerKey) & 0x8000) != 0)
                {
                    lp.Use = 5;
                    Thread.Sleep(13);
                    lp.Use = 4;
                    Thread.Sleep(13);
                }
            }
        }
    }
}