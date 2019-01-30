using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Compact_Control.Properties;

namespace Compact_Control
{
    public partial class ClientControls : UserControl
    {
        //private SerialPort _serialPort1;
        //public SerialPort serialPort1
        //{
        //    set
        //    {
        //        if (_serialPort1 != value)
        //            _serialPort1 = value;
        //    }
        //    get 
        //    {
        //        return _serialPort1;
        //    }
        //}
        string gant_cofin;
        public static double gant_gain, gant_offset;
        string gant_dv;
        string gnd;
        public static string gant_zpnt, gant_length, gant_fine_length;

        string collim_cofin;
        public static double collim_gain, collim_offset;
        string collim_dv;
        string cld;
        public static string collim_zpnt, collim_length, collim_fine_length;

        double gant_t2, gant_d2, collim_t2, collim_d2;

        public static string gant_tol0, gant_tol1, gant_tol2, gant_v1, gant_v2, gant_v3;
        public static string collim_tol0, collim_tol1, collim_tol2, collim_v1, collim_v2, collim_v3;
        public static string x1_tol0, x1_tol1, x1_tol2, x1_v1, x1_v2, x1_v3;
        public static string x2_tol0, x2_tol1, x2_tol2, x2_v1, x2_v2, x2_v3;
        public static string y1_tol0, y1_tol1, y1_tol2, y1_v1, y1_v2, y1_v3;
        public static string y2_tol0, y2_tol1, y2_tol2, y2_v1, y2_v2, y2_v3;
        bool sendParametersFlag = false;

        string x1_co;
        public static double x1_gain, x1_offset;
        string x1_dv;
        string x1d;

        string x2_co;
        public static double x2_gain, x2_offset;
        string x2_dv;
        string x2d;

        string y1_co;
        public static double y1_gain, y1_offset;
        string y1_dv;
        string y1d;

        string y2_co;
        public static double y2_gain, y2_offset;
        string y2_dv;
        string y2d;

        string gant_set="0";
        string collim_set="0";
        string x1_set="0";
        string x2_set="0";
        string y1_set="0";
        string y2_set="0";

        string adc;

        public static int curr_baudrate = 57600;
        public static string curr_port;

        public ClientControls()
        {
            InitializeComponent();
        }

        public void TimerStatus(bool enabled)
        {
            timer1.Enabled = enabled;
            timer3.Enabled = enabled;
        }

        public void FillValues()
        {
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
            txt_gant_s.Text = Class_PatientData.currValues[15];
            txt_coli_s.Text = Class_PatientData.currValues[16];
            txt_x1_s.Text = Class_PatientData.currValues[17];
            txt_x2_s.Text = Class_PatientData.currValues[18];
            txt_y1_s.Text = Class_PatientData.currValues[19];
            txt_y2_s.Text = Class_PatientData.currValues[20];
        }

