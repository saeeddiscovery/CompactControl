﻿namespace Compact_Control
{
    partial class ClientControls
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientControls));
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_x1_s = new System.Windows.Forms.TextBox();
            this.txt_x2_s = new System.Windows.Forms.TextBox();
            this.txt_y1_s = new System.Windows.Forms.TextBox();
            this.txt_y2_s = new System.Windows.Forms.TextBox();
            this.txt_coli_s = new System.Windows.Forms.TextBox();
            this.txt_gant_s = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txt_x1_a = new System.Windows.Forms.TextBox();
            this.txt_x2_a = new System.Windows.Forms.TextBox();
            this.txt_y1_a = new System.Windows.Forms.TextBox();
            this.txt_y2_a = new System.Windows.Forms.TextBox();
            this.txt_coli_a = new System.Windows.Forms.TextBox();
            this.txt_gant_a = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.picBtnToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Reading_Error = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.txt_x_s = new System.Windows.Forms.TextBox();
            this.txt_x_a = new System.Windows.Forms.TextBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.txt_y_a = new System.Windows.Forms.TextBox();
            this.txt_y_s = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.btn_clearTerminal_in = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.tb_terminal_in = new System.Windows.Forms.TextBox();
            this.btn_clearTerminal = new System.Windows.Forms.Button();
            this.label48 = new System.Windows.Forms.Label();
            this.tb_terminal_out = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox6.Location = new System.Drawing.Point(201, 180);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(20, 29);
            this.pictureBox6.TabIndex = 78;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Visible = false;
            this.pictureBox6.BackgroundImageChanged += new System.EventHandler(this.x12ErrorSign_VisibleChanged);
            this.pictureBox6.VisibleChanged += new System.EventHandler(this.x12ErrorSign_VisibleChanged);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox5.Location = new System.Drawing.Point(201, 219);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(20, 29);
            this.pictureBox5.TabIndex = 77;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Visible = false;
            this.pictureBox5.BackgroundImageChanged += new System.EventHandler(this.x12ErrorSign_VisibleChanged);
            this.pictureBox5.VisibleChanged += new System.EventHandler(this.x12ErrorSign_VisibleChanged);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox4.Location = new System.Drawing.Point(201, 101);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 29);
            this.pictureBox4.TabIndex = 76;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Visible = false;
            this.pictureBox4.BackgroundImageChanged += new System.EventHandler(this.y12ErrorSign_VisibleChanged);
            this.pictureBox4.VisibleChanged += new System.EventHandler(this.y12ErrorSign_VisibleChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.Location = new System.Drawing.Point(201, 140);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 29);
            this.pictureBox3.TabIndex = 75;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            this.pictureBox3.BackgroundImageChanged += new System.EventHandler(this.y12ErrorSign_VisibleChanged);
            this.pictureBox3.VisibleChanged += new System.EventHandler(this.y12ErrorSign_VisibleChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(203, 57);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 27);
            this.pictureBox2.TabIndex = 74;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(203, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 27);
            this.pictureBox1.TabIndex = 73;
            this.pictureBox1.TabStop = false;
            // 
            // txt_x1_s
            // 
            this.txt_x1_s.BackColor = System.Drawing.SystemColors.Window;
            this.txt_x1_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_x1_s.Location = new System.Drawing.Point(227, 181);
            this.txt_x1_s.Name = "txt_x1_s";
            this.txt_x1_s.Size = new System.Drawing.Size(80, 27);
            this.txt_x1_s.TabIndex = 7;
            this.txt_x1_s.Click += new System.EventHandler(this.txtBox_Enter);
            this.txt_x1_s.TextChanged += new System.EventHandler(this.textBox37_TextChanged);
            this.txt_x1_s.Enter += new System.EventHandler(this.txtBox_Enter);
            this.txt_x1_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_x1_s_KeyPress);
            // 
            // txt_x2_s
            // 
            this.txt_x2_s.BackColor = System.Drawing.SystemColors.Window;
            this.txt_x2_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_x2_s.Location = new System.Drawing.Point(227, 220);
            this.txt_x2_s.Name = "txt_x2_s";
            this.txt_x2_s.Size = new System.Drawing.Size(80, 27);
            this.txt_x2_s.TabIndex = 8;
            this.txt_x2_s.Click += new System.EventHandler(this.txtBox_Enter);
            this.txt_x2_s.TextChanged += new System.EventHandler(this.textBox38_TextChanged);
            this.txt_x2_s.Enter += new System.EventHandler(this.txtBox_Enter);
            this.txt_x2_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_x2_s_KeyPress);
            // 
            // txt_y1_s
            // 
            this.txt_y1_s.BackColor = System.Drawing.SystemColors.Window;
            this.txt_y1_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_y1_s.Location = new System.Drawing.Point(227, 101);
            this.txt_y1_s.Name = "txt_y1_s";
            this.txt_y1_s.Size = new System.Drawing.Size(80, 27);
            this.txt_y1_s.TabIndex = 5;
            this.txt_y1_s.Click += new System.EventHandler(this.txtBox_Enter);
            this.txt_y1_s.TextChanged += new System.EventHandler(this.textBox39_TextChanged);
            this.txt_y1_s.Enter += new System.EventHandler(this.txtBox_Enter);
            this.txt_y1_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_y1_s_KeyPress);
            // 
            // txt_y2_s
            // 
            this.txt_y2_s.BackColor = System.Drawing.SystemColors.Window;
            this.txt_y2_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_y2_s.Location = new System.Drawing.Point(227, 140);
            this.txt_y2_s.Name = "txt_y2_s";
            this.txt_y2_s.Size = new System.Drawing.Size(80, 27);
            this.txt_y2_s.TabIndex = 6;
            this.txt_y2_s.Click += new System.EventHandler(this.txtBox_Enter);
            this.txt_y2_s.TextChanged += new System.EventHandler(this.textBox40_TextChanged);
            this.txt_y2_s.Enter += new System.EventHandler(this.txtBox_Enter);
            this.txt_y2_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_y2_s_KeyPress);
            // 
            // txt_coli_s
            // 
            this.txt_coli_s.BackColor = System.Drawing.SystemColors.Window;
            this.txt_coli_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_coli_s.Location = new System.Drawing.Point(229, 57);
            this.txt_coli_s.Name = "txt_coli_s";
            this.txt_coli_s.Size = new System.Drawing.Size(80, 27);
            this.txt_coli_s.TabIndex = 2;
            this.txt_coli_s.Click += new System.EventHandler(this.txtBox_Enter);
            this.txt_coli_s.TextChanged += new System.EventHandler(this.textBox41_TextChanged);
            this.txt_coli_s.Enter += new System.EventHandler(this.txtBox_Enter);
            this.txt_coli_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_coli_s_KeyPress);
            // 
            // txt_gant_s
            // 
            this.txt_gant_s.BackColor = System.Drawing.SystemColors.Window;
            this.txt_gant_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_gant_s.Location = new System.Drawing.Point(229, 23);
            this.txt_gant_s.Name = "txt_gant_s";
            this.txt_gant_s.Size = new System.Drawing.Size(80, 27);
            this.txt_gant_s.TabIndex = 1;
            this.txt_gant_s.Click += new System.EventHandler(this.txtBox_Enter);
            this.txt_gant_s.TextChanged += new System.EventHandler(this.txt_gant_s_TextChanged);
            this.txt_gant_s.Enter += new System.EventHandler(this.txtBox_Enter);
            this.txt_gant_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_gant_s_KeyPress);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(715, 43);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(56, 20);
            this.label37.TabIndex = 66;
            this.label37.Text = "Actual";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(630, 43);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(34, 20);
            this.label36.TabIndex = 65;
            this.label36.Text = "Set";
            // 
            // txt_x1_a
            // 
            this.txt_x1_a.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_x1_a.Enabled = false;
            this.txt_x1_a.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_x1_a.Location = new System.Drawing.Point(324, 181);
            this.txt_x1_a.Name = "txt_x1_a";
            this.txt_x1_a.ReadOnly = true;
            this.txt_x1_a.Size = new System.Drawing.Size(80, 27);
            this.txt_x1_a.TabIndex = 64;
            // 
            // txt_x2_a
            // 
            this.txt_x2_a.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_x2_a.Enabled = false;
            this.txt_x2_a.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_x2_a.Location = new System.Drawing.Point(324, 220);
            this.txt_x2_a.Name = "txt_x2_a";
            this.txt_x2_a.ReadOnly = true;
            this.txt_x2_a.Size = new System.Drawing.Size(80, 27);
            this.txt_x2_a.TabIndex = 63;
            // 
            // txt_y1_a
            // 
            this.txt_y1_a.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_y1_a.Enabled = false;
            this.txt_y1_a.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_y1_a.Location = new System.Drawing.Point(324, 101);
            this.txt_y1_a.Name = "txt_y1_a";
            this.txt_y1_a.ReadOnly = true;
            this.txt_y1_a.Size = new System.Drawing.Size(80, 27);
            this.txt_y1_a.TabIndex = 62;
            // 
            // txt_y2_a
            // 
            this.txt_y2_a.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_y2_a.Enabled = false;
            this.txt_y2_a.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_y2_a.Location = new System.Drawing.Point(324, 140);
            this.txt_y2_a.Name = "txt_y2_a";
            this.txt_y2_a.ReadOnly = true;
            this.txt_y2_a.Size = new System.Drawing.Size(80, 27);
            this.txt_y2_a.TabIndex = 61;
            // 
            // txt_coli_a
            // 
            this.txt_coli_a.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_coli_a.Enabled = false;
            this.txt_coli_a.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_coli_a.Location = new System.Drawing.Point(326, 57);
            this.txt_coli_a.Name = "txt_coli_a";
            this.txt_coli_a.ReadOnly = true;
            this.txt_coli_a.Size = new System.Drawing.Size(80, 27);
            this.txt_coli_a.TabIndex = 60;
            // 
            // txt_gant_a
            // 
            this.txt_gant_a.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_gant_a.Enabled = false;
            this.txt_gant_a.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_gant_a.Location = new System.Drawing.Point(326, 22);
            this.txt_gant_a.Name = "txt_gant_a";
            this.txt_gant_a.ReadOnly = true;
            this.txt_gant_a.Size = new System.Drawing.Size(80, 27);
            this.txt_gant_a.TabIndex = 59;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label35.Location = new System.Drawing.Point(8, 183);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(136, 22);
            this.label35.TabIndex = 58;
            this.label35.Text = "Diaphragm X1";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label34.Location = new System.Drawing.Point(8, 223);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(136, 22);
            this.label34.TabIndex = 57;
            this.label34.Text = "Diaphragm X2";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label33.Location = new System.Drawing.Point(8, 104);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(136, 22);
            this.label33.TabIndex = 56;
            this.label33.Text = "Diaphragm Y1";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label32.Location = new System.Drawing.Point(8, 144);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(136, 22);
            this.label32.TabIndex = 55;
            this.label32.Text = "Diaphragm Y2";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label31.Location = new System.Drawing.Point(8, 58);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(157, 22);
            this.label31.TabIndex = 54;
            this.label31.Text = "Collimator Angle";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label30.Location = new System.Drawing.Point(8, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(127, 22);
            this.label30.TabIndex = 53;
            this.label30.Text = "Gantry Angle";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 57600;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Error2.png");
            this.imageList1.Images.SetKeyName(1, "Request2.png");
            // 
            // picBtnToolTip
            // 
            this.picBtnToolTip.AutoPopDelay = 5000;
            this.picBtnToolTip.BackColor = System.Drawing.Color.White;
            this.picBtnToolTip.InitialDelay = 500;
            this.picBtnToolTip.ReshowDelay = 100;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Reading_Error);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.pictureBox15);
            this.groupBox2.Controls.Add(this.txt_x_s);
            this.groupBox2.Controls.Add(this.txt_x_a);
            this.groupBox2.Controls.Add(this.pictureBox14);
            this.groupBox2.Controls.Add(this.pictureBox6);
            this.groupBox2.Controls.Add(this.txt_y_a);
            this.groupBox2.Controls.Add(this.pictureBox5);
            this.groupBox2.Controls.Add(this.txt_y_s);
            this.groupBox2.Controls.Add(this.pictureBox4);
            this.groupBox2.Controls.Add(this.txt_x2_s);
            this.groupBox2.Controls.Add(this.txt_y1_s);
            this.groupBox2.Controls.Add(this.txt_x1_s);
            this.groupBox2.Controls.Add(this.txt_x2_a);
            this.groupBox2.Controls.Add(this.txt_x1_a);
            this.groupBox2.Controls.Add(this.pictureBox3);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.txt_y1_a);
            this.groupBox2.Controls.Add(this.label34);
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.txt_y2_s);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Controls.Add(this.txt_y2_a);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(380, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(429, 286);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            // 
            // Reading_Error
            // 
            this.Reading_Error.AutoSize = true;
            this.Reading_Error.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Reading_Error.ForeColor = System.Drawing.Color.Red;
            this.Reading_Error.Location = new System.Drawing.Point(6, 259);
            this.Reading_Error.Name = "Reading_Error";
            this.Reading_Error.Size = new System.Drawing.Size(16, 22);
            this.Reading_Error.TabIndex = 87;
            this.Reading_Error.Text = ".";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label5.Location = new System.Drawing.Point(8, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 22);
            this.label5.TabIndex = 86;
            this.label5.Text = "Fieldsize X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label4.Location = new System.Drawing.Point(8, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 22);
            this.label4.TabIndex = 85;
            this.label4.Text = "Fieldsize Y";
            // 
            // pictureBox15
            // 
            this.pictureBox15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox15.Location = new System.Drawing.Point(201, 64);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(20, 27);
            this.pictureBox15.TabIndex = 84;
            this.pictureBox15.TabStop = false;
            this.pictureBox15.Visible = false;
            // 
            // txt_x_s
            // 
            this.txt_x_s.BackColor = System.Drawing.SystemColors.Window;
            this.txt_x_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_x_s.Location = new System.Drawing.Point(227, 63);
            this.txt_x_s.Name = "txt_x_s";
            this.txt_x_s.Size = new System.Drawing.Size(80, 27);
            this.txt_x_s.TabIndex = 4;
            this.txt_x_s.Click += new System.EventHandler(this.txtBox_Enter);
            this.txt_x_s.TextChanged += new System.EventHandler(this.txt_x_s_TextChanged);
            this.txt_x_s.Enter += new System.EventHandler(this.txtBox_Enter);
            this.txt_x_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_x_s_KeyPress);
            // 
            // txt_x_a
            // 
            this.txt_x_a.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_x_a.Enabled = false;
            this.txt_x_a.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_x_a.Location = new System.Drawing.Point(324, 63);
            this.txt_x_a.Name = "txt_x_a";
            this.txt_x_a.ReadOnly = true;
            this.txt_x_a.Size = new System.Drawing.Size(80, 27);
            this.txt_x_a.TabIndex = 83;
            // 
            // pictureBox14
            // 
            this.pictureBox14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox14.Location = new System.Drawing.Point(201, 23);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(20, 27);
            this.pictureBox14.TabIndex = 81;
            this.pictureBox14.TabStop = false;
            this.pictureBox14.Visible = false;
            // 
            // txt_y_a
            // 
            this.txt_y_a.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_y_a.Enabled = false;
            this.txt_y_a.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_y_a.Location = new System.Drawing.Point(324, 24);
            this.txt_y_a.Name = "txt_y_a";
            this.txt_y_a.ReadOnly = true;
            this.txt_y_a.Size = new System.Drawing.Size(80, 27);
            this.txt_y_a.TabIndex = 80;
            // 
            // txt_y_s
            // 
            this.txt_y_s.BackColor = System.Drawing.SystemColors.Window;
            this.txt_y_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txt_y_s.Location = new System.Drawing.Point(227, 24);
            this.txt_y_s.Name = "txt_y_s";
            this.txt_y_s.Size = new System.Drawing.Size(80, 27);
            this.txt_y_s.TabIndex = 3;
            this.txt_y_s.Click += new System.EventHandler(this.txtBox_Enter);
            this.txt_y_s.TextChanged += new System.EventHandler(this.txt_y_s_TextChanged);
            this.txt_y_s.Enter += new System.EventHandler(this.txtBox_Enter);
            this.txt_y_s.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_y_s_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.txt_coli_s);
            this.groupBox3.Controls.Add(this.txt_gant_s);
            this.groupBox3.Controls.Add(this.txt_coli_a);
            this.groupBox3.Controls.Add(this.txt_gant_a);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(380, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(429, 91);
            this.groupBox3.TabIndex = 107;
            this.groupBox3.TabStop = false;
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 5;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // btn_clearTerminal_in
            // 
            this.btn_clearTerminal_in.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btn_clearTerminal_in.Location = new System.Drawing.Point(977, 380);
            this.btn_clearTerminal_in.Name = "btn_clearTerminal_in";
            this.btn_clearTerminal_in.Size = new System.Drawing.Size(109, 30);
            this.btn_clearTerminal_in.TabIndex = 113;
            this.btn_clearTerminal_in.Text = "Clear";
            this.btn_clearTerminal_in.UseVisualStyleBackColor = true;
            this.btn_clearTerminal_in.Click += new System.EventHandler(this.btn_clearTerminal_in_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label51.Location = new System.Drawing.Point(852, 224);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(103, 18);
            this.label51.TabIndex = 112;
            this.label51.Text = "Terminal (in)";
            // 
            // tb_terminal_in
            // 
            this.tb_terminal_in.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.tb_terminal_in.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.tb_terminal_in.ForeColor = System.Drawing.Color.Cyan;
            this.tb_terminal_in.Location = new System.Drawing.Point(852, 245);
            this.tb_terminal_in.Multiline = true;
            this.tb_terminal_in.Name = "tb_terminal_in";
            this.tb_terminal_in.ReadOnly = true;
            this.tb_terminal_in.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_terminal_in.Size = new System.Drawing.Size(234, 132);
            this.tb_terminal_in.TabIndex = 111;
            // 
            // btn_clearTerminal
            // 
            this.btn_clearTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btn_clearTerminal.Location = new System.Drawing.Point(978, 202);
            this.btn_clearTerminal.Name = "btn_clearTerminal";
            this.btn_clearTerminal.Size = new System.Drawing.Size(109, 30);
            this.btn_clearTerminal.TabIndex = 110;
            this.btn_clearTerminal.Text = "Clear";
            this.btn_clearTerminal.UseVisualStyleBackColor = true;
            this.btn_clearTerminal.Click += new System.EventHandler(this.btn_clearTerminal_Click);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label48.Location = new System.Drawing.Point(852, 46);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(114, 18);
            this.label48.TabIndex = 109;
            this.label48.Text = "Terminal (out)";
            // 
            // tb_terminal_out
            // 
            this.tb_terminal_out.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.tb_terminal_out.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.tb_terminal_out.ForeColor = System.Drawing.Color.Cyan;
            this.tb_terminal_out.Location = new System.Drawing.Point(852, 67);
            this.tb_terminal_out.Multiline = true;
            this.tb_terminal_out.Name = "tb_terminal_out";
            this.tb_terminal_out.ReadOnly = true;
            this.tb_terminal_out.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_terminal_out.Size = new System.Drawing.Size(234, 132);
            this.tb_terminal_out.TabIndex = 108;
            // 
            // ClientControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_clearTerminal_in);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.tb_terminal_in);
            this.Controls.Add(this.btn_clearTerminal);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.tb_terminal_out);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Name = "ClientControls";
            this.Size = new System.Drawing.Size(1188, 525);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_x1_s;
        private System.Windows.Forms.TextBox txt_x2_s;
        private System.Windows.Forms.TextBox txt_y1_s;
        private System.Windows.Forms.TextBox txt_y2_s;
        private System.Windows.Forms.TextBox txt_coli_s;
        private System.Windows.Forms.TextBox txt_gant_s;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txt_x1_a;
        private System.Windows.Forms.TextBox txt_x2_a;
        private System.Windows.Forms.TextBox txt_y1_a;
        private System.Windows.Forms.TextBox txt_y2_a;
        private System.Windows.Forms.TextBox txt_coli_a;
        private System.Windows.Forms.TextBox txt_gant_a;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Timer timer1;
        public System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip picBtnToolTip;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.TextBox txt_x_s;
        private System.Windows.Forms.TextBox txt_x_a;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.TextBox txt_y_s;
        private System.Windows.Forms.TextBox txt_y_a;
        private System.Windows.Forms.Label Reading_Error;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button btn_clearTerminal_in;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox tb_terminal_in;
        private System.Windows.Forms.Button btn_clearTerminal;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox tb_terminal_out;
    }
}
