using System;
using System.Threading;
using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;
using AnimeSoftware.Utils;

namespace AnimeSoftware.Hack.Models
{
    public static class Engine
    {
        public static IntPtr ClientState => (IntPtr) Memory.Read<int>(Memory.Engine + Signatures.dwClientState);

        private static bool _lastTimeInGame = false;

        public static bool InGame
        {
            get
            {
                var r = Memory.Read<int>(ClientState + Signatures.dwClientState_State) == 6;

                if (r && !_lastTimeInGame)
                {
                    var lp = new LocalPlayer();
                    if (lp.Ptr != IntPtr.Zero && !lp.Dormant)
                    {
                        LocalPlayer.GlobalName = lp.Name;
                        _lastTimeInGame = true;
                    }
                }

                if (!r)
                {
                    LocalPlayer.GlobalName = string.Empty;
                    LocalPlayer.LocalPlayerIndex = -1;
                }
                
                return r;
            }
        }

        public static string MapName
        {
            get
            {
                unsafe
                {
                    return ((Char_t*) (ClientState + Signatures.dwClientState_Map))->ToString();
                }
            }
        }

        public static string MapDirectory
        {
            get
            {
                unsafe
                {
                    return ((Char_t*) (ClientState + Signatures.dwClientState_MapDirectory))->ToString();
                }
            }
        }
    }
}