using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using AnimeSoftware.Utils;

namespace AnimeSoftware
{
    internal class Memory
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("Kernel32.dll")]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
            uint nSize, ref uint lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer,
            int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
            IntPtr nSize, ref uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size,
            int lpNumberOfBytesWritten);

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
                    if (module.ModuleName == "client.dll")
                    {
                        Client = (int) module.BaseAddress;
                        ClientSize = (int) module.ModuleMemorySize;
                    }
                    else if (module.ModuleName == "engine.dll")
                    {
                        Engine = (int) module.BaseAddress;
                        EngineSize = (int) module.ModuleMemorySize;
                    }
                    else if (module.ModuleName == "vstdlib.dll")
                    {
                        vstdlib = (int) module.BaseAddress;
                        vstdlibSize = (int) module.ModuleMemorySize;
                    }

                if ((IntPtr) Client == IntPtr.Zero || (IntPtr) Engine == IntPtr.Zero || (IntPtr) vstdlib == IntPtr.Zero)
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
            var buffer = new byte[length];
            var nBytesRead = uint.MinValue;
            var success = ReadProcessMemory(pHandle, (IntPtr) address, buffer, (uint) length, ref nBytesRead);
            return buffer;
        }

        public static byte[] ReadBytes(IntPtr address, int length)
        {
            return ReadBytes((int) address, length);
        }

        public static byte ReadByte(int address)
        {
            return ReadBytes(address, 1)[0];
        }

        public static T Read<T>(int address)
        {
            var length = Marshal.SizeOf(typeof(T));

            if (typeof(T) == typeof(bool))
                length = 1;

            var buffer = new byte[length];
            var nBytesRead = uint.MinValue;
            ReadProcessMemory(pHandle, (IntPtr) address, buffer, (uint) length, ref nBytesRead);
            return GetStructure<T>(buffer);
        }

        public static T Read<T>(IntPtr address)
        {
            return Read<T>((int) address);
        }

        public static void WriteBytes(int address, byte[] value)
        {
            var nBytesRead = uint.MinValue;
            WriteProcessMemory(pHandle, (IntPtr) address, value, (IntPtr) value.Length, ref nBytesRead);
        }

        public static void WriteBytes(IntPtr address, byte[] value)
        {
            WriteBytes((int) address, value);
        }

        public static void Write<T>(int address, T value)
        {
            var length = Marshal.SizeOf(typeof(T));
            var buffer = new byte[length];

            var ptr = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, buffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            var nBytesRead = uint.MinValue;
            WriteProcessMemory(pHandle, (IntPtr) address, buffer, (IntPtr) length, ref nBytesRead);
        }

        public static void Write<T>(IntPtr address, T value)
        {
            Write<T>((int) address, value);
        }

        public static T GetStructure<T>(byte[] bytes)
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var structure = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structure;
        }


        public static string ReadString(int address, int bufferSize, Encoding enc)
        {
            var buffer = new byte[bufferSize];
            uint nBytesRead = 0;
            var success = ReadProcessMemory(pHandle, (IntPtr) address, buffer, (uint) bufferSize, ref nBytesRead);
            var text = enc.GetString(buffer);
            if (text.Contains('\0'))
                text = text.Substring(0, text.IndexOf('\0'));
            return text;
        }

        public static string ReadText(IntPtr hProcess, IntPtr address)
        {
            using (var ms = new MemoryStream())
            {
                var offset = 0;
                byte read;
                while ((read = ReadMemory(hProcess, address + offset, 1)[0]) != 0)
                {
                    ms.WriteByte(read);
                    offset++;
                }

                var data = ms.ToArray();
                return Encoding.UTF8.GetString(data, 0, data.Length);
            }
        }

        public static byte[] ReadMemory(IntPtr hProcess, IntPtr address, int length)
        {
            var data = new byte[length];
            if (!ReadProcessMemory(hProcess, address, data, data.Length, out var unused)) return null;

            return data;
        }

        public static int FindPattern(byte[] pattern, string mask, int moduleBase, int moduleSize, int skip = 0)
        {
            var moduleBytes = new byte[moduleSize];
            uint numBytes = 0;

            if (ReadProcessMemory(pHandle, (IntPtr) moduleBase, moduleBytes, (uint) moduleSize, ref numBytes))
                for (var i = 0; i < moduleSize; i++)
                {
                    var found = true;

                    for (var l = 0; l < mask.Length; l++)
                    {
                        found = mask[l] == '?' || moduleBytes[l + i] == pattern[l];

                        if (!found)
                            break;
                    }

                    if (found)
                    {
                        if(skip <= 0)
                            return i;
                        skip--;
                    }
                }

            return 0;
        }

        public static int FindPattern(string signature, int moduleBase, int moduleSize, int skip = 0)
        {
            var temp = new List<byte>();
            foreach (var h in signature.Split(' '))
                if (h == "?")
                    temp.Add(0);
                else
                    temp.Add((byte) Convert.ToInt32(h, 16));

            return FindPattern(temp.ToArray(), string.Join("", temp.Select(x => x == 0 ? "?" : "x")), moduleBase,
                moduleSize, skip);
        }

        public class Allocator
        {
            public Dictionary<IntPtr, IntPtr> AllocatedSize = new Dictionary<IntPtr, IntPtr>();

            public IntPtr AlloacNewPage(IntPtr size)
            {
                var address = WinAPI.VirtualAllocEx(Memory.pHandle, IntPtr.Zero, (IntPtr) 4096,
                    (int) 0x1000 | (int) 0x2000, 0x40);

                AllocatedSize.Add(address, size);

                return address;
            }

            public void Free()
            {
                foreach (var key in AllocatedSize)
                    WinAPI.VirtualFreeEx(Memory.pHandle, key.Key, 4096, (int) 0x1000 | (int) 0x2000);
            }

            public IntPtr Alloc(int size)
            {
                for (var i = 0; i < AllocatedSize.Count; ++i)
                {
                    var key = AllocatedSize.ElementAt(i).Key;
                    var value = (int) AllocatedSize[key] + size;
                    if (value < 4096)
                    {
                        var currentAddress = IntPtr.Add(key, (int) AllocatedSize[key]);
                        AllocatedSize[key] = new IntPtr(value);
                        return currentAddress;
                    }
                }

                return AlloacNewPage(new IntPtr(size));
            }
        }
    }
}