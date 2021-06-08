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
    public static class ClanTag
    {
        public static byte[] Shellcode =
        {
            0xB9, 0x00, 0x00, 0x00, 0x00,
            0xBA, 0x00, 0x00, 0x00, 0x00,
            0xB8, 0x00, 0x00, 0x00, 0x00,
            0xFF, 0xD0,
            0xC3,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        public static int Size = Shellcode.Length;
        public static IntPtr Address;
        private static Random rnd = new Random();

        private static string RandomGlitch(int length)
        {
            var source = "#$!@%?^&*8649/\\";
            var result = "";
            var rnd = new Random();
            for (var i = 0; i < length; i++) result += source[rnd.Next(0, source.Length)];
            return result;
        }

        public static void Default()
        {
            var lastState = false;
            while (Properties.Settings.Default.clanTag)
            {
                if (!Engine.InGame)
                {
                    lastState = false;
                    continue;
                }

                var lp = new LocalPlayer();
                
                if(lp.Ptr == IntPtr.Zero)
                    continue;

                if (!lastState && lp.Health == 100)
                    lastState = true;

                if (lp.Health <= 0)
                    if (!lastState)
                        continue;

                var clear = new string(' ', 15);
                var clantag = "animesoftware  ";
                var safetag = clear;
                var delay = 250;


                for (var i = 0; i <= clantag.Length; i++)
                {
                    if (!Properties.Settings.Default.clanTag)
                    {
                        Set("");
                        return;
                    }

                    safetag = clear.Remove(0, i).Insert(0, clantag.Substring(0, i));
                    Set(safetag);
                    Thread.Sleep(delay);
                }

                Thread.Sleep(delay * 2);

                for (var i = 13; i >= 10; i--)
                {
                    if (!Properties.Settings.Default.clanTag)
                    {
                        Set("");
                        return;
                    }

                    safetag = RandomGlitch(i);
                    Set(safetag);
                    Thread.Sleep(delay);
                }

                Set(safetag = "sagirihook  ");
                Thread.Sleep(delay * 2);

                for (var i = 0; i <= clantag.Length; i++)
                {
                    if (!Properties.Settings.Default.clanTag)
                    {
                        Set("");
                        return;
                    }

                    safetag = safetag.Remove(safetag.Length - 1, 1).Insert(0, clear.Substring(0, 1));
                    Set(safetag);
                    Thread.Sleep(delay);
                }
            }

            Set("");
        }

        public static void VelTag()
        {
            var old = 2;
            var changed = false;
            var lastState = false;
            while (Properties.Settings.Default.velTag)
            {
                Thread.Sleep(50);
                if (!Engine.InGame)
                {
                    lastState = false;
                    continue;
                }
                
                var lp = new LocalPlayer();
                
                if(lp.Ptr == IntPtr.Zero)
                    continue;

                if (!lastState && lp.Health == 100)
                    lastState = true;
                if (lp.Health <= 0)
                    if (lastState)
                    {
                        Set("velocity 0");
                        continue;
                    }
                    else
                    {
                        continue;
                    }


                var vel = (int) Math.Floor(lp.Velocity.Length2D);
                Set("velocity " + vel.ToString());
                if (vel > old)
                {
                    old = vel;
                    changed = true;
                }

                if ((vel == 0 || vel == 2) && changed && Properties.Settings.Default.velName)
                {
                    ConVarManager.ChangeName("max " + old.ToString());
                    changed = false;
                    old = 2;
                }
            }

            Set(" ");
        }

        public static void Set(string tag)
        {
            if (Address == IntPtr.Zero)
            {
                var alloc = new Memory.Allocator();
                Address = alloc.Alloc(Size);
                alloc.Free();

                if (Address == IntPtr.Zero)
                    return;

                Buffer.BlockCopy(BitConverter.GetBytes((int) (Address + 18)), 0, Shellcode, 1, 4);
                Buffer.BlockCopy(BitConverter.GetBytes((int) (Address + 18)), 0, Shellcode, 6, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Memory.Engine + Signatures.dwSetClanTag), 0, Shellcode, 11, 4);
            }

            if (!Engine.InGame) return;

            var tag_bytes = Encoding.UTF8.GetBytes(tag + "\0");
            var reset = new byte[]
                {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};

            Buffer.BlockCopy(reset, 0, Shellcode, 18, reset.Length);
            Buffer.BlockCopy(tag_bytes, 0, Shellcode, 18, tag.Length > 15 ? 15 : tag.Length);
            WinAPI.CreateThread(Address, Shellcode);
        }
    }
}