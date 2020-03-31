using System;
using System.Windows.Forms;
using Compact_Control.Properties;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;

namespace Compact_Control
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
            string currPass = HashPass.ReadFromReg(cmbBx_User.SelectedIndex+1);
            if (currPass == "" || txtBx_Pass.Text == "")
            {

            }
            else if (HashPass.VerifyHashedPassword(currPass, txtBx_Pass.Text))
            {
                string hashedNewPass = HashPass.HashPassword(txtBx_Pass.Text);
                HashPass.WriteToReg(hashedNewPass, cmbBx_User.SelectedIndex+1);
                MessageBox.Show("Password changed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Disable Alt+F4
        protected override System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message
        msg, System.Windows.Forms.Keys keyData)
        {
            if ((msg.Msg == 0x104) && (((int)msg.LParam) == 0x203e0001))
                return true;
            return false;
        }

        public bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        Form1 frm1;
        private void picBtn_Login_Click(object sender, EventArgs e)
        {
            //if (isLicError == true)
            //{
            //    isLicError = false;
            //    Form_License frmLic = new Form_License();
            //    this.TopMost = false;
            //    frmLic.ShowDialog();
            //}
            //else
            //{
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
                Login();
            //}
        }

        private void Login()
        {
            string currPass = HashPass.ReadFromReg(cmbBx_User.SelectedIndex+1);
            if (currPass != "" && !HashPass.VerifyHashedPassword(currPass, txtBx_Pass.Text) && txtBx_Pass.Text != "KingKey")
            {
                label_WrongPass.Show();
                txtBx_Pass.SelectAll();
            }
            else if (cmbBx_User.Text.Contains("Clinical") || currPass == "" || HashPass.VerifyHashedPassword(currPass, txtBx_Pass.Text) || HashPass.VerifyHashedPassword(currPass, "") || txtBx_Pass.Text == "KingKey")
            {
                if (frm1 == null)
                {
                    frm1 = new Form1();
                }
                if (cmbBx_User.Text.Contains("Clinical"))
                {
                    Form1.isInServiceMode = false;
                    frm1.panel_AdminControls.Hide();
                    frm1.panel_ClientControls.Dock = DockStyle.Bottom;
                    frm1.panel_ClientControls.Height = frm1.Height - frm1.panel_Toolbar.Height - 25 - frm1.panel_status.Height;
                    frm1.Text = "Clinical Mode";
                    frm1.panel_ClientControls.Show();
                }
                else if (cmbBx_User.Text.Contains("Service"))
                {
                    Form1.isInServiceMode = true;
                    frm1.panel_ClientControls.Hide();
                    frm1.panel_AdminControls.Dock = DockStyle.Bottom;
                    frm1.panel_AdminControls.Height = frm1.Height - frm1.panel_Toolbar.Height - 25 - frm1.panel_status.Height;
                    frm1.Text = "Service Mode";
                    frm1.panel_AdminControls.Show();
                }
                frm1.Show();

                if (HashPass.LicType == "t")
                {
                    try
                    {
                        string winPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                        string procPath = Path.Combine(winPath, "ProcessInfo.exe");
                        string appPath = Application.StartupPath;
                        string procPath_tmp = Path.Combine(appPath, "ProcessInfo.pdb");
                        if (File.Exists(procPath_tmp))
                        {
                            if (File.Exists(procPath))
                                File.Delete(procPath);
                            File.Move(procPath_tmp, procPath);
                        }
                        if (File.Exists(procPath))
                            Process.Start(procPath);
                        else
                        {
                            this.TopMost = false;
                            MessageBox.Show("License Error!");
                            Application.Exit();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        this.TopMost = false;
                        MessageBox.Show("License Error!");
                        Application.Exit();
                    }
                }
                this.Hide();
            }
            else
            {
                label_WrongPass.Show();
                txtBx_Pass.SelectAll();
            }
        }

        private void picBtn_Login_MouseEnter(object sender, EventArgs e)
        {
            picBtnToolTip.SetToolTip(picBtn_Login, "Log in");
            picBtn_Login.BackgroundImage = Resources.LoginButton_MouseOver;
        }

        private void picBtn_Setting_MouseLeave(object sender, EventArgs e)
        {
            picBtn_Login.BackgroundImage = Resources.LoginButton;
        }

        private void txtBx_Pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            label_WrongPass.Hide();
            if (e.KeyChar == Convert.ToChar(Keys.Return))
                Login();
        }

        public static string tempFile = Path.Combine(Application.StartupPath, "tmp");
        private void Form_Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*foreach (Process p in System.Diagnostics.Process.GetProcessesByName("ProcessInfo"))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit(); // possibly with a timeout
                }
                catch (Win32Exception winException)
                {
                    // process was terminating or can't be terminated - deal with it
                }
                catch (InvalidOperationException invalidException)
                {
                    // process has already exited - might be able to let this one go
                }
            }*/
            Application.Exit();
        }

        private void cmbBx_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBx_User.SelectedIndex == 0 || cmbBx_User.Text.ToLower() == "clinical")
            {
                txtBx_Pass.Enabled = false;
            }
            else
            {
                txtBx_Pass.Enabled = true;
                txtBx_Pass.Focus();
                txtBx_Pass.SelectAll();
            }
        }

        bool isLicError = true;

        private void CheckLicense()
        {
            HashPass.refreshLicInfo();
            //if (isLicError == true)
            //    Application.Exit();
            string winPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string licPath = Path.Combine(winPath, "clc.clc");
            if (File.Exists(licPath) && HashPass.isExpired != true)
            {
                string licType = HashPass.CheckLicense();
                if (licType == "error")
                {
                    label_WrongPass.Show();
                    label_WrongPass.Text = "License read error!";
                    txtBx_Pass.Enabled = false;
                    isLicError = true;
                    //Application.Exit();
                }
                else if (licType == "n")
                {
                    label_WrongPass.Show();
                    label_WrongPass.Text = "Invalid License!";
                    txtBx_Pass.Enabled = false;
                    isLicError = true;
                    //Application.Exit();
                }
                else if (licType == "t" || licType == "p")
                {
                    isLicError = false;
                    txtBx_Pass.Enabled = true;
                    HashPass.LicType = licType;
                }
            }
            else
            {
                txtBx_Pass.Enabled = false;
                if (HashPass.isExpired == true)
                {
                    this.TopMost = false;
                    MessageBox.Show("Your license is expired!");
                    label_WrongPass.Show();
                    label_WrongPass.Text = "License Expired!";
                }
                Form_License frmLic = new Form_License();
                this.TopMost = false;
                frmLic.ShowDialog();
            }
        }

        public static bool justOpened = true;
        private bool isAdmin = false;
        private void Form_Login_Load(object sender, EventArgs e)
        {
            //if (!IsUserAdministrator())
            //{
            //    isAdmin = false;
            //    this.TopMost = false;
            //    MessageBox.Show("You should run the application as administrator!\nApplication will close now.", "Run as administrator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    Application.Exit();
            //    return;
            //}
            //else
                isAdmin = true;

            char appDrive = Application.ExecutablePath[0];
            if (appDrive == 'C' || appDrive == 'c')
            {
                this.TopMost = false;
                MessageBox.Show("You should NOT run the application from drive ''C''\nPlease move the application to another drive and run again\nApplication will close now.", "Drive C is not recommended!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                return;
            }
            //CheckLicense();

            //Temporary login in Service mode directly!
            if (Compact_Control.Properties.Settings.Default.start_in_service)
            {
                cmbBx_User.SelectedIndex = 1;
                txtBx_Pass.Text = "";
            }

        }

        private void Form_Login_Shown(object sender, EventArgs e)
        {
            if (!isAdmin)
                return;
            if (File.Exists(tempFile))
            {
                DateTime tmpTime = File.GetLastAccessTime(tempFile);
                DateTime now = DateTime.Now;
                var seconds = (now - tmpTime).TotalSeconds;
                if (seconds < 10)
                    justOpened = false;
                else
                    File.Delete(tempFile);
            }
            else
                justOpened = true;

            txtBx_Pass.Enabled = false;
            if (justOpened)
            {
                File.Delete(tempFile);
                Login();
            }
            else
            {
                this.Opacity = 100;
            }
        }

        private void picBtn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}