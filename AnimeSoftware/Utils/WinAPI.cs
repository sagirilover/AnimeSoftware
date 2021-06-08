using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AnimeSoftware.Hack.Models;

namespace AnimeSoftware.Utils
{
    internal class WinAPI
    {
        [DllImport("kernel32.dll")]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
            IntPtr nSize, ref uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size,
            int lpNumberOfBytesWritten);

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetAsyncKeyState(int vKey);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, int dwFreeType);

        [DllImport("kernel32.dll")]
        internal static extern uint WaitForSingleObject(IntPtr hProcess, uint dwMilliseconds);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, IntPtr dwStackSize,
            IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize,
            uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        internal static extern bool CloseHandle(IntPtr hProcess);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Size GetWindowSize(IntPtr hWnd)
        {
            var rect = new Rect();
            GetWindowRect(hWnd, ref rect);
            return new Size(rect.Bottom - rect.Top, rect.Right - rect.Left);
        }
        
        public static void CreateThread(IntPtr address, byte[] shellcode)
        {
            WinAPI.WriteProcessMemory(Memory.pHandle, address, shellcode, shellcode.Length, 0);
            var thread = WinAPI.CreateRemoteThread(Memory.pHandle, (IntPtr) null, IntPtr.Zero, address,
                (IntPtr) null, 0, (IntPtr) null);
            WinAPI.WaitForSingleObject(thread, 0xFFFFFFFF);
            WinAPI.CloseHandle(thread);
        }

        public static void Execute(IntPtr address)
        {
            var thread = WinAPI.CreateRemoteThread(Memory.pHandle, (IntPtr) null, IntPtr.Zero, address,
                (IntPtr) null, 0, (IntPtr) null);
            WinAPI.WaitForSingleObject(thread, 0xFFFFFFFF);
            WinAPI.CloseHandle(thread);
        }
    }

    
}