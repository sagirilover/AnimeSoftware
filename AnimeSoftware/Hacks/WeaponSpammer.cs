using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;
using System.Linq;
using System.Threading;

namespace AnimeSoftware.Hacks
{
    internal class WeaponSpammer
    {
        public static void Start()
        {
            while (Properties.Settings.Default.weaponspammer)
            {
                Thread.Sleep(10);

                if (!LocalPlayer.InGame)
                {
                    continue;
                }

                if (LocalPlayer.Health <= 0)
                {
                    continue;
                }

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
