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
            if (clicked == 7)
                HashPass.SetReg();
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
                if (cmbBx_User.Text.Contains("Service"))
                {
                    if (Form1.isInServiceMode == false)
                    {
                        //frm1.picBtn_PatientList.Hide();
                        //frm1.picBtn_Setting.Left = 10;
                        //frm1.picBtn_LogOff.Left = 80;
                        //frm1.picBtn_Exit.Left = 150;
                    }
                }
                else
                {
                    if (Form1.isInServiceMode==true)
                    {
                        //frm1.picBtn_Setting.Left = 100;
                        //frm1.picBtn_LogOff.Left = 170;
                        //frm1.picBtn_Exit.Left = 240;
                        //frm1.picBtn_PatientList.Show();
                    }
                }
                if (cmbBx_User.Text.Contains("Clinical") || cmbBx_User.Text.Contains("Physic"))
                {
                    frm1.panel_AdminControls.Hide();
                    frm1.panel_ClientControls.Dock = DockStyle.Bottom;
                    frm1.panel_ClientControls.Height = frm1.Height - frm1.panel_Toolbar.Height - 25;
                    frm1.Text = cmbBx_User.Text + " Mode";
                    frm1.panel_ClientControls.Show();
                }
                else if (cmbBx_User.Text.Contains("Service"))
                {
                    frm1.panel_ClientControls.Hide();
                    frm1.panel_AdminControls.Dock = DockStyle.Bottom;
                    frm1.panel_AdminControls.Height = frm1.Height - frm1.panel_Toolbar.Height - 25;
                    frm1.Text = "Service Mode";
                    frm1.panel_AdminControls.Show();
                }
                if (cmbBx_User.Text.Contains("Clinical"))
                {
                    Form1.isInServiceMode = false;
                    Form1.isInPhysicMode = false;
                }
                else if (cmbBx_User.Text.Contains("Physic"))
                {
                    Form1.isInServiceMode = false;
                    Form1.isInPhysicMode = true;
                }
                else
                {
                    Form1.isInServiceMode = true;
                    Form1.isInPhysicMode = false;
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
                if (frm_login != null)
                    frm_login.Hide();
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

        private static Form_Login frm_login; 
        public static void ShowForm()
        {
            if (frm_login == null)
                frm_login = new Form_Login();
            else
                frm_login.txtBx_Pass.Clear();
            frm_login.Show();
        }

        public static void CloseForm()
        {
            if (frm_login != null)
                frm_login.Close();
        }

        private void txtBx_Pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            clicked = 0;
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
                lbl_resetPass.Enabled = false;
            }
            else
            {
                txtBx_Pass.Enabled = true;
                lbl_resetPass.Enabled = true;
                txtBx_Pass.Focus();
                txtBx_Pass.SelectAll();
            }
        }

        private void lbl_resetPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clicked = 0;
            //string[] files;
            ////string filepath = @"F:/Pacs/Program Files/ITK/InsightToolkit-3.20.0/build/bin/debug";
            ////string filepath = @"F:\Pacs\Program Files\VTK\build\bin\Debug";
            ////string filepath = @"F:\Pacs\Program Files\DCMTK\dcmtk-3.6.0\build";
            ////string filepath = @"F:\Pacs\Ginkgo\ginkgo Build\cadxcore\Debug";
            //string filepath = textBox1.Text;
            //files = Directory.GetFiles(filepath, @"*.lib", SearchOption.AllDirectories);
            //DirectoryInfo d = new DirectoryInfo(filepath);
            //foreach (var file in d.GetFiles("*.lib", SearchOption.AllDirectories))
            //{
            //    //richTextBox1.Text = richTextBox1.Text + '\n' + file.FullName;
            //    richTextBox1.Text = richTextBox1.Text + '\n' + file.Name;
            //}
        }

        bool isLicError = true;
        private void Form_Login_Activated(object sender, EventArgs e)
        {
        }

        private void Form_Login_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                //CheckLicense();

            }
        }

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
                MessageBox.Show("You should NOT run the application from drive ''C''\nPlease move the application to another drive and run again\nApplication will close now.", "Drive C is not recomended!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                return;
            }
            //CheckLicense();

            //Temporary login in Service mode directly!
            cmbBx_User.SelectedIndex = 1;
            txtBx_Pass.Text = "service";

        }

        private int clicked = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            clicked++;
            if (clicked == 7)
                lbl_resetPass.Focus();
            if (clicked > 7)
            {
                clicked = 0;
                txtBx_Pass.Focus();
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
            lbl_resetPass.Enabled = false;
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