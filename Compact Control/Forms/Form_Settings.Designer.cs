namespace Compact_Control
{
    partial class Form_Settings
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
            this.comboBox_Ports = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBox_CurrPass = new System.Windows.Forms.MaskedTextBox();
            this.cmbBx_User = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextBox_NewPass = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maskedTextBox_ConfirmPass = new System.Windows.Forms.MaskedTextBox();
            this.button_Ok = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox_UserManagement = new System.Windows.Forms.GroupBox();
            this.pictureBox_PassConfirm = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox_clinicalTerminals = new System.Windows.Forms.CheckBox();
            this.checkBox_startup = new System.Windows.Forms.CheckBox();
            this.groupBox_portSetting = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_Baudrate = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_licType = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_DataBits = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_Parity = new System.Windows.Forms.ComboBox();
            this.groupBox_UserManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_PassConfirm)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox_portSetting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Ports
            // 
            this.comboBox_Ports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Ports.FormattingEnabled = true;
            this.comboBox_Ports.Location = new System.Drawing.Point(69, 24);
            this.comboBox_Ports.Name = "comboBox_Ports";
            this.comboBox_Ports.Size = new System.Drawing.Size(132, 21);
            this.comboBox_Ports.TabIndex = 0;
            this.comboBox_Ports.SelectedIndexChanged += new System.EventHandler(this.comboBox_Ports_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Serial Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(11, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Current Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "User";
            // 
            // maskedTextBox_CurrPass
            // 
            this.maskedTextBox_CurrPass.Location = new System.Drawing.Point(14, 86);
            this.maskedTextBox_CurrPass.Name = "maskedTextBox_CurrPass";
            this.maskedTextBox_CurrPass.PasswordChar = '●';
            this.maskedTextBox_CurrPass.Size = new System.Drawing.Size(194, 20);
            this.maskedTextBox_CurrPass.TabIndex = 5;
            // 
            // cmbBx_User
            // 
            this.cmbBx_User.FormattingEnabled = true;
            this.cmbBx_User.Items.AddRange(new object[] {
            "Clinical",
            "Service"});
            this.cmbBx_User.Location = new System.Drawing.Point(14, 42);
            this.cmbBx_User.Name = "cmbBx_User";
            this.cmbBx_User.Size = new System.Drawing.Size(194, 21);
            this.cmbBx_User.TabIndex = 6;
            this.cmbBx_User.Text = "Select User...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(11, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "New Password";
            // 
            // maskedTextBox_NewPass
            // 
            this.maskedTextBox_NewPass.Location = new System.Drawing.Point(14, 134);
            this.maskedTextBox_NewPass.Name = "maskedTextBox_NewPass";
            this.maskedTextBox_NewPass.PasswordChar = '●';
            this.maskedTextBox_NewPass.Size = new System.Drawing.Size(194, 20);
            this.maskedTextBox_NewPass.TabIndex = 9;
            this.maskedTextBox_NewPass.TextChanged += new System.EventHandler(this.maskedTextBox_ConfirmPass_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(11, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Confirm New Password";
            // 
            // maskedTextBox_ConfirmPass
            // 
            this.maskedTextBox_ConfirmPass.Location = new System.Drawing.Point(14, 178);
            this.maskedTextBox_ConfirmPass.Name = "maskedTextBox_ConfirmPass";
            this.maskedTextBox_ConfirmPass.PasswordChar = '●';
            this.maskedTextBox_ConfirmPass.Size = new System.Drawing.Size(194, 20);
            this.maskedTextBox_ConfirmPass.TabIndex = 11;
            this.maskedTextBox_ConfirmPass.TextChanged += new System.EventHandler(this.maskedTextBox_ConfirmPass_TextChanged);
            // 
            // button_Ok
            // 
            this.button_Ok.Location = new System.Drawing.Point(195, 6);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(62, 30);
            this.button_Ok.TabIndex = 13;
            this.button_Ok.Text = "OK";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(127, 6);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(62, 30);
            this.button_Cancel.TabIndex = 14;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // groupBox_UserManagement
            // 
            this.groupBox_UserManagement.Controls.Add(this.pictureBox_PassConfirm);
            this.groupBox_UserManagement.Controls.Add(this.label5);
            this.groupBox_UserManagement.Controls.Add(this.maskedTextBox_ConfirmPass);
            this.groupBox_UserManagement.Controls.Add(this.label2);
            this.groupBox_UserManagement.Controls.Add(this.maskedTextBox_NewPass);
            this.groupBox_UserManagement.Controls.Add(this.label3);
            this.groupBox_UserManagement.Controls.Add(this.label4);
            this.groupBox_UserManagement.Controls.Add(this.maskedTextBox_CurrPass);
            this.groupBox_UserManagement.Controls.Add(this.cmbBx_User);
            this.groupBox_UserManagement.Location = new System.Drawing.Point(8, 6);
            this.groupBox_UserManagement.Name = "groupBox_UserManagement";
            this.groupBox_UserManagement.Size = new System.Drawing.Size(233, 207);
            this.groupBox_UserManagement.TabIndex = 15;
            this.groupBox_UserManagement.TabStop = false;
            this.groupBox_UserManagement.Text = "User Management";
            // 
            // pictureBox_PassConfirm
            // 
            this.pictureBox_PassConfirm.BackgroundImage = global::Compact_Control.Properties.Resources.red_sign;
            this.pictureBox_PassConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_PassConfirm.Location = new System.Drawing.Point(209, 178);
            this.pictureBox_PassConfirm.Name = "pictureBox_PassConfirm";
            this.pictureBox_PassConfirm.Size = new System.Drawing.Size(19, 18);
            this.pictureBox_PassConfirm.TabIndex = 13;
            this.pictureBox_PassConfirm.TabStop = false;
            this.pictureBox_PassConfirm.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Cancel);
            this.panel1.Controls.Add(this.button_Ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 264);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 42);
            this.panel1.TabIndex = 16;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(262, 264);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox_clinicalTerminals);
            this.tabPage1.Controls.Add(this.checkBox_startup);
            this.tabPage1.Controls.Add(this.groupBox_portSetting);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(254, 238);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox_clinicalTerminals
            // 
            this.checkBox_clinicalTerminals.AutoSize = true;
            this.checkBox_clinicalTerminals.Location = new System.Drawing.Point(25, 122);
            this.checkBox_clinicalTerminals.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_clinicalTerminals.Name = "checkBox_clinicalTerminals";
            this.checkBox_clinicalTerminals.Size = new System.Drawing.Size(132, 17);
            this.checkBox_clinicalTerminals.TabIndex = 27;
            this.checkBox_clinicalTerminals.Text = "Show clinical terminals";
            this.checkBox_clinicalTerminals.UseVisualStyleBackColor = true;
            // 
            // checkBox_startup
            // 
            this.checkBox_startup.AutoSize = true;
            this.checkBox_startup.Location = new System.Drawing.Point(166, 222);
            this.checkBox_startup.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_startup.Name = "checkBox_startup";
            this.checkBox_startup.Size = new System.Drawing.Size(93, 17);
            this.checkBox_startup.TabIndex = 26;
            this.checkBox_startup.Text = "Run at startup";
            this.checkBox_startup.UseVisualStyleBackColor = true;
            // 
            // groupBox_portSetting
            // 
            this.groupBox_portSetting.Controls.Add(this.label10);
            this.groupBox_portSetting.Controls.Add(this.comboBox_Parity);
            this.groupBox_portSetting.Controls.Add(this.label9);
            this.groupBox_portSetting.Controls.Add(this.comboBox_DataBits);
            this.groupBox_portSetting.Controls.Add(this.label7);
            this.groupBox_portSetting.Controls.Add(this.comboBox_Baudrate);
            this.groupBox_portSetting.Controls.Add(this.label1);
            this.groupBox_portSetting.Controls.Add(this.comboBox_Ports);
            this.groupBox_portSetting.Location = new System.Drawing.Point(13, 9);
            this.groupBox_portSetting.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_portSetting.Name = "groupBox_portSetting";
            this.groupBox_portSetting.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_portSetting.Size = new System.Drawing.Size(226, 108);
            this.groupBox_portSetting.TabIndex = 25;
            this.groupBox_portSetting.TabStop = false;
            this.groupBox_portSetting.Text = "Port setting";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Baudrate:";
            // 
            // comboBox_Baudrate
            // 
            this.comboBox_Baudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Baudrate.FormattingEnabled = true;
            this.comboBox_Baudrate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200",
            "128000"});
            this.comboBox_Baudrate.Location = new System.Drawing.Point(69, 52);
            this.comboBox_Baudrate.Name = "comboBox_Baudrate";
            this.comboBox_Baudrate.Size = new System.Drawing.Size(132, 21);
            this.comboBox_Baudrate.TabIndex = 23;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(7, 173);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 30);
            this.button3.TabIndex = 22;
            this.button3.Text = "Restore Patients...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_licType);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lbl_name);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(10, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 55);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "License";
            // 
            // lbl_licType
            // 
            this.lbl_licType.AutoSize = true;
            this.lbl_licType.ForeColor = System.Drawing.Color.Navy;
            this.lbl_licType.Location = new System.Drawing.Point(117, 70);
            this.lbl_licType.Name = "lbl_licType";
            this.lbl_licType.Size = new System.Drawing.Size(10, 13);
            this.lbl_licType.TabIndex = 20;
            this.lbl_licType.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "License Type:";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.ForeColor = System.Drawing.Color.Navy;
            this.lbl_name.Location = new System.Drawing.Point(11, 49);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(10, 13);
            this.lbl_name.TabIndex = 17;
            this.lbl_name.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "This application is licensed to:";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(7, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 30);
            this.button1.TabIndex = 21;
            this.button1.Text = "Backup Patients...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox_UserManagement);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(254, 238);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Users";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Compact Control License File (*.lic) | *.lic";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "DataBits:";
            // 
            // comboBox_DataBits
            // 
            this.comboBox_DataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DataBits.FormattingEnabled = true;
            this.comboBox_DataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.comboBox_DataBits.Location = new System.Drawing.Point(69, 79);
            this.comboBox_DataBits.Name = "comboBox_DataBits";
            this.comboBox_DataBits.Size = new System.Drawing.Size(38, 21);
            this.comboBox_DataBits.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(109, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Parity:";
            // 
            // comboBox_Parity
            // 
            this.comboBox_Parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Parity.FormattingEnabled = true;
            this.comboBox_Parity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.comboBox_Parity.Location = new System.Drawing.Point(148, 79);
            this.comboBox_Parity.Name = "comboBox_Parity";
            this.comboBox_Parity.Size = new System.Drawing.Size(53, 21);
            this.comboBox_Parity.TabIndex = 27;
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 306);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Settings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Activated += new System.EventHandler(this.Form_Settings_Activated);
            this.Load += new System.EventHandler(this.Form_Settings_Load);
            this.groupBox_UserManagement.ResumeLayout(false);
            this.groupBox_UserManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_PassConfirm)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox_portSetting.ResumeLayout(false);
            this.groupBox_portSetting.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Ports;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_CurrPass;
        private System.Windows.Forms.ComboBox cmbBx_User;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_NewPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_ConfirmPass;
        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.GroupBox groupBox_UserManagement;
        private System.Windows.Forms.PictureBox pictureBox_PassConfirm;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_licType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_Baudrate;
        public System.Windows.Forms.GroupBox groupBox_portSetting;
        public System.Windows.Forms.CheckBox checkBox_startup;
        public System.Windows.Forms.CheckBox checkBox_clinicalTerminals;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_Parity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_DataBits;
    }
}