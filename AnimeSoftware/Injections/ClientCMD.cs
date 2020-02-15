using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeSoftware
{
    class ClientCMD
    {
        public static int Size = 256;
        public static IntPtr Address;

        public static void Exec(string szCmd)
        {
            if (Address == IntPtr.Zero)
            {
                Allocator Alloc = new Allocator();
                Address = Alloc.Alloc(Size);
                if (Address == IntPtr.Zero)
                    return;
            }
            //if (szCmd.Length > 255)
            //    szCmd = szCmd.Substring(0, 255);

            var szCmd_bytes = Encoding.UTF8.GetBytes(szCmd + "\0");

            Memory.WriteProcessMemory(Memory.pHandle, Address, szCmd_bytes, szCmd_bytes.Length, 0);
            IntPtr Thread = DllImport.CreateRemoteThread(Memory.pHandle, (IntPtr)null, IntPtr.Zero, new IntPtr(Memory.Engine + ScannedOffsets.ClientCMD), Address, 0, (IntPtr)null);
            DllImport.CloseHandle(Thread);
            DllImport.WaitForSingleObject(Thread, 0xFFFFFFFF);
        }
    }
}
