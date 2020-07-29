using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeSoftware.Offsets;

namespace AnimeSoftware.Injections
{
    public class ConVar
    {
        public int pThis;
        public ConVar(int Pointer)
        {
            pThis = Pointer;
        }
        public ConVar(string name)
        {
            pThis = GetConVarAddress(name);
        }

        public int GetStringHash(string name)
        {
            CharCodes codes = Memory.Read<CharCodes>(Memory.vstdlib + signatures.convar_name_hash_table);
            int v2 = 0;
            int v3 = 0;
            for (int i = 0; i < name.Length; i += 2)
            {
                v3 = codes.tab[v2 ^ char.ToUpper(name[i])];
                if (i + 1 == name.Length)
                    break;
                v2 = codes.tab[v3 ^ char.ToUpper(name[i + 1])];
            }
            return v2 | (v3 << 8);
        }

        public void ClearCallbacks()
        {
            Memory.Write<int>(pThis + 0x44 + 0xC, 0);
        }
        public int GetConVarAddress(string name)
        {
            var hash = GetStringHash(name);

            int CvarEngine = Memory.Read<int>(Memory.vstdlib + signatures.interface_engine_cvar);
            int Pointer = Memory.Read<int>(Memory.Read<int>(CvarEngine + 0x34) + ((byte)hash * 4));
            Encoding enc = Encoding.UTF8;
            while ((IntPtr)Pointer != IntPtr.Zero)
            {
                if (Memory.Read<int>(Pointer) == hash)
                {
                    int ConVarPointer = Memory.Read<int>(Pointer + 0x4);

                    if (Memory.ReadText(Memory.pHandle, (IntPtr)Memory.Read<int>(ConVarPointer + 0xC)) == name)
                    {
                        return ConVarPointer;
                    }
                }

                Pointer = Memory.Read<int>(Pointer + 0xC);
            }
            return (int)IntPtr.Zero;
        }


    }
}
