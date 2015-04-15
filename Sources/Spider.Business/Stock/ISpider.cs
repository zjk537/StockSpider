using Spider.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spider.Models.Stock;

namespace Spider.Business.Stock
{
    /// <summary>
    /// 同步UI消息
    /// </summary>
    /// <param name="urlId">当前处理Url的Id</param>
    /// <param name="state">当前处理状态</param>
    /// <param name="message">返回UI的消息 </param>
    public delegate void UILogEventHandler(int urlId, ProcessState state, string message);

    public interface ISpider
    {

        void SpiderStart(SourceUrlModel urlModel);


        void SpiderAbort();
        /// <summary>
        /// 同步进度消息到页面
        /// </summary>
        event UILogEventHandler SyncLog;

    }
}
