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
    public class DailyRecordBusiness : BusinessBase
    {
        DailyRecordDataAccess dataAccess = new DailyRecordDataAccess();
        public override void SpideData(int companyIndex)
        {
            string dealDate = DateTime.Now.ToString("yyyy-MM-dd");
            var companyModel = base.CompanyList[companyIndex];
            SyncUILog(ProcessState.Processing, string.Format("开始下载 {0}【{1}】的公司大单汇总数据...-- {2}", companyModel.CompanyName, companyModel.StockCode, companyIndex));
            HttpItem item = new HttpItem()
            {
                URL = string.Format(this.UrlModel.Url, companyModel.StockBourse.ToString().ToLower() + companyModel.StockCode),
                ContentType = "json"
            };
            string jsonData = new HttpHelper().GetHtml(item);
            if (jsonData.ToLower().Contains("error"))
            {
                logger.Error("接口返回错误代码：" + jsonData);
                return;
            }
            //name:open:preClose:now:hight:low:买1价格:买2价格:volume:amount:买1数量:买1价格:买2数量:买2价格:买3数量:买3价格:买4数量:买4价格:买5数量:买5价格:卖1数量:卖1价格:卖2数量:卖2价格:卖3数量:卖3价格:卖4数量:卖4价格:卖5数量:卖5价格:日期:时间
            string newString = jsonData.Substring(jsonData.IndexOf("=\"") + 2).TrimEnd('\"');
            var dataArr = newString.Split(',');
            DailyRecordModel model = new DailyRecordModel()
            {
                Name = dataArr[0],
                StockCode = companyModel.StockCode,
                Open = Convert.ToDecimal(dataArr[1]),
                PreClose = Convert.ToDecimal(dataArr[2]),
                Now = Convert.ToDecimal(dataArr[3]),
                High = Convert.ToDecimal(dataArr[4]),
                Low = Convert.ToDecimal(dataArr[5]),
                Volume = Convert.ToInt64(dataArr[8]),
                Amount = Convert.ToDecimal(dataArr[9]),
                DealDate = Convert.ToDateTime(dataArr[30])
            };
            model.Change = model.Now - model.PreClose;
            model.ChangeP = Math.Round(model.Change / model.PreClose, 4);

            if (dataAccess.HasExist(model))
            {
                dataAccess.UpdateDailyRecord(model);
            }
            else
            {
                dataAccess.AddDailyRecord(model);
            }

        }
    }
}
