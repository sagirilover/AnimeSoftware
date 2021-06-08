using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AnimeSoftware.Offsets;
using AnimeSoftware.Utils;
using AnimeSoftware.Offsets;

namespace AnimeSoftware
{
    internal unsafe class CalcedOffsets
    {
        public static int ClientCMD;
        public static int dwUse;

        public static int cl_sidespeed;
        public static int cl_forwardspeed;

        public static int viewmodel_x;
        public static int viewmodel_y;
        public static int viewmodel_z;

        public static void Init()
        {
            ClientCMD = Memory.FindPattern(
                new byte[]
                {
                    0x55, 0x8B, 0xEC, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x81, 0xF9, 0x00, 0x00, 0x00, 0x00, 0x75,
                    0x0C, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0xEB, 0x05, 0x8B, 0x01, 0xFF,
                    0x50, 0x34, 0x50, 0xA1
                }, "xxxxx????xx????xxx????x????xxxxxxxxx", Memory.Engine, Memory.EngineSize);
            cl_sidespeed = Memory.FindPattern(
                new byte[]
                {
                    0xF3, 0x0F, 0x10, 0x05, 0x00, 0x00, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x44, 0x24, 0x00, 0x81, 0x74,
                    0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD9, 0x44, 0x24, 0x14, 0xEB, 0x07
                }, "xxxx????xxxxx?xxx?????xxxxxx", Memory.Client, Memory.ClientSize) + 0x4;
            cl_forwardspeed = Memory.FindPattern(
                new byte[]
                {
                    0xF3, 0x0F, 0x10, 0x05, 0x00, 0x00, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x44, 0x24, 0x00, 0x81, 0x74,
                    0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0xEB, 0x37
                }, "xxxx????xxxxx?xxx?????xx", Memory.Client, Memory.ClientSize) + 0x4;
            viewmodel_x = Memory.FindPattern(
                new byte[]
                {
                    0xF3, 0x0F, 0x10, 0x05, 0x00, 0x00, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x45, 0x00, 0x81, 0x75, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0xEB, 0x0A, 0x8B, 0x01, 0x8B, 0x40, 0x30, 0xFF, 0xD0, 0xD9, 0x5D, 0x08,
                    0xF3, 0x0F, 0x10, 0x45, 0x00, 0xB9
                }, "xxxx????xxxx?xx?????xxxxxxxxxxxxxxxx?x", Memory.Client, Memory.ClientSize) + 0x4;
            viewmodel_y = Memory.FindPattern(
                new byte[]
                {
                    0xF3, 0x0F, 0x10, 0x05, 0x00, 0x00, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x45, 0x00, 0x8B, 0x45, 0x08,
                    0x35, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0x0C, 0xEB, 0x0A
                }, "xxxx????xxxx?xxxx????xxxxx", Memory.Client, Memory.ClientSize) + 0x4;
            viewmodel_z = Memory.FindPattern(
                new byte[]
                {
                    0xF3, 0x0F, 0x10, 0x05, 0x00, 0x00, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x45, 0x00, 0x8B, 0x45, 0x08,
                    0x35, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xFC, 0xEB, 0x0A
                }, "xxxx????xxxx?xxxx????xxxxx", Memory.Client, Memory.ClientSize) + 0x4;
            dwUse = Memory.FindPattern(
                new byte[] {0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x8B, 0xF2, 0x8B, 0xC1, 0x83, 0xCE, 0x20},
                "xx????xxxxxxx", Memory.Client, Memory.ClientSize) + 2;

            InitXorWithValue();
        }

        public static uint xor_cl_sidespeed;
        public static uint xor_cl_forwardspeed;
        public static uint xor_viewmodel_x;
        public static uint xor_viewmodel_y;
        public static uint xor_viewmodel_z;

        private static void InitXorWithValue()
        {
            xor_cl_sidespeed = CalcXorWithValue(cl_sidespeed);
            xor_cl_forwardspeed = CalcXorWithValue(cl_forwardspeed);
            xor_viewmodel_x = CalcXorWithValue(viewmodel_x);
            xor_viewmodel_y = CalcXorWithValue(viewmodel_y);
            xor_viewmodel_z = CalcXorWithValue(viewmodel_z);
        }

        private static uint CalcXorWithValue(int cvarOffset)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(Memory.Read<int>(Memory.Client + cvarOffset) - 0x2C), 0);
        }
    }
}