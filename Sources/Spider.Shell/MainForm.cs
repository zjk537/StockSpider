using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spider.Business.Stock;
using Spider.Common;
using Spider.Models.Stock;
using System.Reflection;
using System.Threading;
using Spider.Common.Enums;
using Spider.Business.Stock;


namespace Spider.Shell
{
    public partial class MainForm : Form
    {
        Dictionary<int, ISpider> dictSpider = new Dictionary<int, ISpider>();
        public MainForm()
        {
            Application.ApplicationExit += new EventHandler(AppExit);
            InitializeComponent();
        }

        /// <summary>
        /// 程序退出时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppExit(object sender, EventArgs e)
        {
            //this.Close();   只是关闭当前窗口，若不是主窗体的话，是无法退出程序的，另外若有托管线程（非主线程），也无法干净地退出；
            //Application.Exit();  强制所有消息中止，退出所有的窗体，但是若有托管线程（非主线程），也无法干净地退出；
            //Application.ExitThread(); 强制中止调用线程上的所有消息，同样面临其它线程无法正确退出的问题；
            //System.Environment.Exit(0);   这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净。
            UrlBusiness business = new UrlBusiness();
            business.UpdateWorkingUrlStatus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindSourceUrlGrid();
        }


        private delegate void MyDelegetUI();

        private void BindSourceUrlGrid()
        {
            UrlBusiness business = new UrlBusiness();
            this.gridSourceUrl.DataSource = business.GetSourceUrls();
            this.gridSourceUrl.Refresh();
        }

        #region control actions

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindSourceUrlGrid();
        }

        private void btnAddUrl_Click(object sender, EventArgs e)
        {
            AddUrlForm urlForm = new AddUrlForm();
            urlForm.ShowDialog();
        }

        private void gridSourceUrl_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
                return;

            DataGridView view = (DataGridView)sender;
            if (view.Columns[e.ColumnIndex].DataPropertyName == "UrlType")
            {
                FieldInfo fieldInfo = typeof(UrlType).GetField(e.Value.ToString());
                UrlDescAttribute descAttr = (UrlDescAttribute)fieldInfo.GetCustomAttribute(typeof(UrlDescAttribute));
                e.Value = descAttr.Description;
            }
            if (view.Columns[e.ColumnIndex].DataPropertyName == "State")
            {
                int state = Convert.ToInt32(e.Value);
                switch (state)
                {
                    case (int)ProcessState.Start:
                        e.Value = "未下载";
                        break;
                    case (int)ProcessState.Processing:
                        e.Value = "数据下载中...";
                        break;
                    case (int)ProcessState.Abort:
                        e.Value = "用户中止";
                        break;
                    case (int)ProcessState.Complete:
                        e.Value = "完成";
                        break;
                    default:
                        break;
                }

            }
        }

        private void gridSourceUrl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || !(sender is DataGridView))
                return;

            DataGridView view = (DataGridView)sender;

            if (e.ColumnIndex == view.Columns["clmOperate"].Index)
            {
                var dgCell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var rowData = (SourceUrlModel)view.Rows[e.RowIndex].DataBoundItem;
                if (rowData.State == (int)ProcessState.Processing)
                {
                    SpiderAbort(e.RowIndex, rowData);
                    dgCell.Value = "恢复";
                    rowData.State = (int)ProcessState.Abort;
                }
                else
                {
                    SpiderStart(e.RowIndex, rowData);
                    dgCell.Value = "中止";
                    rowData.State = (int)ProcessState.Processing;
                }
                this.gridSourceUrl.UpdateCellValue(view.Columns["clmState"].Index, e.RowIndex);
            }
        }

        #endregion



        private void AddLog(string message)
        {
            string strText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "  " + message;
            if (listLog.InvokeRequired)
            {
                MyDelegetUI d = delegate
                {
                    listLog.Items.Insert(0, strText);
                };
                listLog.Invoke(d);
            }
            else
            {
                listLog.Items.Insert(0, strText);
            }
        }
        private void ShowLog(int urlId, ProcessState state, string message)
        {
            AddLog(message);

            this.Invoke(new MethodInvoker(delegate
                {
                    if (state == ProcessState.Processing)
                    {
                        progressBar1.Value = (progressBar1.Value >= progressBar1.Maximum) ? 0 : progressBar1.Value + 1;
                    }
                    else
                    {
                        progressBar1.Value = 0;
                    }

                }));
            if (state == ProcessState.Complete)
            {
                this.Invoke(new MethodInvoker(delegate
                    {
                        //获取完成，更新Grid状态
                        SpiderComplete(urlId, state);
                    }));
            }
        }

        private void SpiderComplete(int urlId, ProcessState state)
        {
            var dgViewSource = (List<SourceUrlModel>)this.gridSourceUrl.DataSource;
            for (int i = 0; i < dgViewSource.Count; i++)
            {
                var model = dgViewSource[i];
                if (model.UrlId == urlId)
                {
                    model.State = (int)state;
                    this.gridSourceUrl.UpdateCellValue(this.gridSourceUrl.Columns["clmState"].Index, i);
                    this.gridSourceUrl.Rows[i].Cells["clmOperate"].Value = "获取";
                    dictSpider.Remove(i);
                }
            }
        }


        private void SpiderStart(int rowIndex, SourceUrlModel urlModel)
        {
            ISpider spider = null;
            if (dictSpider.ContainsKey(rowIndex))
            {
                spider = dictSpider[rowIndex];
                spider.SpiderStart(urlModel);
                return;
            }

            switch (urlModel.UrlType)
            {
                case UrlType.BasicUrl:
                    spider = new StockCompanyBusiness();
                    break;
                case UrlType.NoticeUrl:
                    spider = new NoticeBusiness();
                    break;
                case UrlType.TradeUrl:

                    break;
                default:
                    break;
            }
            if (spider != null)
            {
                dictSpider.Add(rowIndex, spider);
                spider.SyncLog += ShowLog;
                spider.SpiderStart(urlModel);
            }
        }

        private void SpiderAbort(int rowIndex, SourceUrlModel urlModel)
        {
            ISpider spider = dictSpider[rowIndex];
            spider.SpiderAbort();

        }
        private void btnShowNotice_Click(object sender, EventArgs e)
        {
            NoticeForm form = new NoticeForm();
            form.ShowDialog();
        }


    }


}
