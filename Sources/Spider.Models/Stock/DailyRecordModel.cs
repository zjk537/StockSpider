using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Models.Stock
{
    public class DailyRecordModel
    {
        public int Id { get; set; }

        public string StockCode { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 当前价格
        /// </summary>
        public decimal Now { get; set; }
        /// <summary>
        /// 今天开价
        /// </summary>
        public decimal Open { get; set; }
        /// <summary>
        /// 最高
        /// </summary>
        public decimal High { get; set; }
        /// <summary>
        /// 最低
        /// </summary>
        public decimal Low { get; set; }
        /// <summary>
        /// 昨收
        /// </summary>
        public decimal PreClose { get; set; }
        /// <summary>
        /// 涨跌额
        /// </summary>
        public decimal Change { get; set; }

        /// <summary>
        /// 涨跌幅
        /// </summary>
        public decimal ChangeP { get; set; }

        /// <summary>
        /// 当日成交量
        /// </summary>
        public long Volume { get; set; }

        /// <summary>
        /// 当日成效额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime DealDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
