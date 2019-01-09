namespace Compact_Control
{
    partial class Form_PatientList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Dr = new System.Windows.Forms.Label();
            this.picBtn_BarcodeReader = new System.Windows.Forms.PictureBox();
            this.lbl_PName = new System.Windows.Forms.Label();
            this.lbl_PID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_showFieldsForm = new System.Windows.Forms.Button();
            this.btn_deletePatient = new System.Windows.Forms.Button();
            this.btn_AddPatient = new System.Windows.Forms.Button();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.comboBox_searchIn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picBtnToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btn_EditPatient = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_BarcodeReader)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Snow;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.GridColor = System.Drawing.Color.DarkSlateGray;
            this.dataGridView1.Location = new System.Drawing.Point(13, 184);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(457, 221);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Patient ID";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Last Name";
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "First Name";
            this.Column4.Name = "Column4";
            this.Column4.Width = 150;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Dr.";
            this.Column5.Name = "Column5";
            this.Column5.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_Dr);
            this.groupBox1.Controls.Add(this.picBtn_BarcodeReader);
            this.groupBox1.Controls.Add(this.lbl_PName);
            this.groupBox1.Controls.Add(this.lbl_PID);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 95);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patient Information";
            // 
            // lbl_Dr
            // 
            this.lbl_Dr.AutoSize = true;
            this.lbl_Dr.ForeColor = System.Drawing.Color.SlateBlue;
            this.lbl_Dr.Location = new System.Drawing.Point(73, 72);
            this.lbl_Dr.Name = "lbl_Dr";
            this.lbl_Dr.Size = new System.Drawing.Size(10, 13);
            this.lbl_Dr.TabIndex = 6;
            this.lbl_Dr.Text = "-";
            // 
            // picBtn_BarcodeReader
            // 
            this.picBtn_BarcodeReader.BackgroundImage = global::Compact_Control.Properties.Resources.Barcode_Normal;
            this.picBtn_BarcodeReader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBtn_BarcodeReader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBtn_BarcodeReader.Location = new System.Drawing.Point(372, 14);
            this.picBtn_BarcodeReader.Name = "picBtn_BarcodeReader";
            this.picBtn_BarcodeReader.Size = new System.Drawing.Size(74, 71);
            this.picBtn_BarcodeReader.TabIndex = 20;
            this.picBtn_BarcodeReader.TabStop = false;
            this.picBtn_BarcodeReader.Click += new System.EventHandler(this.picBtn_BarcodeReader_Click);
            this.picBtn_BarcodeReader.MouseEnter += new System.EventHandler(this.picBtn_BarcodeReader_MouseEnter);
            this.picBtn_BarcodeReader.MouseLeave += new System.EventHandler(this.picBtn_BarcodeReader_MouseLeave);
            // 
            // lbl_PName
            // 
            this.lbl_PName.AutoSize = true;
            this.lbl_PName.ForeColor = System.Drawing.Color.SlateBlue;
            this.lbl_PName.Location = new System.Drawing.Point(73, 48);
            this.lbl_PName.Name = "lbl_PName";
            this.lbl_PName.Size = new System.Drawing.Size(10, 13);
            this.lbl_PName.TabIndex = 5;
            this.lbl_PName.Text = "-";
            // 
            // lbl_PID
            // 
            this.lbl_PID.AutoSize = true;
            this.lbl_PID.ForeColor = System.Drawing.Color.SlateBlue;
            this.lbl_PID.Location = new System.Drawing.Point(73, 24);
            this.lbl_PID.Name = "lbl_PID";
            this.lbl_PID.Size = new System.Drawing.Size(10, 13);
            this.lbl_PID.TabIndex = 4;
            this.lbl_PID.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Dr.:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Patient ID:";
            // 
            // btn_showFieldsForm
            // 
            this.btn_showFieldsForm.Location = new System.Drawing.Point(312, 411);
            this.btn_showFieldsForm.Name = "btn_showFieldsForm";
            this.btn_showFieldsForm.Size = new System.Drawing.Size(158, 46);
            this.btn_showFieldsForm.TabIndex = 9;
            this.btn_showFieldsForm.Text = "View Fields...";
            this.btn_showFieldsForm.UseVisualStyleBackColor = true;
            this.btn_showFieldsForm.Click += new System.EventHandler(this.btn_showFieldsForm_Click);
            // 
            // btn_deletePatient
            // 
            this.btn_deletePatient.ForeColor = System.Drawing.Color.Black;
            this.btn_deletePatient.Location = new System.Drawing.Point(100, 411);
            this.btn_deletePatient.Name = "btn_deletePatient";
            this.btn_deletePatient.Size = new System.Drawing.Size(82, 46);
            this.btn_deletePatient.TabIndex = 18;
            this.btn_deletePatient.Text = "Delete Patient";
            this.btn_deletePatient.UseVisualStyleBackColor = true;
            this.btn_deletePatient.Visible = false;
            this.btn_deletePatient.Click += new System.EventHandler(this.btn_deletePatient_Click);
            // 
            // btn_AddPatient
            // 
            this.btn_AddPatient.ForeColor = System.Drawing.Color.Black;
            this.btn_AddPatient.Location = new System.Drawing.Point(12, 411);
            this.btn_AddPatient.Name = "btn_AddPatient";
            this.btn_AddPatient.Size = new System.Drawing.Size(82, 46);
            this.btn_AddPatient.TabIndex = 17;
            this.btn_AddPatient.Text = "Add Patient...";
            this.btn_AddPatient.UseVisualStyleBackColor = true;
            this.btn_AddPatient.Visible = false;
            this.btn_AddPatient.Click += new System.EventHandler(this.btn_AddPatient_Click);
            // 
            // txt_Search
            // 
            this.txt_Search.Location = new System.Drawing.Point(6, 39);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(223, 20);
            this.txt_Search.TabIndex = 19;
            this.txt_Search.TextChanged += new System.EventHandler(this.txt_Search_TextChanged);
            this.txt_Search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Search_KeyPress);
            // 
            // comboBox_searchIn
            // 
            this.comboBox_searchIn.FormattingEnabled = true;
            this.comboBox_searchIn.Items.AddRange(new object[] {
            "Patient ID",
            "Last Name",
            "First Name",
            "Dr"});
            this.comboBox_searchIn.Location = new System.Drawing.Point(311, 39);
            this.comboBox_searchIn.Name = "comboBox_searchIn";
            this.comboBox_searchIn.Size = new System.Drawing.Size(141, 21);
            this.comboBox_searchIn.TabIndex = 21;
            this.comboBox_searchIn.Text = "Patient ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "Find";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.Location = new System.Drawing.Point(308, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Search in:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txt_Search);
            this.groupBox2.Controls.Add(this.comboBox_searchIn);
            this.groupBox2.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox2.Location = new System.Drawing.Point(12, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 65);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search";
            // 
            // picBtnToolTip
            // 
            this.picBtnToolTip.AutoPopDelay = 5000;
            this.picBtnToolTip.BackColor = System.Drawing.Color.White;
            this.picBtnToolTip.InitialDelay = 500;
            this.picBtnToolTip.ReshowDelay = 100;
            // 
            // btn_EditPatient
            // 
            this.btn_EditPatient.ForeColor = System.Drawing.Color.Black;
            this.btn_EditPatient.Location = new System.Drawing.Point(188, 411);
            this.btn_EditPatient.Name = "btn_EditPatient";
            this.btn_EditPatient.Size = new System.Drawing.Size(82, 46);
            this.btn_EditPatient.TabIndex = 22;
            this.btn_EditPatient.Text = "Edit Patient...";
            this.btn_EditPatient.UseVisualStyleBackColor = true;
            this.btn_EditPatient.Click += new System.EventHandler(this.btn_EditPatient_Click);
            // 
            // Form_PatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 466);
            this.Controls.Add(this.btn_EditPatient);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_deletePatient);
            this.Controls.Add(this.btn_AddPatient);
            this.Controls.Add(this.btn_showFieldsForm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_PatientList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient List";
            this.Activated += new System.EventHandler(this.Form_PatientList_Activated);
            this.Load += new System.EventHandler(this.Form_PatientList_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.Form_PatientList_ControlRemoved);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBtn_BarcodeReader)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Dr;
        private System.Windows.Forms.Label lbl_PName;
        private System.Windows.Forms.Label lbl_PID;
        private System.Windows.Forms.Button btn_showFieldsForm;
        private System.Windows.Forms.Button btn_deletePatient;
        private System.Windows.Forms.Button btn_AddPatient;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.PictureBox picBtn_BarcodeReader;
        private System.Windows.Forms.ComboBox comboBox_searchIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip picBtnToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button btn_EditPatient;

    }
}