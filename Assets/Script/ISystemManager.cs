using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ISystemManager {
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
    private static readonly IntPtr HWND_TOP = new IntPtr(0);
    private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

    const UInt32 SWP_NOSIZE = 0x0001;
    const UInt32 SWP_NOMOVE = 0x0002;
    const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    public void SetWindowPosAlways()
    {
        SetWindowPos(GetWindowHandle(), HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
    }

    public void setResolutions(int screen_width, int screen_hight, bool fullScreen)
    {
        Resolution resolutions = Screen.currentResolution;

        if (resolutions.width != screen_width &&
            resolutions.height != screen_hight)
        {
            Screen.SetResolution(screen_width, screen_hight, fullScreen);
        }
    }

    public static System.IntPtr GetWindowHandle()
    {
        return GetActiveWindow();
    }
}
