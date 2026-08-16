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
using xbd.PressurizationStationPro;
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
        #region 字段和属性
        /// <summary>
        /// 系统配置文件路径
        /// </summary>
        private string sysInfoPath = Application.StartupPath + "\\SysInfo.ini";                   //这里我不懂

        /// <summary>
        /// 系统配置文件的服务对象
        /// </summary>
        private SysInfoService infoService = new SysInfoService();    //这个我转到定义里面去看了，这是配置文件，里面是 PLC 的机槽号、通讯地址等内容。

        /// <summary>
        /// 系统配置对象                                ///这里有个很大的问题：sysInfo 和infoService这两个有什么区别？我看里面都是通讯相关的内容，还有机槽、PLC 相关的东西，不理解为什么会有两个这样的对象。
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



        private Timer updateTimer = new Timer();           //这里定义了一个计时器的类实例，在这个main函数里面的最上面有调用过它

        /// <summary>
        /// 第一次扫描标志位                                       //这里的第一次扫描标志位是什么？扫描标志位是程序启动第一次扫描标志位吗？从上到下。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool FirsScan = true;

        private MessageFilter messageFilter;                        //这是一个消息筛选器的一个实例在这个main函数的最下面。

        private DateTime LoginTime = DateTime.Now;  //第一次扫描的登录的时间   这里 data team 它不是时间长度，是一个时间点。

        //摄像头采集对象
        private CameraHelper camera = null;

        //
        private HistoryDataService historyService = new HistoryDataService();

        //把上次存储时间记录下来
        public DateTime lasTime=DateTime.Now;

        #endregion



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
                {   //系统无操作时间*1000除系统设置的500间隔，是否等于program自增加的数字，(这里不懂到时候问ai)
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
           camera?.StopCamera();
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
            //采集摄像头
            this.camera = new CameraHelper(sysInfo.CameraIndex, this.vsp_Panel);
            this.camera.StartCamera();
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

                        //数据存储 1s  扫描周期小于1s
                        int timeSpan = DateTime.Now.Second - lasTime.Second;

                        if(timeSpan==1||timeSpan==-59)
                        {
                            historyService.AddHistoryData(new HistoryData()
                            {
                                InsertTime = DateTime.Now,
                                PressureIn = data.Content.PressureIn.ToString("f2"),
                                PressureOut = data.Content.PressureOut.ToString("f2"),
                                TempIn1 = data.Content.TempIn1.ToString("f2"),
                                TempIn2 = data.Content.TempIn2.ToString("f2"),
                                TempOut = data.Content.TempOut.ToString("f2"),
                                PressureTank1 = data.Content.PressureTank1.ToString("f2"),
                                PressureTank2 = data.Content.PressureTank2.ToString("f2"),
                                LevelTank1 = data.Content.LevelTank1.ToString("f2"),
                                LevelTank2 = data.Content.LevelTank2.ToString("f2"),
                                PressureTankOut = data.Content.PressureTankOut.ToString("f2"),
                            });
                        }
                        lasTime = DateTime.Now;

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
                       var result=dataService.connect(this.sysInfo);      //把下面的plc信息提供过来然后连接

                    dataService.isConnected = result.IsSuccess;  //我不知道.IsSuccess;是怎么读取成功的，也没有给他输入任何变量或标志位，不知道他为什么直接返回了一个通讯链接。
                }                                                
            }
        }
    
        private void btn_ParamSet_Click(object sender, EventArgs e)
        {
            new FrmParamSre(this.sysInfo, this.infoService, this.sysInfoPath).ShowDialog();
        }


        //通用更新UI界面
        private void UpdateUIData(PlcData plcData)     //这里把 PLC 里面的数据传进去，然后用这个方法去更新 UI 界面，也就是更新 UI 界面上控件的显示数据。
        {
            if (this.InvokeRequired)                //InvokeRequired是检测现在所在的程序线程是不是调用它的线程，，不过这里有个疑问他怎么知道他跑的是UI线程啊
            {
                try
                {
                 //委托处理
                  this.Invoke(new Action<PlcData>(UpdateUIData), plcData);  //这里检测如果不是 UI 线程的话，就把数据更新进去。但是这里好像就是 UI 线程吧？Pic data 这不就是 UI 里面的数据显示吗？搞不懂这个地方。
                }
                catch (Exception)
                {

                    return;
                }
               
            }

            else
            {
                ///第一次扫描执行，以后就不执行了  
                if (FirsScan)                 //第一次也就是程序刚刚运行，从上到下扫描周期第一次。
                {
                    this.toggle_Pump1.Checked=plcData.InPump1State;           //这里也不太理解，应该是吧，数据给到优送页面上面的控件商。
                    this.toggle_Pump2.Checked = plcData.InPump2State;
                    FirsScan = false;
                }

                                               
                // 左侧仪表                                               //下面都是 PRC 数据的更新，都往winfrom页面上显示。
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
            dataService.CirclePump2Control(this.btn_Pump1.Text == "启动");
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

        private void btn_UserLogin_Click(object sender, EventArgs e)
        {
           DialogResult  dialogResult= new FrmLoagin().ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                this.lbl_User.Text = Program.CurrentUser.LoginName;
            }
        }
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
