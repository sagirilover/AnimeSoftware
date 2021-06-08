using AnimeSoftware.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimeSoftware.Hack.Models;

namespace AnimeSoftware
{
    internal class Checks
    {
        public static void CheckVersion()
        {
            var url = "https://raw.githubusercontent.com/sagirilover/AnimeSoftware/master/version"; // check for updates
            using (var client = new WebClient())
            {
                var s = client.DownloadString(url);
                if (version != s.Substring(0, 5))
                {
                    var result = MessageBox.Show("New update: " + s + "\nRedirect to github?", "New version.",
                        MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                        Process.Start("https://github.com/sagirilover/AnimeSoftware");
                }
            }
        }

        public static string version = "v5.00";
    }
}