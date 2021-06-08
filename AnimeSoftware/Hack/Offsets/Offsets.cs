using AnimeSoftware.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnimeSoftware.Offsets.NetVarManager;

namespace AnimeSoftware.Offsets
{
    internal class Signatures
    {
        public static int dwClientState;
        public static int dwClientState_GetLocalPlayer;
        public static int dwClientState_IsHLTV;
        public static int dwClientState_Map;
        public static int dwClientState_MapDirectory;
        public static int dwClientState_MaxPlayer;
        public static int dwClientState_PlayerInfo;
        public static int dwClientState_State;
        public static int dwClientState_ViewAngles;
        public static int clientstate_delta_ticks;
        public static int clientstate_last_outgoing_command;
        public static int clientstate_choked_commands;
        public static int clientstate_net_channel;
        public static int dwEntityList;
        public static int dwForceAttack;
        public static int dwForceAttack2;
        public static int dwForceBackward;
        public static int dwForceForward;
        public static int dwForceJump;
        public static int dwForceLeft;
        public static int dwForceRight;
        public static int dwGameDir;
        public static int dwGameRulesProxy;
        public static int dwGetAllClasses;
        public static int dwGlobalVars;
        public static int dwGlowObjectManager;
        public static int dwInput;
        public static int dwInterfaceLinkList;
        public static int dwLocalPlayer;
        public static int dwMouseEnable;
        public static int dwMouseEnablePtr;
        public static int dwPlayerResource;
        public static int dwRadarBase;
        public static int dwSensitivity;
        public static int dwSensitivityPtr;
        public static int dwSetClanTag;
        public static int dwViewMatrix;
        public static int dwWeaponTable;
        public static int dwWeaponTableIndex;
        public static int dwYawPtr;
        public static int dwZoomSensitivityRatioPtr;
        public static int dwbSendPackets;
        public static int dwppDirect3DDevice9;
        public static int m_pStudioHdr;
        public static int m_yawClassPtr;
        public static int m_pitchClassPtr;
        public static int interface_engine_cvar;
        public static int convar_name_hash_table;
        public static int m_bDormant;
        public static int model_ambient_min;
        public static int set_abs_angles;
        public static int set_abs_origin;
        public static int is_c4_owner;
        public static int force_update_spectator_glow;
        public static int anim_overlays;
        public static int m_flSpawnTime;
        public static int find_hud_element;

