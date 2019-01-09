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
    public partial class Form_TrialReport : Form
    {
        public Form_TrialReport()
        {
            InitializeComponent();
        }

        private void Form_TrialReport_Load(object sender, EventArgs e)
        {
            //    string timeElapsed = HashPass.ReadElapsedFromReg();
            //    int hoursElapsed = int.Parse(timeElapsed) / (60*60);
            //    int remainingHours = 240 - hoursElapsed;
            //    progressBar1.Value = remainingHours;
            //    label1.Text = remainingHours.ToString() + " Hours remained";

            string days_str = HashPass.ReadDaysFromReg();
            double days = 0;
            if (days_str != "")
            {
                days = double.Parse(days_str);
            }
            days = 30-days;
            int firstUsed = int.Parse(HashPass.ReadFirstDateFromReg());
            int lastUsed = int.Parse(HashPass.ReadLastDateFromReg());
            int elapsedDaysFromFULU = 30 - (lastUsed - firstUsed);
            string timeElapsed = HashPass.ReadElapsedFromReg();
            int hoursElapsed = int.Parse(timeElapsed) / (60 * 60);
            int remainingHours = 720 - hoursElapsed;
            int progressValue = 0;
            if (elapsedDaysFromFULU > 30 || remainingHours <= 0)
                progressValue = 0;
            else
                progressValue = (days < elapsedDaysFromFULU) ? int.Parse(days.ToString()) : elapsedDaysFromFULU;
            progressBar1.Value = progressValue;
            label1.Text = progressValue.ToString() + " Days remained";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