        /*
        public void FillPatientData()
        {
            lbl_PID.Text = Class_PatientData.currPatient[0];
            lbl_PName.Text = Class_PatientData.currPatient[2] + " " + Class_PatientData.currPatient[1];
            lbl_Dr.Text = Class_PatientData.currPatient[3];
        }

        public void FillFieldData()
        {
            lbl_FName.Text = Class_PatientData.currValues[2];
            lbl_Site.Text = Class_PatientData.currValues[3];
            lbl_ssd.Text = Class_PatientData.currValues[4];
            lbl_Dose.Text = Class_PatientData.currValues[5];
            lbl_MU.Text = Class_PatientData.currValues[6];
            lbl_Wedge.Text = Class_PatientData.currValues[7];
            lbl_shadowTray.Text = Class_PatientData.currValues[8];
            lbl_Bolous.Text = Class_PatientData.currValues[9];
            txt_Iso_s.Text = Class_PatientData.currValues[10];
            txt_Column_s.Text = Class_PatientData.currValues[11];
            txt_Vert_s.Text = Class_PatientData.currValues[12];
            txt_Lat_s.Text = Class_PatientData.currValues[13];
            txt_Long_s.Text = Class_PatientData.currValues[14];
            btn_ShowFields.Enabled = true;
            btn_ClearField.Enabled = true;
        }
         */
        public static string[] ourParameters = new string[42];
        public bool compareParameters(string[] microParams, string[] ourParams)
        {
            bool equal = true;
            for (int i=0; i<microParams.Length; i++)
            {
                if (microParams[i] != ourParams[i])
                {
                    equal = false;
                    break;
                }
            }
            return equal;
        }
        public bool sendParameters()
        {
            try
            {
                
                if (serialPort1.IsOpen == false)
                {
                    serialPort1.BaudRate = curr_baudrate;
                    serialPort1.PortName = curr_port;
                    serialPort1.Open();
                }
                    
                //serialPort1.Open();
                string gant_tol0_t = Math.Round(Math.Abs((double.Parse(gant_tol0) - gant_offset) / gant_gain)).ToString();
                string gant_tol1_t = Math.Round(Math.Abs((double.Parse(gant_tol1) - gant_offset) / gant_gain)).ToString();
                string gant_tol2_t = Math.Round(Math.Abs((double.Parse(gant_tol2) - gant_offset) / gant_gain)).ToString();

                string collim_tol0_t = Math.Round(Math.Abs((double.Parse(collim_tol0) - collim_offset) / collim_gain)).ToString();
                string collim_tol1_t = Math.Round(Math.Abs((double.Parse(collim_tol1) - collim_offset) / collim_gain)).ToString();
                string collim_tol2_t = Math.Round(Math.Abs((double.Parse(collim_tol2) - collim_offset) / collim_gain)).ToString();

                string x1_tol0_t = Math.Round(Math.Abs((double.Parse(x1_tol0) - x1_offset) / x1_gain)).ToString();
                string x1_tol1_t = Math.Round(Math.Abs((double.Parse(x1_tol1) - x1_offset) / x1_gain)).ToString();
                string x1_tol2_t = Math.Round(Math.Abs((double.Parse(x1_tol2) - x1_offset) / x1_gain)).ToString();

                string x2_tol0_t = Math.Round(Math.Abs((double.Parse(x2_tol0) - x2_offset) / x2_gain)).ToString();
                string x2_tol1_t = Math.Round(Math.Abs((double.Parse(x2_tol1) - x2_offset) / x2_gain)).ToString();
                string x2_tol2_t = Math.Round(Math.Abs((double.Parse(x2_tol2) - x2_offset) / x2_gain)).ToString();

                string y1_tol0_t = Math.Round(Math.Abs((double.Parse(y1_tol0) - y1_offset) / y1_gain)).ToString();
                string y1_tol1_t = Math.Round(Math.Abs((double.Parse(y1_tol1) - y1_offset) / y1_gain)).ToString();
                string y1_tol2_t = Math.Round(Math.Abs((double.Parse(y1_tol2) - y1_offset) / y1_gain)).ToString();

                string y2_tol0_t = Math.Round(Math.Abs((double.Parse(y2_tol0) - y2_offset) / y2_gain)).ToString();
                string y2_tol1_t = Math.Round(Math.Abs((double.Parse(y2_tol1) - y2_offset) / y2_gain)).ToString();
                string y2_tol2_t = Math.Round(Math.Abs((double.Parse(y2_tol2) - y2_offset) / y2_gain)).ToString();

                ourSum = double.Parse(gant_tol0_t) + double.Parse(gant_tol1_t) + double.Parse(gant_tol2_t) +
                         double.Parse(collim_tol0_t) + double.Parse(collim_tol1_t) + double.Parse(collim_tol2_t) +
                         double.Parse(x1_tol0_t) + double.Parse(x1_tol1_t) + double.Parse(x1_tol2_t) +
                         double.Parse(x2_tol0_t) + double.Parse(x2_tol1_t) + double.Parse(x2_tol2_t) +
                         double.Parse(y1_tol0_t) + double.Parse(y1_tol1_t) + double.Parse(y1_tol2_t) +
                         double.Parse(y2_tol0_t) + double.Parse(y2_tol1_t) + double.Parse(y2_tol2_t) +
                         double.Parse(gant_v1) + double.Parse(gant_v2) + double.Parse(gant_v3) +
                         double.Parse(collim_v1) + double.Parse(collim_v2) + double.Parse(collim_v3) +
                         double.Parse(x1_v1) + double.Parse(x1_v2) + double.Parse(x1_v3) +
                         double.Parse(x2_v1) + double.Parse(x2_v2) + double.Parse(x2_v3) +
                         double.Parse(y1_v1) + double.Parse(y1_v2) + double.Parse(y1_v3) +
                         double.Parse(y2_v1) + double.Parse(y2_v2) + double.Parse(y2_v3) +
                         double.Parse(gant_zpnt) + double.Parse(gant_length) + double.Parse(gant_fine_length) +
                         double.Parse(collim_zpnt) + double.Parse(collim_length) + double.Parse(collim_fine_length);

                write("w");
                write(gant_zpnt + "/" + gant_length + "/" + gant_fine_length + "/");
                write(collim_zpnt + "/" + collim_length + "/" + collim_fine_length + "/");
                write(gant_tol0_t + "/" + gant_tol1_t + "/" + gant_tol2_t + "/");
                write(gant_v1 + "/" + gant_v2 + "/" + gant_v3 + "/");
                write(collim_tol0_t + "/" + collim_tol1_t + "/" + collim_tol2_t + "/");
                write(collim_v1 + "/" + collim_v2 + "/" + collim_v3 + "/");
                write(x1_tol0_t + "/" + x1_tol1_t + "/" + x1_tol2_t + "/");
                write(x1_v1 + "/" + x1_v2 + "/" + x1_v3 + "/");
                write(x2_tol0_t + "/" + x2_tol1_t + "/" + x2_tol2_t + "/");
                write(x2_v1 + "/" + x2_v2 + "/" + x2_v3 + "/");
                write(y1_tol0_t + "/" + y1_tol1_t + "/" + y1_tol2_t + "/");
                write(y1_v1 + "/" + y1_v2 + "/" + y1_v3 + "/");
                write(y2_tol0_t + "/" + y2_tol1_t + "/" + y2_tol2_t + "/");
                write(y2_v1 + "/" + y2_v2 + "/" + y2_v3 + "/");
                return true;

            }
            catch(Exception ex)
            {
                Form1.initState = 2;
                MessageBox.Show("Unable to send parameters!" + Environment.NewLine + ex.ToString().Split('\n')[0]);
                return false;
            }
        }

        string[] microParameters = new string[42];
        public Queue<string> receiveQ = new Queue<string>();
        public void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            while (serialPort1.BytesToRead > 0)
            {
                string currReceived = serialPort1.ReadLine();
                receiveQ.Enqueue(currReceived);
            }
            //string currReceived = serialPort1.ReadExisting();

            //string a = "";
            //try
            //{
            //    if (serialPort1.IsOpen)
            //        a = serialPort1.ReadLine();
            //}
            //catch
            //{                               
            //}
        }

        private double y1dv, y2dv, xa, x1dv, x2dv, ya;

        private void btn_clearTerminal_Click(object sender, EventArgs e)
        {
            tb_terminal_out.Clear();
            lbl_out_cnt.Text = "0";
        }

        private void btn_clearTerminal_in_Click(object sender, EventArgs e)
        {
            tb_terminal_in.Clear();
            lbl_in_cnt.Text = "0";
        }

