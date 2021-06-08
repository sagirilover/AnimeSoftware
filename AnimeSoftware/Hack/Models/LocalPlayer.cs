using System.Threading;
using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;

namespace AnimeSoftware.Hack.Models
{
    public class LocalPlayer : Player
    {
        public static int LocalPlayerIndex = -1;

        public static string GlobalName = string.Empty;

        public override int Index
        {
            get
            {
                if (LocalPlayerIndex == -1)
                {
                    EntityList.GetPlayers();
                }

                return LocalPlayerIndex;
            }
        }

        public LocalPlayer(uint ptr = 0) : base(ptr)
        {
            base._ptr = Memory.Read<uint>(Memory.Client + Signatures.dwLocalPlayer);
        }

        public int ShotsFired => Memory.Read<int>(Ptr + Netvars.m_iShotsFired);

        public Vector ViewAngle
        {
            get
            {
                var viewAngles = Memory.Read<Vector>(Engine.ClientState + Signatures.dwClientState_ViewAngles);
                return viewAngles;
            }
            set
            {
                value.Clamp();
                Memory.Write<Vector>(Engine.ClientState + Signatures.dwClientState_ViewAngles, value);
            }
        }

        public Vector PunchAngle => Memory.Read<Vector>(Ptr + Netvars.m_aimPunchAngle);

        public void Jump() => Client.Jump();

        public float SideSpeed
        {
            set => Client.SideSpeed = value;
        }

        public float ForwardSpeed
        {
            set => Client.ForwardSpeed = value;
        }

        public void MoveRight() => Client.MoveRight();

        public void MoveLeft() => Client.MoveLeft();

        public void MoveClearY() => Client.MoveClearY();

        public void MoveForward() => Client.MoveForward();

        public void MoveClearX() => Client.MoveClearX();

        public int Use
        {
            set
            {
                Client.Use = value;
            }
        }

        public int CrossHair => Memory.Read<int>(Ptr + Netvars.m_iCrosshairId);
    }
}