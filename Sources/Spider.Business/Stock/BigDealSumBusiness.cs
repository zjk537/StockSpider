using Newtonsoft.Json;
using Spider.Common.Enums;
using Spider.Common.Http;
using Spider.DataAccess.Stock;
using Spider.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Business.Stock
{
    public class BigDealSumBusiness : BusinessBase
    {
        BigDealSumDataAccess dataAccess = new BigDealSumDataAccess();
        public override void SpideData(int companyIndex)
        {
            string dealDate = DateTime.Now.ToString("yyyy-MM-dd");
            var companyModel = base.CompanyList[companyIndex];
            SyncUILog(ProcessState.Processing, string.Format("开始下载 {0}【{1}】的公司大单汇总数据...-- {2}", companyModel.CompanyName, companyModel.StockCode, companyIndex));
            HttpItem item = new HttpItem()
            {
                URL = string.Format(this.UrlModel.Url, companyModel.StockBourse.ToString().ToLower() + companyModel.StockCode, 40000, dealDate),
                ContentType = "json"
            };
            string jsonData = new HttpHelper().GetHtml(item);
            if (jsonData.ToLower().Contains("error"))
            {
                logger.Error("接口返回错误代码：" + jsonData);
                return;
            }

            var bigDealSumModels = JsonConvert.DeserializeObject<List<BigDealSumModel>>(jsonData);
            //未请求到数据，中断处理
            if (bigDealSumModels == null || bigDealSumModels.Count == 0)
                return;

            var bigDealSumModel = bigDealSumModels[0];
            if (dataAccess.HasExist(bigDealSumModel))
            {
                dataAccess.UpdateBigDealSumRecord(bigDealSumModel);
            }
            else
            {
                dataAccess.AddBigDealSumRecord(bigDealSumModel);
            }
        }
    }
}
