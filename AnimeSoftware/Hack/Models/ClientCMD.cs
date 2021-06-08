using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeSoftware.Injections;
using AnimeSoftware.Utils;

namespace AnimeSoftware.Objects
{
    internal class ClientCMD
    {
        private static int Size = 256;
        private static IntPtr _address;

        public static void Exec(string szCmd)
        {
            if (_address == IntPtr.Zero)
            {
                var allocator = new Memory.Allocator();
                _address = allocator.Alloc(Size);
                if (_address == IntPtr.Zero)
                    return;
            }

            if (szCmd.Length > 255)
                szCmd = szCmd.Substring(0, 255);

            var szCmdBytes = Encoding.UTF8.GetBytes(szCmd + "\0");

            Memory.WriteProcessMemory(Memory.pHandle, _address, szCmdBytes, szCmdBytes.Length, 0);
            var remoteThread = WinAPI.CreateRemoteThread(Memory.pHandle, (IntPtr) null, IntPtr.Zero,
                new IntPtr(Memory.Engine + CalcedOffsets.ClientCMD), _address, 0, (IntPtr) null);
            WinAPI.CloseHandle(remoteThread);
            WinAPI.WaitForSingleObject(remoteThread, 0xFFFFFFFF);
        }
    }
}