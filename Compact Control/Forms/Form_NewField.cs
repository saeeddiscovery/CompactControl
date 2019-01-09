using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compact_Control
{
    public partial class Form_NewField : Form
    {
        public Form_NewField()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearAll();
            this.Close();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            foreach (Control ctrl in groupBox_Field.Controls)
            {
                if (ctrl is TextBox)
                    (ctrl as TextBox).Text = "";
                if (ctrl is ComboBox)
                    (ctrl as ComboBox).Text = "";
            }
            foreach (Control ctrl in groupBox_values.Controls)
            {
                if (ctrl is TextBox)
                    (ctrl as TextBox).Text = "";
                if (ctrl is ComboBox)
                    (ctrl as ComboBox).Text = "";
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "")
            {
                MessageBox.Show("The Field Name can not be empty!");
                txt_name.Focus();
                return;
            }
            foreach (Control ctrl in groupBox_Field.Controls)
            {
                if (ctrl is TextBox)
                    if ((ctrl as TextBox).Text == "")
                        (ctrl as TextBox).Text = "-";
                if (ctrl is ComboBox)
                    if ((ctrl as ComboBox).Text == "")
                        (ctrl as ComboBox).Text = "-";
                if (txt_bolous.Text == "-")
                    txt_bolous.Text = "NO";
            }
            foreach (Control ctrl in groupBox_values.Controls)
            {
                if (ctrl is TextBox)
                    if ((ctrl as TextBox).Text == "")
                        (ctrl as TextBox).Text = "-";
            }

            bool success = false;
            if (Class_PatientData.isInEditField == true)
            {
                Class_PatientData.isInEditField = false;
                Class_PatientData.DeleteField();
                success = Class_PatientData.AddField(txt_name.Text, txt_site.Text, txt_ssd.Text, txt_dose.Text, txt_mu.Text
                    , txt_wedge.Text, txt_shadowTray.Text, txt_bolous.Text, txt_Iso.Text, txt_Column.Text, txt_Vert.Text, txt_Lat.Text, txt_Long.Text
                    , txt_gant.Text, txt_coli.Text, txt_x1.Text, txt_x2.Text, txt_y1.Text, txt_y2.Text);
            }
            else
            {
                success = Class_PatientData.AddField(txt_name.Text, txt_site.Text, txt_ssd.Text, txt_dose.Text, txt_mu.Text
                    , txt_wedge.Text, txt_shadowTray.Text, txt_bolous.Text, txt_Iso.Text, txt_Column.Text, txt_Vert.Text, txt_Lat.Text, txt_Long.Text
                    , txt_gant.Text, txt_coli.Text, txt_x1.Text, txt_x2.Text, txt_y1.Text, txt_y2.Text);
            }
            if (success == true)
            {
                this.DialogResult = DialogResult.OK;
                Class_PatientData.isFieldsChanged = true;
                this.Close();
            }
        }

        private void Form_NewField_Load(object sender, EventArgs e)
        {
            if (Class_PatientData.isInEditField == true)
            {
                txt_name.Text = Class_PatientData.currValues[2];
                txt_site.Text = Class_PatientData.currValues[3];
                txt_ssd.Text = Class_PatientData.currValues[4];
                txt_dose.Text = Class_PatientData.currValues[5];
                txt_mu.Text = Class_PatientData.currValues[6];
                txt_wedge.Text = Class_PatientData.currValues[7];
                txt_shadowTray.Text = Class_PatientData.currValues[8];
                txt_bolous.Text = Class_PatientData.currValues[9];
                txt_Iso.Text = Class_PatientData.currValues[10];
                txt_Column.Text = Class_PatientData.currValues[11];
                txt_Vert.Text = Class_PatientData.currValues[12];
                txt_Lat.Text = Class_PatientData.currValues[13];
                txt_Long.Text = Class_PatientData.currValues[14];
                txt_gant.Text = Class_PatientData.currValues[15];
                txt_coli.Text = Class_PatientData.currValues[16];
                txt_x1.Text = Class_PatientData.currValues[17];
                txt_x2.Text = Class_PatientData.currValues[18];
                txt_y1.Text = Class_PatientData.currValues[19];
                txt_y2.Text = Class_PatientData.currValues[20];
            }
        }

        private void Form_NewField_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class_PatientData.isInEditField = false;
            ClearAll();
        }
    }
}
