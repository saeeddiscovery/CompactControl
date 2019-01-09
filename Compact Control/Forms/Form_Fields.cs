using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Compact_Control.Properties;

namespace Compact_Control
{
    public partial class Form_Fields : Form
    {
        DataGridView dataGrid_Fields = Class_PatientData.dataGrid_Fields;
        public Form_Fields()
        {
            InitializeComponent();
        }
        private bool selectionChanged = false;
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            selectionChanged = true;
            picBtn_AcceptPatient.BackgroundImage = Resources.btn_AcceptPatient;
            picBtn_AcceptPatient.Enabled = true;
            if (dataGridView_Fields.SelectedRows.Count > 0)
            {
                Class_PatientData.currFID = Class_PatientData.dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[0].Value.ToString();
                lbl_FName.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[2].Value.ToString();
                lbl_Site.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[3].Value.ToString();
                lbl_ssd.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[4].Value.ToString();
                lbl_Dose.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[5].Value.ToString();
                lbl_MU.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[6].Value.ToString();
                lbl_Wedge.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[7].Value.ToString();
                lbl_shadowTray.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[8].Value.ToString();
                lbl_Bolous.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[9].Value.ToString();
                lbl_Iso.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[10].Value.ToString();
                lbl_Column.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[11].Value.ToString();
                lbl_Vert.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[12].Value.ToString();
                lbl_Lat.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[13].Value.ToString();
                lbl_Long.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[14].Value.ToString();

                lbl_Gantry.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[15].Value.ToString();
                lbl_Coli.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[16].Value.ToString();
                lbl_X1.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[17].Value.ToString();
                lbl_X2.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[18].Value.ToString();
                lbl_Y1.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[19].Value.ToString();
                lbl_Y2.Text = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[20].Value.ToString();

                Class_PatientData.currValues[0] = Class_PatientData.currFID;
                Class_PatientData.currValues[1] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[1].Value.ToString();
                Class_PatientData.currValues[2] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[2].Value.ToString();
                Class_PatientData.currValues[3] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[3].Value.ToString();
                Class_PatientData.currValues[4] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[4].Value.ToString();
                Class_PatientData.currValues[5] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[5].Value.ToString();
                Class_PatientData.currValues[6] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[6].Value.ToString();
                Class_PatientData.currValues[7] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[7].Value.ToString();
                Class_PatientData.currValues[8] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[8].Value.ToString();
                Class_PatientData.currValues[9] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[9].Value.ToString();
                Class_PatientData.currValues[10] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[10].Value.ToString();
                Class_PatientData.currValues[11] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[11].Value.ToString();
                Class_PatientData.currValues[12] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[12].Value.ToString();
                Class_PatientData.currValues[13] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[13].Value.ToString();
                Class_PatientData.currValues[14] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[14].Value.ToString();
                Class_PatientData.currValues[15] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[15].Value.ToString();
                Class_PatientData.currValues[16] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[16].Value.ToString();
                Class_PatientData.currValues[17] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[17].Value.ToString();
                Class_PatientData.currValues[18] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[18].Value.ToString();
                Class_PatientData.currValues[19] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[19].Value.ToString();
                Class_PatientData.currValues[20] = dataGrid_Fields.Rows[dataGridView_Fields.SelectedRows[0].Index].Cells[20].Value.ToString();
            }
        }

        private void picBtn_AcceptPatient_Click(object sender, EventArgs e)
        {
            if (Form1.isInPhysicMode == false && selectionChanged == true)
            {
                Class_PatientData.valuesChanged = true;
                selectionChanged = false;
            }
            this.Close();
        }

        private void picBtn_AcceptPatient_MouseEnter(object sender, EventArgs e)
        {
            picBtnToolTip.SetToolTip(picBtn_AcceptPatient, "Accept Patient");
            picBtn_AcceptPatient.BackgroundImage = Resources.btn_AcceptPatient_MouseOver;
        }

        private void picBtn_AcceptPatient_MouseLeave(object sender, EventArgs e)
        {
            picBtn_AcceptPatient.BackgroundImage = Resources.btn_AcceptPatient;
        }

