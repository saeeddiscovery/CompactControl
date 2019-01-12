using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Compact_Control.Properties;
using System.IO;
using Microsoft.Win32;


namespace Compact_Control
{
    public partial class Form_Settings : Form
    {
        public Form_Settings()
        {
            InitializeComponent();
        }


        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (checkBox_startup.Checked)
                rk.SetValue(Application.ProductName, Application.ExecutablePath.ToString());
            else
                rk.DeleteValue(Application.ProductName, false);

        }

    private void button_Ok_Click(object sender, EventArgs e)
        {
            SetStartup();
            if (tabControl1.SelectedIndex == 1)
            {
                if (cmbBx_User.Text.Contains("Select"))
                {
                    MessageBox.Show("Please select user", "No User Selected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbBx_User.SelectAll();
                    return;
                }
                if (maskedTextBox_NewPass.Text != maskedTextBox_ConfirmPass.Text && Form1.isInServiceMode == true)
                {
                    MessageBox.Show("Your new password doesn't match with confirm password!", "Match error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBox_ConfirmPass.SelectAll();
                    return;
                }

                int selectedIndex = 0;
                if (cmbBx_User.Text.ToLower() == "service")
                    selectedIndex = 2;
                else if (cmbBx_User.Text.ToLower() == "clinical")
                    selectedIndex = 1;
                string currPass = HashPass.ReadFromReg(selectedIndex);
                if (currPass == "" || HashPass.VerifyHashedPassword(currPass, maskedTextBox_CurrPass.Text))
                {
                    string hashedNewPass = HashPass.HashPassword(maskedTextBox_NewPass.Text);
                    HashPass.WriteToReg(hashedNewPass, selectedIndex);
                    MessageBox.Show("Password changed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Entered password is incorrect!", "Wrong Password!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBox_CurrPass.SelectAll();
                    return;
                }
            }

            string portName = comboBox_Ports.Text;
            if (comboBox_Ports.Text == "" || comboBox_Ports.Text == "No Serial Port!")
                portName = "Null";
            Form1.portName = portName;

            string filename = "portSettings.json";
            string baudrate = comboBox_Baudrate.Text;
            if (File.Exists(filename))
                File.Delete(filename);
            HashPass.writeSettingsJson(filename, portName, baudrate);
            Form1.curr_baudRate = comboBox_Baudrate.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void maskedTextBox_ConfirmPass_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox_ConfirmPass.Text.Length > 0)
                pictureBox_PassConfirm.Show();
            else
                pictureBox_PassConfirm.Hide();
            if (maskedTextBox_ConfirmPass.Text == maskedTextBox_NewPass.Text)
                pictureBox_PassConfirm.BackgroundImage = Resources.green_sign;
            else
                pictureBox_PassConfirm.BackgroundImage = Resources.red_sign;
        }

        private void btn_importLic_Click(object sender, EventArgs e)
        {
            Form_License licForm = new Form_License();
            licForm.Show();
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    try
            //    {
            //        if (MessageBox.Show("Application will close after importing the license\nClick OK to continue", "Application will close!", MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            //        {
            //            string fileName = openFileDialog1.FileName;
            //            string name = openFileDialog1.SafeFileNames[0].Remove(openFileDialog1.SafeFileNames[0].Length - 4, 4);
            //            HashPass.WriteToReg(name);
            //            string winPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            //            string newFileName = Path.Combine(winPath, "clc.clc");
            //            if (File.Exists(newFileName))
            //                File.Delete(newFileName);
            //            File.Move(fileName, newFileName);
            //            MessageBox.Show("License imported!");
            //            HashPass.CheckLicense();
            //            refreshLicInfo();
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("License import error!");
            //    }
            //}
        }

        private void Form_Settings_Activated(object sender, EventArgs e)
        {
            string keyName = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            string valueName = Application.ProductName;

            using (RegistryKey Key = Registry.CurrentUser.OpenSubKey(keyName))
                if (Key != null)
                {
                    string[] val = Key.GetValueNames();
                    if (!val.Contains(valueName))
                    {
                        checkBox_startup.Checked = false;
                    }
                    else
                    {
                        checkBox_startup.Checked = true;
                    }
                }

            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                for (int i = 0; i < ports.Length; i++)
                    comboBox_Ports.Items.Add(ports[0]);
                comboBox_Ports.SelectedIndex = 0;
                comboBox_Baudrate.Text = Form1.curr_baudRate;
            }
            else
                comboBox_Ports.Text = "No Serial Port!";

            HashPass.refreshLicInfo();
            lbl_name.Text = HashPass.licenseName;
            lbl_licType.Text = HashPass.licenseType;
            if (HashPass.licenseType == "Expired!")
            {
                lbl_licType.ForeColor = Color.Red;
            }
            else
            {
                lbl_licType.ForeColor = Color.Navy;
            }

            cmbBx_User.Items.Clear();
            if (Form1.isInServiceMode)
            {
                cmbBx_User.Items.Add("Service");
                cmbBx_User.Text = "Service";
            }
            else
            {
                cmbBx_User.Items.Add("Clinical");
                cmbBx_User.Text = "Clinical";
            }
        }


        private void comboBox_Ports_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename = "portSettings.json";

            string port = comboBox_Ports.Text;
            string baudrate = comboBox_Baudrate.Text;

            if (File.Exists(filename))
                File.Delete(filename);

            HashPass.writeSettingsJson(filename, port, baudrate);
        }

        private void Form_Settings_Load(object sender, EventArgs e)
        {
            string filename = "portSettings.json";
            if (File.Exists(filename))
            {
                //string[] portSettings = readJson(filename);
                HashPass.PortSettings pSettings = HashPass.readSettingsJson(filename);
                //comboBox_Ports.Text = pSettings.Port;
                comboBox_Baudrate.Text = pSettings.Baudrate;
            }

        }
    }
}
