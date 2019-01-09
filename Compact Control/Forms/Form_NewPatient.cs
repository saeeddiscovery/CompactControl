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
    public partial class Form_NewPatient : Form
    {
        public Form_NewPatient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtBx_PID.Text == "")
            {
                MessageBox.Show("The Patient ID can not be empty!");
                txtBx_PID.Focus();
                return;
            }
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is TextBox)
                    if ((ctrl as TextBox).Text == "")
                        (ctrl as TextBox).Text = "-";
            }
            Class_PatientData.isPatientsChanged = false;
            if (Class_PatientData.isInEditPatient == true)
            {
                //Class_PatientData.UpdatePatient(txtBx_LastName.Text, txtBx_FirstName.Text, txtBx_Dr.Text);
                Class_PatientData.DeletePatient();
                Class_PatientData.Insert(txtBx_PID.Text, txtBx_LastName.Text, txtBx_FirstName.Text, txtBx_Dr.Text);
                Class_PatientData.isInEditPatient = false;
                this.DialogResult = DialogResult.OK;
                Class_PatientData.isPatientsChanged = true;
                this.Close();
            }
            else
            {
                if (Class_PatientData.Insert(txtBx_PID.Text, txtBx_LastName.Text, txtBx_FirstName.Text, txtBx_Dr.Text) == true)
                {
                    this.DialogResult = DialogResult.OK;
                    Class_PatientData.isPatientsChanged = true;
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Form_NewPatient_Load(object sender, EventArgs e)
        {
            if (Class_PatientData.isInEditPatient == true)
            {
                txtBx_PID.Text = Class_PatientData.currPatient[0];
                txtBx_PID.Enabled = false;
                txtBx_LastName.Text = Class_PatientData.currPatient[1];
                txtBx_FirstName.Text = Class_PatientData.currPatient[2];
                txtBx_Dr.Text = Class_PatientData.currPatient[3];
            }
            else
            {
                txtBx_PID.Enabled = true;
            }
        }

        private void Form_NewPatient_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class_PatientData.isInEditPatient = false;
            ClearAll();
        }

        private void ClearAll()
        {
            txtBx_PID.Text = "";
            txtBx_FirstName.Text = "";
            txtBx_LastName.Text = "";
            txtBx_Dr.Text = "";
        }
    }
}
