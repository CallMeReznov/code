namespace face_recognition
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Log_Box = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Face_Db_Count = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Face_Pic_Count = new System.Windows.Forms.Label();
            this.Res_Cre_Db = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Face_React = new System.Windows.Forms.CheckBox();
            this.Face_Debug = new System.Windows.Forms.CheckBox();
            this.Face_Start = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Pic_Path_Text = new System.Windows.Forms.TextBox();
            this.Face_Name_Text = new System.Windows.Forms.TextBox();
            this.Pic_Path = new System.Windows.Forms.Label();
            this.Face_Name = new System.Windows.Forms.Label();
            this.Cut_Pic_Save = new System.Windows.Forms.Button();
            this.Cut_Pic_Box = new System.Windows.Forms.PictureBox();
            this.Sel_Pic_Btn = new System.Windows.Forms.Button();
            this.Sel_Pic_box = new System.Windows.Forms.PictureBox();
            this.Face_Stop = new System.Windows.Forms.Button();
            this.Face_Local = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cut_Pic_Box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sel_Pic_box)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1002, 712);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Log_Box);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(994, 680);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "系统设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Log_Box
            // 
            this.Log_Box.Location = new System.Drawing.Point(470, 33);
            this.Log_Box.Multiline = true;
            this.Log_Box.Name = "Log_Box";
            this.Log_Box.Size = new System.Drawing.Size(516, 614);
            this.Log_Box.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Face_Db_Count);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Face_Pic_Count);
            this.groupBox2.Controls.Add(this.Res_Cre_Db);
            this.groupBox2.Location = new System.Drawing.Point(22, 232);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 415);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "人脸库";
            // 
            // Face_Db_Count
            // 
            this.Face_Db_Count.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Face_Db_Count.Location = new System.Drawing.Point(275, 179);
            this.Face_Db_Count.Name = "Face_Db_Count";
            this.Face_Db_Count.Size = new System.Drawing.Size(136, 55);
            this.Face_Db_Count.TabIndex = 4;
            this.Face_Db_Count.Text = "9999";
            this.Face_Db_Count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Face_Db_Count.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 55);
            this.label1.TabIndex = 3;
            this.label1.Text = "已有人脸数据:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 55);
            this.label2.TabIndex = 2;
            this.label2.Text = "已有人脸照片:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // Face_Pic_Count
            // 
            this.Face_Pic_Count.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Face_Pic_Count.Location = new System.Drawing.Point(275, 124);
            this.Face_Pic_Count.Name = "Face_Pic_Count";
            this.Face_Pic_Count.Size = new System.Drawing.Size(136, 55);
            this.Face_Pic_Count.TabIndex = 1;
            this.Face_Pic_Count.Text = "Null";
            this.Face_Pic_Count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Face_Pic_Count.Visible = false;
            // 
            // Res_Cre_Db
            // 
            this.Res_Cre_Db.Location = new System.Drawing.Point(22, 59);
            this.Res_Cre_Db.Name = "Res_Cre_Db";
            this.Res_Cre_Db.Size = new System.Drawing.Size(389, 50);
            this.Res_Cre_Db.TabIndex = 0;
            this.Res_Cre_Db.Text = "重建人脸库";
            this.Res_Cre_Db.UseVisualStyleBackColor = true;
            this.Res_Cre_Db.Click += new System.EventHandler(this.Res_Cre_Db_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Face_Local);
            this.groupBox1.Controls.Add(this.Face_Stop);
            this.groupBox1.Controls.Add(this.Face_React);
            this.groupBox1.Controls.Add(this.Face_Debug);
            this.groupBox1.Controls.Add(this.Face_Start);
            this.groupBox1.Location = new System.Drawing.Point(22, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "开关";
            // 
            // Face_React
            // 
            this.Face_React.AutoSize = true;
            this.Face_React.Location = new System.Drawing.Point(258, 82);
            this.Face_React.Name = "Face_React";
            this.Face_React.Size = new System.Drawing.Size(106, 22);
            this.Face_React.TabIndex = 2;
            this.Face_React.Text = "联动模式";
            this.Face_React.UseVisualStyleBackColor = true;
            this.Face_React.CheckedChanged += new System.EventHandler(this.Face_React_CheckedChanged);
            // 
            // Face_Debug
            // 
            this.Face_Debug.AutoSize = true;
            this.Face_Debug.Location = new System.Drawing.Point(258, 27);
            this.Face_Debug.Name = "Face_Debug";
            this.Face_Debug.Size = new System.Drawing.Size(106, 22);
            this.Face_Debug.TabIndex = 1;
            this.Face_Debug.Text = "调试模式";
            this.Face_Debug.UseVisualStyleBackColor = true;
            this.Face_Debug.CheckedChanged += new System.EventHandler(this.Face_Debug_CheckedChanged);
            // 
            // Face_Start
            // 
            this.Face_Start.Location = new System.Drawing.Point(22, 27);
            this.Face_Start.Name = "Face_Start";
            this.Face_Start.Size = new System.Drawing.Size(180, 50);
            this.Face_Start.TabIndex = 0;
            this.Face_Start.Text = "启动";
            this.Face_Start.UseVisualStyleBackColor = true;
            this.Face_Start.Click += new System.EventHandler(this.Face_Start_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Pic_Path_Text);
            this.tabPage2.Controls.Add(this.Face_Name_Text);
            this.tabPage2.Controls.Add(this.Pic_Path);
            this.tabPage2.Controls.Add(this.Face_Name);
            this.tabPage2.Controls.Add(this.Cut_Pic_Save);
            this.tabPage2.Controls.Add(this.Cut_Pic_Box);
            this.tabPage2.Controls.Add(this.Sel_Pic_Btn);
            this.tabPage2.Controls.Add(this.Sel_Pic_box);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(994, 680);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "信息录入";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Pic_Path_Text
            // 
            this.Pic_Path_Text.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pic_Path_Text.Location = new System.Drawing.Point(165, 95);
            this.Pic_Path_Text.Name = "Pic_Path_Text";
            this.Pic_Path_Text.ReadOnly = true;
            this.Pic_Path_Text.Size = new System.Drawing.Size(808, 39);
            this.Pic_Path_Text.TabIndex = 9;
            // 
            // Face_Name_Text
            // 
            this.Face_Name_Text.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Face_Name_Text.Location = new System.Drawing.Point(165, 32);
            this.Face_Name_Text.Name = "Face_Name_Text";
            this.Face_Name_Text.Size = new System.Drawing.Size(808, 39);
            this.Face_Name_Text.TabIndex = 8;
            // 
            // Pic_Path
            // 
            this.Pic_Path.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Pic_Path.Location = new System.Drawing.Point(8, 95);
            this.Pic_Path.Name = "Pic_Path";
            this.Pic_Path.Size = new System.Drawing.Size(160, 30);
            this.Pic_Path.TabIndex = 7;
            this.Pic_Path.Text = "图片路径:";
            this.Pic_Path.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Face_Name
            // 
            this.Face_Name.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Face_Name.Location = new System.Drawing.Point(8, 30);
            this.Face_Name.Name = "Face_Name";
            this.Face_Name.Size = new System.Drawing.Size(160, 30);
            this.Face_Name.TabIndex = 6;
            this.Face_Name.Text = "人脸名称:";
            this.Face_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cut_Pic_Save
            // 
            this.Cut_Pic_Save.Location = new System.Drawing.Point(536, 152);
            this.Cut_Pic_Save.Name = "Cut_Pic_Save";
            this.Cut_Pic_Save.Size = new System.Drawing.Size(450, 50);
            this.Cut_Pic_Save.TabIndex = 5;
            this.Cut_Pic_Save.Text = "录入系统";
            this.Cut_Pic_Save.UseVisualStyleBackColor = true;
            this.Cut_Pic_Save.Click += new System.EventHandler(this.Cut_Pic_Save_Click);
            // 
            // Cut_Pic_Box
            // 
            this.Cut_Pic_Box.Location = new System.Drawing.Point(536, 208);
            this.Cut_Pic_Box.Name = "Cut_Pic_Box";
            this.Cut_Pic_Box.Size = new System.Drawing.Size(450, 450);
            this.Cut_Pic_Box.TabIndex = 4;
            this.Cut_Pic_Box.TabStop = false;
            // 
            // Sel_Pic_Btn
            // 
            this.Sel_Pic_Btn.Location = new System.Drawing.Point(8, 152);
            this.Sel_Pic_Btn.Name = "Sel_Pic_Btn";
            this.Sel_Pic_Btn.Size = new System.Drawing.Size(450, 50);
            this.Sel_Pic_Btn.TabIndex = 3;
            this.Sel_Pic_Btn.Text = "选取图片";
            this.Sel_Pic_Btn.UseVisualStyleBackColor = true;
            this.Sel_Pic_Btn.Click += new System.EventHandler(this.Sel_Pic_Btn_Click);
            // 
            // Sel_Pic_box
            // 
            this.Sel_Pic_box.Location = new System.Drawing.Point(8, 208);
            this.Sel_Pic_box.Name = "Sel_Pic_box";
            this.Sel_Pic_box.Size = new System.Drawing.Size(450, 450);
            this.Sel_Pic_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Sel_Pic_box.TabIndex = 2;
            this.Sel_Pic_box.TabStop = false;
            this.Sel_Pic_box.Paint += new System.Windows.Forms.PaintEventHandler(this.Sel_Pic_box_Paint);
            this.Sel_Pic_box.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Sel_Pic_box_MouseDown);
            this.Sel_Pic_box.MouseHover += new System.EventHandler(this.Sel_Pic_box_MouseHover);
            this.Sel_Pic_box.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Sel_Pic_box_MouseMove);
            this.Sel_Pic_box.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Sel_Pic_box_MouseUp);
            // 
            // Face_Stop
            // 
            this.Face_Stop.Enabled = false;
            this.Face_Stop.Location = new System.Drawing.Point(22, 106);
            this.Face_Stop.Name = "Face_Stop";
            this.Face_Stop.Size = new System.Drawing.Size(180, 50);
            this.Face_Stop.TabIndex = 3;
            this.Face_Stop.Text = "停止";
            this.Face_Stop.UseVisualStyleBackColor = true;
            this.Face_Stop.Click += new System.EventHandler(this.button1_Click);
            // 
            // Face_Local
            // 
            this.Face_Local.AutoSize = true;
            this.Face_Local.Location = new System.Drawing.Point(258, 134);
            this.Face_Local.Name = "Face_Local";
            this.Face_Local.Size = new System.Drawing.Size(106, 22);
            this.Face_Local.TabIndex = 4;
            this.Face_Local.Text = "本地模式";
            this.Face_Local.UseVisualStyleBackColor = true;
            this.Face_Local.CheckedChanged += new System.EventHandler(this.Face_Local_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 712);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "人脸抓怕系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cut_Pic_Box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sel_Pic_box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Cut_Pic_Save;
        private System.Windows.Forms.PictureBox Cut_Pic_Box;
        private System.Windows.Forms.Button Sel_Pic_Btn;
        private System.Windows.Forms.PictureBox Sel_Pic_box;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Face_Start;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Res_Cre_Db;
        private System.Windows.Forms.Label Face_Pic_Count;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Log_Box;
        private System.Windows.Forms.Label Pic_Path;
        private System.Windows.Forms.Label Face_Name;
        private System.Windows.Forms.TextBox Pic_Path_Text;
        private System.Windows.Forms.TextBox Face_Name_Text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Face_Db_Count;
        private System.Windows.Forms.CheckBox Face_React;
        private System.Windows.Forms.CheckBox Face_Debug;
        private System.Windows.Forms.Button Face_Stop;
        private System.Windows.Forms.CheckBox Face_Local;
    }
}

