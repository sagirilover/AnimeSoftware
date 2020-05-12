using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hazedumper;

namespace AnimeSoftware
{
    class ScannedOffsets
    {
        public static int ClientCMD;
        public static int UserInfoTable;
        public static int SetConVar;
        public static int dwUse;
        public static int Console;

        public static int cl_sidespeed;
        public static int cl_forwardspeed;
        

        public static void Init()
        {
            ClientCMD = Memory.FindPattern(new byte[] { 0x55, 0x8B, 0xEC, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x81, 0xF9, 0x00, 0x00, 0x00, 0x00, 0x75, 0x0C, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0xEB, 0x05, 0x8B, 0x01, 0xFF, 0x50, 0x34, 0x50, 0xA1 }, "xxxxx????xx????xxx????x????xxxxxxxxx", Memory.Engine, Memory.EngineSize);
            
            dwUse = Memory.FindPattern(new byte[] { 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x8B, 0xF2, 0x8B, 0xC1, 0x83, 0xCE, 0x20 }, "xx????xxxxxxx", Memory.Client, Memory.ClientSize) + 2;

            
        }

        
    }
}
