namespace W.PressurizationStationPro
{
    partial class MeterShow
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_ParamName = new System.Windows.Forms.Label();
            this.lbl_ParamValue = new System.Windows.Forms.Label();
            this.metet_Param = new xbd.ControlLib.xbdAnalogMeter();
            this.SuspendLayout();
            // 
            // lbl_ParamName
            // 
            this.lbl_ParamName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_ParamName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_ParamName.Location = new System.Drawing.Point(0, 144);
            this.lbl_ParamName.Name = "lbl_ParamName";
            this.lbl_ParamName.Size = new System.Drawing.Size(147, 24);
            this.lbl_ParamName.TabIndex = 0;
            this.lbl_ParamName.Text = "出水管温度";
            this.lbl_ParamName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ParamValue
            // 
            this.lbl_ParamValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbl_ParamValue.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_ParamValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.lbl_ParamValue.Location = new System.Drawing.Point(37, 118);
            this.lbl_ParamValue.Name = "lbl_ParamValue";
            this.lbl_ParamValue.Size = new System.Drawing.Size(82, 26);
            this.lbl_ParamValue.TabIndex = 2;
            this.lbl_ParamValue.Text = "0.00℃";
            this.lbl_ParamValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metet_Param
            // 
            this.metet_Param.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.metet_Param.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.metet_Param.Location = new System.Drawing.Point(5, 5);
            this.metet_Param.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.metet_Param.MaxValue = 100D;
            this.metet_Param.MinValue = 0D;
            this.metet_Param.Name = "metet_Param";
            this.metet_Param.NeedleColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.metet_Param.Renderer = null;
            this.metet_Param.ScaleColor = System.Drawing.Color.White;
            this.metet_Param.ScaleDivisions = 11;
            this.metet_Param.ScaleSubDivisions = 4;
            this.metet_Param.Size = new System.Drawing.Size(145, 143);
            this.metet_Param.TabIndex = 1;
            this.metet_Param.Value = 0D;
            this.metet_Param.ViewGlass = false;
            // 
            // MeterShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.lbl_ParamValue);
            this.Controls.Add(this.metet_Param);
            this.Controls.Add(this.lbl_ParamName);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MeterShow";
            this.Size = new System.Drawing.Size(147, 168);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_ParamName;
        private System.Windows.Forms.Label lbl_ParamValue;
        private xbd.ControlLib.xbdAnalogMeter metet_Param;
    }
}