        public void write(string data)
        {
            serialPort1.Write(data);
            tb_terminal_out.AppendText(data + Environment.NewLine);
        }

        private void btn_clearTerminal_oth_Click(object sender, EventArgs e)
        {
            tb_terminal_oth.Clear();
        }

        public double ourSum = 0;
        private bool checkSum(double microSum, double ourSum)
        {
            bool equal = false;
            if (microSum == ourSum)
                equal = true;
            return equal;
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            
            bool noWrite = true;
            if (receiveQ.Count == 0)
                return;
            string currData = receiveQ.Dequeue();
            //string[] lines = currData.Split('\n');
            tb_terminal_in.AppendText(currData + Environment.NewLine);
            string a = currData;
            double c;
            //foreach (string a in lines)
            //{
            try
                {
                    switch (a.Substring(0, 3))
                    {
                    case "ini":
                        Form1.initState = 0;
                        sendParametersFlag = true;
                        //sendParameters();
                        break;
                    case "sum":
                        string microSum = a.Substring(3, a.Length - 3);
                        if (checkSum(double.Parse(microSum), ourSum) == true)
                        {
                            write("{|}~");
                            Form1.initState = 1;
                        }
                        else
                        {
                            //write("$");
                            //sendParametersFlag = true;
                            //sendParameters();
                        }
                        break;
                    case "gfn":
                        gant_cofin = a.Substring(3, a.Length - 3);
                        c = Math.Round((gant_gain * double.Parse(gant_cofin) + gant_offset), 1, MidpointRounding.ToEven);
                        if (c < 0)
                        {
                            c = c + 360;
                        }
                        gant_dv = c.ToString();
                        break;
                    case "cfn":
                        collim_cofin = a.Substring(3, a.Length - 3);
                        c = Math.Round((collim_gain * double.Parse(collim_cofin) + collim_offset), 1, MidpointRounding.ToEven);
                        if (c < 0)
                        {
                            c = c + 360;
                        }
                        collim_dv = c.ToString();
                        break;
                    case "wco":
                        x1_co = a.Substring(3, a.Length - 3);
                        x1_dv = Math.Round(((x1_gain * double.Parse(x1_co)) + x1_offset), 1, MidpointRounding.ToEven).ToString();
                        break;
                    case "xco":
                        x2_co = a.Substring(3, a.Length - 3);
                        x2_dv = Math.Round(-((x2_gain * double.Parse(x2_co)) + x2_offset), 1, MidpointRounding.ToEven).ToString();
                        break;
                    case "yco":
                        y1_co = a.Substring(3, a.Length - 3);
                        y1_dv = Math.Round(((y1_gain * double.Parse(y1_co)) + y1_offset), 1, MidpointRounding.ToEven).ToString();
                        break;
                    case "zco":
                        y2_co = a.Substring(3, a.Length - 3);
                        y2_dv = Math.Round(-((y2_gain * double.Parse(y2_co)) + y2_offset), 1, MidpointRounding.ToEven).ToString();
                        break;
                    case "gnd":
                        gnd = a.Substring(3, a.Length - 3);
                        break;
                    case "cld":
                        cld = a.Substring(3, a.Length - 3);
                        break;
                    case "x1d":
                        x1d = a.Substring(3, a.Length - 3);
                        break;
                    case "x2d":
                        x2d = a.Substring(3, a.Length - 3);
                        break;
                    case "y1d":
                        y1d = a.Substring(3, a.Length - 3);
                        break;
                    case "y2d":
                        y2d = a.Substring(3, a.Length - 3);
                        break;
                    case "adc":
                        int i = int.Parse(lbl_in_cnt.Text);
                        i = i + 1;
                        lbl_in_cnt.Text = i.ToString();
                        adc = a.Substring(3, a.Length - 3);
                        //adc = "1999";
                        if (int.Parse(adc) < 2000 || int.Parse(adc) > 2100)
                        {
                            gant_set = "0";
                            collim_set = "0";
                            x1_set = "0";
                            x2_set = "0";
                            y1_set = "0";
                            y2_set = "0";
                            Reading_Error.Text = "Reading Error";
                        }
                        else
                        {
                            Reading_Error.Text = "";
                        }
                        if (int.Parse(gant_set) != int.Parse(gnd))
                        {
                            write("m" + gant_set + (gant_set.Length + 1).ToString() + "/");
                            noWrite = false;
                        }
                        if (int.Parse(collim_set) != int.Parse(cld))
                        { 
                            write("n" + collim_set + (collim_set.Length + 1).ToString() + "/");
                            noWrite = false;
                        }
                        if (int.Parse(x1_set) != int.Parse(x1d))
                        { 
                            write("o" + x1_set + (x1_set.Length + 1).ToString() + "/");
                            noWrite = false;
                        }
                        if (int.Parse(x2_set) != int.Parse(x2d))
                        { 
                            write("p" + x2_set + (x2_set.Length + 1).ToString() + "/");
                            noWrite = false;
                        }
                        if (int.Parse(y1_set) != int.Parse(y1d))
                        { 
                            write("q" + y1_set + (y1_set.Length + 1).ToString() + "/");
                            noWrite = false;
                        }
                        if (int.Parse(y2_set) != int.Parse(y2d))
                        { 
                            write("r" + y2_set + (y2_set.Length + 1).ToString() + "/");
                            noWrite = false;
                        }
                        if (noWrite == false)
                        {
                            int o = int.Parse(lbl_out_cnt.Text);
                            o = o + 1;
                            lbl_out_cnt.Text = o.ToString();
                        }
                        break;
                    case "gco":
                        break;
                    case "gf1":
                        break;
                    case "gf2":
                        break;
                    case "cco":
                        break;
                    case "cf1":
                        break;
                    case "cf2":
                        break;
                    default:
                        tb_terminal_oth.AppendText(a + "-->" + a.Substring(0, 3) + Environment.NewLine);
                        break;
                    }
                    if (Class_PatientData.isBoardReadWrite)
                    {
                    }
                }
                catch
                {
                }
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sendParametersFlag == true)
            {
                sendParametersFlag = false;
                sendParameters();
                //if (sendParameters() == true)
                //{
                //    MessageBox.Show("Parameters Save & Send successful!");
                //}
            }

