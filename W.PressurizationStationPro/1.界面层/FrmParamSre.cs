using AForge.Imaging.Filters;
using AForge.Video.DirectShow;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.s7netplus;

namespace W.PressurizationStationPro
{
    public partial class FrmParamSre : Form
    {
    

        public FrmParamSre(SysInfo sysInfo, SysInfoService infoService, string sysInfoPath)
        {
            InitializeComponent();
            SysInfo = sysInfo;
            InfoService = infoService;
            SysInfoPath = sysInfoPath;

            ///初始化
           
            this.cmb_CPUType.DataSource=Enum.GetNames(typeof(CpuType));
            FilterInfoCollection infoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach(FilterInfo item in infoCollection)
            {
                this.cmb_Camera.Items.Add(item.Name);
            }
            ///更新

            if(this.SysInfo != null)
            {
                this.txt_IPAddress.Text = this.SysInfo.IPAddress;
                this.cmb_CPUType.Text=this.SysInfo.CpuType.ToString();
                this.txt_Rack.Text = this.SysInfo.Rack.ToString();
                this.txt_Slot.Text = this.SysInfo.slot.ToString();

                this.toggle_AutoStart.Checked = this.SysInfo.AutoStart;
                this.txt_ScreenTime.Text = this.SysInfo.ScreenTime.ToString();
                this.txt_LogoffTime.Text = this.SysInfo.LogoffTime.ToString();

                if(infoCollection.Count>this.SysInfo.CameraIndex)
                {
                    this.cmb_Camera.SelectedIndex = this.SysInfo.CameraIndex;
                }
                this.cmb_Camera.SelectedIndex = this.SysInfo.CameraIndex;

            }

              this.toggle_AutoStart.CheckedChanged+=this.toggle_AutoStart_CheckedChanged;


        }

        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public SysInfo SysInfo;
        public SysInfoService InfoService;
        public string SysInfoPath;
        #region 无边框拖动 

        private Point mPoint;

        

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }



        #endregion

        private void but_PLCSet_Click(object sender, EventArgs e)
        {
            if(this.SysInfo == null)
            {
                this.SysInfo = new SysInfo();
            }
            this.SysInfo.IPAddress = this.txt_IPAddress.Text.Trim();
            this.SysInfo.CpuType = (CpuType)Enum.Parse(typeof(CpuType), this.cmb_CPUType.Text.Trim(),true);
            this.SysInfo.Rack =Convert.ToInt16(this.txt_Rack.Text.Trim());
            this.SysInfo.slot = Convert.ToInt16(this.txt_Slot.Text.Trim());

            bool result = this.InfoService.SetSysInfoToPath(this.SysInfo, this.SysInfoPath);

            if (result == false)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                new FrmMsgNoAck("通信配置保存失败", "通信参数").ShowDialog();
            }
    }

        private void but_PLCCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel;
        }

        private void but_SysSet_Click(object sender, EventArgs e)
        {
            if (this.SysInfo == null)
            {
                this.SysInfo = new SysInfo();
            }
            this.SysInfo.AutoStart = this.toggle_AutoStart.Checked;
            this.SysInfo.ScreenTime = Convert.ToInt32(this.txt_ScreenTime.Text.Trim());
            this.SysInfo.LogoffTime = Convert.ToInt16(this.txt_LogoffTime.Text.Trim());
            this.SysInfo.CameraIndex = this.cmb_Camera.SelectedIndex;

            bool result = this.InfoService.SetSysInfoToPath(this.SysInfo, this.SysInfoPath);

            if (result == false)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                new FrmMsgNoAck("通信配置保存失败", "通信参数").ShowDialog();
            }
        }

        private void but_SysCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void lbl_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toggle_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            AutoStart(this.toggle_AutoStart.Checked);
        }
        #region 开机启动
        /// <summary>  
        /// 修改程序在注册表中的键值  
        /// </summary>  
        /// <param name="isAuto">true:开机启动,false:不开机自启</param> 
        private void AutoStart(bool isAuto = true)
        {
            if (isAuto == true)
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                R_run.SetValue("PressurizationStationPro", System.Windows.Forms.Application.ExecutablePath);
                R_run.Close();
                R_local.Close();
            }
            else
            {
                RegistryKey R_local = Registry.CurrentUser;
                RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                R_run.DeleteValue("PressurizationStationPro", false);
                R_run.Close();
                R_local.Close();
            }
        }
        #endregion
    }
}
