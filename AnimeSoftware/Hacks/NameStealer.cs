using AnimeSoftware.Objects;
using System.Threading;

namespace AnimeSoftware.Hacks
{
    internal class NameStealer
    {
        public static int fakenametargetid = -1;
        public static bool faked = false;
        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!LocalPlayer.InGame)
                {
                    return;
                }

                if (Properties.Settings.Default.fakefriendlyfire && fakenametargetid != -1)
                {

                    if (LocalPlayer.CrossHair < 64 && LocalPlayer.CrossHair > 0)
                    {
                        if (new Entity(LocalPlayer.CrossHair).isTeam)
                        {
                            if (LocalPlayer.Name != " " + new Entity(fakenametargetid).Name2 + " " && LocalPlayer.Name != new Entity(fakenametargetid).Name2 && !faked)
                            {
                                ConVarManager.StealName(fakenametargetid);
                                faked = true;
                            }

                        }

                    }
                    else
                    {
                        if (faked)
                        {
                            ConVarManager.ChangeName(LocalPlayer.Name);
                            faked = false;
                        }

                    }
                }

                if (Properties.Settings.Default.namestealer)
                {
                    foreach (Entity x in Entity.List())
                    {
                        if (!Properties.Settings.Default.namestealer)
                        {
                            break;
                        }

                        ConVarManager.StealName(x.Index);
                        Thread.Sleep(250);
                    }
                }

            }
        }

    }


}
