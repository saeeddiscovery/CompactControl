using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Compact_Control.Properties;

namespace Compact_Control
{
    public partial class Form_PatientList : Form
    {
        OleDbConnection cn = Class_PatientData.cn;
        OleDbCommand cmd = Class_PatientData.cmd;
        DataGridView dataGrid_Fields = Class_PatientData.dataGrid_Fields;
        DataGridView dataGrid_Patients = Class_PatientData.dataGrid_Patients;

        public Form_PatientList()
        {
            InitializeComponent();
            InitializeFieldsDataGrid();
            InitializePatientsDataGrid();
        }

        private void InitializePatientsDataGrid()
        {
            dataGrid_Patients.Columns.Add("ID", "#");
            dataGrid_Patients.Columns.Add("PID", "Patient ID");
            dataGrid_Patients.Columns.Add("LName", "Last Name");
            dataGrid_Patients.Columns.Add("FName", "First Name");
            dataGrid_Patients.Columns.Add("FName", "Field Name");
            dataGrid_Patients.Columns.Add("Dr", "Dr.");
        }
        private void InitializeFieldsDataGrid()
        {
            dataGrid_Fields.Columns.Add("ID", "#");
            dataGrid_Fields.Columns.Add("PID", "Patient ID");
            dataGrid_Fields.Columns.Add("FName", "Field Name");
            dataGrid_Fields.Columns.Add("Site", "SITE");
            dataGrid_Fields.Columns.Add("SSD", "SSD");
            dataGrid_Fields.Columns.Add("Dose", "DOSE");
            dataGrid_Fields.Columns.Add("MU", "MU");
            dataGrid_Fields.Columns.Add("Wedge", "WEDGE");
            dataGrid_Fields.Columns.Add("ShadowTray", "Shadow Tray");
            dataGrid_Fields.Columns.Add("Bolous", "BOLOUS");
            dataGrid_Fields.Columns.Add("Iso", "Iso");
            dataGrid_Fields.Columns.Add("Column", "Column");
            dataGrid_Fields.Columns.Add("Vert", "Vert");
            dataGrid_Fields.Columns.Add("Lat", "Lat");
            dataGrid_Fields.Columns.Add("Long", "Long");
            dataGrid_Fields.Columns.Add("Gant", "Gantry");
            dataGrid_Fields.Columns.Add("Coli", "Colimator");
            dataGrid_Fields.Columns.Add("X1", "X1");
            dataGrid_Fields.Columns.Add("X2", "X2");
            dataGrid_Fields.Columns.Add("Y1", "Y1");
            dataGrid_Fields.Columns.Add("Y2", "Y2");
        }
        //private static Form_PatientList form_PatientList;
        //public static void Show(bool tt)
        //{
        //    form_PatientList = new Form_PatientList();
        //    form_PatientList.Show();
        //}
        public void ClearSelection()
        {
            dataGrid_Patients.ClearSelection();
            dataGrid_Fields.ClearSelection();
        }


        private void Form_PatientList_Load(object sender, EventArgs e)
        {
            if (Form1.isInPhysicMode == true)
            {
                btn_AddPatient.Show();
                btn_deletePatient.Show();
                btn_EditPatient.Show();
            }
            else
            {
                btn_AddPatient.Hide();
                btn_deletePatient.Hide();
                btn_EditPatient.Hide();
            }
            UpdatePatientDataGrid();

                //using (OleDbCommand cmd = new OleDbCommand())
                //{
                //    cn.Open();
                //    cmd.CommandText = "SELECT * FROM Table_Patients";
                //    cmd.Connection = cn;
                //    using (OleDbDataReader dr = cmd.ExecuteReader())
                //    {
                //        while (dr.Read())
                //        {
                //            dataGridView1.Rows.Add(dr[0].ToString(), dr[4].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                //        }
                //        //dr.Close();
                //    }
                //}
        }

