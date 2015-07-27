using Spider.Common.Enums;
using Spider.Common.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spider.Models.Stock;
using Spider.DataAccess.Stock;
using System.Text.RegularExpressions;


namespace Spider.Business.Stock
{
    [BusinessDesc("公司大单交易数据")]
    public class BigDealBusiness : BusinessBase
    {
        BigDealDataAccess dataAccess = new BigDealDataAccess();
        /// <summary>
        ///  爬取网络数据，获取公司公司大单交易数据
        /// </summary>
        /// <param name="objCompany">当前公司对象</param>
        public override void SpideData(int companyIndex)
        {
            var companyModel = base.CompanyList[companyIndex];
            SyncUILog(ProcessState.Processing, string.Format("开始下载 {0}【{1}】的公司公司大单交易数据...-- {2}", companyModel.CompanyName, companyModel.StockCode, companyIndex));
            PageSpideData(companyModel);
        }

        public void PageSpideData(StockCompanyModel companyModel)
        {
            var pageIndex = 1;
            string dealDate = DateTime.Now.ToString("yyyy-MM-dd");
            HttpItem item = new HttpItem()
            {
                URL = string.Format(this.UrlModel.Url, companyModel.StockBourse.ToString().ToLower() + companyModel.StockCode, 40000, dealDate),
                ContentType = "json"
            };
            while (true)
            {
                item.URL = Regex.Replace(item.URL, @"page=\d+", "page=" + pageIndex);
                pageIndex++;
                string jsonData = new HttpHelper().GetHtml(item);
                if (jsonData.ToLower().Contains("error"))
                {
                    logger.Error("接口返回错误代码：" + jsonData);
                    return;
                }

                var bigDealModels = JsonConvert.DeserializeObject<List<BigDealModel>>(jsonData);
                //未请求到数据，中断处理
                if (bigDealModels == null || bigDealModels.Count == 0)
                    break;

                List<BigDealModel> filterModels = null;
                BigDealModel maxModel = dataAccess.GetMaxTickTime(companyModel.StockCode, dealDate);
                if (maxModel == null)
                {
                    filterModels = bigDealModels;
                }
                else
                {
                    filterModels = bigDealModels.Where(e => Convert.ToDateTime(e.TickTime) >= Convert.ToDateTime(maxModel.TickTime) && e.Volume != maxModel.Volume).ToList();
                }
                //maxTickTime之前数据已采集过，可以中止采集
                if (filterModels.Count == 0)
                    continue;
                dataAccess.AddBigDealRecord(filterModels, dealDate);
            }
        }
    }
}
