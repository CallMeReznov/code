namespace PowerStatu
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.uiRichTextBox1 = new Sunny.UI.UIRichTextBox();
            this.uiTextBox1 = new Sunny.UI.UITextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSymbolButton1.Location = new System.Drawing.Point(245, 40);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Size = new System.Drawing.Size(75, 29);
            this.uiSymbolButton1.Symbol = 61683;
            this.uiSymbolButton1.TabIndex = 0;
            this.uiSymbolButton1.Text = "Start";
            this.uiSymbolButton1.Click += new System.EventHandler(this.uiSymbolButton1_Click);
            // 
            // uiRichTextBox1
            // 
            this.uiRichTextBox1.AutoWordSelection = true;
            this.uiRichTextBox1.FillColor = System.Drawing.Color.White;
            this.uiRichTextBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiRichTextBox1.Location = new System.Drawing.Point(4, 79);
            this.uiRichTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRichTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox1.Name = "uiRichTextBox1";
            this.uiRichTextBox1.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox1.Size = new System.Drawing.Size(315, 370);
            this.uiRichTextBox1.TabIndex = 1;
            this.uiRichTextBox1.DoubleClick += new System.EventHandler(this.uiRichTextBox1_DoubleClick);
            this.uiRichTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.uiRichTextBox1_MouseDoubleClick);
            // 
            // uiTextBox1
            // 
            this.uiTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox1.FillColor = System.Drawing.Color.White;
            this.uiTextBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiTextBox1.Location = new System.Drawing.Point(4, 40);
            this.uiTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox1.Maximum = 2147483647D;
            this.uiTextBox1.Minimum = -2147483648D;
            this.uiTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox1.Name = "uiTextBox1";
            this.uiTextBox1.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox1.Size = new System.Drawing.Size(233, 29);
            this.uiTextBox1.TabIndex = 2;
            this.uiTextBox1.Watermark = "请输入电话号码";
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 455);
            this.Controls.Add(this.uiTextBox1);
            this.Controls.Add(this.uiRichTextBox1);
            this.Controls.Add(this.uiSymbolButton1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "电源检测";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UIRichTextBox uiRichTextBox1;
        private Sunny.UI.UITextBox uiTextBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

