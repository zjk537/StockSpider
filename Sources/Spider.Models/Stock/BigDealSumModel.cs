using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spider.Models.Stock
{
    public class BigDealSumModel
    {
        public int Id { get; set; }

        [JsonProperty("symbol")]
        public string StockCode { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 大单总成交量
        /// </summary>
        public long TotalVol { get; set; }

        /// <summary>
        /// 大单总成交量占比
        /// </summary>
        public decimal TotalVolPCT { get; set; }

        /// <summary>
        /// 大单总成交额
        /// </summary>
        public long TotalAmt { get; set; }

        /// <summary>
        /// 大单总成交额占比
        /// </summary>
        public decimal TotalAmtPCT { get; set; }

        /// <summary>
        /// 大单平均成交价
        /// </summary>
        public decimal AvgPrice { get; set; }

        /// <summary>
        /// 主买量
        /// </summary>
        public long KuVolume { get; set; }

        /// <summary>
        /// 主买盘总额
        /// </summary>
        public decimal KuAmount { get; set; }

        /// <summary>
        /// 中性量
        /// </summary>
        public long KeVolume { get; set; }

        /// <summary>
        /// 中性盘总额
        /// </summary>
        public long KeAmount { get; set; }

        /// <summary>
        /// 主卖量
        /// </summary>
        public long KdVolume { get; set; }

        /// <summary>
        /// 主卖盘总额
        /// </summary>
        public long KdAmount { get; set; }

        /// <summary>
        /// 总成交量
        /// </summary>
        public long StockVol { get; set; }

        /// <summary>
        /// 总成交额
        /// </summary>
        public long StockAmt { get; set; }

        [JsonProperty("opendate")]
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime DealDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
