namespace GTAVCSMM.Config
{
    class Offsets
    {
        #region World Offsets
        // World Offsets
        private int _ReadValuePointer = 3;
        private int _ReadValuePointer2 = 6;
        private int _MovePointer = 7;
        private int _MovePointer2 = 10;
        private int _MovePointer3 = 0x89;

        public int ReadValuePointer
        {
            get
            {
                return _ReadValuePointer;
            }
        }

        public int ReadValuePointer2
        {
            get
            {
                return _ReadValuePointer2;
            }
        }

        public int MovePointer
        {
            get
            {
                return _MovePointer;
            }
        }

        public int MovePointer2
        {
            get
            {
                return _MovePointer2;
            }
        }

        public int MovePointer3
        {
            get
            {
                return _MovePointer3;
            }
        }
        #endregion

        #region CNetworkPlayerMgr
        // CNetworkPlayerMgr
        private int _pCNetPlayerInfo = 0xA0;
        private int _pCNetPed = 0x1E8;
        private int _oNumPlayers = 0x180;
        private int _oRid = 0x090;

        public int pCNetPlayerInfo
        {
            get
            {
                return _pCNetPlayerInfo;
            }
        }

        public int pCNetPed
        {
            get
            {
                return _pCNetPed;
            }
        }

        public int oNumPlayers
        {
            get
            {
                return _oNumPlayers;
            }
        }

        public int oRid
        {
            get
            {
                return _oRid;
            }
        }
        #endregion

        #region AutoConnect
        // Landing Page Offsets for AutoConnect function / story mode.
        private int _oStartupFlow = 0x2B0;
        private int _oGTAOnline = 0xF0;

        public int oStartupFlow
        {
            get
            {
                return _oStartupFlow;
            }
        }

        public int oGTAOnline
        {
            get
            {
                return _oGTAOnline;
            }
        }
        #endregion

        #region CPedFactory
        // CPedFactory (WorldPTR)
        private int _pCPed = 0x08;

        public int pCPed
        {
            get
            {
                return _pCPed;
            }
        }
        #endregion

        #region CPed Offsets
        // CPed Offsets
        private int _oEntityType = 0x2B; // int 156:Player 152:Other
        private int _pCNavigation = 0x30;
        private int _oVisualX = 0x90;
        private int _oVisualY = 0x94;
        private int _oVisualZ = 0x98; // float, vector3
        private int _oGod = 0x189; // int8 0:false 1:true
        private int _oHostility = 0x18C;
        private int _oHealth = 0x280; // float
        private int _oHealthMax = 0x2A0;
        private int _pAttackers = 0x2A8;
        private int _pCVehicle = 0xD30;
        private int _oRagdoll = 0x10B8; // int 32:false 1/0:true
        private int _pCPlayerInfo = 0x10C8;
        private int _pCPedWeaponManager = 0x10D8;
        private int _oSeatbelt = 0x145C; // byte 55:false 56:true
        private int _oInVehicle = 0x1618; // int 16:false 0:true (perhaps 0xE52)
        private int _pedArmor = 0x1530; // not working what

        public int oEntityType
        {
            get
            {
                return _oEntityType;
            }
        }

        public int pCNavigation
        {
            get
            {
                return _pCNavigation;
            }
        }

        public int oVisualX
        {
            get
            {
                return _oVisualX;
            }
        }

        public int oVisualY
        {
            get
            {
                return _oVisualY;
            }
        }

        public int oVisualZ
        {
            get
            {
                return _oVisualZ;
            }
        }

        public int oGod
        {
            get
            {
                return _oGod;
            }
        }

        public int oHostility
        {
            get
            {
                return _oHostility;
            }
        }

        public int oHealth
        {
            get
            {
                return _oHealth;
            }
        }

        public int oHealthMax
        {
            get
            {
                return _oHealthMax;
            }
        }

        public int pAttackers
        {
            get
            {
                return _pAttackers;
            }
        }

        public int pCVehicle
        {
            get
            {
                return _pCVehicle;
            }
        }

        public int oRagdoll
        {
            get
            {
                return _oRagdoll;
            }
        }

