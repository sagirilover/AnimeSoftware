using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

namespace Opulos.Core.UI
{
    public class AlphaColorDialog : ColorDialog
    {
        ///<summary>Event is fired after the color or alpha value are changed via any of the possible user-interface controls.</summary>
        public event EventHandler ColorChanged;

        private Color _color = Color.Black; // currently selected color
        private AlphaDialog dialogAlpha = null;
        private AlphaColorPanel panelAlpha = null;
        private Button btnAlpha = new Button {Text = "Alpha"};
        private IntPtr handle = IntPtr.Zero; // handle of this ColorDialog
        private IntPtr hWndRed = IntPtr.Zero; // handles to TextBoxes
        private IntPtr hWndGreen = IntPtr.Zero;
        private IntPtr hWndBlue = IntPtr.Zero;

        public AlphaColorDialog()
        {
            btnAlpha.Click += btnAlpha_Click;
        }

        ///<summary>The handle for the ColorDialog window.</summary>
        public IntPtr Handle => handle;

        private void btnAlpha_Click(object sender, EventArgs e)
        {
            if (dialogAlpha == null)
            {
                dialogAlpha = new AlphaDialog(this);
                panelAlpha = new AlphaColorPanel();
                panelAlpha.AlphaChanged += panelAlpha_AlphaChanged;
                dialogAlpha.Controls.Add(panelAlpha);
                dialogAlpha.Text = "Alpha";
                //dialogAlpha.StartPosition = FormStartPosition.CenterParent; // doesn't work
                dialogAlpha.StartPosition = FormStartPosition.Manual;
                dialogAlpha.ClientSize = panelAlpha.PreferredSize;
                var sz = dialogAlpha.Size;
                var r = new RECT();
                GetWindowRect(handle, ref r);
                dialogAlpha.Location = new Point(r.Left + (r.Right - r.Left - sz.Width) / 2,
                    r.Top + (r.Bottom - r.Top - sz.Height) / 2);
            }

            panelAlpha.Color = _color;

            if (!dialogAlpha.IsHandleCreated || !dialogAlpha.Visible)
            {
                dialogAlpha.Visible = false; // sometimes IsHandleCreated is reset, so Visible must be reset
                dialogAlpha.Show(new SimpleWindow {Handle = handle});
            }
            else
            {
                if (dialogAlpha.WindowState == FormWindowState.Minimized)
                    dialogAlpha.WindowState = FormWindowState.Normal;

                dialogAlpha.Activate();
                dialogAlpha.BringToFront();
                dialogAlpha.Focus();
            }
        }

        private void panelAlpha_AlphaChanged(object sender, EventArgs e)
        {
            SetColorInternal(panelAlpha.Color);
        }

