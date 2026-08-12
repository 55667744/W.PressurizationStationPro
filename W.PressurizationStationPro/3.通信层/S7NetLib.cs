using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xbd.DataConvertLib;
using xbd.s7netplus;
using DataType = xbd.s7netplus.DataType;


namespace W.PressurizationStationPro
{
    public class S7NetLib
    {

        //私有字段
        private Plc siemen;


        //公有属性
        public CpuType CpuType {  get; set; }

        public string  IPAddress {  get; set; }

        public short Rack {  get; set; }

        public short Slot {  get; set; }


        public S7NetLib()  
        {

        }

        //有参构造函数
        public S7NetLib(CpuType cpuType,string ip, short rack ,short slot)
        {
            this.CpuType = cpuType;
            this.IPAddress = ip;
            this.Rack = rack;
            this.Slot = slot;
        }

        //锁标志位
        private static object objLock=new object();


        //建立连接的方法
        public OperateResult Conncet()
        {
            try
            {
            //如果已经连接先断开再重新连
            if(this.siemen !=null&&this.siemen.IsConnected)
            {
                this.siemen.Close();
            }
            //链接plc
            siemen=new Plc(this.CpuType,this.IPAddress,this.Rack,this.Slot);
            siemen.Open();
                return OperateResult.CreateSuccessResult();

            }
            catch (Exception ex)
            {

               return OperateResult.CreateFailResult(ex.Message);
            }

        }


        //断开连接
        public void DisConnect()
        {
            if (this.siemen != null && this.siemen.IsConnected)
            {
                this.siemen.Close();
            }
        }
    
        //读取字节
        public OperateResult<byte[]>ReadByteArray(DataType dataTypr,int db,int start ,int count)
        {
            if (this.siemen!=null&&this.siemen.IsConnected)
            {
                try
                {
                lock (objLock)
                    {
                return OperateResult.CreateSuccessResult(siemen.ReadBytes(dataTypr, db, start, count));
                    }
                }
                catch (Exception ex)
                {

                    return OperateResult.CreateFailResult<byte[]>("读取失败："+ex.Message);
                }
               
            }
            else
            {
                return OperateResult.CreateFailResult<byte[]>("请检查plc连接是否正常");
            }
            
        }



        //读取单个变量
        public OperateResult<object> ReadVariable(string varAddress)
        {
            if (this.siemen != null && this.siemen.IsConnected)
            {
                try
                {
                    lock (objLock)
                    {
                        return OperateResult.CreateSuccessResult(siemen.Read(varAddress));
                    }
                }
                catch (Exception ex)
                {

                    return OperateResult.CreateFailResult<object>("读取失败：" + ex.Message);
                }

            }
            else
            {
                return OperateResult.CreateFailResult<object>("请检查plc连接是否正常");
            }

        }



        //读取类对象
        public OperateResult<T> ReadClass<T>(int db ,int start) where T : class
        {
            if (this.siemen != null && this.siemen.IsConnected)
            {
                try
                {
                    lock (objLock)
                    {
                        return OperateResult.CreateSuccessResult(siemen.ReadClass<T>(db,start));
                    }
                }
                catch (Exception ex)
                {

                    return OperateResult.CreateFailResult<T>("读取失败：" + ex.Message);
                }

            }
            else
            {
                return OperateResult.CreateFailResult<T>("请检查plc连接是否正常");
            }

        }


        //单个变量写入
        public OperateResult WriteVariable(string varAddress,object  value)
        {
            if (this.siemen != null && this.siemen.IsConnected)
            {
                try
                {
                    lock (objLock)
                    {
                        siemen.Write(varAddress, value);
                        return OperateResult.CreateSuccessResult();
                    }
                
                }
                catch (Exception ex)
                {

                    return OperateResult.CreateFailResult("读取失败：" + ex.Message);
                }

            }
            else
            {
                return OperateResult.CreateFailResult("请检查plc连接是否正常");
            }

        }

    }
}
