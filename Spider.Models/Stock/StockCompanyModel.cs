using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Models.Stock
{
    public class StockCompanyModel
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public int CompanyId { get; set; }
        public string StockCode { get; set; }
        /// <summary>
        /// 交易所：1--上海 2--深圳
        /// </summary>
        public BourseType StockBourse { get; set; }
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>
        public string CompanyNature { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
