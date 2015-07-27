using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spider.Models.Stock
{
    public class NoticeModel
    {
        [JsonIgnore]
        public int NoticeId { get; set; }

        public int Id { get; set; }

        [JsonIgnore]
        public string StockCode { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }
    }
}
