using System;

namespace AnimeSoftware.Injections
{
    public static class CreateThread
    {
        public static void Create(IntPtr address, byte[] shellcode)
        {
            DllImport.WriteProcessMemory(Memory.pHandle, address, shellcode, shellcode.Length, 0);
            IntPtr _Thread = DllImport.CreateRemoteThread(Memory.pHandle, (IntPtr)null, IntPtr.Zero, address, (IntPtr)null, 0, (IntPtr)null);
            DllImport.WaitForSingleObject(_Thread, 0xFFFFFFFF);
            DllImport.CloseHandle(_Thread);
        }

        public static void Execute(IntPtr address)
        {
            IntPtr _Thread = DllImport.CreateRemoteThread(Memory.pHandle, (IntPtr)null, IntPtr.Zero, address, (IntPtr)null, 0, (IntPtr)null);
            DllImport.WaitForSingleObject(_Thread, 0xFFFFFFFF);
            DllImport.CloseHandle(_Thread);
        }
    }
}
