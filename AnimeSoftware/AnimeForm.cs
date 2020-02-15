using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using hazedumper;
using Opulos;
using Opulos.Core.UI;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            

            while (!Init())
            {
                DialogResult result = MessageBox.Show("The game is not open.\nAlso make sure that you open the application as administrator.", "Can't attach to process", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
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
            ScannedOffsets.Init();
            Properties.Settings.Default.namestealer = false;
            Properties.Settings.Default.Save();
            Start();
            
        }



        public static void Start()
        {
            Thread blockbotThread = new Thread(new ThreadStart(BlockBot.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            blockbotThread.Start();

            Thread bhopThread = new Thread(new ThreadStart(BHop.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            bhopThread.Start();

            Thread doorspamThread = new Thread(new ThreadStart(DoorSpam.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            doorspamThread.Start();

            Thread checksThread = new Thread(new ThreadStart(Checks.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            checksThread.Start();

            Thread namestealerThread = new Thread(new ThreadStart(NameStealer.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
                namestealerThread.Start();

            Thread perfectnameThread = new Thread(new ThreadStart(PerfectNade.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            perfectnameThread.Start();

            //Thread runboostThread = new Thread(new ThreadStart(RunboostBot.Start))
            //{
            //    Priority = ThreadPriority.Highest,                                       // disabled
            //    IsBackground = true,
            //};
            //runboostThread.Start();

            Thread visualsThread = new Thread(new ThreadStart(Visuals.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            visualsThread.Start();
        }



        public void UpdateNickBox()
        {
            nickBox.Rows.Clear();
            if (!LocalPlayer.InGame)
                return;
            nickBox.Rows.Add(LocalPlayer.Index, LocalPlayer.Name);
            foreach (Entity x in Entity.List().Where(x => x.isTeam))
            {
                Color teamColor = Color.Blue;
                Color statusColor;
                if (x.IsDead)
                    statusColor = Color.YellowGreen;
                else
                    statusColor = Color.Green;

                int ind = nickBox.Rows.Add(x.Index, x.Name2, !x.IsDead);
                nickBox.Rows[ind].Cells["nameColumn"].Style.ForeColor = teamColor;
                nickBox.Rows[ind].Cells["aliveColumn"].Style.ForeColor = statusColor;
            }
            foreach (Entity x in Entity.List().Where(x => !x.isTeam))
            {
                Color teamColor = Color.Red;
                Color statusColor;
                if (x.IsDead)
                    statusColor = Color.YellowGreen;
                else
                    statusColor = Color.Green;

                int ind = nickBox.Rows.Add(x.Index, x.Name2, !x.IsDead);
                nickBox.Rows[ind].Cells["nameColumn"].Style.ForeColor = teamColor;
                nickBox.Rows[ind].Cells["aliveColumn"].Style.ForeColor = statusColor;
            }
        }
        public static bool Init()
        {
            Checks.CheckVersion();
            if(Properties.Settings.Default.debug)
                Console.WriteLine("Update checked...");
            if (!Memory.OpenProcess("csgo"))
                return false;
            if (Properties.Settings.Default.debug)
                Console.WriteLine("Process opened...");
            Thread.Sleep(100);
            if (!Memory.ProcessHandle())
                return false;
            if (Properties.Settings.Default.debug)
                Console.WriteLine("Process handled...");
            Thread.Sleep(100);
            if (!Memory.GetModules())
                return false;
            if (Properties.Settings.Default.debug)
                Console.WriteLine("Module get succses...");
            return true;
        }



        private void refreshButton_Click(object sender, EventArgs e)
        {
            UpdateNickBox();
        }



        private void changeButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(nickBox.Rows[nickBox.SelectedCells[0].RowIndex].Cells[nickBox.Columns["idColumn"].Index].Value);

            ConVarManager.StealName(id);

            UpdateNickBox();
        }

        private void controlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DllImport.ReleaseCapture();
                DllImport.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
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
            ConVarManager.ChangeName(LocalPlayer.Name);
            UpdateNickBox();
        }

        private void kickButton_Click(object sender, EventArgs e)
        {
            // idk how get UserID lol
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
            namestealerCheckBox.Checked = Properties.Settings.Default.namestealer;
            autostrafeCheckBox.Checked = Properties.Settings.Default.autostrafe;
            autostrafeCheckBox.Enabled = bhopCheckBox.Checked;
            aimbotCheckBox.Checked = Properties.Settings.Default.aimbot;
            rscCheckBox.Checked = Properties.Settings.Default.rsc;
            ffCheckBox.Checked = Properties.Settings.Default.friendlyfire;
            perfectnadeCheckBox.Checked = Properties.Settings.Default.perfectnade;
            fovTrackBar.Value = (int)(Properties.Settings.Default.fov*100);
            fovLabel.Text = Properties.Settings.Default.fov.ToString();
            smoothTrackBar.Value = (int)(Properties.Settings.Default.smooth * 100);
            smoothLabel.Text = Properties.Settings.Default.smooth.ToString();
            chokeTrackBar.Value = Properties.Settings.Default.bhopChoke;
            if(Properties.Settings.Default.unlock)
                this.Width += 145;
            foreach (string x in Structs.Hitbox.Values)
                hitboxComboBox.Items.Add(x);
            if(Properties.Settings.Default.boneid!=0)
                hitboxComboBox.SelectedItem = Structs.Hitbox[Properties.Settings.Default.boneid];
        }
        public void InitHotkey()
        {
            blockbotButton.Text = ((Keys)Properties.Hotkey.Default.blockbotKey).ToString();
            doorspammerButton.Text = ((Keys)Properties.Hotkey.Default.doorspammerKey).ToString();
        }

        private void bhopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.bhop = bhopCheckBox.Checked;
            Properties.Settings.Default.Save();

            autostrafeCheckBox.Enabled = bhopCheckBox.Checked;
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

        private void namestealerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.namestealer = namestealerCheckBox.Checked;
            Properties.Settings.Default.Save();
            
        }
        private void autostrafeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.autostrafe = autostrafeCheckBox.Checked;
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
            label6.Focus();
        }

        private void blockbotButton_KeyUp(object sender, KeyEventArgs e)
        {
            Properties.Hotkey.Default.blockbotKey = e.KeyValue;
            Properties.Hotkey.Default.Save();
            InitHotkey();
            label6.Focus();
        }
        private void runboostbotButton_KeyUp(object sender, KeyEventArgs e)
        {
            Properties.Hotkey.Default.runboostbotKey = e.KeyValue;
            Properties.Hotkey.Default.Save();
            InitHotkey();
            label6.Focus();
        }
        private void fullrefreshButton_Click(object sender, EventArgs e)
        {
            Checks.PreLoad();
            UpdateNickBox();
        }

        private void setupButton_Click(object sender, EventArgs e)
        {
            ConVarManager.ChangeName(customnameTextBox.Text);
            LocalPlayer.Name = customnameTextBox.Text;
            UpdateNickBox();
        }

        private void nickBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (!(e.Button == MouseButtons.Right))
                return;

            int currentMouseOverRow = nickBox.HitTest(e.X, e.Y).RowIndex;

            if (currentMouseOverRow >= 0)
            {
                nickBoxContextMenuStrip.Show(Cursor.Position);
            }

        }
        private void nickBoxContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == stealNameToolStripMenuItem)
            {
                changeButton.PerformClick();                // switch doesnt work lol
                return;
            }
            if (e.ClickedItem == stealWhenYouFriendlyfireToolStripMenuItem)
            {
                NameStealer.fakenametargetid = (int)nickBox.Rows[nickBox.SelectedCells[0].RowIndex].Cells[nickBox.Columns["idColumn"].Index].Value;
            }
            if (e.ClickedItem == removeGlowToolStripMenuItem)
            {
                List<Entity> ToGlow = Visuals.ToGlow;
                List<int> entityIndex = new List<int>();
                foreach (DataGridViewCell i in nickBox.SelectedCells)
                {
                    if (i.ColumnIndex == nickBox.Columns["idColumn"].Index)
                        entityIndex.Add(Convert.ToInt32(i.Value));
                }
                foreach (Entity x in Entity.List())
                {
                    if (entityIndex.Contains(x.Index))
                    {
                        ToGlow.Remove(ToGlow.Find(j => j.Index == x.Index));
                    }
                }
                Visuals.ToGlow = ToGlow;
            }
        }
        private void toGlowListChange(GlowColor glowColor)
        {
            List<Entity> ToGlow = Visuals.ToGlow;
            List<int> entityIndex = new List<int>();
            foreach (DataGridViewCell i in nickBox.SelectedCells)
            {
                if (i.ColumnIndex == nickBox.Columns["idColumn"].Index)
                    entityIndex.Add(Convert.ToInt32(i.Value));
                if (i.ColumnIndex == nickBox.Columns["glowColumn"].Index)
                    i.Style.BackColor = glowColor.ToColor;

            }
            foreach (Entity x in Entity.List())
            {
                if (entityIndex.Contains(x.Index))
                {
                    ToGlow.Remove(ToGlow.Find(j => j.Index == x.Index));
                    x.Glowing = true;
                    x.glowColor = glowColor;
                    x.glowSettings = new GlowSettings(true, false, false);
                    ToGlow.Add(x);
                    continue;
                }
            }
            nickBox.ClearSelection();
            Visuals.ToGlow = ToGlow;
        }

        private void nickBox_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewCell x in nickBox.SelectedCells)
            {
                nickBox.Rows[x.RowIndex].Selected = true;
            }
        }

        private void setGlowToolStripMenuItem_DropDownItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            GlowColor glowColor = new GlowColor();
            if (e.ClickedItem == redToolStripMenuItem)
            {
                glowColor = new GlowColor(Color.Red);
            }
            if (e.ClickedItem == greenToolStripMenuItem)
            {
                glowColor = new GlowColor(Color.Green);
            }
            if (e.ClickedItem == blueToolStripMenuItem)
            {
                glowColor = new GlowColor(Color.Blue);
            }
            if (e.ClickedItem == customToolStripMenuItem)
            {

                AlphaColorDialog colorDialog = new AlphaColorDialog()
                {
                    FullOpen = true,
                    Color = Color.Gray,
                };
                nickBoxContextMenuStrip.Hide();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    glowColor = new GlowColor(colorDialog.Color);
                }
            }

            toGlowListChange(glowColor);
        }

        private void rightspamButton_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.weaponspammer = rightspamButton.Checked;
            Properties.Settings.Default.Save();
            Thread weaponspammerThread = new Thread(new ThreadStart(WeaponSpammer.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            if (Properties.Settings.Default.weaponspammer)
                weaponspammerThread.Start();
        }

        private void fovTrackBar_Scroll(object sender, EventArgs e)
        {
            float fov = fovTrackBar.Value / 100f;
            fovLabel.Text = fov.ToString();
            Properties.Settings.Default.fov = fov;
            Properties.Settings.Default.Save();
        }

        private void hitboxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.boneid = Structs.Hitbox.ElementAt(hitboxComboBox.SelectedIndex).Key;
            Properties.Settings.Default.Save();
        }

        private void smoothTrackBar_Scroll(object sender, EventArgs e)
        {
            float smooth = smoothTrackBar.Value / 100f;
            smoothLabel.Text = smooth.ToString();
            Properties.Settings.Default.smooth = smooth;
            Properties.Settings.Default.Save();
        }

        private void aimbotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.aimbot = aimbotCheckBox.Checked;
            Thread aimbotThread = new Thread(new ThreadStart(Aimbot.Start))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            aimbotThread.Start();
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

        private void unlockButton_Click(object sender, EventArgs e)
        {
            if (customnameTextBox.Text == "for some reason I needed extra functions that this cheat does not imply" || customnameTextBox.Text == "sagiri best girl")
            {
                if(!Properties.Settings.Default.unlock)
                    this.Width += 145;
                Properties.Settings.Default.unlock = true;
                Properties.Settings.Default.Save();
            }
            if (customnameTextBox.Text == "")
            {
                if (Properties.Settings.Default.unlock)
                    this.Width -= 145;
                Properties.Settings.Default.unlock = false;
                Properties.Settings.Default.Save();
            }
                
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

            Thread chatcleanerThread = new Thread(new ThreadStart(ChatSpammer.ChatCleaner))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            chatcleanerThread.Start();
        }

        private void chokeTrackBar_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.bhopChoke = chokeTrackBar.Value;
            Properties.Settings.Default.Save();
        }







        //    public static byte[] Shellcode = {
        //                    0x55,
        //                    0x8B, 0xEC,
        //                    0x83, 0xE4, 0xF8,
        //                    0x83, 0xEC, 0x44,
        //                    0x53,
        //                    0x56,
        //                    0x57,
        //                    0xBF, 0x00, 0x00, 0x00, 0x00,
        //                    0xBE, 0x00, 0x00, 0x00, 0x00,
        //                    0xB8, 0x00, 0x00, 0x00, 0x00,
        //                    0xFF, 0xE0,
        //                    0x6E, 0x61, 0x6D, 0x65, 0x00,
        //                    0x00
        //                };

        //    public static int Size = Shellcode.Length;
        //    public static IntPtr Address;
        //    private void button1_Click_1(object sender, EventArgs e)
        //    {
        //        string name = "\n\xAD\xAD\xAD";
        //        Allocator alloc = new Allocator();

        //        if (Address == IntPtr.Zero)
        //        {
        //            Address = alloc.Alloc(Size);

        //            if (Address == IntPtr.Zero)
        //                return;

        //            Buffer.BlockCopy(BitConverter.GetBytes((int)Address + 0x1D), 0, Shellcode, 0xD, 4);
        //            Buffer.BlockCopy(BitConverter.GetBytes((int)Address + 0x22), 0, Shellcode, 0x12, 4);
        //            Buffer.BlockCopy(BitConverter.GetBytes(Memory.Engine + ScannedOffsets.SetConVar), 0, Shellcode, 0x17, 4);

        //        }

        //        if (!LocalPlayer.InGame) return;

        //        byte[] reset = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        //        byte[] name_bytes;
        //        if (name == "\n")
        //        {
        //            name_bytes = Encoding.UTF8.GetBytes('\n' + "\0");
        //        }
        //        else
        //        {
        //            name_bytes = Encoding.UTF8.GetBytes(name + "\0");
        //        }

        //        //Buffer.BlockCopy(reset, 0, Shellcode, 0x22, reset.Length);
        //        //Buffer.BlockCopy(name_bytes, 0, Shellcode, 0x22, name_bytes.Length);
        //        Memory.WriteProcessMemory(Memory.pHandle, Address, Shellcode, Shellcode.Length, 0);

        //        for (int i = 0; i < 1000; i++)
        //        {
        //            CreateThread.Execute(Address);
        //            Thread.Sleep(1);
        //        }

        //    }

        //}
        //public static class CreateThread
        //{
        //    public static void Create(IntPtr address, byte[] shellcode)
        //    {
        //        Memory.WriteProcessMemory(Memory.pHandle, address, shellcode, shellcode.Length, 0);
        //        IntPtr _Thread = DllImport.CreateRemoteThread(Memory.pHandle, (IntPtr)null, IntPtr.Zero, address, (IntPtr)null, 0, (IntPtr)null);
        //        DllImport.WaitForSingleObject(_Thread, 0xFFFFFFFF);
        //        DllImport.CloseHandle(_Thread);
        //    }

        //    public static void Execute(IntPtr address)
        //    {
        //        IntPtr _Thread = DllImport.CreateRemoteThread(Memory.pHandle, (IntPtr)null, IntPtr.Zero, address, (IntPtr)null, 0, (IntPtr)null);
        //        DllImport.WaitForSingleObject(_Thread, 0xFFFFFFFF);
        //        DllImport.CloseHandle(_Thread);
        //    }
    }
}
