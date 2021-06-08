using System;
using System.Threading;
using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;

namespace AnimeSoftware.Hack.Models
{
     public static class Client
    {
        public static float SideSpeed
        {
            set
            {
                Memory.WriteBytes(Memory.Read<int>(Memory.Client + CalcedOffsets.cl_sidespeed),
                    BitConverter.GetBytes(BitConverter.ToInt32(BitConverter.GetBytes(value), 0) ^
                                          CalcedOffsets.xor_cl_sidespeed));
            }
        }

        public static float ForwardSpeed
        {
            set
            {
                Memory.WriteBytes(Memory.Read<int>(Memory.Client + CalcedOffsets.cl_forwardspeed),
                    BitConverter.GetBytes(BitConverter.ToInt32(BitConverter.GetBytes(value), 0) ^
                                          CalcedOffsets.xor_cl_forwardspeed));
            }
        }

        public static void MoveRight()
        {
            ClientCMD.Exec("-moveleft");
            Thread.Sleep(1);
            ClientCMD.Exec("+moveright");
        }

        public static void MoveLeft()
        {
            ClientCMD.Exec("-moveright");
            Thread.Sleep(1);
            ClientCMD.Exec("+moveleft");
        }

        public static void MoveClearY()
        {
            ClientCMD.Exec("-moveright");
            Thread.Sleep(1);
            ClientCMD.Exec("-moveleft");
        }

        public static void MoveForward()
        {
            ClientCMD.Exec("+forward");
        }

        public static void MoveClearX()
        {
            ClientCMD.Exec("-forward");
        }

        public static void Jump()
        {
            Memory.Write<int>(Memory.Client + Signatures.dwForceJump, 5);
            Thread.Sleep(15);
            Memory.Write<int>(Memory.Client + Signatures.dwForceJump, 4);
        }

        public static float ViewmodelX
        {
            set => Memory.WriteBytes(Memory.Read<int>(Memory.Client + CalcedOffsets.viewmodel_x),
                BitConverter.GetBytes(BitConverter.ToInt32(BitConverter.GetBytes(value), 0) ^
                                      CalcedOffsets.xor_viewmodel_x));
        }

        public static float ViewmodelY
        {
            set => Memory.WriteBytes(Memory.Read<int>(Memory.Client + CalcedOffsets.viewmodel_y),
                BitConverter.GetBytes(BitConverter.ToInt32(BitConverter.GetBytes(value), 0) ^
                                      CalcedOffsets.xor_viewmodel_y));
        }

        public static float ViewmodelZ
        {
            set => Memory.WriteBytes(Memory.Read<int>(Memory.Client + CalcedOffsets.viewmodel_z),
                BitConverter.GetBytes(BitConverter.ToInt32(BitConverter.GetBytes(value), 0) ^
                                      CalcedOffsets.xor_viewmodel_z));
        }
        
        public static int Use
        {
            set
            {
                Memory.Write<int>(Memory.Read<int>(Memory.Client + CalcedOffsets.dwUse), value);
            }
        }
    }
}