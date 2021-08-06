using System.Runtime.InteropServices;
using GTAV_External_Trainer.Helpers;
using Simple_GTAV_External_Trainer.Config;
using Simple_GTAV_External_Trainer.Settings;
using Simple_GTAV_External_Trainer.Memory;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Threading;
using System.Diagnostics;

namespace Simple_GTAV_External_Trainer
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

        #endregion

        #region PROCESS INFO
        private bool bGodMode = false;
        private bool bgodState = false;
        private bool bNeverWanted = false;
        private bool bNoRagdoll = false;
        private bool bUndeadOffRadar = false;
        private bool bSeatBelt = false;
        private bool bSuperJump = false;
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
        public void vGODMODE()
        {
            if (bVehicleGodMode)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.oGod }, 1);
                if (!settings.vgodm)
                {
                    Activate();
                }
                settings.vgodm = true;

                godModeToolStripMenuItem.Checked = true;
            }
            else
            {
                if (settings.vgodm)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.oGod }, 0);
                    settings.vgodm = false;
                    Deactivate();
                    godModeToolStripMenuItem.Checked = false;
                }
            }
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

            this.BackColor = Color.Orange;
            this.TransparencyKey = Color.Orange;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            int InitialStyle = GetWindowLong(this.Handle, -10);
            SetWindowLong(this.Handle, -10, InitialStyle | 0x800000 | 0x20);
            GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, 30);
            this.Top = rect.top;
            this.Left = rect.left;

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
            pSUPERJUMP();
            vGODMODE();
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

        private void teleportWaypoint()
        {
            Teleport(WaypointCoords);
        }

        private void teleportObjective()
        {
            Teleport(ObjectiveCoords);
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
            Mem.Write(GA(Index), null, value);
        }
        public void _SG_String(int Index, string value)
        {
            Mem.Write(GA(Index), null, value);
        }

        public void LoadSession(int id)
        {
            if(id == -1)
            {
                _SG_Int(1312443 + 2, -1);
                _SG_Int(1312443, 1);
                Thread.Sleep(200);
                _SG_Int(1312443, 0);
            }
            else if (id == -2)
            {
                _SG_Int(31622, 1);
                Thread.Sleep(200);
                _SG_Int(31622, 0);
            }
            else
            {
                _SG_Int(1312860, id);
                _SG_Int(1312443, 1);
                Thread.Sleep(200);
                _SG_Int(1312443, 0);
            }
        }

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
            teleportObjective();
            if (!bgodState)
            {
                bGodMode = false;
            }
        }

        private void leaveOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(-1);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(-2);
        }
        private void newPublicSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(1);
        }

        private void emptySessionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void joinPublicSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(0);
        }

        private void soloSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(10);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            LoadSession(11);
        }

        private void findFriendSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(9);
        }

        private void closedFriendSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(6);
        }

        private void crewSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(3);
        }

        private void joinCrewSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(12);
        }

        private void closedCrewSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSession(2);
        }
    }
    struct Location { public float x, y, z; }
}