        private void btn_AddField_Click(object sender, EventArgs e)
        {
            Form_NewField newFieldFrm = new Form_NewField();
            if (newFieldFrm.ShowDialog() == DialogResult.OK)
                UpdateFieldDataGrid();
        }

        private void Form_Fields_Load(object sender, EventArgs e)
        {
            if (Form1.isInPhysicMode == true)
            {
                btn_AddField.Show();
                btn_deleteField.Show();
                btn_editField.Show();
            }
            else
            {
                btn_AddField.Hide();
                btn_deleteField.Hide();
                btn_editField.Hide();
            }
            lbl_PID.Text = Class_PatientData.currPatient[0];
            lbl_PName.Text = Class_PatientData.currPatient[2] + " " + Class_PatientData.currPatient[1];
            lbl_Dr.Text = Class_PatientData.currPatient[3];
            UpdateFieldDataGrid();
            //dataGridView_Fields.Rows.Clear();
            //for (int i = 0; i < dataGrid_Fields.Rows.Count; i++)
            //{
            //    string gg = dataGrid_Fields.Rows[i].Cells[0].Value.ToString();
            //    dataGridView_Fields.Rows.Add(dataGrid_Fields.Rows[i].Cells[1].Value, dataGrid_Fields.Rows[i].Cells[2].Value);
            //}
        }

        private void btn_deleteField_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete selected Field?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Class_PatientData.currFID = Class_PatientData.currValues[0];
                Class_PatientData.DeleteField();
                Class_PatientData.FindPatientID();
                Class_PatientData.isFieldsChanged = true;
                UpdateFieldDataGrid();
            }
        }

        private void UpdateFieldDataGrid()
        {
            if (Class_PatientData.isFieldsChanged == true)
            {
                Class_PatientData.RefreshPatientList();
                Class_PatientData.FindPatientID();
                dataGridView_Fields.Rows.Clear();
                DataGridViewRow ri;
                for (int i = 0; i < Class_PatientData.dataGrid_Fields.Rows.Count; i++)
                {
                    ri = Class_PatientData.dataGrid_Fields.Rows[i];
                    dataGridView_Fields.Rows.Add(ri.Cells[2].Value, ri.Cells[3].Value, ri.Cells[4].Value
                        , ri.Cells[5].Value, ri.Cells[6].Value, ri.Cells[7].Value, ri.Cells[8].Value, ri.Cells[9].Value, ri.Cells[10].Value
                        , ri.Cells[11].Value, ri.Cells[12].Value, ri.Cells[13].Value, ri.Cells[14].Value, ri.Cells[15].Value, ri.Cells[16].Value, ri.Cells[17].Value, ri.Cells[18].Value, ri.Cells[19].Value, ri.Cells[20].Value);
                }
                Class_PatientData.isFieldsChanged = false;
            }
            dataGridView_Fields.ClearSelection();
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is Label && (ctrl as Label).Name.Contains("lbl_"))
                    (ctrl as Label).Text = "-";
            }
            foreach (Control ctrl in groupBox2.Controls)
            {
                if (ctrl is Label && (ctrl as Label).Name.Contains("lbl_"))
                    (ctrl as Label).Text = "-";
            }
            selectionChanged = false;
            picBtn_AcceptPatient.BackgroundImage = Resources.btn_AcceptPatient_Disabled;
            picBtn_AcceptPatient.Enabled = false;
        }

        private void Form_Fields_Activated(object sender, EventArgs e)
        {
            if (Class_PatientData.isFieldsChanged == true)
                UpdateFieldDataGrid();
        }

        private void dataGridView_Fields_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                if (Form1.isInPhysicMode == false)
                {
                    Class_PatientData.valuesChanged = true;
                    this.Close();
                }
            }
        }

        private void dataGridView_Fields_DoubleClick(object sender, EventArgs e)
        {
            if (Form1.isInPhysicMode == false)
            {
                Class_PatientData.valuesChanged = true;
                this.Close();
            }
        }

        private void btn_editField_Click(object sender, EventArgs e)
        {
            Class_PatientData.isInEditField = true;
            Form_NewField editFieldFrm = new Form_NewField();
            editFieldFrm.ShowDialog();
            //if (Class_PatientData.isFieldsChanged == true)
              //  UpdateFieldDataGrid();
        }
    }
}
