namespace GTAVCSMM.Settings
{
    class TSettings
    {
        #region Globals
        private string _gameName = "GTA5";
        private int _gameProcess = 0;
        private long _globalptr;
        private long _worldptr;
        private long _blipptr;
        private long _replayinterfaceptr;
        private long _localscriptsptr;
        private long _playercountptr;
        private long _pickupdataptr;
        private long _weatheraddr;
        private long _settingsptr;
        private long _aimcpedptr;
        private long _friendlistptr;

        public string gameName
        {
            get
            {
                return _gameName;
            }
        }

        public int gameProcess
        {
            get
            {
                return _gameProcess;
            }
            set
            {
                _gameProcess = value;
            }
        }

        public long GlobalPTR
        {
            get
            {
                return _globalptr;
            }
            set
            {
                _globalptr = value;
            }
        }

        public long WorldPTR
        {
            get
            {
                return _worldptr;
            }
            set
            {
                _worldptr = value;
            }
        }

        public long BlipPTR
        {
            get
            {
                return _blipptr;
            }
            set
            {
                _blipptr = value;
            }
        }

        public long ReplayInterfacePTR
        {
            get
            {
                return _replayinterfaceptr;
            }
            set
            {
                _replayinterfaceptr = value;
            }
        }

        public long LocalScriptsPTR
        {
            get
            {
                return _localscriptsptr;
            }
            set
            {
                _localscriptsptr = value;
            }
        }

        public long PlayerCountPTR
        {
            get
            {
                return _playercountptr;
            }
            set
            {
                _playercountptr = value;
            }
        }

        public long PickupDataPTR
        {
            get
            {
                return _pickupdataptr;
            }
            set
            {
                _pickupdataptr = value;
            }
        }

        public long WeatherADDR
        {
            get
            {
                return _weatheraddr;
            }
            set
            {
                _weatheraddr = value;
            }
        }

        public long SettingsPTR
        {
            get
            {
                return _settingsptr;
            }
            set
            {
                _settingsptr = value;
            }
        }

        public long AimCPedPTR
        {
            get
            {
                return _aimcpedptr;
            }
            set
            {
                _aimcpedptr = value;
            }
        }

        public long FriendlistPTR
        {
            get
            {
                return _friendlistptr;
            }
            set
            {
                _friendlistptr = value;
            }
        }
        #endregion

        #region Trainer settings
        private int _aConnect = 0;
        private int _aFKtoggle = 0;
        private int _aGodMode = 0;
        private int _aLogPlayers = 0;
        private int _aDarkMode = 0;
        private int _player_tracking = 0;
        //tbl_origPlayersList = {}
        private int _pictureGrabOFF = 0;
        //player_watch = {}
        //tbl_Players = {}
        //tbl_PlayersAll = {}
        //tbl_Tracking = {}
        private int _markMyRid = -1;
        private int _myCPed = -1;

        private bool _GlobalPTRFound = false;
        private bool _WorldPTRFound = false;
        private bool _BlipPTRFound = false;
        private bool _ReplayInterfacePTRFound = false;
        private bool _LocalScriptsPTRFound = false;
        private bool _PlayerCountPTRFound = false;
        private bool _PickupDataPTRFound = false;
        private bool _WeatherADDRFound = false;
        private bool _SettingsPTRFound = false;
        private bool _AimCPedPTRFound = false;
        private bool _FriendlistPTRFound = false;

        public int aConnect
        {
            get
            {
                return _aConnect;
            }
            set
            {
                _aConnect = value;
            }
        }

        public int aFKtoggle
        {
            get
            {
                return _aFKtoggle;
            }
            set
            {
                _aFKtoggle = value;
            }
        }

        public int aGodMode
        {
            get
            {
                return _aGodMode;
            }
            set
            {
                _aGodMode = value;
            }
        }

        public int aLogPlayers
        {
            get
            {
                return _aLogPlayers;
            }
            set
            {
                _aLogPlayers = value;
            }
        }

        public int aDarkMode
        {
            get
            {
                return _aDarkMode;
            }
            set
            {
                _aDarkMode = value;
            }
        }

        public int player_tracking
        {
            get
            {
                return _player_tracking;
            }
            set
            {
                _player_tracking = value;
            }
        }

        public int pictureGrabOFF
        {
            get
            {
                return _pictureGrabOFF;
            }
            set
            {
                _pictureGrabOFF = value;
            }
        }

        public int markMyRid
        {
            get
            {
                return _markMyRid;
            }
            set
            {
                _markMyRid = value;
            }
        }

        public int myCPed
        {
            get
            {
                return _myCPed;
            }
            set
            {
                _myCPed = value;
            }
        }

        public bool GlobalPTRFound
        {
            get
            {
                return _GlobalPTRFound;
            }
            set
            {
                _GlobalPTRFound = value;
            }
        }

        public bool WorldPTRFound
        {
            get
            {
                return _WorldPTRFound;
            }
            set
            {
                _WorldPTRFound = value;
            }
        }

