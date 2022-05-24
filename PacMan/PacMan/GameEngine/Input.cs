using System.Runtime.InteropServices;

namespace GameEngine;

public static class Input
{
    private const int KEY_PRESSED = 0x8000;

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern short GetKeyState(int keyCode);

    public static bool IsKeyDown(Keys key) => Convert.ToBoolean(GetKeyState((int)key) & KEY_PRESSED);

    public static int GetHorizontalAxis()
    {
        if (IsKeyDown(Keys.A) || IsKeyDown(Keys.Left))
            return -1;
        else if (IsKeyDown(Keys.D) || IsKeyDown(Keys.Right))
            return 1;
        else
            return 0;
    }

    public static int GetVerticalAxis()
    {
        if (IsKeyDown(Keys.W) || IsKeyDown(Keys.Up))
            return -1;
        else if (IsKeyDown(Keys.S) || IsKeyDown(Keys.Down))
            return 1;
        else
            return 0;
    }
}
