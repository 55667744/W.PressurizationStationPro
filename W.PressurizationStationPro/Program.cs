using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W.PressurizationStationPro
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
        /// <summary>
        /// 锁屏时间的滴答次数
        /// </summary>
        public static int TickCount { get; set; }

        /// <summary>
        /// 当前登录的用户
        /// </summary>
        public static SysAmin CurrentUser;
    }
}