        public int pCPlayerInfo
        {
            get
            {
                return _pCPlayerInfo;
            }
        }

        public int pCPedWeaponManager
        {
            get
            {
                return _pCPedWeaponManager;
            }
        }

        public int oSeatbelt
        {
            get
            {
                return _oSeatbelt;
            }
        }

        public int oInVehicle
        {
            get
            {
                return _oInVehicle;
            }
        }

        public int pedArmor
        {
            get
            {
                return _pedArmor;
            }
        }
        #endregion

        #region CNavigation Offsets
        // CNavigation Offsets
        private int _oHeading = 0x20; // float
        private int _oHeading2 = 0x24;
        private int _oRotation = 0x30;
        private int _oRotation2 = 0x34;
        private int _oRotation3 = 0x38; // float, vector3
        private int _oPositionX = 0x50;
        private int _oPositionY = 0x54;
        private int _oPositionZ = 0x58;

        public int oHeading
        {
            get
            {
                return _oHeading;
            }
        }

        public int oHeading2
        {
            get
            {
                return _oHeading2;
            }
        }

        public int oRotation
        {
            get
            {
                return _oRotation;
            }
        }

        public int oRotation2
        {
            get
            {
                return _oRotation2;
            }
        }

        public int oRotation3
        {
            get
            {
                return _oRotation3;
            }
        }

        public int oPositionX
        {
            get
            {
                return _oPositionX;
            }
        }

        public int oPositionY
        {
            get
            {
                return _oPositionY;
            }
        }

        public int oPositionZ
        {
            get
            {
                return _oPositionZ;
            }
        }
        #endregion

        #region CPlayerInfo Offsets
        // CPlayerInfo Offsets
        private int _oName = 0xA4; // string[20]
        private int _oSwimSpeed = 0x170; // float
        private int _oFrameFlags = 0x219;
        private int _oWalkSpeed = 0x18C;
        private int _oWanted = 0x888; // int8
        private int _oRunSpeed = 0xCF0; // float
        private int _oStamina = 0xCF4;
        private int _oStaminaRegen = 0xCF8;

        public int oName
        {
            get
            {
                return _oName;
            }
        }

        public int oSwimSpeed
        {
            get
            {
                return _oSwimSpeed;
            }
        }

        public int oFrameFlags
        {
            get
            {
                return _oFrameFlags;
            }
        }

        public int oWalkSpeed
        {
            get
            {
                return _oWalkSpeed;
            }
        }

        public int oWanted
        {
            get
            {
                return _oWanted;
            }
        }

        public int oRunSpeed
        {
            get
            {
                return _oRunSpeed;
            }
        }

        public int oStamina
        {
            get
            {
                return _oStamina;
            }
        }

        public int oStaminaRegen
        {
            get
            {
                return _oStaminaRegen;
            }
        }
        #endregion

