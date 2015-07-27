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

namespace Spider.Shell
{
    public partial class NoticeForm : Form
    {
        public NoticeForm()
        {
            InitializeComponent();
        }

        private void NoticeForm_Load(object sender, EventArgs e)
        {
            BindNotice();
        }

        private void BindNotice()
        {
            NoticeBusiness business = new NoticeBusiness();
            this.gridNotice.DataSource = business.GetLastNotices();
            this.gridNotice.Refresh();
        }
    }
}
