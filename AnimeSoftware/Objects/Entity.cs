using AnimeSoftware.Offsets;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeSoftware.Objects
{
    internal class Entity : IDisposable
    {
        public void Dispose()
        {

        }

        public int Index;
        public int Ptr => Memory.Read<int>(Memory.Client + signatures.dwEntityList + (Index - 1) * 0x10);

        public string Name
        {
            get
            {

                int radarBasePtr = 0x78;// : 0x54;
                int radarStructSize = 0x174;// : 0x1E0;
                int radarStructPos = 0x18;// : 0x24;

                Encoding enc = Encoding.UTF8;// : Encoding.Unicode;

                int radarBase = Memory.Read<int>(Memory.Client + signatures.dwRadarBase);

                int radarPtr = Memory.Read<int>(radarBase + radarBasePtr);

                int ind = Index + 1;

                int nameAddr = radarPtr + ind * radarStructSize + radarStructPos;
                return Memory.ReadString(nameAddr, 64, enc);

            }
        }
        public string Name2 => Encoding.UTF8.GetString(pInfo.m_szPlayerName);
        public GlowColor glowColor { get; set; }
        public GlowSettings glowSettings { get; set; }
        public bool Glowing { get; set; }
        public int GlowIndex => Memory.Read<int>(Ptr + netvars.m_iGlowIndex);
        public float DistanceToPlayer => Position.DistanceTo(LocalPlayer.Position);

        public Vector Velocity
        {
            get
            {
                Vector velocity = Memory.Read<Vector>(Ptr + netvars.m_vecVelocity);
                return velocity;
            }
        }

        public player_info_s pInfo
        {
            get
            {
                int ClientState = Memory.Read<int>(Memory.Engine + signatures.dwClientState);
                int pInfo = Memory.Read<int>(ClientState + signatures.dwClientState_PlayerInfo);
                pInfo = Memory.Read<int>(pInfo + 0x40);
                pInfo = Memory.Read<int>(pInfo + 0xC);
                pInfo = Memory.Read<int>(pInfo + 0x28 + (Index - 1) * 0x34);
                player_info_s info = Memory.Read<player_info_s>(pInfo);

                return info;
            }
        }

        public bool IsDead => Health <= 0;

        public float Speed
        {
            get
            {
                Vector velocity = Velocity;
                float result = (float)Math.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y + velocity.z * velocity.z);
                return result;
            }
        }
        public Vector Position
        {
            get
            {
                Vector position = Memory.Read<Vector>(Ptr + netvars.m_vecOrigin);
                return position;
            }
        }

        public Vector ViewPosition
        {
            get
            {
                Vector position = Position;
                position.z += Memory.Read<float>(Ptr + netvars.m_vecViewOffset + 0x8);
                return position;
            }
        }
        public int Health => Memory.Read<int>(Ptr + netvars.m_iHealth);

        public bool Dormant => Memory.Read<bool>(Ptr + signatures.m_bDormant);

        public bool isTeam => Memory.Read<int>(Ptr + netvars.m_iTeamNum) == Memory.Read<int>(LocalPlayer.Ptr + netvars.m_iTeamNum);

        public static Entity[] List()
        {

            List<Entity> entityList = new List<Entity>();
            for (int i = 1; i < 64; i++)
            {
                Entity entity = new Entity(i);

                if (entity.Ptr == 0)
                {
                    continue;
                }

                if (entity.Ptr == LocalPlayer.Ptr)
                {
                    LocalPlayer.Index = i;
                    continue;
                }

                entityList.Add(entity);
            }
            return entityList.ToArray();
        }

        public Vector BonePosition(int BoneID)
        {
            int BoneMatrix = Memory.Read<int>(Ptr + netvars.m_dwBoneMatrix);
            Vector position = new Vector
            {
                x = Memory.Read<float>(BoneMatrix + 0x30 * BoneID + 0x0C),
                y = Memory.Read<float>(BoneMatrix + 0x30 * BoneID + 0x1C),
                z = Memory.Read<float>(BoneMatrix + 0x30 * BoneID + 0x2C)
            };
            return position;
        }

        public Entity(int index)
        {
            Index = index;
        }
    }
}