        #region CPedWeaponManager Offsets
        // CPedWeaponManager Offsets
        private int _pCWeaponInfo = 0x20;
        private int _oImpactType = 0x20; // int 3:bullet 5:explosion
        private int _oImpactExplosion = 0x24; // int32
        private int _oImpactType2 = 0x54; // int
        private int _pCAmmoInfo = 0x60;
        private int _pCVehicleWeapon = 0x70;
        private int _oSpread = 0x7C; // float
        private int _oDamage = 0xB0;
        private int _oForce = 0xD8;
        private int _oForcePed = 0xDC;
        private int _oForceVehicle = 0xE0;
        private int _oForceFlying = 0xE4;
        private int _oPenetration = 0x110;
        private int _oMuzzleVelocity = 0x11C;
        private int _oBulletBatch = 0x124;
        private int _oReloadVehicleMult = 0x130;
        private int _oReloadMult = 0x134;
        private int _oShotTime = 0x13C;
        private int _oLockRange = 0x288;
        private int _oRange = 0x28C;
        private int _pCAmmoWrap = 0x8;
        private int _oMaxAmmo = 0x28;
        private int _pCAmmo = 0x0;
        private int _oCurrentAmmo = 0x18;
        private int _pCWeaponInventory = 0x10D0;
        private int _oAmmoModifier = 0x78;
        public int pCWeaponInfo
        {
            get
            {
                return _pCWeaponInfo;
            }
        }
        public int oImpactType
        {
            get
            {
                return _oImpactType;
            }
        }
        public int oImpactExplosion
        {
            get
            {
                return _oImpactExplosion;
            }
        }
        public int oImpactType2
        {
            get
            {
                return _oImpactType2;
            }
        }
        public int pCAmmoInfo
        {
            get
            {
                return _pCAmmoInfo;
            }
        }
        public int pCVehicleWeapon
        {
            get
            {
                return _pCVehicleWeapon;
            }
        }
        public int oSpread
        {
            get
            {
                return _oSpread;
            }
        }
        public int oDamage
        {
            get
            {
                return _oDamage;
            }
        }
        public int oForce
        {
            get
            {
                return _oForce;
            }
        }
        public int oForcePed
        {
            get
            {
                return _oForcePed;
            }
        }
        public int oForceVehicle
        {
            get
            {
                return _oForceVehicle;
            }
        }
        public int oForceFlying
        {
            get
            {
                return _oForceFlying;
            }
        }
        public int oPenetration
        {
            get
            {
                return _oPenetration;
            }
        }
        public int oMuzzleVelocity
        {
            get
            {
                return _oMuzzleVelocity;
            }
        }
        public int oBulletBatch
        {
            get
            {
                return _oBulletBatch;
            }
        }
        public int oReloadVehicleMult
        {
            get
            {
                return _oReloadVehicleMult;
            }
        }
        public int oReloadMult
        {
            get
            {
                return _oReloadMult;
            }
        }
        public int oShotTime
        {
            get
            {
                return _oShotTime;
            }
        }
        public int oLockRange
        {
            get
            {
                return _oLockRange;
            }
        }
        public int oRange
        {
            get
            {
                return _oRange;
            }
        }
        public int pCAmmoWrap
        {
            get
            {
                return _pCAmmoWrap;
            }
        }
        public int oMaxAmmo
        {
            get
            {
                return _oMaxAmmo;
            }
        }
        public int pCAmmo
        {
            get
            {
                return _pCAmmo;
            }
        }
        public int oCurrentAmmo
        {
            get
            {
                return _oCurrentAmmo;
            }
        }
        public int pCWeaponInventory
        {
            get
            {
                return _pCWeaponInventory;
            }
        }
        public int oAmmoModifier
        {
            get
            {
                return _oAmmoModifier;
            }
        }
        #endregion

