namespace httprequesttests
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_response = new System.Windows.Forms.TextBox();
            this.button_Webclient_Request = new System.Windows.Forms.Button();
            this.DG1 = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.views = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sells = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wishlists = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Durchschnitt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Timer_Toggle = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox_Alerts = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.abbrechenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_DisposeAlerts = new System.Windows.Forms.Button();
            this.checkBox_NotifyViews = new System.Windows.Forms.CheckBox();
            this.checkBox_LogViews = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DG1)).BeginInit();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_response
            // 
            this.textBox_response.Location = new System.Drawing.Point(599, 36);
            this.textBox_response.Multiline = true;
            this.textBox_response.Name = "textBox_response";
            this.textBox_response.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_response.Size = new System.Drawing.Size(465, 634);
            this.textBox_response.TabIndex = 1;
            // 
            // button_Webclient_Request
            // 
            this.button_Webclient_Request.Location = new System.Drawing.Point(253, 582);
            this.button_Webclient_Request.Name = "button_Webclient_Request";
            this.button_Webclient_Request.Size = new System.Drawing.Size(64, 23);
            this.button_Webclient_Request.TabIndex = 3;
            this.button_Webclient_Request.Text = "Refresh Manually";
            this.button_Webclient_Request.UseVisualStyleBackColor = true;
            this.button_Webclient_Request.Click += new System.EventHandler(this.button_Webclient_Request_Click);
            // 
            // DG1
            // 
            this.DG1.AllowUserToAddRows = false;
            this.DG1.AllowUserToDeleteRows = false;
            this.DG1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.name,
            this.views,
            this.sells,
            this.wishlists,
            this.ratings,
            this.Durchschnitt});
            this.DG1.Location = new System.Drawing.Point(12, 12);
            this.DG1.Name = "DG1";
            this.DG1.ReadOnly = true;
            this.DG1.RowHeadersVisible = false;
            this.DG1.Size = new System.Drawing.Size(570, 166);
            this.DG1.TabIndex = 4;
            // 
            // number
            // 
            this.number.HeaderText = "#";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 50;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 300;
            // 
            // views
            // 
            this.views.HeaderText = "Views";
            this.views.Name = "views";
            this.views.ReadOnly = true;
            this.views.Width = 40;
            // 
            // sells
            // 
            this.sells.HeaderText = "Verk.";
            this.sells.Name = "sells";
            this.sells.ReadOnly = true;
            this.sells.Width = 40;
            // 
            // wishlists
            // 
            this.wishlists.HeaderText = "WL";
            this.wishlists.Name = "wishlists";
            this.wishlists.ReadOnly = true;
            this.wishlists.Width = 40;
            // 
            // ratings
            // 
            this.ratings.HeaderText = "Bew.";
            this.ratings.Name = "ratings";
            this.ratings.ReadOnly = true;
            this.ratings.Width = 40;
            // 
            // Durchschnitt
            // 
            this.Durchschnitt.HeaderText = "DS";
            this.Durchschnitt.Name = "Durchschnitt";
            this.Durchschnitt.ReadOnly = true;
            this.Durchschnitt.Width = 40;
            // 
            // button_Timer_Toggle
            // 
            this.button_Timer_Toggle.Location = new System.Drawing.Point(390, 581);
            this.button_Timer_Toggle.Name = "button_Timer_Toggle";
            this.button_Timer_Toggle.Size = new System.Drawing.Size(192, 23);
            this.button_Timer_Toggle.TabIndex = 5;
            this.button_Timer_Toggle.Text = "Activate Tracking";
            this.button_Timer_Toggle.UseVisualStyleBackColor = true;
            this.button_Timer_Toggle.Click += new System.EventHandler(this.button_Timer_Toggle_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox_Alerts
            // 
            this.textBox_Alerts.Location = new System.Drawing.Point(12, 184);
            this.textBox_Alerts.Multiline = true;
            this.textBox_Alerts.Name = "textBox_Alerts";
            this.textBox_Alerts.Size = new System.Drawing.Size(570, 391);
            this.textBox_Alerts.TabIndex = 6;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "asdf";
            this.notifyIcon1.BalloonTipTitle = "asdf";
            this.notifyIcon1.ContextMenuStrip = this.contextMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.abbrechenToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(133, 76);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.showToolStripMenuItem.Text = "Anzeigen";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.closeToolStripMenuItem.Text = "Beenden";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(129, 6);
            // 
            // abbrechenToolStripMenuItem
            // 
            this.abbrechenToolStripMenuItem.Name = "abbrechenToolStripMenuItem";
            this.abbrechenToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.abbrechenToolStripMenuItem.Text = "Abbrechen";
            this.abbrechenToolStripMenuItem.Click += new System.EventHandler(this.abbrechenToolStripMenuItem_Click);
            // 
            // button_DisposeAlerts
            // 
            this.button_DisposeAlerts.Location = new System.Drawing.Point(13, 582);
            this.button_DisposeAlerts.Name = "button_DisposeAlerts";
            this.button_DisposeAlerts.Size = new System.Drawing.Size(176, 23);
            this.button_DisposeAlerts.TabIndex = 7;
            this.button_DisposeAlerts.Text = "Meldungen leeren";
            this.button_DisposeAlerts.UseVisualStyleBackColor = true;
            this.button_DisposeAlerts.Click += new System.EventHandler(this.button_DisposeAlerts_Click);
            // 
            // checkBox_NotifyViews
            // 
            this.checkBox_NotifyViews.AutoSize = true;
            this.checkBox_NotifyViews.Checked = true;
            this.checkBox_NotifyViews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_NotifyViews.Location = new System.Drawing.Point(105, 611);
            this.checkBox_NotifyViews.Name = "checkBox_NotifyViews";
            this.checkBox_NotifyViews.Size = new System.Drawing.Size(84, 17);
            this.checkBox_NotifyViews.TabIndex = 8;
            this.checkBox_NotifyViews.Text = "Notify Views";
            this.checkBox_NotifyViews.UseVisualStyleBackColor = true;
            this.checkBox_NotifyViews.CheckedChanged += new System.EventHandler(this.checkBox_NotifyViews_CheckedChanged);
            // 
            // checkBox_LogViews
            // 
            this.checkBox_LogViews.AutoSize = true;
            this.checkBox_LogViews.Checked = true;
            this.checkBox_LogViews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_LogViews.Location = new System.Drawing.Point(13, 611);
            this.checkBox_LogViews.Name = "checkBox_LogViews";
            this.checkBox_LogViews.Size = new System.Drawing.Size(75, 17);
            this.checkBox_LogViews.TabIndex = 9;
            this.checkBox_LogViews.Text = "Log Views";
            this.checkBox_LogViews.UseVisualStyleBackColor = true;
            this.checkBox_LogViews.CheckedChanged += new System.EventHandler(this.checkBox_LogViews_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(426, 610);
            this.trackBar1.Maximum = 180000;
            this.trackBar1.Minimum = 10000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(121, 45);
            this.trackBar1.TabIndex = 12;
            this.trackBar1.TickFrequency = 10000;
            this.trackBar1.Value = 10000;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 612);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "10 Sek";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(543, 612);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "30 Min";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 637);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_LogViews);
            this.Controls.Add(this.checkBox_NotifyViews);
            this.Controls.Add(this.button_DisposeAlerts);
            this.Controls.Add(this.textBox_Alerts);
            this.Controls.Add(this.button_Timer_Toggle);
            this.Controls.Add(this.DG1);
            this.Controls.Add(this.button_Webclient_Request);
            this.Controls.Add(this.textBox_response);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "CrazyPatterns Event-Tracker... Offline";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DG1)).EndInit();
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_response;
        private System.Windows.Forms.Button button_Webclient_Request;
        private System.Windows.Forms.DataGridView DG1;
        private System.Windows.Forms.Button button_Timer_Toggle;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn views;
        private System.Windows.Forms.DataGridViewTextBoxColumn sells;
        private System.Windows.Forms.DataGridViewTextBoxColumn wishlists;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratings;
        private System.Windows.Forms.DataGridViewTextBoxColumn Durchschnitt;
        private System.Windows.Forms.TextBox textBox_Alerts;
        private System.Windows.Forms.Button button_DisposeAlerts;
        private System.Windows.Forms.CheckBox checkBox_NotifyViews;
        private System.Windows.Forms.CheckBox checkBox_LogViews;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem abbrechenToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

