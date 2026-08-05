using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using xbd.ControlLib;
using Timer = System.Windows.Forms.Timer;

namespace W.PressurizationStationPro
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            this.updateTimer.Interval = 500;             //interval 是timer的一个方法，时间间隔！这里tpdatime是从179行new的一个方法
            this.updateTimer.Tick += updataTimer_Tick;        //+=这个知识点是事件绑定这里吧.tick事件给了后者。他是到时触发的作用，额是从0到1的逻辑吗？应该是运行下面的方法
            this.updateTimer.Start();    //启动定时器


            // infoService.SetSysInfoToPath(new SysInfo(), sysInfoPath);  测试代码
           this.Load += FrmMain_Load;             //调用69行的方法绑定到窗体加载事件，他本身就是用户自定义方法，是一个系统加载方法。，里面有获取系统信息吧plc信息在加载winform的时候就读上来
            this.FormClosing += FrmMain_FormClosing;  
        }

        private void updataTimer_Tick(object sender, EventArgs e)     //这是一个定时器点击事件，里面有一个方法是获取系统信息的方法
        {
            this.lbl_Time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+ " " +                  //这两行是给窗体的时间标签赋值，格式是年月日时分秒
                CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);   //获取当前时间的星期几
            new CultureInfo("zh-CN").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);     //这个是获取当前时间的星期几的中文格式（但是我不太懂这个语法）
            this.led_PLCState.State = dataService.isConnected;   //dataservice是下面new的plc系统信息对象isconnected是里面的连接状态，把plc连接状态反馈给窗体控件，作为页面连接状态提示

            //如果大于0才启用该功能
            if (sysInfo.ScreenTime > 0)             //sysinfo是下面new的一个系统信息类，screentime是这个类中的一个属性，是检测无操作时间的
            {                                                  
                Program.TickCount++;                //program是c#系统运行的主程序里面有一个tickcount属性，自增加代表记录无操作时间（这里有个疑问，为什么不直接拿ScreenTime判断）
                if (sysInfo.ScreenTime*1000/this.updateTimer.Interval==Program.TickCount)
                {   //系统无操作时间*1000除系统设置的500间隔，是否等于program自增加的数字，(这里不懂到时候问claude)
                    //锁屏调用Windows底层API
                    LockWorkStation();   
                }
            }
            if(sysInfo.ScreenTime > 0)              //启用用户注销（退出登录）功能
            {
                TimeSpan timeSpan = DateTime.Now-this.LoginTime;    //当前时间减去上次登录第一次扫描位时的时间
                if (timeSpan.TotalMinutes >= sysInfo.ScreenTime)           //登录总时长大于无操作的时间              TimeSpan timeSpan这个类型我不理解回头要记
                {
                    // 注销用户
                    Program.CurrentUser = null;
                    this.btn_UserLogin.Text = "用户登录";                //这应该是改变用户登录状态
                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)   //这也不知道，，cts是那个退出令牌的实例。
        {
           cts?.Cancel();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.sysInfo=infoService.GetSysInfoFromPath(sysInfoPath);   //这个获取本地电脑信息，给sysinfo。GetSysInfoFromPath我缺少他的笔记！

            if (sysInfo == null)         //如果系统配置对象为空
            {
                new FrmMsgNoAck("系统配置加载失败","系统配置").ShowDialog();          //这里不理解这个语法
                return;
            }
            //这里又有一个锁屏？上面不是锁过了吗，，
            if(sysInfo.ScreenTime > 0)
            {
                messageFilter = new MessageFilter();              //new了一个检测鼠标是否移动的实例
                Application.AddMessageFilter(messageFilter);     //Application.AddMessageFilter没见过这个类，，很陌生
            }

            cts=new CancellationTokenSource();         //这里很奇怪下面已经定义了一个cts了。

            Task.Run(new Action(() =>      //多线程和委托这是重难点 new了一个action委托      运行下面的方法体。 
            {

                PLCCommunication();
            }));
        }

        /// <summary>
        /// 多线程方法体，与plc实时通信
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void PLCCommunication()       //这是那个多线程的方法体（plc的连接）
        {
           while(!cts.IsCancellationRequested)      //有点绕有两个cts，IsCancellationRequested这个我也不知道是什么，回去做笔记
            {
                //已经连接成功
                if(dataService.isConnected)                //plc数据服务对象的实例，调用检测plc是否连接成功的属性
                {
                    var data=  dataService.ReadPLCData();       //读取数据存到data里面
                    if(data.IsSuccess)             // 这里刚刚定义了一个data，为什么可以直接调用issuccess？随便一个类型都可以吗？这个issuccess是自定义类里面的吧                                
                    {
                        //清零错误次数
                        dataService.ErrorTimes = 0;            // 操作成功通讯错误次数清零
                        //更新    
                        this.UpdateUIData(data.Content);  //data是刚才读到的plc数据，然后他为什么调用content？我转定义显示是数据，应该是plc数据作为updateuidata更新页面的参数
                        //逻辑


                        //数据存储
                    }
                    else
                    {
                        //容错次数
                        dataService.ErrorTimes++;   //通信错误次数自增加
                        if (dataService.ErrorTimes >= dataService.AllowErrorTimes)        //超过允许的错误次数
                        {
                            dataService.isConnected = false;                  //把连接状态改为false
                        }

                    }
                    Thread.Sleep(300);       //线程睡眠300毫秒    

                    dataService.isConnected = false;     //又有一个把连接状态改为false
                }
                //连接
                else
                {
                    //如果是第一次扫描就直接连接
                    //如果不是第一次扫描就先断开再连接
                   if(!dataService.IsFirstScan)   //判断是不是第一次扫描
                    {
                        //重连周期
                        Thread.Sleep(3000);     //3s
                        //断开连接
                        dataService.Disconnect();
                    }
                   else
                    {
                        dataService.IsFirstScan = false;    //这里是第一次扫描的置0位！
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
        private SysInfo sysInfo = new SysInfo();      //这个是系统配置对象，里面有plc的ip地址，端口号，连接方式等信息，
                                                                                           //好像是当时老师让我写的一个类，里面有很多属性，都是系统配置相关的，
                                                                                           //这个在实际运用中应该是填写plc的相关信息

        /// <summary>
        /// 多线程取消源
        /// </summary>
        private CancellationTokenSource cts;        //CancellationTokenSource这个我不太懂，查了一下是取消令牌，cts是这个类的实例。

        private PlcDataservice dataService = new PlcDataservice();     // 这里很关键，这里new了一个PLC数据服务对象，dataservice
                                                                                                               //  里面有很多方法和属性，主要是和PLC通信的逻辑



        private Timer updateTimer = new Timer();

        /// <summary>
        /// 第一次扫描标志位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool FirsScan=true;

        private MessageFilter messageFilter;

        private DateTime LoginTime=DateTime.Now;  //第一次扫描的登录的时间
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
                ///第一次扫描执行，以后就不执行了  
                if (FirsScan)
                {
                    this.toggle_Pump1.Checked=plcData.InPump1State;
                    this.toggle_Pump2.Checked = plcData.InPump2State;
                    FirsScan = false;
                }


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
                this.led_SysAlarmState.State =!plcData.SysAlarmState;


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

        private void button3_Click(object sender, EventArgs e)  //退出按钮
        {
            this.Close();
        }

        private void btn_Pump1_Click(object sender, EventArgs e)
        {
            dataService.CirclePump2Control(this.btn_Pump2.Text == "启动");
        }

        private void btn_Pump2_Click(object sender, EventArgs e)
        {
            dataService.CirclePump2Control(this.btn_Pump2.Text == "启动");
        }

        private void toggle_Pump1_CheckedChanged(object sender, EventArgs e)
        {
            if (dataService.InPump1Control(this.toggle_Pump1.Checked) == false)
            {
                this.toggle_Pump1.CheckedChanged -= toggle_Pump1_CheckedChanged;
                this.toggle_Pump1.Checked = !this.toggle_Pump1.Checked;
                this.toggle_Pump1.CheckedChanged += toggle_Pump1_CheckedChanged;
            }

        }

        private void toggle_Pump2_CheckedChanged(object sender, EventArgs e)
        {
         
            if (dataService.InPump2Control(this.toggle_Pump2.Checked) == false)
            {
                this.toggle_Pump2.CheckedChanged -= toggle_Pump2_CheckedChanged;
                this.toggle_Pump2.Checked = !this.toggle_Pump2.Checked;
                this.toggle_Pump2.CheckedChanged += toggle_Pump2_CheckedChanged;
            }
  
        }

        private void btu_SysReset_Click(object sender, EventArgs e)
        {
            dataService.SysReset();
        }

        private void CommonValve_DoubleClick(object sender, EventArgs e)
        {
            if(sender is xbdValve valve)
            {
                FrmValveControl   frmValveControl=new FrmValveControl(valve.ValveName,valve.State,this.dataService);


                frmValveControl.ShowDialog();


            }
        }

        #region 系统锁屏

        [DllImport("user32")]
        public static extern bool LockWorkStation();

        #endregion
    }


    #region 消息筛选器    这个是老师代码库的代码直接拖过来的，应该是检测鼠标是否移动，理解他难度有点大
    public class MessageFilter : IMessageFilter        
    {
        public bool PreFilterMessage(ref Message m)
        {
            //如果检测到有鼠标或则键盘的消息，则使计数为0.....
            if (m.Msg == 0x0200 || m.Msg == 0x0201 || m.Msg == 0x0204 || m.Msg == 0x0207)
            {
                Program.TickCount = 0;
            }
            return false;
        }
    }
    #endregion


}
