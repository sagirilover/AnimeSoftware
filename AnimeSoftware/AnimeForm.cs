using AnimeSoftware.Hacks;
using AnimeSoftware.Injections;
using AnimeSoftware.Objects;
using AnimeSoftware.Offsets;
using AnimeSoftware.Utils;
using AnimeSoftware.Offsets;
using Opulos.Core.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AnimeSoftware.Hack.Features;
using AnimeSoftware.Hack.Models;

namespace AnimeSoftware
{
    public partial class AnimeForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public AnimeForm()
        {
            InitializeComponent();
        }

        private unsafe void Form1_Load(object sender, EventArgs e)
        {
            while (!Init())
            {
                var result =
                    MessageBox.Show(
                        "The game is not open.\nAlso make sure that you open the application as administrator.",
                        "Can't attach to process", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                switch (result)
                {
                    case DialogResult.Retry:
                        break;
                    case DialogResult.Cancel:
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }

                Thread.Sleep(100);
            }

            CalcedOffsets.Init();
            Signatures.Init();
            Netvars.Init();

            Properties.Settings.Default.namestealer = false;
            Properties.Settings.Default.Save();
            Start();
        }

        public static void Start()
        {
            var blockbotThread = new Thread(new ThreadStart(BlockBot.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            blockbotThread.Start();

            var bhopThread = new Thread(new ThreadStart(Bhop.Run))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            bhopThread.Start();

            var doorspamThread = new Thread(new ThreadStart(DoorSpam.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            doorspamThread.Start();

            var perfectnadeThread = new Thread(new ThreadStart(PerfectNade.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            perfectnadeThread.Start();

            var visualsThread = new Thread(new ThreadStart(Visuals.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            visualsThread.Start();
            
            var aimbotThread = new Thread(new ThreadStart(Aimbot.Run))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            aimbotThread.Start();
        }

        public void UpdateNickBox()
        {
            nickBox.Rows.Clear();
            if (!Engine.InGame)
                return;

            var lp = new LocalPlayer();
            if (lp.Ptr == IntPtr.Zero)
                return;

            nickBox.Rows.Add(lp.Index, lp.Name);

            foreach (var x in EntityList.GetPlayers().Where(p => p.Team == lp.Team && !p.Dormant))
            {
                var teamColor = Color.Blue;
                Color statusColor;
                if (x.Health <= 0)
                    statusColor = Color.YellowGreen;
                else
                    statusColor = Color.Green;

                var ind = nickBox.Rows.Add(x.Index, x.Name, !(x.Health <= 0));
                nickBox.Rows[ind].Cells["nameColumn"].Style.ForeColor = teamColor;
                nickBox.Rows[ind].Cells["aliveColumn"].Style.ForeColor = statusColor;
            }

            foreach (var x in EntityList.GetPlayers().Where(p => p.Team != lp.Team && !p.Dormant))
            {
                var teamColor = Color.Red;
                Color statusColor;
                if (x.Health <= 0)
                    statusColor = Color.YellowGreen;
                else
                    statusColor = Color.Green;

                var ind = nickBox.Rows.Add(x.Index, x.Name, !(x.Health <= 0));
                nickBox.Rows[ind].Cells["nameColumn"].Style.ForeColor = teamColor;
                nickBox.Rows[ind].Cells["aliveColumn"].Style.ForeColor = statusColor;
            }
        }

        public static bool Init()
        {
            Checks.CheckVersion();
            Log.Debug("Update checked...");

            if (!Memory.OpenProcess("csgo"))
                return false;
            Log.Debug("Process opened...");

            Thread.Sleep(100);
            if (!Memory.ProcessHandle())
                return false;
            Log.Debug("Process handled...");

            Thread.Sleep(100);
            if (!Memory.GetModules())
                return false;
            Log.Debug("Module get succses...");

            return true;
        }


        private void refreshButton_Click(object sender, EventArgs e)
        {
            UpdateNickBox();
        }


        private void changeButton_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(nickBox.Rows[nickBox.SelectedCells[0].RowIndex]
                .Cells[nickBox.Columns["idColumn"].Index].Value);

            ConVarManager.StealName(id);

            UpdateNickBox();
        }

        private void controlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WinAPI.ReleaseCapture();
                WinAPI.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void nickBox_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nickBox.Rows[e.RowIndex].Selected = true;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ConVarManager.ChangeName(LocalPlayer.GlobalName);
            UpdateNickBox();
        }

        private void AnimeForm_Shown(object sender, EventArgs e)
        {
            UpdateNickBox();

            InitForm();
            InitHotkey();
        }

        public void InitForm()
        {
            bhopCheckBox.Checked = Properties.Settings.Default.bhop;
            doorspammerCheckBox.Checked = Properties.Settings.Default.doorspammer;
            blockbotCheckBox.Checked = Properties.Settings.Default.blockbot;
            aimbotCheckBox.Checked = Properties.Settings.Default.aimbot;
            rscCheckBox.Checked = Properties.Settings.Default.rsc;
            ffCheckBox.Checked = Properties.Settings.Default.friendlyfire;
            perfectnadeCheckBox.Checked = Properties.Settings.Default.perfectnade;
            fovTrackBar.Value = (int) (Properties.Settings.Default.fov * 100);
            fovLabel.Text = Properties.Settings.Default.fov.ToString();
            smoothTrackBar.Value = (int) (Properties.Settings.Default.smooth * 100);
            smoothLabel.Text = Properties.Settings.Default.smooth.ToString();
            Properties.Settings.Default.velName = false;
            Properties.Settings.Default.Save();
            trackBar1.Value = (int) (BlockBot.trajFactor * 100);
            label8.Text = BlockBot.trajFactor.ToString();
            trackBar2.Value = (int) (BlockBot.distanceFactor * 10);
            label7.Text = BlockBot.distanceFactor.ToString();
        }

        public void InitHotkey()
        {
            blockbotButton.Text = ((Keys) Properties.Hotkey.Default.blockbotKey).ToString();
            doorspammerButton.Text = ((Keys) Properties.Hotkey.Default.doorspammerKey).ToString();
        }

        private void bhopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.bhop = bhopCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void doorspammerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.doorspammer = doorspammerCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void blockbotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.blockbot = blockbotCheckBox.Checked;
            Properties.Settings.Default.Save();
        }
        

        private void doorspammerButton_Click(object sender, EventArgs e)
        {
            doorspammerButton.Text = "Press key";
        }

        private void blockbotButton_Click(object sender, EventArgs e)
        {
            blockbotButton.Text = "Press key";
        }

        private void doorspammerButton_KeyUp(object sender, KeyEventArgs e)
        {
            Properties.Hotkey.Default.doorspammerKey = e.KeyValue;
            Properties.Hotkey.Default.Save();
            InitHotkey();
        }

        private void blockbotButton_KeyUp(object sender, KeyEventArgs e)
        {
            Properties.Hotkey.Default.blockbotKey = e.KeyValue;
            Properties.Hotkey.Default.Save();
            InitHotkey();
        }

        private void fullrefreshButton_Click(object sender, EventArgs e)
        {
            LocalPlayer.GlobalName = new LocalPlayer().Name;
            UpdateNickBox();
        }

        private void setupButton_Click(object sender, EventArgs e)
        {
            ConVarManager.ChangeName(customnameTextBox.Text);
            LocalPlayer.GlobalName = customnameTextBox.Text;
            UpdateNickBox();
        }

        private void nickBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!(e.Button == MouseButtons.Right))
                return;

            var currentMouseOverRow = nickBox.HitTest(e.X, e.Y).RowIndex;

            if (currentMouseOverRow >= 0) nickBoxContextMenuStrip.Show(Cursor.Position);
        }

        private void nickBoxContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == stealNameToolStripMenuItem)
            {
                changeButton.PerformClick(); // switch doesnt work lol
                return;
            }

            if (e.ClickedItem == removeGlowToolStripMenuItem)
            {
                foreach (DataGridViewCell i in nickBox.SelectedCells)
                    if (i.ColumnIndex == nickBox.Columns["idColumn"].Index)
                        Visuals.ToGlow[Convert.ToInt32(i.Value)] = false;
            }

            if (e.ClickedItem == getByteNameToolStripMenuItem)
                Console.WriteLine(new Player(Convert.ToInt32(nickBox.SelectedRows[0].Cells[0].Value)).Name);
        }

        private void toGlowListChange(GlowColor glowColor)
        {
            var entityIndex = new List<int>();
            foreach (DataGridViewCell i in nickBox.SelectedCells)
            {
                if (i.ColumnIndex == nickBox.Columns["idColumn"].Index)
                {
                    var index = Convert.ToInt32(i.Value);
                    Visuals.Add(index, glowColor, new GlowSettings(true, false, false));
                }

                if (i.ColumnIndex == nickBox.Columns["glowColumn"].Index)
                {
                    i.Style.BackColor = glowColor.ToColor;
                }
            }

            nickBox.ClearSelection();
        }

        private void nickBox_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewCell x in nickBox.SelectedCells) nickBox.Rows[x.RowIndex].Selected = true;
        }

