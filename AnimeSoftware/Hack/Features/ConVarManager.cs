using AnimeSoftware.Injections;
using AnimeSoftware.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeSoftware.Hack.Models;

namespace AnimeSoftware.Hacks
{
    internal class ConVarManager
    {
        public static void ChangeName(string name)
        {
            var nick = new ConVar("name");
            nick.ClearCallbacks();
            ClientCMD.Exec("name \"" + name + "\"");
        }

        public static void StealName(int id)
        {
            var nick = new ConVar("name");
            nick.ClearCallbacks();
            ClientCMD.Exec("name \" " + new Player(id).Name + " \"");
        }

        public static void VoteKick(int id)
        {
            //ConVar vote = new ConVar("vote");
            //vote.ClearCallbacks();
            ClientCMD.Exec("callvote kick " + id);
        }

        public static void InstantChange()
        {
            var nick = new ConVar("name");
            nick.ClearCallbacks();
            ClientCMD.Exec("name \"\n\xAD\xAD\xAD\"");
        }
    }
}