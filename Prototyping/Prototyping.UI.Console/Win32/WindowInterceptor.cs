//********************************************************************************************
//Author: Sergey Stoyan, CliverSoft Co.
//        stoyan@cliversoft.com
//        sergey.stoyan@gmail.com
//        http://www.cliversoft.com
//        07 September 2006
//Copyright: (C) 2006, Sergey Stoyan
//********************************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Win32;
using System.Windows.Forms;

namespace CliverSoft
{    
    /// <summary>
    /// Intercept creation of window and get its HWND
    /// </summary>
    public class WindowInterceptor
    {
        IntPtr hook_id = IntPtr.Zero;

        Win32.Functions.HookProc cbf;

        /// <summary>
        /// Delegate to process intercepted window 
        /// </summary>
        /// <param name="hwnd"></param>
        public delegate void ProcessWindow(IntPtr hwnd);
        ProcessWindow process_window;  

        IntPtr owner_window = IntPtr.Zero;

        /// <summary>
        /// Start dialog box interception for the specified owner window
        /// </summary>
        /// <param name="owner_window">owner window, if it is IntPtr.Zero then any windows will be intercepted</param>
        /// <param name="process_window">custom delegate to process intercepted window. It should be a fast code in order to have no message stack overflow.</param>
        public WindowInterceptor(IntPtr owner_window, ProcessWindow process_window)
        {
            if (process_window == null)
                throw new Exception("process_window cannot be null!");
            this.process_window = process_window;

            this.owner_window = owner_window;

            cbf = new Win32.Functions.HookProc(dlg_box_hook_proc);
            //notice that win32 callback function must be a global variable within class to avoid disposing it!
            hook_id = Win32.Functions.SetWindowsHookEx(Win32.HookType.WH_CALLWNDPROCRET, cbf, IntPtr.Zero, Win32.Functions.GetCurrentThreadId());   
        }

        /// <summary>
        /// Stop intercepting. Should be called to calm unmanaged code correctly
        /// </summary>
        public void Stop()
        {
            if (hook_id != IntPtr.Zero)
                Win32.Functions.UnhookWindowsHookEx(hook_id);
            hook_id = IntPtr.Zero;
        }

        ~WindowInterceptor()
        {
            if (hook_id != IntPtr.Zero)
                Win32.Functions.UnhookWindowsHookEx(hook_id);
        }

        private IntPtr dlg_box_hook_proc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return Win32.Functions.CallNextHookEx(hook_id, nCode, wParam, lParam);

            Win32.CWPRETSTRUCT msg = (Win32.CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(Win32.CWPRETSTRUCT));

            //filter out create window events only
            if (msg.message == (uint)Win32.Messages.WM_SHOWWINDOW)
            {
                int h = Win32.Functions.GetWindow(msg.hwnd, Win32.Functions.GW_OWNER);
                //check if owner is that is specified
                if (owner_window == IntPtr.Zero || owner_window == new IntPtr(h))
                {
                    if (process_window != null)
                        process_window(msg.hwnd);
                }
            }

            return Win32.Functions.CallNextHookEx(hook_id, nCode, wParam, lParam);
        }
    }
}
