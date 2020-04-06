using Compact_Control.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Compact_Control
{
    public partial class ClientControls : UserControl
    {
        private Form1 frm1;

        public bool sendParametersFlag = false;

        public ClientControls()
        {
            InitializeComponent();
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

        public bool compareParameters(string[] microParams, string[] ourParams)
        {
            bool equal = true;
            for (int i = 0; i < microParams.Length; i++)
            {
                if (microParams[i] != ourParams[i])
                {
                    equal = false;
                    break;
                }
            }
            return equal;
        }

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
            frm1.write(data);
        }


        private void btn_clearTerminal_oth_Click(object sender, EventArgs e)
        {
            tb_terminal_oth.Clear();
            lbl_oth_cnt.Text = "0";
        }

        private void ClientControls_Load(object sender, EventArgs e)
        {
            if (this.frm1 == null)
                this.frm1 = this.ParentForm as Form1;
        }

        public double ourSum = 0;

        private void ClientControls_VisibleChanged(object sender, EventArgs e)
        {
            if (Form1.showClinicalTerminals == "1")
            {
                gb_terminals.Show();
            }
            else
            {
                gb_terminals.Hide();
            }
        }

        public void writeToOtherTerminal(string text, bool isCatch)
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

        private bool x1err = false;
        private bool x2err = false;
        private bool y1err = false;
        private bool y2err = false;
        private bool xy_isTextChangedFromCode = false;
        private bool XY_isTextChangedFromCode = false;
        private bool gant_isTextChangedFromCode = false;
        bool isYSymmetric = false;
        bool isXSymmetric = false;
        private void txtBox_Enter(object sender, EventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void txt_gant_s_TextChanged(object sender, EventArgs e)
        {
            if (gant_isTextChangedFromCode == false)
            {
                //if (string.IsNullOrEmpty(txt_gant_s.Text) || string.IsNullOrWhiteSpace(txt_gant_s.Text))
                {
                    txt_gant_s.BackColor = Color.White;
                    //gant_set = "0";
                    frm1.gant_valid_raw = "0";
                    frm1.gant_valid_deg = "0";
                    pic_gant_status.Hide();
                }
            }
        }

        private void txt_coli_s_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_coli_s.Text) || string.IsNullOrWhiteSpace(txt_coli_s.Text))
            {
                txt_coli_s.BackColor = Color.White;
                //collim_set = "0";
                frm1.collim_valid_deg = "0";
                frm1.collim_valid_raw = "0";
                pic_coli_status.Hide();
            }
        }

        private void txt_y1_s_TextChanged(object sender, EventArgs e)
        {
            if (!xy_isTextChangedFromCode)
            {
                frm1.isY1Set = true;
                //isY1Set = false;
                y1err = false;
                txt_y1_s.BackColor = Color.White;
                txt_y_s.BackColor = Color.White;
                XY_isTextChangedFromCode = true;
                txt_y_s.Clear();
                XY_isTextChangedFromCode = false;
                pic_y1_status.Hide();
                //pictureBox14.Hide();
                //x2_set = "0";
                //frm1.x2_valid_deg = "0";
                frm1.x2_valid_raw = "0";
            }
        }

        private void txt_y2_s_TextChanged(object sender, EventArgs e)
        {
            if (!xy_isTextChangedFromCode)
            {
                frm1.isY2Set = true;
                //isY2Set = false;
                y2err = false;
                txt_y2_s.BackColor = Color.White;
                txt_y_s.BackColor = Color.White;
                XY_isTextChangedFromCode = true;
                txt_y_s.Clear();
                XY_isTextChangedFromCode = false;
                pic_y2_status.Hide();
                //pictureBox14.Hide();
                //x1_set = "0";
                frm1.x1_valid_deg = "0";
                frm1.x1_valid_raw = "0";
            }
        }

        private void txt_x1_s_TextChanged(object sender, EventArgs e)
        {
            if (!xy_isTextChangedFromCode)
            {
                frm1.isX1Set = true;
                //isX1Set = false;
                x1err = false;
                txt_x1_s.BackColor = Color.White;
                txt_x_s.BackColor = Color.White;
                XY_isTextChangedFromCode = true;
                txt_x_s.Clear();
                XY_isTextChangedFromCode = false;
                pic_x1_status.Hide();
                //pictureBox15.Hide();
                //y2_set = "0";
                frm1.y2_valid_deg = "0";
                frm1.y2_valid_raw = "0";
            }
        }

        private void txt_x2_s_TextChanged(object sender, EventArgs e)
        {
            if (!xy_isTextChangedFromCode)
            {
                frm1.isX2Set = true;
                //isX2Set = false;
                x2err = false;
                txt_x2_s.BackColor = Color.White;
                txt_x_s.BackColor = Color.White;
                XY_isTextChangedFromCode = true;
                txt_x_s.Clear();
                XY_isTextChangedFromCode = false;
                //pictureBox5.Hide();
                //pictureBox15.Hide();
                //y1_set = "0";
                frm1.y1_valid_deg = "0";
                frm1.y1_valid_raw = "0";
                pic_x2_status.Hide();
            }
        }

        private void gantAct()
        {
            if (string.IsNullOrEmpty(txt_gant_s.Text) || string.IsNullOrWhiteSpace(txt_gant_s.Text))
            {
                //gant_set = "0";
                frm1.gant_valid_deg = "0";
                frm1.gant_valid_raw = "0";
                pic_gant_status.Hide();
                pic_gant_status.BackgroundImage = Resources.Request;
                txt_coli_s.Focus();
                return;
            }

            double aa;
            try
            {
                aa = double.Parse(txt_gant_s.Text);
                if (aa < 0 || aa >= 360)
                {
                    //gant_set = "0";
                    frm1.gant_valid_deg = "0";
                    frm1.gant_valid_raw = "0";
                    pic_gant_status.BackgroundImage = frm1.errorImage;
                    pic_gant_status.Show();
                    txt_gant_s.BackColor = Color.White;
                    txt_gant_s.SelectAll();
                    return;
                }
                double gantValueActual = double.Parse(frm1.gant_dv);
                if (gantValueActual > 180 && aa == 180)
                {
                    gant_isTextChangedFromCode = true;
                    txt_gant_s.Text = "180.05";
                    aa = 180.05;
                    gant_isTextChangedFromCode = false;
                }
                //if (gentValueActual > 180 && a == 180)
                //    a = 180.05;
                frm1.gant_valid_deg = aa.ToString();
                if (aa > 180)
                    aa = aa - 360;
                //gant_set = ((int)((aa - gant_offset) / gant_gain)).ToString();
                frm1.gant_valid_raw = ((int)((aa - frm1.gant_offset) / frm1.gant_gain)).ToString();
                frm1.gant_t2 = double.Parse(txt_gant_s.Text);
                frm1.gant_d2 = double.Parse(frm1.gant_dv);
                if (frm1.gant_t2 > 180)
                    frm1.gant_t2 = frm1.gant_t2 - 360;
                if (frm1.gant_d2 > 180)
                    frm1.gant_d2 = frm1.gant_d2 - 360;
                if (Math.Abs(frm1.gant_t2 - frm1.gant_d2) > .11)
                {
                    pic_gant_status.BackgroundImage = Resources.Request;
                    pic_gant_status.Show();
                }
                else
                {
                    pic_gant_status.Hide();
                }
                txt_gant_s.BackColor = Color.LightGreen;
                txt_coli_s.Focus();
            }
            catch
            {
                txt_gant_s.SelectAll();
                //gant_set = "0";
                frm1.gant_valid_deg = "0";
                frm1.gant_valid_raw = "0";
                pic_gant_status.BackgroundImage = frm1.errorImage;
                pic_gant_status.Show();
                txt_gant_s.BackColor = Color.White;
                return;
            }
        }

        private void txt_gant_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gantAct();
            }
        }

        private void coliAct()
        {
            if (string.IsNullOrEmpty(txt_coli_s.Text) || string.IsNullOrWhiteSpace(txt_coli_s.Text))
            {
                //collim_set = "0";
                frm1.collim_valid_deg = "0";
                frm1.collim_valid_raw = "0";
                pic_coli_status.Hide();
                pic_coli_status.BackgroundImage = Resources.Request;
                txt_y_s.Focus();
                return;
            }

            double a;
            try
            {
                a = double.Parse(txt_coli_s.Text);
                if (a < 0 || a >= 360)
                {
                    //collim_set = "0";
                    frm1.collim_valid_deg = "0";
                    frm1.collim_valid_raw = "0";
                    pic_coli_status.BackgroundImage = frm1.errorImage;
                    pic_coli_status.Show();
                    txt_coli_s.BackColor = Color.White;
                    txt_coli_s.SelectAll();
                    return;
                }

                frm1.collim_valid_deg = a.ToString();
                if (a > 180)
                    a = a - 360;
                //collim_set = ((int)((a - collim_offset) / collim_gain)).ToString();
                frm1.collim_valid_raw = ((int)((a - frm1.collim_offset) / frm1.collim_gain)).ToString();
                frm1.collim_t2 = double.Parse(txt_coli_s.Text);
                frm1.collim_d2 = double.Parse(frm1.collim_dv);
                if (frm1.collim_t2 > 180)
                    frm1.collim_t2 = frm1.collim_t2 - 360;
                if (frm1.collim_d2 > 180)
                    frm1.collim_d2 = frm1.collim_d2 - 360;
                if (Math.Abs(frm1.collim_t2 - frm1.collim_d2) > .11)
                {
                    pic_coli_status.BackgroundImage = Resources.Request;
                    pic_coli_status.Show();
                }
                else
                {
                    pic_coli_status.Hide();
                }
                txt_coli_s.BackColor = Color.LightGreen;
                txt_y_s.Focus();
            }
            catch
            {
                //collim_set = "0";
                frm1.collim_valid_deg = "0";
                frm1.collim_valid_raw = "0";
                pic_coli_status.BackgroundImage = frm1.errorImage;
                pic_coli_status.Show();
                txt_coli_s.BackColor = Color.White;
                txt_coli_s.SelectAll();
                return;
            }
        }

        private void txt_coli_s_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                coliAct();
            }
        }

        private void txt_x_s_TextChanged(object sender, EventArgs e)
        {
            if (!XY_isTextChangedFromCode)
            {
                frm1.isX1Set = true;
                frm1.isX2Set = true;
                //if (string.IsNullOrEmpty(txt_x_s.Text) || string.IsNullOrWhiteSpace(txt_x_s.Text))
                {
                    //isX1Set = false;
                    //isX2Set = false;
                    txt_x_s.BackColor = Color.White;
                    txt_x1_s.BackColor = Color.White;
                    txt_x2_s.BackColor = Color.White;
                    XY_isTextChangedFromCode = false;
                    //pictureBox15.Hide();
                    pic_x1_status.Hide();
                    pic_x2_status.Hide();
                    //y2_set = "0";
                    //y1_set = "0";
                    frm1.y1_valid_deg = "0";
                    frm1.y2_valid_deg = "0";
                    frm1.y1_valid_raw = "0";
                    frm1.y2_valid_raw = "0";
                }
            }
        }

        private void txt_y_s_TextChanged(object sender, EventArgs e)
        {

            if (!XY_isTextChangedFromCode)
            {
                frm1.isY1Set = true;
                frm1.isY2Set = true;
                //if (string.IsNullOrEmpty(txt_y_s.Text) || string.IsNullOrWhiteSpace(txt_y_s.Text))
                {
                    //isY1Set = false;
                    //isY2Set = false;
                    txt_y_s.BackColor = Color.White;
                    txt_y1_s.BackColor = Color.White;
                    txt_y2_s.BackColor = Color.White;
                    XY_isTextChangedFromCode = false;
                    //pictureBox14.Hide();
                    pic_y1_status.Hide();
                    pic_y2_status.Hide();
                    //x2_set = "0";
                    //x1_set = "0";
                    frm1.x2_valid_deg = "0";
                    frm1.x1_valid_deg = "0";
                    frm1.x2_valid_raw = "0";
                    frm1.x1_valid_raw = "0";
                }
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
                                if (x12 < 1)
                                {
                                    txt_x_s.SelectAll();
                                    //pictureBox14.BackgroundImage = Resources.Error;
                                    //pictureBox14.Show();
                                    txt_x1_s.Clear();
                                    txt_x2_s.Clear();
                                    pic_x1_status.Hide();
                                    pic_x2_status.Hide();
                                    return;
                                }
                            }
                            catch
                            {
                                txt_x_s.SelectAll();
                                //pictureBox15.BackgroundImage = Resources.Error;
                                //pictureBox15.Show();
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
                                if (y12 < 1)
                                {
                                    txt_y_s.SelectAll();
                                    //pictureBox14.BackgroundImage = Resources.Error;
                                    //pictureBox14.Show();
                                    txt_y1_s.Clear();
                                    txt_y2_s.Clear();
                                    pic_y1_status.Hide();
                                    pic_y2_status.Hide();
                                    return;
                                }
                            }
                            catch
                            {
                                txt_y_s.SelectAll();
                                //pictureBox14.BackgroundImage = Resources.Error;
                                //pictureBox14.Show();
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

        private void y1Act()
        {
            frm1.isY1Set = true;
            try
            {
                if (string.IsNullOrEmpty(txt_y1_s.Text) || string.IsNullOrWhiteSpace(txt_y1_s.Text))
                {
                    XY_isTextChangedFromCode = true;
                    txt_y_s.Clear();
                    XY_isTextChangedFromCode = false;
                    frm1.x2_valid_raw = "0";
                    pic_y1_status.Hide();
                    y1err = false;
                    txt_y2_s.Focus();
                    return;
                }
                
                double a = double.Parse(txt_y1_s.Text);
                double b = 0;

                if (a < -20 || a > 0)
                {

                    frm1.x2_valid_raw = "0";
                    pic_y1_status.BackgroundImage = frm1.errorImage;
                    pic_y1_status.Show();
                    txt_y1_s.BackColor = Color.White;
                    txt_y1_s.SelectAll();
                    y1err = false;
                    return;
                }

                else
                {                    
                    if (frm1.x1_valid_raw == "0")
                        b = double.Parse(frm1.x1_dv);
                    else
                        b = double.Parse(frm1.x1_valid_deg);

                    if (a > b - 1)
                    {
                        frm1.x2_valid_raw = "0";
                        pic_y1_status.BackgroundImage = frm1.errorImage;
                        pic_y1_status.Show();
                        txt_y1_s.BackColor = Color.White;
                        txt_y2_s.Focus();
                        //txt_y1_s.SelectAll();
                        y1err = true;
                        return;
                    }
                    else
                        pic_y1_status.Hide();
                }

                if (!xy_isTextChangedFromCode)
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

                frm1.x2_valid_raw = Math.Abs((int)((-a - frm1.x2_offset) / frm1.x2_gain)).ToString();
                if (int.Parse(frm1.x2_valid_raw) > 65534 | int.Parse(frm1.x2_valid_raw) < 0)
                {
                    frm1.x2_valid_raw = "0";
                }
                else
                {
                    frm1.x2_valid_deg = a.ToString();
                }
                y1err = false;
                if (Math.Abs(double.Parse(txt_y1_s.Text) - double.Parse(frm1.x2_dv)) >= .09)
                {
                    pic_y1_status.BackgroundImage = Resources.Request;
                    pic_y1_status.Show();
                }
                else
                {
                    pic_y1_status.Hide();
                }

                txt_y1_s.BackColor = Color.LightGreen;
                if (y2err)
                    y2Act();
                txt_y2_s.Focus();
            }
            catch
            {
                frm1.x2_valid_raw = "0";
                txt_y1_s.SelectAll();
                pic_y1_status.BackgroundImage = frm1.errorImage;
                pic_y1_status.Show();
                txt_y1_s.BackColor = Color.White;
                y1err = false;
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

        private void y2Act()
        {
            try
            {
                frm1.isY2Set = true;
                if (string.IsNullOrEmpty(txt_y2_s.Text) || string.IsNullOrWhiteSpace(txt_y2_s.Text))
                {
                    XY_isTextChangedFromCode = true;
                    txt_y_s.Clear();
                    XY_isTextChangedFromCode = false;
                    frm1.x1_valid_raw = "0";
                    pic_y2_status.Hide();
                    pic_y2_status.BackgroundImage = Resources.Request;
                    txt_x1_s.Focus();
                    y2err = false;
                    return;
                }

                double a = double.Parse(txt_y2_s.Text);
                double b = 0;
                if (a < 0 || a > 20)
                {
                    frm1.x1_valid_raw = "0";
                    pic_y2_status.BackgroundImage = frm1.errorImage;
                    pic_y2_status.Show();
                    txt_y2_s.BackColor = Color.White;
                    txt_y2_s.SelectAll();
                    y2err = false;
                    return;
                }
                else
                {
                    if (frm1.x2_valid_raw == "0")
                        b = double.Parse(frm1.x2_dv);
                    else
                        b = double.Parse(frm1.x2_valid_deg);

                    if (a < b + 1)
                    {
                        frm1.x1_valid_raw = "0";
                        pic_y2_status.BackgroundImage = frm1.errorImage;
                        pic_y2_status.Show();
                        txt_y2_s.BackColor = Color.White;
                        txt_x1_s.Focus();
                        //txt_y2_s.SelectAll();
                        y2err = true;
                        return;
                    }

                    else
                        pic_y2_status.Hide();
                }

                if (!xy_isTextChangedFromCode)
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

                if (frm1.x1_dv != null)
                {
                    frm1.x1_valid_raw = Math.Abs((int)((a - frm1.x1_offset) / frm1.x1_gain)).ToString();
                    if (int.Parse(frm1.x1_valid_raw) > 65534 | int.Parse(frm1.x1_valid_raw) < 0)
                    {
                        frm1.x1_valid_raw = "0";
                    }
                    else
                    {
                        frm1.x1_valid_deg = a.ToString();
                        //x1_valid_raw = x1_set;
                    }
                    y2err = false;

                    if (Math.Abs(double.Parse(txt_y2_s.Text) - double.Parse(frm1.x1_dv)) >= .09)
                    {
                        pic_y2_status.BackgroundImage = Resources.Request;
                        pic_y2_status.Show();
                    }
                    else
                    {
                        pic_y2_status.Hide();
                    }
                }
                txt_y2_s.BackColor = Color.LightGreen;
                if (y1err)
                    y1Act();
                txt_x1_s.Focus();
            }
            catch
            {
                frm1.x1_valid_deg = frm1.x1_dv;
                frm1.x1_valid_raw = "0";
                txt_y2_s.SelectAll();
                pic_y2_status.BackgroundImage = frm1.errorImage;
                pic_y2_status.Show();
                txt_y2_s.BackColor = Color.White;
                y2err = false;
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

        private void x1Act()
        {
            frm1.isX1Set = true;
            try
            {
                if (string.IsNullOrEmpty(txt_x1_s.Text) || string.IsNullOrWhiteSpace(txt_x1_s.Text))
                {
                    XY_isTextChangedFromCode = true;
                    txt_x_s.Clear();
                    XY_isTextChangedFromCode = false;
                    frm1.y2_valid_raw = "0";
                    pic_x1_status.Hide();
                    txt_x2_s.Focus();
                    x1err = false;
                    return;
                }

                double a = double.Parse(txt_x1_s.Text);
                double b = 0;

                if (a < -20 || a > 12.5)
                {
                    frm1.y2_valid_raw = "0";
                    pic_x1_status.BackgroundImage = frm1.errorImage;
                    pic_x1_status.Show();
                    txt_x1_s.BackColor = Color.White;
                    txt_x1_s.SelectAll();
                    x1err = false;
                    return;
                }
                else
                {
                    if (frm1.y1_valid_raw == "0")
                        b = double.Parse(frm1.y1_dv);
                    else
                        b = double.Parse(frm1.y1_valid_deg);

                    if (a > b - 1)
                    {
                        frm1.y2_valid_raw = "0";
                        pic_x1_status.BackgroundImage = frm1.errorImage;
                        pic_x1_status.Show();
                        txt_x1_s.BackColor = Color.White;
                        txt_x2_s.Focus();
                        //txt_x1_s.SelectAll();
                        x1err = true;
                        return;
                    }
                    else
                        pic_x1_status.Hide();
                }

                if (!xy_isTextChangedFromCode)
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

                frm1.y2_valid_raw = Math.Abs((int)((-a - frm1.y2_offset) / frm1.y2_gain)).ToString();
                if (int.Parse(frm1.y2_valid_raw) > 65534 | int.Parse(frm1.y2_valid_raw) < 0)
                {
                    //y2_set = "0";
                    frm1.y2_valid_deg = frm1.y2_dv;
                    frm1.y2_valid_raw = "0";
                }
                else
                {
                    frm1.y2_valid_deg = a.ToString();
                }

                if (Math.Abs(double.Parse(txt_x1_s.Text) - double.Parse(frm1.y2_dv)) >= .09)
                {
                    pic_x1_status.BackgroundImage = Resources.Request;
                    if (x2err)
                        x2Act();
                    pic_x1_status.Show();
                }
                else
                {
                    pic_x1_status.Hide();
                }

                txt_x1_s.BackColor = Color.LightGreen;
                txt_x2_s.Focus();
            }
            catch
            {
                frm1.y2_valid_raw = "0";
                txt_x1_s.SelectAll();
                pic_x1_status.BackgroundImage = frm1.errorImage;
                pic_x1_status.Show();
                txt_x1_s.BackColor = Color.White;
                x1err = false;
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

        private void x2Act()
        {
            frm1.isX2Set = true;
            try
            {
                if (string.IsNullOrEmpty(txt_x2_s.Text) || string.IsNullOrWhiteSpace(txt_x2_s.Text))
                {
                    XY_isTextChangedFromCode = true;
                    txt_x_s.Clear();
                    XY_isTextChangedFromCode = false;
                    frm1.y1_valid_raw = "0";
                    pic_x2_status.Hide();
                    pic_x2_status.BackgroundImage = Resources.Request;
                    txt_gant_s.Focus();
                    x2err = false;
                    return;
                }

                double a = double.Parse(txt_x2_s.Text);
                double b = 0;

                if (a < -12.5 || a > 20)
                {
                    frm1.y1_valid_raw = "0";
                    pic_x2_status.BackgroundImage = frm1.errorImage;
                    pic_x2_status.Show();
                    txt_x2_s.BackColor = Color.White;
                    txt_x2_s.SelectAll();
                    x2err = false;
                    return;
                }
                else
                {
                    if (frm1.y2_valid_raw == "0")
                        b = double.Parse(frm1.y2_dv);
                    else
                        b = double.Parse(frm1.y2_valid_deg);

                    if (a < b + 1)
                    {
                        frm1.y1_valid_raw = "0";
                        pic_x2_status.BackgroundImage = frm1.errorImage;
                        pic_x2_status.Show();
                        txt_x2_s.BackColor = Color.White;
                        txt_gant_s.Focus();
                        //txt_x2_s.SelectAll();
                        x2err = true;
                        return;
                    }

                    else
                        pic_x2_status.Hide();
                }            

                if (!xy_isTextChangedFromCode)
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

                if (frm1.y1_dv != null)
                {
                    frm1.y1_valid_raw = Math.Abs((int)((a - frm1.y1_offset) / frm1.y1_gain)).ToString();
                    if (int.Parse(frm1.y1_valid_raw) > 65534 | int.Parse(frm1.y1_valid_raw) < 0)
                    {
                        frm1.y1_valid_raw = "0";
                    }
                    else
                    {
                        frm1.y1_valid_deg = a.ToString();
                    }

                    if (Math.Abs(double.Parse(txt_x2_s.Text) - double.Parse(frm1.y1_dv)) >= .09)
                    {
                        pic_x2_status.BackgroundImage = Resources.Request;
                        pic_x2_status.Show();
                    }
                    else
                    {
                        pic_x2_status.Hide();
                    }
                }
                txt_x2_s.BackColor = Color.LightGreen;
                if (x1err)
                    x1Act();
                txt_gant_s.Focus();
            }
            catch
            {
                frm1.y1_valid_raw = "0";
                txt_x2_s.SelectAll();
                pic_x2_status.BackgroundImage = frm1.errorImage;
                pic_x2_status.Show();
                txt_x2_s.BackColor = Color.White;
                x2err = false;
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

        private void btn_getReceiveQ_Click(object sender, EventArgs e)
        {
            string tmp = "-Empty-";
            int qCount = 0;
            try
            {
                qCount = frm1.receiveQ.Count;
                tmp = frm1.receiveQ.Dequeue();
            }
            catch
            {
                qCount = 0;
                tmp = "-Empty-";
            }
            writeToOtherTerminal("Q(" + qCount.ToString() + ")> " + tmp, false);
        }

        private void btn_getSerialPort_Click(object sender, EventArgs e)
        {
            string tmp = "-Empty-";
            int serialBytes = 0;
            try
            {
                serialBytes = frm1.serialPort1.BytesToRead;
                tmp = frm1.serialPort1.ReadExisting();
            }
            catch
            {
                serialBytes = 0;
                tmp = "-Empty-";
            }
            writeToOtherTerminal("S(" + serialBytes.ToString() + ")> " + tmp, false);
        }

    }
}