        private static string GetWindowText(IntPtr hWnd)
        {
            var sb = new StringBuilder(256);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        private class SimpleWindow : IWin32Window
        {
            public IntPtr Handle { get; set; }
        }

        private static Bitmap ConvertToBitmap(IntPtr hWnd)
        {
            var r = new RECT();
            GetWindowRect(hWnd, ref r);
            var w = r.Right - r.Left;
            var h = r.Bottom - r.Top;

            var g = Graphics.FromHwnd(hWnd);
            var bmp = new Bitmap(w, h);
            var g2 = Graphics.FromImage(bmp);
            var g2_hdc = g2.GetHdc();
            var g_hdc = g.GetHdc();
            BitBlt(g2_hdc, 0, 0, w, h, g_hdc, 0, 0, SRC);
            g.ReleaseHdc(g_hdc);
            g2.ReleaseHdc(g2_hdc);
            g.Dispose();
            g2.Dispose();

            return bmp;
        }

        private struct IJL
        {
            public int i;
            public int j;
            public int len;

            public override string ToString()
            {
                return i + " " + j + " " + len;
            }
        }

        private static Color? FindSelectedColor(IntPtr hWnd)
        {
            // This method assumes there is a bounding rectangle around a color swatch.
            // The rectangle can be any color, but must be a single color. Since
            // the rectangle surrounds the swatch, it must have a run of pixels that
            // is longer than the run of pixels inside the swatch. Since it is
            // a rectangle, and we are scanning from top to bottom (left to right would also work),
            // then there must be exactly two runs that tie for longest. If two runs cannot
            // be found, then there is no bounding rectangle.

            var bmp = ConvertToBitmap(hWnd);
            var w = bmp.Width;
            var h = bmp.Height;
            var bg = bmp.GetPixel(0, 0);

            var ijl = new IJL();
            var ijl0 = new IJL();
            var ijl1 = new IJL();
            var k = 0;

            for (var i = 0; i < w; i++)
            {
                var lastColor = Color.Empty;
                for (var j = 0; j <= h; j++)
                {
                    var c = j == h ? Color.Empty : bmp.GetPixel(i, j);
                    if (c == lastColor)
                    {
                        ijl.len++;
                    }
                    else
                    {
                        if (ijl.len < h)
                            if (ijl.len > 1 && bg != lastColor)
                            {
                                if (ijl.len > ijl0.len)
                                {
                                    ijl0 = ijl;
                                    k = 0;
                                }
                                else if (ijl.len == ijl0.len)
                                {
                                    ijl1 = ijl;
                                    k++;
                                }
                            }

                        ijl = new IJL();
                        ijl.i = i;
                        ijl.j = j;
                        ijl.len = 1;
                        lastColor = c;
                    }
                }
            }

            if (k != 1)
            {
                bmp.Dispose();
                return null;
            }

            // k == 1 means there are exactly two runs of maximum length
            var x = ijl0.i + (ijl1.i - ijl0.i) / 2;
            var y = ijl0.j + ijl0.len / 2;
            var c1 = bmp.GetPixel(x, y);
            bmp.Dispose();
            return c1;
        }

        private Color GetColorInternal()
        {
            var a = panelAlpha != null ? panelAlpha.Alpha : 255;
            var _r = GetWindowText(hWndRed);
            if (_r.Length > 0)
            {
                // Define Custom Colors UI is visible.
                var r = int.Parse(_r);
                var g = int.Parse(GetWindowText(hWndGreen));
                var b = int.Parse(GetWindowText(hWndBlue));
                return Color.FromArgb(a, r, g, b);
            }
            else
            {
                // if the RGB text boxes aren't visible, then resort to trying to find
                // the selected color by looking for the solid line rectangle that indicates the
                // currently selected color.
                var c = FindSelectedColor(GetDlgItem(handle, 0x02d0)); // Basic colors
                if (!c.HasValue)
                    c = FindSelectedColor(GetDlgItem(handle, 0x02d1)); // Custom colors

                return c.HasValue ? Color.FromArgb(a, c.Value) : Color.FromArgb(a, Color.Black);
            }
        }

        private static bool AreEqual(Color c1, Color c2)
        {
            // Color.Black != (255, 0, 0, 0)
            return c1.A == c2.A && c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }

        private void SetColorInternal(Color c)
        {
            if (AreEqual(c, _color))
                return;

            _color = c;
            if (ColorChanged != null)
                ColorChanged(this, EventArgs.Empty);
        }

        public new Color Color
        {
            get => _color;

            set
            {
                SetColorInternal(value);
                if (panelAlpha != null)
                    panelAlpha.Alpha = value.A;

                base.Color = value;
            }
        }

        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            //System.Diagnostics.Debug.WriteLine((Opulos.Core.Win32.WM) msg);
            if (msg == WM_INITDIALOG)
            {
                var hWndOK = GetDlgItem(hWnd, 0x1); // 0x1 == OK button
                var rOK = new RECT();
                GetWindowRect(hWndOK, ref rOK);

                var hWndDefineCustomColors = GetDlgItem(hWnd, 0x02cf);
                var rDefineCustomColors = new RECT();
                GetWindowRect(hWndDefineCustomColors, ref rDefineCustomColors);

                var hWndCancel = GetDlgItem(hWnd, 0x2); // 0x2 == Cancel button
                var rCancel = new RECT();
                GetWindowRect(hWndCancel, ref rCancel);
                // Convert the cancel button's screen coordinates to client coordinates
                var pt = new POINT();
                pt.X = rCancel.Right;
                pt.Y = rCancel.Top;
                ScreenToClient(hWnd, ref pt);
                var hWndParent = GetParent(hWndCancel);
                var w = rCancel.Right - rCancel.Left;
                var h = rCancel.Bottom - rCancel.Top;
                var gap = rCancel.Left - rOK.Right;

                // the "Define Custom Colors >>" button is slightly less wide than the total width of the
                // OK, Cancel and Alpha buttons. Options:
                // 1) Increase the width of the define button so it right aligns with the alpha button
                // 2) Make the alpha button smaller in width
                // 3) Decrease the widths of all three button and decrease the gap between them.
                // Option 1 looks better than option 2. Didn't try option 3.
                if (rCancel.Right + gap + w > rDefineCustomColors.Right)
                {
                    // screen coordinates
                    var diff = rCancel.Right + gap + w - rDefineCustomColors.Right;
                    // Option 2: //w = w - diff;
                    // Option 1:
                    var w2 = rDefineCustomColors.Right - rDefineCustomColors.Left;
                    var h2 = rDefineCustomColors.Bottom - rDefineCustomColors.Top;
                    SetWindowPos(hWndDefineCustomColors, IntPtr.Zero, 0, 0, w2 + diff, h2, SWP_NOMOVE | SWP_NOZORDER);
                }

                var hWndAlpha = btnAlpha.Handle; // creates the handle
                btnAlpha.Bounds = new Rectangle(pt.X + gap, pt.Y, w, h);
                SetParent(hWndAlpha, hWndParent);
                var hWndFont = SendMessage(hWndCancel, WM_GETFONT, 0, 0);
                SendMessage(hWndAlpha, WM_SETFONT, hWndFont, 0);

                // Alternative way to create the Alpha button, but would have to handle the WM_NOTIFY messages for the button click events.
                //hWndAlpha = CreateWindowEx(0, "Button", "alphabutton", WS_VISIBLE | WS_CHILD | WS_TABSTOP, pt.X + gap, pt.Y, w, h, hWndParent, 0, 0, 0);
                //SetWindowText(hWndAlpha, "Alpha");
                //int hWndFont = SendMessage(hWndCancel, WM_GETFONT, 0, 0);
                //SendMessage(hWndAlpha, WM_SETFONT, hWndFont, 0);

                // calling ColorDialog.Color does not return the currently selected color until after the OK button
                // is clicked. So the values from the textboxes are used. To find the controlIDs, use Spy++.
                hWndRed = GetDlgItem(hWnd, 0x02c2); // red text box
                hWndGreen = GetDlgItem(hWnd, 0x02c3);
                hWndBlue = GetDlgItem(hWnd, 0x02c4);
            }
            else if (msg == WM_SHOWWINDOW)
            {
                //center the dialog on the parent window:
                var cr = new RECT();
                var r0 = new RECT();
                var parent = GetParent(hWnd);
                GetWindowRect(hWnd, ref r0);
                GetWindowRect(parent, ref cr);
                handle = hWnd;

                var x = cr.Left + (cr.Right - cr.Left - (r0.Right - r0.Left)) / 2;
                var y = cr.Top + (cr.Bottom - cr.Top - (r0.Bottom - r0.Top)) / 2;
                SetWindowPos(hWnd, IntPtr.Zero, x, y, 0, 0, SWP_NOZORDER | SWP_NOSIZE);
            }
            else if (msg == ACD_COLORCHANGED)
            {
                var c = GetColorInternal();
                SetColorInternal(c);
                if (panelAlpha != null)
                    panelAlpha.Color = c;
            }
            else if (msg == WM_COMMAND || msg == WM_CHAR || msg == WM_LBUTTONDOWN)
            {
                PostMessage(hWnd, ACD_COLORCHANGED, 0, 0);
            }

            return base.HookProc(hWnd, msg, wparam, lparam);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (btnAlpha != null)
                    btnAlpha.Dispose();

                if (dialogAlpha != null)
                    dialogAlpha.Dispose();

                btnAlpha = null;
                dialogAlpha = null;
            }
        }

