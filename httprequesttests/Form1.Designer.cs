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
            this.textBox_response = new System.Windows.Forms.TextBox();
            this.button_Webclient_Request = new System.Windows.Forms.Button();
            this.DG1 = new System.Windows.Forms.DataGridView();
            this.button_Timer_Toggle = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.views = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sells = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wishlists = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Durchschnitt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DG1)).BeginInit();
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
            this.button_Webclient_Request.Location = new System.Drawing.Point(12, 7);
            this.button_Webclient_Request.Name = "button_Webclient_Request";
            this.button_Webclient_Request.Size = new System.Drawing.Size(110, 23);
            this.button_Webclient_Request.TabIndex = 3;
            this.button_Webclient_Request.Text = "RequestWebClient";
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
            this.DG1.Location = new System.Drawing.Point(12, 36);
            this.DG1.Name = "DG1";
            this.DG1.ReadOnly = true;
            this.DG1.RowHeadersVisible = false;
            this.DG1.Size = new System.Drawing.Size(570, 634);
            this.DG1.TabIndex = 4;
            // 
            // button_Timer_Toggle
            // 
            this.button_Timer_Toggle.Location = new System.Drawing.Point(128, 7);
            this.button_Timer_Toggle.Name = "button_Timer_Toggle";
            this.button_Timer_Toggle.Size = new System.Drawing.Size(75, 23);
            this.button_Timer_Toggle.TabIndex = 5;
            this.button_Timer_Toggle.Text = "Activate Tracking";
            this.button_Timer_Toggle.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 682);
            this.Controls.Add(this.button_Timer_Toggle);
            this.Controls.Add(this.DG1);
            this.Controls.Add(this.button_Webclient_Request);
            this.Controls.Add(this.textBox_response);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DG1)).EndInit();
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
    }
}