        #region CVehicle Offsets       
        // CVehicle Offsets
        private int _pCModelInfo = 0x20;
        private int _oVInvisibility = 0x2C; // int
        private int _pCVehicleDrawHandler = 0x48;
        private int _oVPositionX = 0x90;
        private int _oVPositionY = 0x94;
        private int _oVPositionZ = 0x98; // float, vector3
        private int _oVState = 0xD8; // int 0:Player 1:NPC 2:Unused 3:Destroyed
        private int _oVBurnt = 0x18A; // int 64:off 72:on
        private int _oVHealth = 0x280; // float
        private int _oVHealthMax = 0x2A0;
        private int _oVBoostSpeed = 0x120;
        private int _oVBoost = 0x320;
        private int _oVBoostRecharge = 0x324;
        private int _oVHealth2 = 0x840;
        private int _oVHealth3 = 0x844; // used
        private int _oVEngineHealth = 0x908; // used
        private int _pCHandlingData = 0x938;
        private int _oVDirt = 0x9F8; // float
        private int _oBulletproofTires = 0x943; // int
        private int _oLightMult = 0xA14; // float
        private int _oVGravity = 0xC5C; // float
        private int _oCurPassenger = 0xC62;
        private int _oMk2Missiles = 0x12A4; // int
        private int _oAircraftBomb = 0x12B4;
        private int _oCountermeasures = 0x12B8;
        public int pCModelInfo
        {
            get
            {
                return _pCModelInfo;
            }
        }
        public int oVInvisibility
        {
            get
            {
                return _oVInvisibility;
            }
        }
        public int pCVehicleDrawHandler
        {
            get
            {
                return _pCVehicleDrawHandler;
            }
        }
        public int oVPositionX
        {
            get
            {
                return _oVPositionX;
            }
        }
        public int oVPositionY
        {
            get
            {
                return _oVPositionY;
            }
        }
        public int oVPositionZ
        {
            get
            {
                return _oVPositionZ;
            }
        }
        public int oVState
        {
            get
            {
                return _oVState;
            }
        }
        public int oVBurnt
        {
            get
            {
                return _oVBurnt;
            }
        }
        public int oVHealth
        {
            get
            {
                return _oVHealth;
            }
        }
        public int oVHealthMax
        {
            get
            {
                return _oVHealthMax;
            }
        }
        public int oVBoostSpeed
        {
            get
            {
                return _oVBoostSpeed;
            }
        }
        public int oVBoost
        {
            get
            {
                return _oVBoost;
            }
        }
        public int oVBoostRecharge
        {
            get
            {
                return _oVBoostRecharge;
            }
        }
        public int oVHealth2
        {
            get
            {
                return _oVHealth2;
            }
        }
        public int oVHealth3
        {
            get
            {
                return _oVHealth3;
            }
        }
        public int oVEngineHealth
        {
            get
            {
                return _oVEngineHealth;
            }
        }
        public int pCHandlingData
        {
            get
            {
                return _pCHandlingData;
            }
        }
        public int oVDirt
        {
            get
            {
                return _oVDirt;
            }
        }
        public int oBulletproofTires
        {
            get
            {
                return _oBulletproofTires;
            }
        }
        public int oLightMult
        {
            get
            {
                return _oLightMult;
            }
        }
        public int oVGravity
        {
            get
            {
                return _oVGravity;
            }
        }
        public int oCurPassenger
        {
            get
            {
                return _oCurPassenger;
            }
        }
        public int oMk2Missiles
        {
            get
            {
                return _oMk2Missiles;
            }
        }
        public int oAircraftBomb
        {
            get
            {
                return _oAircraftBomb;
            }
        }
        public int oCountermeasures
        {
            get
            {
                return _oCountermeasures;
            }
        }
        #endregion

        #region  CHandlingData Offsets
        // CHandlingData Offsets
        private int _oMass = 0xC; // float
        private int _oBouyancy = 0x40; // float
        private int _oAcceleration = 0x4C;
        private int _oDriveInertia = 0x54;
        private int _oInitialDriveForce = 0x60;
        private int _oBrakeForce = 0x6C;
        private int _oHandbrakeForce = 0x7C;
        private int _oTractionCurveMax = 0x88;
        private int _oTractionCurveMin = 0x90;
        private int _oCollisionMult = 0xF0;
        private int _oWeaponMult = 0xF4;
        private int _oDeformationMult = 0xF8;
        private int _oEngineMult = 0xFC;
        private int _oThrust = 0x338;
        public int oMass
        {
            get
            {
                return _oMass;
            }
        }
        public int oBouyancy
        {
            get
            {
                return _oBouyancy;
            }
        }
        public int oAcceleration
        {
            get
            {
                return _oAcceleration;
            }
        }
        public int oDriveInertia
        {
            get
            {
                return _oDriveInertia;
            }
        }
        public int oInitialDriveForce
        {
            get
            {
                return _oInitialDriveForce;
            }
        }
        public int oBrakeForce
        {
            get
            {
                return _oBrakeForce;
            }
        }
        public int oHandbrakeForce
        {
            get
            {
                return _oHandbrakeForce;
            }
        }
        public int oTractionCurveMax
        {
            get
            {
                return _oTractionCurveMax;
            }
        }
        public int oTractionCurveMin
        {
            get
            {
                return _oTractionCurveMin;
            }
        }
        public int oCollisionMult
        {
            get
            {
                return _oCollisionMult;
            }
        }
        public int oWeaponMult
        {
            get
            {
                return _oWeaponMult;
            }
        }
        public int oDeformationMult
        {
            get
            {
                return _oDeformationMult;
            }
        }
        public int oEngineMult
        {
            get
            {
                return _oEngineMult;
            }
        }
        public int oThrust
        {
            get
            {
                return _oThrust;
            }
        }
        #endregion