        private void UpdatePatientDataGrid()
        {
            Class_PatientData.RefreshPatientList();
            dataGridView1.Rows.Clear();
            DataGridViewRow ri;
            for (int i = 0; i < Class_PatientData.dataGrid_Patients.Rows.Count-1; i++)
            {
                ri = Class_PatientData.dataGrid_Patients.Rows[i];
                dataGridView1.Rows.Add(ri.Cells[1].Value, ri.Cells[2].Value, ri.Cells[3].Value, ri.Cells[4].Value);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            dataGrid_Fields.AllowUserToAddRows = false;

            string PID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Class_PatientData.currPID = PID;
            Class_PatientData.currPatient[0] = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Class_PatientData.currPatient[1] = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Class_PatientData.currPatient[2] = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Class_PatientData.currPatient[3] = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            if (Class_PatientData.FindPatientID() == false)
                return;

            lbl_PID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            lbl_PName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString() + " " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            lbl_Dr.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btn_AddField_Click(object sender, EventArgs e)
        {
            Form_NewField newFieldFrm = new Form_NewField();
            newFieldFrm.Show();
        }

        private void Form_PatientList_Activated(object sender, EventArgs e)
        {
            //UpdatePatientDataGrid();
            if (Class_PatientData.barcodeReaderUsed == true)
            {
                Class_PatientData.barcodeReaderUsed = false;
                txt_Search.Text = Class_PatientData.searchPhrase;
                if (FindPatient() == true)
                    ShowFields();
                else
                    MessageBox.Show("There is no Patient with entered " + comboBox_searchIn.Text + "!","Nothing found!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private bool FindPatient()
        {
            var found = (from DataGridViewRow r in dataGridView1.Rows where r.Cells[comboBox_searchIn.SelectedIndex].Value.ToString() == txt_Search.Text select r).FirstOrDefault();

            if (found != null)
            {
                dataGridView1.Rows[found.Index].Selected = true;
                return true;
                //Do Something to x
                // x is your row
                // x == null when not found
            }
            else
                return false;
        }

        private void btn_showFieldsForm_Click(object sender, EventArgs e)
        {
            ShowFields();
        }

        private void ShowFields()
        {
            Class_PatientData.isFieldsChanged = true;
            Form_Fields fieldsFrm = new Form_Fields();
            fieldsFrm.ShowDialog();
            fieldsFrm.dataGridView_Fields.ClearSelection();
            if (Form1.isInPhysicMode == false)
                this.Close();
        }

        private void btn_AddPatient_Click(object sender, EventArgs e)
        {
            Form_NewPatient newPatientFrm = new Form_NewPatient();
            if (newPatientFrm.ShowDialog() == DialogResult.OK)
                UpdatePatientDataGrid();
        }

        private void btn_deletePatient_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete selected patient?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Class_PatientData.currPID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                Class_PatientData.DeletePatient();
                UpdatePatientDataGrid();
            }
        }

        private void btn_EditPatient_Click(object sender, EventArgs e)
        {
            Class_PatientData.isInEditPatient = true;
            Form_NewPatient editPatientFrm = new Form_NewPatient();
            editPatientFrm.ShowDialog();
            if (Class_PatientData.isPatientsChanged == true)
                UpdatePatientDataGrid();
        }

        private void picBtn_BarcodeReader_MouseEnter(object sender, EventArgs e)
        {
            picBtnToolTip.SetToolTip(picBtn_BarcodeReader, "Search by Barcode Reader");
            picBtn_BarcodeReader.BackgroundImage = Resources.Barcode_MouseOver;
        }

        private void picBtn_BarcodeReader_MouseLeave(object sender, EventArgs e)
        {
            picBtn_BarcodeReader.BackgroundImage = Resources.Barcode_Normal;
        }

        private void picBtn_BarcodeReader_Click(object sender, EventArgs e)
        {
            //PictureBox barcodePic = new PictureBox();
            //barcodePic.Size = new Size(386, 364);
            //barcodePic.Location = new Point(11, 11);
            //this.Controls.Add(barcodePic);
            //barcodePic.BringToFront();
            //barcodePic.BackgroundImage = Resources.BarcodeScan;
            //barcodePic.BackgroundImageLayout = ImageLayout.Zoom;

            BarcodeScan barcodeScan = new BarcodeScan();
            this.Controls.Add(barcodeScan);
            barcodeScan.Location = new Point(11, 11);
            barcodeScan.Show();
            barcodeScan.BringToFront();
        }

        private void txt_Search_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && e.KeyChar == Convert.ToChar(Keys.Return))
            {
                FindPatient();
                ShowFields();
            }
        }

        private void Form_PatientList_ControlRemoved(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            this.OnActivated(e);
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            FindPatient();
        }

        private void dataGridView1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                if (dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1)
                {
                    int currentRow = dataGridView1.SelectedRows[0].Index - 1;
                    dataGridView1.CurrentCell = dataGridView1.Rows[currentRow].Cells[0];
                }
                ShowFields();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowFields();
        }

    }
}
