using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spider.Common;
using Spider.Common.Enums;
using System.Threading;
using Spider.Models.Stock;
using Spider.Common.Http;
using Spider.DataAccess.Stock;
using log4net;
using System.Reflection;

namespace Spider.Business.Stock
{
    public class BusinessBase : ISpider
    {

        public ILog logger = LogManager.GetLogger("Spider");
        UrlDataAccess urlDataAccess = new UrlDataAccess();

        public event UILogEventHandler SyncLog;

        public ProcessState State { get; set; }    // 处理状态
        private int maxThread = 10;    // 最大线程数
        private object objLock = new object(); // 线程锁
        private int listIndex = 0;
        /// <summary>
        /// 当前数据请求的 Url 对象 
        /// </summary>
        public SourceUrlModel UrlModel { get; private set; }


        private List<StockCompanyModel> companyList;
        public List<StockCompanyModel> CompanyList
        {
            get
            {
                if (companyList != null && companyList.Count > 0)
                    return companyList;

                StockCompanyDataAccess companyDataAccess = new StockCompanyDataAccess();
                companyList = companyDataAccess.GetAllCompanies();
                if (companyList == null || companyList.Count == 0)
                {
                    SyncUILog(ProcessState.Error, "上市公司信息错误：上市公司信息为空！");
                    return null;
                }
                return companyList;
            }
        }


        /// <summary>
        /// 获取当前操作
        /// </summary>
        private string businessDesc = null;
        public string BusinessDesc
        {
            get
            {
                if (!string.IsNullOrEmpty(businessDesc))
                {
                    return businessDesc;
                }
                Type type = this.GetType();
                BusinessDescAttribute businessDescAttr = (BusinessDescAttribute)type.GetCustomAttribute(typeof(BusinessDescAttribute));
                if (businessDescAttr != null)
                {
                    businessDesc = businessDescAttr.Description;
                }
                return businessDesc;
            }
        }

        /// <summary>
        /// 统计可开启线程数 默认10个线程
        /// </summary>
        /// <returns></returns>
        private int GetTheadCount()
        {
            int companyCount = CompanyList.Count;
            int threadCount = companyCount < maxThread ? companyCount : maxThread;
            return threadCount;
        }

        /// <summary>
        /// 开启 多线程 准备分析数据
        /// </summary>
        /// <param name="objModel">UI 端的UrlModel</param>
        public void SpiderStart(SourceUrlModel urlModel)
        {
            try
            {
                logger.Info(this.BusinessDesc + " Spider Start!");
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
                        if (State != ProcessState.Processing) return;

                        int threadCount = GetTheadCount();
                        SyncUILog(ProcessState.Start, "总开启线程数：" + threadCount);
                        //开启多线程
                        Thread[] arrThread = new Thread[threadCount];
                        for (int i = 0; i < threadCount; i++)
                        {
                            //arrThread[i] = new Thread(new ParameterizedThreadStart(SpiderData));
                            //arrThread[i].Start(threadParams);
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

        public void SpiderAbort()
        {
            UILogAbort();
        }

        public void ThreadSpiderFun()
        {
            // 非 处理中 状态，代表 UI点击暂停或已爬完数据
            if (State != ProcessState.Processing) return;

            while (true)
            {
                lock (objLock)
                {
                    // 非 处理中 状态，代表 UI点击暂停或已爬完数据
                    if (State != ProcessState.Processing) break;
                    if (listIndex >= this.CompanyList.Count) break;
                    SpiderData(listIndex++);
                }
            }

            lock (objLock)
            {
                // 处理中 状态，代表 UI未点击暂停 且已爬完数据
                if (State == ProcessState.Processing)
                {
                    UILogComplete();
                }
            }
        }

        /// <summary>
        /// 采集数据，调用网络接口，开始爬取数据
        /// </summary>
        /// <param name="companyModel">公司信息对象</param>
        public virtual void SpiderData(int companyIndex)
        {
            throw new Exception("The method SpiderData is not override.");
        }

        /// <summary>
        /// 用户中止操作
        /// </summary>
        private void UILogAbort()
        {
            State = ProcessState.Abort;
            urlDataAccess.UpdateUrlState(this.UrlModel.UrlId, (int)State);
            SyncUILog(State, string.Format("用户中止获取{0} 操作！", this.BusinessDesc));
        }
        /// <summary>
        /// 操作完成
        /// </summary>
        private void UILogComplete()
        {
            State = ProcessState.Complete;
            urlDataAccess.UpdateUrlState(this.UrlModel.UrlId, (int)State);
            SyncUILog(State, string.Format("获取{0} 操作完成！", this.BusinessDesc));
        }

        /// <summary>
        /// 处理中...
        /// </summary>
        private void UILogProcessing()
        {
            State = ProcessState.Processing;
            urlDataAccess.UpdateUrlState(this.UrlModel.UrlId, (int)State);
            SyncUILog(State, string.Format("获取{0} 操作处理中...", this.BusinessDesc));
        }

        /// <summary>
        /// 同步UI消息
        /// </summary>
        /// <param name="state">当前处理状态</param>
        /// <param name="message">返回UI的消息 </param>
        public void SyncUILog(ProcessState state, string message)
        {
            if (SyncLog != null)
            {
                SyncLog(this.UrlModel.UrlId, state, message);
            }
        }

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BusinessDescAttribute : Attribute
    {
        public string Description { get; set; }

        public BusinessDescAttribute(string desc)
        {
            this.Description = desc;
        }
    }
}