        #region CVehicleDrawHandler Offsets
        // CVehicleDrawHandler Offsets
        private int _pCVehicleVisual = 0x20;
        private int _oNeonR = 0x3A2; // int
        private int _oNeonG = 0x3A1;
        private int _oNeonB = 0x3A0;
        private int _oNeonLeft = 0x402;
        private int _oNeonRight = 0x403;
        private int _oNeonFront = 0x404;
        private int _oNeonBack = 0x405;
        private int _oEMS = 0x3D6;
        private int _oBrakes = 0x3D7;
        private int _oTransmission = 0x3D8;
        private int _oHorn = 0x3D9;
        private int _oSuspension = 0x3DA;
        private int _oArmor = 0x3DB;
        private int _oTurbo = 0x3DD;
        private int _oXenonLight = 0x3E1;
        private int _oSmokeR = 0x3FC;
        private int _oSmokeG = 0x3FD;
        private int _oSmokeB = 0x3FE;
        private int _oWindows = 0x3FF;
        private int _oColorLight = 0x406;
        public int pCVehicleVisual
        {
            get
            {
                return _pCVehicleVisual;
            }
        }
        public int oNeonR
        {
            get
            {
                return _oNeonR;
            }
        }
        public int oNeonG
        {
            get
            {
                return _oNeonG;
            }
        }
        public int oNeonB
        {
            get
            {
                return _oNeonB;
            }
        }
        public int oNeonLeft
        {
            get
            {
                return _oNeonLeft;
            }
        }
        public int oNeonRight
        {
            get
            {
                return _oNeonRight;
            }
        }
        public int oNeonFront
        {
            get
            {
                return _oNeonFront;
            }
        }
        public int oNeonBack
        {
            get
            {
                return _oNeonBack;
            }
        }
        public int oEMS
        {
            get
            {
                return _oEMS;
            }
        }
        public int oBrakes
        {
            get
            {
                return _oBrakes;
            }
        }
        public int oTransmission
        {
            get
            {
                return _oTransmission;
            }
        }
        public int oHorn
        {
            get
            {
                return _oHorn;
            }
        }
        public int oSuspension
        {
            get
            {
                return _oSuspension;
            }
        }
        public int oArmor
        {
            get
            {
                return _oArmor;
            }
        }
        public int oTurbo
        {
            get
            {
                return _oTurbo;
            }
        }
        public int oXenonLight
        {
            get
            {
                return _oXenonLight;
            }
        }
        public int oSmokeR
        {
            get
            {
                return _oSmokeR;
            }
        }
        public int oSmokeG
        {
            get
            {
                return _oSmokeG;
            }
        }
        public int oSmokeB
        {
            get
            {
                return _oSmokeB;
            }
        }
        public int oWindows
        {
            get
            {
                return _oWindows;
            }
        }
        public int oColorLight
        {
            get
            {
                return _oColorLight;
            }
        }
        #endregion

        #region CVehicleVisual
        // CVehicleVisual
        private int _oPrimaryR = 0xA6; // int
        private int _oPrimaryG = 0xA5;
        private int _oPrimaryB = 0xA4;
        private int _oSecondaryR = 0xAA;
        private int _oSecondaryG = 0xA9;
        private int _oSecondaryB = 0xA8;
        private int _oLicensePlate = 0x130; // dword / byte array[8]
        public int oPrimaryR
        {
            get
            {
                return _oPrimaryR;
            }
        }
        public int oPrimaryG
        {
            get
            {
                return _oPrimaryG;
            }
        }
        public int oPrimaryB
        {
            get
            {
                return _oPrimaryB;
            }
        }
        public int oSecondaryR
        {
            get
            {
                return _oSecondaryR;
            }
        }
        public int oSecondaryG
        {
            get
            {
                return _oSecondaryG;
            }
        }
        public int oSecondaryB
        {
            get
            {
                return _oSecondaryB;
            }
        }
        public int oLicensePlate
        {
            get
            {
                return _oLicensePlate;
            }
        }
        #endregion

