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
    class ChatSpammer
    {
        public static void ChatCleaner()
        {
            while (Properties.Settings.Default.chatcleaner)
            {
                Thread.Sleep(100);

                ClientCMD.Exec("say \"﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽﷽ ﷽﷽\"");
            }
        }
    }
}
