using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.PressurizationStationPro
{
    public class HistoryDataService
    {
        /// <summary>
        /// 插入一条数据记录
        /// </summary>
        /// <param name="historyData"></param>
        /// <returns></returns>
        public bool AddHistoryData(HistoryData historyData)
        {
            string sql = "Insert into HistoryData " +
                         "(InsertTime,PressureIn,PressureOut,TempIn1,TempIn2,TempOut,PressureTank1,PressureTank2,LevelTank1,LevelTank2,PressureTankOut) " +
                         "values " +
                         "(@InsertTime,@PressureIn,@PressureOut,@TempIn1,@TempIn2,@TempOut,@PressureTank1,@PressureTank2,@LevelTank1,@LevelTank2,@PressureTankOut)";

            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@InsertTime", historyData.InsertTime),
                new SQLiteParameter("@PressureIn", historyData.PressureIn),
                new SQLiteParameter("@PressureOut", historyData.PressureOut),
                new SQLiteParameter("@TempIn1", historyData.TempIn1),
                new SQLiteParameter("@TempIn2", historyData.TempIn2),
                new SQLiteParameter("@TempOut", historyData.TempOut),
                new SQLiteParameter("@PressureTank1", historyData.PressureTank1),
                new SQLiteParameter("@PressureTank2", historyData.PressureTank2),
                new SQLiteParameter("@LevelTank1", historyData.LevelTank1),
                new SQLiteParameter("@LevelTank2", historyData.LevelTank2),
                new SQLiteParameter("@PressureTankOut", historyData.PressureTankOut),
            };

            return SQLiteHelper.ExecuteNonQuery(sql, parameters) == 1;
        }

    }
}
