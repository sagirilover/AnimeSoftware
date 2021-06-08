namespace AnimeSoftware
{
    partial class AnimeForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.refreshButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.nickBox = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aliveColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.glowColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resetButton = new System.Windows.Forms.Button();
            this.bhopCheckBox = new System.Windows.Forms.CheckBox();
            this.doorspammerCheckBox = new System.Windows.Forms.CheckBox();
            this.blockbotCheckBox = new System.Windows.Forms.CheckBox();
            this.doorspammerButton = new System.Windows.Forms.Button();
            this.blockbotButton = new System.Windows.Forms.Button();
            this.fullrefreshButton = new System.Windows.Forms.Button();
            this.customnameTextBox = new System.Windows.Forms.TextBox();
            this.setupButton = new System.Windows.Forms.Button();
            this.nickBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stealNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setGlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeGlowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voteKickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stealWhenYouFriendlyfireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getByteNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightspamButton = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.smoothLabel = new System.Windows.Forms.Label();
            this.smoothTrackBar = new System.Windows.Forms.TrackBar();
            this.rscCheckBox = new System.Windows.Forms.CheckBox();
            this.fovLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fovTrackBar = new System.Windows.Forms.TrackBar();
            this.ffCheckBox = new System.Windows.Forms.CheckBox();
            this.aimbotCheckBox = new System.Windows.Forms.CheckBox();
            this.perfectnadeCheckBox = new System.Windows.Forms.CheckBox();
            this.chatcleanerCheckBox = new System.Windows.Forms.CheckBox();
            this.clantagCheckBox = new System.Windows.Forms.CheckBox();
            this.velCheckBox = new System.Windows.Forms.CheckBox();
            this.velnameCheckBox = new System.Windows.Forms.CheckBox();
            this.clanTextBox = new System.Windows.Forms.TextBox();
            this.clanButton = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.nickBox)).BeginInit();
            this.nickBoxContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.smoothTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.fovTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar5)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(173, 349);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 26);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // changeButton
            // 
            this.changeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.changeButton.Location = new System.Drawing.Point(12, 349);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(75, 26);
            this.changeButton.TabIndex = 5;
            this.changeButton.Text = "Steal Name";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // nickBox
            // 
            this.nickBox.AllowUserToAddRows = false;
            this.nickBox.AllowUserToDeleteRows = false;
            this.nickBox.AllowUserToResizeRows = false;
            this.nickBox.BackgroundColor = System.Drawing.Color.White;
            this.nickBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nickBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.idColumn, this.nameColumn, this.aliveColumn, this.glowColumn});
            this.nickBox.GridColor = System.Drawing.Color.Silver;
            this.nickBox.Location = new System.Drawing.Point(12, 12);
            this.nickBox.Name = "nickBox";
            this.nickBox.ReadOnly = true;
            this.nickBox.RowHeadersVisible = false;
            this.nickBox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.nickBox.Size = new System.Drawing.Size(317, 331);
            this.nickBox.TabIndex = 10;
            this.nickBox.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.nickBox_CellClick);
            this.nickBox.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.nickBox_CellMouseUp);
            this.nickBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nickBox_MouseClick);
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "Id";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Width = 30;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.Width = 185;
            // 
            // aliveColumn
            // 
            this.aliveColumn.HeaderText = "Alive";
            this.aliveColumn.Name = "aliveColumn";
            this.aliveColumn.ReadOnly = true;
            this.aliveColumn.Width = 60;
            // 
            // glowColumn
            // 
            this.glowColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.glowColumn.HeaderText = "Glow";
            this.glowColumn.Name = "glowColumn";
            this.glowColumn.ReadOnly = true;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(93, 349);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 26);
            this.resetButton.TabIndex = 11;
            this.resetButton.Text = "Reset name";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // bhopCheckBox
            // 
            this.bhopCheckBox.AutoSize = true;
            this.bhopCheckBox.Location = new System.Drawing.Point(335, 12);
            this.bhopCheckBox.Name = "bhopCheckBox";
            this.bhopCheckBox.Size = new System.Drawing.Size(53, 17);
            this.bhopCheckBox.TabIndex = 13;
            this.bhopCheckBox.Text = "BHop";
            this.bhopCheckBox.UseVisualStyleBackColor = true;
            this.bhopCheckBox.CheckedChanged += new System.EventHandler(this.bhopCheckBox_CheckedChanged);
            // 
            // doorspammerCheckBox
            // 
            this.doorspammerCheckBox.AutoSize = true;
            this.doorspammerCheckBox.Location = new System.Drawing.Point(481, 12);
            this.doorspammerCheckBox.Name = "doorspammerCheckBox";
            this.doorspammerCheckBox.Size = new System.Drawing.Size(93, 17);
            this.doorspammerCheckBox.TabIndex = 14;
            this.doorspammerCheckBox.Text = "DoorSpammer";
            this.doorspammerCheckBox.UseVisualStyleBackColor = true;
            this.doorspammerCheckBox.CheckedChanged += new System.EventHandler(this.doorspammerCheckBox_CheckedChanged);
            // 
            // blockbotCheckBox
            // 
            this.blockbotCheckBox.AutoSize = true;
            this.blockbotCheckBox.Location = new System.Drawing.Point(481, 57);
            this.blockbotCheckBox.Name = "blockbotCheckBox";
            this.blockbotCheckBox.Size = new System.Drawing.Size(69, 17);
            this.blockbotCheckBox.TabIndex = 15;
            this.blockbotCheckBox.Text = "BlockBot";
            this.blockbotCheckBox.UseVisualStyleBackColor = true;
            this.blockbotCheckBox.CheckedChanged += new System.EventHandler(this.blockbotCheckBox_CheckedChanged);
            // 
            // doorspammerButton
            // 
            this.doorspammerButton.Location = new System.Drawing.Point(481, 28);
            this.doorspammerButton.Name = "doorspammerButton";
            this.doorspammerButton.Size = new System.Drawing.Size(84, 23);
            this.doorspammerButton.TabIndex = 17;
            this.doorspammerButton.Text = "button2";
            this.doorspammerButton.UseVisualStyleBackColor = true;
            this.doorspammerButton.Click += new System.EventHandler(this.doorspammerButton_Click);
            this.doorspammerButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.doorspammerButton_KeyUp);
            // 
            // blockbotButton
            // 
            this.blockbotButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.blockbotButton.Location = new System.Drawing.Point(481, 73);
            this.blockbotButton.Name = "blockbotButton";
            this.blockbotButton.Size = new System.Drawing.Size(84, 23);
            this.blockbotButton.TabIndex = 18;
            this.blockbotButton.Text = "button3";
            this.blockbotButton.UseVisualStyleBackColor = true;
            this.blockbotButton.Click += new System.EventHandler(this.blockbotButton_Click);
            this.blockbotButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.blockbotButton_KeyUp);
            // 
            // fullrefreshButton
            // 
            this.fullrefreshButton.Location = new System.Drawing.Point(254, 349);
            this.fullrefreshButton.Name = "fullrefreshButton";
            this.fullrefreshButton.Size = new System.Drawing.Size(75, 26);
            this.fullrefreshButton.TabIndex = 19;
            this.fullrefreshButton.Text = "Full refresh";
            this.fullrefreshButton.UseVisualStyleBackColor = true;
            this.fullrefreshButton.Click += new System.EventHandler(this.fullrefreshButton_Click);
            // 
            // customnameTextBox
            // 
            this.customnameTextBox.Location = new System.Drawing.Point(12, 381);
            this.customnameTextBox.Name = "customnameTextBox";
            this.customnameTextBox.Size = new System.Drawing.Size(259, 20);
            this.customnameTextBox.TabIndex = 21;
            this.customnameTextBox.Text = "Custom name";
            // 
            // setupButton
            // 
            this.setupButton.Location = new System.Drawing.Point(277, 381);
            this.setupButton.Name = "setupButton";
            this.setupButton.Size = new System.Drawing.Size(52, 20);
            this.setupButton.TabIndex = 22;
            this.setupButton.Text = "Set";
            this.setupButton.UseVisualStyleBackColor = true;
            this.setupButton.Click += new System.EventHandler(this.setupButton_Click);
            // 
            // nickBoxContextMenuStrip
            // 
            this.nickBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.stealNameToolStripMenuItem, this.setGlowToolStripMenuItem, this.removeGlowToolStripMenuItem, this.voteKickToolStripMenuItem, this.stealWhenYouFriendlyfireToolStripMenuItem, this.getByteNameToolStripMenuItem});
            this.nickBoxContextMenuStrip.Name = "nickBoxContextMenuStrip";
            this.nickBoxContextMenuStrip.Size = new System.Drawing.Size(215, 136);
            this.nickBoxContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.nickBoxContextMenuStrip_ItemClicked);
            // 
            // stealNameToolStripMenuItem
            // 
            this.stealNameToolStripMenuItem.Name = "stealNameToolStripMenuItem";
            this.stealNameToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.stealNameToolStripMenuItem.Text = "Steal Name";
            // 
            // setGlowToolStripMenuItem
            // 
            this.setGlowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.redToolStripMenuItem, this.blueToolStripMenuItem, this.greenToolStripMenuItem, this.customToolStripMenuItem});
            this.setGlowToolStripMenuItem.Name = "setGlowToolStripMenuItem";
            this.setGlowToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.setGlowToolStripMenuItem.Text = "Set Glow";
            this.setGlowToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.setGlowToolStripMenuItem_DropDownItemClicked_1);
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.redToolStripMenuItem.Text = "Red";
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.greenToolStripMenuItem.Text = "Green";
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.customToolStripMenuItem.Text = "Custom";
            // 
            // removeGlowToolStripMenuItem
            // 
            this.removeGlowToolStripMenuItem.Name = "removeGlowToolStripMenuItem";
            this.removeGlowToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.removeGlowToolStripMenuItem.Text = "Remove Glow";
            // 
            // voteKickToolStripMenuItem
            // 
            this.voteKickToolStripMenuItem.Enabled = false;
            this.voteKickToolStripMenuItem.Name = "voteKickToolStripMenuItem";
            this.voteKickToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.voteKickToolStripMenuItem.Text = "Vote Kick";
            // 
            // stealWhenYouFriendlyfireToolStripMenuItem
            // 
            this.stealWhenYouFriendlyfireToolStripMenuItem.Name = "stealWhenYouFriendlyfireToolStripMenuItem";
            this.stealWhenYouFriendlyfireToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.stealWhenYouFriendlyfireToolStripMenuItem.Text = "Steal when you friendlyfire";
            // 
            // getByteNameToolStripMenuItem
            // 
            this.getByteNameToolStripMenuItem.Name = "getByteNameToolStripMenuItem";
            this.getByteNameToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.getByteNameToolStripMenuItem.Text = "Get byte name";
            // 
            // rightspamButton
            // 
            this.rightspamButton.AutoSize = true;
            this.rightspamButton.Location = new System.Drawing.Point(335, 35);
            this.rightspamButton.Name = "rightspamButton";
            this.rightspamButton.Size = new System.Drawing.Size(135, 30);
            this.rightspamButton.TabIndex = 28;
            this.rightspamButton.Text = "Spam Glock-18/Famas\r\nScope/R8";
            this.rightspamButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rightspamButton.UseVisualStyleBackColor = true;
            this.rightspamButton.CheckedChanged += new System.EventHandler(this.rightspamButton_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(335, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Speed";
            // 
            // smoothLabel
            // 
            this.smoothLabel.Location = new System.Drawing.Point(338, 368);
            this.smoothLabel.Name = "smoothLabel";
            this.smoothLabel.Size = new System.Drawing.Size(100, 17);
            this.smoothLabel.TabIndex = 9;
            this.smoothLabel.Text = "0";
            this.smoothLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // smoothTrackBar
            // 
            this.smoothTrackBar.AutoSize = false;
            this.smoothTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.smoothTrackBar.Location = new System.Drawing.Point(338, 342);
            this.smoothTrackBar.Maximum = 1000;
            this.smoothTrackBar.Name = "smoothTrackBar";
            this.smoothTrackBar.Size = new System.Drawing.Size(100, 27);
            this.smoothTrackBar.TabIndex = 8;
            this.smoothTrackBar.Value = 1;
            this.smoothTrackBar.Scroll += new System.EventHandler(this.smoothTrackBar_Scroll);
            // 
            // rscCheckBox
            // 
            this.rscCheckBox.AutoSize = true;
            this.rscCheckBox.Location = new System.Drawing.Point(349, 227);
            this.rscCheckBox.Name = "rscCheckBox";
            this.rscCheckBox.Size = new System.Drawing.Size(48, 17);
            this.rscCheckBox.TabIndex = 7;
            this.rscCheckBox.Text = "RSC";
            this.rscCheckBox.UseVisualStyleBackColor = true;
            this.rscCheckBox.CheckedChanged += new System.EventHandler(this.rscCheckBox_CheckedChanged);
            // 
            // fovLabel
            // 
            this.fovLabel.Location = new System.Drawing.Point(338, 304);
            this.fovLabel.Name = "fovLabel";
            this.fovLabel.Size = new System.Drawing.Size(100, 13);
            this.fovLabel.TabIndex = 6;
            this.fovLabel.Text = "0";
            this.fovLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "FOV";
            // 
            // fovTrackBar
            // 
            this.fovTrackBar.AutoSize = false;
            this.fovTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.fovTrackBar.Location = new System.Drawing.Point(338, 276);
            this.fovTrackBar.Maximum = 18000;
            this.fovTrackBar.Name = "fovTrackBar";
            this.fovTrackBar.Size = new System.Drawing.Size(100, 27);
            this.fovTrackBar.TabIndex = 4;
            this.fovTrackBar.Scroll += new System.EventHandler(this.fovTrackBar_Scroll);
            // 
            // ffCheckBox
            // 
            this.ffCheckBox.AutoSize = true;
            this.ffCheckBox.Location = new System.Drawing.Point(349, 209);
            this.ffCheckBox.Name = "ffCheckBox";
            this.ffCheckBox.Size = new System.Drawing.Size(82, 17);
            this.ffCheckBox.TabIndex = 1;
            this.ffCheckBox.Text = "Friendly Fire";
            this.ffCheckBox.UseVisualStyleBackColor = true;
            this.ffCheckBox.CheckedChanged += new System.EventHandler(this.ffCheckBox_CheckedChanged);
            // 
            // aimbotCheckBox
            // 
            this.aimbotCheckBox.AutoSize = true;
            this.aimbotCheckBox.Location = new System.Drawing.Point(335, 186);
            this.aimbotCheckBox.Name = "aimbotCheckBox";
            this.aimbotCheckBox.Size = new System.Drawing.Size(58, 17);
            this.aimbotCheckBox.TabIndex = 0;
            this.aimbotCheckBox.Text = "Aimbot";
            this.aimbotCheckBox.UseVisualStyleBackColor = true;
            this.aimbotCheckBox.CheckedChanged += new System.EventHandler(this.aimbotCheckBox_CheckedChanged);
            // 
            // perfectnadeCheckBox
            // 
            this.perfectnadeCheckBox.AutoSize = true;
            this.perfectnadeCheckBox.Location = new System.Drawing.Point(335, 71);
            this.perfectnadeCheckBox.Name = "perfectnadeCheckBox";
            this.perfectnadeCheckBox.Size = new System.Drawing.Size(68, 17);
            this.perfectnadeCheckBox.TabIndex = 34;
            this.perfectnadeCheckBox.Text = "-98 nade";
            this.perfectnadeCheckBox.UseVisualStyleBackColor = true;
            this.perfectnadeCheckBox.CheckedChanged += new System.EventHandler(this.perfectnadeCheckBox_CheckedChanged);
            // 
            // chatcleanerCheckBox
            // 
            this.chatcleanerCheckBox.AutoSize = true;
            this.chatcleanerCheckBox.Location = new System.Drawing.Point(335, 94);
            this.chatcleanerCheckBox.Name = "chatcleanerCheckBox";
            this.chatcleanerCheckBox.Size = new System.Drawing.Size(85, 17);
            this.chatcleanerCheckBox.TabIndex = 35;
            this.chatcleanerCheckBox.Text = "ChatBreaker";
            this.chatcleanerCheckBox.UseVisualStyleBackColor = true;
            this.chatcleanerCheckBox.CheckedChanged += new System.EventHandler(this.chatcleanerCheckBox_CheckedChanged);
            // 
            // clantagCheckBox
            // 
            this.clantagCheckBox.AutoSize = true;
            this.clantagCheckBox.Location = new System.Drawing.Point(335, 117);
            this.clantagCheckBox.Name = "clantagCheckBox";
            this.clantagCheckBox.Size = new System.Drawing.Size(66, 17);
            this.clantagCheckBox.TabIndex = 38;
            this.clantagCheckBox.Text = "ClanTag";
            this.clantagCheckBox.UseVisualStyleBackColor = true;
            this.clantagCheckBox.CheckedChanged += new System.EventHandler(this.clantagCheckBox_CheckedChanged);
            // 
            // velCheckBox
            // 
            this.velCheckBox.AutoSize = true;
            this.velCheckBox.Location = new System.Drawing.Point(335, 140);
            this.velCheckBox.Name = "velCheckBox";
            this.velCheckBox.Size = new System.Drawing.Size(82, 17);
            this.velCheckBox.TabIndex = 40;
            this.velCheckBox.Text = "VelocityTag";
            this.velCheckBox.UseVisualStyleBackColor = true;
            this.velCheckBox.CheckedChanged += new System.EventHandler(this.velCheckBox_CheckedChanged);
            // 
            // velnameCheckBox
            // 
            this.velnameCheckBox.AutoSize = true;
            this.velnameCheckBox.Enabled = false;
            this.velnameCheckBox.Location = new System.Drawing.Point(351, 163);
            this.velnameCheckBox.Name = "velnameCheckBox";
            this.velnameCheckBox.Size = new System.Drawing.Size(111, 17);
            this.velnameCheckBox.TabIndex = 41;
            this.velnameCheckBox.Text = "MaxVelocityName";
            this.velnameCheckBox.UseVisualStyleBackColor = true;
            this.velnameCheckBox.CheckedChanged += new System.EventHandler(this.velnameCheckBox_CheckedChanged);
            // 
            // clanTextBox
            // 
            this.clanTextBox.Location = new System.Drawing.Point(12, 403);
            this.clanTextBox.Name = "clanTextBox";
            this.clanTextBox.Size = new System.Drawing.Size(259, 20);
            this.clanTextBox.TabIndex = 42;
            this.clanTextBox.Text = "Cutsom clantag";
            // 
            // clanButton
            // 
            this.clanButton.Location = new System.Drawing.Point(277, 403);
            this.clanButton.Name = "clanButton";
            this.clanButton.Size = new System.Drawing.Size(52, 20);
            this.clanButton.TabIndex = 43;
            this.clanButton.Text = "Set";
            this.clanButton.UseVisualStyleBackColor = true;
            this.clanButton.Click += new System.EventHandler(this.clanButton_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(481, 120);
            this.trackBar1.Maximum = 300;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 44;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(481, 190);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 45;
            this.trackBar2.Value = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(478, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Distance Factor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(478, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Trajectory Factor";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(514, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 48;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(514, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 49;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(481, 281);
            this.trackBar3.Maximum = 2000;
            this.trackBar3.Minimum = -2000;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(104, 45);
            this.trackBar3.TabIndex = 50;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // trackBar4
            // 
            this.trackBar4.Location = new System.Drawing.Point(481, 332);
            this.trackBar4.Maximum = 2000;
            this.trackBar4.Minimum = -2000;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(104, 45);
            this.trackBar4.TabIndex = 51;
            this.trackBar4.Scroll += new System.EventHandler(this.trackBar4_Scroll);
            // 
            // trackBar5
            // 
            this.trackBar5.Location = new System.Drawing.Point(481, 378);
            this.trackBar5.Maximum = 2000;
            this.trackBar5.Minimum = -2000;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(104, 45);
            this.trackBar5.TabIndex = 52;
            this.trackBar5.Scroll += new System.EventHandler(this.trackBar5_Scroll);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(478, 260);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 53;
            this.label9.Text = "Viewmodel";
            // 
            // AnimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 431);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.smoothLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.smoothTrackBar);
            this.Controls.Add(this.trackBar5);
            this.Controls.Add(this.rscCheckBox);
            this.Controls.Add(this.trackBar4);
            this.Controls.Add(this.fovLabel);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.fovTrackBar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ffCheckBox);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.aimbotCheckBox);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.clanButton);
            this.Controls.Add(this.clanTextBox);
            this.Controls.Add(this.velnameCheckBox);
            this.Controls.Add(this.velCheckBox);
            this.Controls.Add(this.clantagCheckBox);
            this.Controls.Add(this.chatcleanerCheckBox);
            this.Controls.Add(this.perfectnadeCheckBox);
            this.Controls.Add(this.rightspamButton);
            this.Controls.Add(this.setupButton);
            this.Controls.Add(this.customnameTextBox);
            this.Controls.Add(this.fullrefreshButton);
            this.Controls.Add(this.blockbotButton);
            this.Controls.Add(this.doorspammerButton);
            this.Controls.Add(this.blockbotCheckBox);
            this.Controls.Add(this.doorspammerCheckBox);
            this.Controls.Add(this.bhopCheckBox);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.nickBox);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.refreshButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.Name = "AnimeForm";
            this.Text = "Animesoftware";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.AnimeForm_Shown);
            ((System.ComponentModel.ISupportInitialize) (this.nickBox)).EndInit();
            this.nickBoxContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.smoothTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.fovTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.trackBar5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.DataGridView nickBox;
        private System.Windows.Forms.Button resetButton;
        public System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.CheckBox bhopCheckBox;
        private System.Windows.Forms.CheckBox doorspammerCheckBox;
        private System.Windows.Forms.CheckBox blockbotCheckBox;
        private System.Windows.Forms.Button doorspammerButton;
        private System.Windows.Forms.Button blockbotButton;
        private System.Windows.Forms.Button fullrefreshButton;
        private System.Windows.Forms.TextBox customnameTextBox;
        private System.Windows.Forms.Button setupButton;
        private System.Windows.Forms.ContextMenuStrip nickBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem stealNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setGlowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voteKickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeGlowToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aliveColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn glowColumn;
        private System.Windows.Forms.CheckBox rightspamButton;
        private System.Windows.Forms.CheckBox aimbotCheckBox;
        private System.Windows.Forms.TrackBar fovTrackBar;
        private System.Windows.Forms.CheckBox ffCheckBox;
        private System.Windows.Forms.Label fovLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label smoothLabel;
        private System.Windows.Forms.TrackBar smoothTrackBar;
        private System.Windows.Forms.CheckBox rscCheckBox;
        private System.Windows.Forms.ToolStripMenuItem stealWhenYouFriendlyfireToolStripMenuItem;
        private System.Windows.Forms.CheckBox perfectnadeCheckBox;
        private System.Windows.Forms.CheckBox chatcleanerCheckBox;
        private System.Windows.Forms.CheckBox clantagCheckBox;
        private System.Windows.Forms.CheckBox velCheckBox;
        private System.Windows.Forms.CheckBox velnameCheckBox;
        private System.Windows.Forms.TextBox clanTextBox;
        private System.Windows.Forms.Button clanButton;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.ToolStripMenuItem getByteNameToolStripMenuItem;
        private System.Windows.Forms.Label label9;
    }
}

