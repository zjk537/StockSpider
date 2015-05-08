using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spider.Common.Http;
using Spider.DataAccess.Stock;
using Spider.Models.Stock;
using Spider.Common;
using Spider.Common.Enums;
using System.Threading;
using log4net;

namespace Spider.Business.Stock
{
    [BusinessDesc("公司分红信息")]
    public class NoticeBusiness : BusinessBase
    {
        NoticeDataAccess noticeDataAccess = new NoticeDataAccess();
        /// <summary>
        ///  爬取网络数据，获取公司分红信息
        /// </summary>
        /// <param name="objCompany">当前公司对象</param>
        public override void SpideData(int companyIndex)
        {
            var companyModel = base.CompanyList[companyIndex];
            SyncUILog(ProcessState.Processing, string.Format("开始下载 {0}【{1}】的公司分红信息...-- {2}", companyModel.CompanyName, companyModel.StockCode, companyIndex));
            HttpItem item = new HttpItem()
            {
                URL = string.Format(this.UrlModel.Url, companyModel.StockCode),
                ContentType = "json"
            };
            string jsonData = new HttpHelper().GetHtml(item);
            if (jsonData.ToLower().Contains("error"))
            {
                logger.Error("接口返回错误代码：" + jsonData);
                return;
            }

            string newString = jsonData.Substring(jsonData.IndexOf("=(") + 2).TrimEnd(')');
            var noticeModels = JsonConvert.DeserializeObject<List<NoticeModel>>(newString);
            if (noticeModels == null || noticeModels.Count == 0) return;

            var filterModels = noticeModels.Where(e =>
            {
                if (e.Title.Contains("登记日"))
                {
                    e.StockCode = companyModel.StockCode;
                    return true;
                }
                return false;
            }).ToList();
            if (filterModels.Count > 0)
            {
                noticeDataAccess.AddNotices(filterModels);
            }
        }

        /// <summary>
        /// 获取最新的 公司公告信息
        /// </summary>
        /// <returns></returns>
        public List<NoticeModel> GetLastNotices()
        {
            return noticeDataAccess.GetLastNotices();
        }
    }
}
