using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeSoftware.Utils
{
    internal class Log
    {
        public static void Error(params object[] args)
        {
            Console.WriteLine(
                $"[{DateTime.Now.ToString()}] [Error] {string.Join(" ", args.Select(x => x.ToString()))}");
        }

        public static void Info(params object[] args)
        {
            Console.WriteLine($"[{DateTime.Now.ToString()}] [Info] {string.Join(" ", args.Select(x => x.ToString()))}");
        }

        public static void Debug(params object[] args)
        {
            //if (!Properties.Settings.Default.debug)
            //    return;
            Console.WriteLine(
                $"[{DateTime.Now.ToString()}] [Debug] {string.Join(" ", args.Select(x => x.GetType().Equals(typeof(IntPtr)) ? ((IntPtr) x).ToString("X") : x.ToString()))}");
        }
    }
}