        #region CModelInfo
        // CModelInfo
        private int _oModelHash = 0x18; // int
        private int _oCamDist = 0x38; // float
        private int _oVName = 0x298; // string[10]
        private int _oVMaker = 0x2A4; // string[10]
        private int _oVExtras = 0x58B; // short
        private int _oVParachute = 0x58C;
        public int oModelHash
        {
            get
            {
                return _oModelHash;
            }
        }
        public int oCamDist
        {
            get
            {
                return _oCamDist;
            }
        }
        public int oVName
        {
            get
            {
                return _oVName;
            }
        }
        public int oVMaker
        {
            get
            {
                return _oVMaker;
            }
        }
        public int oVExtras
        {
            get
            {
                return _oVExtras;
            }
        }
        public int oVParachute
        {
            get
            {
                return _oVParachute;
            }
        }
        #endregion

        #region CReplayInterface Offsets
        // CReplayInterface Offsets and more below
        private int _pCVehicleInterface = 0x10;
        private int _pCPedInterface = 0x18;
        private int _pVehList = 0x180;
        private int _oVehNum = 0x190;
        private int _pPedList = 0x100;
        private int _oPedNum = 0x110;

        public int pCVehicleInterface
        {
            get
            {
                return _pCVehicleInterface;
            }
        }

        public int pCPedInterface
        {
            get
            {
                return _pCPedInterface;
            }
        }
        public int pVehList
        {
            get
            {
                return _pVehList;
            }
        }
        public int oVehNum
        {
            get
            {
                return _oVehNum;
            }
        }
        public int pPedList
        {
            get
            {
                return _pPedList;
            }
        }
        public int oPedNum
        {
            get
            {
                return _oPedNum;
            }
        }
        #endregion

        #region CPickupData/CReplayInterface Offsets
        // CPickupData/CReplayInterface Offsets from DMKiller
        private int _pBST = 0x160;
        private int _pFixVeh = 0x228;
        private int _pPickupList = 0x100;
        private int _oPickupNum = 0x110;
        private int _pDroppedPickupData = 0x490;
        private int _pCPickupInterface = 0x20;
        public int pBST
        {
            get
            {
                return _pBST;
            }
        }
        public int pFixVeh
        {
            get
            {
                return _pFixVeh;
            }
        }
        public int pPickupList
        {
            get
            {
                return _pPickupList;
            }
        }
        public int oPickupNum
        {
            get
            {
                return _oPickupNum;
            }
        }
        public int pDroppedPickupData
        {
            get
            {
                return _pDroppedPickupData;
            }
        }
        public int pCPickupInterface
        {
            get
            {
                return _pCPickupInterface;
            }
        }
        #endregion

        #region Vehicle Menus Globals
        // Vehicle Menus Globals
        private int _oVMCreate = 2725260; // Create any vehicle.
        private int _oVMYCar = 2810287;  // Get my car.
        private int _oVGETIn = 2671444;  // Spawn into vehicle.
        private int _oVMSlots = 1585844;  // Get vehicle slots.
        public int oVMCreate
        {
            get
            {
                return _oVMCreate;
            }
        }
        public int oVMYCar
        {
            get
            {
                return _oVMYCar;
            }
        }
        public int oVGETIn
        {
            get
            {
                return _oVGETIn;
            }
        }
        public int oVMSlots
        {
            get
            {
                return _oVMSlots;
            }
        }
        #endregion

        #region Some Player / Network times associated Globals
        // Some Player / Network times associated Globals
        private int _oPlayerGA = 2441237;
        private int _oPlayerIDHelp = 2426865;
        private int _oNETTimeHelp = 2441237;
        public int oPlayerGA
        {
            get
            {
                return _oPlayerGA;
            }
        }
        public int oPlayerIDHelp
        {
            get
            {
                return _oPlayerIDHelp;
            }
        }
        public int oNETTimeHelp
        {
            get
            {
                return _oNETTimeHelp;
            }
        }
        #endregion
    }
}
