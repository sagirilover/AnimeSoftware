using AnimeSoftware.Injections;
using AnimeSoftware.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimeSoftware.Hacks
{
    class DoorSpam
    {
        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(50);

                if (!Properties.Settings.Default.doorspammer)
                    continue;
                if (!LocalPlayer.InGame)
                    continue;
                if (LocalPlayer.Health <= 0)
                    continue;

                while ((DllImport.GetAsyncKeyState(Properties.Hotkey.Default.doorspammerKey) & 0x8000) != 0)
                {

                    LocalPlayer.Use = 5;
                    Thread.Sleep(13);
                    LocalPlayer.Use = 4;
                    Thread.Sleep(13);
                }

            }
        }
    }
}
