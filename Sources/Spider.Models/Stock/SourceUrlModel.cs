using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Models.Stock
{
    public class SourceUrlModel
    {
        public int UrlId { get; set; }
        public string Url { get; set; }
        public UrlType UrlType { get; set; }
        public int State { get; set; }
        public string Remark { get; set; }
    }
}