        public bool BlipPTRFound
        {
            get
            {
                return _BlipPTRFound;
            }
            set
            {
                _BlipPTRFound = value;
            }
        }

        public bool ReplayInterfacePTRFound
        {
            get
            {
                return _ReplayInterfacePTRFound;
            }
            set
            {
                _ReplayInterfacePTRFound = value;
            }
        }

        public bool LocalScriptsPTRFound
        {
            get
            {
                return _LocalScriptsPTRFound;
            }
            set
            {
                _LocalScriptsPTRFound = value;
            }
        }

        public bool PlayerCountPTRFound
        {
            get
            {
                return _PlayerCountPTRFound;
            }
            set
            {
                _PlayerCountPTRFound = value;
            }
        }

        public bool PickupDataPTRFound
        {
            get
            {
                return _PickupDataPTRFound;
            }
            set
            {
                _PickupDataPTRFound = value;
            }
        }

        public bool WeatherADDRFound
        {
            get
            {
                return _WeatherADDRFound;
            }
            set
            {
                _WeatherADDRFound = value;
            }
        }

        public bool SettingsPTRFound
        {
            get
            {
                return _SettingsPTRFound;
            }
            set
            {
                _SettingsPTRFound = value;
            }
        }

        public bool AimCPedPTRFound
        {
            get
            {
                return _AimCPedPTRFound;
            }
            set
            {
                _AimCPedPTRFound = value;
            }
        }

        public bool FriendlistPTRFound
        {
            get
            {
                return _FriendlistPTRFound;
            }
            set
            {
                _FriendlistPTRFound = value;
            }
        }
        #endregion

        private bool _pgodm = false;
        private bool _pgodm_last_hit = false;
        private bool _pnwanted = false;
        private bool _pnwanted_last_hit = false;
        private bool _pnragdoll = false;
        private bool _pnragdoll_last_hit = false;
        private bool _puoffradar = false;
        private bool _puoffradar_last_hit = false;
        private bool _psbelt = false;
        private bool _psbelt_last_hit = false;
        private bool _psjump = false;
        private bool _psjump_last_hit = false;
        private bool _psexammo = false;
        private bool _psexammo_last_hit = false;
        private bool _pdiscol = false;
        private bool _pdiscol_last_hit = false;
        private bool _vgodm = false;
        private bool _vgodm_last_hit = false;

        public bool pgodm
        {
            get
            {
                return _pgodm;
            }
            set
            {
                _pgodm = value;
            }
        }
        public bool pgodm_last_hit
        {
            get
            {
                return _pgodm_last_hit;
            }
            set
            {
                _pgodm_last_hit = value;
            }
        }
        public bool pnwanted
        {
            get
            {
                return _pnwanted;
            }
            set
            {
                _pnwanted = value;
            }
        }
        public bool pnwanted_last_hit
        {
            get
            {
                return _pnwanted_last_hit;
            }
            set
            {
                _pnwanted_last_hit = value;
            }
        }
        public bool pnragdoll
        {
            get
            {
                return _pnragdoll;
            }
            set
            {
                _pnragdoll = value;
            }
        }
        public bool pnragdoll_last_hit
        {
            get
            {
                return _pnragdoll_last_hit;
            }
            set
            {
                _pnragdoll_last_hit = value;
            }
        }
        public bool puoffradar
        {
            get
            {
                return _puoffradar;
            }
            set
            {
                _puoffradar = value;
            }
        }
        public bool puoffradar_last_hit
        {
            get
            {
                return _puoffradar_last_hit;
            }
            set
            {
                _puoffradar_last_hit = value;
            }
        }
        public bool psbelt
        {
            get
            {
                return _psbelt;
            }
            set
            {
                _psbelt = value;
            }
        }
        public bool psbelt_last_hit
        {
            get
            {
                return _psbelt_last_hit;
            }
            set
            {
                _psbelt_last_hit = value;
            }
        }
        public bool psjump
        {
            get
            {
                return _psjump;
            }
            set
            {
                _psjump = value;
            }
        }
        public bool psjump_last_hit
        {
            get
            {
                return _psjump_last_hit;
            }
            set
            {
                _psjump_last_hit = value;
            }
        }
        public bool psexammo
        {
            get
            {
                return _psexammo;
            }
            set
            {
                _psexammo = value;
            }
        }
        public bool psexammo_last_hit
        {
            get
            {
                return _psexammo_last_hit;
            }
            set
            {
                _psexammo_last_hit = value;
            }
        }
        public bool pdiscol
        {
            get
            {
                return _pdiscol;
            }
            set
            {
                _pdiscol = value;
            }
        }
        public bool pdiscol_last_hit
        {
            get
            {
                return _pdiscol_last_hit;
            }
            set
            {
                _pdiscol_last_hit = value;
            }
        }
        public bool vgodm
        {
            get
            {
                return _vgodm;
            }
            set
            {
                _vgodm = value;
            }
        }
        public bool vgodm_last_hit
        {
            get
            {
                return _vgodm_last_hit;
            }
            set
            {
                _vgodm_last_hit = value;
            }
        }
    }
}
