using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Configuration;
using Npgsql;




namespace DisPlayCount
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“testdbDataSet.test_view”中。您可以根据需要移动或删除它。
            this.test_viewTableAdapter.Fill(this.testdbDataSet.test_view);
            timer1.Enabled = true;
            //contextMenuStrip1.ItemAdded = 

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.test_viewTableAdapter.Fill(this.testdbDataSet.test_view);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string returnValue = Microsoft.VisualBasic.Interaction.InputBox("输入修改值(0.1----1)", "修改窗体透明度", "", 300, 300);
            try {
                this.Opacity = Convert.ToDouble(returnValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FormBorderStyle == FormBorderStyle.None)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                }
                else
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
