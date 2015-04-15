using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Models.Stock
{
    public enum UrlType
    {
        [UrlDesc("基础数据")]
        BasicUrl = 1,
        [UrlDesc("交易数据")]
        TradeUrl = 2,
        [UrlDesc("公司消息")]
        NoticeUrl = 3,
        [UrlDesc("大单交易名细")]
        DealDetailUrl = 4
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
