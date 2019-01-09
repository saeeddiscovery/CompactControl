using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compact_Control
{
    public partial class BarcodeScan : UserControl
    {
        public BarcodeScan()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }

        private void BarcodeScan_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
                textBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Tab) || e.KeyChar == Convert.ToChar(Keys.Escape))
                e.Handled = true;
            else if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                Class_PatientData.searchPhrase = textBox1.Text;
                Class_PatientData.barcodeReaderUsed = true;
                this.SendToBack();
                //this.Hide();
                this.Dispose();
            }
        }

        private void BarcodeScan_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void BarcodeScan_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void BarcodeScan_Enter(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void BarcodeScan_DragEnter(object sender, DragEventArgs e)
        {
            textBox1.Focus();
        }

        private void BarcodeScan_DragLeave(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
