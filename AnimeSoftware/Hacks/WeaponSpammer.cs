using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnimeSoftware.Injections;
using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;

namespace AnimeSoftware.Hacks
{
    class WeaponSpammer
    {
        public static void Start()
        {
            while (Properties.Settings.Default.weaponspammer)
            {
                Thread.Sleep(10);

                if (!LocalPlayer.InGame)
                    continue;

                if (LocalPlayer.Health <= 0)
                    continue;

                if (Structs.SpamWeaponList.Contains(LocalPlayer.ActiveWeapon))
                {
                    if (LocalPlayer.ActiveWeapon == 64 || LocalPlayer.ActiveWeapon == 262208)
                    {
                        Memory.Write<int>(Memory.Client + signatures.dwForceAttack, 5);
                        Thread.Sleep(100);
                        Memory.Write<int>(Memory.Client + signatures.dwForceAttack, 4);
                        Thread.Sleep(100);
                    }
                    else
                    {
                        ClientCMD.Exec("+attack2");
                        Thread.Sleep(10);
                        ClientCMD.Exec("-attack2");
                    }
                }
                    
            }
            ClientCMD.Exec("-attack2");
        }
    }
}
