using System;
using System.Collections.Generic;

namespace AnimeSoftware.Hack.Models
{
    public static class EntityList
    {
        public static List<Player> GetPlayers()
        {
            var lp = new LocalPlayer();
            
            var result = new List<Player>();

            if (lp.Ptr == IntPtr.Zero)
                return result;

            for (int i = 0; i < 64; i++)
            {
                var player = new Player(i);

                if (player.Ptr == IntPtr.Zero) continue;

                if (player.Ptr == lp.Ptr)
                {
                    LocalPlayer.LocalPlayerIndex = i;
                    continue;
                }
                
                result.Add(player);
            }

            return result;
        }
    }
}