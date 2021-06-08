using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnimeSoftware.Injections;
using AnimeSoftware.Objects;

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
            var rnd = new Random();
            while (Properties.Settings.Default.chatcleaner)
            {
                Thread.Sleep(100);

                var mem = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7} pwned 0_o", rnd.Next(100), rnd.Next(100),
                    rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100), rnd.Next(100));

                ClientCMD.Exec("say \"" + mem + "\"");
            }
        }
    }
}