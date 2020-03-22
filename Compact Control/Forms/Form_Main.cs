using Compact_Control.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Compact_Control
{
    public partial class Form1 : Form
    {
        public static string portName = "Null";
        public static string curr_baudRate = "";
        public static string showClinicalTerminals = "0";
        public static string DataBits = "8";
        public static string Parity = "None";

        public string gant_co;
        public string gant_f1;
        public string gant_f2;
        public string gant_cofin;
        public double gant_cofin_temp1;
        public double gant_cofin_temp2;
        public double gant_gain;
        public double gant_offset;
        public double gant_gain_temp;
        public double gant_offset_temp;
        public string gant_dv = "";
        public string gnd;
        public string gant_zpnt;
        public string gant_length;
        public string gant_fine_length;

        public string collim_co;
        public string collim_f1;
        public string collim_f2;
        public string collim_cofin;
        public double collim_cofin_temp1;
        public double collim_cofin_temp2;
        public double collim_gain;
        public double collim_offset;
        public double collim_gain_temp;
        public double collim_offset_temp;
        public string collim_dv = "";
        public string cld;
        public string collim_zpnt;
        public string collim_length;
        public string collim_fine_length;

        public string gant_tol_1, gant_tol0, gant_tol1, gant_tol2, gant_v1, gant_v2, gant_v3;
        public string collim_tol_1, collim_tol0, collim_tol1, collim_tol2, collim_v1, collim_v2, collim_v3;
        public string x1_tol_1, x1_tol0, x1_tol1, x1_tol2, x1_v1, x1_v2, x1_v3;
        public string x2_tol_1, x2_tol0, x2_tol1, x2_tol2, x2_v1, x2_v2, x2_v3;
        public string y1_tol_1, y1_tol0, y1_tol1, y1_tol2, y1_v1, y1_v2, y1_v3;
        public string y2_tol_1, y2_tol0, y2_tol1, y2_tol2, y2_v1, y2_v2, y2_v3;
        public string gravity_up, gravity_down;
        public bool sendParametersFlag = false;

        public  string x1_co;
        public double x1_co_temp1;
        public double x1_co_temp2;
        public double x1_gain = 0;
        public double x1_offset = 0;
        public double x1_gain_temp;
        public double x1_offset_temp;
        public string x1_dv = "";
        public string x1d;

        public string x2_co;
        public double x2_co_temp1;
        public double x2_co_temp2;
        public double x2_gain = 0;
        public double x2_offset = 0;
        public double x2_gain_temp;
        public double x2_offset_temp;
        public string x2_dv = "";
        public string x2d;

        public string y1_co;
        public double y1_co_temp1;
        public double y1_co_temp2;
        public double y1_gain = 0;
        public double y1_offset;
        public double y1_gain_temp;
        public double y1_offset_temp;
        public string y1_dv = "";
        public string y1d;


        public string y2_co;
        public double y2_co_temp1;
        public double y2_co_temp2;
        public double y2_gain = 0;
        public double y2_offset = 0;
        public double y2_gain_temp;
        public double y2_offset_temp;
        public string y2_dv = "";
        public string y2d;

        public string gant_set = "0";
        public string collim_set = "0";
        public string x1_set = "0";
        public string x2_set = "0";
        public string y1_set = "0";
        public string y2_set = "0";

        bool in_diag = false;

        public string adc;

        public static bool quit = false;

        Image errorImage = Resources.Error;
        Image requestImage = Resources.Request;

        public static bool isInServiceMode = false;

        private ClientControls clientFrm = new ClientControls();

        private DateTime startTime = DateTime.Now;
        public DateTime connectTime;
        private DateTime initTime;
        private DateTime sendAndTime;
        private int connCounter = 0;
        private int initCounter = 0;
        public double TotalVisibleMemorySize;
        public double FreePhysicalMemory;
        public double TotalVirtualMemorySize;
        public double FreeVirtualMemory;
        public bool validPort = false;
        public Form1()
        {
            InitializeComponent();

            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                TotalVisibleMemorySize = double.Parse(result["TotalVisibleMemorySize"].ToString()) / 1024;
                FreePhysicalMemory = double.Parse(result["FreePhysicalMemory"].ToString()) / 1024;
                TotalVirtualMemorySize = double.Parse(result["TotalVirtualMemorySize"].ToString()) / 1024;
                FreeVirtualMemory = double.Parse(result["FreeVirtualMemory"].ToString()) / 1024;
            }

            if (isInServiceMode == false)
            {
                label_title.Text = "Clinical";
                if (panel_ClientControls.Controls.Count != 3)
                {
                    clientFrm = new ClientControls();
                    clientFrm.Dock = DockStyle.Top;
                    panel_ClientControls.Controls.Add(clientFrm);
                }
            }
            else
            {
                label_title.Text = "Service";
                //GlobalSerialPort = serialPort1;
                //GlobalSerialPort.DataReceived += serialPort1_DataReceived_1;
            }
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length == 0)
            {
                label_ConnectStatus.Text = "No Serial port detected!";
                label_ConnectStatus.ForeColor = Color.Red;
                //MessageBox.Show("No Serial port detected!", "COM Port error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ports.Length >= 1)
            {
                if (serialPort1.IsOpen == false)
                {
                    string filename = "Settings.json";
                    try
                    {
                        if (System.IO.File.Exists(filename))
                        {
                            //string[] portSettings = readJson(filename);
                            HashPass.AppSettings pSettings = HashPass.readSettingsJson(filename);
                            // HashPass.WriteBaudrateToReg(pSettings.Baudrate);

                            portName = pSettings.Port;
                            curr_baudRate = pSettings.Baudrate;
                            DataBits = pSettings.DataBits;
                            Parity = pSettings.Parity;

                            serialPort1.PortName = pSettings.Port;
                            serialPort1.BaudRate = int.Parse(pSettings.Baudrate);
                            serialPort1.DataBits = int.Parse(pSettings.DataBits);
                            if (Parity == "None")
                                serialPort1.Parity = System.IO.Ports.Parity.None;
                            else if (Parity == "Odd")
                                serialPort1.Parity = System.IO.Ports.Parity.Odd;
                            else if (Parity == "Even")
                                serialPort1.Parity = System.IO.Ports.Parity.Even;
                            Form1.showClinicalTerminals = pSettings.clinicalTerminals;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading settings from file!" + Environment.NewLine + ex.ToString());
                    }

                    foreach (string prt in ports)
                    {
                        if (prt == portName)
                            validPort = true;
                    }
                    if (portName == "Null" || portName == "" || validPort == false)
                    {
                        MessageBox.Show("Port name is invalid\nConnected to " + ports[0], "Invalid Port Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        portName = ports[0];
                        serialPort1.PortName = ports[0];
                    }
                }
            }
        }


        // Disable Alt+F4
        protected override System.Boolean ProcessCmdKey(ref System.Windows.Forms.Message
        msg, System.Windows.Forms.Keys keyData)
        {
            if (!isInServiceMode)
                if ((msg.Msg == 0x104) && (((int)msg.LParam) == 0x203e0001))
                    return true;
            return false;
        }


        public void KillCtrlAltDelete()
        {
            //RegistryKey regkey;
            //string keyValueInt = "1";
            //string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

            //try
            //{
            //    regkey = Registry.CurrentUser.CreateSubKey(subKey);
            //    regkey.SetValue("DisableTaskMgr", keyValueInt);
            //    regkey.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }


        public static void EnableCTRLALTDEL()
        {
            try
            {
                string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
                RegistryKey rk = Registry.CurrentUser;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 != null)
                    rk.DeleteSubKeyTree(subKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void Board(bool isLock)
        {
        }

        public void write(string data)
        {
            //serialPort1.DiscardOutBuffer();
            serialPort1.Write(data);
            writeOutQ.Enqueue(data);
        }

        public void writeInQtoTerminal()
        {
            if (writeInQ.Count == 0)
                return;

            while (writeInQ.Count > 0)
            {
                string data = writeInQ.Dequeue();

                if (isInServiceMode)
                    tb_terminal_in.AppendText(data + Environment.NewLine);
                else if (showClinicalTerminals == "1")
                    clientFrm.tb_terminal_in.AppendText(data + Environment.NewLine);
            }
        }
        public void writeOutQtoTerminal()
        {
            if (writeOutQ.Count == 0)
                return;

            while (writeOutQ.Count > 0)
            {
                string data = writeOutQ.Dequeue();

                if (isInServiceMode)
                {
                    int o = int.Parse(lbl_out_cnt.Text);
                    o = o + 1;
                    lbl_out_cnt.Text = o.ToString();

                    if (int.Parse(lbl_out_cnt.Text) > 100)
                    {
                        lbl_out_cnt.Text = "0";
                        tb_terminal_out.Clear();
                    }
                    tb_terminal_out.AppendText(data + Environment.NewLine);
                }
                else if (showClinicalTerminals == "1")
                {
                    int o = int.Parse(clientFrm.lbl_out_cnt.Text);
                    o = o + 1;
                    clientFrm.lbl_out_cnt.Text = o.ToString();

                    if (int.Parse(clientFrm.lbl_out_cnt.Text) > 100)
                    {
                        clientFrm.lbl_out_cnt.Text = "0";
                        clientFrm.tb_terminal_out.Clear();
                    }
                    clientFrm.tb_terminal_out.AppendText(data + Environment.NewLine);
                }
            }
        }

        public void writeToOtherTerminal_service(string text, bool isCatch)
        {
            try
            {
                if ((text.Length >= 3) && (text.Substring(0, 3) != "sss" && text.Substring(0, 3) != "ccc"))
                {
                    if (isCatch)
                        tb_terminal_oth.AppendText("* " + text + Environment.NewLine);
                    else
                        tb_terminal_oth.AppendText(text + Environment.NewLine);
                    int o = int.Parse(lbl_oth_cnt.Text);
                    o = o + 1;
                    lbl_oth_cnt.Text = o.ToString();

                    if (o > 100)
                    {
                        lbl_oth_cnt.Text = "0";
                        tb_terminal_oth.Clear();
                    }
                }
            }
            catch
            {
                tb_terminal_oth.AppendText("-- Error --" + Environment.NewLine);
            }
        }

        public void writeToOtherTerminal(string text, bool isCatch)
        {
            if (isInServiceMode)
                writeToOtherTerminal_service(text, isCatch);
            else
                clientFrm.writeToOtherTerminal(text, isCatch);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("a");
            write(trackBar_gant.Value.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("b");
            write(trackBar_coli.Value.ToString());
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("c");
            write(trackBar_x1.Value.ToString());
        }


        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("d");
            write(trackBar_x2.Value.ToString());
        }


        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("e");
            write(trackBar_y1.Value.ToString());
        }

        private void button11_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("f");
            write(trackBar_y2.Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("g");
            write(trackBar_gant.Value.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("h");
            write(trackBar_coli.Value.ToString());
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("i");
            write(trackBar_x1.Value.ToString());
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("j");
            write(trackBar_x2.Value.ToString());
        }

        private void button12_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("k");
            write(trackBar_y1.Value.ToString());
        }

        private void button13_MouseDown(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("l");
            write(trackBar_y2.Value.ToString());
        }


        private void button6_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button9_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button10_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button12_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button11_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button13_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (btn_start_stop.Text == "Start")
            {
                write("S$");
                in_diag = true;
                btn_start_stop.Text = "Stop";
                groupBox7.Enabled = true;
                timer1.Stop();
            }
            else
            {
                if (serialPort1.IsOpen == false)
                    serialPort1.Open();
                write("s");
                in_diag = false;
                btn_start_stop.Text = "Start";
                groupBox7.Enabled = false;
                timer1.Start();
            }
        }


        private void button6_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            write("s");
        }

        //private void button15_Click(object sender, EventArgs e)
        //{
        //    if (serialPort1.IsOpen == true)
        //        serialPort1.Close();
        //}

        private System.Diagnostics.Stopwatch watch;
        private bool watchStarted = false;
        private long maxTime = 0;
        private long minTime = 10000;
        public void startTimeMeasure()
        {
            if (!watchStarted)
                watch = new System.Diagnostics.Stopwatch();
            watch.Start();
        }

        public void stopTimeMeasure()
        {
            watch.Stop();
            if (watch.ElapsedMilliseconds > maxTime)
                maxTime = watch.ElapsedMilliseconds;
            if (watch.ElapsedMilliseconds < minTime)
                minTime = watch.ElapsedMilliseconds;
            writeToOtherTerminal($"Max: {maxTime} ms", false);
            writeToOtherTerminal($"Min: {minTime} ms", false);
            writeToOtherTerminal("", false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            writeInQtoTerminal();
            writeOutQtoTerminal();

            //if (sendParametersFlag == true)
            //{
            //sendParameters();
            //sendParametersFlag = false;

            //if (sendParameters() == true)
            //{
            //    MessageBox.Show("Parameters Save & Send successful!");
            //}
            //}

            textBox1.Text = gant_co;
            textBox7.Text = gant_f1;
            textBox8.Text = gant_f2;
            textBox43.Text = gant_cofin;

            textBox2.Text = collim_co;
            textBox9.Text = collim_f1;
            textBox10.Text = collim_f2;
            textBox44.Text = collim_cofin;

            tb_x1_co.Text = x1_co;
            tb_x2_co.Text = x2_co;
            tb_y1_co.Text = y1_co;
            tb_y2_co.Text = y2_co;

            textBox31.Text = gant_dv;
            textBox32.Text = collim_dv;
            textBox33.Text = x1_dv;
            textBox34.Text = x2_dv;
            textBox35.Text = y1_dv;
            textBox36.Text = y2_dv;
            adcheck.Text = adc;

            if (isGantSet)
                gantSet();
            if (isColiSet)
                coliSet();
            if (isY1Set)
                y1Set();
            if (isY2Set)
                y2Set();
            if (isX1Set)
                x1Set();
            if (isX2Set)
                x2Set();

            //textBox42_TextChanged(sender, e);
            //textBox41_TextChanged(sender, e);
            //textBox40_TextChanged(sender, e);
            //textBox39_TextChanged(sender, e);
            //textBox38_TextChanged(sender, e);
            //textBox37_TextChanged(sender, e);


            switch (comboBox1.Text)
            {
                case "Gantry":
                    //if (textBox13.Enabled & textBox13.ReadOnly == false & checkBox1.Checked == false)
                    textBox13.Text = gant_dv;
                    //if (textBox16.Enabled & textBox16.ReadOnly == false & checkBox2.Checked == false)
                    textBox16.Text = gant_dv;
                    break;
                case "Collimator":
                    // if (textBox13.Enabled & textBox13.ReadOnly == false & checkBox1.Checked == false)
                    textBox13.Text = collim_dv;
                    // if (textBox16.Enabled & textBox16.ReadOnly == false & checkBox2.Checked == false)
                    textBox16.Text = collim_dv;
                    break;
                case "X1":
                    //if (textBox13.Enabled & textBox13.ReadOnly==false & checkBox1.Checked == false)
                    textBox13.Text = x1_dv;
                    //if (textBox16.Enabled & textBox16.ReadOnly == false & checkBox2.Checked == false)
                    textBox16.Text = x1_dv;
                    break;
                case "X2":
                    //if (textBox13.Enabled & textBox13.ReadOnly == false & checkBox1.Checked == false)
                    textBox13.Text = x2_dv;
                    // if (textBox16.Enabled & textBox16.ReadOnly == false & checkBox2.Checked == false)
                    textBox16.Text = x2_dv;
                    break;
                case "Y1":
                    // if (textBox13.Enabled & textBox13.ReadOnly == false & checkBox1.Checked == false)
                    textBox13.Text = y1_dv;
                    //if (textBox16.Enabled & textBox16.ReadOnly == false & checkBox2.Checked == false)
                    textBox16.Text = y1_dv;
                    break;
                case "Y2":
                    //if (textBox13.Enabled & textBox13.ReadOnly == false & checkBox1.Checked == false)
                    textBox13.Text = y2_dv;
                    //if (textBox16.Enabled & textBox16.ReadOnly == false & checkBox2.Checked == false)
                    textBox16.Text = y2_dv;
                    break;
            }

            if (andSent)
            {
                TimeSpan fromLastAnd = DateTime.Now - sendAndTime;
                if (fromLastAnd.Milliseconds > 50)
                {
                    write("&");
                    write(summ.ToString() + "/");
                    andSent = false;
                    sendAnd = false;
                }
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked & checkBox2.Checked)
                try
                {
                    switch (comboBox1.Text)
                    {
                        case "Gantry":
                            gant_gain = gant_gain_temp;
                            gant_offset = gant_offset_temp;
                            gant_dv = (gant_gain * double.Parse(gant_cofin) + gant_offset).ToString();
                            textBox11.Text = gant_gain.ToString();
                            textBox12.Text = gant_offset.ToString();

                            tb_gant_gain.Text = Math.Round(gant_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_gant_offset.Text = Math.Round(gant_offset, 3, MidpointRounding.ToEven).ToString();

                            break;
                        case "Collimator":
                            collim_gain = collim_gain_temp;
                            collim_offset = collim_offset_temp;
                            collim_dv = (collim_gain * double.Parse(collim_cofin) + collim_offset).ToString();
                            textBox11.Text = collim_gain.ToString();
                            textBox12.Text = collim_offset.ToString();

                            tb_coli_gain.Text = Math.Round(collim_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_coli_offset.Text = Math.Round(collim_offset, 3, MidpointRounding.ToEven).ToString();

                            break;
                        case "X1":
                            x1_gain = x1_gain_temp;
                            x1_offset = x1_offset_temp;
                            x1_dv = (x1_gain * double.Parse(x1_co) + x1_offset).ToString();
                            textBox11.Text = x1_gain.ToString();
                            textBox12.Text = x1_offset.ToString();

                            tb_x1_gain.Text = Math.Round(x1_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_x1_offset.Text = Math.Round(x1_offset, 3, MidpointRounding.ToEven).ToString();

                            break;
                        case "X2":
                            x2_gain = x2_gain_temp;
                            x2_offset = x2_offset_temp;
                            x2_dv = (x2_gain * double.Parse(x2_co) + x2_offset).ToString();
                            textBox11.Text = x2_gain.ToString();
                            textBox12.Text = x2_offset.ToString();

                            tb_x2_gain.Text = Math.Round(x2_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_x2_offset.Text = Math.Round(x2_offset, 3, MidpointRounding.ToEven).ToString();

                            break;
                        case "Y1":
                            y1_gain = y1_gain_temp;
                            y1_offset = y1_offset_temp;
                            y1_dv = (y1_gain * double.Parse(y1_co) + y1_offset).ToString();
                            textBox11.Text = y1_gain.ToString();
                            textBox12.Text = y1_offset.ToString();

                            tb_y1_gain.Text = Math.Round(y1_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_y1_offset.Text = Math.Round(y1_offset, 3, MidpointRounding.ToEven).ToString();

                            break;
                        case "Y2":
                            y2_gain = y2_gain_temp;
                            y2_offset = y2_offset_temp;
                            y2_dv = (y2_gain * double.Parse(y2_co) + y2_offset).ToString();
                            textBox11.Text = y2_gain.ToString();
                            textBox12.Text = y2_offset.ToString();

                            tb_y2_gain.Text = Math.Round(y2_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_y2_offset.Text = Math.Round(y2_offset, 3, MidpointRounding.ToEven).ToString();

                            break;
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Wrong Nubmer!");
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            double a;
            if (checkBox1.Checked)
            {
                try
                {
                    a = double.Parse(textBox14.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Wrong Number!");
                    checkBox1.Checked = false;
                    return;
                }

                try
                {
                    gant_cofin_temp1 = double.Parse(gant_cofin);
                    collim_cofin_temp1 = double.Parse(collim_cofin);
                    x1_co_temp1 = double.Parse(x1_co);
                    x2_co_temp1 = double.Parse(x2_co);
                    y1_co_temp1 = double.Parse(y1_co);
                    y2_co_temp1 = double.Parse(y2_co);
                }
                catch
                {
                }

                textBox14.ReadOnly = true;
                if (checkBox2.Checked)
                {
                    try
                    {
                        switch (comboBox1.Text)
                        {
                            case "Gantry":
                                if (gant_cofin_temp1 == gant_cofin_temp2)
                                {
                                    MessageBox.Show("The Gantry angle muste be different in step1 and step2");
                                    checkBox1.Checked = false;
                                    return;
                                }
                                gant_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (gant_cofin_temp2 - gant_cofin_temp1);
                                gant_offset_temp = double.Parse(textBox15.Text) - (gant_gain_temp * gant_cofin_temp2);
                                textBox17.Text = gant_gain_temp.ToString();
                                textBox18.Text = gant_offset_temp.ToString();
                                break;
                            case "Collimator":
                                if (collim_cofin_temp1 == collim_cofin_temp2)
                                {
                                    MessageBox.Show("The Collimator angle muste be different in step1 and step2");
                                    checkBox1.Checked = false;
                                    return;
                                }
                                collim_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (collim_cofin_temp2 - collim_cofin_temp1);
                                collim_offset_temp = double.Parse(textBox15.Text) - (collim_gain_temp * collim_cofin_temp2);
                                textBox17.Text = collim_gain_temp.ToString();
                                textBox18.Text = collim_offset_temp.ToString();
                                break;
                            case "X1":
                                if (x1_co_temp1 == x1_co_temp2)
                                {
                                    MessageBox.Show("The X1 Position muste be different in step1 and step2");
                                    checkBox1.Checked = false;
                                    return;
                                }
                                x1_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (x1_co_temp2 - x1_co_temp1);
                                x1_offset_temp = double.Parse(textBox15.Text) - (x1_gain_temp * x1_co_temp2);
                                textBox17.Text = x1_gain_temp.ToString();
                                textBox18.Text = x1_offset_temp.ToString();
                                break;
                            case "X2":
                                if (x2_co_temp1 == x2_co_temp2)
                                {
                                    MessageBox.Show("The X2 Position muste be different in step1 and step2");
                                    checkBox1.Checked = false;
                                    return;
                                }
                                x2_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (x2_co_temp2 - x2_co_temp1);
                                x2_offset_temp = double.Parse(textBox15.Text) - (x2_gain_temp * x2_co_temp2);
                                textBox17.Text = x2_gain_temp.ToString();
                                textBox18.Text = x2_offset_temp.ToString();
                                break;
                            case "Y1":
                                if (y1_co_temp1 == y1_co_temp2)
                                {
                                    MessageBox.Show("The Y1 Position muste be different in step1 and step2");
                                    checkBox1.Checked = false;
                                    return;
                                }
                                y1_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (y1_co_temp2 - y1_co_temp1);
                                y1_offset_temp = double.Parse(textBox15.Text) - (y1_gain_temp * y1_co_temp2);
                                textBox17.Text = y1_gain_temp.ToString();
                                textBox18.Text = y1_offset_temp.ToString();
                                break;
                            case "Y2":
                                if (y2_co_temp1 == y2_co_temp2)
                                {
                                    MessageBox.Show("The Y2 Position muste be different in step1 and step2");
                                    checkBox1.Checked = false;
                                    return;
                                }
                                y2_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (y2_co_temp2 - y2_co_temp1);
                                y2_offset_temp = double.Parse(textBox15.Text) - (y2_gain_temp * y2_co_temp2);
                                textBox17.Text = y2_gain_temp.ToString();
                                textBox18.Text = y2_offset_temp.ToString();
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Wrong Nubmer!");
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                    }
                    button16.Enabled = true;
                }
            }
            else
            {
                textBox14.ReadOnly = false;
                button16.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            double a;
            if (checkBox2.Checked)
            {
                try
                {
                    a = double.Parse(textBox15.Text);
                }

                catch (FormatException)
                {
                    MessageBox.Show("Wrong Number!");
                    checkBox2.Checked = false;
                    return;
                }

                try
                {
                    gant_cofin_temp2 = double.Parse(gant_cofin);
                    collim_cofin_temp2 = double.Parse(collim_cofin);
                    x1_co_temp2 = double.Parse(x1_co);
                    x2_co_temp2 = double.Parse(x2_co);
                    y1_co_temp2 = double.Parse(y1_co);
                    y2_co_temp2 = double.Parse(y2_co);
                }
                catch
                {
                }
                textBox15.ReadOnly = true;
                if (checkBox1.Checked)
                {
                    try
                    {
                        switch (comboBox1.Text)
                        {
                            case "Gantry":
                                if (gant_cofin_temp1 == gant_cofin_temp2)
                                {
                                    MessageBox.Show("The Gantry angle muste be different in step1 and step2");
                                    checkBox2.Checked = false;
                                    return;
                                }
                                gant_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (gant_cofin_temp2 - gant_cofin_temp1);
                                gant_offset_temp = double.Parse(textBox15.Text) - (gant_gain_temp * gant_cofin_temp2);
                                textBox17.Text = gant_gain_temp.ToString();
                                textBox18.Text = gant_offset_temp.ToString();
                                break;
                            case "Collimator":
                                if (collim_cofin_temp1 == collim_cofin_temp2)
                                {
                                    MessageBox.Show("The Collimator angle muste be different in step1 and step2");
                                    checkBox2.Checked = false;
                                    return;
                                }
                                collim_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (collim_cofin_temp2 - collim_cofin_temp1);
                                collim_offset_temp = double.Parse(textBox15.Text) - (collim_gain_temp * collim_cofin_temp2);
                                textBox17.Text = collim_gain_temp.ToString();
                                textBox18.Text = collim_offset_temp.ToString();
                                break;
                            case "X1":
                                if (x1_co_temp1 == x1_co_temp2)
                                {
                                    MessageBox.Show("The X1 Position muste be different in step1 and step2");
                                    checkBox2.Checked = false;
                                    return;
                                }
                                x1_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (x1_co_temp2 - x1_co_temp1);
                                x1_offset_temp = double.Parse(textBox15.Text) - (x1_gain_temp * x1_co_temp2);
                                textBox17.Text = x1_gain_temp.ToString();
                                textBox18.Text = x1_offset_temp.ToString();
                                break;
                            case "X2":
                                if (x2_co_temp1 == x2_co_temp2)
                                {
                                    MessageBox.Show("The X2 Position muste be different in step1 and step2");
                                    checkBox2.Checked = false;
                                    return;
                                }
                                x2_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (x2_co_temp2 - x2_co_temp1);
                                x2_offset_temp = double.Parse(textBox15.Text) - (x2_gain_temp * x2_co_temp2);
                                textBox17.Text = x2_gain_temp.ToString();
                                textBox18.Text = x2_offset_temp.ToString();
                                break;
                            case "Y1":
                                if (y1_co_temp1 == y1_co_temp2)
                                {
                                    MessageBox.Show("The Y1 Position muste be different in step1 and step2");
                                    checkBox2.Checked = false;
                                    return;
                                }
                                y1_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (y1_co_temp2 - y1_co_temp1);
                                y1_offset_temp = double.Parse(textBox15.Text) - (y1_gain_temp * y1_co_temp2);
                                textBox17.Text = y1_gain_temp.ToString();
                                textBox18.Text = y1_offset_temp.ToString();
                                break;
                            case "Y2":
                                if (y2_co_temp1 == y2_co_temp2)
                                {
                                    MessageBox.Show("The Y2 Position muste be different in step1 and step2");
                                    checkBox2.Checked = false;
                                    return;
                                }
                                y2_gain_temp = (double.Parse(textBox15.Text) - double.Parse(textBox14.Text)) / (y2_co_temp2 - y2_co_temp1);
                                y2_offset_temp = double.Parse(textBox15.Text) - (y2_gain_temp * y2_co_temp2);
                                textBox17.Text = y2_gain_temp.ToString();
                                textBox18.Text = y2_offset_temp.ToString();
                                break;
                        }
                    }

                    catch (FormatException)
                    {
                        MessageBox.Show("Wrong Nubmer!");
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                    }
                    button16.Enabled = true;
                }
            }
            else
            {
                textBox15.ReadOnly = false;
                button16.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            textBox14.Enabled = true;
            textBox15.Enabled = true;
            textBox17.Enabled = true;
            textBox18.Enabled = true;

            textBox17.Text = "";
            textBox18.Text = "";

            label12.Enabled = true;
            label13.Enabled = true;
            label14.Enabled = true;
            label15.Enabled = true;
            label16.Enabled = true;
            label17.Enabled = true;
            label18.Enabled = true;
            label19.Enabled = true;
            label20.Enabled = true;
            label21.Enabled = true;

            switch (comboBox1.Text)
            {
                case "Gantry":
                    btn_learn.Enabled = true;
                    textBox11.Text = Math.Round(gant_gain, 7, MidpointRounding.ToEven).ToString();
                    textBox12.Text = Math.Round(gant_offset, 3, MidpointRounding.ToEven).ToString();
                    break;
                case "Collimator":
                    btn_learn.Enabled = true;
                    textBox11.Text = Math.Round(collim_gain, 7, MidpointRounding.ToEven).ToString();
                    textBox12.Text = Math.Round(collim_offset, 3, MidpointRounding.ToEven).ToString();
                    break;
                case "X1":
                    btn_learn.Enabled = false;
                    textBox11.Text = Math.Round(x1_gain, 7, MidpointRounding.ToEven).ToString();
                    textBox12.Text = Math.Round(x1_offset, 3, MidpointRounding.ToEven).ToString();
                    break;
                case "X2":
                    btn_learn.Enabled = false;
                    textBox11.Text = Math.Round(x2_gain, 7, MidpointRounding.ToEven).ToString();
                    textBox12.Text = Math.Round(x2_offset, 3, MidpointRounding.ToEven).ToString();
                    break;
                case "Y1":
                    btn_learn.Enabled = false;
                    textBox11.Text = Math.Round(y1_gain, 7, MidpointRounding.ToEven).ToString();
                    textBox12.Text = Math.Round(y1_offset, 3, MidpointRounding.ToEven).ToString();
                    break;
                case "Y2":
                    btn_learn.Enabled = false;
                    textBox11.Text = Math.Round(y2_gain, 7, MidpointRounding.ToEven).ToString();
                    textBox12.Text = Math.Round(y2_offset, 3, MidpointRounding.ToEven).ToString();
                    break;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            groupBox4.Enabled = true;
            comboBox1.Enabled = true;
            label11.Enabled = true;
            btn_cancelLearn.Enabled = true;
            btn_save.Enabled = true;
        }

        void SaveCalibFile()
        {
            try
            {
                string appPath = Application.StartupPath;
                string dataPath = System.IO.Path.Combine(appPath, "Calib.dat");
                string[] values = { gant_gain.ToString(), gant_offset.ToString(), collim_gain.ToString(), collim_offset.ToString(),
                                 x1_gain.ToString(), x1_offset.ToString(), x2_gain.ToString(), x2_offset.ToString(),
                                 y1_gain.ToString(), y1_offset.ToString(), y2_gain.ToString(), y2_offset.ToString()};
                //System.IO.File.WriteAllLines(dataPath, lines);
                HashPass.writeCalibJson(dataPath, values);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to Calib file" + Environment.NewLine + ex.ToString().Split('\n')[0]);
            }
        }

        public void SaveLearnFile()
        {
            try
            {
                string appPath = Application.StartupPath;
                string dataPath = System.IO.Path.Combine(appPath, "Learn.dat");
                string[] values = { gant_zpnt, gant_length, gant_fine_length, collim_zpnt, collim_length, collim_fine_length };
                //System.IO.File.WriteAllLines(dataPath, lines);
                HashPass.writeLearnJson(dataPath, values);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to Learn file" + Environment.NewLine + ex.ToString().Split('\n')[0]);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                serialPort1.Open();
            else
            {
                SaveCalibFile();
                SaveLearnFile();
            }

            btn_cancelLearn.Enabled = false;
            btn_save.Enabled = false;
            groupBox4.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClosePort();
            CloseProcess();
            Class_PatientData.cmd.Dispose();
            Application.Exit();
        }

        private static void CloseProcess()
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

        private void ClosePort()
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.DiscardOutBuffer();
                serialPort1.DiscardInBuffer();
                serialPort1.Dispose();
                serialPort1.Close();
            }
        }

        public static string[] ourParameters = new string[42];
        //public bool compareParameters(string[] microParams, string[] ourParams)
        //{
        //    bool equal = true;
        //    for (int i = 0; i < microParams.Length; i++)
        //    {
        //        if (microParams[i] != ourParams[i])
        //        {
        //            equal = false;
        //            write("$");
        //            initState = 2;
        //            MessageBox.Show("Parameter " + i.ToString() + " not equal > from micro: " + microParams[i] + " != ours: " + ourParams[i]);
        //            //break;
        //        }
        //    }
        //    return equal;
        //}
        //public bool compareData(string microParam, string ourParam)
        //{
        //    bool equal = true;
        //    if (double.Parse(microParam) != double.Parse(ourParam))
        //    {
        //        equal = false;
        //        write("$");
        //        sendParameters();
        //        initState = 2;
        //        MessageBox.Show("Parameter not equal > from micro: " + microParam + " != ours: " + ourParam);
        //        //break;
        //    }
        //    else
        //        write(".");
        //    return equal;
        //}
        public bool sendParameters()
        {
            try
            {
                string gant_tol_1_t = Math.Abs(Math.Round(double.Parse(gant_tol_1) / gant_gain)).ToString();
                string gant_tol0_t = Math.Abs(Math.Round(double.Parse(gant_tol0) / gant_gain)).ToString();
                string gant_tol1_t = Math.Abs(Math.Round(double.Parse(gant_tol1) / gant_gain)).ToString();
                string gant_tol2_t = Math.Abs(Math.Round(double.Parse(gant_tol2) / gant_gain)).ToString();

                string collim_tol_1_t = Math.Abs(Math.Round(double.Parse(collim_tol_1) / collim_gain)).ToString();
                string collim_tol0_t = Math.Abs(Math.Round(double.Parse(collim_tol0) / collim_gain)).ToString();
                string collim_tol1_t = Math.Abs(Math.Round(double.Parse(collim_tol1) / collim_gain)).ToString();
                string collim_tol2_t = Math.Abs(Math.Round(double.Parse(collim_tol2) / collim_gain)).ToString();

                string x1_tol_1_t = Math.Abs(Math.Round(double.Parse(x1_tol_1) / x1_gain)).ToString();
                string x1_tol0_t = Math.Abs(Math.Round(double.Parse(x1_tol0) / x1_gain)).ToString();
                string x1_tol1_t = Math.Abs(Math.Round(double.Parse(x1_tol1) / x1_gain)).ToString();
                string x1_tol2_t = Math.Abs(Math.Round(double.Parse(x1_tol2) / x1_gain)).ToString();

                string x2_tol_1_t = Math.Abs(Math.Round(double.Parse(x2_tol_1) / x2_gain)).ToString();
                string x2_tol0_t = Math.Abs(Math.Round(double.Parse(x2_tol0) / x2_gain)).ToString();
                string x2_tol1_t = Math.Abs(Math.Round(double.Parse(x2_tol1) / x2_gain)).ToString();
                string x2_tol2_t = Math.Abs(Math.Round(double.Parse(x2_tol2) / x2_gain)).ToString();

                string y1_tol_1_t = Math.Abs(Math.Round(double.Parse(y1_tol_1) / y1_gain)).ToString();
                string y1_tol0_t = Math.Abs(Math.Round(double.Parse(y1_tol0) / y1_gain)).ToString();
                string y1_tol1_t = Math.Abs(Math.Round(double.Parse(y1_tol1) / y1_gain)).ToString();
                string y1_tol2_t = Math.Abs(Math.Round(double.Parse(y1_tol2) / y1_gain)).ToString();

                string y2_tol_1_t = Math.Abs(Math.Round(double.Parse(y2_tol_1) / y2_gain)).ToString();
                string y2_tol0_t = Math.Abs(Math.Round(double.Parse(y2_tol0) / y2_gain)).ToString();
                string y2_tol1_t = Math.Abs(Math.Round(double.Parse(y2_tol1) / y2_gain)).ToString();
                string y2_tol2_t = Math.Abs(Math.Round(double.Parse(y2_tol2) / y2_gain)).ToString();

                double gant_gain_int = Math.Round(gant_gain * 100000);
                double gant_offset_int = Math.Round(gant_offset);
                double collim_gain_int = Math.Round(collim_gain * 100000);
                double collim_offset_int = Math.Round(collim_offset);

                ourSum = double.Parse(gant_tol_1_t) + double.Parse(gant_tol0_t) + double.Parse(gant_tol1_t) + double.Parse(gant_tol2_t) +
                         double.Parse(collim_tol_1_t) + double.Parse(collim_tol0_t) + double.Parse(collim_tol1_t) + double.Parse(collim_tol2_t) +
                         gant_gain_int + gant_offset_int + collim_gain_int + collim_offset_int +
                         double.Parse(x1_tol_1_t) + double.Parse(x1_tol0_t) + double.Parse(x1_tol1_t) + double.Parse(x1_tol2_t) +
                         double.Parse(x2_tol_1_t) + double.Parse(x2_tol0_t) + double.Parse(x2_tol1_t) + double.Parse(x2_tol2_t) +
                         double.Parse(y1_tol_1_t) + double.Parse(y1_tol0_t) + double.Parse(y1_tol1_t) + double.Parse(y1_tol2_t) +
                         double.Parse(y2_tol_1_t) + double.Parse(y2_tol0_t) + double.Parse(y2_tol1_t) + double.Parse(y2_tol2_t) +
                         double.Parse(gant_v1) + double.Parse(gant_v2) + double.Parse(gant_v3) +
                         double.Parse(collim_v1) + double.Parse(collim_v2) + double.Parse(collim_v3) +
                         double.Parse(x1_v1) + double.Parse(x1_v2) + double.Parse(x1_v3) +
                         double.Parse(x2_v1) + double.Parse(x2_v2) + double.Parse(x2_v3) +
                         double.Parse(y1_v1) + double.Parse(y1_v2) + double.Parse(y1_v3) +
                         double.Parse(y2_v1) + double.Parse(y2_v2) + double.Parse(y2_v3) +
                         double.Parse(gant_zpnt) + double.Parse(gant_length) + double.Parse(gant_fine_length) +
                         double.Parse(collim_zpnt) + double.Parse(collim_length) + double.Parse(collim_fine_length) +
                         double.Parse(gravity_up) + double.Parse(gravity_down);

                serialPort1.DiscardOutBuffer();
                //MessageBox.Show(ourSum.ToString());
                write("z");
                write(gant_zpnt + "/" + gant_length + "/" + gant_fine_length + "/" + gant_gain_int + "/" + gant_offset_int + "/");
                write(collim_zpnt + "/" + collim_length + "/" + collim_fine_length + "/" + collim_gain_int + "/" + collim_offset_int + "/");
                write(gant_tol_1_t + "/" + gant_tol0_t + "/" + gant_tol1_t + "/" + gant_tol2_t + "/");
                write(gant_v1 + "/" + gant_v2 + "/" + gant_v3 + "/");
                write(collim_tol_1_t + "/" + collim_tol0_t + "/" + collim_tol1_t + "/" + collim_tol2_t + "/");
                write(collim_v1 + "/" + collim_v2 + "/" + collim_v3 + "/");
                write(x1_tol_1_t + "/" + x1_tol0_t + "/" + x1_tol1_t + "/" + x1_tol2_t + "/");
                write(x1_v1 + "/" + x1_v2 + "/" + x1_v3 + "/");
                write(x2_tol_1_t + "/" + x2_tol0_t + "/" + x2_tol1_t + "/" + x2_tol2_t + "/");
                write(x2_v1 + "/" + x2_v2 + "/" + x2_v3 + "/");
                write(y1_tol_1_t + "/" + y1_tol0_t + "/" + y1_tol1_t + "/" + y1_tol2_t + "/");
                write(y1_v1 + "/" + y1_v2 + "/" + y1_v3 + "/");
                write(y2_tol_1_t + "/" + y2_tol0_t + "/" + y2_tol1_t + "/" + y2_tol2_t + "/");
                write(y2_v1 + "/" + y2_v2 + "/" + y2_v3 + "/" + gravity_up + "/" + gravity_down + "/");
                return true;
            }
            catch (Exception ex)
            {
                if ((isInServiceMode) && (btn_saveParameters.Enabled == false))
                    btn_saveParameters.Enabled = true;
                setInitState(2);
                // MessageBox.Show("Unable to send parameters!" + Environment.NewLine + ex.ToString().Split('\n')[0]);                }
                return false;
            }
        }

        private bool checkSum(double microSum, double ourSum)
        {
            bool equal = false;
            if (microSum == ourSum)
                equal = true;
            return equal;
        }
        public Queue<string> receiveQ = new Queue<string>();
        public Queue<string> writeOutQ = new Queue<string>();
        public Queue<string> writeInQ = new Queue<string>();
        bool gant_stat = false, coli_stat = false,
            x1_stat = false, x2_stat = false,
            y1_stat = false, y2_stat = false;
        string[] microParameters = new string[42];
        string currReceived;

        private void picBtn_Connect_MouseEnter(object sender, EventArgs e)
        {
            //picBtnToolTip.SetToolTip(picBtn_Connect, "Connect");
            picBtn_Connect.BackgroundImage = Resources.ConnectButton_MouseOver;
        }

        private void picBtn_Connect_MouseLeave(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                picBtn_Connect.BackgroundImage = Resources.ConnectButton_Connected;
            else
                picBtn_Connect.BackgroundImage = Resources.ConnectButton;
        }

        private void picBtn_PatientList_MouseEnter(object sender, EventArgs e)
        {
            //picBtnToolTip.SetToolTip(picBtn_PatientList, "Show Patient List...");
            //picBtn_PatientList.BackgroundImage = Resources.btn_PatientList_MouseOver;
        }

        private void picBtn_PatientList_MouseLeave(object sender, EventArgs e)
        {
            //picBtn_PatientList.BackgroundImage = Resources.btn_PatientList;
        }

        private void picBtn_Setting_MouseEnter(object sender, EventArgs e)
        {
            picBtnToolTip.SetToolTip(picBtn_Setting, "Settings");
            picBtn_Setting.BackgroundImage = Resources.Setting_mouseOver;
        }

        private void picBtn_Setting_MouseLeave(object sender, EventArgs e)
        {
            picBtn_Setting.BackgroundImage = Resources.Setting;
        }

        private void picBtn_LogOff_MouseEnter(object sender, EventArgs e)
        {
            picBtnToolTip.SetToolTip(picBtn_LogOff, "Log off");
            picBtn_LogOff.BackgroundImage = Resources.Logout_mouseOver;
        }

        private void picBtn_LogOff_MouseLeave(object sender, EventArgs e)
        {
            picBtn_LogOff.BackgroundImage = Resources.Logout;
        }

        private void picBtn_Exit_MouseEnter(object sender, EventArgs e)
        {
            picBtnToolTip.SetToolTip(picBtn_Exit, "Exit");
            picBtn_Exit.BackgroundImage = Resources.Exit_mouseOver2;
        }

        private void picBtn_Exit_MouseLeave(object sender, EventArgs e)
        {
            picBtn_Exit.BackgroundImage = Resources.Exit2;
        }

        private void btn_clearTerminal_Click(object sender, EventArgs e)
        {
            tb_terminal_out.Clear();
            lbl_out_cnt.Text = "0";
        }

        private void tb_terminal_TextChanged(object sender, EventArgs e)
        {
        }

        private void btn_clearTerminal_in_Click(object sender, EventArgs e)
        {
            tb_terminal_in.Clear();
            lbl_in_cnt.Text = "0";
        }

        private void picBtn_LogOff_Click(object sender, EventArgs e)
        {
            if (btn_start_stop.Text == "Stop")
            {
                button14_Click(sender, e);
            }

            string appPath = Application.StartupPath;
            string tempFile = System.IO.Path.Combine(appPath, Form_Login.tempFile);
            if (System.IO.File.Exists(tempFile))
                System.IO.File.Delete(tempFile);
            System.IO.File.Create(tempFile).Dispose();
            CloseProcess();
            this.Hide();
            try
            {
                DisconnectPort();
            }
            catch { }
            //Form_Login.ShowForm();
            Application.Restart();
        }

        bool sendAnd = false;
        bool andSent = false;
        int summ = 0;
        string currData;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!checkBox3.Checked)
                return;
            if (serialPort1.IsOpen == false)
                return;
            try
            {
                while (serialPort1.BytesToRead > 0)
                {
                    currReceived = serialPort1.ReadLine();
                    receiveQ.Enqueue(currReceived);
                }
                //serialPort1.DiscardInBuffer();
            }
            catch { }


            while (true)
            {
                //if (receiveQ.Count == 0)
                //return;
                try
                {
                    currData = receiveQ.Dequeue();
                }
                catch
                {
                    return;
                }
                //string[] lines = currData.Split('\n');

                double c;
                try
                {
                    switch (currData.Substring(0, 3))
                    {
                        case "ini":
                            sendParameters();
                            if ((isInServiceMode) && (btn_saveParameters.Enabled == true))
                            {
                                btn_saveParameters.Enabled = false;
                                //sendParametersFlag = true;
                            }
                            //else
                            //clientFrm.sendParametersFlag = true;
                            //sendParameters();
                            setInitState(0);
                            break;
                        case "sum":
                            string microSum = currData.Substring(3, currData.Length - 3);
                            if (checkSum(double.Parse(microSum), ourSum) == true)
                            {
                                write("{|}~");
                                setInitState(1);
                                if (isInServiceMode)
                                    btn_saveParameters.Enabled = true;
                            }
                            else
                            {
                                if (isInServiceMode)
                                    btn_saveParameters.Enabled = true;
                                //write("$");
                                //sendParametersFlag = true;
                                //sendParameters();
                            }
                            break;
                        case "SSS":
                            if (isInServiceMode)
                            {
                                if (in_diag == false)
                                    write("s");
                            }
                            else
                                write("s");
                            break;
                        case "sss":
                            if (!isInServiceMode)
                                write("x");
                            break;
                        case "ccc":
                            if (isInServiceMode)
                                write("y");
                            break;
                        case "gco":
                            if (isInServiceMode)
                                gant_co = currData.Substring(3, currData.Length - 3);
                            break;
                        case "gf1":
                            if (isInServiceMode)
                                gant_f1 = currData.Substring(3, currData.Length - 3);
                            break;
                        case "gf2":
                            if (isInServiceMode)
                                gant_f2 = currData.Substring(3, currData.Length - 3);
                            break;
                        case "gfn":

                            gant_cofin = currData.Substring(3, currData.Length - 3);
                            if (isInServiceMode)
                                gant_dv = Math.Round((gant_gain * double.Parse(gant_cofin) + gant_offset), 2, MidpointRounding.ToEven).ToString();
                            else
                            {
                                c = Math.Round((gant_gain * double.Parse(gant_cofin) + gant_offset), 1, MidpointRounding.ToEven);
                                if (c < 0)
                                {
                                    c = c + 360;
                                }
                                gant_dv = c.ToString();
                            }
                            break;
                        case "cco":
                            if (isInServiceMode)
                                collim_co = currData.Substring(3, currData.Length - 3);
                            break;
                        case "cf1":
                            if (isInServiceMode)
                                collim_f1 = currData.Substring(3, currData.Length - 3);
                            break;
                        case "cf2":
                            if (isInServiceMode)
                                collim_f2 = currData.Substring(3, currData.Length - 3);
                            break;
                        case "cfn":
                            collim_cofin = currData.Substring(3, currData.Length - 3);
                            if (isInServiceMode)
                                collim_dv = Math.Round((collim_gain * double.Parse(collim_cofin) + collim_offset), 2, MidpointRounding.ToEven).ToString();
                            else
                            {
                                c = Math.Round((collim_gain * double.Parse(collim_cofin) + collim_offset), 1, MidpointRounding.ToEven);
                                if (c < 0)
                                {
                                    c = c + 360;
                                }
                                collim_dv = c.ToString();
                            }
                            break;
                        case "wco":
                            x1_co = currData.Substring(3, currData.Length - 3);
                            x1_dv = Math.Round(((x1_gain * double.Parse(x1_co)) + x1_offset), 2, MidpointRounding.ToEven).ToString();
                            break;
                        case "xco":
                            x2_co = currData.Substring(3, currData.Length - 3);
                            if (isInServiceMode)
                                x2_dv = Math.Round(((x2_gain * double.Parse(x2_co)) + x2_offset), 2, MidpointRounding.ToEven).ToString();
                            else
                                x2_dv = Math.Round(-((x2_gain * double.Parse(x2_co)) + x2_offset), 1, MidpointRounding.ToEven).ToString();
                            break;
                        case "yco":
                            y1_co = currData.Substring(3, currData.Length - 3);
                            y1_dv = Math.Round(((y1_gain * double.Parse(y1_co)) + y1_offset), 2, MidpointRounding.ToEven).ToString();
                            break;
                        case "zco":
                            y2_co = currData.Substring(3, currData.Length - 3);
                            if (isInServiceMode)
                                y2_dv = Math.Round(((y2_gain * double.Parse(y2_co)) + y2_offset), 2, MidpointRounding.ToEven).ToString();
                            else
                                y2_dv = Math.Round(-((y2_gain * double.Parse(y2_co)) + y2_offset), 1, MidpointRounding.ToEven).ToString();
                            break;
                        case "lok":
                            tb_gant_gain.Text = Math.Round(gant_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_coli_gain.Text = Math.Round(collim_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_x1_gain.Text = Math.Round(x1_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_x2_gain.Text = Math.Round(x2_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_y1_gain.Text = Math.Round(y1_gain, 7, MidpointRounding.ToEven).ToString();
                            tb_y2_gain.Text = Math.Round(y2_gain, 7, MidpointRounding.ToEven).ToString();

                            tb_gant_offset.Text = Math.Round(gant_offset, 3, MidpointRounding.ToEven).ToString();
                            tb_coli_offset.Text = Math.Round(collim_offset, 3, MidpointRounding.ToEven).ToString();
                            tb_x1_offset.Text = Math.Round(x1_offset, 3, MidpointRounding.ToEven).ToString();
                            tb_x2_offset.Text = Math.Round(x2_offset, 3, MidpointRounding.ToEven).ToString();
                            tb_y1_offset.Text = Math.Round(y1_offset, 3, MidpointRounding.ToEven).ToString();
                            tb_y2_offset.Text = Math.Round(y2_offset, 3, MidpointRounding.ToEven).ToString();

                            tb_gant_zpnt.Text = gant_zpnt;
                            tb_gant_len.Text = gant_length;
                            tb_gant_flen.Text = gant_fine_length;
                            tb_coli_zpnt.Text = collim_zpnt;
                            tb_coli_len.Text = collim_length;
                            tb_coli_flen.Text = collim_fine_length;
                            try
                            {
                                MessageBox.Show("Learning was succesfull\nUse the Save button to save the results");
                                btn_cancelLearn.Enabled = true;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error saving to file" + Environment.NewLine + ex.ToString().Split('\n')[0]);
                            }
                            break;
                        case "sok":
                            MessageBox.Show("Saving was succesfull!");
                            break;
                        case "snk":
                            MessageBox.Show("Error: Saving was not succesfull!");
                            break;
                        case "c43":
                            gant_zpnt = currData.Substring(3, currData.Length - 3);
                            break;
                        case "c44":
                            gant_length = currData.Substring(3, currData.Length - 3);
                            break;
                        case "c45":
                            gant_fine_length = currData.Substring(3, currData.Length - 3);
                            //write(gant_zpnt + (gant_zpnt.Length + 1).ToString() + "/" + gant_length + (gant_length.Length + 1).ToString() + "/" + gant_fine_length + (gant_fine_length.Length + 1).ToString() + "/");
                            write(gant_zpnt + "/" + gant_length + "/" + gant_fine_length + "/");
                            break;
                        case "c46":
                            collim_zpnt = currData.Substring(3, currData.Length - 3);
                            break;
                        case "c47":
                            collim_length = currData.Substring(3, currData.Length - 3);
                            break;
                        case "c48":
                            collim_fine_length = currData.Substring(3, currData.Length - 3);
                            write(collim_zpnt + "/" + collim_length + "/" + collim_fine_length + "/");
                            //write(collim_zpnt + (collim_zpnt.Length + 1).ToString() + "/");
                            //write(collim_length + (collim_length.Length + 1).ToString() + "/");
                            //write(collim_fine_length + (collim_fine_length.Length + 1).ToString() + "/");
                            break;
                        case "gnd":
                            gnd = currData.Substring(3, currData.Length - 3);
                            break;
                        case "cld":
                            cld = currData.Substring(3, currData.Length - 3);
                            break;
                        case "x1d":
                            x1d = currData.Substring(3, currData.Length - 3);
                            break;
                        case "x2d":
                            x2d = currData.Substring(3, currData.Length - 3);
                            break;
                        case "y1d":
                            y1d = currData.Substring(3, currData.Length - 3);
                            break;
                        case "y2d":
                            y2d = currData.Substring(3, currData.Length - 3);
                            break;
                        case "adc":
                            inputADC = true;

                            if (!isInServiceMode)
                                if (clientFrm.txt_fakeADC.Visible == true)
                                    adc = clientFrm.txt_fakeADC.Text;



                            adc = currData.Substring(3, currData.Length - 3);
                            if (int.Parse(adc) < 1850 || int.Parse(adc) > 2250)
                            {
                                adcCheck_counter = adcCheck_counter + 1;
                                if (adcCheck_counter >= 10)
                                {
                                    gant_set = "0";
                                    collim_set = "0";
                                    x1_set = "0";
                                    x2_set = "0";
                                    y1_set = "0";
                                    y2_set = "0";
                                    clientFrm.lbl_readingError.Show();
                                    clientFrm.lbl_pleaseRestart.Show();
                                    readError = true;
                                    timer1.Enabled = false;
                                    clientFrm.timer_gant.Enabled = false;
                                    clientFrm.timer_coli.Enabled = false;
                                    clientFrm.timer_x1.Enabled = false;
                                    clientFrm.timer_x2.Enabled = false;
                                    clientFrm.timer_y1.Enabled = false;
                                    clientFrm.timer_y2.Enabled = false;
                                }
                                break;
                            }
                            else
                                adcCheck_counter = 0;


                            if (int.Parse(gant_set) != int.Parse(gnd))
                                gant_stat = false;
                            else
                                gant_stat = true;
                            if (int.Parse(collim_set) != int.Parse(cld))
                                coli_stat = false;
                            else
                                coli_stat = true;
                            if (int.Parse(x1_set) != int.Parse(x1d))
                                x1_stat = false; //y2_led
                            else
                                x1_stat = true;
                            if (int.Parse(x2_set) != int.Parse(x2d))
                                x2_stat = false; //y1_led
                            else
                                x2_stat = true;
                            if (int.Parse(y1_set) != int.Parse(y1d))
                                y1_stat = false; //x2_led
                            else
                                y1_stat = true;
                            if (int.Parse(y2_set) != int.Parse(y2d))
                                y2_stat = false; //x1_led
                            else
                                y2_stat = true;

                            if ((gant_stat && coli_stat && x1_stat && x2_stat && y1_stat && y2_stat) == false)
                            {
                                write("$");
                                sendAnd = true;
                            }

                            if (isInServiceMode)
                            {
                                int i = int.Parse(lbl_in_cnt.Text);
                                i = i + 1;
                                lbl_in_cnt.Text = i.ToString();
                                if (int.Parse(lbl_in_cnt.Text) > 100)
                                {
                                    lbl_in_cnt.Text = "0";
                                    tb_terminal_in.Clear();
                                }
                            }
                            else if (showClinicalTerminals == "1")
                            {
                                int i = int.Parse(clientFrm.lbl_in_cnt.Text);
                                i = i + 1;
                                clientFrm.lbl_in_cnt.Text = i.ToString();

                                if (int.Parse(clientFrm.lbl_in_cnt.Text) > 100)
                                {
                                    clientFrm.lbl_in_cnt.Text = "0";
                                    clientFrm.tb_terminal_in.Clear();
                                }
                            }

                            break;
                        case "req":
                            summ = 0;
                            if (gant_stat == false)
                            {
                                write("m" + gant_set + "/");
                                summ = summ + int.Parse(gant_set);
                            }
                            if (coli_stat == false)
                            {
                                write("n" + collim_set + "/");
                                summ = summ + int.Parse(collim_set);
                            }
                            if (x1_stat == false)
                            {
                                write("o" + x1_set + "/");
                                summ = summ + int.Parse(x1_set);
                            }
                            if (x2_stat == false)
                            {
                                write("p" + x2_set + "/");
                                summ = summ + int.Parse(x2_set);
                            }
                            if (y1_stat == false)
                            {
                                write("q" + y1_set + "/");
                                summ = summ + int.Parse(y1_set);
                            }
                            if (y2_stat == false)
                            {
                                write("r" + y2_set + "/");
                                summ = summ + int.Parse(y2_set);
                            }

                            //if (sendAnd)
                            //{
                            write("&");
                            write(summ.ToString() + "/");
                            andSent = true;
                            sendAndTime = DateTime.Now;
                            //}
                            break;
                        case "&&&":
                            summ = 0;
                            sendAnd = false;
                            andSent = false;
                            break;
                        default:
                            writeToOtherTerminal(currData, false);
                            break;
                    }
                    if (quit == true)
                    {
                        ClosePort();
                        Application.Exit();
                    }
                }
                catch
                {
                    writeToOtherTerminal(currData, true);
                }

                writeInQ.Enqueue(currData);
            }
            
        }

        private void Validate_Text_tol(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                float f;
                if (float.TryParse(tb.Text, out f))
                {
                    if ((Regex.IsMatch((sender as TextBox).Text, "^\\d*\\.\\d{2}$")) ||
                        (Regex.IsMatch((sender as TextBox).Text, "^\\d*\\.\\d{1}$")) ||
                        (Regex.IsMatch((sender as TextBox).Text, "^\\d*$")))
                        return;
                }
            }
            MessageBox.Show("Invalid input\nValue must be a number with maximum 2 decimal places");
            tb.SelectAll();
            e.Cancel = true;
        }

        private void Validate_Text_V(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                int i;
                if (int.TryParse(tb.Text, out i))
                {
                    if (i >= 0 && i < 128)
                        return;
                }
            }
            MessageBox.Show("Invalid input\nValue must be a number between 0 and 127");
            tb.SelectAll();
            e.Cancel = true;
        }

        private void btn_clearTerminal_oth_Click(object sender, EventArgs e)
        {
            tb_terminal_oth.Clear();

            //MessageBox.Show("global: " + GlobalSerialPort.PortName + "/" + GlobalSerialPort.BaudRate + '\n' +
            //    "service: " + serialPort1.PortName + "/" + serialPort1.BaudRate + '\n' +
            //    "client: " + clientFrm.serialPort1.PortName + "/" + clientFrm.serialPort1.BaudRate);
        }

        private void btn_learn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                
                if (serialPort1.IsOpen == false)
                    serialPort1.Open();
                if (comboBox1.Text == "Gantry")
                    write("t");
                if (comboBox1.Text == "Collimator")
                    write("u");
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            lbl_init.Hide();
            timer4.Stop();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
            {
                if (btn_start_stop.Text == "Stop")
                {
                    button14_Click(sender, e);
                }
            }
        }

        private void btn_cancelLearn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                groupBox4.Enabled = false;
                comboBox1.Enabled = false;
                label11.Enabled = false;

                ReadCalibFile();
                ReadLearnFile();

                btn_cancelLearn.Enabled = false;
                btn_save.Enabled = false;
                MessageBox.Show("Learn process is cancelled\nAll parameters reverted back to their previous values");
            }
        }

        private void picBtn_Exit_Click(object sender, EventArgs e)
        {
            if (btn_start_stop.Text == "Stop")
            {
                button14_Click(sender, e);
            }

            quit = true;
            //ClosePort();
            if (serialPort1.IsOpen == true)
                MessageBox.Show("Please click on ''Disconnect'' button before exit!");
            else
                Application.Exit();
        }

        private void picBtn_Connect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                SetConnection(true);
            else
            {
                SetConnection(false);
            }
            if (btn_saveParameters.Enabled == false)
            {
                btn_saveParameters.Enabled = true;
            }
        }

        bool isGantSet = false;
        bool isColiSet = false;
        bool isY1Set = false;
        bool isY2Set = false;
        bool isX1Set = false;
        bool isX2Set = false;

        private void gantSet()
        {
            if (isGantSet)
            {
                try
                {
                    if (Math.Abs(double.Parse(tb_gant_set.Text) - double.Parse(gant_dv)) <= .1)
                    {
                        pictureBox1.Hide();
                        isGantSet = false;
                    }
                }
                catch { }
            }
        }

        private void tb_coli_set_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(tb_coli_set.Text) || string.IsNullOrWhiteSpace(tb_coli_set.Text))
                {
                    collim_set = "0";
                    pictureBox2.Hide();
                    pictureBox2.BackgroundImage = Resources.Request;
                    tb_y1_set.Focus();
                    isColiSet = false;
                    return;
                }

                double a;
                try
                {
                    a = double.Parse(tb_coli_set.Text);
                    if (a < -180 || a > 180)
                    {
                        collim_set = "0";
                        pictureBox2.BackgroundImage = Resources.Error;
                        pictureBox2.Show();
                        isColiSet = false;
                        return;
                    }

                    collim_set = ((int)((a - collim_offset) / collim_gain)).ToString();
                    pictureBox2.BackgroundImage = requestImage;

                    if (Math.Abs(double.Parse(tb_coli_set.Text) - double.Parse(collim_dv)) > .1)
                    {
                        isColiSet = true;
                        pictureBox2.Show();
                    }
                    else
                    {
                        isColiSet = false;
                        pictureBox2.Hide();
                        pictureBox2.BackgroundImage = requestImage;
                    }
                    tb_coli_set.BackColor = Color.LightGreen;
                    tb_y1_set.Focus();
                }
                catch
                {
                    collim_set = "0";
                    pictureBox2.BackgroundImage = Resources.Error;
                    pictureBox2.Show();
                    isColiSet = false;
                    return;
                }
            }
        }

        private void coliSet()
        {
            if (isColiSet)
            {
                try
                {
                    if (Math.Abs(double.Parse(tb_coli_set.Text) - double.Parse(collim_dv)) <= .1)
                    {
                        pictureBox2.Hide();
                        isColiSet = false;
                    }
                }
                catch { }
            }
        }

        private void tb_y1_set_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(tb_y1_set.Text) || string.IsNullOrWhiteSpace(tb_y1_set.Text))
                    {
                        y1_set = "0";
                        pictureBox5.Hide();
                        tb_y2_set.Focus();
                        isY1Set = false;
                        return;
                    }

                    double a = double.Parse(tb_y1_set.Text);

                    if (a < -12.5 || a > 20)
                    {
                        y1_set = "0";
                        pictureBox5.BackgroundImage = errorImage;
                        pictureBox5.Show();
                        tb_y1_set.SelectAll();
                        isY1Set = false;
                        return;
                    }
                    else if (-a > double.Parse(y2_dv) - 1)
                    {
                        y1_set = "0";
                        pictureBox5.BackgroundImage = Resources.Error;
                        pictureBox5.Show();
                        tb_y1_set.SelectAll();
                        isY1Set = false;
                        return;
                    }
                    else if (y2_set != "0")
                    {
                        if (-a > double.Parse(tb_y2_set.Text) - 1)
                        {
                            y1_set = "0";
                            pictureBox5.BackgroundImage = Resources.Error;
                            pictureBox5.Show();
                            tb_y1_set.SelectAll();
                            isY1Set = false;
                            return;
                        }
                    }

                    y1_set = ((int)((a - y1_offset) / y1_gain)).ToString();
                    pictureBox5.BackgroundImage = requestImage;

                    if (Math.Abs(double.Parse(tb_y1_set.Text) - double.Parse(y1_dv)) > .1)
                    {
                        isY1Set = true;
                        pictureBox5.Show();
                    }
                    else
                    {
                        isY1Set = false;
                        pictureBox5.Hide();
                        pictureBox5.BackgroundImage = requestImage;
                    }

                    tb_y1_set.BackColor = Color.LightGreen;
                    tb_y2_set.Focus();
                }
                catch
                {
                    tb_y1_set.SelectAll();
                    y1_set = "0";
                    pictureBox5.BackgroundImage = Resources.Error;
                    pictureBox5.Show();
                    isY1Set = false;
                    return;
                }
            }
        }

        private void y1Set()
        {
            if (isY1Set)
            {
                try
                {
                    if (Math.Abs(double.Parse(tb_y1_set.Text) - double.Parse(y1_dv)) <= .1)
                    {
                        pictureBox5.Hide();
                        isY1Set = false;
                    }
                }
                catch { }
            }
        }
        private void tb_y2_set_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(tb_y2_set.Text) || string.IsNullOrWhiteSpace(tb_y2_set.Text))
                    {
                        y2_set = "0";
                        pictureBox3.Hide();
                        tb_x1_set.Focus();
                        isY2Set = false;
                        return;
                    }

                    double a = double.Parse(tb_y2_set.Text);

                    if (a < -12.5 || a > 20)
                    {
                        y2_set = "0";
                        pictureBox6.BackgroundImage = errorImage;
                        pictureBox6.Show();
                        tb_y2_set.SelectAll();
                        isY2Set = false;
                        return;
                    }
                    else if (-a > double.Parse(y1_dv) - 1)
                    {
                        y2_set = "0";
                        pictureBox6.BackgroundImage = Resources.Error;
                        pictureBox6.Show();
                        tb_y2_set.SelectAll();
                        isY2Set = false;
                        return;
                    }
                    else if (y1_set != "0")
                    {
                        if (-a > double.Parse(tb_y1_set.Text) - 1)
                        {
                            y2_set = "0";
                            pictureBox6.BackgroundImage = Resources.Error;
                            pictureBox6.Show();
                            tb_y2_set.SelectAll();
                            isY2Set = false;
                            return;
                        }
                    }

                    y2_set = ((int)((a - y2_offset) / y2_gain)).ToString();
                    pictureBox6.BackgroundImage = requestImage;

                    if (Math.Abs(double.Parse(tb_y2_set.Text) - double.Parse(y2_dv)) > .1)
                    {
                        isY2Set = true;
                        pictureBox6.Show();
                    }
                    else
                    {
                        isY2Set = false;
                        pictureBox6.Hide();
                        pictureBox6.BackgroundImage = requestImage;
                    }

                    tb_y2_set.BackColor = Color.LightGreen;
                    tb_x1_set.Focus();
                }
                catch
                {
                    y2_set = "0";
                    tb_y2_set.SelectAll();
                    pictureBox6.BackgroundImage = Resources.Error;
                    pictureBox6.Show();
                    isY2Set = false;
                    return;
                }
            }
        }

        private void y2Set()
        {
            if (isY2Set)
            {
                try
                {
                    if (Math.Abs(double.Parse(tb_y2_set.Text) - double.Parse(y2_dv)) <= .1)
                    {
                        pictureBox6.Hide();
                        isY2Set = false;
                    }
                }
                catch { }
            }

        }

        private void tb_x1_set_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(tb_x1_set.Text) || string.IsNullOrWhiteSpace(tb_x1_set.Text))
                    {
                        x1_set = "0";
                        pictureBox6.Hide();
                        tb_x2_set.Focus();
                        isX1Set = false;
                        return;
                    }

                    double a = double.Parse(tb_x1_set.Text);

                    if (a < 0 || a > 20)
                    {
                        x1_set = "0";
                        pictureBox3.BackgroundImage = errorImage;
                        pictureBox3.Show();
                        tb_x1_set.SelectAll();
                        isX1Set = false;
                        return;
                    }
                    else if (-a > double.Parse(x2_dv) - 1)
                    {
                        x1_set = "0";
                        pictureBox3.BackgroundImage = Resources.Error;
                        pictureBox3.Show();
                        tb_x1_set.SelectAll();
                        isX1Set = false;
                        return;
                    }
                    else if (x2_set != "0")
                    {
                        if (-a > double.Parse(tb_x2_set.Text) - 1)
                        {
                            x1_set = "0";
                            pictureBox3.BackgroundImage = Resources.Error;
                            pictureBox3.Show();
                            tb_x1_set.SelectAll();
                            isX1Set = false;
                            return;
                        }
                    }

                    x1_set = ((int)((a - x1_offset) / x1_gain)).ToString();
                    pictureBox3.BackgroundImage = requestImage;

                    if (Math.Abs(double.Parse(tb_x1_set.Text) - double.Parse(x1_dv)) > .1)
                    {
                        isX1Set = true;
                        pictureBox3.Show();
                    }
                    else
                    {
                        isX1Set = false;
                        pictureBox3.Hide();
                        pictureBox3.BackgroundImage = requestImage;
                    }

                    tb_x1_set.BackColor = Color.LightGreen;
                    tb_x2_set.Focus();
                }
                catch
                {
                    x1_set = "0";
                    tb_x1_set.SelectAll();
                    pictureBox3.BackgroundImage = Resources.Error;
                    pictureBox3.Show();
                    isX1Set = false;
                    return;
                }
            }
        }

        private void x1Set()
        {
            if (isX1Set)
            {
                try
                {
                    if (Math.Abs(double.Parse(tb_x1_set.Text) - double.Parse(x1_dv)) <= .1)
                    {
                        pictureBox3.Hide();
                        isX1Set = false;
                    }
                }
                catch { }
            }
        }

        private void tb_x2_set_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (string.IsNullOrEmpty(tb_x2_set.Text) || string.IsNullOrWhiteSpace(tb_x2_set.Text))
                    {
                        x2_set = "0";
                        pictureBox5.Hide();
                        tb_gant_set.Focus();
                        isX2Set = false;
                        return;
                    }

                    double a = double.Parse(tb_x2_set.Text);

                    if (a < 0 || a > 20)
                    {
                        x2_set = "0";
                        pictureBox4.BackgroundImage = errorImage;
                        pictureBox4.Show();
                        tb_x2_set.SelectAll();
                        isX2Set = false;
                        return;
                    }
                    //if (x1_dv != null && (-a > double.Parse(x1_dv) - 1))
                    if (-a > double.Parse(x1_dv) - 1)
                    {
                        x2_set = "0";
                        pictureBox4.BackgroundImage = Resources.Error;
                        pictureBox4.Show();
                        tb_x2_set.SelectAll();
                        isX2Set = false;
                        return;
                    }
                    else if (x1_set != "0")
                    {
                        if (-a > double.Parse(tb_x1_set.Text) - 1)
                        {
                            x2_set = "0";
                            pictureBox4.BackgroundImage = Resources.Error;
                            pictureBox4.Show();
                            tb_x2_set.SelectAll();
                            isX2Set = false;
                            return;
                        }
                    }

                    x2_set = ((int)((a - x2_offset) / x2_gain)).ToString();
                    pictureBox4.BackgroundImage = requestImage;

                    if (Math.Abs(double.Parse(tb_x2_set.Text) - double.Parse(x2_dv)) > .1)
                    {
                        isX2Set = true;
                        pictureBox4.Show();
                    }
                    else
                    {
                        isX2Set = false;
                        pictureBox4.Hide();
                        pictureBox4.BackgroundImage = requestImage;
                    }

                    tb_x2_set.BackColor = Color.LightGreen;
                    tb_gant_set.Focus();
                }
                catch
                {
                    x2_set = "0";
                    tb_x2_set.SelectAll();
                    pictureBox4.BackgroundImage = Resources.Error;
                    pictureBox4.Show();
                    isX2Set = false;
                    return;
                }
            }
        }

        private void x2Set()
        {
            if (isX2Set)
            {
                try
                {
                    if (Math.Abs(double.Parse(tb_x2_set.Text) - double.Parse(x2_dv)) <= .1)
                    {
                        isX2Set = false;
                        pictureBox4.Hide();
                    }
                }
                catch { }
            }
        }


        private void timer5_Tick(object sender, EventArgs e)
        {
            
            if ((!isInServiceMode) && (serialPort1.IsOpen == true))
            {
                if ((!gant_stat) && (clientFrm.pb_gant_status.BackgroundImage != Resources.led_red))
                    clientFrm.pb_gant_status.BackgroundImage = Resources.led_red;
                else if (clientFrm.pb_gant_status.BackgroundImage != Resources.led_green)
                    clientFrm.pb_gant_status.BackgroundImage = Resources.led_green;
                if ((!coli_stat) && clientFrm.pb_coli_status.BackgroundImage != Resources.led_red)
                    clientFrm.pb_coli_status.BackgroundImage = Resources.led_red;
                else if (clientFrm.pb_coli_status.BackgroundImage != Resources.led_green)
                    clientFrm.pb_coli_status.BackgroundImage = Resources.led_green;
                if ((!x1_stat) && (clientFrm.pb_y2_status.BackgroundImage != Resources.led_red))
                    clientFrm.pb_y2_status.BackgroundImage = Resources.led_red;
                else if (clientFrm.pb_y2_status.BackgroundImage != Resources.led_green)
                    clientFrm.pb_y2_status.BackgroundImage = Resources.led_green;
                if ((!x2_stat) && (clientFrm.pb_y1_status.BackgroundImage != Resources.led_red))
                    clientFrm.pb_y1_status.BackgroundImage = Resources.led_red;
                else if (clientFrm.pb_y1_status.BackgroundImage != Resources.led_green)
                    clientFrm.pb_y1_status.BackgroundImage = Resources.led_green;
                if ((!y1_stat) && (clientFrm.pb_x2_status.BackgroundImage != Resources.led_red))
                    clientFrm.pb_x2_status.BackgroundImage = Resources.led_red;
                else if (clientFrm.pb_x2_status.BackgroundImage != Resources.led_green)
                    clientFrm.pb_x2_status.BackgroundImage = Resources.led_green;
                if ((!y2_stat) && (clientFrm.pb_x1_status.BackgroundImage != Resources.led_red))
                    clientFrm.pb_x1_status.BackgroundImage = Resources.led_red;
                else if (clientFrm.pb_x1_status.BackgroundImage != Resources.led_green)
                    clientFrm.pb_x1_status.BackgroundImage = Resources.led_green;
            }
        }

        private void tb_gant_set_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tb_gant_set.Text) || string.IsNullOrWhiteSpace(tb_gant_set.Text))
            {
                gant_set = "0";
                pictureBox1.Hide();
                return;
            }
        }

        private void tb_coli_set_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tb_coli_set.Text) || string.IsNullOrWhiteSpace(tb_coli_set.Text))
            {
                collim_set = "0";
                pictureBox2.Hide();
                return;
            }
        }

        private void tb_gant_set_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(tb_gant_set.Text) || string.IsNullOrWhiteSpace(tb_gant_set.Text))
                {
                    gant_set = "0";
                    pictureBox1.Hide();
                    pictureBox1.BackgroundImage = Resources.Request;
                    tb_coli_set.Focus();
                    isGantSet = false;
                    return;
                }

                double aa;
                try
                {
                    aa = double.Parse(tb_gant_set.Text);
                    if (aa < -180 || aa > 180)
                    {
                        gant_set = "0";
                        pictureBox1.BackgroundImage = Resources.Error;
                        pictureBox1.Show();
                        isGantSet = false;
                        return;
                    }
                    double gentValueActual = double.Parse(gant_dv);

                    gant_set = ((int)((aa - gant_offset) / gant_gain)).ToString();
                    pictureBox1.BackgroundImage = requestImage;

                    if (Math.Abs(double.Parse(tb_gant_set.Text) - double.Parse(gant_dv)) > .1)
                    {
                        isGantSet = true;
                        pictureBox1.Show();
                    }
                    else
                    {
                        pictureBox1.Hide();
                        isGantSet = false;
                    }
                    tb_gant_set.BackColor = Color.LightGreen;
                    tb_coli_set.Focus();
                }
                catch
                {
                    tb_gant_set.SelectAll();
                    gant_set = "0";
                    pictureBox1.BackgroundImage = Resources.Error;
                    pictureBox1.Show();
                    isGantSet = false;
                    return;
                }
            }
        }

        private void tb_y1_set_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tb_y1_set.Text) || string.IsNullOrWhiteSpace(tb_y1_set.Text))
            {
                y1_set = "0";
                pictureBox5.Hide();
                return;
            }
        }

        private void tb_y2_set_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tb_y2_set.Text) || string.IsNullOrWhiteSpace(tb_y2_set.Text))
            {
                y2_set = "0";
                pictureBox6.Hide();
                return;
            }
        }

        private void tb_x1_set_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tb_x1_set.Text) || string.IsNullOrWhiteSpace(tb_x1_set.Text))
            {
                x1_set = "0";
                pictureBox3.Hide();
                return;
            }
        }

        private void tb_x2_set_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tb_x2_set.Text) || string.IsNullOrWhiteSpace(tb_x2_set.Text))
            {
                x2_set = "0";
                pictureBox4.Hide();
                return;
            }
        }

        public void setInitState(int state)
        {
            initState = state;
            if (initState == 0)
            {
                lbl_init.Show();
                lbl_init.Text = "Initializing";
                lbl_init.ForeColor = Color.Orange;
            }
            else if (initState == 1)
            {
                lbl_init.Show();
                lbl_init.Text = "Initialized";
                lbl_init.ForeColor = Color.LightGreen;
                timer4.Start();
                initTime = DateTime.Now;
                initCounter = initCounter + 1;
            }
            else if (initState == 2)
            {
                lbl_init.Show();
                lbl_init.Text = "Initialization Failed";
                lbl_init.ForeColor = Color.Red;
                timer4.Start();
            }
            else if (initState == -1)
            {
                lbl_init.Hide();
            }
        }

        public void SetConnection(bool connect)
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length == 0)
            {
                MessageBox.Show("No Serial port detected!", "COM Port error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //else if (ports.Length > 1)
            //    MessageBox.Show("More than one Serial port detected!\n" + portName + " Selected to connect.", "Multiple COM Port", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (connect)
            {
                if (portName != "Null" && portName != "")
                {
                    serialPort1.PortName = portName;
                }
                else
                {
                    serialPort1.PortName = ports[0];
                }
                portName = serialPort1.PortName;

                serialPort1.BaudRate = int.Parse(curr_baudRate);
                serialPort1.DataBits = int.Parse(DataBits);
                if (Parity == "None")
                    serialPort1.Parity = System.IO.Ports.Parity.None;
                else if (Parity == "Odd")
                    serialPort1.Parity = System.IO.Ports.Parity.Odd;
                else if (Parity == "Even")
                    serialPort1.Parity = System.IO.Ports.Parity.Even;

                //int BaudRate = 0;
                //if (int.TryParse(HashPass.ReadBaudRateFromReg(), out BaudRate) == true && BaudRate != 0)
                //{
                //    serialPort1.BaudRate = BaudRate;
                //}

                //DisconnectPort();
                //Thread.Sleep(200);
                ConnectToPort();
                
            }
            else
            {
                DisconnectPort();
            }
        }

        private void DisconnectPort()
        {
            //GlobalSerialPort.DiscardInBuffer();
            //GlobalSerialPort.Close();
            tabControl1.Enabled = false;
            //panel_AdminControls.Enabled = false;
            panel_ClientControls.Enabled = false;
            picBtn_Connect.BackgroundImage = Resources.ConnectButton;
            picBtnToolTip.SetToolTip(picBtn_Connect, "Connect");
            label_ConnectStatus.ForeColor = Color.Red;
            label_ConnectStatus.Text = "Disconnected!";
            ClosePort();
        }

        public void ReadCalibFile()
        {
            try
            {
                string appPath = Application.StartupPath;
                string dataPath = System.IO.Path.Combine(appPath, "Calib.dat");
                if (!System.IO.File.Exists(dataPath))
                {
                    timer1.Stop();
                    timer1.Enabled = false;
                    MessageBox.Show("Can not connect to port!\n''Calib.dat'' file not found!", "Calibration file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                HashPass.CalibData values = HashPass.readCalibJson(dataPath);
                gant_gain = double.Parse(values.gant_gain);
                gant_offset = double.Parse(values.gant_offset);
                collim_gain = double.Parse(values.collim_gain);
                collim_offset = double.Parse(values.collim_offset);
                x1_gain = double.Parse(values.x1_gain);
                x1_offset = double.Parse(values.x1_offset);
                x2_gain = double.Parse(values.x2_gain);
                x2_offset = double.Parse(values.x2_offset);
                y1_gain = double.Parse(values.y1_gain);
                y1_offset = double.Parse(values.y1_offset);
                y2_gain = double.Parse(values.y2_gain);
                y2_offset = double.Parse(values.y2_offset);

                tb_gant_gain.Text = Math.Round(gant_gain, 7, MidpointRounding.ToEven).ToString();
                tb_coli_gain.Text = Math.Round(collim_gain, 7, MidpointRounding.ToEven).ToString();
                tb_x1_gain.Text = Math.Round(x1_gain, 7, MidpointRounding.ToEven).ToString();
                tb_x2_gain.Text = Math.Round(x2_gain, 7, MidpointRounding.ToEven).ToString();
                tb_y1_gain.Text = Math.Round(y1_gain, 7, MidpointRounding.ToEven).ToString();
                tb_y2_gain.Text = Math.Round(y2_gain, 7, MidpointRounding.ToEven).ToString();

                tb_gant_offset.Text = Math.Round(gant_offset, 3, MidpointRounding.ToEven).ToString();
                tb_coli_offset.Text = Math.Round(collim_offset, 3, MidpointRounding.ToEven).ToString();
                tb_x1_offset.Text = Math.Round(x1_offset, 3, MidpointRounding.ToEven).ToString();
                tb_x2_offset.Text = Math.Round(x2_offset, 3, MidpointRounding.ToEven).ToString();
                tb_y1_offset.Text = Math.Round(y1_offset, 3, MidpointRounding.ToEven).ToString();
                tb_y2_offset.Text = Math.Round(y2_offset, 3, MidpointRounding.ToEven).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading from Calib file" + Environment.NewLine + ex.ToString().Split('\n')[0]);
            }
        }

        public void ReadLearnFile()
        {
            try
            {
                string appPath = Application.StartupPath;
                string dataPath = System.IO.Path.Combine(appPath, "Learn.dat");
                if (!System.IO.File.Exists(dataPath))
                {
                    timer1.Stop();
                    timer1.Enabled = false;
                    MessageBox.Show("Can not connect to port!\n''Learn.dat'' file not found!", "Learn file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                HashPass.LearnData values = HashPass.readLearnJson(dataPath);
                tb_gant_zpnt.Text = gant_zpnt = values.gant_zpnt;
                tb_gant_len.Text = gant_length = values.gant_length;
                tb_gant_flen.Text = gant_fine_length = values.gant_fine_length;
                tb_coli_zpnt.Text = collim_zpnt = values.collim_zpnt;
                tb_coli_len.Text = collim_length = values.collim_length;
                tb_coli_flen.Text = collim_fine_length = values.collim_fine_length;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading from Learn file" + Environment.NewLine + ex.ToString().Split('\n')[0]);
            }
        }

        public void ReadParametersFile()
        {
            try
            {
                string appPath = Application.StartupPath;
                string dataPath = System.IO.Path.Combine(appPath, "Parameters.dat");
                if (!System.IO.File.Exists(dataPath))
                {
                    timer1.Stop();
                    timer1.Enabled = false;
                    MessageBox.Show("Can not connect to port!\n''Parameters.dat'' file not found!", "Parameters file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                HashPass.ParametersData values = HashPass.readParametersJson(dataPath);

                string[] prms = new string[44];

                prms[0] = gant_tol_1 = values.gant_tol_1;
                prms[1] = gant_tol0 = values.gant_tol0;
                prms[2] = gant_tol1 = values.gant_tol1;
                prms[3] = gant_tol2 = values.gant_tol2;
                prms[4] = gant_v1 = values.gant_v1;
                prms[5] = gant_v2 = values.gant_v2;
                prms[6] = gant_v3 = values.gant_v3;
                prms[7] = collim_tol_1 = values.collim_tol_1;
                prms[8] = collim_tol0 = values.collim_tol0;
                prms[9] = collim_tol1 = values.collim_tol1;
                prms[10] = collim_tol2 = values.collim_tol2;
                prms[11] = collim_v1 = values.collim_v1;
                prms[12] = collim_v2 = values.collim_v2;
                prms[13] = collim_v3 = values.collim_v3;
                prms[14] = x1_tol_1 = values.x1_tol_1;
                prms[15] = x1_tol0 = values.x1_tol0;
                prms[16] = x1_tol1 = values.x1_tol1;
                prms[17] = x1_tol2 = values.x1_tol2;
                prms[18] = x1_v1 = values.x1_v1;
                prms[19] = x1_v2 = values.x1_v2;
                prms[20] = x1_v3 = values.x1_v3;
                prms[21] = x2_tol_1 = values.x2_tol_1;
                prms[22] = x2_tol0 = values.x2_tol0;
                prms[23] = x2_tol1 = values.x2_tol1;
                prms[24] = x2_tol2 = values.x2_tol2;
                prms[25] = x2_v1 = values.x2_v1;
                prms[26] = x2_v2 = values.x2_v2;
                prms[27] = x2_v3 = values.x2_v3;
                prms[28] = y1_tol_1 = values.y1_tol_1;
                prms[29] = y1_tol0 = values.y1_tol0;
                prms[30] = y1_tol1 = values.y1_tol1;
                prms[31] = y1_tol2 = values.y1_tol2;
                prms[32] = y1_v1 = values.y1_v1;
                prms[33] = y1_v2 = values.y1_v2;
                prms[34] = y1_v3 = values.y1_v3;
                prms[35] = y2_tol_1 = values.y2_tol_1;
                prms[36] = y2_tol0 = values.y2_tol0;
                prms[37] = y2_tol1 = values.y2_tol1;
                prms[38] = y2_tol2 = values.y2_tol2;
                prms[39] = y2_v1 = values.y2_v1;
                prms[40] = y2_v2 = values.y2_v2;
                prms[41] = y2_v3 = values.y2_v3;
                prms[42] = gravity_up = values.gravity_up;
                prms[43] = gravity_down = values.gravity_down;

                int i = 0;
                foreach (Control tb in gb_parameters.Controls)
                {
                    if (tb is TextBox)
                    {
                        tb.Text = prms[tb.TabIndex - 7];
                        i = i + 1;
                    }
                }

                string[] ourParams = new string[50];
                ourParams[0] = gant_zpnt;
                ourParams[1] = gant_length;
                ourParams[2] = gant_fine_length;
                ourParams[3] = collim_zpnt;
                ourParams[4] = collim_length;
                ourParams[5] = collim_fine_length;
                Array.Copy(prms, 0, ourParams, 6, prms.Length);
                ourParameters = ourParams;
                ClientControls.ourParameters = ourParams;
                //ourSum = 0;
                //foreach (string param in ourParams)
                //{
                //    ourSum = ourSum + double.Parse(param);
                //}
                //ClientControls.sendParametersFlag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading from Parameters file" + Environment.NewLine + ex.ToString().Split('\n')[0]);
            }
        }

        private void ConnectToPort()
        {
            try
            {
                connectTime = DateTime.Now;
                connCounter = connCounter + 1;

                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                    //Thread.Sleep(200);
                }

                if (isInServiceMode == true)
                {
                    //tabControl1.Enabled = true;
                    //panel_AdminControls.Enabled = true;
                    //write("y");
                    timer1.Enabled = true;
                    clientFrm.timer1.Enabled = false;
                }
                else
                {
                    //write("x");
                    //panel_ClientControls.Enabled = true;
                    timer1.Enabled = false;
                    clientFrm.timer1.Enabled = true;
                }

                ReadCalibFile();
                ReadLearnFile();
                ReadParametersFile();

                tabControl1.Enabled = true;
                panel_AdminControls.Enabled = true;
                panel_ClientControls.Enabled = true;
                picBtn_Connect.BackgroundImage = Resources.ConnectButton_Connected;
                picBtnToolTip.SetToolTip(picBtn_Connect, "Disconnect");
                label_ConnectStatus.ForeColor = Color.LightGreen;
                label_ConnectStatus.Text = "Connected!";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error!" + Environment.NewLine + ex.ToString().Split('\n')[0] , "An error occured during connection!\n", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label_ConnectStatus.Text = "Connection error!";
                label_ConnectStatus.ForeColor = Color.Red;
                //ConnectToPort();
            }
        }

        private void picBtn_Setting_Click(object sender, EventArgs e)
        {
            Form_Settings frmSet = new Form_Settings();
            if (isInServiceMode == false)
            {
                PrepareSettingForPort(frmSet);
            }
            else if (this.Text.Contains("Service"))
            {
                curr_baudRate = serialPort1.BaudRate.ToString();
                PrepareSettingForAll(frmSet);
            }
            if (frmSet.ShowDialog() == DialogResult.OK)
            {
                if (portName == "Null")
                    return;
                if (isInServiceMode)
                {
                    DisconnectPort();
                    Thread.Sleep(200);

                    serialPort1.PortName = portName;
                    serialPort1.BaudRate = int.Parse(curr_baudRate);
                    serialPort1.DataBits = int.Parse(DataBits);
                    if (Parity == "None")
                        serialPort1.Parity = System.IO.Ports.Parity.None;
                    else if (Parity == "Odd")
                        serialPort1.Parity = System.IO.Ports.Parity.Odd;
                    else if (Parity == "Even")
                        serialPort1.Parity = System.IO.Ports.Parity.Even;

                    // HashPass.WriteBaudrateToReg(curr_baudRate);
                    ConnectToPort();
                }
            }
        }

        private static void PrepareSettingForAll(Form_Settings frmSet)
        {
            frmSet.checkBox_startup.Show();
            frmSet.groupBox_UserManagement.Show();
            frmSet.groupBox_portSetting.Show();
        }

        private static void PrepareSettingForPort(Form_Settings frmSet)
        {
            frmSet.checkBox_startup.Hide();
            frmSet.groupBox_UserManagement.Hide();
            frmSet.groupBox_portSetting.Hide();
        }


        private void Form1_Activated(object sender, EventArgs e)
        {
            //if (isInServiceMode)
            //{
            //    this.TopMost = false;
            //    EnableCTRLALTDEL();
            //}
            //else
            //{
            //    this.TopMost = true;
            //    KillCtrlAltDelete();
            //}
            if (Class_PatientData.valuesChanged == true)
            {
                clientFrm.FillValues();
                //clientFrm.FillPatientData();
                //clientFrm.FillFieldData();
                Class_PatientData.valuesChanged = false;
            }
        }

        private void label40_Click(object sender, EventArgs e)
        {
            //Form_TrialReport trialFrm = new Form_TrialReport();
            //trialFrm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String[] versionInfo = Application.ProductVersion.Split('.');
            lbl_version.Text = versionInfo[0] + '.' + versionInfo[1] + '.' + versionInfo[2];
            if (isInServiceMode)
            {
                //panel1.BackColor = Color.Turquoise;
                label_title.ForeColor = Color.Turquoise;
                label_title.Text = "Service";
                picBtn_Exit.Show();
                picBtn_Close.Show();
                picBtn_Setting.Show();
            }
            else
            {
                //panel1.BackColor = Color.LightPink;
                label_title.ForeColor = Color.LightPink;
                label_title.Text = "Clinical";
                //picBtn_Exit.Hide();
                //picBtn_Close.Hide();
                picBtn_Setting.Hide();
            }
            //Form_TrialReport trialFrm = new Form_TrialReport();
            //trialFrm.ShowDialog();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtBox_Enter(object sender, EventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void picBtn_Restart_MouseEnter(object sender, EventArgs e)
        {
            picBtnToolTip.SetToolTip(picBtn_Restart, "Restart Computer");
            picBtn_Restart.BackgroundImage = Resources.Restart_mouseOver;
        }

        private void picBtn_Restart_MouseLeave(object sender, EventArgs e)
        {
            picBtn_Restart.BackgroundImage = Resources.Restart;
        }

        private void picBtn_Shutdown_MouseEnter(object sender, EventArgs e)
        {
            picBtnToolTip.SetToolTip(picBtn_Shutdown, "Shutdown Computer");
            picBtn_Shutdown.BackgroundImage = Resources.Shutdown_mouseOver;
        }

        private void picBtn_Shutdown_MouseLeave(object sender, EventArgs e)
        {
            picBtn_Shutdown.BackgroundImage = Resources.Shutdown;
        }

        private void picBtn_Restart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Restart the computer?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Process.Start("Shutdown", "/r /t 1");
            }
        }

        private void picBtn_Shutdown_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Shutdown the computer?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Process.Start("Shutdown", "/s /t 1");
            }
        }


        public bool inputADC = false;
        public bool readError = false;
        int adcCheck_counter = 0;
        private PerformanceCounter theCPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter theMemCounter = new PerformanceCounter("Memory", "Available MBytes");

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            TimeSpan upTime = now - startTime;
            label_upTime.Text = "UpTime: " + upTime.Hours + ":" + upTime.Minutes + ":" + upTime.Seconds;

            if (label_ConnectStatus.Text == "Connected!")
            {
                TimeSpan connectedTime = now - connectTime;
                label_connectTime.Text = "Conn(" + connCounter + "): " + connectedTime.Hours + ":" + connectedTime.Minutes + ":" + connectedTime.Seconds;
            }
            else
                label_connectTime.Text = "Conn(" + connCounter + "): x";


            if (initState == 1)
            {
                TimeSpan initializedTime = now - initTime;
                label_initTime.Text = "Init(" + initCounter + "): " + initializedTime.Hours + ":" + initializedTime.Minutes + ":" + initializedTime.Seconds;
            }
            else
                label_initTime.Text = "Init(" + initCounter + "): x";


            double cpuUsage = this.theCPUCounter.NextValue();
            label_cpu.Text = "CPU: " + cpuUsage.ToString("00.") + " %";
            double ram = this.theMemCounter.NextValue();
            double ramUsage = 100 - (ram / TotalVisibleMemorySize) * 100;
            label_ram.Text = "RAM: " + ramUsage.ToString("00.") + " %";
            if (cpuUsage > 90)
                label_cpu.ForeColor = Color.OrangeRed;
            else
                label_cpu.ForeColor = Color.LightGreen;
            if (ramUsage > 90)
                label_ram.ForeColor = Color.OrangeRed;
            else
                label_ram.ForeColor = Color.LightGreen;


            string time = now.ToShortTimeString();
            string date = now.ToShortDateString();
            //PersianCalendar pc = new PersianCalendar();
            string miladiDate = now.Year + "/" +
                now.Month + "/" + now.Day;
            //string shamsiDate = pc.GetYear(now) + "/" +
            //    pc.GetMonth(now) + "/" + pc.GetDayOfMonth(now);
            label_time.Text = time;
            label_date.Text = miladiDate;
            //label_shamsiDate.Text = shamsiDate;

            if (inputADC == false || readError)
            {
                if (isInServiceMode)
                    pb_receiveStatus.BackgroundImage = Resources.led_red;
                else
                {
                    clientFrm.pb_receiveStatus.BackgroundImage = Resources.led_red;
                }
            }
            else
            {
                if (isInServiceMode)
                    pb_receiveStatus.BackgroundImage = Resources.led_green;
                else
                {
                    clientFrm.pb_receiveStatus.BackgroundImage = Resources.led_green;
                }
            }

            inputADC = false;
            if ((!isInServiceMode) && (!serialPort1.IsOpen))
            {
                clientFrm.pb_gant_status.BackgroundImage = Resources.led_red;
                clientFrm.pb_coli_status.BackgroundImage = Resources.led_red;
                clientFrm.pb_x1_status.BackgroundImage = Resources.led_red;
                clientFrm.pb_x2_status.BackgroundImage = Resources.led_red;
                clientFrm.pb_y1_status.BackgroundImage = Resources.led_red;
                clientFrm.pb_y2_status.BackgroundImage = Resources.led_red;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btn_start_stop.Text == "Stop")
            {
                button14_Click(sender, e);
            }
        }

        private void picBtn_Close_Click(object sender, EventArgs e)
        {
            quit = true;
            //ClosePort();
            if (serialPort1.IsOpen == true)
                MessageBox.Show("Please click on ''Disconnect'' button before exit!");
            else
                Application.Exit();

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                SetConnection(true);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Left)
            {
                Application.Exit();
            }
        }

        public static int initState = -1;
        public static double ourSum = 0;
        private void btn_saveParameters_Click(object sender, EventArgs e)
        {
            //initState = 0 ;
            string[] values = new string[44];
            int i = 0;
            foreach (Control tb in gb_parameters.Controls)
            {
                if (tb is TextBox)
                {
                    values[tb.TabIndex-7] = tb.Text;
                    i = i + 1;
                }
            }
            gant_tol_1 = values[0];
            gant_tol0 = values[1];
            gant_tol1 = values[2];
            gant_tol2 = values[3];
            gant_v1 = values[4];
            gant_v2 = values[5];
            gant_v3 = values[6];
            collim_tol_1 = values[7];
            collim_tol0 = values[8];
            collim_tol1 = values[9];
            collim_tol2 = values[10];
            collim_v1 = values[11];
            collim_v2 = values[12];
            collim_v3 = values[13];
            y1_tol_1 = values[14];
            y1_tol0 = values[15];
            y1_tol1 = values[16];
            y1_tol2 = values[17];
            y1_v1 = values[18];
            y1_v2 = values[19];
            y1_v3 = values[20];
            y2_tol_1 = values[21];
            y2_tol0 = values[22];
            y2_tol1 = values[23];
            y2_tol2 = values[24];
            y2_v1 = values[25];
            y2_v2 = values[26];
            y2_v3 = values[27];
            x1_tol_1 = values[28];
            x1_tol0 = values[29];
            x1_tol1 = values[30];
            x1_tol2 = values[31];
            x1_v1 = values[32];
            x1_v2 = values[33];
            x1_v3 = values[34];
            x2_tol_1 = values[35];
            x2_tol0 = values[36];
            x2_tol1 = values[37];
            x2_tol2 = values[38];
            x2_v1 = values[39];
            x2_v2 = values[40];
            x2_v3 = values[41];
            gravity_up = values[42];
            gravity_down = values[43];

            try
            {
                string appPath = Application.StartupPath;
                string dataPath = System.IO.Path.Combine(appPath, "Parameters.dat");
                HashPass.writeParametersJson(dataPath, values);

                string[] ourParams = new string[50];
                ourParams[0] = gant_zpnt;
                ourParams[1] = gant_length;
                ourParams[2] = gant_fine_length;
                ourParams[3] = collim_zpnt;
                ourParams[4] = collim_length;
                ourParams[5] = collim_fine_length;
                Array.Copy(values, 0, ourParams, 6, values.Length);
                ourParameters = ourParams;
                ClientControls.ourParameters = ourParams;
                //ourSum = 0;
                //foreach (string param in ourParams)
                //{
                //    ourSum = ourSum + double.Parse(param);
                //}
                write("w");
                //sendParameters();
                btn_saveParameters.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error saving parameters to file" + Environment.NewLine + ex.ToString().Split('\n')[0]);
            }
        }
    }
}