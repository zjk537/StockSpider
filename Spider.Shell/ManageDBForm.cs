using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spider.Business.Stock;
using Spider.Common.Enums;

namespace Spider.Shell
{
    public partial class ManageDBForm : Form
    {
        BusinessBase bsBase = new BusinessBase();
        UrlBusiness bsUrl = new UrlBusiness();
        public ManageDBForm()
        {
            InitializeComponent();
        }

        private void btnExeClear_Click(object sender, EventArgs e)
        {
            lblClearMsg.Text = "数据清理中....";
            List<string> tableNames = new List<string>();
            foreach (Control ctl in this.gbClear.Controls)
            {
                CheckBox ckb = ctl as CheckBox;
                if (ckb != null && ckb.Checked)
                {
                    tableNames.Add(ckb.Tag.ToString());
                }
            }
            bsBase.ClearExpireData(tableNames.ToArray());
            lblClearMsg.Text = "数据清理完成.";
        }

        private void btnExeUrl_Click(object sender, EventArgs e)
        {
            bsUrl.UpdateWorkingUrlStatus(ProcessState.Start);
        }
    }
}
