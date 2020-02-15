using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimeSoftware
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
                    ClientCMD.Exec("+use");  // I did not add this to LocalPlayer to avoid delay. But i dont try, maybe this will not happen lol
                    Thread.Sleep(15);
                    ClientCMD.Exec("-use");
                    Thread.Sleep(15);
                }
                
            }
        }
    }
}
