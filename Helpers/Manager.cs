using System;
using System.Runtime.InteropServices;

namespace GTAV_External_Trainer.Helpers
{
    class Manager
    {
        // READ FLAGS
        public static uint PROCESS_VM_READ = 0x0010;
        public static uint PROCESS_VM_WRITE = 0x0020;
        public static uint PROCESS_VM_OPERATION = 0x0008;
        public static uint PAGE_READWRITE = 0x0004;

        // WINDOW FLAGS
        public static uint WS_MAXIMIZE = 0x1000000;
        public static uint WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
        public static uint WS_BORDER = 0x800000;
        public static uint WS_VISIBLE = 0x10000000;
        public static int GWL_STYLE = (-16);

        // KEY PRESSED
        public const int KEY_PRESSED = 0x8000;

        //MOUSE BUTTONS
        public const int VK_LEFTBUTTON = 0x01;
        public const int VK_MIDBUTTON = 0x04;
        public const int VK_RIGHTBUTTON = 0X02;
        public const int VK_LBUTTON = VK_LEFTBUTTON;
        public const int VK_MBUTTON = VK_MIDBUTTON;
        public const int VK_RBUTTON = VK_RIGHTBUTTON;

        // KEYBOARD
        public const int VK_CANCEL = 0x03;
        public const int VK_BACKSPACE = 0x08;
        public const int VK_TAB = 0x09;
        public const int VK_CLEAR = 0x0C;
        public const int VK_ENTER = 0x0D;
        public const int VK_SHIFT = 0x10;     // Any of the 2 shift keys
        public const int VK_LSHIFT = 0xA0;    // Left Shift Key
        public const int VK_RSHIFT = 0xA1;    // Right shift key
        public const int VK_CTRL = 0x11;      // Any of the 2 CTRL keys
        public const int VK_LCTRL = 0xA2;     // Left CTRL key
        public const int VK_RCTRL = 0xA3;     // Right CTRL key
        public const int VK_ALT = 0x12;       // Any of the 2 ALT keys
        public const int VK_LMENU = 0xA4;     // Left ALT key
        public const int VK_RMENU = 0xA5;     // Right ALT key
        public const int VK_CAPSLOCK = 0x14;
        public const int VK_ESCAPE = 0x1B;
        public const int VK_SPACE = 0x20;
        public const int VK_LEFT = 0x25;
        public const int VK_UP = 0x26;
        public const int VK_RIGHT = 0x27;
        public const int VK_DOWN = 0x28;
        public const int VK_SELECT = 0x29;
        public const int VK_INSERT = 0x2D;
        public const int VK_DELETE = 0x2E;
        public const int VK_HELP = 0x2F;

        // KEYBOARD NUMBERS
        public const int VK_KEYB0 = 0x30;
        public const int VK_KEYB1 = 0x31;
        public const int VK_KEYB2 = 0x32;
        public const int VK_KEYB3 = 0x33;
        public const int VK_KEYB4 = 0x34;
        public const int VK_KEYB5 = 0x35;
        public const int VK_KEYB6 = 0x36;
        public const int VK_KEYB7 = 0x37;
        public const int VK_KEYB8 = 0x38;
        public const int VK_KEYB9 = 0x39;

        // NUMPAD
        public const int VK_NUMLOCK = 0x90;
        public const int VK_NUMPAD0 = 0x60;
        public const int VK_NUMPAD1 = 0x61;
        public const int VK_NUMPAD2 = 0x62;
        public const int VK_NUMPAD3 = 0x63;
        public const int VK_NUMPAD4 = 0x64;
        public const int VK_NUMPAD5 = 0x65;
        public const int VK_NUMPAD6 = 0x66;
        public const int VK_NUMPAD7 = 0x67;
        public const int VK_NUMPAD8 = 0x68;
        public const int VK_NUMPAD9 = 0x69;
        public const int VK_MULTIPLY = 0x6A;
        public const int VK_ADD = 0x6B;
        public const int VK_SEPARATOR = 0x6C;
        public const int VK_SUBTRACT = 0x6D;
        public const int VK_DECIMAL = 0x6E;
        public const int VK_DIVIDE = 0x6F;

        // FUNCTION KEYS
        public const int VK_F1 = 0x70;
        public const int VK_F2 = 0x71;
        public const int VK_F3 = 0x72;
        public const int VK_F4 = 0x73;
        public const int VK_F5 = 0x74;
        public const int VK_F6 = 0x75;
        public const int VK_F7 = 0x76;
        public const int VK_F8 = 0x77;
        public const int VK_F9 = 0x78;
        public const int VK_F10 = 0x79;
        public const int VK_F11 = 0x7A;
        public const int VK_F12 = 0x7B;
        public const int VK_F13 = 0x7C;
        public const int VK_F14 = 0x7D;
        public const int VK_F15 = 0x7E;
        public const int VK_F16 = 0x7F;
        public const int VK_F17 = 0x80;
        public const int VK_F18 = 0x81;
        public const int VK_F19 = 0x82;
        public const int VK_F20 = 0x83;
        public const int VK_F21 = 0x84;
        public const int VK_F22 = 0x85;
        public const int VK_F23 = 0x86;
        public const int VK_F24 = 0x87;

        // LETTERS
        public const int VK_A = 0x41;
        public const int VK_B = 0x42;
        public const int VK_C = 0x43;
        public const int VK_D = 0x44;
        public const int VK_E = 0x45;
        public const int VK_F = 0x46;
        public const int VK_G = 0x47;
        public const int VK_H = 0x48;
        public const int VK_I = 0x49;
        public const int VK_J = 0x4A;
        public const int VK_K = 0x4B;
        public const int VK_L = 0x4C;
        public const int VK_M = 0x4D;
        public const int VK_N = 0x4E;
        public const int VK_O = 0x4F;
        public const int VK_P = 0x50;
        public const int VK_Q = 0x51;
        public const int VK_R = 0x52;
        public const int VK_S = 0x53;
        public const int VK_T = 0x54;
        public const int VK_U = 0x55;
        public const int VK_V = 0x56;
        public const int VK_W = 0x57;
        public const int VK_X = 0x58;
        public const int VK_Y = 0x59;
        public const int VK_Z = 0x5A;

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int KeyStates);

        //These will be needed for function hooking (infinite ammo)
        //Expect an update with that included
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwAccess, bool inherit, int pid);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UInt32 dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, [In, Out] byte[] lpBuffer, UInt64 dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, [In, Out] byte[] lpBuffer, UInt64 dwSize, out IntPtr lpNumberOfBytesWritten);

    }
}
