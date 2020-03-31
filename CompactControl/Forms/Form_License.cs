using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Compact_Control
{
    public partial class Form_License : Form
    {
        public Form_License()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("GenerateHardwareID.exe"))
            {
                Process.Start("GenerateHardwareID.exe");
            }
            else
            {
                MessageBox.Show("ID generator application (GenerateHardwareID.exe) not found!");
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    //if (MessageBox.Show("Application will close after importing the license\nClick OK to continue", "Application will close!", MessageBoxButtons.OK, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                    //{
                        string fileName = openFileDialog1.FileName;
                        string name = openFileDialog1.SafeFileNames[0].Remove(openFileDialog1.SafeFileNames[0].Length - 4, 4);
                        HashPass.WriteToReg(name);
                        string winPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                        string newFileName = Path.Combine(winPath, "clc.clc");
                        if (File.Exists(newFileName))
                            File.Delete(newFileName);
                        File.Move(fileName, newFileName);
                        if (HashPass.LicType == "p")
                        {
                            foreach (Process p in System.Diagnostics.Process.GetProcessesByName("ProcessInfo"))
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
                            }
                        }
                        HashPass.CheckLicense();
                        MessageBox.Show("License imported!");
                        this.Close();
                    }
                //}
                catch
                {
                    MessageBox.Show("License import error!");
                }
            }
        }

    }
}
