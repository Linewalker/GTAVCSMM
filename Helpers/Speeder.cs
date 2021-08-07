using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GTAVCSMM.Helpers
{

    public static class Speeder
    {
        public static bool Suspend(int process)
        {
            try
            {
                // Obtain process instance.
                Process proc = Process.GetProcessById(process);

                if (proc == null)
                    return false;

                // Obtain thread handle.
                ProcessThread thread = proc.Threads[0];
                IntPtr lpThreadHandle = NativeAPI.OpenThread(NativeAPI.ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (lpThreadHandle == IntPtr.Zero)
                    return false;

                NativeAPI.SuspendThread(lpThreadHandle);
                NativeAPI.CloseHandle(lpThreadHandle);

                return true;
            }
            catch { return false; }
        }

        public static bool Resume(int process, bool bForceResume = false)
        {
            try
            {
                // Obtain process instance.
                Process proc = Process.GetProcessById(process);
                if (proc == null)
                    return false;

                // Obtain thread handle.
                ProcessThread thread = proc.Threads[0];
                IntPtr lpThreadHandle = NativeAPI.OpenThread(NativeAPI.ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (lpThreadHandle == IntPtr.Zero)
                    return false;

                if (bForceResume == true)
                    while (NativeAPI.ResumeThread(lpThreadHandle) != 0) ;
                else
                    NativeAPI.ResumeThread(lpThreadHandle);

                NativeAPI.CloseHandle(lpThreadHandle);
                return true;
            }
            catch { return false; }
        }

        #region "Native Win32 API Definitions"
        /// <summary>
        /// Native Win32 API Definitions
        ///
        /// API needed in order to do the required tasks of
        /// loading, injection, and remote export calling.
        /// </summary>
        internal class NativeAPI
        {
            /// <summary>
            /// kernel32.CloseHandle
            /// </summary>
            /// <param name="hObject"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CloseHandle(
                IntPtr hObject
                );

            /// <summary>
            /// kernel32.OpenThread
            /// </summary>
            /// <param name="dwDesiredAccess"></param>
            /// <param name="bInheritHandle"></param>
            /// <param name="dwThreadId"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenThread(
                ThreadAccess dwDesiredAccess,
                bool bInheritHandle,
                uint dwThreadId
                );


            /// <summary>
            /// kernel32.ResumeThread
            /// </summary>
            /// <param name="hThread"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll")]
            public static extern uint ResumeThread(
                IntPtr hThread
                );

            /// <summary>
            /// kernel32.SuspendThread
            /// </summary>
            /// <param name="hThread"></param>
            /// <returns></returns>
            [DllImport("kernel32.dll")]
            public static extern uint SuspendThread(
                IntPtr hThread
                );

            /// <summary>
            /// Thread access flags.
            /// </summary>
            [Flags]
            public enum ThreadAccess : int
            {
                TERMINATE = (0x0001),
                SUSPEND_RESUME = (0x0002),
                GET_CONTEXT = (0x0008),
                SET_CONTEXT = (0x0010),
                SET_INFORMATION = (0x0020),
                QUERY_INFORMATION = (0x0040),
                SET_THREAD_TOKEN = (0x0080),
                IMPERSONATE = (0x0100),
                DIRECT_IMPERSONATION = (0x0200)
            }
        }
        #endregion 
    }
}
