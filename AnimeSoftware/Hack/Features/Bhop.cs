using System;
using System.Threading;
using System.Windows.Forms;
using AnimeSoftware.Hack.Models;
using AnimeSoftware.Utils;

namespace AnimeSoftware.Hack.Features
{
    public static class Bhop
    {
        public static void Run()
        {
            while (true)
            {
                if(!Properties.Settings.Default.bhop|| !Engine.InGame)
                    continue;

                var lp = new LocalPlayer();

                if(lp.Ptr == IntPtr.Zero)
                    continue;

                if (Input.KeyDown(Keys.Space) && lp.OnGround && lp.Velocity.Length > 0.1f)
                {
                    lp.Jump();
                }
            }
        }
    }
}