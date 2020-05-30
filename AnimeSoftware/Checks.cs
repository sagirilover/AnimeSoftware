using AnimeSoftware.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimeSoftware
{
    class Checks
    {
        public static bool Update = false;
        public static void Start()
        {
            while (true)
            {
                if (!LocalPlayer.InGame)
                    Update = false;

                if (LocalPlayer.InGame && !Update)
                {
                    PreLoad();
                }
                Thread.Sleep(1000);
            }
        }

        public static void PreLoad()
        {
            if (!LocalPlayer.InGame)
                return;

            Update = true;
            LocalPlayer.GetIndex();
            LocalPlayer.Name = LocalPlayer.GetName2;
        }

        public static void CheckVersion()
        {
            string url = "https://raw.githubusercontent.com/sagirilover/AnimeSoftware/master/version"; // check for updates
            using (WebClient client = new WebClient())
            {
                string s = client.DownloadString(url);
                if (version != s.Substring(0, 5))
                {
                    DialogResult result = MessageBox.Show("New update: " + s + "\nRedirect to github?", "New version.", MessageBoxButtons.YesNo);
                    if(result==DialogResult.Yes)
                        System.Diagnostics.Process.Start("https://github.com/sagirilover/AnimeSoftware");
                }
            
            }
        }

        public static string version = "v3.00";
        
    }
}
