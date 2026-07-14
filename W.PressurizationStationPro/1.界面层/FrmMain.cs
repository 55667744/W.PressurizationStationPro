using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using xbd.ControlLib;

namespace W.PressurizationStationPro
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
           // infoService.SetSysInfoToPath(new SysInfo(), sysInfoPath);  测试代码
           this.Load += FrmMain_Load;
           this.FormClosing += FrmMain_FormClosing;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
           cts?.Cancel();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.sysInfo=infoService.GetSysInfoFromPath(sysInfoPath);

            if(sysInfo == null)
            {
                new FrmMsgNoAck("系统配置加载失败","系统配置").ShowDialog();
                return;
            }

            cts=new CancellationTokenSource();

            Task.Run(new Action(() =>
            {

                PLCCommunication();
            }));
        }

        /// <summary>
        /// 多线程方法体，与plc实时通信
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void PLCCommunication()
        {
           while(!cts.IsCancellationRequested)
            {
                //已经连接成功
                if(dataService.isConnected)
                {
                    var data=  dataService.ReadPLCData();
                    if(data.IsSuccess)
                    {
                        //清零错误次数
                        dataService.ErrorTimes = 0;
                        //更新 
                        this.UpdateUIData(data.Content);
                        //逻辑


                        //数据存储
                    }
                    else
                    {
                        //容错次数
                        dataService.ErrorTimes++;
                        if (dataService.ErrorTimes >= dataService.AllowErrorTimes)
                        {
                            dataService.isConnected = false;
                        }

                    }
                    Thread.Sleep(300);

                    dataService.isConnected = false;
                }
                //连接
                else
                {
                    //如果是第一次扫描就直接连接
                    //如果不是第一次扫描就先断开再连接
                   if(!dataService.IsFirstScan)
                    {
                        //重连周期
                        Thread.Sleep(3000);
                        //断开连接
                        dataService.Disconnect();
                    }
                   else
                    {
                        dataService.IsFirstScan = false;
                    }
                    //连接
                       var result=dataService.connect(this.sysInfo);

                    dataService.isConnected = result.IsSuccess;
                }
            }
        }

        /// <summary>
        /// 系统配置文件路径
        /// </summary>
        private string sysInfoPath = Application.StartupPath + "\\SysInfo.ini";

        /// <summary>
        /// 系统配置文件的服务对象
        /// </summary>
        private SysInfoService infoService = new SysInfoService();
        
        /// <summary>
        /// 系统配置对象
        /// </summary>
        private SysInfo sysInfo = new SysInfo();
  
        /// <summary>
        /// 多线程取消源
        /// </summary>
        private CancellationTokenSource cts;

        private PlcDataservice dataService = new PlcDataservice();









        private void btn_ParamSet_Click(object sender, EventArgs e)
        {
            new FrmParamSre(this.sysInfo, this.infoService, this.sysInfoPath).ShowDialog();
        }


        //通用更新UI界面
        private void UpdateUIData(PlcData plcData)
        {
            if (this.InvokeRequired)
            {
                try
                {
                 //委托处理
                  this.Invoke(new Action<PlcData>(UpdateUIData), plcData);
                }
                catch (Exception)
                {

                    return;
                }
               
            }

            else
            {
                // 左侧仪表
                this.lbl_PressureIn.Text = plcData.PressureIn.ToString("f2") + " bar";
                this.lbl_PressureOut.Text = plcData.PressureOut.ToString("f2") + " bar";
                this.meter_PressureIn.Value = plcData.PressureIn;
                this.meter_PressureOut.Value = plcData.PressureOut;

                // 底侧仪表
                this.ms_TempIn1.ParamValue = plcData.TempIn1;
                this.ms_TempIn2.ParamValue = plcData.TempIn2;
                this.ms_TempOut.ParamValue = plcData.TempOut;
                this.ms_PressureTank1.ParamValue = plcData.PressureTank1;
                this.ms_PressureTank2.ParamValue = plcData.PressureTank2;
                this.ms_PressureTankOut.ParamValue = plcData.PressureTankOut;

                // 系统状态
                this.led_RunState.State = plcData.SysRunState;
                this.led_SysAlarmState.State = plcData.SysAlarmState;


                // 系统参数
                this.lbl_PressureTank1.Text = plcData.PressureTank1.ToString("f2");
                this.lbl_LevelTank1.Text = plcData.PressureTank1.ToString("f2");
                this.lbl_PressureTank2.Text = plcData.PressureTank2.ToString("f2");
                this.lbl_LevelTank2.Text = plcData.PressureTank2.ToString("f2");
                this.lbl_PressureTankOut.Text = plcData.PressureTankOut.ToString("f2");

                // 流程图数据
                this.lbl_Tempin1.Text = plcData.TempIn1.ToString("f2");
                this.lbl_Tempin2.Text = plcData.TempIn2.ToString("f2");
                this.lbl_TempOut.Text = plcData.TempOut.ToString("f2");

                this.pump_In1.IsRun = plcData.InPump1State;
                this.pump_In2.IsRun = plcData.InPump2State;

                this.valve_In.State = plcData.ValveInState;
                this.valve_Out.State = plcData.ValveOutState;
                this.motor_Pump1.PumpState = plcData.CirclePump1State ? PumpState.运行 : PumpState.停止;
                this.motor_Pump2.PumpState = plcData.CirclePump2State ? PumpState.运行 : PumpState.停止;

                // 量程 2m
                this.wave_Tank1.Value = Convert.ToInt32((plcData.LevelTank1 / 2.0f) * 100.0f);

                this.wave_Tank2.Value = Convert.ToInt32((plcData.LevelTank2 / 2.0f) * 100.0f);

                this.lbl_PreTankOut.Text = plcData.PressureTankOut.ToString("f2");

                this.btn_Pump1.Text = plcData.CirclePump1State ? "停止" : "启动";

                this.btn_Pump2.Text = plcData.CirclePump2State ? "停止" : "启动";

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
