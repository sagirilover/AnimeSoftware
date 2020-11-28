using AnimeSoftware.Injections;
using AnimeSoftware.Objects;

namespace AnimeSoftware.Hacks
{
    internal class ConVarManager
    {
        public static void ChangeName(string name)
        {
            ConVar nick = new ConVar("name");
            nick.ClearCallbacks();
            ClientCMD.Exec("name \"" + name + "\"");

        }
        public static void StealName(int id)
        {
            ConVar nick = new ConVar("name");
            nick.ClearCallbacks();
            ClientCMD.Exec("name \" " + new Entity(id).Name2 + " \"");

        }

        public static void VoteKick(int id)
        {
            //ConVar vote = new ConVar("vote");
            //vote.ClearCallbacks();
            ClientCMD.Exec("callvote kick " + id);

        }
        public static void InstantChange()
        {
            ConVar nick = new ConVar("name");
            nick.ClearCallbacks();
            ClientCMD.Exec("name \"\n\xAD\xAD\xAD\"");
        }
    }
}
