using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Sunny.UI;
using System.Diagnostics;

namespace PowerStatu
{
    public partial class Form1 : UIForm
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                uiSymbolButton1.Text = "Stop";
                timer1.Enabled = true;
                uiRichTextBox1.AppendText(DateTime.Now.ToString() + "  开始检测\r");

            }
            else if (timer1.Enabled == true)
            {
                uiSymbolButton1.Text = "Start";
                timer1.Enabled = false;
                uiRichTextBox1.AppendText(DateTime.Now.ToString() + "  停止检测\r");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
            {
                uiRichTextBox1.AppendText(DateTime.Now.ToString() + "  检测正常,Pass\r");
            }
            else if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
            {
                uiRichTextBox1.AppendText(DateTime.Now.ToString() + "  检测断电!报警!\r");
                Process RpTest = new Process();
                RpTest.StartInfo.FileName = @"adb";
                RpTest.StartInfo.Arguments = @"shell am start -a android.intent.action.CALL -d tel:" + uiTextBox1.Text;
                RpTest.StartInfo.UseShellExecute = false;
                RpTest.StartInfo.CreateNoWindow = true;
                RpTest.Start();
                timer1.Interval = 300000;
                uiRichTextBox1.AppendText(DateTime.Now.ToString() + "  五分钟后重新报警\r");
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            uiRichTextBox1.ScrollToCaret();
        }

        private void uiRichTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void uiRichTextBox1_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
