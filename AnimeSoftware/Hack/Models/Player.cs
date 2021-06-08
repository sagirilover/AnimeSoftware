using System;
using System.Text;
using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;

namespace AnimeSoftware.Hack.Models
{
    public class Player : Entity
    {
        public Player(int index) : base(index)
        {
        }

        public Player(uint ptr) : base(ptr)
        {
        }

        public EEntityFlags Flags => (EEntityFlags) Memory.Read<int>(Ptr + Netvars.m_fFlags);
        
        public bool OnGround => (Flags & EEntityFlags.FL_ONGROUND) != 0;

        public bool Dormant => Memory.Read<byte>(Ptr + Signatures.m_bDormant) != 0;

        public int Health => Memory.Read<int>(Ptr + Netvars.m_iHealth);

        public Matrix3x4 CoordinateFrame => Memory.Read<Matrix3x4>(Ptr + Netvars.m_rgflCoordinateFrame);

        public Vector Mins => Memory.Read<Vector>(Ptr + Netvars.m_vecMins);

        public Vector Maxs => Memory.Read<Vector>(Ptr + Netvars.m_vecMaxs);

        public int Team => Memory.Read<int>(Ptr + Netvars.m_iTeamNum);
        
        public Weapon ActiveWeapon => new Weapon(Memory.Read<int>(Ptr + Netvars.m_hActiveWeapon) & 0xFFF);
        
        public Weapon Weapon(int index) => new Weapon(Memory.Read<int>(Ptr + Netvars.m_hMyWeapons + (index - 1) * 0x4) & 0xFFF);
        
        public Vector GetBonePosition(int boneId)
        {
            var boneMatrix = Memory.Read<int>(Ptr + Netvars.m_dwBoneMatrix);
            var position = new Vector
            {
                X = Memory.Read<float>((IntPtr)boneMatrix + 0x30 * boneId + 0x0C),
                Y = Memory.Read<float>((IntPtr)boneMatrix + 0x30 * boneId + 0x1C),
                Z = Memory.Read<float>((IntPtr)boneMatrix + 0x30 * boneId + 0x2C)
            };
            return position;
        }
        
        public Vector Position => Memory.Read<Vector>(Ptr + Netvars.m_vecOrigin);

        public Vector EyePosition => Position + Memory.Read<Vector>(Ptr + Netvars.m_vecViewOffset);

        public Vector Velocity => Memory.Read<Vector>(Ptr + Netvars.m_vecVelocity);

        public bool Spotted
        {
            get => Memory.Read<int>(Ptr + Netvars.m_bSpotted) != 0;
            set => Memory.Write(Ptr + Netvars.m_bSpotted, value ? 1 : 0);
        }
        
        public int GlowIndex => Memory.Read<int>(Ptr + Netvars.m_iGlowIndex);
        
        public PlayerInfo Info
        {
            get
            {
                var pInfo = Memory.Read<int>(Engine.ClientState + Signatures.dwClientState_PlayerInfo);
                pInfo = Memory.Read<int>(pInfo + 0x40);
                pInfo = Memory.Read<int>(pInfo + 0xC);
                pInfo = Memory.Read<int>(pInfo + 0x28 + (Index - 1) * 0x34);
                var info = Memory.Read<PlayerInfo>(pInfo);

                return info;
            }
        }

        public string Name => Encoding.UTF8.GetString(Info.Name);
    }
}