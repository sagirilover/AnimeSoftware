using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;

namespace AnimeSoftware.Hack.Models
{
    public class Weapon : Entity
    {
        public Weapon(int index) : base(index)
        {
        }

        public Weapon(uint ptr) : base(ptr)
        {
        }

        public float NextPrimaryAttack => Memory.Read<float>(Ptr + Netvars.m_flNextPrimaryAttack);
        
        public bool CanFire => NextPrimaryAttack <= 0 || NextPrimaryAttack < Memory.Read<float>(Memory.Client + Netvars.m_nTickBase);
        
        public short Id => Memory.Read<short>(Ptr + Netvars.m_iItemDefinitionIndex);

        public bool IsBomb() => Id == 49;

        public bool IsGrenade()
        {
            switch (Id)
            {
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsKnife()
        {
            switch (Id)
            {
                case 41:
                case 42:
                case 59:
                case 500:
                case 505:
                case 506:
                case 507:
                case 508:
                case 509:
                case 512:
                case 514:
                case 515:
                case 516:
                case 519:
                case 520:
                case 522:
                case 523:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsPistol()
        {
            switch (Id)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 30:
                case 32:
                case 36:
                case 61:
                case 63:
                case 64:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsSniper()
        {
            switch (Id)
            {
                case 9:
                case 11:
                case 38:
                case 40:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsRifile()
        {
            switch (Id)
            {
                case 7:
                case 8:
                case 10:
                case 13:
                case 16:
                case 39:
                case 60:
                    return true;

                default:
                    return false;
            }
        }

        public bool IsSmg()
        {
            switch (Id)
            {
                case 17:
                case 19:
                case 24:
                case 26:
                case 33:
                case 34:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsShotgun()
        {
            switch (Id)
            {
                case 25:
                case 27:
                case 29:
                case 35:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsLmg()
        {
            switch (Id)
            {
                case 14:
                case 28:
                    return true;
                default:
                    return false;
            }
        }

        /*
        public string Name
        {
            get
            {
                if (IsKnife()) return "Knife";

                return Id switch
                {
                    1 => "Desert Eagle",
                    2 => "Duel Berettas",
                    3 => "Five-SeveN",
                    4 => "Glock-18",
                    7 => "AK-47",
                    8 => "AUG",
                    9 => "AWP",
                    10 => "FAMAS",
                    11 => "G3SG1",
                    13 => "Galil AR",
                    14 => "M249",
                    16 => "M4A4",
                    17 => "MAC-10",
                    19 => "P90",
                    24 => "UMP-45",
                    25 => "XM1014",
                    26 => "PP-Bizon",
                    27 => "MAG-7",
                    28 => "Negev",
                    29 => "Sawed-Off",
                    30 => "Tec-9",
                    31 => "Zeus x27",
                    32 => "P2000",
                    33 => "MP7",
                    34 => "MP9",
                    35 => "Nova",
                    36 => "P250",
                    38 => "SCAR-20",
                    39 => "SG 553",
                    40 => "SSG 08",
                    43 => "Flashbang",
                    44 => "HE Grenade",
                    45 => "Smoke Grenade",
                    46 => "Molotov",
                    47 => "Decoy",
                    48 => "Incendiary",
                    49 => "C4",
                    69 => "M4A1-S",
                    61 => "USP-S",
                    63 => "CZ75-Auto",
                    64 => "R8 Revolver",
                    _ => Id.ToString()
                };
            }
        }
    */
    }
}