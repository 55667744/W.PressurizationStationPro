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
        public DateTime InsertTime {  get; set; }
        public string PressureIn {  get; set; }
        public string PressureOut {  get; set; }
        public string TempIn1 {  get; set; }
        public string TempIn2 {  set; get; }
        public string TempOut {  set; get; }
        public string PressureTank1 {  get; set; }
        public string PressureTank2 {  get; set; }
        public string LevelTank1 {  set; get; }
        public string LevelTank2 {  set; get; }
        public string PressureTankOut {  set; get; }
    }
}
