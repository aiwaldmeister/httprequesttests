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
            this.textBox_response = new System.Windows.Forms.TextBox();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.button_request = new System.Windows.Forms.Button();
            this.textBox_LoginURL = new System.Windows.Forms.TextBox();
            this.button_Login = new System.Windows.Forms.Button();
            this.button_WebClient_Login = new System.Windows.Forms.Button();
            this.button_Webclient_Request = new System.Windows.Forms.Button();
            this.textBox_BaseURL = new System.Windows.Forms.TextBox();
            this.DG1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DG1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_response
            // 
            this.textBox_response.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_response.Location = new System.Drawing.Point(2, 32);
            this.textBox_response.Multiline = true;
            this.textBox_response.Name = "textBox_response";
            this.textBox_response.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_response.Size = new System.Drawing.Size(1114, 287);
            this.textBox_response.TabIndex = 1;
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(307, 8);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(218, 20);
            this.textBox_url.TabIndex = 2;
            this.textBox_url.Text = "https://www.crazypatterns.net/de/patman";
            this.textBox_url.Visible = false;
            // 
            // button_request
            // 
            this.button_request.Location = new System.Drawing.Point(531, 6);
            this.button_request.Name = "button_request";
            this.button_request.Size = new System.Drawing.Size(75, 23);
            this.button_request.TabIndex = 0;
            this.button_request.Text = "Request";
            this.button_request.UseVisualStyleBackColor = true;
            this.button_request.Click += new System.EventHandler(this.button_request_Click);
            // 
            // textBox_LoginURL
            // 
            this.textBox_LoginURL.Location = new System.Drawing.Point(83, 7);
            this.textBox_LoginURL.Name = "textBox_LoginURL";
            this.textBox_LoginURL.Size = new System.Drawing.Size(218, 20);
            this.textBox_LoginURL.TabIndex = 2;
            this.textBox_LoginURL.Text = "https://www.crazypatterns.net/de/users/login";
            this.textBox_LoginURL.Visible = false;
            // 
            // button_Login
            // 
            this.button_Login.Location = new System.Drawing.Point(2, 5);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(75, 23);
            this.button_Login.TabIndex = 0;
            this.button_Login.Text = "Login";
            this.button_Login.UseVisualStyleBackColor = true;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // button_WebClient_Login
            // 
            this.button_WebClient_Login.Location = new System.Drawing.Point(642, 6);
            this.button_WebClient_Login.Name = "button_WebClient_Login";
            this.button_WebClient_Login.Size = new System.Drawing.Size(110, 23);
            this.button_WebClient_Login.TabIndex = 3;
            this.button_WebClient_Login.Text = "ConnectWebClient";
            this.button_WebClient_Login.UseVisualStyleBackColor = true;
            this.button_WebClient_Login.Click += new System.EventHandler(this.button_WebClient_Login_Click);
            // 
            // button_Webclient_Request
            // 
            this.button_Webclient_Request.Location = new System.Drawing.Point(758, 6);
            this.button_Webclient_Request.Name = "button_Webclient_Request";
            this.button_Webclient_Request.Size = new System.Drawing.Size(110, 23);
            this.button_Webclient_Request.TabIndex = 3;
            this.button_Webclient_Request.Text = "RequestWebClient";
            this.button_Webclient_Request.UseVisualStyleBackColor = true;
            this.button_Webclient_Request.Click += new System.EventHandler(this.button_Webclient_Request_Click);
            // 
            // textBox_BaseURL
            // 
            this.textBox_BaseURL.Location = new System.Drawing.Point(874, 7);
            this.textBox_BaseURL.Name = "textBox_BaseURL";
            this.textBox_BaseURL.Size = new System.Drawing.Size(218, 20);
            this.textBox_BaseURL.TabIndex = 2;
            this.textBox_BaseURL.Text = "https://www.crazypatterns.net/de/";
            this.textBox_BaseURL.Visible = false;
            // 
            // DG1
            // 
            this.DG1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG1.Location = new System.Drawing.Point(12, 361);
            this.DG1.Name = "DG1";
            this.DG1.Size = new System.Drawing.Size(1104, 309);
            this.DG1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 682);
            this.Controls.Add(this.DG1);
            this.Controls.Add(this.button_Webclient_Request);
            this.Controls.Add(this.button_WebClient_Login);
            this.Controls.Add(this.textBox_response);
            this.Controls.Add(this.textBox_BaseURL);
            this.Controls.Add(this.textBox_LoginURL);
            this.Controls.Add(this.textBox_url);
            this.Controls.Add(this.button_Login);
            this.Controls.Add(this.button_request);
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
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Button button_request;
        private System.Windows.Forms.TextBox textBox_LoginURL;
        private System.Windows.Forms.Button button_Login;
        private System.Windows.Forms.Button button_WebClient_Login;
        private System.Windows.Forms.Button button_Webclient_Request;
        private System.Windows.Forms.TextBox textBox_BaseURL;
        private System.Windows.Forms.DataGridView DG1;
    }
}

