using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W.PressurizationStationPro
{
    public class HistoryData
    {
        [ExcelColumnName("日期时间")]
        [ExcelFormat("yyyy-MM-dd HH:mm:ss")]
        public DateTime InsertTime {  get; set; }
        [ExcelColumnName("进口压力")]
        public string PressureIn {  get; set; }
        [ExcelColumnName("出口压力")]
        public string PressureOut {  get; set; }
        [ExcelColumnName("进口温度1")]
        public string TempIn1 {  get; set; }
        [ExcelColumnName("进口温度2")]
        public string TempIn2 {  set; get; }
        [ExcelColumnName("出口温度")]

        public string TempOut {  set; get; }
        [ExcelColumnName("水箱压力1")]
        public string PressureTank1 {  get; set; }
        [ExcelColumnName("水箱压力2")]
        public string PressureTank2 {  get; set; }
        [ExcelColumnName("水箱液位1")]
        public string LevelTank1 {  set; get; }
        [ExcelColumnName("水箱液位12")]
        public string LevelTank2 {  set; get; }
        [ExcelColumnName("水箱出口压力")]
        public string PressureTankOut {  set; get; }

    }
}
