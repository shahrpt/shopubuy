namespace shopubuyapp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabHome = new System.Windows.Forms.TabPage();
            this.tabAccount = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelAccountId = new System.Windows.Forms.Label();
            this.csvLocation = new System.Windows.Forms.TextBox();
            this.SessionId = new System.Windows.Forms.TextBox();
            this.MachineId = new System.Windows.Forms.TextBox();
            this.Token = new System.Windows.Forms.TextBox();
            this.Authorization = new System.Windows.Forms.TextBox();
            this.X_ECG_UDID = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.AccountId = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabHome.SuspendLayout();
            this.tabAccount.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabHome);
            this.tabControl1.Controls.Add(this.tabAccount);
            this.tabControl1.Location = new System.Drawing.Point(12, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 434);
            this.tabControl1.TabIndex = 0;
            // 
            // tabHome
            // 
            this.tabHome.Controls.Add(this.dataGridView1);
            this.tabHome.Controls.Add(this.panel1);
            this.tabHome.Location = new System.Drawing.Point(4, 22);
            this.tabHome.Name = "tabHome";
            this.tabHome.Padding = new System.Windows.Forms.Padding(3);
            this.tabHome.Size = new System.Drawing.Size(768, 408);
            this.tabHome.TabIndex = 0;
            this.tabHome.Text = "HOME";
            this.tabHome.UseVisualStyleBackColor = true;
            // 
            // tabAccount
            // 
            this.tabAccount.Controls.Add(this.btnCancel);
            this.tabAccount.Controls.Add(this.btnPost);
            this.tabAccount.Controls.Add(this.btnBrowse);
            this.tabAccount.Controls.Add(this.label7);
            this.tabAccount.Controls.Add(this.label6);
            this.tabAccount.Controls.Add(this.label1);
            this.tabAccount.Controls.Add(this.label5);
            this.tabAccount.Controls.Add(this.label4);
            this.tabAccount.Controls.Add(this.label3);
            this.tabAccount.Controls.Add(this.label2);
            this.tabAccount.Controls.Add(this.labelAccountId);
            this.tabAccount.Controls.Add(this.csvLocation);
            this.tabAccount.Controls.Add(this.SessionId);
            this.tabAccount.Controls.Add(this.MachineId);
            this.tabAccount.Controls.Add(this.Token);
            this.tabAccount.Controls.Add(this.Authorization);
            this.tabAccount.Controls.Add(this.X_ECG_UDID);
            this.tabAccount.Controls.Add(this.Email);
            this.tabAccount.Controls.Add(this.AccountId);
            this.tabAccount.Location = new System.Drawing.Point(4, 22);
            this.tabAccount.Name = "tabAccount";
            this.tabAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccount.Size = new System.Drawing.Size(768, 408);
            this.tabAccount.TabIndex = 1;
            this.tabAccount.Text = "ACCOUNT";
            this.tabAccount.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 346);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 59);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(762, 320);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(472, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(376, 321);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(75, 23);
            this.btnPost.TabIndex = 23;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(533, 273);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(52, 23);
            this.btnBrowse.TabIndex = 22;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "CSV Locatioin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 239);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "SessionId";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "MachineId";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Token";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Authorization";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "X_ECG_UDID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "E-mail";
            // 
            // labelAccountId
            // 
            this.labelAccountId.AutoSize = true;
            this.labelAccountId.Location = new System.Drawing.Point(11, 24);
            this.labelAccountId.Name = "labelAccountId";
            this.labelAccountId.Size = new System.Drawing.Size(58, 13);
            this.labelAccountId.TabIndex = 14;
            this.labelAccountId.Text = "AccountID";
            // 
            // csvLocation
            // 
            this.csvLocation.Location = new System.Drawing.Point(101, 274);
            this.csvLocation.Name = "csvLocation";
            this.csvLocation.Size = new System.Drawing.Size(426, 20);
            this.csvLocation.TabIndex = 13;
            // 
            // SessionId
            // 
            this.SessionId.Location = new System.Drawing.Point(101, 234);
            this.SessionId.Name = "SessionId";
            this.SessionId.Size = new System.Drawing.Size(484, 20);
            this.SessionId.TabIndex = 12;
            // 
            // MachineId
            // 
            this.MachineId.Location = new System.Drawing.Point(101, 197);
            this.MachineId.Name = "MachineId";
            this.MachineId.Size = new System.Drawing.Size(484, 20);
            this.MachineId.TabIndex = 11;
            // 
            // Token
            // 
            this.Token.Location = new System.Drawing.Point(101, 153);
            this.Token.Name = "Token";
            this.Token.Size = new System.Drawing.Size(446, 20);
            this.Token.TabIndex = 10;
            // 
            // Authorization
            // 
            this.Authorization.Location = new System.Drawing.Point(101, 113);
            this.Authorization.Name = "Authorization";
            this.Authorization.Size = new System.Drawing.Size(446, 20);
            this.Authorization.TabIndex = 9;
            // 
            // X_ECG_UDID
            // 
            this.X_ECG_UDID.Location = new System.Drawing.Point(101, 79);
            this.X_ECG_UDID.Name = "X_ECG_UDID";
            this.X_ECG_UDID.Size = new System.Drawing.Size(446, 20);
            this.X_ECG_UDID.TabIndex = 8;
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(101, 48);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(181, 20);
            this.Email.TabIndex = 7;
            // 
            // AccountId
            // 
            this.AccountId.Location = new System.Drawing.Point(101, 19);
            this.AccountId.Name = "AccountId";
            this.AccountId.Size = new System.Drawing.Size(181, 20);
            this.AccountId.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(680, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Gumtree";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabHome.ResumeLayout(false);
            this.tabAccount.ResumeLayout(false);
            this.tabAccount.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabHome;
        private System.Windows.Forms.TabPage tabAccount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelAccountId;
        private System.Windows.Forms.TextBox csvLocation;
        private System.Windows.Forms.TextBox SessionId;
        private System.Windows.Forms.TextBox MachineId;
        private System.Windows.Forms.TextBox Token;
        private System.Windows.Forms.TextBox Authorization;
        private System.Windows.Forms.TextBox X_ECG_UDID;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TextBox AccountId;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.Windows.Forms.Label lblStatus;
    }
}

