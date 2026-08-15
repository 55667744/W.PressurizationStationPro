using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace W.PressurizationStationPro
{
    public partial class FrmLoagin : Form
    {
        public FrmLoagin()
        {
            InitializeComponent();
            this.Load += FrmLogin_Load;

        }


        private SysAdminService adminService =new SysAdminService();    //给登录窗口创建一个“用户服务助手”，后面查用户、验证登录都用它。

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            var sysAdmins=adminService.QuerySysAdmins();
            if(sysAdmins.Count>0)
            {
                foreach(var item in sysAdmins)
                {
                    this.cmb_User.Items.Add(item.LoginName);
                }
                this.cmb_User.SelectedIndex = 0;

            }
            else
            {
                new FrmMsgNoAck("没有可以使用的登录用户，请联系管理员！", "登录提示").ShowDialog();          
            }    
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            //【1】验证数据
            if(this.cmb_User.Text.Trim().Length==0)
            {
                new FrmMsgNoAck("请选择登录用户！","登录提示").ShowDialog();
                return;
            }
            if (this.txt_Pwd.Text.Trim().Length == 0)
            {
                new FrmMsgNoAck("请选择登录密码！", "登录提示").ShowDialog();
                return;
            }

            //【2】封装对象
            SysAdmin sysAdmin = new SysAdmin()
            {
                LoginName = this.cmb_User.Text.Trim(),
                LoginPwd= this.txt_Pwd.Text.Trim()
            };

            //【3】用户查询
            sysAdmin=adminService.AdminLogin(sysAdmin);
            //【4】判断结果
            if(sysAdmin==null)
            {
                new FrmMsgNoAck("用户账号或密码不正确！", "登录提示").ShowDialog();
                return;
            }
            else
            {
                this.DialogResult=DialogResult.OK;
                //将用户数据存取
                Program.CurrentUser = sysAdmin;
            }

        }

        private void txt_Pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Login_Click(null,null);


            }
        }

        private void lbl_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #region 无边框拖动 

        private Point mPoint;
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        #endregion
    }
}
