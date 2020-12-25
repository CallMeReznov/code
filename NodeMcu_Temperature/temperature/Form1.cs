using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ysq.Zabbix;
using System.Threading;
using System.IO.Ports;
using System.Configuration;
using System.Diagnostics;

namespace temperature
{

    public partial class Form1 : Form
    {
        ThreadStart Thget;
        Thread Thloop;
        dynamic api;
        Response response;
        dynamic param;
        dynamic ns ;
        public Form1()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach ( string ports in SerialPort.GetPortNames() )
            {
                comboBox1.Items.Add(ports);
            }
            var appSettings = ConfigurationManager.AppSettings;
            textBox2.Text = appSettings["Temp"] ?? "Not Found";
            textBox3.Text = appSettings["Phonenum"] ?? "Not Found";
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "Start")
            {
                Zlogin();
                ZLoad();
                Thget = new ThreadStart(Zget);
                Thloop = new Thread(Thget);
                Thloop.Start();
                button1.Text = "Stop";
                textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  开始\n\r");
                this.Text = comboBox1.Text;
                this.notifyIcon1.Text = "Working";
                comboBox1.Enabled = false;
                checkBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
            else
            {
                Thloop.Abort();
                serialPort1.Close();
                button1.Text = "Start";
                textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  结束\n\r");
                this.notifyIcon1.Text = "Idle";
                comboBox1.Enabled = true;
                checkBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
            }
        }


        public void Zlogin()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string Url = appSettings["Url"] ?? "Not Found";
                string User = appSettings["User"] ?? "Not Found";
                string Passwd = appSettings["Passwd"] ?? "Not Found";
                api = new ApiClient(Url, User, Passwd);
                api.Login();
            }
            catch
            {
                textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  配置文件错误\n\r");
            }
        }


        public void ZLoad()
        {
            param = new Dictionary<string, string>();
            param.Add("itemids", "51205");
            param.Add("history", "0");
            param.Add("sortfield", "clock");
            param.Add("sortorder", "DESC");
            param.Add("limit", "1");

        }

        public void Zget()
        {
            Process Rp = new Process();
            Rp.StartInfo.FileName = @"adb";
            Rp.StartInfo.Arguments = @"shell am start -a android.intent.action.CALL -d tel:" + textBox3.Text;
            Rp.StartInfo.UseShellExecute = false;
            Rp.StartInfo.CreateNoWindow = true;
            
            serialPort1.PortName = comboBox1.Text;
            try
            {
                serialPort1.Open();
            }
            catch
            {
                textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  串口通信异常\n\r");
            }
            if (serialPort1.IsOpen)
            {
                while (true)
                {
                    #region GetZabbixApi
                    //TODO:判断API获取的时间是否有变动,如有变动才更新
                    #endregion
                    try
                    {
                        response = api.Call("history.get", param);
                        string aaaa = response.Result[0].value;

                        
                        if (ns != response.Result[0].clock)
                        {
                            textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  当前温度:" + response.Result[0].value + "\n\r");
                            serialPort1.WriteLine(aaaa);
                            ns = response.Result[0].clock;
                            if (checkBox1.Checked == true)
                            {
                                if (Convert.ToDouble(response.Result[0].value) > Convert.ToDouble(textBox2.Text))
                                {
                                    textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  设备传感器温度高于阈值,发送警报\n\r");
                                    Rp.Start();
                                }
                            }
                        }
                        else
                        {
                            textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  Pass\n\r");
                        }
                    }
                    catch
                    {
                        textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  接口数据获取异常\n\r");

                    }
                    Thread.Sleep(60000);
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  当前选中端口:" + comboBox1.Text+"\n\r");

        }
        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  串口数据发送异常\n\r");
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                textBox1.ScrollToCaret();
            }
            else
            {
                WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                textBox1.ScrollToCaret();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process RpTest = new Process();
            RpTest.StartInfo.FileName = @"adb";
            RpTest.StartInfo.Arguments = @"shell am start -a android.intent.action.CALL -d tel:" + textBox3.Text;
            RpTest.StartInfo.UseShellExecute = false;
            RpTest.StartInfo.CreateNoWindow = true;
            RpTest.Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  开启电话报警\n\r");
            }
            else
            {
                textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  关闭电话报警\n\r");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }


}

