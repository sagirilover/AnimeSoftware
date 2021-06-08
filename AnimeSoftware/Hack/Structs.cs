using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using AnimeSoftware.Utils;

namespace AnimeSoftware.Hack.Models
{
    internal class Structs
    {
        public static readonly int[] SpamWeaponList = new int[] {4, 9, 10, 11, 38, 40, 64, 262208};

        public static Dictionary<int, string> Hitbox = new Dictionary<int, string>
        {
            [8] = "Head",
            [7] = "Neck",
            [6] = "Body"
        };
    }
    
    [Flags]
    public enum EEntityFlags : int
    {
        FL_ONGROUND = (1 << 0), // At rest / on the ground
        FL_DUCKING = (1 << 1), // Player flag -- Player is fully crouched
        FL_WATERJUMP = (1 << 2), // player jumping out of water

        FL_ONTRAIN = (1 << 3), // Player is _controlling_ a train, so movement commands should be ignored on client during prediction.
        FL_INRAIN = (1 << 4), // Indicates the entity is standing in rain
        FL_FROZEN = (1 << 5), // Player is frozen for 3rd person camera
        FL_ATCONTROLS = (1 << 6), // Player can't move, but keeps key inputs for controlling another entity
        FL_CLIENT = (1 << 7), // Is a player
        FL_FAKECLIENT = (1 << 8), // Fake client, simulated server side; don't send network messages to them
        FL_INWATER = (1 << 10) // In water
    };

    public unsafe struct ClientClass
    {
        private IntPtr m_pCreateFn;
        private IntPtr m_pCreateEventFn;
        private Char_t* m_pNetworkName;
        private RecvTable* m_pRecvTable;
        private ClientClass* m_pNext;
        private int m_ClassID;

        public string GetName()
        {
            fixed (ClientClass* ptr = &this)
            {
                return ((Char_t*) Memory.Read<IntPtr>((int) ptr + 0x8))->ToString();
            }
        }

        public RecvTable* GetRecvTable()
        {
            fixed (ClientClass* ptr = &this)
            {
                return (RecvTable*) Memory.Read<IntPtr>((int) ptr + 0xC);
            }
        }

        public ClientClass* Next()
        {
            fixed (ClientClass* ptr = &this)
            {
                return (ClientClass*) Memory.Read<IntPtr>((int) ptr + 0x10);
            }
        }
    }

    public unsafe struct RecvTable
    {
        private RecvProp* m_pProps;
        private int m_nProps;
        private IntPtr m_pDecoder;
        private Char_t* m_pNetTableName;
        private byte m_bInitialized;
        private byte m_bInMainList;

        public string GetName()
        {
            fixed (RecvTable* ptr = &this)
            {
                return ((Char_t*) Memory.Read<IntPtr>((int) ptr + 0xC))->ToString();
            }
        }

        public RecvProp* GetRecvProps()
        {
            fixed (RecvTable* ptr = &this)
            {
                return (RecvProp*) Memory.Read<IntPtr>((int) ptr);
            }
        }

        public int GetPropsCount()
        {
            fixed (RecvTable* ptr = &this)
            {
                return Memory.Read<int>((int) ptr + 0x4);
            }
        }
    }

    public unsafe struct RecvProp
    {
        private Char_t* m_pVarName;
        private int m_RecvType;
        private int m_Flags;
        private int m_StringBufferSize;
        private int m_bInsideArray;
        private IntPtr m_pExtraData;
        private RecvProp* m_pArrayProp;
        private IntPtr m_ArrayLengthProxy;
        private IntPtr m_ProxyFn;
        private IntPtr m_DataTableProxyFn;
        private RecvTable* m_pDataTable;
        private int m_Offset;
        private int m_ElementStride;
        private int m_nElements;
        private Char_t* m_pParentArrayPropName;

        public RecvTable* GetDataTable()
        {
            fixed (RecvProp* ptr = &this)
            {
                return (RecvTable*) Memory.Read<IntPtr>((int) ptr + 0x28);
            }
        }

