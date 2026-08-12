using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace W.PressurizationStationPro
{
    public partial class FrmValveControl : Form
    {
        public FrmValveControl(string valveName , bool state  , PlcDataservice dataService)
        {
            InitializeComponent();
            this.TopMost = true;
            this.valvaName = valveName;
            this.state = state;
            this.dataSerice = dataService;
            this.lbl_Message.Text = "是否确定要 " + (this.state ? "打开" : "关闭") + " " + this.valvaName + "？";
          
        }
        private string valvaName;
        private bool state = false;
        private PlcDataservice dataSerice;


       private void but_OK_Click(object sender, EventArgs e)
            {
            if(dataSerice.isConnected)
            {
                bool result = true;
                switch(valvaName)
                {
                    case("进水阀"):
                        result=dataSerice.ValveInControl(!this.state);
                        break;
                    case ("出水阀"):
                        result=dataSerice.ValveInControl(!this.state);
                        break;
                    default:
                        new FrmMsgNoAck("未知阀门，请检查！","阀门控制").ShowDialog();
                        return;
                }
                if(result)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    new FrmMsgNoAck("阀门控制失败，请检查！", "阀门控制").ShowDialog();
                }
            }
            else
            {
                new FrmMsgNoAck("请检查PLC是否连接正常！", "阀门控制").ShowDialog();
            }
                this.DialogResult = DialogResult.OK;
            }
        private void btn_Cancel_Click(object sender, EventArgs e)
                {
                    this.DialogResult= DialogResult.Cancel;
                }
        private void lbl_Exit_Click(object sender, EventArgs e)
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
