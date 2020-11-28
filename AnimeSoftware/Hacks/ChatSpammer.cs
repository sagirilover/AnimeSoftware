using AnimeSoftware.Objects;
using System;
using System.Threading;

namespace AnimeSoftware.Hacks
{
    internal class ChatSpammer
    {
        public static void ChatCleaner()
        {
            while (Properties.Settings.Default.chatcleaner)
            {
                Thread.Sleep(100);

                ClientCMD.Exec("say \"﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽\"");
            }
        }
        public static void RceMem()
        {
            Random rnd = new Random();
            while (Properties.Settings.Default.chatcleaner)
            {
                Thread.Sleep(100);

                string mem = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7} pwned 0_o", rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100));

                ClientCMD.Exec("say \"" + mem + "\"");
            }
        }
    }
}