            //double a;
            //try
            //{
            //    if (gant_cofin != null)
            //    {
            //        a = Math.Round((gant_gain * double.Parse(gant_cofin) + gant_offset), 1, MidpointRounding.ToEven);
            //        if (a < 0)
            //        {
            //            a = a + 360;
            //        }
            //        gant_dv = a.ToString();
            //    }

            //    if (collim_cofin != null)
            //    {
            //        a = Math.Round((collim_gain * double.Parse(collim_cofin) + collim_offset), 1, MidpointRounding.ToEven);
            //        if (a < 0)
            //        {
            //            a = a + 360;
            //        }
            //        collim_dv = a.ToString();
            //    }

            //    if (x1_co != null)
            //        x1_dv = Math.Round(((x1_gain * double.Parse(x1_co)) + x1_offset), 1, MidpointRounding.ToEven).ToString();
            //    if (x2_co != null)
            //        x2_dv = Math.Round(-((x2_gain * double.Parse(x2_co)) + x2_offset), 1, MidpointRounding.ToEven).ToString();
            //    if (y1_co != null)
            //        y1_dv = Math.Round(((y1_gain * double.Parse(y1_co)) + y1_offset), 1, MidpointRounding.ToEven).ToString();
            //    if (y2_co != null)
            //        y2_dv = Math.Round(-((y2_gain * double.Parse(y2_co)) + y2_offset), 1, MidpointRounding.ToEven).ToString();                
            //}
            //catch
            //{
            //}

            txt_gant_a.Text = gant_dv;
            txt_coli_a.Text = collim_dv;
            txt_y2_a.Text = x1_dv;
            txt_y1_a.Text = x2_dv;
            txt_x2_a.Text = y1_dv;
            txt_x1_a.Text = y2_dv;

