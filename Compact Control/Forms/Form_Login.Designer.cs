namespace Compact_Control
{
    partial class Form_Login
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
            this.components = new System.ComponentModel.Container();
            this.cmbBx_User = new System.Windows.Forms.ComboBox();
            this.txtBx_Pass = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_resetPass = new System.Windows.Forms.LinkLabel();
            this.picBtn_Login = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picBtnToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label_WrongPass = new System.Windows.Forms.Label();
            this.picBtn_exit = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_Login)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_exit)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbBx_User
            // 
            this.cmbBx_User.FormattingEnabled = true;
            this.cmbBx_User.Items.AddRange(new object[] {
            "Clinical",
            "Service"});
            this.cmbBx_User.Location = new System.Drawing.Point(356, 42);
            this.cmbBx_User.Margin = new System.Windows.Forms.Padding(6);
            this.cmbBx_User.Name = "cmbBx_User";
            this.cmbBx_User.Size = new System.Drawing.Size(352, 32);
            this.cmbBx_User.TabIndex = 1;
            this.cmbBx_User.Text = "Clinical";
            this.picBtnToolTip.SetToolTip(this.cmbBx_User, "Select User");
            this.cmbBx_User.SelectedIndexChanged += new System.EventHandler(this.cmbBx_User_SelectedIndexChanged);
            // 
            // txtBx_Pass
            // 
            this.txtBx_Pass.Enabled = false;
            this.txtBx_Pass.Location = new System.Drawing.Point(356, 124);
            this.txtBx_Pass.Margin = new System.Windows.Forms.Padding(6);
            this.txtBx_Pass.Name = "txtBx_Pass";
            this.txtBx_Pass.PasswordChar = '●';
            this.txtBx_Pass.Size = new System.Drawing.Size(352, 29);
            this.txtBx_Pass.TabIndex = 0;
            this.picBtnToolTip.SetToolTip(this.txtBx_Pass, "Enter Password");
            this.txtBx_Pass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBx_Pass_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(350, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(350, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // lbl_resetPass
            // 
            this.lbl_resetPass.AutoSize = true;
            this.lbl_resetPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_resetPass.Enabled = false;
            this.lbl_resetPass.Location = new System.Drawing.Point(356, 166);
            this.lbl_resetPass.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_resetPass.Name = "lbl_resetPass";
            this.lbl_resetPass.Size = new System.Drawing.Size(153, 25);
            this.lbl_resetPass.TabIndex = 2;
            this.lbl_resetPass.TabStop = true;
            this.lbl_resetPass.Text = "Reset Password";
            this.lbl_resetPass.Visible = false;
            this.lbl_resetPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_resetPass_LinkClicked);
            // 
            // picBtn_Login
            // 
            this.picBtn_Login.BackgroundImage = global::Compact_Control.Properties.Resources.LoginButton;
            this.picBtn_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBtn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_Login.Location = new System.Drawing.Point(579, 183);
            this.picBtn_Login.Margin = new System.Windows.Forms.Padding(6);
            this.picBtn_Login.Name = "picBtn_Login";
            this.picBtn_Login.Size = new System.Drawing.Size(136, 131);
            this.picBtn_Login.TabIndex = 7;
            this.picBtn_Login.TabStop = false;
            this.picBtnToolTip.SetToolTip(this.picBtn_Login, "Log in");
            this.picBtn_Login.Click += new System.EventHandler(this.picBtn_Login_Click);
            this.picBtn_Login.MouseEnter += new System.EventHandler(this.picBtn_Login_MouseEnter);
            this.picBtn_Login.MouseLeave += new System.EventHandler(this.picBtn_Setting_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Compact_Control.Properties.Resources.Untitled_1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(345, 335);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // picBtnToolTip
            // 
            this.picBtnToolTip.AutoPopDelay = 5000;
            this.picBtnToolTip.BackColor = System.Drawing.Color.White;
            this.picBtnToolTip.InitialDelay = 500;
            this.picBtnToolTip.ReshowDelay = 100;
            // 
            // label_WrongPass
            // 
            this.label_WrongPass.AutoSize = true;
            this.label_WrongPass.ForeColor = System.Drawing.Color.Red;
            this.label_WrongPass.Location = new System.Drawing.Point(356, 290);
            this.label_WrongPass.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_WrongPass.Name = "label_WrongPass";
            this.label_WrongPass.Size = new System.Drawing.Size(168, 25);
            this.label_WrongPass.TabIndex = 8;
            this.label_WrongPass.Text = "Wrong Password!";
            this.label_WrongPass.Visible = false;
            // 
            // picBtn_exit
            // 
            this.picBtn_exit.BackgroundImage = global::Compact_Control.Properties.Resources.Error;
            this.picBtn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBtn_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_exit.Location = new System.Drawing.Point(689, 0);
            this.picBtn_exit.Name = "picBtn_exit";
            this.picBtn_exit.Size = new System.Drawing.Size(31, 31);
            this.picBtn_exit.TabIndex = 9;
            this.picBtn_exit.TabStop = false;
            this.picBtn_exit.Visible = false;
            this.picBtn_exit.Click += new System.EventHandler(this.picBtn_exit_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picBtn_exit);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label_WrongPass);
            this.panel1.Controls.Add(this.picBtn_Login);
            this.panel1.Controls.Add(this.lbl_resetPass);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBx_Pass);
            this.panel1.Controls.Add(this.cmbBx_User);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 337);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Turquoise;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label_title);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(921, 65);
            this.panel2.TabIndex = 15;
            // 
            // label_title
            // 
            this.label_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label_title.Location = new System.Drawing.Point(0, 0);
            this.label_title.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(919, 63);
            this.label_title.TabIndex = 6;
            this.label_title.Text = "Login";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 442);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form_Login";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Form_Login_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Login_FormClosed);
            this.Load += new System.EventHandler(this.Form_Login_Load);
            this.Shown += new System.EventHandler(this.Form_Login_Shown);
            this.VisibleChanged += new System.EventHandler(this.Form_Login_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_Login)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_exit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBx_User;
        private System.Windows.Forms.MaskedTextBox txtBx_Pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lbl_resetPass;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox picBtn_Login;
        private System.Windows.Forms.ToolTip picBtnToolTip;
        private System.Windows.Forms.Label label_WrongPass;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox picBtn_exit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_title;
    }
}