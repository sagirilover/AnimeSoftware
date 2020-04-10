using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AnimeSoftware.Injections
{
    class DllImport
    {
        [DllImport("kernel32.dll")]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, ref UInt32 lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetAsyncKeyState(int vKey);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, int dwFreeType);

        [DllImport("kernel32.dll")]
        internal static extern UInt32 WaitForSingleObject(IntPtr hProcess, uint dwMilliseconds);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, UInt32 flAllocationType, UInt32 flProtect);

        [DllImport("kernel32.dll")]
        internal static extern bool CloseHandle(IntPtr hProcess);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public class Allocator
    {
        public Dictionary<IntPtr, IntPtr> AllocatedSize = new Dictionary<IntPtr, IntPtr>();

        public IntPtr AlloacNewPage(IntPtr size)
        {
            var Address = DllImport.VirtualAllocEx(Memory.pHandle, IntPtr.Zero, (IntPtr)4096, (int)0x1000 | (int)0x2000, 0x40);

            AllocatedSize.Add(Address, size);

            return Address;
        }

        public void Free()
        {
            foreach (var key in AllocatedSize)
                DllImport.VirtualFreeEx(Memory.pHandle, key.Key, 4096, (int)0x1000 | (int)0x2000);
        }

        public IntPtr Alloc(int size)
        {
            for (int i = 0; i < AllocatedSize.Count; ++i)
            {
                var key = AllocatedSize.ElementAt(i).Key;
                int value = (int)AllocatedSize[key] + size;
                if (value < 4096)
                {
                    IntPtr CurrentAddres = IntPtr.Add(key, (int)AllocatedSize[key]);
                    AllocatedSize[key] = new IntPtr(value);
                    return CurrentAddres;
                }
            }

            return AlloacNewPage(new IntPtr(size));
        }

    }
}
