using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace face_recognition
{
    public partial class Form1 : Form
    {
        System.Drawing.Rectangle Rect;
        System.Drawing.Point LocationXY;
        System.Drawing.Point LocationX1Y1;

        bool IsMouseDown = false;
        private int Pid;
        private string FaceCorePath;
        private string FaceCoreDebug = " 0 ";
        private string FaceCoreReact = " 0 ";
        private string FaceCoreLocal = " 0 ";

        public Form1()
        {
            InitializeComponent();
        }

        private void Sel_Pic_Btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog Open_Pic_File = new OpenFileDialog();
            Open_Pic_File.Filter = "Image Files(*.bmp,*.gif,*.jpg)|*.bmp;*.gif;*.jpg;";
            if (Open_Pic_File.ShowDialog() == DialogResult.OK)
                Sel_Pic_box.Image = System.Drawing.Image.FromFile(Open_Pic_File.FileName, true);
            Pic_Path_Text.Text = Open_Pic_File.FileName;
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            //Face_Pic_Count.Text = System.IO.Directory.GetFiles(Environment.CurrentDirectory + @"\FaceFile\").Length.ToString();
            //Face_Db_Count.Text = System.IO.Directory.GetFiles(Environment.CurrentDirectory + @"\FaceData").Length.ToString();




            //string Db_file = @"test.db";
            //if (File.Exists(Db_file))
            //{
            //    Face_Pic_Count.Text = "Ok";
            //}
            //else
            //{
            //    Face_Pic_Count.Text = "Error";
            //}
        }

        private void Sel_Pic_box_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            LocationXY = e.Location;
        }

        private void Sel_Pic_box_MouseHover(object sender, EventArgs e)
        {

        }

        private void Sel_Pic_box_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {
                LocationX1Y1 = e.Location;
                Refresh();
            }
        }

        private void Sel_Pic_box_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {

                LocationX1Y1 = e.Location;
                IsMouseDown = false;
                if (Rect != null)
                {
                    Bitmap bit = new Bitmap(Sel_Pic_box.Image, Sel_Pic_box.Width, Sel_Pic_box.Height);
                    Bitmap cropImg = new Bitmap(Rect.Width, Rect.Height);
                    Graphics g = Graphics.FromImage(cropImg);
                    g.DrawImage(bit, 0, 0, Rect, GraphicsUnit.Pixel);
                    Cut_Pic_Box.Image = cropImg;
                }
            }
        }

        private void Sel_Pic_box_Paint(object sender, PaintEventArgs e)
        {
            if (Rect != null)
                e.Graphics.DrawRectangle(Pens.Red, GetRect());
        }

        private System.Drawing.Rectangle GetRect()
        {
            Rect = new System.Drawing.Rectangle();
            Rect.X = Math.Min(LocationXY.X, LocationX1Y1.X);
            Rect.Y = Math.Min(LocationXY.Y, LocationX1Y1.Y);
            Rect.Width = Math.Abs(LocationXY.X - LocationX1Y1.X);
            Rect.Height = Math.Abs(LocationXY.Y - LocationX1Y1.Y);

            return Rect;
        }

        private void Res_Cre_Db_Click(object sender, EventArgs e)
        {
            if (Face_Start.Enabled == true)
            {
                DialogResult result;
                result = MessageBox.Show("重建数据库将导致已有数据丢失!", "警告", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    #region  弃用Sqlite
                    ////this.Close();
                    //SqliteConnection Creconn = null;

                    //string dbPath = "Data Source =" + Environment.CurrentDirectory + @"\test.db";
                    //Creconn = new SqliteConnection(dbPath);//创建数据库实例，指定文件位置  
                    //string sqlstr = "drop table if exists Face_Image ;Create table Face_Image(id INTEGER PRIMARY KEY AUTOINCREMENT, Face_No varchar(100), Face_name TEXT, Face_img BLOB); ";


                    //try
                    //{
                    //    //如果文件存在就改名bak并生成新的数据库
                    //    Creconn.Open();//打开数据库，若文件不存在会自动创建
                    //    SqliteCommand Sqlcomm = new SqliteCommand(sqlstr, Creconn);
                    //    Sqlcomm.ExecuteReader();
                    //    Creconn.Close();
                    //    string Db_file = @"test.db";
                    //    if (File.Exists(Db_file))
                    //    {
                    //        Log_Box.AppendText("数据库重建成功!\r\n");
                    //        Face_Pic_Count.Text = "Ok";
                    //    }
                    //    else
                    //    {
                    //        Log_Box.AppendText("数据库重建失败!\r\n");
                    //        Face_Pic_Count.Text = "Error";
                    //    }

                    //}
                    //catch (System.Exception ex)
                    //{
                    //    Face_Pic_Count.Text = "Error";
                    //    Log_Box.AppendText("数据库重建失败!\r\n");
                    //    Log_Box.AppendText(ex.Message + "\r\n");
                    //}
                    #endregion
                    Process Rp = new Process();
                    Rp.StartInfo.FileName = @"python";
                    Rp.StartInfo.Arguments = @"-u ./encode.py";
                    Rp.StartInfo.UseShellExecute = false;          //不显示shell                    
                    Rp.StartInfo.CreateNoWindow = true;            //不创建窗口  
                    Rp.StartInfo.RedirectStandardInput = true;     //打开流输入  
                    Rp.StartInfo.RedirectStandardOutput = true;    //打开流输出  
                    Rp.StartInfo.RedirectStandardError = true;     //打开错误流  
                    Rp.Start();
                    Rp.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);
                    Rp.BeginOutputReadLine();
                    Rp.BeginErrorReadLine();

                }
            }
            else
            {
                MessageBox.Show("系统正在运行中!", "警告");
            }
            //Face_Pic_Count.Text = System.IO.Directory.GetFiles(Environment.CurrentDirectory + @"\FaceFile\").Length.ToString();
            //Face_Db_Count.Text = System.IO.Directory.GetFiles(Environment.CurrentDirectory + @"\FaceData").Length.ToString();

        }

        private void Cut_Pic_Save_Click(object sender, EventArgs e)
        {

            if (Face_Name_Text.Text == "")
            {
                MessageBox.Show("必须输入人名!");
            }
            else
            {
                if (Cut_Pic_Box.Image != null)
                {


                    try
                    {
                        Cut_Pic_Box.Image.Save(Environment.CurrentDirectory + @"\FaceFile\" + Face_Name_Text.Text + @".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        MessageBox.Show("录入成功");

                    }
                    catch (System.Exception ex)
                    {
                        Log_Box.AppendText("人脸数据添加失败!\r\n");
                        Log_Box.AppendText(ex.Message + "\r\n");
                    }

                }
                else
                {
                    Log_Box.AppendText("未选择任何图片!\r\n");
                }

            }
        }

        private void Face_Start_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = @"python";
            p.StartInfo.Arguments = @"-u ./core.py" + FaceCoreDebug + FaceCoreReact + FaceCoreLocal;
            p.StartInfo.UseShellExecute = false;          //不显示shell                    
            p.StartInfo.CreateNoWindow = true;            //不创建窗口  
            p.StartInfo.RedirectStandardInput = true;     //打开流输入  
            p.StartInfo.RedirectStandardOutput = true;    //打开流输出  
            p.StartInfo.RedirectStandardError = true;     //打开错误流  
            p.Start();
            p.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            //打印Pid
            //Log_Box.AppendText(p.Id.ToString()+"\r\n");      
            Pid = p.Id;
            Face_Start.Enabled = false;
            Face_Stop.Enabled = true;

            

        }
        void SortOutputHandler(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine(e.Data);
            this.BeginInvoke(new MethodInvoker(() =>
            {
                Log_Box.AppendText("\r\n"+DateTime.Now.ToLocalTime().ToString()+": " + e.Data ?? string.Empty);
                
            }));
        }

        private void Face_Stop_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log_Box.AppendText("\r\n" + DateTime.Now.ToLocalTime().ToString() + ": 停止运行!");
            Process Sp = new Process();
            Sp = Process.GetProcessById(Pid);
            Sp.Kill();
            Face_Stop.Enabled = false;
            Face_Start.Enabled = true;
            Log_Box.AppendText("\r\n" + DateTime.Now.ToLocalTime().ToString()+": 系统已停止!");
        }

        private void Face_Debug_CheckedChanged(object sender, EventArgs e)
        {
            if (Face_Debug.Checked == true)
            {
                FaceCoreDebug = " 1 ";
                Log_Box.AppendText("\r\n" + DateTime.Now.ToLocalTime().ToString() + ": DeBug模式开启");
            }
            else
            {
                FaceCoreDebug = " 0 ";
                Log_Box.AppendText("\r\n" + DateTime.Now.ToLocalTime().ToString() + ": DeBug模式关闭");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Log_Box.AppendText(@FaceCorePath + FaceCoreDebug + FaceCoreReact);
        }

        private void Face_React_CheckedChanged(object sender, EventArgs e)
        {
            if (Face_React.Checked == true)
            {
                FaceCoreReact = " 1 ";
                Log_Box.AppendText("\r\n" + DateTime.Now.ToLocalTime().ToString() + ": 联动模式开启");
            }
            else
            {
                FaceCoreReact = " 0 ";
                Log_Box.AppendText("\r\n" + DateTime.Now.ToLocalTime().ToString() + ": 联动模式关闭");
            }
        }

        private void Face_Local_CheckedChanged(object sender, EventArgs e)
        {
            if (Face_Local.Checked == true)
            {
                FaceCoreLocal = " 1 ";
                Log_Box.AppendText("\r\n" + DateTime.Now.ToLocalTime().ToString() + ": 本地模式开启");
            }
            else
            {
                FaceCoreLocal = " 0 ";
                Log_Box.AppendText("\r\n" + DateTime.Now.ToLocalTime().ToString() + ": 本地模式关闭");
            }
        }
    }

}
