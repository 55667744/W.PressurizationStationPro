using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.DataConvertLib;

namespace W.PressurizationStationPro
{

    /// <summary>
    /// 业务类       核心：方法
    /// </summary>
    public class PlcDataservice
    {
       /// <summary>
       /// 第一次扫描的标志位
       /// </summary>
       public bool IsFirstScan { get; set; } = true; 
        
        /// <summary>
        /// 当前通讯，标志位
        /// </summary>
        public bool isConnected { get; set; } = false;

        //通信错误次数
        public int ErrorTimes { get; set; }

        //允许错误次数
        public int AllowErrorTimes { get; set; } = 3;


        private S7NetLib s7Net = null;   //私有字段：PLC通信对象

        ///共有方法：建立连接
        public OperateResult connect(SysInfo sysInfo)
        {
            s7Net=new S7NetLib(sysInfo.CpuType,sysInfo.IPAddress,sysInfo.Rack, sysInfo.slot);
            return s7Net.Conncet();
        }



        ///共有方法：断开连接
        public void Disconnect()
        {
            if (s7Net!=null)
            {
                s7Net.DisConnect();
            }



         }

        /// <summary>
        /// 数据读取方法
        /// </summary>
        /// <returns></returns>
        public OperateResult<PlcData>ReadPLCData()
        {

            int byteCount = 44;
            //批量读取
            var result = this.s7Net.ReadByteArray(xbd.s7netplus.DataType.DataBlock,1,0, byteCount);

            if(result.IsSuccess&&result.Content.Length== byteCount)
            {

                PlcData plcData= new PlcData();
                //数据解析
                //bool解析  DB1.DBX0.0  先确定类，再确定方法
                plcData.InPump1State = BitLib.GetBitFromByteArray(result.Content,0, 0);
                plcData.InPump2State = BitLib.GetBitFromByteArray(result.Content, 0, 1);
                plcData.CirclePump1State = BitLib.GetBitFromByteArray(result.Content, 0, 2);
                plcData.CirclePump2State = BitLib.GetBitFromByteArray(result.Content, 0, 3);
                plcData.ValveInState = BitLib.GetBitFromByteArray(result.Content, 0, 4);
                plcData.ValveOutState = BitLib.GetBitFromByteArray(result.Content, 0, 5);
                plcData.SysRunState = BitLib.GetBitFromByteArray(result.Content, 0, 6);
                plcData.SysAlarmState = BitLib.GetBitFromByteArray(result.Content, 0, 7);

                //float解析  DB1.DBD4
                plcData.PressureIn = FloatLib.GetFloatFromByteArray(result.Content, 4);
                plcData.PressureOut = FloatLib.GetFloatFromByteArray(result.Content, 8);
                plcData.TempIn1 = FloatLib.GetFloatFromByteArray(result.Content, 12);
                plcData.TempIn2 = FloatLib.GetFloatFromByteArray(result.Content, 16);
                plcData.TempOut = FloatLib.GetFloatFromByteArray(result.Content, 20);
                plcData.PressureTank1 = FloatLib.GetFloatFromByteArray(result.Content, 24);
                plcData.PressureTank2 = FloatLib.GetFloatFromByteArray(result.Content, 28);
                plcData.LevelTank1 = FloatLib.GetFloatFromByteArray(result.Content, 32);
                plcData.LevelTank2 = FloatLib.GetFloatFromByteArray(result.Content, 36);
                plcData.PressureTankOut = FloatLib.GetFloatFromByteArray(result.Content, 40);


                return OperateResult.CreateSuccessResult(plcData);


            }
            else
            {

                return OperateResult.CreateFailResult<PlcData>(result.Message);


            }

        }
        /// <summary>
        /// 1号进水泵控制方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InPump1Control(bool value)
        {
            string startAddress = "DB1.DBX0.0";
            string stopAddress = "DB1.DBX0.1";
            string controlAddress=value? startAddress : stopAddress;
            bool result=s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result&=s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

        /// <summary>
        /// 2号进水泵控制方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InPump2Control(bool value)
        {
            string startAddress = "DB1.DBX0.2";
            string stopAddress = "DB1.DBX0.3";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

        /// <summary>
        /// 1号循环泵控制方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CirclePump1Control(bool value)
        {
            string startAddress = "DB1.DBX0.4";
            string stopAddress = "DB1.DBX0.5";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }


        /// <summary>
        /// 2号循环泵控制方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CirclePump2Control(bool value)
        {
            string startAddress = "DB1.DBX0.6";
            string stopAddress = "DB1.DBX0.7";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

        /// <summary>
        /// 系统复位方法方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SysReset()
        {
           
            string controlAddress = "DB1.DBX0.101.4";
           
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

        /// <summary>
        /// 进水阀控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValveInControl(bool value)
        {
            string startAddress = "DB1.DBX101.0";
            string stopAddress = "DB1.DBX101.1";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

        /// <summary>
        /// 出水阀控制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValveOutControl(bool value)
        {
            string startAddress = "DB1.DBX101.2";
            string stopAddress = "DB1.DBX101.3";
            string controlAddress = value ? startAddress : stopAddress;
            bool result = s7Net.WriteVariable(controlAddress, true).IsSuccess;
            Thread.Sleep(50);
            result &= s7Net.WriteVariable(controlAddress, false).IsSuccess;
            return result;
        }

    }
}
