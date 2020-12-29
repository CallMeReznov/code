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
using ConfigCSharpDemo;
using System.Runtime.InteropServices;

namespace temperature
{

    public partial class Form1 : Form
    {
        ThreadStart Thget;
        Thread Thloop;
        dynamic api;
        Response response;
        dynamic param;
        dynamic ns;
        public Int32 m_lUserID = -1;
        private CHCNetSDK.NET_DVR_SHOWSTRING_V30 DeviceOsd;
        string temp;
        CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();
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
            textBox4.Text = appSettings["WebCamip"] ?? "Not Found";
            textBox5.Text = appSettings["WebCamport"] ?? "Not Found";

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
                checkBox2.Enabled = false;
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
                checkBox2.Enabled = true;
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
            var appSettings = ConfigurationManager.AppSettings;
            string WebCamip = appSettings["WebCamip"] ?? "Not Found";
            string WebCamport = appSettings["WebCamport"] ?? "Not Found";
            string WebCamUser = appSettings["WebCamUser"] ?? "Not Found";
            string WebCamPasswd = appSettings["WebCamPasswd"] ?? "Not Found";
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
                        temp = response.Result[0].value;
                        if (ns != response.Result[0].clock)
                        {
                            textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  当前温度:" + response.Result[0].value + "\n\r");
                            serialPort1.WriteLine(temp);
                            ns = response.Result[0].clock;
                            if (checkBox2.Checked == true)
                            {

                                CHCNetSDK.NET_DVR_Init();
                                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(textBox4.Text, UInt16.Parse(textBox5.Text), WebCamUser, WebCamPasswd, ref DeviceInfo);
                                //textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  登录错误,错误代码:"+ CHCNetSDK.NET_DVR_GetLastError() + "\n\r");
                                UInt32 dwReturn = 0;
                                Int32 nSize = Marshal.SizeOf(DeviceOsd);
                                IntPtr ptrShowStrCfg = Marshal.AllocHGlobal(nSize);
                                Marshal.StructureToPtr(DeviceOsd, ptrShowStrCfg, false);
                                if (!CHCNetSDK.NET_DVR_GetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_GET_SHOWSTRING_V30, 1, ptrShowStrCfg, (UInt32)nSize, ref dwReturn))
                                {
                                    textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  获取失败,错误代码:" + CHCNetSDK.NET_DVR_GetLastError() + "\n\r");
                                }
                                DeviceOsd = (CHCNetSDK.NET_DVR_SHOWSTRING_V30)Marshal.PtrToStructure(ptrShowStrCfg, typeof(CHCNetSDK.NET_DVR_SHOWSTRING_V30));
                                temp = DateTime.Now.ToLongTimeString().ToString() + "  " + temp + "℃";
                                DeviceOsd.struStringInfo[0].wShowString = 1;
                                DeviceOsd.struStringInfo[0].sString = temp;
                                DeviceOsd.struStringInfo[0].wStringSize = (ushort)temp.Length;
                                DeviceOsd.struStringInfo[0].wShowStringTopLeftX = UInt16.Parse("50");
                                DeviceOsd.struStringInfo[0].wShowStringTopLeftY = UInt16.Parse("650");
                                Marshal.StructureToPtr(DeviceOsd, ptrShowStrCfg, false);
                                if (!CHCNetSDK.NET_DVR_SetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_SET_SHOWSTRING_V30, 1, ptrShowStrCfg, (UInt32)nSize))
                                {
                                    textBox1.AppendText("\n\r" + DateTime.Now.ToString() + "  设置失败,错误代码:" + CHCNetSDK.NET_DVR_GetLastError() + "\n\r");
                                }

                                Marshal.FreeHGlobal(ptrShowStrCfg);
                                CHCNetSDK.NET_DVR_Logout(m_lUserID);
                                CHCNetSDK.NET_DVR_Cleanup();
                            }
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
