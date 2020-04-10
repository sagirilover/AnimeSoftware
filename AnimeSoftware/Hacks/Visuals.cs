using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hazedumper;
using System.Drawing;
using AnimeSoftware.Objects;
using System.Threading;

namespace AnimeSoftware.Hacks
{
    class Visuals
    {
        public static List<Entity> ToGlow = new List<Entity>();
        public static void Start()
        {
            try
            {
                while (true)
                {
                    SetGlow();
                    Thread.Sleep(1);
                }
            }
            catch
            {
                Start();
            }
    }


        public static void SetGlow()
        {
            try
            {
                if (ToGlow.Count <= 0)
                    return;
            }
            catch (NullReferenceException)
            {
                return;
            }
            int GlowPtr = Memory.Read<int>(Memory.Client + signatures.dwGlowObjectManager);

            foreach(Entity x in ToGlow)
            {
                Memory.Write<GlowColor>(GlowPtr + (x.GlowIndex * 0x38 + 0x4), x.glowColor);
                Memory.Write<GlowSettings>(GlowPtr + (x.GlowIndex * 0x38 + 0x24), x.glowSettings);
            }
        }
    }
}