        private void setGlowToolStripMenuItem_DropDownItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            var glowColor = new GlowColor();
            if (e.ClickedItem == redToolStripMenuItem) glowColor = new GlowColor(Color.Red);
            if (e.ClickedItem == greenToolStripMenuItem) glowColor = new GlowColor(Color.Green);
            if (e.ClickedItem == blueToolStripMenuItem) glowColor = new GlowColor(Color.Blue);
            if (e.ClickedItem == customToolStripMenuItem)
            {
                var colorDialog = new AlphaColorDialog()
                {
                    FullOpen = true,
                    Color = Color.Gray
                };
                nickBoxContextMenuStrip.Hide();
                if (colorDialog.ShowDialog() == DialogResult.OK) glowColor = new GlowColor(colorDialog.Color);
            }

            toGlowListChange(glowColor);
        }

        private void rightspamButton_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.weaponspammer = rightspamButton.Checked;
            Properties.Settings.Default.Save();
            var weaponspammerThread = new Thread(new ThreadStart(WeaponSpammer.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            if (Properties.Settings.Default.weaponspammer)
                weaponspammerThread.Start();
        }

        private void fovTrackBar_Scroll(object sender, EventArgs e)
        {
            var fov = fovTrackBar.Value / 100f;
            fovLabel.Text = fov.ToString();
            Properties.Settings.Default.fov = fov;
            Properties.Settings.Default.Save();
        }
        

        private void smoothTrackBar_Scroll(object sender, EventArgs e)
        {
            var smooth = smoothTrackBar.Value / 100f;
            smoothLabel.Text = smooth.ToString();
            Properties.Settings.Default.smooth = smooth;
            Properties.Settings.Default.Save();
        }

        private void aimbotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.aimbot = aimbotCheckBox.Checked;
            
            Properties.Settings.Default.Save();
        }

        private void ffCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.friendlyfire = ffCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void rscCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.rsc = rscCheckBox.Checked;
            Properties.Settings.Default.Save();
        }



        private void perfectnadeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.perfectnade = perfectnadeCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void chatcleanerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.chatcleaner = chatcleanerCheckBox.Checked;
            Properties.Settings.Default.Save();

            var chatcleanerThread = new Thread(new ThreadStart(ChatSpammer.ChatCleaner))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            chatcleanerThread.Start();
        }
        
        private void clantagCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.clanTag = clantagCheckBox.Checked;
            if (Properties.Settings.Default.clanTag)
            {
                var clantagThread = new Thread(new ThreadStart(ClanTag.Default))
                {
                    Priority = ThreadPriority.Highest,
                    IsBackground = true
                };
                clantagThread.Start();
            }
        }

        private void velCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.velTag = velCheckBox.Checked;
            if (Properties.Settings.Default.velTag)
            {
                var veltagThread = new Thread(new ThreadStart(ClanTag.VelTag))
                {
                    Priority = ThreadPriority.Highest,
                    IsBackground = true
                };
                veltagThread.Start();
            }

            velnameCheckBox.Enabled = velCheckBox.Checked;
        }

        private void velnameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (velnameCheckBox.Checked)
                if (MessageBox.Show(
                        "Make sure, that you forced infinity name switching exploit in your other cheat.\nIn other way close this window.") !=
                    DialogResult.OK)
                    velnameCheckBox.Checked = false;
            Properties.Settings.Default.velName = velnameCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void clanButton_Click(object sender, EventArgs e)
        {
            ClanTag.Set(clanTextBox.Text);
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label8.Text = (trackBar1.Value / 100f).ToString();
            BlockBot.trajFactor = trackBar1.Value / 100f;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label7.Text = (trackBar2.Value / 10f).ToString();
            BlockBot.distanceFactor = trackBar2.Value / 10f;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Client.ViewmodelX = trackBar3.Value / 100f;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            Client.ViewmodelY = trackBar4.Value / 100f;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            Client.ViewmodelZ = trackBar5.Value / 100f;
        }
    }
}