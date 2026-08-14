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
    public partial class FrmLagin : Form
    {
        public FrmLagin()
        {
            InitializeComponent();
            //this.Load += FrmLogin_Load;

        }


        private SysAdminService adminService =new SysAdminService();

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
            //【验证数据】
            if()
        }

        private void txt_Pwd_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
