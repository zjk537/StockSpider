using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Models.Stock
{
    public enum UrlType
    {
        /// <summary>
        /// 基础数据
        /// </summary>
        [UrlDesc("基础数据")]
        BasicUrl = 1,
        /// <summary>
        /// 实时数据
        /// </summary>
        [UrlDesc("实时数据")]
        TradeUrl = 2,
        /// <summary>
        /// 公司消息
        /// </summary>
        [UrlDesc("公司消息")]
        NoticeUrl = 3,
        /// <summary>
        /// 大单交易
        /// </summary>
        [UrlDesc("大单交易")]
        BigDealUrl = 4
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class UrlDescAttribute : Attribute
    {
        public string Description { get; set; }

        public UrlDescAttribute(string desc)
        {
            this.Description = desc;
        }
    }
}
