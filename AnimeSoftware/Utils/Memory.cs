using AnimeSoftware.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AnimeSoftware
{
    internal class Memory
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("Kernel32.dll")]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, ref uint lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, ref uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

        private const int PROCESS_VM_OPERATION = 0x0008;
        private const int PROCESS_VM_READ = 0x0010;
        private const int PROCESS_VM_WRITE = 0x0020;

        public static Process process;
        public static IntPtr pHandle;

        public static int Client;
        public static int ClientSize;
        public static int Engine;
        public static int EngineSize;
        public static int vstdlib;
        public static int vstdlibSize;



        public static bool OpenProcess(string name)
        {

            try
            {
                process = Process.GetProcessesByName(name)[0];
                return true;
            }
            catch
            {
                Log.Error("Can't open process.");
                return false;
            }
        }
        public static bool ProcessHandle()
        {
            try
            {
                pHandle = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, process.Id);
                return true;
            }
            catch
            {

                Log.Error("Can't get handle.");
                return false;
            }
        }

        public static bool GetModules()
        {
            try
            {
                foreach (ProcessModule module in process.Modules)
                {
                    if (module.ModuleName == "client.dll")
                    {
                        Client = (int)module.BaseAddress;
                        ClientSize = module.ModuleMemorySize;
                    }
                    else if (module.ModuleName == "engine.dll")
                    {
                        Engine = (int)module.BaseAddress;
                        EngineSize = module.ModuleMemorySize;
                    }
                    else if (module.ModuleName == "vstdlib.dll")
                    {
                        vstdlib = (int)module.BaseAddress;
                        vstdlibSize = module.ModuleMemorySize;
                    }
                }
                if ((IntPtr)Client == IntPtr.Zero || (IntPtr)Engine == IntPtr.Zero || (IntPtr)vstdlib == IntPtr.Zero)
                {
                    Log.Error("Module error");
                    return false;
                }

                return true;
            }
            catch
            {
                Log.Error("Module get error");
                return false;
            }

        }

        public static byte[] ReadBytes(int address, int length)
        {
            byte[] buffer = new byte[length];
            uint nBytesRead = uint.MinValue;
            bool success = ReadProcessMemory(pHandle, (IntPtr)address, buffer, (uint)length, ref nBytesRead);
            return buffer;
        }

        public static byte[] ReadBytes(IntPtr address, int length)
        {
            return ReadBytes((int)address, length);
        }

        public static byte ReadByte(int address)
        {
            return ReadBytes(address, 1)[0];
        }

        public static T Read<T>(int address)
        {
            int length = Marshal.SizeOf(typeof(T));

            if (typeof(T) == typeof(bool))
            {
                length = 1;
            }

            byte[] buffer = new byte[length];
            uint nBytesRead = uint.MinValue;
            ReadProcessMemory(pHandle, (IntPtr)address, buffer, (uint)length, ref nBytesRead);
            return GetStructure<T>(buffer);
        }

        public static T Read<T>(IntPtr address)
        {
            return Read<T>((int)address);
        }

        public static void WriteBytes(int address, byte[] value)
        {
            uint nBytesRead = uint.MinValue;
            WriteProcessMemory(pHandle, (IntPtr)address, value, (IntPtr)value.Length, ref nBytesRead);
        }

        public static void WriteBytes(IntPtr address, byte[] value)
        {
            WriteBytes((int)address, value);
        }

        public static void Write<T>(int address, T value)
        {
            int length = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[length];

            IntPtr ptr = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, buffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            uint nBytesRead = uint.MinValue;
            WriteProcessMemory(pHandle, (IntPtr)address, buffer, (IntPtr)length, ref nBytesRead);
        }

        public static void Write<T>(IntPtr address, T value)
        {
            Write<T>((int)address, value);
        }

        public static T GetStructure<T>(byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structure;
        }


        public static string ReadString(int address, int bufferSize, Encoding enc)
        {
            byte[] buffer = new byte[bufferSize];
            uint nBytesRead = 0;
            bool success = ReadProcessMemory(pHandle, (IntPtr)address, buffer, (uint)bufferSize, ref nBytesRead);
            string text = enc.GetString(buffer);
            if (text.Contains('\0'))
            {
                text = text.Substring(0, text.IndexOf('\0'));
            }

            return text;
        }

        public static string ReadText(IntPtr hProcess, IntPtr address)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                int offset = 0;
                byte read;
                while ((read = ReadMemory(hProcess, address + offset, 1)[0]) != 0)
                {
                    ms.WriteByte(read);
                    offset++;
                }
                byte[] data = ms.ToArray();
                return Encoding.UTF8.GetString(data, 0, data.Length);
            }
        }

        public static byte[] ReadMemory(IntPtr hProcess, IntPtr address, int length)
        {
            byte[] data = new byte[length];
            if (!ReadProcessMemory(hProcess, address, data, data.Length, out IntPtr unused))
            {
                return null;
            }
            return data;
        }

        public static int FindPattern(byte[] pattern, string mask, int moduleBase, int moduleSize)
        {
            byte[] moduleBytes = new byte[moduleSize];
            uint numBytes = 0;

            if (ReadProcessMemory(Memory.pHandle, (IntPtr)moduleBase, moduleBytes, (uint)moduleSize, ref numBytes))
            {
                for (int i = 0; i < moduleSize; i++)
                {
                    bool found = true;

                    for (int l = 0; l < mask.Length; l++)
                    {
                        found = mask[l] == '?' || moduleBytes[l + i] == pattern[l];

                        if (!found)
                        {
                            break;
                        }
                    }

                    if (found)
                    {
                        return i;
                    }
                }
            }

            return 0;
        }

        public static int FindPattern(string signature, int moduleBase, int moduleSize)
        {
            List<byte> temp = new List<byte>();
            foreach (string h in signature.Split(' '))
            {
                if (h == "?")
                {
                    temp.Add(0);
                }
                else
                {
                    temp.Add((byte)Convert.ToInt32(h, 16));
                }
            }

            return FindPattern(temp.ToArray(), string.Join("", temp.Select(x => x == 0 ? "?" : "x")), moduleBase, moduleSize);
        }
    }
}
