namespace W.PressurizationStationPro
{
    partial class FrmParamSre
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopPanel = new System.Windows.Forms.Panel();
            this.cmb_CPUType = new System.Windows.Forms.ComboBox();
            this.cmb_Camera = new System.Windows.Forms.ComboBox();
            this.toggle_AutoStart = new xbd.ControlLib.xbdToggle();
            this.but_SysCancel = new System.Windows.Forms.Button();
            this.but_SysSet = new System.Windows.Forms.Button();
            this.but_PLCCancel = new System.Windows.Forms.Button();
            this.but_PLCSet = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Slot = new System.Windows.Forms.TextBox();
            this.txt_LogoffTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Rack = new System.Windows.Forms.TextBox();
            this.txt_ScreenTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_IPAddress = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Exit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.TopPanel.Controls.Add(this.cmb_CPUType);
            this.TopPanel.Controls.Add(this.cmb_Camera);
            this.TopPanel.Controls.Add(this.toggle_AutoStart);
            this.TopPanel.Controls.Add(this.but_SysCancel);
            this.TopPanel.Controls.Add(this.but_SysSet);
            this.TopPanel.Controls.Add(this.but_PLCCancel);
            this.TopPanel.Controls.Add(this.but_PLCSet);
            this.TopPanel.Controls.Add(this.label14);
            this.TopPanel.Controls.Add(this.txt_Slot);
            this.TopPanel.Controls.Add(this.txt_LogoffTime);
            this.TopPanel.Controls.Add(this.label7);
            this.TopPanel.Controls.Add(this.label13);
            this.TopPanel.Controls.Add(this.txt_Rack);
            this.TopPanel.Controls.Add(this.txt_ScreenTime);
            this.TopPanel.Controls.Add(this.label8);
            this.TopPanel.Controls.Add(this.label16);
            this.TopPanel.Controls.Add(this.label15);
            this.TopPanel.Controls.Add(this.label12);
            this.TopPanel.Controls.Add(this.label6);
            this.TopPanel.Controls.Add(this.txt_IPAddress);
            this.TopPanel.Controls.Add(this.label11);
            this.TopPanel.Controls.Add(this.label9);
            this.TopPanel.Controls.Add(this.label5);
            this.TopPanel.Controls.Add(this.label10);
            this.TopPanel.Controls.Add(this.panel1);
            this.TopPanel.Controls.Add(this.label4);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopPanel.Location = new System.Drawing.Point(1, 1);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(612, 392);
            this.TopPanel.TabIndex = 0;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // cmb_CPUType
            // 
            this.cmb_CPUType.FormattingEnabled = true;
            this.cmb_CPUType.Location = new System.Drawing.Point(116, 176);
            this.cmb_CPUType.Name = "cmb_CPUType";
            this.cmb_CPUType.Size = new System.Drawing.Size(137, 28);
            this.cmb_CPUType.TabIndex = 12;
            // 
            // cmb_Camera
            // 
            this.cmb_Camera.FormattingEnabled = true;
            this.cmb_Camera.Location = new System.Drawing.Point(447, 274);
            this.cmb_Camera.Name = "cmb_Camera";
            this.cmb_Camera.Size = new System.Drawing.Size(96, 28);
            this.cmb_Camera.TabIndex = 12;
            // 
            // toggle_AutoStart
            // 
            this.toggle_AutoStart.Checked = false;
            this.toggle_AutoStart.FalseColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.toggle_AutoStart.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toggle_AutoStart.Location = new System.Drawing.Point(435, 133);
            this.toggle_AutoStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toggle_AutoStart.Name = "toggle_AutoStart";
            this.toggle_AutoStart.Size = new System.Drawing.Size(77, 32);
            this.toggle_AutoStart.SwitchType = xbd.ControlLib.SwitchType.Quadrilateral;
            this.toggle_AutoStart.TabIndex = 11;
            this.toggle_AutoStart.Texts = null;
            this.toggle_AutoStart.TrueColor = System.Drawing.Color.LimeGreen;
            // 
            // but_SysCancel
            // 
            this.but_SysCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.but_SysCancel.FlatAppearance.BorderSize = 0;
            this.but_SysCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.but_SysCancel.Image = global::W.PressurizationStationPro.Properties.Resources.Yellow;
            this.but_SysCancel.Location = new System.Drawing.Point(481, 340);
            this.but_SysCancel.Name = "but_SysCancel";
            this.but_SysCancel.Size = new System.Drawing.Size(75, 36);
            this.but_SysCancel.TabIndex = 10;
            this.but_SysCancel.Text = "取消设置";
            this.but_SysCancel.UseVisualStyleBackColor = true;
            this.but_SysCancel.Click += new System.EventHandler(this.but_SysCancel_Click);
            // 
            // but_SysSet
            // 
            this.but_SysSet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.but_SysSet.FlatAppearance.BorderSize = 0;
            this.but_SysSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.but_SysSet.Image = global::W.PressurizationStationPro.Properties.Resources.Pink;
            this.but_SysSet.Location = new System.Drawing.Point(358, 340);
            this.but_SysSet.Name = "but_SysSet";
            this.but_SysSet.Size = new System.Drawing.Size(75, 36);
            this.but_SysSet.TabIndex = 10;
            this.but_SysSet.Text = "设置完成";
            this.but_SysSet.UseVisualStyleBackColor = true;
            this.but_SysSet.Click += new System.EventHandler(this.but_SysSet_Click);
            // 
            // but_PLCCancel
            // 
            this.but_PLCCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.but_PLCCancel.FlatAppearance.BorderSize = 0;
            this.but_PLCCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.but_PLCCancel.Image = global::W.PressurizationStationPro.Properties.Resources.Yellow;
            this.but_PLCCancel.Location = new System.Drawing.Point(169, 340);
            this.but_PLCCancel.Name = "but_PLCCancel";
            this.but_PLCCancel.Size = new System.Drawing.Size(75, 36);
            this.but_PLCCancel.TabIndex = 10;
            this.but_PLCCancel.Text = "取消设置";
            this.but_PLCCancel.UseVisualStyleBackColor = true;
            this.but_PLCCancel.Click += new System.EventHandler(this.but_PLCCancel_Click);
            // 
            // but_PLCSet
            // 
            this.but_PLCSet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.but_PLCSet.FlatAppearance.BorderSize = 0;
            this.but_PLCSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.but_PLCSet.Image = global::W.PressurizationStationPro.Properties.Resources.Pink;
            this.but_PLCSet.Location = new System.Drawing.Point(46, 340);
            this.but_PLCSet.Name = "but_PLCSet";
            this.but_PLCSet.Size = new System.Drawing.Size(75, 36);
            this.but_PLCSet.TabIndex = 10;
            this.but_PLCSet.Text = "设置完成";
            this.but_PLCSet.UseVisualStyleBackColor = true;
            this.but_PLCSet.Click += new System.EventHandler(this.but_PLCSet_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(347, 276);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(106, 21);
            this.label14.TabIndex = 8;
            this.label14.Text = "摄像头选择：";
            // 
            // txt_Slot
            // 
            this.txt_Slot.Location = new System.Drawing.Point(116, 275);
            this.txt_Slot.Name = "txt_Slot";
            this.txt_Slot.Size = new System.Drawing.Size(137, 26);
            this.txt_Slot.TabIndex = 9;
            // 
            // txt_LogoffTime
            // 
            this.txt_LogoffTime.Location = new System.Drawing.Point(435, 230);
            this.txt_LogoffTime.Name = "txt_LogoffTime";
            this.txt_LogoffTime.Size = new System.Drawing.Size(73, 26);
            this.txt_LogoffTime.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(35, 276);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 21);
            this.label7.TabIndex = 8;
            this.label7.Text = "插槽号：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(347, 229);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 21);
            this.label13.TabIndex = 6;
            this.label13.Text = "注销时间：";
            // 
            // txt_Rack
            // 
            this.txt_Rack.Location = new System.Drawing.Point(116, 228);
            this.txt_Rack.Name = "txt_Rack";
            this.txt_Rack.Size = new System.Drawing.Size(137, 26);
            this.txt_Rack.TabIndex = 7;
            // 
            // txt_ScreenTime
            // 
            this.txt_ScreenTime.Location = new System.Drawing.Point(435, 180);
            this.txt_ScreenTime.Name = "txt_ScreenTime";
            this.txt_ScreenTime.Size = new System.Drawing.Size(73, 26);
            this.txt_ScreenTime.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(35, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 6;
            this.label8.Text = "机架号：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(514, 232);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 21);
            this.label16.TabIndex = 4;
            this.label16.Text = "S";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(514, 183);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 21);
            this.label15.TabIndex = 4;
            this.label15.Text = "S";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(347, 180);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 21);
            this.label12.TabIndex = 4;
            this.label12.Text = "息屏时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(35, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 21);
            this.label6.TabIndex = 4;
            this.label6.Text = "CPU类型：";
            // 
            // txt_IPAddress
            // 
            this.txt_IPAddress.Location = new System.Drawing.Point(116, 132);
            this.txt_IPAddress.Name = "txt_IPAddress";
            this.txt_IPAddress.Size = new System.Drawing.Size(137, 26);
            this.txt_IPAddress.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(347, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 21);
            this.label11.TabIndex = 2;
            this.label11.Text = "开机启动：";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(300, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 240);
            this.label9.TabIndex = 2;
            this.label9.Text = "IP地址：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(35, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "IP地址：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(335, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 22);
            this.label10.TabIndex = 1;
            this.label10.Text = "▷系统参数";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_Exit);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 48);
            this.panel1.TabIndex = 0;
            // 
            // lbl_Exit
            // 
            this.lbl_Exit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_Exit.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Exit.ForeColor = System.Drawing.Color.White;
            this.lbl_Exit.Location = new System.Drawing.Point(579, 0);
            this.lbl_Exit.Name = "lbl_Exit";
            this.lbl_Exit.Size = new System.Drawing.Size(33, 47);
            this.lbl_Exit.TabIndex = 1;
            this.lbl_Exit.Text = "X";
            this.lbl_Exit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Exit.Click += new System.EventHandler(this.lbl_Exit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(41, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "参数设置";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::W.PressurizationStationPro.Properties.Resources.Param;
            this.pictureBox1.Location = new System.Drawing.Point(4, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(612, 1);
            this.label1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(23, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 22);
            this.label4.TabIndex = 1;
            this.label4.Text = "▷通讯参数";
            // 
            // FrmParamSre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(614, 394);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmParamSre";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmParamSre";
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Exit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_IPAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button but_PLCSet;
        private System.Windows.Forms.TextBox txt_Slot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Rack;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button but_PLCCancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_CPUType;
        private System.Windows.Forms.ComboBox cmb_Camera;
        private xbd.ControlLib.xbdToggle toggle_AutoStart;
        private System.Windows.Forms.Button but_SysCancel;
        private System.Windows.Forms.Button but_SysSet;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_LogoffTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_ScreenTime;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}