        public static void Init()
        {
            Log.Debug("Offset scan has started.");
            dwClientState =
                Memory.Read<int>(Memory.Engine + Memory.FindPattern("A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0",
                    Memory.Engine, Memory.EngineSize) + 1) + 0 - Memory.Engine;
            dwClientState_GetLocalPlayer = Memory.Read<int>(Memory.Engine +
                                                            Memory.FindPattern("8B 80 ? ? ? ? 40 C3", Memory.Engine,
                                                                Memory.EngineSize) + 2) + 0;
            dwClientState_IsHLTV = Memory.Read<int>(Memory.Engine +
                                                    Memory.FindPattern("80 BF ? ? ? ? ? 0F 84 ? ? ? ? 32 DB",
                                                        Memory.Engine, Memory.EngineSize) + 2) + 0;
            dwClientState_Map = Memory.Read<int>(Memory.Engine +
                                                 Memory.FindPattern("05 ? ? ? ? C3 CC CC CC CC CC CC CC A1",
                                                     Memory.Engine, Memory.EngineSize) + 1) + 0;
            dwClientState_MapDirectory = Memory.Read<int>(Memory.Engine +
                                                          Memory.FindPattern("B8 ? ? ? ? C3 05 ? ? ? ? C3",
                                                              Memory.Engine, Memory.EngineSize) + 7) + 0;
            dwClientState_MaxPlayer = Memory.Read<int>(Memory.Engine +
                                                       Memory.FindPattern(
                                                           "A1 ? ? ? ? 8B 80 ? ? ? ? C3 CC CC CC CC 55 8B EC 8A 45 08",
                                                           Memory.Engine, Memory.EngineSize) + 7) + 0;
            dwClientState_PlayerInfo = Memory.Read<int>(Memory.Engine +
                                                        Memory.FindPattern("8B 89 ? ? ? ? 85 C9 0F 84 ? ? ? ? 8B 01",
                                                            Memory.Engine, Memory.EngineSize) + 2) + 0;
            dwClientState_State = Memory.Read<int>(Memory.Engine +
                                                   Memory.FindPattern("83 B8 ? ? ? ? ? 0F 94 C0 C3", Memory.Engine,
                                                       Memory.EngineSize) + 2) + 0;
            dwClientState_ViewAngles = Memory.Read<int>(Memory.Engine +
                                                        Memory.FindPattern("F3 0F 11 80 ? ? ? ? D9 46 04 D9 05",
                                                            Memory.Engine, Memory.EngineSize) + 4) + 0;
            clientstate_delta_ticks = Memory.Read<int>(Memory.Engine +
                                                       Memory.FindPattern(
                                                           "C7 87 ? ? ? ? ? ? ? ? FF 15 ? ? ? ? 83 C4 08",
                                                           Memory.Engine, Memory.EngineSize) + 2) + 0;
            clientstate_last_outgoing_command = Memory.Read<int>(Memory.Engine +
                                                                 Memory.FindPattern("8B 8F ? ? ? ? 8B 87 ? ? ? ? 41",
                                                                     Memory.Engine, Memory.EngineSize) + 2) + 0;
            clientstate_choked_commands = Memory.Read<int>(Memory.Engine +
                                                           Memory.FindPattern("8B 87 ? ? ? ? 41", Memory.Engine,
                                                               Memory.EngineSize) + 2) + 0;
            clientstate_net_channel = Memory.Read<int>(Memory.Engine +
                                                       Memory.FindPattern("8B 8F ? ? ? ? 8B 01 8B 40 18", Memory.Engine,
                                                           Memory.EngineSize) + 2) + 0;
            dwEntityList =
                Memory.Read<int>(Memory.Client + Memory.FindPattern("BB ? ? ? ? 83 FF 01 0F 8C ? ? ? ? 3B F8",
                    Memory.Client, Memory.ClientSize) + 1) + 0 - Memory.Client;
            dwForceAttack =
                Memory.Read<int>(Memory.Client + Memory.FindPattern("89 0D ? ? ? ? 8B 0D ? ? ? ? 8B F2 8B C1 83 CE 04",
                    Memory.Client, Memory.ClientSize) + 2) + 0 - Memory.Client;
            dwForceAttack2 =
                Memory.Read<int>(Memory.Client + Memory.FindPattern("89 0D ? ? ? ? 8B 0D ? ? ? ? 8B F2 8B C1 83 CE 04",
                    Memory.Client, Memory.ClientSize) + 2) + 12 - Memory.Client;
            dwForceBackward =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("55 8B EC 51 53 8A 5D 08", Memory.Client, Memory.ClientSize) +
                                 287) + 0 - Memory.Client;
            dwForceForward =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("55 8B EC 51 53 8A 5D 08", Memory.Client, Memory.ClientSize) +
                                 245) + 0 - Memory.Client;
            dwForceJump =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("8B 0D ? ? ? ? 8B D6 8B C1 83 CA 02", Memory.Client,
                                     Memory.ClientSize) + 2) + 0 - Memory.Client;
            dwForceLeft =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("55 8B EC 51 53 8A 5D 08", Memory.Client, Memory.ClientSize) +
                                 465) + 0 - Memory.Client;
            dwForceRight =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("55 8B EC 51 53 8A 5D 08", Memory.Client, Memory.ClientSize) +
                                 512) + 0 - Memory.Client;
            dwGameDir = Memory.Read<int>(Memory.Engine + Memory.FindPattern("68 ? ? ? ? 8D 85 ? ? ? ? 50 68 ? ? ? ? 68",
                Memory.Engine, Memory.EngineSize) + 1) + 0 - Memory.Engine;
            dwGameRulesProxy =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("A1 ? ? ? ? 85 C0 0F 84 ? ? ? ? 80 B8 ? ? ? ? ? 74 7A",
                                     Memory.Client, Memory.ClientSize) + 1) + 0 - Memory.Client;
            dwGetAllClasses =
                Memory.Read<int>(Memory.Read<int>(Memory.Client +
                                                  Memory.FindPattern(
                                                      "A1 ? ? ? ? C3 CC CC CC CC CC CC CC CC CC CC A1 ? ? ? ? B9",
                                                      Memory.Client, Memory.ClientSize) + 1) + 0) + 0 - Memory.Client;
            dwGlobalVars =
                Memory.Read<int>(Memory.Engine + Memory.FindPattern("68 ? ? ? ? 68 ? ? ? ? FF 50 08 85 C0",
                    Memory.Engine, Memory.EngineSize) + 1) + 0 - Memory.Engine;
            dwGlowObjectManager =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("A1 ? ? ? ? A8 01 75 4B", Memory.Client, Memory.ClientSize) + 1) +
                4 - Memory.Client;
            dwInput = Memory.Read<int>(Memory.Client +
                                       Memory.FindPattern("B9 ? ? ? ? F3 0F 11 04 24 FF 50 10", Memory.Client,
                                           Memory.ClientSize) + 1) + 0 - Memory.Client;
            dwInterfaceLinkList = Memory.Client +
                Memory.FindPattern("8B 35 ? ? ? ? 57 85 F6 74 ? 8B 7D 08 8B 4E 04 8B C7 8A 11 3A 10", Memory.Client,
                    Memory.ClientSize) + 0 - Memory.Client;
            dwLocalPlayer =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("8D 34 85 ? ? ? ? 89 15 ? ? ? ? 8B 41 08 8B 48 04 83 F9 FF",
                                     Memory.Client, Memory.ClientSize) + 3) + 4 - Memory.Client;
            dwMouseEnable =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("B9 ? ? ? ? FF 50 34 85 C0 75 10", Memory.Client,
                                     Memory.ClientSize) + 1) + 48 - Memory.Client;
            dwMouseEnablePtr =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("B9 ? ? ? ? FF 50 34 85 C0 75 10", Memory.Client,
                                     Memory.ClientSize) + 1) + 0 - Memory.Client;
            dwPlayerResource =
                Memory.Read<int>(Memory.Client + Memory.FindPattern("8B 3D ? ? ? ? 85 FF 0F 84 ? ? ? ? 81 C7",
                    Memory.Client, Memory.ClientSize) + 2) + 0 - Memory.Client;
            dwRadarBase =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("A1 ? ? ? ? 8B 0C B0 8B 01 FF 50 ? 46 3B 35 ? ? ? ? 7C EA 8B 0D",
                                     Memory.Client, Memory.ClientSize) + 1) + 0 - Memory.Client;
            dwSensitivity =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern(
                                     "81 F9 ? ? ? ? 75 1D F3 0F 10 05 ? ? ? ? F3 0F 11 44 24 ? 8B 44 24 0C 35 ? ? ? ? 89 44 24 0C",
                                     Memory.Client, Memory.ClientSize) + 2) + 44 - Memory.Client;
            dwSensitivityPtr =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern(
                                     "81 F9 ? ? ? ? 75 1D F3 0F 10 05 ? ? ? ? F3 0F 11 44 24 ? 8B 44 24 0C 35 ? ? ? ? 89 44 24 0C",
                                     Memory.Client, Memory.ClientSize) + 2) + 0 - Memory.Client;
            dwSetClanTag = Memory.Engine +
                Memory.FindPattern("53 56 57 8B DA 8B F9 FF 15", Memory.Engine, Memory.EngineSize) + 0 - Memory.Engine;
            dwViewMatrix =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("0F 10 05 ? ? ? ? 8D 85 ? ? ? ? B9", Memory.Client,
                                     Memory.ClientSize) + 3) + 176 - Memory.Client;
            dwWeaponTable =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("B9 ? ? ? ? 6A 00 FF 50 08 C3", Memory.Client, Memory.ClientSize) +
                                 1) + 0 - Memory.Client;
            dwWeaponTableIndex = Memory.Read<int>(Memory.Client +
                                                  Memory.FindPattern("39 86 ? ? ? ? 74 06 89 86 ? ? ? ? 8B 86",
                                                      Memory.Client, Memory.ClientSize) + 2) + 0;
            dwYawPtr = Memory.Read<int>(Memory.Client +
                                        Memory.FindPattern(
                                            "81 F9 ? ? ? ? 75 16 F3 0F 10 05 ? ? ? ? F3 0F 11 45 ? 81 75 ? ? ? ? ? EB 0A 8B 01 8B 40 30 FF D0 D9 5D 0C 8B 55 08",
                                            Memory.Client, Memory.ClientSize) + 2) + 0 - Memory.Client;
            dwZoomSensitivityRatioPtr = Memory.Read<int>(Memory.Client +
                                                         Memory.FindPattern(
                                                             "81 F9 ? ? ? ? 75 1A F3 0F 10 05 ? ? ? ? F3 0F 11 45 ? 8B 45 F4 35 ? ? ? ? 89 45 FC EB 0A 8B 01 8B 40 30 FF D0 D9 5D FC A1",
                                                             Memory.Client, Memory.ClientSize) + 2) + 0 - Memory.Client;
            dwbSendPackets = Memory.Engine +
                Memory.FindPattern("B3 01 8B 01 8B 40 10 FF D0 84 C0 74 0F 80 BF ? ? ? ? ? 0F 84", Memory.Engine,
                    Memory.EngineSize) + 1 - Memory.Engine;
            dwppDirect3DDevice9 =
                Memory.Read<int>(Memory.vstdlib +
                                 Memory.FindPattern("A1 ? ? ? ? 50 8B 08 FF 51 0C", Memory.vstdlib,
                                     Memory.vstdlibSize) + 1) + 0 - Memory.vstdlib;
            m_pStudioHdr = Memory.Read<int>(Memory.Client +
                                            Memory.FindPattern(
                                                "8B B6 ? ? ? ? 85 F6 74 05 83 3E 00 75 02 33 F6 F3 0F 10 44 24",
                                                Memory.Client, Memory.ClientSize) + 2) + 0;
            m_yawClassPtr = Memory.Read<int>(Memory.Client +
                                             Memory.FindPattern(
                                                 "81 F9 ? ? ? ? 75 16 F3 0F 10 05 ? ? ? ? F3 0F 11 45 ? 81 75 ? ? ? ? ? EB 0A 8B 01 8B 40 30 FF D0 D9 5D 0C 8B 55 08",
                                                 Memory.Client, Memory.ClientSize) + 2)
                + 0 - Memory.Client;
            m_pitchClassPtr =
                Memory.Read<int>(Memory.Client +
                                 Memory.FindPattern("A1 ? ? ? ? 89 74 24 28", Memory.Client, Memory.ClientSize) + 1) +
                0 - Memory.Client;
            interface_engine_cvar =
                Memory.Read<int>(Memory.vstdlib +
                                 Memory.FindPattern("8B 0D ? ? ? ? C7 05", Memory.vstdlib, Memory.vstdlibSize) + 2) +
                0 - Memory.vstdlib;
            convar_name_hash_table =
                Memory.Read<int>(
                    Memory.vstdlib + Memory.FindPattern("8B 3C 85", Memory.vstdlib, Memory.vstdlibSize) + 3) + 0 -
                Memory.vstdlib;
            m_bDormant = Memory.Read<int>(Memory.Client +
                                          Memory.FindPattern("8A 81 ? ? ? ? C3 32 C0", Memory.Client,
                                              Memory.ClientSize) + 2) + 8;
            model_ambient_min =
                Memory.Read<int>(Memory.Engine +
                                 Memory.FindPattern(
                                     "F3 0F 10 0D ? ? ? ? F3 0F 11 4C 24 ? 8B 44 24 20 35 ? ? ? ? 89 44 24 0C",
                                     Memory.Engine, Memory.EngineSize) + 4) + 0 - Memory.Engine;
            set_abs_angles = Memory.Client +
                Memory.FindPattern("55 8B EC 83 E4 F8 83 EC 64 53 56 57 8B F1 E8", Memory.Client, Memory.ClientSize) +
                0 - Memory.Client;
            set_abs_origin = Memory.Client +
                             Memory.FindPattern("55 8B EC 83 E4 F8 51 53 56 57 8B F1 E8", Memory.Client,
                                 Memory.ClientSize) + 0 -
                             Memory.Client;
            is_c4_owner = Memory.Client + Memory.FindPattern("56 8B F1 85 F6 74 31", Memory.Client, Memory.ClientSize) +
                0 - Memory.Client;
            force_update_spectator_glow = Memory.Client +
                                          Memory.FindPattern("74 07 8B CB E8 ? ? ? ? 83 C7 10", Memory.Client,
                                              Memory.ClientSize) + 0 -
                                          Memory.Client;
            anim_overlays = Memory.Read<int>(Memory.Client +
                                             Memory.FindPattern("8B 89 ? ? ? ? 8D 0C D1", Memory.Client,
                                                 Memory.ClientSize) + 2) + 0;
            m_flSpawnTime = Memory.Read<int>(Memory.Client +
                                             Memory.FindPattern("89 86 ? ? ? ? E8 ? ? ? ? 80 BE ? ? ? ? ?",
                                                 Memory.Client, Memory.ClientSize) + 2) + 0;
            find_hud_element = Memory.Client + Memory.FindPattern("55 8B EC 53 8B 5D 08 56 57 8B F9 33 F6 39 77 28",
                Memory.Client, Memory.ClientSize) + 0;
            Log.Debug("Offsets scanned.");
        }
    }

    internal class Netvars
    {
        public static int m_ArmorValue;
        public static int m_Collision;
        public static int m_CollisionGroup;
        public static int m_Local;
        public static int m_MoveType;
        public static int m_OriginalOwnerXuidHigh;
        public static int m_OriginalOwnerXuidLow;
        public static int m_aimPunchAngle;
        public static int m_aimPunchAngleVel;
        public static int m_bGunGameImmunity;
        public static int m_bHasDefuser;
        public static int m_bHasHelmet;
        public static int m_bInReload;
        public static int m_bIsDefusing;
        public static int m_bIsScoped;
        public static int m_bSpotted;
        public static int m_bSpottedByMask;
        public static int m_dwBoneMatrix;
        public static int m_fAccuracyPenalty;
        public static int m_fFlags;
        public static int m_flFallbackWear;
        public static int m_flFlashDuration;
        public static int m_flFlashMaxAlpha;
        public static int m_flNextPrimaryAttack;
        public static int m_hActiveWeapon;
        public static int m_hMyWeapons;
        public static int m_hObserverTarget;
        public static int m_hOwner;
        public static int m_hOwnerEntity;
        public static int m_iAccountID;
        public static int m_iClip1;
        public static int m_iCompetitiveRanking;
        public static int m_iCompetitiveWins;
        public static int m_iCrosshairId;
        public static int m_iEntityQuality;
        public static int m_iFOVStart;
        public static int m_iFOV;
        public static int m_iGlowIndex;
        public static int m_iHealth;
        public static int m_iItemDefinitionIndex;
        public static int m_iItemIDHigh;
        public static int m_iObserverMode;
        public static int m_iShotsFired;
        public static int m_iState;
        public static int m_iTeamNum;
        public static int m_lifeState;
        public static int m_nFallbackPaintKit;
        public static int m_nFallbackSeed;
        public static int m_nFallbackStatTrak;
        public static int m_nForceBone;
        public static int m_nTickBase;
        public static int m_rgflCoordinateFrame;
        public static int m_szCustomName;
        public static int m_szLastPlaceName;
        public static int m_vecOrigin;
        public static int m_vecVelocity;
        public static int m_vecViewOffset;
        public static int m_viewPunchAngle;
        public static int m_thirdPersonViewAngles;
        public static int m_clrRender;
        public static int m_flC4Blow;
        public static int m_flTimerLength;
        public static int m_flDefuseLength;
        public static int m_flDefuseCountDown;
        public static int cs_gamerules_data;
        public static int m_SurvivalRules;
        public static int m_SurvivalGameRuleDecisionTypes;
        public static int m_bIsValveDS;
        public static int m_bFreezePeriod;
        public static int m_bBombPlanted;
        public static int m_bIsQueuedMatchmaking;
        public static int m_flSimulationTime;
        public static int m_flLowerBodyYawTarget;
        public static int m_angEyeAnglesX;
        public static int m_angEyeAnglesY;
        public static int m_flNextAttack;
        public static int m_iMostRecentModelBoneCounter;
        public static int m_flLastBoneSetupTime;
        public static int m_bStartedArming;
        public static int m_bUseCustomBloomScale;
        public static int m_bUseCustomAutoExposureMin;
        public static int m_bUseCustomAutoExposureMax;
        public static int m_flCustomBloomScale;
        public static int m_flCustomAutoExposureMin;
        public static int m_flCustomAutoExposureMax;
        public static int m_vecMins;
        public static int m_vecMaxs;


        public static void Init()
        {
            NetVarManager.Init();

            m_vecMins = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_vecMins"];
            m_vecMaxs = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_vecMins"];
            m_ArmorValue = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_ArmorValue"];
            m_Collision = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_Collision"];
            m_CollisionGroup = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_CollisionGroup"];
            m_Local = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_Local"];
            m_MoveType = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_nRenderMode"] + 1;
            m_OriginalOwnerXuidHigh = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_OriginalOwnerXuidHigh"];
            m_OriginalOwnerXuidLow = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_OriginalOwnerXuidLow"];
            m_aimPunchAngle = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_aimPunchAngle"];
            m_aimPunchAngleVel = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_aimPunchAngleVel"];
            m_bGunGameImmunity = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_bGunGameImmunity"];
            m_bHasDefuser = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_bHasDefuser"];
            m_bHasHelmet = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_bHasHelmet"];
            m_bInReload = (int) ((Hashtable) NetVars["DT_BaseCombatWeapon"])["m_flNextPrimaryAttack"] + 109;
            m_bIsDefusing = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_bIsDefusing"];
            m_bIsScoped = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_bIsScoped"];
            m_bSpotted = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_bSpotted"];
            m_bSpottedByMask = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_bSpottedByMask"];
            m_dwBoneMatrix = (int) ((Hashtable) NetVars["DT_BaseAnimating"])["m_nForceBone"] + 28;
            m_fAccuracyPenalty = (int) ((Hashtable) NetVars["DT_WeaponCSBase"])["m_fAccuracyPenalty"];
            m_fFlags = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_fFlags"];
            m_flFallbackWear = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_flFallbackWear"];
            m_flFlashDuration = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_flFlashDuration"];
            m_flFlashMaxAlpha = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_flFlashMaxAlpha"];
            m_flNextPrimaryAttack = (int) ((Hashtable) NetVars["DT_BaseCombatWeapon"])["m_flNextPrimaryAttack"];
            m_hActiveWeapon = (int) ((Hashtable) NetVars["DT_BaseCombatCharacter"])["m_hActiveWeapon"];
            m_hMyWeapons = (int) ((Hashtable) NetVars["DT_BaseCombatCharacter"])["m_hActiveWeapon"] + -256;
            m_hObserverTarget = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_hObserverTarget"];
            m_hOwner = (int) ((Hashtable) NetVars["DT_BaseViewModel"])["m_hOwner"];
            m_hOwnerEntity = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_hOwnerEntity"];
            m_iAccountID = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_iAccountID"];
            m_iClip1 = (int) ((Hashtable) NetVars["DT_BaseCombatWeapon"])["m_iClip1"];
            m_iCompetitiveRanking = (int) ((Hashtable) NetVars["DT_CSPlayerResource"])["m_iCompetitiveRanking"];
            m_iCompetitiveWins = (int) ((Hashtable) NetVars["DT_CSPlayerResource"])["m_iCompetitiveWins"];
            m_iCrosshairId = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_bHasDefuser"] + 92;
            m_iEntityQuality = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_iEntityQuality"];
            m_iFOVStart = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_iFOVStart"];
            m_iFOV = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_iFOV"];
            m_iGlowIndex = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_flFlashDuration"] + 24;
            m_iHealth = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_iHealth"];
            m_iItemDefinitionIndex = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_iItemDefinitionIndex"];
            m_iItemIDHigh = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_iItemIDHigh"];
            m_iObserverMode = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_iObserverMode"];
            m_iShotsFired = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_iShotsFired"];
            m_iState = (int) ((Hashtable) NetVars["DT_BaseCombatWeapon"])["m_iState"];
            m_iTeamNum = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_iTeamNum"];
            m_lifeState = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_lifeState"];
            m_nFallbackPaintKit = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_nFallbackPaintKit"];
            m_nFallbackSeed = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_nFallbackSeed"];
            m_nFallbackStatTrak = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_nFallbackStatTrak"];
            m_nForceBone = (int) ((Hashtable) NetVars["DT_BaseAnimating"])["m_nForceBone"];
            m_nTickBase = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_nTickBase"];
            m_rgflCoordinateFrame = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_CollisionGroup"] + -48;
            m_szCustomName = (int) ((Hashtable) NetVars["DT_BaseAttributableItem"])["m_szCustomName"];
            m_szLastPlaceName = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_szLastPlaceName"];
            m_vecOrigin = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_vecOrigin"];
            m_vecVelocity = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_vecVelocity[0]"];
            m_vecViewOffset = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_vecViewOffset[0]"];
            m_viewPunchAngle = (int) ((Hashtable) NetVars["DT_BasePlayer"])["m_viewPunchAngle"];
            m_thirdPersonViewAngles = (int) ((Hashtable) NetVars["DT_BasePlayer"])["deadflag"] + 4;
            m_clrRender = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_clrRender"];
            m_flC4Blow = (int) ((Hashtable) NetVars["DT_PlantedC4"])["m_flC4Blow"];
            m_flTimerLength = (int) ((Hashtable) NetVars["DT_PlantedC4"])["m_flTimerLength"];
            m_flDefuseLength = (int) ((Hashtable) NetVars["DT_PlantedC4"])["m_flDefuseLength"];
            m_flDefuseCountDown = (int) ((Hashtable) NetVars["DT_PlantedC4"])["m_flDefuseCountDown"];
            cs_gamerules_data = (int) ((Hashtable) NetVars["DT_CSGameRulesProxy"])["cs_gamerules_data"];
            m_SurvivalRules = (int) ((Hashtable) NetVars["DT_CSGameRulesProxy"])["m_SurvivalRules"];
            m_SurvivalGameRuleDecisionTypes =
                (int) ((Hashtable) NetVars["DT_CSGameRulesProxy"])["m_SurvivalGameRuleDecisionTypes"];
            m_bIsValveDS = (int) ((Hashtable) NetVars["DT_CSGameRulesProxy"])["m_bIsValveDS"];
            m_bFreezePeriod = (int) ((Hashtable) NetVars["DT_CSGameRulesProxy"])["m_bFreezePeriod"];
            m_bBombPlanted = (int) ((Hashtable) NetVars["DT_CSGameRulesProxy"])["m_bBombPlanted"];
            m_bIsQueuedMatchmaking = (int) ((Hashtable) NetVars["DT_CSGameRulesProxy"])["m_bIsQueuedMatchmaking"];
            m_flSimulationTime = (int) ((Hashtable) NetVars["DT_BaseEntity"])["m_flSimulationTime"];
            m_flLowerBodyYawTarget = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_flLowerBodyYawTarget"];
            m_angEyeAnglesX = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_angEyeAngles[0]"];
            m_angEyeAnglesY = (int) ((Hashtable) NetVars["DT_CSPlayer"])["m_angEyeAngles[1]"];
            m_flNextAttack = (int) ((Hashtable) NetVars["DT_BaseCombatCharacter"])["m_flNextAttack"];
            m_iMostRecentModelBoneCounter = (int) ((Hashtable) NetVars["DT_CSRagdoll"])["m_nForceBone"] + 4;
            m_flLastBoneSetupTime = (int) ((Hashtable) NetVars["DT_BaseAnimating"])["m_nSequence"] + 104;
            m_bStartedArming = (int) ((Hashtable) NetVars["DT_WeaponC4"])["m_bStartedArming"];
            m_bUseCustomBloomScale = (int) ((Hashtable) NetVars["DT_EnvTonemapController"])["m_bUseCustomBloomScale"];
            m_bUseCustomAutoExposureMin =
                (int) ((Hashtable) NetVars["DT_EnvTonemapController"])["m_bUseCustomAutoExposureMin"];
            m_bUseCustomAutoExposureMax =
                (int) ((Hashtable) NetVars["DT_EnvTonemapController"])["m_bUseCustomAutoExposureMax"];
            m_flCustomBloomScale = (int) ((Hashtable) NetVars["DT_EnvTonemapController"])["m_flCustomBloomScale"];
            m_flCustomAutoExposureMin =
                (int) ((Hashtable) NetVars["DT_EnvTonemapController"])["m_flCustomAutoExposureMin"];
            m_flCustomAutoExposureMax =
                (int) ((Hashtable) NetVars["DT_EnvTonemapController"])["m_flCustomAutoExposureMax"];
        }
    }
}