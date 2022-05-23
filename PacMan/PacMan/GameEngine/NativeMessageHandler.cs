using System.Runtime.InteropServices;

namespace GameEngine;

public static class NativeMessageHandler
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr hWnd;
        public uint Msg;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint Time;
        public Point Point;
    }

    [DllImport("user32.dll")]
    public static extern IntPtr DispatchMessage([In] ref Message message);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PeekMessage(out Message message, IntPtr hWnd, uint filterMin, uint filterMax, uint flags);

    [DllImport("user32.dll")]
    public static extern bool TranslateMessage([In] ref Message message);
}