using GTAVCSMM.Config;
using GTAVCSMM.Helpers;
using GTAVCSMM.Memory;
using GTAVCSMM.Settings;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GTAVCSMM
{
    static class Program
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        const int SW_HIDE = 0;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static bool isHidden = false;
        private static IntPtr formHandle;

        private static int menuMainLvl = 0;
        private static int LastMenuMainLvl = 0;
        private static int menuLvl = 0;
        private static int LastMenuLvl = 0;
        private static int menuItm = 0;
        private static int LastMenuItm = 0;
        private static Form mainForm = new Form();
        private static ListBox listBx = new ListBox();
        private static Label label1 = new Label();
        private static Label label2 = new Label();
        private static string lastNavigation = string.Empty;

        #region WINDOW SETUP

        public const string WINDOW_NAME = "Grand Theft Auto V";
        public static IntPtr handle = FindWindow(null, WINDOW_NAME);

        public static RECT rect;

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

        public static Offsets offsets = new Offsets();
        public static Addresses addresses = new Addresses();
        public static Patterns pattern = new Patterns();
        public static TSettings settings = new TSettings();
        public static Mem Mem;
        public static Thread _freezeGame;

        public static System.Windows.Forms.Timer ProcessTimer = new System.Windows.Forms.Timer();
        public static System.Windows.Forms.Timer MemoryTimer = new System.Windows.Forms.Timer();
        public static System.Windows.Forms.Timer fastTimer = new System.Windows.Forms.Timer();

        #endregion

        #region PROCESS INFO
        private static bool bGodMode = false;
        private static bool bgodState = false;
        private static bool bNeverWanted = false;
        private static bool bNoRagdoll = false;
        private static bool bUndeadOffRadar = false;
        private static bool bSeatBelt = false;
        private static bool bSuperJump = false;
        private static bool bExplosiveAmmo = false;
        private static bool bDisableCollision = false;
        private static bool bVehicleGodMode = false;
        private static int frameFlagCount = 0;
        private static bool bGetCasinoPrice = false;
        private static int casinoPrice = 0;

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int a, int b, int c, int d, int damnIwonderifpeopleactuallyreadsthis);
        #endregion

        #region METHODS
        public static void pGODMODE()
        {
            if (bGodMode)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oGod }, 1);
                if (!settings.pgodm)
                {
                    Activate();
                }
                settings.pgodm = true;
            }
            else
            {
                if (settings.pgodm)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oGod }, 0);
                    settings.pgodm = false;
                    Deactivate();
                }
            }
        }

        public static void pNEVERWANTED()
        {
            if (bNeverWanted)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 0);
                if (!settings.pnwanted)
                {
                    Activate();
                }
                settings.pnwanted = true;
            }
            else
            {
                if (settings.pnwanted)
                {
                    settings.pnwanted = false;
                    Deactivate();
                }
            }
        }

        public static void pNORAGDOLL()
        {
            if (bNoRagdoll)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRagdoll }, 1);
                if (!settings.pnragdoll)
                {
                    Activate();
                }
                settings.pnragdoll = true;
            }
            else
            {
                if (settings.pnragdoll)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oRagdoll }, 32);
                    settings.pnragdoll = false;
                    Deactivate();
                }
            }
        }

        public static void pUNDEADOFFRADAR()
        {
            if (bUndeadOffRadar)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oHealthMax }, 0);
                if (!settings.puoffradar)
                {
                    Activate();
                }
                settings.puoffradar = true;
            }
            else
            {
                if (settings.puoffradar)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oHealthMax }, 328f);
                    settings.puoffradar = false;
                    Deactivate();
                }
            }
        }

        public static void pSEATBELT()
        {
            if (bSeatBelt)
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oSeatbelt }, -55);
                if (!settings.psbelt)
                {
                    Activate();
                }
                settings.psbelt = true;
            }
            else
            {
                if (settings.psbelt)
                {
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oSeatbelt }, -56);
                    settings.psbelt = false;
                    Deactivate();
                }
            }
        }

        public static void pSUPERJUMP()
        {
            if (bSuperJump)
            {
                if (!settings.psjump)
                {
                    frameFlagCount = frameFlagCount + 64;
                    Activate();
                }
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oFrameFlags }, frameFlagCount);
                settings.psjump = true;
            }
            else
            {
                if (settings.psjump)
                {
                    frameFlagCount = frameFlagCount - 64;
                    Deactivate();
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oFrameFlags }, frameFlagCount);
                    settings.psjump = false;
                }
            }
        }

        public static void pEXPLOSIVEAMMO()
        {
            if (bExplosiveAmmo)
            {
                if (!settings.psexammo)
                {
                    frameFlagCount = frameFlagCount + 8;
                    Activate();
                }
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oFrameFlags }, frameFlagCount);
                settings.psexammo = true;
            }
            else
            {
                if (settings.psexammo)
                {
                    frameFlagCount = frameFlagCount - 8;
                    Deactivate();
                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oFrameFlags }, frameFlagCount);
                    settings.psexammo = false;
                }
            }
        }
        public static void pDISABLECOLLISION()
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
            }
            else
            {
                if (settings.pdiscol)
                {
                    Mem.writeFloat(paddr2, null, 0.25f);
                    settings.pdiscol = false;
                    Deactivate();
                }
            }
        }
        public static void vGODMODE()
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
                }
                else
                {
                    if (settings.vgodm)
                    {
                        Mem.writeInt(paddr2, null, 0);
                        settings.vgodm = false;
                        Deactivate();
                    }
                }
            }
        }
        public static void cPRICE()
        {
            if (bGetCasinoPrice)
            {
                getLuckyWheelPrice(casinoPrice);
            }
        }

        public static void getPointer()
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

                if (settings.gameProcess > 0)
                {
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
                else
                {
                    MessageBox.Show("GTA is not Running!", "Serious Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Quit();
                }
            }
            catch
            {
                MessageBox.Show("GTA is not Running!", "Serious Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Quit();
            }
        }

        public static void freezeGame()
        {
            Console.WriteLine("Freezing game");
            Speeder.Suspend(settings.gameProcess);
            Thread.Sleep(10000);
            Speeder.Resume(settings.gameProcess);
            _freezeGame.Abort();
        }
        #endregion


        #region TIMERS

        private static void ProcessTimer_Tick(object sender, EventArgs e)
        {

        }

        private static void MemoryTimer_Tick(object sender, EventArgs e)
        {
            pGODMODE();
            pNEVERWANTED();
            pNORAGDOLL();
            pUNDEADOFFRADAR();
            pSEATBELT();
            pDISABLECOLLISION();
            vGODMODE();
        }

        private static void fastTimer_Tick(object sender, EventArgs e)
        {
            pSUPERJUMP();
            pEXPLOSIVEAMMO();
            cPRICE();
        }

        #endregion

        private static void Quit()
        {
            Environment.Exit(0);
        }

        //audio response to actions
        public static void Activate()
        {
            Console.Beep(523, 75);
            Console.Beep(587, 75);
            Console.Beep(700, 75);
        }

        public static void Deactivate()
        {
            Console.Beep(700, 75);
            Console.Beep(587, 75);
            Console.Beep(523, 75);
        }

        public static void Toggle()
        {
            Console.Beep(523, 75);
            Console.Beep(523, 75);
            Console.Beep(523, 75);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "GTA5TrainerCS", out createdNew))

                if (createdNew)
                {
                    createMainForm();
                    listBx.Items.Add("Getting game pointers.");
                    listBx.Enabled = false;

                    getPointer();

                    listboxStyle();
                    listboxFill(0, 0);
                    fastTimer.Enabled = true;
                    ProcessTimer.Enabled = true;
                    MemoryTimer.Enabled = true;
                    listBx.Enabled = true;

                    Application.Run();

                }
        }

        public static void createMainForm()
        {
            // 
            // listBx
            // 
            listBx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            listBx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            listBx.Font = new System.Drawing.Font("Arial", 13.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listBx.FormattingEnabled = true;
            listBx.ItemHeight = 24;
            listBx.Location = new System.Drawing.Point(6, 50);
            listBx.Margin = new System.Windows.Forms.Padding(10);
            listBx.MaximumSize = new System.Drawing.Size(290, 500);
            listBx.Name = "listBox1";
            listBx.Size = new System.Drawing.Size(290, 480);
            listBx.TabIndex = 0;

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            label1.Font = new System.Drawing.Font("Arial Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(1, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(168, 33);
            label1.TabIndex = 1;
            label1.Text = "GTAVCSMM";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(162, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(65, 24);
            label2.TabIndex = 2;
            label2.Text = "o1.58";
            // 
            // fastTimer
            // 
            fastTimer.Interval = 1;
            fastTimer.Tick += new System.EventHandler(fastTimer_Tick);
            // 
            // ProcessTimer
            // 
            ProcessTimer.Interval = 100;
            ProcessTimer.Tick += new System.EventHandler(ProcessTimer_Tick);
            // 
            // MemoryTimer
            // 
            MemoryTimer.Interval = 100;
            MemoryTimer.Tick += new System.EventHandler(MemoryTimer_Tick);
            // 
            // Form1
            // 
            mainForm.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            mainForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            mainForm.AutoSize = true;
            mainForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            mainForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            mainForm.ClientSize = new System.Drawing.Size(207, 116);
            mainForm.Controls.Add(label2);
            mainForm.Controls.Add(label1);
            mainForm.Controls.Add(listBx);
            mainForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mainForm.KeyPreview = true;
            mainForm.Name = "Form1";
            mainForm.Opacity = 0.8D;
            mainForm.ShowIcon = false;
            mainForm.ShowInTaskbar = false;
            mainForm.Text = "GTAVCSMM";
            mainForm.TopMost = true;

            formHandle = mainForm.Handle;
            _hookID = SetHook(_proc);

            mainForm.FormBorderStyle = FormBorderStyle.None;
            int InitialStyle = GetWindowLong(mainForm.Handle, -10);
            SetWindowLong(mainForm.Handle, -10, InitialStyle | 0x800000 | 0x20);
            GetWindowRect(handle, out rect);
            mainForm.Top = rect.top - 20;
            mainForm.Left = rect.left + 30;

            mainForm.Show();
        }

        public static void listboxStyle()
        {
        }

        public static void listboxFill(int mainMenulevel, int menulevel)
        {
            listBx.Items.Clear();
            switch (mainMenulevel)
            {
                case 0:
                    /*
                     * Mainlevel 0
                     */
                    listBx.Items.Add("Main \t\t\t ►");       // 0,0
                    listBx.Items.Add("Session \t\t\t ►");    // 0,1
                    listBx.Items.Add("Player \t\t\t ►");     // 0,2
                    listBx.Items.Add("Vehicle \t\t\t ►");    // 0,3
                    listBx.Items.Add("Weapon \t\t\t ►");     // 0,4
                    listBx.Items.Add("Teleport \t\t\t ►");   // 0,5
                    listBx.Items.Add("Tunables \t\t\t ►");   // 0,6
                    listBx.Items.Add("Online Services \t\t ►");   // 0,7

                    menuMainLvl = 0;
                    menuLvl = 0;

                    LastMenuMainLvl = 0;
                    LastMenuLvl = 0;
                    LastMenuItm = 0;
                    break;

                case 1:
                    switch (menulevel)
                    {
                        case 0:
                            listBx.Items.Add("Re-Init");
                            listBx.Items.Add("Quit (Del)");

                            menuMainLvl = 1;
                            menuLvl = 0;

                            LastMenuMainLvl = 0;
                            LastMenuLvl = 1;
                            LastMenuItm = 0;
                            break;

                        case 1:
                            listBx.Items.Add("Join Public Session");
                            listBx.Items.Add("New Public Session");
                            listBx.Items.Add("Solo Session");
                            listBx.Items.Add("Leave Online");
                            listBx.Items.Add("Empty Session (10 Sec. Freeze)");
                            listBx.Items.Add("Invite Only Session");
                            listBx.Items.Add("Find Friend Session");
                            listBx.Items.Add("Closed Friend Session");
                            listBx.Items.Add("Crew Session");
                            listBx.Items.Add("Join Crew Session");
                            listBx.Items.Add("Closed Crew Session");
                            /*
                            listBx.Items.Add("Disconnect");
                            */

                            menuMainLvl = 1;
                            menuLvl = 1;

                            LastMenuMainLvl = 0;
                            LastMenuLvl = 1;
                            LastMenuItm = 1;
                            break;

                        case 2:
                            listBx.Items.Add("God Mode (F6)");
                            listBx.Items.Add("Super Jump");
                            listBx.Items.Add("Never Wanted (F7)");
                            listBx.Items.Add("Seatbelt");
                            listBx.Items.Add("No Ragdoll");
                            listBx.Items.Add("Undead Off-Radar");
                            listBx.Items.Add("Disable Collision");
                            listBx.Items.Add("Skills \t\t\t ►");
                            listBx.Items.Add("Swim Speed \t\t ►");
                            listBx.Items.Add("Stealth Speed \t\t ►");
                            listBx.Items.Add("Run Speed \t\t ►");
                            listBx.Items.Add("Wanted Level \t\t ►");

                            menuMainLvl = 1;
                            menuLvl = 2;

                            LastMenuMainLvl = 0;
                            LastMenuLvl = 1;
                            LastMenuItm = 2;
                            break;

                        case 3:
                            listBx.Items.Add("God Mode");

                            menuMainLvl = 1;
                            menuLvl = 3;

                            LastMenuMainLvl = 0;
                            LastMenuLvl = 1;
                            LastMenuItm = 3;
                            break;

                        case 4:
                            listBx.Items.Add("Explosive Ammo");
                            listBx.Items.Add("Long Range");

                            menuMainLvl = 1;
                            menuLvl = 4;

                            LastMenuMainLvl = 0;
                            LastMenuLvl = 1;
                            LastMenuItm = 4;
                            break;

                        case 5:
                            listBx.Items.Add("Waypoint (F8)");
                            listBx.Items.Add("Objective");
                            listBx.Items.Add("Locations \t\t ►");

                            menuMainLvl = 1;
                            menuLvl = 5;

                            LastMenuMainLvl = 0;
                            LastMenuLvl = 1;
                            LastMenuItm = 5;
                            break;

                        case 6:
                            listBx.Items.Add("RP Multipler \t\t ►");
                            listBx.Items.Add("REP Multipler \t\t ►");
                            listBx.Items.Add("Nightclub Popularity");

                            menuMainLvl = 1;
                            menuLvl = 6;

                            LastMenuMainLvl = 0;
                            LastMenuLvl = 1;
                            LastMenuItm = 6;
                            break;

                        case 7:
                            listBx.Items.Add("Get Lucky Wheel Price \t ►");
                            listBx.Items.Add("Trigger Nightclub Production \t ►");
                            listBx.Items.Add("Quick Car Spawn \t\t ►");
                            listBx.Items.Add("Manual Car Spawn \t\t ►");

                            menuMainLvl = 1;
                            menuLvl = 7;

                            LastMenuMainLvl = 0;
                            LastMenuLvl = 1;
                            LastMenuItm = 7;
                            break;
                    }
                    break;

                case 2:
                    switch (menulevel)
                    {
                        case 7:
                            listBx.Items.Add("Stamina");
                            listBx.Items.Add("Strength");
                            listBx.Items.Add("Lung Capacity");
                            listBx.Items.Add("Driving");
                            listBx.Items.Add("Flying");
                            listBx.Items.Add("Shooting");
                            listBx.Items.Add("Stealth");

                            menuMainLvl = 2;
                            menuLvl = 7;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 2;
                            LastMenuItm = 7;
                            break;

                        case 8:
                            listBx.Items.Add("Swim Speed = 0.0");
                            listBx.Items.Add("Swim Speed = 0.5");
                            listBx.Items.Add("Swim Speed = 1.0 (Default)");
                            listBx.Items.Add("Swim Speed = 1.5");
                            listBx.Items.Add("Swim Speed = 2.0");
                            listBx.Items.Add("Swim Speed = 2.5");
                            listBx.Items.Add("Swim Speed = 3.0");
                            listBx.Items.Add("Swim Speed = 3.5");
                            listBx.Items.Add("Swim Speed = 4.0");
                            listBx.Items.Add("Swim Speed = 4.5");
                            listBx.Items.Add("Swim Speed = 5.0");

                            menuMainLvl = 2;
                            menuLvl = 8;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 2;
                            LastMenuItm = 8;
                            break;

                        case 9:
                            listBx.Items.Add("Stealth Speed = 0.0");
                            listBx.Items.Add("Stealth Speed = 0.5");
                            listBx.Items.Add("Stealth Speed = 1.0 (Default)");
                            listBx.Items.Add("Stealth Speed = 1.5");
                            listBx.Items.Add("Stealth Speed = 2.0");
                            listBx.Items.Add("Stealth Speed = 2.5");
                            listBx.Items.Add("Stealth Speed = 3.0");
                            listBx.Items.Add("Stealth Speed = 3.5");
                            listBx.Items.Add("Stealth Speed = 4.0");
                            listBx.Items.Add("Stealth Speed = 4.5");
                            listBx.Items.Add("Stealth Speed = 5.0");

                            menuMainLvl = 2;
                            menuLvl = 9;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 2;
                            LastMenuItm = 9;
                            break;

                        case 10:
                            listBx.Items.Add("Run Speed = 0.0");
                            listBx.Items.Add("Run Speed = 0.5");
                            listBx.Items.Add("Run Speed = 1.0 (Default)");
                            listBx.Items.Add("Run Speed = 1.5");
                            listBx.Items.Add("Run Speed = 2.0");
                            listBx.Items.Add("Run Speed = 2.5");
                            listBx.Items.Add("Run Speed = 3.0");
                            listBx.Items.Add("Run Speed = 3.5");
                            listBx.Items.Add("Run Speed = 4.0");
                            listBx.Items.Add("Run Speed = 4.5");
                            listBx.Items.Add("Run Speed = 5.0");

                            menuMainLvl = 2;
                            menuLvl = 10;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 2;
                            LastMenuItm = 10;
                            break;

                        case 11:
                            listBx.Items.Add("Wanted Level = 0");
                            listBx.Items.Add("Wanted Level = 1");
                            listBx.Items.Add("Wanted Level = 2");
                            listBx.Items.Add("Wanted Level = 3");
                            listBx.Items.Add("Wanted Level = 4");
                            listBx.Items.Add("Wanted Level = 5");

                            menuMainLvl = 2;
                            menuLvl = 11;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 2;
                            LastMenuItm = 11;
                            break;
                    }
                    break;

                case 5:
                    switch (menulevel)
                    {
                        case 2:
                            listBx.Items.Add("Nightclub");              // 0
                            listBx.Items.Add("Arcade");                 // 1
                            listBx.Items.Add("Office");                 // 2
                            listBx.Items.Add("Bunker");                 // 3
                            listBx.Items.Add("Facility");               // 4
                            listBx.Items.Add("Hangar");                 // 5
                            listBx.Items.Add("Yacht");                  // 6
                            listBx.Items.Add("Kosatka");                // 4
                            listBx.Items.Add("Sell Vehicles & Cargo");  // 8
                            listBx.Items.Add("Goods Warehouse");        // 9
                            listBx.Items.Add("Auto Warehouse");         // 10
                            listBx.Items.Add("MC Clubhouse");           // 11
                            listBx.Items.Add("Meth Lab");               // 12
                            listBx.Items.Add("Cocaine Lockup");         // 13
                            listBx.Items.Add("Weed Farm");              // 14
                            listBx.Items.Add("Counterfeit Cash");       // 15
                            listBx.Items.Add("Document Forgery");       // 16
                            listBx.Items.Add("Casino");                 // 17
                            listBx.Items.Add("LS Car Meet");            // 18
                            listBx.Items.Add("Auto Shop Property");     // 19
                            listBx.Items.Add("Agency (F. Clinton)");    // 20
                            listBx.Items.Add("Music Locker");           // 21
                            listBx.Items.Add("Arena Workshop");         // 22
                            listBx.Items.Add("Cayo Perico");            // 23
                            listBx.Items.Add("Flight School");          // 24
                            listBx.Items.Add("Masks (Vespucci Beach)"); // 25

                            menuMainLvl = 5;
                            menuLvl = 1;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 5;
                            LastMenuItm = 2;
                            break;
                    }
                    break;

                case 6:
                    switch (menulevel)
                    {
                        case 0:
                            listBx.Items.Add("RP x 1.0");
                            listBx.Items.Add("RP x 2.0");
                            listBx.Items.Add("RP x 3.0");
                            listBx.Items.Add("RP x 5.0");
                            listBx.Items.Add("RP x 10.0");
                            listBx.Items.Add("RP x 15.0");
                            listBx.Items.Add("RP x 20.0");
                            listBx.Items.Add("RP x 25.0");
                            listBx.Items.Add("RP x 30.0");
                            listBx.Items.Add("RP x 35.0");
                            listBx.Items.Add("RP x 40.0");
                            listBx.Items.Add("RP x 50.0");
                            listBx.Items.Add("RP x 100.0");

                            menuMainLvl = 6;
                            menuLvl = 0;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 6;
                            LastMenuItm = 0;
                            break;

                        case 1:
                            listBx.Items.Add("REP x 1.0");
                            listBx.Items.Add("REP x 2.0");
                            listBx.Items.Add("REP x 3.0");
                            listBx.Items.Add("REP x 5.0");
                            listBx.Items.Add("REP x 10.0");
                            listBx.Items.Add("REP x 15.0");
                            listBx.Items.Add("REP x 20.0");
                            listBx.Items.Add("REP x 25.0");
                            listBx.Items.Add("REP x 30.0");
                            listBx.Items.Add("REP x 35.0");
                            listBx.Items.Add("REP x 40.0");
                            listBx.Items.Add("REP x 50.0");
                            listBx.Items.Add("REP x 100.0");
                            listBx.Items.Add("REP x 200.0");
                            listBx.Items.Add("REP x 300.0");
                            listBx.Items.Add("REP x 500.0");
                            listBx.Items.Add("REP x 1000.0");

                            menuMainLvl = 6;
                            menuLvl = 1;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 6;
                            LastMenuItm = 1;
                            break;
                    }
                    break;

                case 7:
                    switch (menulevel)
                    {
                        case 0:
                            listBx.Items.Add("Clothes (0)");
                            listBx.Items.Add("RP (1)");
                            listBx.Items.Add("Cash (1)");
                            listBx.Items.Add("Chips (1)");
                            listBx.Items.Add("Discount");
                            listBx.Items.Add("RP (2)");
                            listBx.Items.Add("Cash (2)");
                            listBx.Items.Add("Chips (2)");
                            listBx.Items.Add("Clothes (2)");
                            listBx.Items.Add("RP (3)");
                            listBx.Items.Add("Chips (3)");
                            listBx.Items.Add("Mystery Price");
                            listBx.Items.Add("Clothes (3)");
                            listBx.Items.Add("RP (4)");
                            listBx.Items.Add("Chips (4)");
                            listBx.Items.Add("Clothes (4)");
                            listBx.Items.Add("RP (5)");
                            listBx.Items.Add("Podium Vehicle");

                            menuMainLvl = 7;
                            menuLvl = 0;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 7;
                            LastMenuItm = 0;
                            break;

                        case 1:
                            listBx.Items.Add("South American Imports");
                            listBx.Items.Add("Pharmaceutical Research");
                            listBx.Items.Add("Organic Produce");
                            listBx.Items.Add("Printing and Copying");
                            listBx.Items.Add("Cash Creation");
                            listBx.Items.Add("Sporting Goods");
                            listBx.Items.Add("Cargo and Shipments");

                            menuMainLvl = 7;
                            menuLvl = 1;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 7;
                            LastMenuItm = 1;
                            break;

                        case 2:
                            listBx.Items.Add("ZR380");
                            listBx.Items.Add("Deluxo");
                            listBx.Items.Add("Opressor2");
                            listBx.Items.Add("Vigilante");
                            listBx.Items.Add("Toreador");
                            listBx.Items.Add("Future Brutus");
                            listBx.Items.Add("Future Dominator");
                            listBx.Items.Add("Future Imperator");

                            menuMainLvl = 7;
                            menuLvl = 2;

                            LastMenuMainLvl = 1;
                            LastMenuLvl = 7;
                            LastMenuItm = 2;
                            break;
                    }
                    break;
            }
            listBx.SelectedIndex = 0;
            mainForm.TopMost = true;
        }

        public static void runitem(int mainMenulevel, int menulevel, int menuItem)
        {
            Console.WriteLine("Command to run: " + mainMenulevel + " " + menulevel + " " + menuItem);
            switch (mainMenulevel)
            {
                case 0:
                    switch (menulevel)
                    {
                        case 0:
                            switch (menuItem)
                            {
                                case 0:
                                    listboxFill(1, 0);
                                    break;
                                case 1:
                                    listboxFill(1, 1);
                                    break;
                                case 2:
                                    listboxFill(1, 2);
                                    break;
                                case 3:
                                    listboxFill(1, 3);
                                    break;
                                case 4:
                                    listboxFill(1, 4);
                                    break;
                                case 5:
                                    listboxFill(1, 5);
                                    break;
                                case 6:
                                    listboxFill(1, 6);
                                    break;
                                case 7:
                                    listboxFill(1, 7);
                                    break;
                            }
                            break;
                    }
                    break;

                case 1:
                    switch (menulevel)
                    {
                        case 0:
                            switch (menuItem)
                            {
                                case 0:
                                    // Re-Init
                                    Console.WriteLine("Nothing to do");
                                    break;
                                case 1:
                                    Quit();
                                    break;
                            }
                            break;
                        case 1:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    LoadSession(0);
                                    break;
                                case 1:
                                    Activate();
                                    LoadSession(1);
                                    break;
                                case 2:
                                    Activate();
                                    LoadSession(10);
                                    break;
                                case 3:
                                    Activate();
                                    LoadSession(-1);
                                    break;
                                case 4:
                                    Activate();
                                    _freezeGame = new Thread(freezeGame) { IsBackground = true };
                                    _freezeGame.Start();
                                    break;
                                case 5:
                                    Activate();
                                    LoadSession(11);
                                    break;
                                case 6:
                                    Activate();
                                    LoadSession(9);
                                    break;
                                case 7:
                                    Activate();
                                    LoadSession(6);
                                    break;
                                case 8:
                                    Activate();
                                    LoadSession(3);
                                    break;
                                case 9:
                                    Activate();
                                    LoadSession(12);
                                    break;
                                case 10:
                                    Activate();
                                    LoadSession(2);
                                    break;
                                    /*
                                case 11:
                                    Activate();
                                    LoadSession(-2);
                                    break;
                                    */
                            }
                            break;
                        case 2:
                            switch (menuItem)
                            {
                                case 0:
                                    bGodMode = !bGodMode;
                                    break;
                                case 1:
                                    bSuperJump = !bSuperJump;
                                    break;
                                case 2:
                                    bNeverWanted = !bNeverWanted;
                                    break;
                                case 3:
                                    bSeatBelt = !bSeatBelt;
                                    break;
                                case 4:
                                    bNoRagdoll = !bNoRagdoll;
                                    break;
                                case 5:
                                    bUndeadOffRadar = !bUndeadOffRadar;
                                    break;
                                case 6:
                                    bDisableCollision = !bDisableCollision;
                                    break;
                                case 7:
                                    listboxFill(2, 7);
                                    break;
                                case 8:
                                    listboxFill(2, 8);
                                    break;
                                case 9:
                                    listboxFill(2, 9);
                                    break;
                                case 10:
                                    listboxFill(2, 10);
                                    break;
                                case 11:
                                    listboxFill(2, 11);
                                    break;
                            }
                            break;
                        case 3:
                            switch (menuItem)
                            {
                                case 0:
                                    bVehicleGodMode = !bVehicleGodMode;
                                    break;
                            }
                            break;
                        case 4:
                            switch (menuItem)
                            {
                                case 0:
                                    bExplosiveAmmo = !bExplosiveAmmo;
                                    break;
                                case 1:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oRange }, 250F);
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oLockRange }, 250F);
                                    break;
                            }
                            break;
                        case 5:
                            switch (menuItem)
                            {
                                case 0:
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
                                    int[] tpIdArray = new int[] { 8 };
                                    int[] tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray, 20);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 1:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 1 };
                                    tpColArray = new int[] { 5, 60, 66 };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 2:
                                    listboxFill(5, 2);
                                    break;
                            }
                            break;
                        case 6:
                            switch (menuItem)
                            {
                                case 0:
                                    listboxFill(6, 0);
                                    break;
                                case 1:
                                    listboxFill(6, 1);
                                    break;
                                case 2:
                                    Activate();
                                    setStat("MP0_CLUB_POPULARITY", 1000);
                                    setStat("MP1_CLUB_POPULARITY", 1000);
                                    break;
                            }
                            break;
                        case 7:
                            switch (menuItem)
                            {
                                case 0:
                                    listboxFill(7, 0);
                                    break;
                                case 1:
                                    listboxFill(7, 1);
                                    break;
                                case 2:
                                    listboxFill(7, 2);
                                    break;
                                case 3:
                                    new Thread(() =>
                                    {
                                        Thread.CurrentThread.IsBackground = true;
                                        string promptValue = ShowDialog("Enter the name like \"opressor2\" without the quotes.", "Enter car name!");
                                        if (promptValue != "")
                                        {
                                            Activate();
                                            carSpawn(promptValue, 0);
                                        }
                                    }).Start();
                                    break;
                            }
                            break;

                    }
                    break;
                case 2:
                    switch (menulevel)
                    {
                        case 7:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    setStat("MP0_SCRIPT_INCREASE_STAM", 100);
                                    setStat("MP1_SCRIPT_INCREASE_STAM", 100);
                                    break;
                                case 1:
                                    Activate();
                                    setStat("MP0_SCRIPT_INCREASE_STRN", 100);
                                    setStat("MP1_SCRIPT_INCREASE_STRN", 100);
                                    break;
                                case 2:
                                    Activate();
                                    setStat("MP0_SCRIPT_INCREASE_LUNG", 100);
                                    setStat("MP1_SCRIPT_INCREASE_LUNG", 100);
                                    break;
                                case 3:
                                    Activate();
                                    setStat("MP0_SCRIPT_INCREASE_DRIV", 100);
                                    setStat("MP1_SCRIPT_INCREASE_DRIV", 100);
                                    break;
                                case 4:
                                    Activate();
                                    setStat("MP0_SCRIPT_INCREASE_FLY", 100);
                                    setStat("MP1_SCRIPT_INCREASE_FLY", 100);
                                    break;
                                case 5:
                                    Activate();
                                    setStat("MP0_SCRIPT_INCREASE_SHO", 100);
                                    setStat("MP1_SCRIPT_INCREASE_SHO", 100);
                                    break;
                                case 6:
                                    Activate();
                                    setStat("MP0_SCRIPT_INCREASE_STL", 100);
                                    setStat("MP1_SCRIPT_INCREASE_STL", 100);
                                    break;
                            }
                            break;

                        case 8:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 0.0f);
                                    break;
                                case 1:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 0.5f);
                                    break;
                                case 2:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 1.0f);
                                    break;
                                case 3:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 1.5f);
                                    break;
                                case 4:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 2.0f);
                                    break;
                                case 5:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 2.5f);
                                    break;
                                case 6:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 3.0f);
                                    break;
                                case 7:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 3.5f);
                                    break;
                                case 8:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 4.0f);
                                    break;
                                case 9:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 4.5f);
                                    break;
                                case 10:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oSwimSpeed }, 5.0f);
                                    break;
                            }
                            break;

                        case 9:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 0.0f);
                                    break;
                                case 1:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 0.5f);
                                    break;
                                case 2:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 1.0f);
                                    break;
                                case 3:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 1.5f);
                                    break;
                                case 4:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 2.0f);
                                    break;
                                case 5:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 2.5f);
                                    break;
                                case 6:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 3.0f);
                                    break;
                                case 7:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 3.5f);
                                    break;
                                case 8:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 4.0f);
                                    break;
                                case 9:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 4.5f);
                                    break;
                                case 10:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWalkSpeed }, 5.0f);
                                    break;
                            }
                            break;

                        case 10:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 0.0f);
                                    break;
                                case 1:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 0.5f);
                                    break;
                                case 2:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 1.0f);
                                    break;
                                case 3:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 1.5f);
                                    break;
                                case 4:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 2.0f);
                                    break;
                                case 5:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 2.5f);
                                    break;
                                case 6:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 3.0f);
                                    break;
                                case 7:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 3.5f);
                                    break;
                                case 8:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 4.0f);
                                    break;
                                case 9:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 4.5f);
                                    break;
                                case 10:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oRunSpeed }, 5.0f);
                                    break;
                            }
                            break;

                        case 11:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 0);
                                    break;
                                case 1:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 1);
                                    break;
                                case 2:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 2);
                                    break;
                                case 3:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 3);
                                    break;
                                case 4:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 4);
                                    break;
                                case 5:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPlayerInfo, offsets.oWanted }, 5);
                                    break;
                            }
                            break;
                    }
                    break;

                case 4:
                    switch (menulevel)
                    {
                        case 0:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactType }, 2);
                                    break;
                                case 1:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactType }, 3);
                                    break;
                                case 2:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactType }, 5);
                                    break;
                            }
                            break;

                        case 1:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactExplosion }, -1);
                                    break;
                                case 1:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactExplosion }, 0);
                                    break;
                                case 2:
                                    Activate();
                                    Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCPedWeaponManager, offsets.pCWeaponInfo, offsets.oImpactExplosion }, 59);
                                    break;
                            }
                            break;
                    }
                    break;

                case 5:
                    switch (menulevel)
                    {
                        case 1:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    int[] tpIdArray = new int[] { 614 };
                                    int[] tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 1:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 740 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 2:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 475 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 3:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 557 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 4:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 590 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 5:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 569 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 6:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 455 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 7:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 760 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 8:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 64, 427, 478, 423, 501, 556 };
                                    tpColArray = new int[] { 2, 3 };
                                    teleportBlip(tpIdArray, tpColArray, 2);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 9:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 473 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 10:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 524 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 11:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 492 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 12:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 499 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 13:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 497 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 14:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 496 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 15:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 500 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 16:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 498 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 17:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    /*
                                        Location loc = new Location { x = 918.2499f, y = 50.25024f, z = 80.89696f };
                                        Teleport(loc);
                                    */
                                    tpIdArray = new int[] { 679 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 18:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    /*
                                        loc = new Location { x = 777f, y = -1876f, z = 29.29654f };
                                        Teleport(loc);
                                    */
                                    tpIdArray = new int[] { 777 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 19:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 779 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 20:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 826 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 21:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 136 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 22:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 643 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 23:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 766 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 24:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 90 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                                case 25:
                                    Activate();

                                    if (bGodMode)
                                    {
                                        bgodState = true;
                                    }
                                    else
                                    {
                                        bGodMode = true;
                                        bgodState = false;
                                    }
                                    tpIdArray = new int[] { 362 };
                                    tpColArray = new int[] { };
                                    teleportBlip(tpIdArray, tpColArray);
                                    if (!bgodState)
                                    {
                                        bGodMode = false;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;

                case 6:
                    switch (menulevel)
                    {
                        case 0:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    setRPMultipler(1.0f);
                                    break;
                                case 1:
                                    Activate();
                                    setRPMultipler(2.0f);
                                    break;
                                case 2:
                                    Activate();
                                    setRPMultipler(3.0f);
                                    break;
                                case 3:
                                    Activate();
                                    setRPMultipler(5.0f);
                                    break;
                                case 4:
                                    Activate();
                                    setRPMultipler(10.0f);
                                    break;
                                case 5:
                                    Activate();
                                    setRPMultipler(15.0f);
                                    break;
                                case 6:
                                    Activate();
                                    setRPMultipler(20.0f);
                                    break;
                                case 7:
                                    Activate();
                                    setRPMultipler(25.0f);
                                    break;
                                case 8:
                                    Activate();
                                    setRPMultipler(30.0f);
                                    break;
                                case 9:
                                    Activate();
                                    setRPMultipler(35.0f);
                                    break;
                                case 10:
                                    Activate();
                                    setRPMultipler(40.0f);
                                    break;
                                case 11:
                                    Activate();
                                    setRPMultipler(50.0f);
                                    break;
                                case 12:
                                    Activate();
                                    setRPMultipler(100.0f);
                                    break;
                            }
                            break;

                        case 1:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    setREPMultipler(1.0f);
                                    break;
                                case 1:
                                    Activate();
                                    setREPMultipler(2.0f);
                                    break;
                                case 2:
                                    Activate();
                                    setREPMultipler(3.0f);
                                    break;
                                case 3:
                                    Activate();
                                    setREPMultipler(5.0f);
                                    break;
                                case 4:
                                    Activate();
                                    setREPMultipler(10.0f);
                                    break;
                                case 5:
                                    Activate();
                                    setREPMultipler(15.0f);
                                    break;
                                case 6:
                                    Activate();
                                    setREPMultipler(20.0f);
                                    break;
                                case 7:
                                    Activate();
                                    setREPMultipler(25.0f);
                                    break;
                                case 8:
                                    Activate();
                                    setREPMultipler(30.0f);
                                    break;
                                case 9:
                                    Activate();
                                    setREPMultipler(35.0f);
                                    break;
                                case 10:
                                    Activate();
                                    setREPMultipler(40.0f);
                                    break;
                                case 11:
                                    Activate();
                                    setREPMultipler(50.0f);
                                    break;
                                case 12:
                                    Activate();
                                    setREPMultipler(100.0f);
                                    break;
                                case 13:
                                    Activate();
                                    setREPMultipler(200.0f);
                                    break;
                                case 14:
                                    Activate();
                                    setREPMultipler(300.0f);
                                    break;
                                case 15:
                                    Activate();
                                    setREPMultipler(500.0f);
                                    break;
                                case 16:
                                    Activate();
                                    setREPMultipler(1000.0f);
                                    break;
                            }
                            break;
                    }
                    break;

                case 7:
                    switch (menulevel)
                    {
                        case 0:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    casinoPrice = 1;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 1:
                                    Activate();
                                    casinoPrice = 2;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 2:
                                    Activate();
                                    casinoPrice = 3;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 3:
                                    Activate();
                                    casinoPrice = 4;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 4:
                                    Activate();
                                    casinoPrice = 5;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 5:
                                    Activate();
                                    casinoPrice = 6;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 6:
                                    Activate();
                                    casinoPrice = 7;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 7:
                                    Activate();
                                    casinoPrice = 8;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 8:
                                    Activate();
                                    casinoPrice = 9;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 9:
                                    Activate();
                                    casinoPrice = 10;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 10:
                                    Activate();
                                    casinoPrice = 11;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 11:
                                    Activate();
                                    casinoPrice = 12;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 12:
                                    Activate();
                                    casinoPrice = 13;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 13:
                                    Activate();
                                    casinoPrice = 14;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 14:
                                    Activate();
                                    casinoPrice = 15;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 15:
                                    Activate();
                                    casinoPrice = 16;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 16:
                                    Activate();
                                    casinoPrice = 17;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                                case 17:
                                    Activate();
                                    casinoPrice = 18;
                                    bGetCasinoPrice = !bGetCasinoPrice;
                                    break;
                            }
                            break;
                        case 1:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    _SG_Int(262145 + 24135, 1); // South American Imports (14400000)
                                    break;
                                case 1:
                                    Activate();
                                    _SG_Int(262145 + 24136, 1); // Pharmaceutical Research (7200000)
                                    break;
                                case 2:
                                    Activate();
                                    _SG_Int(262145 + 24137, 1); // Organic Produce (2400000)
                                    break;
                                case 3:
                                    Activate();
                                    _SG_Int(262145 + 24138, 1); // Printing and Copying (1800000)
                                    break;
                                case 4:
                                    Activate();
                                    _SG_Int(262145 + 24139, 1); // Cash Creation (3600000)
                                    break;
                                case 5:
                                    Activate();
                                    _SG_Int(262145 + 24134, 1); // Sporting Goods (4800000)
                                    break;
                                case 6:
                                    Activate();
                                    _SG_Int(262145 + 24140, 1); // Cargo and Shipments (8400000)
                                    break;
                            }
                            break;
                        case 2:
                            switch (menuItem)
                            {
                                case 0:
                                    Activate();
                                    carSpawn("zr380", 0);
                                    break;
                                case 1:
                                    Activate();
                                    carSpawn("Deluxo", 0);
                                    break;
                                case 2:
                                    Activate();
                                    carSpawn("oppressor2", 0);
                                    break;
                                case 3:
                                    Activate();
                                    carSpawn("vigilante", 0);
                                    break;
                                case 4:
                                    Activate();
                                    carSpawn("Toreador", 0);
                                    break;
                                case 5:
                                    Activate();
                                    carSpawn("brutus2", 0);
                                    break;
                                case 6:
                                    Activate();
                                    carSpawn("dominator5", 0);
                                    break;
                                case 7:
                                    Activate();
                                    carSpawn("imperator2", 0);
                                    break;
                            }
                            break;
                    }
                    break;
            }

        }

        public static void runSingleItem()
        {
            Console.WriteLine("Command to run backward: " + LastMenuMainLvl + " " + LastMenuLvl + " " + LastMenuItm);
            int oldMenuItm = LastMenuItm;
            listboxFill(LastMenuMainLvl, LastMenuLvl);
            listBx.SelectedIndex = oldMenuItm;
        }

        private static void mainlistup()
        {
            if (listBx.SelectedIndex > 0)
            {
                listBx.SelectedIndex = listBx.SelectedIndex - 1;
                menuItm = listBx.SelectedIndex;
            }
        }

        private static void mainlistdown()
        {
            if (listBx.SelectedIndex < listBx.Items.Count - 1)
            {
                listBx.SelectedIndex = listBx.SelectedIndex + 1;
                menuItm = listBx.SelectedIndex;
            }
        }

        public static void showHideOverlay()
        {
            if (!isHidden)
            {
                isHidden = true;
                ShowWindow(formHandle, SW_HIDE);
            }
            else
            {
                isHidden = false;
                ShowWindow(formHandle, 1);
            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if ((Keys)vkCode == Keys.F5)
                {
                    showHideOverlay();
                }
                else if ((Keys)vkCode == Keys.NumPad0)
                {
                    if (!isHidden)
                    {
                        if (menuMainLvl <= 0)
                        {
                            isHidden = true;
                            ShowWindow(formHandle, SW_HIDE);
                        }
                        else
                        {
                            runSingleItem();
                        }
                    }
                }
                else if ((Keys)vkCode == Keys.NumPad5)
                {
                    runitem(menuMainLvl, menuLvl, listBx.SelectedIndex);
                }
                else if ((Keys)vkCode == Keys.Up || (Keys)vkCode == Keys.Down || (Keys)vkCode == Keys.Left || (Keys)vkCode == Keys.Right)
                {
                    if (!isHidden)
                    {
                        return (IntPtr)1;
                    }
                }
                else if ((Keys)vkCode == Keys.NumPad2)
                {
                    if (!isHidden)
                    {
                        mainlistdown();
                    }
                }
                else if ((Keys)vkCode == Keys.NumPad8)
                {
                    if (!isHidden)
                    {
                        mainlistup();
                    }
                }
                else if ((Keys)vkCode == Keys.Delete)
                {
                    if (!isHidden)
                    {
                        Quit();
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public static void LoadSession(int id)
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

        public static void getLuckyWheelPrice(int id)
        {
            string script = "casino_lucky_wheel";
            int Index = 274 + 14;
            long scriptAddr = GetLocalScript(script);
            if (scriptAddr > 0 && id > 0)
            {
                long scriptAddr2 = scriptAddr + (8 * Index);
                Console.WriteLine(scriptAddr2);
                int scriptInt = Mem.ReadInt(scriptAddr2, null);
                Console.WriteLine(scriptInt);
                Mem.writeInt(scriptAddr2, null, id);
            }
        }
        public static void setRPMultipler(float m)
        {
            _SG_Float(262145 + 1, m);
        }

        public static void setREPMultipler(float m)
        {
            _SG_Float(262145 + 31278, m); // Street Race
            _SG_Float(262145 + 31279, m); // Pursuit Race
            _SG_Float(262145 + 31280, m); // Scramble
            _SG_Float(262145 + 31281, m); // Head 2 Head
            _SG_Float(262145 + 31283, m); // Car Meet
            _SG_Float(262145 + 31284, m); // Test Track
        }

        #region Teleport part
        private static void Teleport(Location l)
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

        private static void teleportBlip(int[] ID, int[] color, int height = 0)
        {
            Location tmpLoc = getBlipCoords(ID, color, height);
            Location returnLoc = new Location
            {
                x = tmpLoc.x,
                y = tmpLoc.y,
                z = tmpLoc.z
            };
            if (returnLoc.x != 0 && returnLoc.y != 0)
            {
                Teleport(returnLoc);
            }
            else
            {
                Console.WriteLine("No TP, wrong coords (x, y).");
            }
        }

        private static Location getBlipCoords(int[] id, int[] color = null, int height = 0)
        {
            float zOffset = 0;
            Location tempLocation = new() { };
            for (int i = 2000; i > 1; i--)
            {
                long blip = settings.BlipPTR + (i * 8);
                int blipId = Mem.ReadInt(blip, new int[] { 0x40 });
                int blipColor = Mem.ReadInt(blip, new int[] { 0x48 });
                if (id != null && id.Contains(blipId))
                {
                    zOffset = (float)(Math.Round(Math.Pow(i, -0.2), 1) * height);
                    tempLocation = new Location
                    {
                        x = Mem.ReadFloat(blip, new int[] { 0x10 }),
                        y = Mem.ReadFloat(blip, new int[] { 0x14 }),
                        z = Mem.ReadFloat(blip, new int[] { 0x18 })
                    };

                    if (color != null && color.Contains(blipColor))
                    {
                        tempLocation = new Location
                        {
                            x = Mem.ReadFloat(blip, new int[] { 0x10 }),
                            y = Mem.ReadFloat(blip, new int[] { 0x14 }),
                            z = Mem.ReadFloat(blip, new int[] { 0x18 })
                        };
                    }
                }
            }
            if (tempLocation.z == 20)
            {
                tempLocation.z = -255;
            }
            tempLocation.z = tempLocation.z + zOffset;
            if (tempLocation.x > 0) { tempLocation.x = (float)Math.Round(tempLocation.x, 3); }
            if (tempLocation.y > 0) { tempLocation.y = (float)Math.Round(tempLocation.y, 3); }
            if (tempLocation.z > 0) { tempLocation.y = (float)Math.Round(tempLocation.z, 3); }
            Console.WriteLine("New location: " + tempLocation.x + ", " + tempLocation.y + ", " + tempLocation.z);
            return new Location { x = tempLocation.x, y = tempLocation.y, z = tempLocation.z };
        }

        public static float PlayerX
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCNavigation, offsets.oPositionX }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCNavigation, offsets.oPositionX }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oVisualX }, value);
            }
        }
        public static float PlayerY
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCNavigation, offsets.oPositionY }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCNavigation, offsets.oPositionY }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oVisualY }, value);
            }
        }
        public static float PlayerZ
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCNavigation, offsets.oPositionZ }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCNavigation, offsets.oPositionZ }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.oVisualZ }, value);
            }
        }

        public static float CarX
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionX }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionX }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.oVisualX }, value);
            }
        }
        public static float CarY
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionY }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionY }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.oVisualY }, value);
            }
        }
        public static float CarZ
        {
            get { return Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionZ }); }
            set
            {
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.pCNavigation, offsets.oPositionZ }, value);
                Mem.Write(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCVehicle, offsets.oVisualZ }, value);
            }
        }
        #endregion

        #region Global Addresses function
        public static long GA(long Index)
        {
            long p = settings.GlobalPTR + (8 * (Index >> 0x12 & 0x3F));
            long p_ga = Mem.ReadPointer(p, null);
            long p_ga_final = p_ga + (8 * (Index & 0x3FFFF));
            return p_ga_final;
        }
        public static long _GG_Int(int Index)
        {
            return Mem.ReadInt(GA(Index), null);
        }
        public static float _GG_Float(int Index)
        {
            return Mem.ReadFloat(GA(Index), null);
        }
        public static string _GG_String(int Index, int size)
        {
            return Mem.ReadString(GA(Index), null, size);
        }
        public static void _SG_Int(int Index, int value)
        {
            Mem.writeInt(GA(Index), null, value);
        }
        public static void _SG_UInt(int Index, uint value)
        {
            Mem.writeUInt(GA(Index), null, value);
        }
        public static void _SG_Float(int Index, float value)
        {
            Mem.writeFloat(GA(Index), null, value);
        }
        public static void _SG_String(int Index, string value)
        {
            Mem.Write(GA(Index), null, value);
        }

        public static void setStat(string stat, int value)
        {
            long oldhash = _GG_Int(1655444 + 4);
            long oldvalue = _GG_Int(1020252 + 5526);
            _SG_Int(1655444 + 4, (int)JOAAT.GetHashKey(stat));
            _SG_Int(1020252 + 5526, value);
            _SG_Int(1644209 + 1139, -1);
            Thread.Sleep(1000);
            _SG_Int(1655444 + 4, (int)oldhash);
            _SG_Int(1020252 + 5526, (int)oldvalue);
        }
        public static long GetLocalScript(string name)
        {
            int size = name.Length;
            for (int i = 0; i <= 52; i++)
            {
                long lc_p = Mem.ReadPointer(settings.LocalScriptsPTR, new int[] { (i * 8), 0xB0 });
                string lc_n = Mem.ReadString(settings.LocalScriptsPTR, new int[] { (i * 8), 0xD0 }, size);
                if (lc_n == name)
                {
                    i = 53;
                    Console.WriteLine(lc_p);
                    return (lc_p);
                }
            }
            return 0;
        }
        #endregion

        public static void carSpawn(string Hash, int pegasus = 0)
        {
            string model = Hash.ToLower();
            float ped_heading = Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCNavigation, offsets.oHeading });
            float ped_heading2 = Mem.ReadFloat(settings.WorldPTR, new int[] { offsets.pCPed, offsets.pCNavigation, offsets.oHeading2 });
            Console.WriteLine(ped_heading + " " + ped_heading2);
            float spawner_x = PlayerX;
            float spawner_y = PlayerY;
            float spawner_z = PlayerZ;
            spawner_x = spawner_x - (ped_heading2 * 5f);
            spawner_y = spawner_y + (ped_heading * 5f);
            spawner_z = spawner_z + 0.5f;
            _SG_Float(offsets.oVMCreate + 7 + 0, spawner_x);
            _SG_Float(offsets.oVMCreate + 7 + 1, spawner_y);
            _SG_Float(offsets.oVMCreate + 7 + 2, spawner_z);
            _SG_Int(offsets.oVMCreate + 27 + 66, (int)JOAAT.GetHashKey(Hash));
            _SG_Int(offsets.oVMCreate + 27 + 28, 1); // Weaponised ownerflag
            _SG_Int(offsets.oVMCreate + 27 + 60, 1);
            _SG_Int(offsets.oVMCreate + 27 + 95, 14); // Ownerflag
            _SG_Int(offsets.oVMCreate + 27 + 94, 2); // Personal car ownerflag
            _SG_Int(offsets.oVMCreate + 5, 1); // SET('i', CarSpawn + 0x1168, 1)--can spawn flag must be odd
            _SG_Int(offsets.oVMCreate + 2, 1); // SET('i', CarSpawn + 0x1180, 1)--spawn toggle gets reset to 0 on car spawn
            _SG_Int(offsets.oVMCreate + 3, pegasus);
            _SG_Int(offsets.oVMCreate + 27 + 74, 1); // Red Neon Amount 1-255 100%-0%
            _SG_Int(offsets.oVMCreate + 27 + 75, 1); // Green Neon Amount 1-255 100%-0%
            _SG_Int(offsets.oVMCreate + 27 + 76, 0); // Blue Neon Amount 1-255 100%-0%
            _SG_UInt(offsets.oVMCreate + 27 + 60, 4030726305); // landinggear / vehstate
            _SG_Int(offsets.oVMCreate + 27 + 5, -1); // default paintjob primary -1 auto 120
            _SG_Int(offsets.oVMCreate + 27 + 6, -1); // default paintjob secondary -1 auto 120
            _SG_Int(offsets.oVMCreate + 27 + 7, -1);
            _SG_Int(offsets.oVMCreate + 27 + 8, -1);
            _SG_Int(offsets.oVMCreate + 27 + 19, 4);
            _SG_Int(offsets.oVMCreate + 27 + 21, 4); // Engine(0 - 3)
            _SG_Int(offsets.oVMCreate + 27 + 22, 3);
            _SG_Int(offsets.oVMCreate + 27 + 23, 3); // Transmission(0 - 9)
            _SG_Int(offsets.oVMCreate + 27 + 24, 58);
            _SG_Int(offsets.oVMCreate + 27 + 26, 5); // Armor(0 - 18)
            _SG_Int(offsets.oVMCreate + 27 + 27, 1); // Turbo(0 - 1)
            _SG_Int(offsets.oVMCreate + 27 + 65, 2); // Window tint 0 - 6
            _SG_Int(offsets.oVMCreate + 27 + 69, -1); // Wheel type
            _SG_Int(offsets.oVMCreate + 27 + 33, -1); // Wheel Selection
            _SG_Int(offsets.oVMCreate + 27 + 25, 8); // Suspension(0 - 13)
            _SG_Int(offsets.oVMCreate + 27 + 19, -1);
            Mem.writeInt(GA(offsets.oVMCreate + 27 + 77) + 1, null, 2); // 2:bulletproof 0:false

            int weapon1 = 2;
            int weapon2 = 1;

            if (model == "oppressor2")
            {
                weapon1 = 2;
            }
            else if (model == "apc")
            {
                weapon1 = -1;
            }
            else if (model == "deluxo")
            {
                weapon1 = -1;
            }
            else if (model == "bombushka")
            {
                weapon1 = 1;
            }
            else if (model == "tampa3")
            {
                weapon1 = 3;
            }
            else if (model == "insurgent3")
            {
                weapon1 = 3;
            }
            else if (model == "halftrack")
            {
                weapon1 = 3;
            }
            else if (model == "barrage")
            {
                weapon1 = 30;
            }
            _SG_Int(offsets.oVMCreate + 27 + 15, weapon1); // primary weapon
            _SG_Int(offsets.oVMCreate + 27 + 20, weapon2); // primary weapon
            // _SG_Int(offsets.oVMCreate + 27 + 1, "FCK4FD"); // License plate
        }
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.Manual,
                Location = new Point(100, 100)
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            prompt.MaximizeBox = false;
            prompt.MinimizeBox = false;
            prompt.TopMost = true;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

    }
    struct Location { public float x, y, z; }
}
