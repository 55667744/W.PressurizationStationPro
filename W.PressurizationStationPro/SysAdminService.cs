using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace W.PressurizationStationPro
{
    /// <summary>
    /// 业务层：核心是方法
    /// </summary>
    public class SysAdminService
    {
        ///获取所有用户对象
        public List<SysAdmin> QuerySysAdmin()
        {
            string sql = "select LoginId,LoginName,LoginPwd,RoleName form SysAdmin";

            SQLiteDataReader dataReader = SQLiteHelper.ExecuteReader(sql);

            List<SysAdmin> sysAdmins = new List<SysAdmin>();

            while (dataReader.Read())
            {
                SysAdmin sysAdmin = (new SysAdmin()
                {
                    LoginId = Convert.ToInt32(dataReader["LoginId"]),
                    LoginName = dataReader["LoginName"].ToString(),
                    LoginPwd = dataReader["Password"].ToString(),
                    RoleName = (RoleName)Enum.Parse(typeof(RoleName), dataReader["RoleName"].ToString())
                });


            }
            //关闭datareader
            dataReader.Close();
            return sysAdmins;
        }

            ///用户验证
            
             public SysAdmin AdminLogin(SysAdmin sysAdmin)
             {
            //[1]封装sql语句
            string sql = "stelect LoginId,RoleName from SysAdmin where LoginName=@LoginName and LoginPwd=@LoginPwd";

            SQLiteParameter[] parameters = new SQLiteParameter[]
             {
               new SQLiteParameter("@LoginName",sysAdmin.LoginName),
               new SQLiteParameter("@LoginPwd",sysAdmin.LoginPwd)
             };
            //[2]提交查询
            SQLiteDataReader dataReader =SQLiteHelper.ExecuteReader(sql,parameters);

            //[3]判断是否成功
            if(dataReader.Read())
            {
                sysAdmin.LoginId = Convert.ToInt32(dataReader["LoginId"]);
                sysAdmin.RoleName = (RoleName)Enum.Parse(typeof(RoleName), dataReader
                    ["RoleName"].ToString());
            }
            else
            {
                //赋值为空对象
                sysAdmin = null;

            }
            //关闭dataReader
            dataReader.Close ();
            return sysAdmin;


             }
        }

    }

