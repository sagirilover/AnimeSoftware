using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using hazedumper;

namespace AnimeSoftware
{
    class NameStealer
    {
        public static int fakenametargetid = -1;
        public static bool faked = false;
        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!LocalPlayer.InGame)
                    return;

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
                            break;
                        ConVarManager.StealName(x.Index);
                        Thread.Sleep(250);
                    }
                }

            }
        }

        public static void ChangeName() // pasted from real gamer. another method to change
        {
            string name = "\n\xAD\xAD\xAD";
            byte len = (byte)name.Length;
            byte[] a = { 0x6, (byte)(0x8 + len), 0xA, (byte)(0x6 + len), 0xA, (byte)(0x4 + len), 0x12, len }; // prepend needed bytes
            byte[] b = Encoding.ASCII.GetBytes(name);
            byte[] c = { 0x18, 0x6 }; // add suffix
            var final = new byte[a.Length + b.Length + c.Length]; // combine that shit boyo

            // combine em like a real gamer
            a.CopyTo(final, 0);
            b.CopyTo(final, a.Length);
            c.CopyTo(final, a.Length + b.Length);

            int clientState = Memory.Read<int>(Memory.Engine + signatures.dwClientState);
            int netChan = Memory.Read<int>(clientState + 0x9C);
            int voiceStream = Memory.Read<int>(netChan + 0x78); // voicestream is the biggest nigga stream

            uint tmp = 0;
            int curbit = final.Length * 8;
            Memory.WriteProcessMemory(Memory.pHandle, (IntPtr)voiceStream, final, (IntPtr)final.Length, ref tmp); // write bytes to voicestream data
            Memory.WriteProcessMemory(Memory.pHandle, (IntPtr)netChan + 0x84, BitConverter.GetBytes(curbit), (IntPtr)4, ref tmp); // write curbit for voicestream
        }

    }


}
