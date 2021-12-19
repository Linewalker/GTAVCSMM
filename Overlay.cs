using System.Runtime.InteropServices;
using GTAVCSMM.Helpers;
using GTAVCSMM.Config;
using GTAVCSMM.Settings;
using GTAVCSMM.Memory;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GTAVCSMM
{
    public partial class Overlay : Form
    {

        #region WINDOW SETUP

        public const string WINDOW_NAME = "Grand Theft Auto V";
        IntPtr handle = FindWindow(null, WINDOW_NAME);

        RECT rect;

        public struct RECT
        {
            public int left, top, right, bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT IpRect);

        Offsets offsets = new Offsets();
        Addresses addresses = new Addresses();
        Patterns pattern = new Patterns();
        TSettings settings = new TSettings();
        Mem Mem;
        Thread _getPointer;
        Thread _freezeGame;

        #endregion

        #region PROCESS INFO
        private bool bGodMode = false;
        private bool bgodState = false;
        private bool bNeverWanted = false;
        private bool bNoRagdoll = false;
        private bool bUndeadOffRadar = false;
        private bool bSeatBelt = false;
        private bool bSuperJump = false;
        private bool bDisableCollision = false;
        private bool bVehicleGodMode = false;

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int a, int b, int c, int d, int damnIwonderifpeopleactuallyreadsthis);
        #endregion

        #region METHODS
        public void pGODMODE()
        {
            if (bGodMode)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oGod }, 1);
                if (!settings.pgodm)
                {
                    Activate();
                }
                settings.pgodm = true;

                godModeToolStripMenuItem.Checked = true;
            }
            else
            {
                if (settings.pgodm)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oGod }, 0);
                    settings.pgodm = false;
                    Deactivate();
                    godModeToolStripMenuItem.Checked = false;
                }
            }
        }

        public void pNEVERWANTED()
        {
            if (bNeverWanted)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 0);
                if (!settings.pnwanted)
                {
                    Activate();
                }
                settings.pnwanted = true;

                neverWantedToolStripMenuItem.Checked = true;
            }
            else
            {
                if (settings.pnwanted)
                {
                    settings.pnwanted = false;
                    neverWantedToolStripMenuItem.Checked = false;
                    Deactivate();
                }
            }
        }

        public void pNORAGDOLL()
        {
            if (bNoRagdoll)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRagdoll }, 1);
                if (!settings.pnragdoll)
                {
                    Activate();
                }
                settings.pnragdoll = true;

                noRagdollToolStripMenuItem.Checked = true;
            }
            else
            {
                if (settings.pnragdoll)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRagdoll }, 32);
                    settings.pnragdoll = false;
                    noRagdollToolStripMenuItem.Checked = false;
                    Deactivate();
                }
            }
        }

        public void pUNDEADOFFRADAR()
        {
            if (bUndeadOffRadar)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oHealthMax }, 0);
                if (!settings.puoffradar)
                {
                    Activate();
                }
                settings.puoffradar = true;

                undeadOffToolStripMenuItem.Checked = true;
            }
            else
            {
                if (settings.puoffradar)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oHealthMax }, 328f);
                    settings.puoffradar = false;
                    undeadOffToolStripMenuItem.Checked = false;
                    Deactivate();
                }
            }
        }

        public void pSEATBELT()
        {
            if (bSeatBelt)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oSeatbelt }, -55);
                if (!settings.psbelt)
                {
                    Activate();
                }
                settings.psbelt = true;

                seatbeltToolStripMenuItem.Checked = true;
            }
            else
            {
                if (settings.psbelt)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oSeatbelt }, -56);
                    settings.psbelt = false;
                    seatbeltToolStripMenuItem.Checked = false;
                    Deactivate();
                }
            }
        }

        public void pSUPERJUMP()
        {
            if (bSuperJump)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oFrameFlags }, 64);
                if (!settings.psjump)
                {
                    Activate();
                }
                settings.psjump = true;

                superJumpToolStripMenuItem.Checked = true;
            }
            else
            {
                if (settings.psjump)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oFrameFlags }, 0);
                    settings.psjump = false;
                    superJumpToolStripMenuItem.Checked = false;
                    Deactivate();
                }
            }
        }
        public void pDISABLECOLLISION()
        {
            long paddr = Mem.ReadPointer(settings.WorldPTR, new int[] { offsets.pCPed, 0x30, 0x10, 0x20, 0x70, 0x0 });
            long paddr2 = Mem.GetPtrAddr(paddr + 0x2C, null);

            if (bDisableCollision)
            {
                Mem.writeFloat(paddr2, null, -1.0f);
                if (!settings.pdiscol)
                {
                    Activate();
                }
                settings.pdiscol = true;

                disableCollisionToolStripMenuItem.Checked = true;
            }
            else
            {
                if (settings.pdiscol)
                {
                    Mem.writeFloat(paddr2, null, 0.25f);
                    settings.pdiscol = false;
                    Deactivate();
                    disableCollisionToolStripMenuItem.Checked = false;
                }
            }
        }
        public void vGODMODE()
        {
            long paddr = Mem.ReadPointer(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle });
            if (paddr > 0)
            {
                long paddr2 = Mem.GetPtrAddr(paddr + offsets.oGod, null);
                if (bVehicleGodMode)
                {
                    Mem.writeInt(paddr2, null, 1);
                    if (!settings.vgodm)
                    {
                        Activate();
                    }
                    settings.vgodm = true;

                    godModeToolStripMenuItem1.Checked = true;
                }
                else
                {
                    if (settings.vgodm)
                    {
                        Mem.writeInt(paddr2, null, 0);
                        settings.vgodm = false;
                        Deactivate();
                        godModeToolStripMenuItem1.Checked = false;
                    }
                }
            }
        }
        async Task releaseMouse()
        {
            SendKeys.SendWait("^{ESC}");
            await Task.Delay(2000);
        }

        public void getPointer()
        {
            try
            {
                Mem = new Mem(settings.gameName);

                var processes = Process.GetProcessesByName(settings.gameName);
                foreach (var p in processes)
                {
                    if (p.Id > 0)
                    {
                        settings.gameProcess = p.Id;
                    }
                }

                // GlobalPTR
                long addr = Mem.FindPattern(pattern.GlobalPTR, pattern.GlobalPTR_Mask);
                settings.GlobalPTR = addr + Mem.ReadInt(addr + 3, null) + 7;

                // WorldPTR
                long addr2 = Mem.FindPattern(pattern.WorldPTR, pattern.WorldPTR_Mask);
                settings.WorldPTR = addr2 + Mem.ReadInt(addr2 + 3, null) + 7;

                // BlipPTR
                long addr3 = Mem.FindPattern(pattern.BlipPTR, pattern.BlipPTR_Mask);
                settings.BlipPTR = addr3 + Mem.ReadInt(addr3 + 3, null) + 7;

                // ReplayInterfacePTR
                long addr4 = Mem.FindPattern(pattern.ReplayInterfacePTR, pattern.ReplayInterfacePTR_Mask);
                settings.ReplayInterfacePTR = addr4 + Mem.ReadInt(addr4 + 3, null) + 7;

                // LocalScriptsPTR
                long addr5 = Mem.FindPattern(pattern.LocalScriptsPTR, pattern.LocalScriptsPTR_Mask);
                settings.LocalScriptsPTR = addr5 + Mem.ReadInt(addr5 + 3, null) + 7;

                // PlayerCountPTR
                long addr6 = Mem.FindPattern(pattern.PlayerCountPTR, pattern.PlayerCountPTR_Mask);
                settings.PlayerCountPTR = addr6 + Mem.ReadInt(addr6 + 3, null) + 7;

                // PickupDataPTR
                long addr7 = Mem.FindPattern(pattern.PickupDataPTR, pattern.PickupDataPTR_Mask);
                settings.PickupDataPTR = addr7 + Mem.ReadInt(addr7 + 3, null) + 7;

                // WeatherADDR
                long addr8 = Mem.FindPattern(pattern.WeatherADDR, pattern.WeatherADDR_Mask);
                settings.WeatherADDR = addr8 + Mem.ReadInt(addr8 + 6, null) + 10;

                // SettingsPTR
                long addr9 = Mem.FindPattern(pattern.SettingsPTR, pattern.SettingsPTR_Mask);
                settings.SettingsPTR = addr9 + Mem.ReadInt(addr9 + 3, null) - Convert.ToInt64("0x89", 16);

                // AimCPedPTR
                long addr10 = Mem.FindPattern(pattern.AimCPedPTR, pattern.AimCPedPTR_Mask);
                settings.AimCPedPTR = addr10 + Mem.ReadInt(addr10 + 3, null) + 7;

                // FriendlistPTR
                long addr11 = Mem.FindPattern(pattern.FriendlistPTR, pattern.FriendlistPTR_Mask);
                settings.FriendlistPTR = addr11 + Mem.ReadInt(addr11 + 3, null) + 7;
            }
            catch
            {
                MessageBox.Show("GTA is not Running!", "Serious Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Quit();
            }
            finally
            {
                this.Invoke(new Action(() =>
                {
                    toolStripMenuItem2.Enabled = true;
                    reInitToolStripMenuItem.Visible = true;
                    sessionToolStripMenuItem.Visible = true;
                    playerToolStripMenuItem.Visible = true;
                    vehicleToolStripMenuItem.Visible = true;
                    weaponToolStripMenuItem.Visible = true;
                    teleportToolStripMenuItem.Visible = true;
                    tunablesToolStripMenuItem.Visible = true;
                    reInitToolStripMenuItem.Enabled = true;
                    sessionToolStripMenuItem.Enabled = true;
                    playerToolStripMenuItem.Enabled = true;
                    vehicleToolStripMenuItem.Enabled = true;
                    weaponToolStripMenuItem.Enabled = true;
                    teleportToolStripMenuItem.Enabled = true;
                    tunablesToolStripMenuItem.Enabled = true;
                }));
                Console.WriteLine("Pointer initialized.");
            }
            _getPointer.Abort();
        }

        public void freezeGame()
        {
            Console.WriteLine("Freezing game");
            this.Invoke(new Action(() =>
            {
                emptySessionToolStripMenuItem.Enabled = false;
                toolStripMenuItem2.Enabled = false;
                reInitToolStripMenuItem.Enabled = false;
                sessionToolStripMenuItem.Enabled = false;
                playerToolStripMenuItem.Enabled = false;
                vehicleToolStripMenuItem.Enabled = false;
                weaponToolStripMenuItem.Enabled = false;
                teleportToolStripMenuItem.Enabled = false;
                tunablesToolStripMenuItem.Enabled = false;
            }));            
            Speeder.Suspend(settings.gameProcess);
            Thread.Sleep(10000);
            Speeder.Resume(settings.gameProcess);
            this.Invoke(new Action(() =>
            {
                emptySessionToolStripMenuItem.Enabled = true;
                toolStripMenuItem2.Enabled = true;
                reInitToolStripMenuItem.Enabled = true;
                sessionToolStripMenuItem.Enabled = true;
                playerToolStripMenuItem.Enabled = true;
                vehicleToolStripMenuItem.Enabled = true;
                weaponToolStripMenuItem.Enabled = true;
                teleportToolStripMenuItem.Enabled = true;
                tunablesToolStripMenuItem.Enabled = true;
            }));
            _freezeGame.Abort();
        }
        #endregion

        #region KEYBINDS
        public void KeyAssign()
        {
            KeysMgr keyMgr = new KeysMgr();
            keyMgr.AddKey(Keys.Insert);     // -
            keyMgr.AddKey(Keys.Delete);     // QUIT
            keyMgr.AddKey(Keys.F5);         // -
            keyMgr.AddKey(Keys.F6);         // God Mode
            keyMgr.AddKey(Keys.F7);         // Never Wanted
            keyMgr.AddKey(Keys.F8);         // Teleport WP
            keyMgr.KeyDownEvent += new KeysMgr.KeyHandler(KeyDownEvent);
        }

        private void KeyDownEvent(int Id, string Name)
        {
            switch ((Keys)Id)
            {
                case Keys.Delete:
                    Quit();
                    break;
                case Keys.F5:
                    Task.Factory.StartNew(releaseMouse);
                    break;
                case Keys.F6:
                    this.bGodMode = !this.bGodMode;
                    break;
                case Keys.F7:
                    this.bNeverWanted = !this.bNeverWanted;
                    break;
                case Keys.F8:
                    teleportWaypoint();
                    break;
            }
        }
        #endregion

        #region MAIN FORM

        public Overlay()
        {
            InitializeComponent();
        }

        private void Initialize(object sender, EventArgs e)
        {
            toolStripMenuItem2.Enabled = false;
            reInitToolStripMenuItem.Visible = false;
            sessionToolStripMenuItem.Visible = false;
            playerToolStripMenuItem.Visible = false;
            vehicleToolStripMenuItem.Visible = false;
            weaponToolStripMenuItem.Visible = false;
            teleportToolStripMenuItem.Visible = false;
            tunablesToolStripMenuItem.Visible = false;
            onlineServicesToolStripMenuItem.Visible = false;
            reInitToolStripMenuItem.Enabled = false;
            sessionToolStripMenuItem.Enabled = false;
            playerToolStripMenuItem.Enabled = false;
            vehicleToolStripMenuItem.Enabled = false;
            weaponToolStripMenuItem.Enabled = false;
            teleportToolStripMenuItem.Enabled = false;
            tunablesToolStripMenuItem.Enabled = false;
            onlineServicesToolStripMenuItem.Enabled = false;

            _getPointer = new Thread(getPointer) { IsBackground = true };
            _getPointer.Start();

            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            int InitialStyle = GetWindowLong(this.Handle, -10);
            SetWindowLong(this.Handle, -10, InitialStyle | 0x800000 | 0x20);
            GetWindowRect(handle, out rect);
            this.Top = rect.top + 20;
            this.Left = rect.left + 30;
            /*
            int InitialStyle = GetWindowLong(this.Handle, -10);
            SetWindowLong(this.Handle, -10, InitialStyle | 0x800000 | 0x20);
            GetWindowRect(handle, out rect);
            this.Size = new Size((rect.right-12) - rect.left, 24);
            this.Top = rect.top + 2;
            this.Left = rect.left + 3;
            */

            KeyAssign();
        }

        private void Close(object sender, FormClosingEventArgs e)
        {
            Quit();
        }

        private void Quit()
        {
            Environment.Exit(0);
        }

        #region TIMERS

        private void ProcessTimer_Tick(object sender, EventArgs e)
        {

        }

        private void MemoryTimer_Tick(object sender, EventArgs e)
        {
            pGODMODE();
            pNEVERWANTED();
            pNORAGDOLL();
            pUNDEADOFFRADAR();
            pSEATBELT();
            pDISABLECOLLISION();
            vGODMODE();
        }

        private void fastTimer_Tick(object sender, EventArgs e)
        {
            pSUPERJUMP();
        }

        #endregion

        #endregion

        //audio response to actions
        public void Activate()
        {
            Console.Beep(523, 75);
            Console.Beep(587, 75);
            Console.Beep(700, 75);
        }

        public void Deactivate()
        {
            Console.Beep(700, 75);
            Console.Beep(587, 75);
            Console.Beep(523, 75);
        }

        public void Toggle()
        {
            Console.Beep(523, 75);
            Console.Beep(523, 75);
            Console.Beep(523, 75);
        }

        public void LoadSession(int id)
        {
            if (id == -1)
            {
                _SG_Int(1574587 + 2, -1);
                _SG_Int(1574587, 1);
                Thread.Sleep(200);
                _SG_Int(1574587, 0);
            }
            else if (id == -2)
            {
                _SG_Int(1574587 + 2, 1);
                Thread.Sleep(200);
                _SG_Int(1574587, 0);
            }
            else
            {
                _SG_Int(1575004, id);
                _SG_Int(1574587, 1);
                Thread.Sleep(200);
                _SG_Int(1574587, 0);
            }
        }

        #region Teleport part

        private void teleportWaypoint()
        {
            Teleport(WaypointCoords);
        }

        private void teleportObjective()
        {
            Teleport(ObjectiveCoords);
        }

        private void getLuckyWheelPrice(int price)
        {
            Console.WriteLine("Get Lucky Wheel Price");
            long p = 0;
            long p2 = 0;
            int i;

            for (i = 1; i <= 52; i++)
            {
                string pPointer = "casino_lucky_wheel";
                int pSize = pPointer.Length;
                p = Mem.ReadPointer(settings.LocalScriptsPTR, new int[] { i * 0x08 });
                p2 = Mem.GetPtrAddr(p + 0xD0, null);
                string pS = Mem.ReadString(settings.LocalScriptsPTR, new int[] { i * 0x08, 0xD0 }, pSize);
                if (pS == pPointer)
                {
                    //Mem.Write(settings.LocalScriptsPTR, new int[] { i * 0x08, 0xD0, 0x150 }, price);
                    break;
                }
            }
            Console.WriteLine(p2.ToString("X8"));
        }

        public void setRPMultipler(float m)
        {
            _SG_Float(262145 + 1, m);
        }

        public void setREPMultipler(float m)
        {
            _SG_Float(262145 + 31118, m); // Street Race
            _SG_Float(262145 + 31119, m); // Pursuit Race
            _SG_Float(262145 + 31120, m); // Scramble
            _SG_Float(262145 + 31121, m); // Head 2 Head
        }

        private void Teleport(Location l)
        {
            if (Mem.ReadInt(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oInVehicle }) == 0)
            {
                CarX = l.x;
                CarY = l.y;
                CarZ = l.z;
            }
            else
            {
                PlayerX = l.x;
                PlayerY = l.y;
                PlayerZ = l.z;
            }
        }

        private Location WaypointCoords
        {
            get
            {
                for (int i = 2000; i > 1; i--)
                {
                    if (Mem.ReadInt(settings.BlipPTR + (i * 8), new int[] { 0x48 }) == 84 && Mem.ReadInt(settings.BlipPTR + (i * 8), new int[] { 0x40 }) == 8)
                    {
                        return new Location
                        {
                            x = Mem.ReadFloat(settings.BlipPTR + (i * 8), new int[] { 0x10 }),
                            y = Mem.ReadFloat(settings.BlipPTR + (i * 8), new int[] { 0x14 }),
                            z = -210F
                        };
                    }
                }
                return new Location { x = PlayerX, y = PlayerY, z = PlayerZ };
            }
        }

        private Location ObjectiveCoords
        {
            get
            {
                for (int i = 2000; i > 1; i--)
                {
                    int objDetect = Mem.ReadInt(settings.BlipPTR + (i * 8), new int[] { 0x48 });
                    if (Mem.ReadInt(settings.BlipPTR + (i * 8), new int[] { 0x40 }) == 1 && ((objDetect == 1) || (objDetect == 66) || (objDetect == 60)))
                    {
                        return new Location
                        {
                            x = Mem.ReadFloat(settings.BlipPTR + (i * 8), new int[] { 0x10 }),
                            y = Mem.ReadFloat(settings.BlipPTR + (i * 8), new int[] { 0x14 }),
                            z = Mem.ReadFloat(settings.BlipPTR + (i * 8), new int[] { 0x18 }) + 10F
                        };
                    }
                }
                return new Location { x = PlayerX, y = PlayerY, z = PlayerZ };
            }
        }

        public float PlayerX
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRotation, offsets.oPositionX }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRotation, offsets.oPositionX }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oVisualX }, value);
            }
        }
        public float PlayerY
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRotation, offsets.oPositionX }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRotation, offsets.oPositionY }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oVisualY }, value);
            }
        }
        public float PlayerZ
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRotation, offsets.oPositionX }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRotation, offsets.oPositionZ }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oVisualZ }, value);
            }
        }

        public float CarX
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionX }); }
            set {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionX }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oVPositionX }, value);
            }
        }
        public float CarY
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionY }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionY }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oVPositionY }, value);
            }
        }
        public float CarZ
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionZ }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionZ }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oVPositionZ }, value);
            }
        }
        #endregion

        #region Global Addresses function
        public long GA(int Index) {
            long p = settings.GlobalPTR + (8 * (Index >> 0x12 & 0x3F));
            long p_ga = Mem.ReadPointer(p, null);
            long p_ga_final = p_ga + (8 * (Index & 0x3FFFF));
            return p_ga_final;
        }
        public long _GG_Int(int Index)
        {
            return Mem.ReadInt(GA(Index), null);
        }
        public float _GG_Float(int Index)
        {
            return Mem.ReadFloat(GA(Index), null);
        }
        public string _GG_String(int Index, int size)
        {
            return Mem.ReadString(GA(Index), null, size);
        }
        public void _SG_Int(int Index, int value)
        {
            Mem.writeInt(GA(Index), null, value);
        }
        public void _SG_Float(int Index, float value)
        {
            Mem.writeFloat(GA(Index), null, value);
        }
        public void _SG_String(int Index, string value)
        {
            Mem.Write(GA(Index), null, value);
        }

        public void setStat(string stat, int value)
        {
            long oldhash = _GG_Int(1390343 + 4);
            long oldvalue = _GG_Int(939452 + 5526);
            _SG_Int(1390343 + 4, (int)JOAAT.GetHashKey(stat));
            _SG_Int(939452 + 5526, value);
            _SG_Int(1379108 + 1139, -1);
            Thread.Sleep(1000);
            _SG_Int(1390343 + 4, (int)oldhash);
            _SG_Int(939452 + 5526, (int)oldvalue);
        }
        
        public void bunkerMoney(int myMoney, int value)
        {
            int money_in_bunker = value;
            int total_cargo = Mem.ReadInt(settings.GlobalPTR-128, new int[] { 1180, 0x37D0 });
            int money = myMoney * total_cargo / money_in_bunker;
            Mem.Write(settings.GlobalPTR - 128, new int[] { 1180, 0x3F78 }, money);
            int time_remaining = Mem.ReadInt(settings.GlobalPTR - 128, new int[] { 1180, 2568 });
            int time_deliver = Mem.ReadInt(settings.GlobalPTR - 128, new int[] { 1180, 3810 });
            int mission_time = time_deliver - (time_remaining - 1000);
            Mem.Write(settings.GlobalPTR - 128, new int[] { 1180, 3810 }, mission_time);
        }
        #endregion

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void godModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.bGodMode = !this.bGodMode;
        }

        private void neverWantedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.bNeverWanted = !this.bNeverWanted;
        }

        private void noRagdollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.bNoRagdoll = !this.bNoRagdoll;
        }

        private void undeadOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.bUndeadOffRadar = !this.bUndeadOffRadar;
        }

        private void seatbeltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.bSeatBelt = !this.bSeatBelt;
        }

        private void superJumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.bSuperJump = !this.bSuperJump;
        }

        private void godModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.bVehicleGodMode = !this.bVehicleGodMode;
        }

        private void waypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bGodMode)
            {
                bgodState = true;
            }
            else
            {
                bGodMode = true;
                bgodState = false;
            }
            Activate();
            teleportWaypoint();
            if (!bgodState)
            {
                bGodMode = false;
            }
        }

        private void objectiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bGodMode)
            {
                bgodState = true;
            }
            else
            {
                bGodMode = true;
                bgodState = false;
            }
            Activate();
            teleportObjective();
            if (!bgodState)
            {
                bGodMode = false;
            }
        }

        private void leaveOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(-1);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(-2);
        }
        private void newPublicSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(1);
        }

        private void emptySessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            _freezeGame = new Thread(freezeGame) { IsBackground = true };
            _freezeGame.Start();
        }

        private void joinPublicSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(0);
        }

        private void soloSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(10);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(11);
        }

        private void findFriendSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(9);
        }

        private void closedFriendSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(6);
        }

        private void crewSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(3);
        }

        private void joinCrewSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(12);
        }

        private void closedCrewSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            LoadSession(2);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 0);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 1);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 2);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 3);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 4);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 5);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 0.0f);
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 0.5f);
        }

        private void x10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 1.0f);
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 1.5f);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 2.0f);
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 2.5f);
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 3.0f);
        }

        private void xToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 3.5f);
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 4.0f);
        }

        private void xToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 4.5f);
        }

        private void xToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 5.0f);
        }

        private void xToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 0.0f);
        }

        private void xToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 0.5f);
        }

        private void xToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 1.0f);
        }

        private void xToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 1.5f);
        }

        private void xToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 2.0f);
        }

        private void xToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 2.5f);
        }

        private void xToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 3.0f);
        }

        private void xToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 3.5f);
        }

        private void xToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 4.0f);
        }

        private void xToolStripMenuItem13_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 4.5f);
        }

        private void xToolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 5.0f);
        }

        private void xToolStripMenuItem15_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 0.0f);
        }

        private void xToolStripMenuItem16_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 0.5f);
        }

        private void xDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 1.0f);
        }

        private void xToolStripMenuItem17_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 1.5f);
        }

        private void xToolStripMenuItem18_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 2.0f);
        }

        private void xToolStripMenuItem19_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 2.5f);
        }

        private void xToolStripMenuItem20_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 3.0f);
        }

        private void xToolStripMenuItem21_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 3.5f);
        }

        private void xToolStripMenuItem22_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 4.0f);
        }

        private void xToolStripMenuItem23_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 4.5f);
        }

        private void xToolStripMenuItem24_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 5.0f);
        }

        private void disableCollisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.bDisableCollision = !this.bDisableCollision;
        }

        private void fistsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactType }, 2);
        }

        private void bulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactType }, 3);
        }

        private void explosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactType }, 5);
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactExplosion }, -1);
        }

        private void grenadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactExplosion }, 0);
        }

        private void orbitalCannonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactExplosion }, 59);
        }

        private void reInitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            toolStripMenuItem2.Enabled = false;
            reInitToolStripMenuItem.Visible = false;
            sessionToolStripMenuItem.Visible = false;
            playerToolStripMenuItem.Visible = false;
            vehicleToolStripMenuItem.Visible = false;
            weaponToolStripMenuItem.Visible = false;
            teleportToolStripMenuItem.Visible = false;
            tunablesToolStripMenuItem.Visible = false;
            reInitToolStripMenuItem.Enabled = false;
            sessionToolStripMenuItem.Enabled = false;
            playerToolStripMenuItem.Enabled = false;
            vehicleToolStripMenuItem.Enabled = false;
            weaponToolStripMenuItem.Enabled = false;
            teleportToolStripMenuItem.Enabled = false;
            tunablesToolStripMenuItem.Enabled = false;

            _getPointer = new Thread(getPointer) { IsBackground = true };
            _getPointer.Start();
        }

        private void clothing1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(0);
        }

        private void rPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(1);
        }

        private void cashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(2);
        }

        private void chipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(3);
        }

        private void discountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(4);
        }

        private void rPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(5);
        }

        private void cashToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(6);
        }

        private void chipsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(7);
        }

        private void clothing2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(8);
        }

        private void rPToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(9);
        }

        private void chipsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(10);
        }

        private void mysteryPrizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(11);
        }

        private void clothing3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(12);
        }

        private void rPToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(13);
        }

        private void cashToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(14);
        }

        private void chipsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(15);
        }

        private void clothing4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(16);
        }

        private void rPToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(17);
        }

        private void podiumVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(18);
        }

        private void cashToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Activate();
            getLuckyWheelPrice(19);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(1.0f);
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(2.0f);
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(3.0f);
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(5.0f);
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(10.0f);
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(15.0f);
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(20.0f);
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(25.0f);
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(30.0f);
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(35.0f);
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(40.0f);
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(50.0f);
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(100.0f);
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(200.0f);
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(300.0f);
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(500.0f);
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            Activate();
            setREPMultipler(1000.0f);
        }

        private void defaultToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(1.0f);
        }

        private void toolStripMenuItem39_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(2.0f);
        }

        private void toolStripMenuItem40_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(3.0f);
        }

        private void toolStripMenuItem41_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(5.0f);
        }

        private void toolStripMenuItem42_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(10.0f);
        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(15.0f);
        }

        private void toolStripMenuItem44_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(20.0f);
        }

        private void toolStripMenuItem45_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(25.0f);
        }

        private void toolStripMenuItem46_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(30.0f);
        }

        private void toolStripMenuItem47_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(35.0f);
        }

        private void toolStripMenuItem48_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(40.0f);
        }

        private void toolStripMenuItem49_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(50.0f);
        }

        private void toolStripMenuItem50_Click(object sender, EventArgs e)
        {
            Activate();
            setRPMultipler(100.0f);
        }

        private void nightclubPopularityMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            setStat("MP0_CLUB_POPULARITY", 1000);
            setStat("MP1_CLUB_POPULARITY", 1000);
        }

        private void staminaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            setStat("MP0_SCRIPT_INCREASE_STAM", 100);
            setStat("MP1_SCRIPT_INCREASE_STAM", 100);
        }

        private void strentghToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            setStat("MP0_SCRIPT_INCREASE_STRN", 100);
            setStat("MP1_SCRIPT_INCREASE_STRN", 100);
        }

        private void lungCapacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            setStat("MP0_SCRIPT_INCREASE_LUNG", 100);
            setStat("MP1_SCRIPT_INCREASE_LUNG", 100);
        }

        private void drivingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            setStat("MP0_SCRIPT_INCREASE_DRIV", 100);
            setStat("MP1_SCRIPT_INCREASE_DRIV", 100);
        }

        private void flyingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            setStat("MP0_SCRIPT_INCREASE_FLY", 100);
            setStat("MP1_SCRIPT_INCREASE_FLY", 100);
        }

        private void shootingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            setStat("MP0_SCRIPT_INCREASE_SHO", 100);
            setStat("MP1_SCRIPT_INCREASE_SHO", 100);
        }

        private void stealthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            setStat("MP0_SCRIPT_INCREASE_STL", 100);
            setStat("MP1_SCRIPT_INCREASE_STL", 100);
        }

        private void bunkerMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bunkerMoney(2041000, 195000);
        }

        private void longRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oRange }, 250F);
            Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oLockRange }, 250F);
        }
    }
    struct Location { public float x, y, z; }
}
