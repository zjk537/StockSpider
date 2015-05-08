using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spider.Common;
using Spider.Common.Http;
using Spider.DataAccess.Stock;
using Spider.Models.Stock;
using Spider.Common.Enums;
using System.Threading;
using System.Text.RegularExpressions;
using log4net;

namespace Spider.Business.Stock
{
    public class StockCompanyBusiness : ISpider
    {
        ILog logger = LogManager.GetLogger("Spider");
        StockCompanyDataAccess companyDataAccess = new StockCompanyDataAccess();
        UrlDataAccess urlDataAccess = new UrlDataAccess();

        private int maxThread = 10;    // 最大线程数
        private object objLock = new object();// 安全锁
        private int pageIndex = 1;
        SourceUrlModel UrlModel = null;
        ProcessState state;
        public event UILogEventHandler SyncLog;


        /// <summary>
        /// 开始爬取数据
        /// </summary>
        /// <param name="pageIndex"></param>
        public void SpiderData(int pageIndex)
        {
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = this.UrlModel.Url,
                ContentType = "json"
            };
            int pageSize = Convert.ToInt32(Utility.GetQueryStringValue(item.URL, "pageSize"));
            SyncUILog(state, string.Format("数据采集中... 每页{0}条数据，当前页：{1}", pageSize, pageIndex));
            List<StockCompanyModel> companyList = new List<StockCompanyModel>();

            item.URL = Regex.Replace(item.URL, @"page=\d", "page=" + pageIndex);
            string jsonData = helper.GetHtml(item);
            if (jsonData.ToLower().Contains("error"))
            {
                logger.ErrorFormat("接口:{0} 返回错误代码：{1}", item.URL, jsonData);
                return;
            }

            string newString = jsonData.Substring(jsonData.IndexOf('=') + 1);
            RankData rankData = JsonConvert.DeserializeObject<RankData>(newString);
            rankData.Rank.ForEach(e =>
            {
                string[] dataArray = e.Split(',');
                companyList.Add(new StockCompanyModel()
                {
                    StockBourse = dataArray[0].EndsWith("1") ? BourseType.SH : BourseType.SZ,
                    StockCode = dataArray[1],
                    CompanyName = dataArray[2],
                    CreatedDate = DateTime.Now
                });
            });

            // 过滤 Code
            var stockCodes = companyList.Select(e => e.StockCode).ToArray();
            var filterCodes = companyDataAccess.FilterExistCodes(stockCodes);
            if (filterCodes.Length > 0)
            {
                List<StockCompanyModel> newCompanyList = companyList.Where(e => filterCodes.Contains(e.StockCode)).ToList();
                companyDataAccess.AddCompanies(newCompanyList);
            }
        }


        private int pageCount = 0;
        /// <summary>
        /// 获取共有多少page
        /// </summary>
        private int PageCount
        {
            get
            {
                if (pageCount > 0)
                {
                    return pageCount;
                }
                HttpHelper helper = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = this.UrlModel.Url,
                    ContentType = "json"
                };
                item.URL = Regex.Replace(item.URL, @"page=\d+", "page=1");
                string jsonData = helper.GetHtml(item);
                string newString = jsonData.Substring(jsonData.IndexOf('=') + 1);
                if (newString.Contains("error"))
                {
                    logger.ErrorFormat("接口:{0} 返回错误代码：{1}", item.URL, jsonData);
                    return 0;
                }
                RankData rankData = JsonConvert.DeserializeObject<RankData>(newString);
                pageCount = rankData.Pages;
                return pageCount;
            }
        }


        /// <summary>
        /// 开启多线程 分析数据
        /// </summary>
        /// <param name="urlModel"></param>
        public void SpiderStart(SourceUrlModel urlModel)
        {
            try
            {
                logger.Info("Spider Start");
                // model 
                if (urlModel == null)
                {
                    return;
                }
                this.UrlModel = urlModel;

                UILogProcessing();
                Thread thread = new Thread(delegate()
                {
                    while (true)
                    {
                        if (state != ProcessState.Processing) return;

                        int threadCount = maxThread;
                        SyncUILog(ProcessState.Start, "总开启线程数：" + threadCount);
                        //开启多线程
                        Thread[] arrThread = new Thread[threadCount];
                        for (int i = 0; i < threadCount; i++)
                        {
                            arrThread[i] = new Thread(new ThreadStart(ThreadSpiderFun));
                            arrThread[i].IsBackground = true;
                            arrThread[i].Start();
                        }

                        for (int i = 0; i < threadCount; i++)
                        {
                            if (arrThread[i] != null && arrThread[i].IsAlive)
                            {
                                arrThread[i].Join();
                            }
                        }
                    }
                });

                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }

        private void ThreadSpiderFun()
        {
            // 非 处理中 状态，代表 UI点击暂停或已爬完数据
            if (state != ProcessState.Processing) return;

            while (true)
            {
                lock (objLock)
                {
                    if (state != ProcessState.Processing) break;
                    if (pageIndex > this.PageCount) break;
                    SpiderData(pageIndex++);
                }
            }
            lock (objLock)
            {
                // 处理中 状态，代表 UI未点击暂停 且已爬完数据
                if (state == ProcessState.Processing)
                {
                    UILogComplete();
                }
            }
        }


        public void SpiderAbort()
        {
            UILogAbort();
        }
        /// <summary>
        /// 用户中止操作
        /// </summary>
        private void UILogAbort()
        {
            logger.Info("Spider Abort");
            state = ProcessState.Abort;
            urlDataAccess.UpdateUrlState(this.UrlModel.UrlId, (int)state);
            SyncUILog(state, "用户中止公司数据采集！");
        }

        /// <summary>
        /// 操作完成
        /// </summary>
        private void UILogComplete()
        {
            logger.Info("Spider Complete");
            state = ProcessState.Complete;
            urlDataAccess.UpdateUrlState(this.UrlModel.UrlId, (int)state);
            SyncUILog(state, "公司数据采集完成！");
        }

        /// <summary>
        /// 处理中...
        /// </summary>
        private void UILogProcessing()
        {
            state = ProcessState.Processing;
            urlDataAccess.UpdateUrlState(this.UrlModel.UrlId, (int)state);
            SyncUILog(state, "公司数据采集中...");
        }

        /// <summary>
        /// 同步UI 消息
        /// </summary>
        /// <param name="state"></param>
        /// <param name="message"></param>
        private void SyncUILog(ProcessState state, string message)
        {
            if (SyncLog != null)
            {
                SyncLog(this.UrlModel.UrlId, state, message);
            }
        }


        public class RankData
        {
            public List<string> Rank { get; set; }
            public int Pages { get; set; }
        }
    }
}
