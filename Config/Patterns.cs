namespace Simple_GTAV_External_Trainer.Config
{
    class Patterns
    {
        #region Global Patterns
        private byte[] _globalptr = new byte[] { 0x4C, 0x8D, 0x05, 0x0, 0x0, 0x0, 0x0, 0x4D, 0x8B, 0x08, 0x4D, 0x85, 0xC9, 0x74, 0x11 };
        private string _globalptr_mask = "xxx????xxxxxxxx";

        private byte[] _worldptr = new byte[] { 0x48, 0x8B, 0x05, 0x0, 0x0, 0x0, 0x0, 0x45, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8B, 0x48, 0x08, 0x48, 0x85, 0xC9, 0x74, 0x07 };
        private string _worldptr_mask = "xxx????x????xxxxxxxxx";

        private byte[] _blipptr = new byte[] { 0x4C, 0x8D, 0x05, 0x0, 0x0, 0x0, 0x0, 0x0F, 0xB7, 0xC1 };
        private string _blipptr_mask = "xxx????xxx";

        private byte[] _replayinterfaceptr = new byte[] { 0x48, 0x8D, 0x0D, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8B, 0xD7, 0xE8, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8D, 0x0D, 0x0, 0x0, 0x0, 0x0, 0x8A, 0xD8, 0xE8 };
        private string _replayinterfaceptr_mask = "xxx????xxxx????xxx????xxx";

        private byte[] _localscriptsptr = new byte[] { 0x48, 0x8B, 0x05, 0x0, 0x0, 0x0, 0x0, 0x8B, 0xCF, 0x48, 0x8B, 0x0C, 0xC8, 0x39, 0x59, 0x68 };
        private string _localscriptsptr_mask = "xxx????xxxxxxxxx";

        private byte[] _playercountptr = new byte[] { 0x48, 0x8B, 0x0D, 0x0, 0x0, 0x0, 0x0, 0xE8, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8B, 0xC8, 0xE8, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8B, 0xCF };
        private string _playercountptr_mask = "xxx????x????xxxx????xxx";

        private byte[] _pickupdataptr = new byte[] { 0x48, 0x8B, 0x05, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8B, 0x1C, 0xF8, 0x8B };
        private string _pickupdataptr_mask = "xxx????xxxxx";

        private byte[] _weatheraddr = new byte[] { 0x48, 0x83, 0xEC, 0x0, 0x8B, 0x05, 0x0, 0x0, 0x0, 0x0, 0x8B, 0x3D, 0x0, 0x0, 0x0, 0x0, 0x49 };
        private string _weatheraddr_mask = "xxx?xx????xx????x";

        private byte[] _settingsptr = new byte[] { 0x44, 0x39, 0x05, 0x0, 0x0, 0x0, 0x0, 0x75, 0x0D };
        private string _settingsptr_mask = "xxx????xx";

        private byte[] _aimcpedptr = new byte[] { 0x48, 0x8B, 0x0D, 0x0, 0x0, 0x0, 0x0, 0x48, 0x85, 0xC9, 0x74, 0x0C, 0x48, 0x8D, 0x15, 0x0, 0x0, 0x0, 0x0, 0xE8, 0x0, 0x0, 0x0, 0x0, 0x48, 0x89, 0x1D };
        private string _aimcpedptr_mask = "xxx????xxxxxxxx????x????xxx";

        private byte[] _friendlistptr = new byte[] { 0x48, 0x8B, 0x0D, 0x0, 0x0, 0x0, 0x0, 0x8B, 0xC6, 0x48, 0x8D, 0x5C, 0x24, 0x70 };
        private string _friendlistptr_mask = "xxx????xxxxxxx";

        public byte[] GlobalPTR
        {
            get
            {
                return _globalptr;
            }
        }
        public string GlobalPTR_Mask
        {
            get
            {
                return _globalptr_mask;
            }
        }

        public byte[] WorldPTR
        {
            get
            {
                return _worldptr;
            }
        }

        public string WorldPTR_Mask
        {
            get
            {
                return _worldptr_mask;
            }
        }

        public byte[] BlipPTR
        {
            get
            {
                return _blipptr;
            }
        }

        public string BlipPTR_Mask
        {
            get
            {
                return _blipptr_mask;
            }
        }

        public byte[] ReplayInterfacePTR
        {
            get
            {
                return _replayinterfaceptr;
            }
        }

        public string ReplayInterfacePTR_Mask
        {
            get
            {
                return _replayinterfaceptr_mask;
            }
        }

        public byte[] LocalScriptsPTR
        {
            get
            {
                return _localscriptsptr;
            }
        }

        public string LocalScriptsPTR_Mask
        {
            get
            {
                return _localscriptsptr_mask;
            }
        }

        public byte[] PlayerCountPTR
        {
            get
            {
                return _playercountptr;
            }
        }

        public string PlayerCountPTR_Mask
        {
            get
            {
                return _playercountptr_mask;
            }
        }

        public byte[] PickupDataPTR
        {
            get
            {
                return _pickupdataptr;
            }
        }

        public string PickupDataPTR_Mask
        {
            get
            {
                return _pickupdataptr_mask;
            }
        }

        public byte[] WeatherADDR
        {
            get
            {
                return _weatheraddr;
            }
        }

        public string WeatherADDR_Mask
        {
            get
            {
                return _weatheraddr_mask;
            }
        }

        public byte[] SettingsPTR
        {
            get
            {
                return _settingsptr;
            }
        }

        public string SettingsPTR_Mask
        {
            get
            {
                return _settingsptr_mask;
            }
        }

        public byte[] AimCPedPTR
        {
            get
            {
                return _aimcpedptr;
            }
        }

        public string AimCPedPTR_Mask
        {
            get
            {
                return _aimcpedptr_mask;
            }
        }

        public byte[] FriendlistPTR
        {
            get
            {
                return _friendlistptr;
            }
        }

        public string FriendlistPTR_Mask
        {
            get
            {
                return _friendlistptr_mask;
            }
        }
        #endregion
    }
}
