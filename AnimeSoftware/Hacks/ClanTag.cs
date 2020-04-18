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
    public static class ClanTag
    {
        public static byte[] Shellcode = {
            0xB9,0x00,0x00,0x00,0x00,
            0xBA,0x00,0x00,0x00,0x00,
            0xB8,0x00,0x00,0x00,0x00,
            0xFF,0xD0,
            0xC3,
            0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
        };

        public static int Size = Shellcode.Length;
        public static IntPtr Address;
        private static Random rnd = new Random();

        private static string RandomGlitch(int length)
        {
            string source = "#$!@%?^&*8649/\\";
            string result = "";
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                result += source[rnd.Next(0, source.Length)];
            }
            return result;
        }
        public static void Default()
        {
            bool lastState = false;
            while (Properties.Settings.Default.clanTag)
            {
                if (!LocalPlayer.InGame)
                {
                    lastState = false;
                    continue;
                }

                if (!lastState && LocalPlayer.Health == 100)
                    lastState = true;

                if (LocalPlayer.IsDead)
                    if (!lastState)
                        continue;

                string clear = new string(' ', 15);
                string clantag = "animesoftware  ";
                string safetag = clear;
                int delay = 250;


                for (int i = 0; i <= clantag.Length; i++)
                {
                    if (!Properties.Settings.Default.clanTag)
                    {
                        ClanTag.Set("");
                        return;
                    }
                        
                    safetag = clear.Remove(0, i).Insert(0, clantag.Substring(0, i));
                    ClanTag.Set(safetag);
                    Thread.Sleep(delay);
                }

                Thread.Sleep(delay * 2);

                for (int i = 13; i >= 10; i--)
                {
                    if (!Properties.Settings.Default.clanTag)
                    {
                        ClanTag.Set("");
                        return;
                    }
                    safetag = RandomGlitch(i);
                    ClanTag.Set(safetag);
                    Thread.Sleep(delay);
                }

                ClanTag.Set(safetag = "sagirihook  ");
                Thread.Sleep(delay*2);

                for (int i = 0; i <= clantag.Length; i++)
                {
                    if (!Properties.Settings.Default.clanTag)
                    {
                        ClanTag.Set("");
                        return;
                    }
                    safetag = safetag.Remove(safetag.Length - 1, 1).Insert(0, clear.Substring(0, 1));
                    ClanTag.Set(safetag);
                    Thread.Sleep(delay);
                }
            }
            ClanTag.Set("");
        }

        public static void VelTag()
        {
            int old = 0;
            bool changed = false;
            bool lastState = false;
            while (Properties.Settings.Default.velTag)
            {
                Thread.Sleep(50);
                if (!LocalPlayer.InGame)
                {
                    lastState = false;
                    continue;
                }

                if (!lastState && LocalPlayer.Health == 100)if (LocalPlayer.IsDead)
                    if (lastState)
                    {
                        Set("velocity 0");
                        continue;
                    }
                    else
                        continue;
                    lastState = true;

                
                     
                int vel = (int)Math.Floor(LocalPlayer.Speed);
                Set("velocity " + vel.ToString());
                if (vel > old)
                {
                    old = vel;
                    changed = true;
                }  
                if (vel == 0 && changed && Properties.Settings.Default.velName)
                {
                    ConVarManager.ChangeName("max " + old.ToString());
                    changed = false;
                    old = 0;
                }
            }
            ClanTag.Set(" ");
        }

        public static void Set(string tag)
        {
            if (Address == IntPtr.Zero)
            {
                Allocator alloc = new Allocator();
                Address = alloc.Alloc(Size);
                alloc.Free();

                if (Address == IntPtr.Zero)
                    return;

                Buffer.BlockCopy(BitConverter.GetBytes((int)(Address + 18)), 0, Shellcode, 1, 4);
                Buffer.BlockCopy(BitConverter.GetBytes((int)(Address + 18)), 0, Shellcode, 6, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Memory.Engine + signatures.dwSetClanTag), 0, Shellcode, 11, 4);
            }

            if (!LocalPlayer.InGame) return;

            byte[] tag_bytes = Encoding.UTF8.GetBytes(tag + "\0");
            byte[] reset = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            Buffer.BlockCopy(reset, 0, Shellcode, 18, reset.Length);
            Buffer.BlockCopy(tag_bytes, 0, Shellcode, 18, tag.Length > 15 ? 15 : tag.Length);
            CreateThread.Create(Address, Shellcode);
        }
    }
}
