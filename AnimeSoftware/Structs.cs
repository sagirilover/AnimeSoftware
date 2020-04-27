using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Globalization;

namespace AnimeSoftware
{
    class Structs
    {
        public static readonly int[] SpamWeaponList = new int[]{ 4,9,10,11,38,40,64,262208 };
        public static Dictionary<int, string> Hitbox = new Dictionary<int, string>
        {
            [8] = "Head",
            [7] = "Neck",
            [6] = "Body"
        };
    }

    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    public enum Hitbox
    {
        HEAD = 8,
        NECK = 7,
        BODY = 6
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharCodes
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public int[] tab;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct player_info_s
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] __pad0;
        public int m_nXuidLow;
        public int m_nXuidHigh;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] m_szPlayerName;
        public uint m_nUserID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
        public char[] m_szSteamID;
        public uint m_nSteam3ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] m_szFriendsName;
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bIsFakePlayer;
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bIsHLTV;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] m_dwCustomFiles;
        public char m_FilesDownloaded;
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Input_t
    {
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_pVftable;                   // 0x00
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bTrackIRAvailable;          // 0x04
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bMouseInitialized;          // 0x05
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bMouseActive;               // 0x06
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bJoystickAdvancedInit;      // 0x07
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 44)]
        public int[] Unk1;                     // 0x08
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_pKeys;                      // 0x34
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public int[] Unk2;                    // 0x38
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bCameraInterceptingMouse;   // 0x9C
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bCameraInThirdPerson;       // 0x9D
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bCameraMovingWithMouse;     // 0x9E
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector3 m_vecCameraOffset;            // 0xA0
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bCameraDistanceMove;        // 0xAC
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nCameraOldX;                // 0xB0
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nCameraOldY;                // 0xB4
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nCameraX;                   // 0xB8
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nCameraY;                   // 0xBC
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public bool m_bCameraIsOrthographic;      // 0xC0
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector3 m_vecPreviousViewAngles;      // 0xC4
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector3 m_vecPreviousViewAnglesTilt;  // 0xD0
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flLastForwardMove;          // 0xDC
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nClearInputState;           // 0xE0
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] Unk3;                    // 0xE4
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_pCommands;                  // 0xEC
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_pVerifiedCommands;          // 0xF0
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UserCmd_t
    {
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int pVft;                // 0x00
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iCmdNumber;        // 0x04
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iTickCount;        // 0x08
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector3 m_vecViewAngles;     // 0x0C
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector3 m_vecAimDirection;   // 0x18
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flForwardmove;     // 0x24
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flSidemove;        // 0x28
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flUpmove;          // 0x2C
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iButtons;          // 0x30
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int m_bImpulse;          // 0x34
        public int[] Pad1;
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iWeaponSelect;     // 0x38
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iWeaponSubtype;    // 0x3C
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iRandomSeed;       // 0x40
        [MarshalAs(UnmanagedType.U1, SizeConst = 2)]
        public UInt16 m_siMouseDx;         // 0x44
        [MarshalAs(UnmanagedType.U1, SizeConst = 2)]
        public UInt16 m_siMouseDy;         // 0x46
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        bool m_bHasBeenPredicted; // 0x48
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] Pad2;
    }; // size is 100 or 0x64

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct VerifiedUserCmd_t
    {
        public UserCmd_t m_Command;
        public UInt32 m_Crc;
    };

    public struct Signature
    {
        public readonly int Offset;
        public readonly byte[] ByteArray;
        public readonly IntPtr Address;
        public readonly string Mask;

        public Signature(byte[] _byteArray, string _mask, int _offset = 0)
        {
            ByteArray = _byteArray;
            Mask = _mask;
            Offset = _offset;
            Address = IntPtr.Zero;
        }

        public Signature(IntPtr _address)
        {
            ByteArray = null;
            Offset = 0;
            Address = _address;
            Mask = string.Empty;
        }

        public Signature(string _signature, int _offset = 0)
        {
            var _mask = string.Empty;
            var patternBlocks = _signature.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var pattern = new byte[patternBlocks.Length];

            for (int i = 0; i < patternBlocks.Length; i++)
            {
                var block = patternBlocks[i];

                if (block == "?")
                {
                    _mask += block;
                    pattern[i] = 0;
                }
                else
                {
                    _mask += "x";
                    if (!byte.TryParse(patternBlocks[i], NumberStyles.HexNumber,
                        CultureInfo.DefaultThreadCurrentCulture, out pattern[i]))
                        throw new Exception("Signature Parsing Error");
                }
            }

            ByteArray = pattern;
            Offset = _offset;
            Address = IntPtr.Zero;
            Mask = _mask;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GlowSettings
    {
        byte renderWhenOccluded;
        byte renderWhenUnoccluded;
        byte fullBloomRender;

        public GlowSettings(bool __renderWhenOccluded, bool __renderWhenUnoccluded, bool __fullBloom)
        {
            renderWhenOccluded = __renderWhenOccluded == true ? (byte)1 : (byte)0;
            renderWhenUnoccluded = __renderWhenUnoccluded == true ? (byte)1 : (byte)0;
            fullBloomRender = __fullBloom == true ? (byte)1 : (byte)0;
        }
    }

    public struct GlowColor
    {
        public float r;
        public float g;
        public float b;
        public float a;
        public GlowColor(Color color)
        {
            r = color.R / 255f;
            g = color.G / 255f;
            b = color.B / 255f;
            a = color.A / 255f;
        }
        public GlowColor(float _r,float _g,float _b, float _a)
        {
            r = _r;
            g = _g;
            b = _b;
            a = _a;
        }
        public static Color operator *(GlowColor a, int b)
        {
            return Color.FromArgb((int)a.a*b, (int)a.r * b, (int)a.g * b, (int)a.b * b);
        }
        public Color ToColor
        {
            get
            {
                return Color.FromArgb((int)(a*255), (int)(r *255), (int)(g *255), (int)(b *255));
            }
        }
    }
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public float Length
        {
            get
            {
                return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
            }
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static Vector3 operator /(Vector3 a, int b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }
        public static Vector3 operator *(Vector3 a, int b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }
        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }
        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }
    }
}
