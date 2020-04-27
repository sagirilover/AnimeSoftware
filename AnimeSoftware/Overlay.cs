using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimeSoftware.Injections;
using AnimeSoftware.Objects;

namespace AnimeSoftware
{
    public partial class Overlay : Form
    {
        public Overlay()
        {
            InitializeComponent();
        }

        public static Graphics g = null;
        public static List<float> pointsLine = new List<float>();
        public const int maxDisplayVel = 500;
        public static int lineWidth = 0;
        public static int maxHeight = 0;
        public static float pointsPerVelocity = 0;
        private void Overlay_Load(object sender, EventArgs e)
        {
            int initialStyle = DllImport.GetWindowLong(this.Handle, -20);
            DllImport.SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            IntPtr hWnd = Process.GetProcessesByName("csgo")[0].MainWindowHandle;
            Rect rect = new Rect();
            DllImport.GetWindowRect(hWnd, ref rect);

            this.Width = 1000;
            this.Height = 500;
            this.Left = ((rect.right - rect.left) - this.Width) / 2;
            this.Top = ((rect.bottom - rect.top) - this.Height) / 2;

            lineWidth = (int)((0.9 * this.Width) - (0.1 * this.Width));
            maxHeight = (int)((0.9 * this.Height) - (0.1 * this.Height));
            pointsPerVelocity  = maxHeight / (float)maxDisplayVel;

            g = paintBox.CreateGraphics();
            pointsLine = new float[lineWidth].ToList(); 

            

        }

        private void paintBox_Paint(object sender, PaintEventArgs e)
        {
            
        }

        public static void UpdateLine(float velocity)
        {
            Console.WriteLine();
            pointsLine.RemoveAt(0);
            pointsLine.Add(GetSchedulePoint(velocity));
        }

        public static float GetSchedulePoint(float velocity)
        {

            if (velocity >= maxDisplayVel)
                return maxHeight;

            return pointsPerVelocity * velocity;
        }

        private void Overlay_Shown(object sender, EventArgs e)
        {
            while (true)
            {
                UpdateLine(LocalPlayer.Speed);

                Invalidate();
            }
        }

        private void paintBox_Paint_1(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Red);
            myPen.Width = 2;
            float offset = (float)0.1 * this.Width;

            for (int i = 0; i < lineWidth - 1; i++)
            {
                g.DrawLine(myPen, new PointF(offset + i, pointsLine[i]), new PointF(offset + i + 1, pointsLine[i + 1]));
            }
        }
    }
}
