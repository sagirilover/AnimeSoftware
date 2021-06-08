using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeSoftware.Offsets;
using System.Drawing;
using AnimeSoftware.Objects;
using System.Threading;
using AnimeSoftware.Hack.Models;

namespace AnimeSoftware.Hacks
{
    internal class Visuals
    {
        public static GlowColor[] GlowColors = new GlowColor[65];
        public static GlowSettings[] GlowSettings = new GlowSettings[65];
        public static bool[] ToGlow = new bool[65];

        public static void Add(int index, GlowColor color, GlowSettings settings)
        {
            ToGlow[index] = true;
            GlowColors[index] = color;
            GlowSettings[index] = settings;
        }
        
        public static void Start()
        {
            while (true)
            {
                Thread.Sleep(1);
                
                var glowManager = Memory.Read<int>(Memory.Client + Signatures.dwGlowObjectManager);

                for (var i = 0; i < ToGlow.Length; i++)
                {
                    if (!ToGlow[i])
                        continue;

                    var p = new Player(i);

                    Memory.Write((IntPtr) glowManager + p.GlowIndex * 0x38 + 0x4,
                        GlowColors[i]);
                    Memory.Write((IntPtr) glowManager + p.GlowIndex * 0x38 + 0x24,
                        GlowSettings[i]);
                }
            }
        }
    }
}