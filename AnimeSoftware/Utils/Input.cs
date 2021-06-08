using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AnimeSoftware.Utils
{
    public static class Input
    {
        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(Keys vKey);

        private static bool[] KeyStates = new bool[256];
        public static bool KeyPressed(int i)
        {
            if (((GetAsyncKeyState(i) & 0x8000) != 0) != KeyStates[i])
                return KeyStates[i] = !KeyStates[i];
            else
                return false;
        }

        public static bool KeyPressed(Keys key)
        {
            return KeyPressed((int) key);
        }

        public static bool KeyDown(int i)
        {
            return ((GetAsyncKeyState(i) & 0x8000) != 0);
        }

        public static bool KeyDown(Keys key)
        {
            return KeyDown((int) key);
        }

        public static int[] GetScreenSize()
        {
            return new int[] {1920,1080};
        }
    }
}