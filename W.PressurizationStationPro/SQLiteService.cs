using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.PressurizationStationPro
{
    /// <summary>
    /// 设置连接字符串
    /// </summary>
    public class SQLiteService
    {

        public void SrtConnectStr(string connStr)
        {
            SQLiteHelper.ConnString=connStr;



        }


    }
}
