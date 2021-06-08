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

namespace AnimeSoftware.Hacks
{
    internal class WeaponSpammer
    {
        public static void Start()
        {
            while (Properties.Settings.Default.weaponspammer)
            {
                Thread.Sleep(10);

                if (!Engine.InGame)
                    continue;

                var lp = new LocalPlayer();
                if(lp.Ptr == IntPtr.Zero)
                    continue;
                
                if (lp.Health <= 0)
                    continue;

                var weapon = lp.ActiveWeapon.Id;
                
                if (Structs.SpamWeaponList.Contains(weapon))
                {
                    if (weapon == 64 || weapon == 262208)
                    {
                        Memory.Write<int>(Memory.Client + Signatures.dwForceAttack, 5);
                        Thread.Sleep(100);
                        Memory.Write<int>(Memory.Client + Signatures.dwForceAttack, 4);
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