        private class AlphaDialog : Form
        {
            private AlphaColorDialog AOwner;

            public AlphaDialog(AlphaColorDialog owner)
            {
                AOwner = owner;
                ShowIcon = false;
            }

            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    Hide();
                    SetForegroundWindow(AOwner.handle);
                }

                base.OnFormClosing(e);
            }
        }

        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy,
            uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("gdi32.dll", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool BitBlt(IntPtr pHdc, int iX, int iY, int iWidth, int iHeight, IntPtr pHdcSource,
            int iXSource, int iYSource, int dw);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int hMenu, int hInstance, int lpParam);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern bool SetWindowText(IntPtr hWnd, String lpString);

        //[DllImport("user32.dll")]
        //private static extern bool DestroyWindow(IntPtr hwnd);

        private const int ACD_COLORCHANGED = 0x0400; // custom message
        private const int SRC = 0xCC0020;

        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;

        private const int WM_INITDIALOG = 0x110;
        private const int WM_SETFONT = 0x0030;
        private const int WM_GETFONT = 0x0031;

        private const int WM_SHOWWINDOW = 0x18;

        //private const int WM_NOTIFY;
        // messages that indicate a color change:
        private const int WM_COMMAND = 0x111;
        private const int WM_CHAR = 0x102;
        private const int WM_LBUTTONDOWN = 0x201;

        //private const uint WS_VISIBLE = 0x10000000;
        //private const uint WS_CHILD = 0x40000000;
        //private const uint WS_TABSTOP = 0x00010000;
    }
}