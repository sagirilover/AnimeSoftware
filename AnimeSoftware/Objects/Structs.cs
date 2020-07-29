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

    public unsafe struct ClientClass
    {
        IntPtr m_pCreateFn;
        IntPtr m_pCreateEventFn;
        ConstChar* m_pNetworkName;
        RecvTable* m_pRecvTable;
        ClientClass* m_pNext;
        int m_ClassID;

        public string GetName()
        {
            fixed (ClientClass* ptr = &this)
                return ((ConstChar*)Memory.Read<IntPtr>((int)ptr + 0x8))->ToString();
        }

        public RecvTable* GetRecvTable()
        {
            fixed (ClientClass* ptr = &this)
                return ((RecvTable*)Memory.Read<IntPtr>((int)ptr + 0xC));
        }

        public ClientClass* Next()
        {
            fixed (ClientClass* ptr = &this)
                return ((ClientClass*)Memory.Read<IntPtr>((int)ptr + 0x10));
        }
    }
    public unsafe struct RecvTable
    {
        RecvProp* m_pProps;
        int m_nProps;
        IntPtr m_pDecoder;
        ConstChar* m_pNetTableName;
        byte m_bInitialized;
        byte m_bInMainList;

        public string GetName()
        {
            fixed (RecvTable* ptr = &this)
                return ((ConstChar*)Memory.Read<IntPtr>((int)ptr + 0xC))->ToString();
        }

        public RecvProp* GetRecvProps()
        {
            fixed (RecvTable* ptr = &this)
                return ((RecvProp*)Memory.Read<IntPtr>((int)ptr));
        }

        public int GetPropsCount()
        {
            fixed (RecvTable* ptr = &this)
                return Memory.Read<int>((int)ptr + 0x4);
        }
    }
    public unsafe struct RecvProp
    {
        ConstChar* m_pVarName;
        int m_RecvType;
        int m_Flags;
        int m_StringBufferSize;
        int m_bInsideArray;
        IntPtr m_pExtraData;
        RecvProp* m_pArrayProp;
        IntPtr m_ArrayLengthProxy;
        IntPtr m_ProxyFn;
        IntPtr m_DataTableProxyFn;
        RecvTable* m_pDataTable;
        int m_Offset;
        int m_ElementStride;
        int m_nElements;
        ConstChar* m_pParentArrayPropName;

        public RecvTable* GetDataTable()
        {
            fixed (RecvProp* ptr = &this)
                return ((RecvTable*)Memory.Read<IntPtr>((int)ptr + 0x28));
        }

        public string GetName()
        {
            fixed (RecvProp* ptr = &this)
                return ((ConstChar*)Memory.Read<IntPtr>((int)ptr))->ToString();
        }

        public int GetOffset()
        {
            fixed (RecvProp* ptr = &this)
                return (int)Memory.Read<IntPtr>((int)ptr + 0x2C);
        }
    }
    public unsafe struct ConstChar
    {
        public override string ToString()
        {
            int len = 0;
            fixed (ConstChar* ptr = &this)
            {
                while (Memory.ReadByte((int)ptr + len) != 0)
                    len++;

                return Encoding.UTF8.GetString(Memory.ReadBytes((int)ptr, len));
            }
        }

        public bool Contains(string target)
        {
            return this.ToString().Contains(target);
        }
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
        public Vector m_vecCameraOffset;            // 0xA0
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
        public Vector m_vecPreviousViewAngles;      // 0xC4
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector m_vecPreviousViewAnglesTilt;  // 0xD0
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
        public Vector m_vecViewAngles;     // 0x0C
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector m_vecAimDirection;   // 0x18
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

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector
    {

        public float x;
        public float y;
        public float z;

        public Vector(float x = 0, float y = 0, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return (a.x == b.x && a.y == b.y && a.z == b.z);
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return (a.x != b.x || a.y != b.y || a.z != b.z);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static Vector operator /(Vector a, int b)
        {
            return new Vector(a.x / b, a.y / b, a.z / b);
        }
        public static Vector operator *(Vector a, int b)
        {
            return new Vector(a.x * b, a.y * b, a.z * b);
        }
        public static Vector operator /(Vector a, float b)
        {
            return new Vector(a.x / b, a.y / b, a.z / b);
        }
        public static Vector operator *(Vector a, float b)
        {
            return new Vector(a.x * b, a.y * b, a.z * b);
        }

        public float DotProduct(float x, float y, float z)
        {
            return this.x * x + this.y * y + this.z * z;
        }

        public float DistanceTo(Vector dst)
        {
            return (float)Math.Sqrt((dst.x - x) * (dst.x - x) + (dst.y - y) * (dst.y - y) + (dst.z - z) * (dst.z - z));
        }

        public float Length
        {
            get
            {
                return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
            }
        }
        public float Length2D
        {
            get
            {
                return (float)Math.Sqrt((x * x) + (y * y));
            }
        }

        public void Normalize()
        {
            while (y > 180)
            {
                y -= 360;
            }
            while (y < -180)
            {
                y += 360;
            }

            while (x > 89)
            {
                x -= 180;
            }

            while (x < -89)
            {
                x += 180;
            }
        }

        public override string ToString()
        {
            return $"X: {x}, Y: {y}, Z: {z}";
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