            bool y1Valid = double.TryParse(y1_dv, out y1dv);
            bool y2Valid = double.TryParse(y2_dv, out y2dv);
            if (y1Valid && y2Valid)
            {
                xa = y1dv - y2dv;
                txt_x_a.Text = xa.ToString();
            }
            bool x1Valid = double.TryParse(x1_dv, out x1dv);
            bool x2Valid = double.TryParse(x2_dv, out x2dv);
            if (x1Valid && x2Valid)
            {
                ya = x1dv - x2dv;
                txt_y_a.Text = ya.ToString();
            }


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
        }



        private void txt_gant_s_TextChanged(object sender, EventArgs e)
        {
            if (gant_isTextChangedFromCode == false)
            {
                isGantSet = false;
                txt_gant_s.BackColor = Color.White;
                pictureBox1.Hide();
                gant_set = "0";
            }
        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            isColiSet = false;
            txt_coli_s.BackColor = Color.White;
            pictureBox2.Hide();
            collim_set = "0";
        }

        private bool x1err = false;
        private bool x2err = false;
        private bool y1err = false;
        private bool y2err = false;
        private bool xy_isTextChangedFromCode = false;
        private bool XY_isTextChangedFromCode = false;
        private bool gant_isTextChangedFromCode = false;
        bool isGantSet = false;
        bool isColiSet = false;
        bool isY1Set = false;
        bool isY2Set = false;
        bool isX1Set = false;
        bool isX2Set = false;
        bool isYSymmetric = false;
        bool isXSymmetric = false;
        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            if (!xy_isTextChangedFromCode)
            {
                isY2Set = false;
                txt_y2_s.BackColor = Color.White;
                txt_y_s.BackColor = Color.White;
                txt_y_s.Clear();
                pictureBox3.Hide();
                pictureBox14.Hide();
                x1_set = "0";
            }
        }

        private void textBox39_TextChanged(object sender, EventArgs e)
        {
            if (!xy_isTextChangedFromCode)
            {
                isY1Set = false;
                txt_y1_s.BackColor = Color.White;
                txt_y_s.BackColor = Color.White;
                txt_y_s.Clear();
                pictureBox4.Hide();
                pictureBox14.Hide();
                x2_set = "0";
            }
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            if (!xy_isTextChangedFromCode)
            {
                isX2Set = false;
                txt_x2_s.BackColor = Color.White;
                txt_x_s.BackColor = Color.White;
                txt_x_s.Clear();
                pictureBox5.Hide();
                pictureBox15.Hide();
                y1_set = "0";
            }
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            if (!xy_isTextChangedFromCode)
            {
                isX1Set = false;
                txt_x1_s.BackColor = Color.White;
                txt_x_s.BackColor = Color.White;
                txt_x_s.Clear();
                pictureBox6.Hide();
                pictureBox15.Hide();
                y2_set = "0";
            }
        }

        private void ShowFields()
        {
            Class_PatientData.isFieldsChanged = true;
            Form_Fields fieldsFrm = new Form_Fields();
            fieldsFrm.ShowDialog();
        }

        private void txtBox_Enter(object sender, EventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void txt_y_s_TextChanged(object sender, EventArgs e)
        {
            if (!XY_isTextChangedFromCode)
            {
                isY1Set = false;
                isY2Set = false;
                txt_y_s.BackColor = Color.White;
                txt_y1_s.BackColor = Color.White;
                txt_y2_s.BackColor = Color.White;
                XY_isTextChangedFromCode = false;
                pictureBox14.Hide();
                pictureBox4.Hide();
                pictureBox3.Hide();
                x2_set = "0";
                x1_set = "0";
            }
        }

        private void txt_x_s_TextChanged(object sender, EventArgs e)
        {
            if (!XY_isTextChangedFromCode)
            {
                isX1Set = false;
                isX2Set = false;
                txt_x_s.BackColor = Color.White;
                txt_x1_s.BackColor = Color.White;
                txt_x2_s.BackColor = Color.White;
                XY_isTextChangedFromCode = false;
                pictureBox15.Hide();
                pictureBox6.Hide();
                pictureBox5.Hide();
                y2_set = "0";
                y1_set = "0";
            }
        }

        private void y12ErrorSign_VisibleChanged(object sender, EventArgs e)
        {
            if (y1err == true || y2err == true)
                pictureBox14.BackgroundImage = Resources.Error;
            else
                pictureBox14.BackgroundImage = Resources.Request;
            if (isYSymmetric && (pictureBox3.Visible || pictureBox4.Visible))
                pictureBox14.Show();
            else
                pictureBox14.Hide();
        }

        private void x12ErrorSign_VisibleChanged(object sender, EventArgs e)
        {
            if (x1err == true || x2err == true)
                pictureBox15.BackgroundImage = Resources.Error;
            else
                pictureBox15.BackgroundImage = Resources.Request;
            if (isXSymmetric && (pictureBox5.Visible || pictureBox6.Visible))
                pictureBox15.Show();
            else
                pictureBox15.Hide();
            
        }

        private void txt_y_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txt_y_s.Text))
                {
                    if (!XY_isTextChangedFromCode)
                    {
                        xy_isTextChangedFromCode = true;
                        if (string.IsNullOrEmpty(txt_y_s.Text) || string.IsNullOrWhiteSpace(txt_y_s.Text))
                        {
                            txt_y1_s.Clear();
                            txt_y2_s.Clear();
                        }
                        else
                        {
                            decimal y12;
                            try
                            {
                                y12 = decimal.Parse(txt_y_s.Text);
                            }
                            catch
                            {
                                txt_y_s.SelectAll();
                                pictureBox14.BackgroundImage = Resources.Error;
                                pictureBox14.Show();
                                return;
                            }
                            y12 = y12 / 2;
                            y12 = decimal.Round(y12, 2);
                            txt_y2_s.Text = y12.ToString();
                            y12 = y12 * -1;
                            txt_y1_s.Text = y12.ToString();
                            y1Act();
                            y2Act();
                            txt_y_s.BackColor = Color.LightGreen;
                            txt_y1_s.BackColor = Color.LightGreen;
                            txt_y2_s.BackColor = Color.LightGreen;
                        }
                        xy_isTextChangedFromCode = false;
                    }

                }
                txt_x_s.Focus();
            }
        }

        private void txt_x_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txt_x_s.Text))
                {
                    if (!XY_isTextChangedFromCode)
                    {
                        xy_isTextChangedFromCode = true;
                        if (string.IsNullOrEmpty(txt_x_s.Text) || string.IsNullOrWhiteSpace(txt_x_s.Text))
                        {
                            txt_x1_s.Clear();
                            txt_x2_s.Clear();
                        }
                        else
                        {
                            decimal x12;
                            try
                            {
                                x12 = decimal.Parse(txt_x_s.Text);
                            }
                            catch
                            {
                                txt_x_s.SelectAll();
                                pictureBox15.BackgroundImage = Resources.Error;
                                pictureBox15.Show();
                                return;
                            }
                            x12 = x12 / 2;
                            x12 = decimal.Round(x12, 2);
                            txt_x2_s.Text = x12.ToString();
                            x12 = x12 * -1;
                            txt_x1_s.Text = x12.ToString();
                            x1Act();
                            x2Act();
                            txt_x_s.BackColor = Color.LightGreen;
                            txt_x1_s.BackColor = Color.LightGreen;
                            txt_x2_s.BackColor = Color.LightGreen;
                        }
                        xy_isTextChangedFromCode = false;
                    }

                }
                txt_y1_s.Focus();
            }
        }

        private void y1Act()
        {
            try
            {
                if (string.IsNullOrEmpty(txt_y1_s.Text) || string.IsNullOrWhiteSpace(txt_y1_s.Text))
                {
                    XY_isTextChangedFromCode = true;
                    txt_y_s.Clear();
                    XY_isTextChangedFromCode = false;
                    x2_set = "0";
                    pictureBox4.Hide();
                    pictureBox4.BackgroundImage = Resources.Request;
                    y1err = false;
                    return;
                }
                else if (!xy_isTextChangedFromCode)
                {
                    XY_isTextChangedFromCode = true;
                    decimal y1, y2;
                    bool isY1Valid = decimal.TryParse(txt_y1_s.Text, out y1);
                    bool isY2Valid = decimal.TryParse(txt_y2_s.Text, out y2);
                    if (isY1Valid && isY2Valid && (y2 + y1 == 0))
                    {
                        decimal y = y2 - y1;
                        y = decimal.Round(y, 2);
                        XY_isTextChangedFromCode = true;
                        txt_y_s.Text = y.ToString();
                        XY_isTextChangedFromCode = false;
                        isYSymmetric = true;
                    }
                    else
                    {
                        isYSymmetric = false;
                        XY_isTextChangedFromCode = false;
                    }
                }

                double a = double.Parse(txt_y1_s.Text);

                if (a < -20 || a > 0)
                {
                    x2_set = "0";
                    pictureBox4.BackgroundImage = Resources.Error;
                    y1err = true;
                    pictureBox4.Show();
                    return;
                }
                if (a > double.Parse(x1_dv) - 1)
                {
                    x2_set = "0";
                    pictureBox6.BackgroundImage = Resources.Error;
                    x1err = true;
                    pictureBox6.Show();
                    return;
                }
                if (x1_set != "0")
                    try
                    {
                        if (a > double.Parse(txt_y2_s.Text) - 1)
                        {
                            x2_set = "0";
                            pictureBox6.BackgroundImage = Resources.Error;
                            x1err = true;
                            pictureBox6.Show();
                            return;
                        }
                    }
                    catch
                    { return; }

                x2_set = Math.Abs((int)((-a - x2_offset) / x2_gain)).ToString();
                if (int.Parse(x2_set) > 65534 | int.Parse(y2_set) < 0)
                    x2_set = "0";
                pictureBox4.BackgroundImage = Resources.Request;
                y1err = false;
                if (Math.Abs(double.Parse(txt_y1_s.Text) - double.Parse(x2_dv)) > .1)
                {
                    isY1Set = true;
                    pictureBox4.Show();
                }
                else
                {
                    pictureBox4.Hide();
                    isY1Set = false;
                    pictureBox4.BackgroundImage = Resources.Request;
                }

                txt_y1_s.BackColor = Color.LightGreen;
                txt_y2_s.Focus();
            }
            catch
            {
                txt_y1_s.SelectAll();
                x2_set = "0";
                pictureBox4.BackgroundImage = Resources.Error;
                pictureBox4.Show();
                return;
            }
        }

        private void txt_y1_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                y1Act();
            }
        }

        private void y1Set()
        {
            if (isY1Set)
            {
                try
                {
                    if (Math.Abs(double.Parse(txt_y1_s.Text) - double.Parse(x2_dv)) < .1)
                    {
                        pictureBox4.Hide();
                        isY1Set = false;
                    }
                }
                catch { }
            }
        }

        private void y2Act()
        {
            try
            {
                if (string.IsNullOrEmpty(txt_y2_s.Text) || string.IsNullOrWhiteSpace(txt_y2_s.Text))
                {
                    XY_isTextChangedFromCode = true;
                    txt_y_s.Clear();
                    XY_isTextChangedFromCode = false;
                    x1_set = "0";
                    pictureBox3.Hide();
                    pictureBox3.BackgroundImage = Resources.Request;
                    y2err = false;
                    return;
                }
                else if (!xy_isTextChangedFromCode)
                {
                    XY_isTextChangedFromCode = true;
                    decimal y1, y2;
                    bool isY1Valid = decimal.TryParse(txt_y1_s.Text, out y1);
                    bool isY2Valid = decimal.TryParse(txt_y2_s.Text, out y2);
                    if (isY1Valid && isY2Valid && (y2 + y1 == 0))
                    {
                        decimal y = y2 - y1;
                        y = decimal.Round(y, 2);
                        XY_isTextChangedFromCode = true;
                        txt_y_s.Text = y.ToString();
                        XY_isTextChangedFromCode = false;
                        isYSymmetric = true;
                    }
                    else
                    {
                        isYSymmetric = false;
                        XY_isTextChangedFromCode = false;
                    }
                }

                double a = double.Parse(txt_y2_s.Text);

                if (a < 0 || a > 20)
                {
                    x1_set = "0";
                    pictureBox3.BackgroundImage = Resources.Error;
                    y2err = true;
                    pictureBox3.Show();
                    return;
                }
                double x2_dvo;
                double.TryParse(x2_dv, out x2_dvo);
                if (a < x2_dvo + 1)
                {
                    x1_set = "0";
                    pictureBox6.BackgroundImage = Resources.Error;
                    x1err = true;
                    pictureBox6.Show();
                    return;
                }
                if (x2_set != "0")
                    try
                    {
                        if (a < double.Parse(txt_y1_s.Text) + 1)
                        {
                            x1_set = "0";
                            pictureBox6.BackgroundImage = Resources.Error;
                            x1err = true;
                            pictureBox6.Show();
                            return;
                        }
                    }
                    catch
                    { return; }
                if (x1_dv != null)
                {
                    x1_set = Math.Abs((int)((a - x1_offset) / x1_gain)).ToString();
                    if (int.Parse(x1_set) > 65534 | int.Parse(y2_set) < 0)
                        x1_set = "0";
                    pictureBox3.BackgroundImage = Resources.Request;
                    y2err = false;

                    if (Math.Abs(double.Parse(txt_y2_s.Text) - double.Parse(x1_dv)) > .1)
                    {
                        isY2Set = true;
                        pictureBox3.Show();
                    }
                    else
                    {
                        pictureBox3.Hide();
                        isY2Set = false;
                        pictureBox3.BackgroundImage = Resources.Request;
                    }
                }
                txt_y2_s.BackColor = Color.LightGreen;
                txt_x1_s.Focus();
            }
            catch
            {
                x1_set = "0";
                txt_y2_s.SelectAll();
                pictureBox3.BackgroundImage = Resources.Error;
                pictureBox3.Show();
                return;
            }
        }
        private void txt_y2_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                y2Act();
            }
        }

        private void y2Set()
        {
            if (isY2Set)
            {
                try
                {
                    if (Math.Abs(double.Parse(txt_y2_s.Text) - double.Parse(x1_dv)) < .1)
                    {
                        pictureBox3.Hide();
                        isY2Set = false;
                    }
                }
                catch { }
            }

        }

        private void x1Act()
        {
            try
            {
                if (string.IsNullOrEmpty(txt_x1_s.Text) || string.IsNullOrWhiteSpace(txt_x1_s.Text))
                {
                    XY_isTextChangedFromCode = true;
                    txt_x_s.Clear();
                    XY_isTextChangedFromCode = false;
                    y2_set = "0";
                    pictureBox6.Hide();
                    pictureBox6.BackgroundImage = Resources.Request;
                    x1err = false;
                    return;
                }
                else if (!xy_isTextChangedFromCode)
                {
                    XY_isTextChangedFromCode = true;
                    decimal x1, x2;
                    bool isX1Valid = decimal.TryParse(txt_x1_s.Text, out x1);
                    bool isX2Valid = decimal.TryParse(txt_x2_s.Text, out x2);
                    if (isX1Valid && isX2Valid && (x2 + x1 == 0))
                    {
                        decimal x = x2 - x1;
                        x = decimal.Round(x, 2);
                        XY_isTextChangedFromCode = true;
                        txt_x_s.Text = x.ToString();
                        XY_isTextChangedFromCode = false;
                        isXSymmetric = true;
                    }
                    else
                    {
                        isXSymmetric = false;
                        XY_isTextChangedFromCode = false;
                    }
                }

                double a = double.Parse(txt_x1_s.Text);
                if (a < -20 || a > 12.5)
                {
                    y2_set = "0";
                    pictureBox6.BackgroundImage = Resources.Error;
                    x1err = true;
                    pictureBox6.Show();
                    return;
                }
                try
                {
                    if (a > double.Parse(y1_dv) - 1)
                    {
                        y2_set = "0";
                        pictureBox6.BackgroundImage = Resources.Error;
                        x1err = true;
                        pictureBox6.Show();
                        return;
                    }
                }
                catch
                {
                }
                if (y1_set != "0")
                    try
                    {
                        if (a > double.Parse(txt_x2_s.Text) - 1)
                        {
                            y2_set = "0";
                            pictureBox6.BackgroundImage = Resources.Error;
                            x1err = true;
                            pictureBox6.Show();
                            return;
                        }
                    }
                    catch
                    { return; }
                if (y2_dv != null)
                {
                    y2_set = Math.Abs((int)((-a - y2_offset) / y2_gain)).ToString();
                    if (int.Parse(y2_set) > 65534 | int.Parse(y2_set) < 0)
                        y2_set = "0";
                    pictureBox6.BackgroundImage = Resources.Request;
                    x1err = false;

                    if (Math.Abs(double.Parse(txt_x1_s.Text) - double.Parse(y2_dv)) > .1)
                    {
                        isX1Set = true;
                        pictureBox6.Show();
                    }
                    else
                    {
                        isX1Set = false;
                        pictureBox6.Hide();
                        pictureBox6.BackgroundImage = Resources.Request;
                    }
                }
                txt_x1_s.BackColor = Color.LightGreen;
                txt_x2_s.Focus();
            }
            catch
            {
                y2_set = "0";
                txt_x1_s.SelectAll();
                pictureBox6.BackgroundImage = Resources.Error;
                pictureBox6.Show();
                return;
            }
        }

        private void txt_x1_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                x1Act();
            }
        }

        private void x1Set()
        {
            if (isX1Set)
            {
                try
                {
                    if (Math.Abs(double.Parse(txt_x1_s.Text) - double.Parse(y2_dv)) < .1)
                    {
                        pictureBox6.Hide();
                        isX1Set = false;
                    }
                }
                catch { }
            }
        }

        private void x2Act()
        {
            try
            {
                if (string.IsNullOrEmpty(txt_x2_s.Text) || string.IsNullOrWhiteSpace(txt_x2_s.Text))
                {
                    XY_isTextChangedFromCode = true;
                    txt_x_s.Clear();
                    XY_isTextChangedFromCode = false;
                    y1_set = "0";
                    pictureBox5.Hide();
                    pictureBox5.BackgroundImage = Resources.Request;
                    x2err = false;
                    return;
                }
                else if (!xy_isTextChangedFromCode)
                {
                    XY_isTextChangedFromCode = true;
                    decimal x1, x2;
                    bool isX1Valid = decimal.TryParse(txt_x1_s.Text, out x1);
                    bool isX2Valid = decimal.TryParse(txt_x2_s.Text, out x2);
                    if (isX1Valid && isX2Valid && (x2 + x1 == 0))
                    {
                        decimal x = x2 - x1;
                        x = decimal.Round(x, 2);
                        XY_isTextChangedFromCode = true;
                        txt_x_s.Text = x.ToString();
                        XY_isTextChangedFromCode = false;
                        isXSymmetric = true;
                    }
                    else
                    {
                        isXSymmetric = false;
                        XY_isTextChangedFromCode = false;
                    }
                }

                double a = double.Parse(txt_x2_s.Text);

                if (a < -12.5 || a > 20)
                {
                    y1_set = "0";
                    pictureBox5.BackgroundImage = Resources.Error;
                    x2err = true;
                    pictureBox5.Show();
                    return;
                }
                double y2double;
                double.TryParse(y2_dv, out y2double);
                if (a < y2double + 1)
                {
                    y1_set = "0";
                    pictureBox6.BackgroundImage = Resources.Error;
                    pictureBox6.Show();
                    x1err = true;
                    return;
                }
                if (y2_set != "0")
                    try
                    {
                        if (a < double.Parse(txt_x1_s.Text) + 1)
                        {
                            y1_set = "0";
                            pictureBox6.BackgroundImage = Resources.Error;
                            x1err = true;
                            pictureBox6.Show();
                            return;
                        }
                    }
                    catch
                    { return; }
                if (y1_dv != null)
                {
                    y1_set = Math.Abs((int)((a - y1_offset) / y1_gain)).ToString();
                    if (int.Parse(y1_set) > 65534 | int.Parse(y2_set) < 0)
                        y1_set = "0";
                    pictureBox5.BackgroundImage = Resources.Request;
                    x2err = false;

                    if (Math.Abs(double.Parse(txt_x2_s.Text) - double.Parse(y1_dv)) > .1)
                    {
                        isX2Set = true;
                        pictureBox5.Show();
                    }
                    else
                    {
                        pictureBox5.Hide();
                        isX2Set = false;
                        pictureBox5.BackgroundImage = Resources.Request;
                    }
                }
                txt_x2_s.BackColor = Color.LightGreen;
                txt_gant_s.Focus();
            }
            catch
            {
                y1_set = "0";
                txt_x2_s.SelectAll();
                pictureBox5.BackgroundImage = Resources.Error;
                pictureBox5.Show();
                return;
            }
        }

        private void txt_x2_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                x2Act();
            }
        }

        private void x2Set()
        {
            if (isX2Set)
            {
                try
                {
                    if (Math.Abs(double.Parse(txt_x2_s.Text) - double.Parse(y1_dv)) < .1)
                    {
                        isX2Set = false;
                        pictureBox5.Hide();
                    }
                }
                catch { }
            }
        }

        private void txt_gant_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txt_gant_s.Text) || string.IsNullOrWhiteSpace(txt_gant_s.Text))
                {
                    gant_set = "0";
                    pictureBox1.Hide();
                    pictureBox1.BackgroundImage = Resources.Request;
                    return;
                }

                double aa;
                try
                {
                    aa = double.Parse(txt_gant_s.Text);
                    if (aa < 0 || aa >= 360)
                    {
                        gant_set = "0";
                        pictureBox1.BackgroundImage = Resources.Error;
                        pictureBox1.Show();
                        return;
                    }
                    double gentValueActual = double.Parse(gant_dv);
                    if (gentValueActual > 180 && aa == 180)
                    {
                        gant_isTextChangedFromCode = true;
                        txt_gant_s.Text = "180.05";
                        gant_isTextChangedFromCode = false;
                    }
                    //if (gentValueActual > 180 && a == 180)
                    //    a = 180.05;
                    if (aa > 180)
                        aa = aa - 360;
                    gant_set = ((int)((aa - gant_offset) / gant_gain)).ToString();
                    gant_t2 = double.Parse(txt_gant_s.Text);
                    gant_d2 = double.Parse(gant_dv);
                    if (gant_t2 > 180)
                        gant_t2 = gant_t2 - 360;
                    if (gant_d2 > 180)
                        gant_d2 = gant_d2 - 360;
                    if (Math.Abs(gant_t2 - gant_d2) > .4)
                    {
                        pictureBox1.BackgroundImage = Resources.Request;
                        isGantSet = true;
                        pictureBox1.Show();
                    }
                    else
                    {
                        pictureBox1.Hide();
                        isGantSet = false;
                        pictureBox1.BackgroundImage = Resources.Request;
                    }
                    txt_gant_s.BackColor = Color.LightGreen;
                    txt_coli_s.Focus();
                }
                catch
                {
                    txt_gant_s.SelectAll();
                    gant_set = "0";
                    pictureBox1.BackgroundImage = Resources.Error;
                    pictureBox1.Show();
                    return;
                }
            }
        }

        private void gantSet()
        {
            if (isGantSet)
            {
                gant_t2 = double.Parse(txt_gant_s.Text);
                gant_d2 = double.Parse(gant_dv);
                if (gant_t2 > 180)
                    gant_t2 = gant_t2 - 360;
                if (gant_d2 > 180)
                    gant_d2 = gant_d2 - 360;
                if (Math.Abs(gant_t2 - gant_d2) < .4)
                {
                    pictureBox1.Hide();
                    isGantSet = false;
                }
            }
        }

        private void txt_coli_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                isColiSet = true;
                if (string.IsNullOrEmpty(txt_coli_s.Text) || string.IsNullOrWhiteSpace(txt_coli_s.Text))
                {
                    collim_set = "0";
                    pictureBox2.Hide();
                    pictureBox2.BackgroundImage = Resources.Request;
                    return;
                }

                double a;
                try
                {
                    a = double.Parse(txt_coli_s.Text);
                    if (a < 0 || a >= 360)
                    {
                        collim_set = "0";
                        pictureBox2.BackgroundImage = Resources.Error;
                        pictureBox2.Show();
                        return;
                    }

                    if (a > 180)
                        a = a - 360;
                    collim_set = ((int)((a - collim_offset) / collim_gain)).ToString();
                    collim_t2 = double.Parse(txt_coli_s.Text);
                    collim_d2 = double.Parse(collim_dv);
                    if (collim_t2 > 180)
                        collim_t2 = collim_t2 - 360;
                    if (collim_d2 > 180)
                        collim_d2 = collim_d2 - 360;
                    if (Math.Abs(collim_t2 - collim_d2) > .4)
                    {
                        pictureBox2.BackgroundImage = Resources.Request;
                        isColiSet = true;
                        pictureBox2.Show();
                    }
                    else
                    {
                        pictureBox2.Hide();
                        isColiSet = false;
                        pictureBox2.BackgroundImage = Resources.Request;
                    }
                    txt_coli_s.BackColor = Color.LightGreen;
                    txt_y_s.Focus();
                }
                catch
                {
                    collim_set = "0";
                    pictureBox2.BackgroundImage = Resources.Error;
                    pictureBox2.Show();
                    return;
                }
                
            }
        }

        private void coliSet()
        {
            if (isColiSet)
            {
                collim_t2 = double.Parse(txt_coli_s.Text);
                collim_d2 = double.Parse(collim_dv);
                if (collim_t2 > 180)
                    collim_t2 = collim_t2 - 360;
                if (collim_d2 > 180)
                    collim_d2 = collim_d2 - 360;
                if (Math.Abs(collim_t2 - collim_d2) < .4)
                {
                    pictureBox2.Hide();
                    isColiSet = false;
                }
            }
        } 
    }
}
