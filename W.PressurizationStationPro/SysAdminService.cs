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
        public List<SysAdmin> QuerySysAdmins()         //创建了一个QuerySysAdmins方法，返回类型是SysAdmin的list集合
        {
            string sql = "select LoginId,LoginName,LoginPwd,RoleName from SysAdmin";       //写一个查询用的sql语句在SysAdmin表中查询id用户属性用户密码

            SQLiteDataReader dataReader = SQLiteHelper.ExecuteReader(sql);       //SQLiteDataReader是sqlite的官方查询工具类，和SQL Server的SqlDataReader一样，
                                                                                 //SQLiteHelper是一个数据库帮助类，.ExecuteReader(sql)这个是一个静态方法，获取一个可以
                                                                                 //让阅读器阅读的对象

            List<SysAdmin> sysAdmins = new List<SysAdmin>();       //这里new了一个list集合sysAdmins，集合属性是SysAdmin。

            while (dataReader.Read())                 //while循环的条件是刚刚上面创建的读工具类dataReader
            {
                sysAdmins.Add(new SysAdmin()     //循环体内部，创建sysadmin用户属性，然后获取当下“dataReader.Read()”给while循环的索引，取历遍这个索引的用户数据。
                {
                    LoginId = Convert.ToInt32(dataReader["LoginId"]),
                    LoginName = dataReader["LoginName"].ToString(),
                    LoginPwd = dataReader["Password"].ToString(),
                    RoleName = (RoleName)Enum.Parse(typeof(RoleName), dataReader["RoleName"].ToString())
                }
                
                );

            }
            //关闭datareader
            dataReader.Close();
            return sysAdmins;    //返回读取的用户数据
        }

            ///用户验证
            
             public SysAdmin AdminLogin(SysAdmin sysAdmin)    //创建一个AdminLogin方法返回类型为SysAdmin，输入为SysAdmin类型的参数：sysAdmin。
        {
            //[1]封装sql语句
            string sql = "select LoginId,RoleName from SysAdmin where LoginName=@LoginName and LoginPwd=@LoginPwd";

            SQLiteParameter[] parameters = new SQLiteParameter[]         //new了一个SQLiteParameter集合parameters
             {
               new SQLiteParameter("@LoginName",sysAdmin.LoginName),       //把这两个数据给加@的两个占位符，然后传给parameters，写入sql数据库
               new SQLiteParameter("@LoginPwd",sysAdmin.LoginPwd)
             };
            //[2]提交查询
            SQLiteDataReader dataReader =SQLiteHelper.ExecuteReader(sql,parameters);    //用SQLiteHelper类里面的ExecuteReader方法查询，

            //[3]判断是否成功
            if (dataReader.Read())
            {                    
                sysAdmin.LoginId = Convert.ToInt32(dataReader["LoginId"]);              //如果读到数据就把读到的这两个参数写入到运行程序的sysAdmin里面保存
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