        public string GetName()
        {
            fixed (RecvProp* ptr = &this)
            {
                return ((Char_t*) Memory.Read<IntPtr>((int) ptr))->ToString();
            }
        }

        public int GetOffset()
        {
            fixed (RecvProp* ptr = &this)
            {
                return (int) Memory.Read<IntPtr>((int) ptr + 0x2C);
            }
        }
    }

    public unsafe struct Char_t
    {
        public override string ToString()
        {
            var len = 0;
            fixed (Char_t* ptr = &this)
            {
                while (Memory.ReadByte((int) ptr + len) != 0)
                    len++;

                return Encoding.UTF8.GetString(Memory.ReadBytes((int) ptr, len));
            }
        }

        public bool Contains(string target)
        {
            return ToString().Contains(target);
        }
    }

    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
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
    
    
    public unsafe struct PlayerInfo
    {
        public Int64 pad;
        public int xuidLow;
        public int xuidHigh;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] Name;
        public int UserId;
        public fixed byte guid[20];
        public fixed byte pad1[16];
        public int friendsid;
        public fixed byte szFriendsName[128];
        public byte fakeplayer;
        public byte ishltv;
        public fixed int customfiles[4];
        public byte filesdownloaded;
    }

    public unsafe struct InterfaceReg
    {
        public IntPtr CreateFnPtr;
        public Char_t* Name;
        public InterfaceReg* Next;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UserCmd_t
    {
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int pVft; // 0x00

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iCmdNumber; // 0x04

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iTickCount; // 0x08

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector m_vecViewAngles; // 0x0C

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vector m_vecAimDirection; // 0x18

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flForwardmove; // 0x24

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flSidemove; // 0x28

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flUpmove; // 0x2C

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iButtons; // 0x30

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int m_bImpulse; // 0x34

        public int[] Pad1;

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iWeaponSelect; // 0x38

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iWeaponSubtype; // 0x3C

        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iRandomSeed; // 0x40

        [MarshalAs(UnmanagedType.U1, SizeConst = 2)]
        public ushort m_siMouseDx; // 0x44

        [MarshalAs(UnmanagedType.U1, SizeConst = 2)]
        public ushort m_siMouseDy; // 0x46

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        private bool m_bHasBeenPredicted; // 0x48

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] Pad2;
    }; // size is 100 or 0x64

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct VerifiedUserCmd_t
    {
        public UserCmd_t m_Command;
        public uint m_Crc;
    };


    [StructLayout(LayoutKind.Sequential)]
    public struct GlowSettings
    {
        private readonly byte renderWhenOccluded;
        private readonly byte renderWhenUnoccluded;
        private readonly byte fullBloomRender;

        public GlowSettings(bool renderWhenOccluded, bool renderWhenUnoccluded, bool fullBloom)
        {
            this.renderWhenOccluded = renderWhenOccluded == true ? (byte) 1 : (byte) 0;
            this.renderWhenUnoccluded = renderWhenUnoccluded == true ? (byte) 1 : (byte) 0;
            fullBloomRender = fullBloom == true ? (byte) 1 : (byte) 0;
        }
    }

    public struct GlowColor
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public GlowColor(Color color)
        {
            R = color.R / 255f;
            G = color.G / 255f;
            B = color.B / 255f;
            A = color.A / 255f;
        }

        public GlowColor(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static Color operator *(GlowColor a, int b)
        {
            return Color.FromArgb((int) a.A * b, (int) a.R * b, (int) a.G * b, (int) a.B * b);
        }

        public Color ToColor => Color.FromArgb((int) (A * 255), (int) (R * 255), (int) (G * 255), (int) (B * 255));
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Vector
    {
        public float X;
        public float Y;
        public float Z;

        public Vector(float x = 0, float y = 0, float z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return (a.X == b.X && a.Y == b.Y && a.Z == b.Z);
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return (a.X != b.X || a.Y != b.Y || a.Z != b.Z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector operator /(Vector a, int b)
        {
            return new Vector(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector operator *(Vector a, int b)
        {
            return new Vector(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector operator /(Vector a, float b)
        {
            return new Vector(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector operator *(Vector a, float b)
        {
            return new Vector(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector operator +(Vector a, float b)
        {
            return new Vector(a.X + b, a.Y + b, a.Z * b);
        }
        
        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    case 2:
                        return this.Z;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    case 2:
                        this.Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public Vector ToAngle()
        {
            float yaw, pitch;

            if (X == 0 && Y == 0)
            {
                yaw = 0;
                pitch = Z > 0 ? 270 : 90;
            }
            else
            {
                yaw = (float) Math.Atan2(Y, X) * 180 / 3.141592654f;
                if (yaw < 0)
                    yaw += 360;

                pitch = (float) Math.Atan2(-Z, Length2D) * 180 / 3.141592654f;
                if (pitch < 0)
                    pitch += 360;
            }

            return new Vector(pitch, yaw, 0);
        }

        public Vector ToVector()
        {
            float yaw_sin, pitch_sin, yaw_cos, pitch_cos;
            pitch_sin = (float) Math.Sin(X * 0.017453292512f);
            pitch_cos = (float) Math.Cos(X * 0.017453292512f);
            yaw_sin = (float) Math.Sin(Y * 0.017453292512f);
            yaw_cos = (float) Math.Cos(Y * 0.017453292512f);

            return new Vector(pitch_cos * yaw_cos, pitch_cos * yaw_sin, -pitch_sin);
        }
        
        public float Dot(Vector right)
        {
            return (X * right.X) + (Y * right.Y) + (Z * right.Z);
        }

        public static float Dot(Vector left, Vector right)
        {
            return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
        }

        public float Dot(float x, float y, float z)
        {
            return X * x + Y * y + Z * z;
        }

        public unsafe Vector Transform(Matrix3x4* mat)
        {
            return new Vector()
            {
                X = Dot(mat->_11, mat->_12, mat->_13) + mat->_14,
                Y = Dot(mat->_21, mat->_22, mat->_23) + mat->_24,
                Z = Dot(mat->_31, mat->_32, mat->_33) + mat->_34,
            };
        }

        public unsafe Vector Transform(Matrix3x4 mat)
        {
            return new Vector()
            {
                X = Dot(mat._11, mat._12, mat._13) + mat._14,
                Y = Dot(mat._21, mat._22, mat._23) + mat._24,
                Z = Dot(mat._31, mat._32, mat._33) + mat._34,
            };
        }

        public float Length
        {
            get { return (float) Math.Sqrt((X * X) + (Y * Y) + (Z * Z)); }
        }

        public float Length2D
        {
            get { return (float) Math.Sqrt((X * X) + (Y * Y)); }
        }

        public void Clamp()
        {
            X = ExtraMath.Clamp(X, -89, 89);
            Y = ExtraMath.Clamp(Y, -180, 180);
            Z = 0;
        }

        public void NormalizeAngle()
        {
            X = ExtraMath.Clamp(X, -89, 89);

            while (Y < -180)
            {
                Y += 360;
            }

            while (Y > 180)
            {
                Y -= 360;
            }
        }

        public void Normalize()
        {
            var len = Length;
            len = len == 0.0f ? 0.01f : len;
            X /= len;
            Y /= len;
            Z /= len;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}";
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
    
    public unsafe struct Matrix3x4
    {
        public float _11, _12, _13, _14;
        public float _21, _22, _23, _24;
        public float _31, _32, _33, _34;
    }
    
    public unsafe struct Matrix4x4
    {
        public float _11, _12, _13, _14;
        public float _21, _22, _23, _24;
        public float _31, _32, _33, _34;
        public float _41, _42, _43, _44;

        public override string ToString()
        {
            return
                $"\n{_11} {_12} {_13} {_14}\n{_21} {_22} {_23} {_24}\n{_31} {_32} {_33} {_34}\n{_41} {_42} {_43} {_44}";
        }
    }
}