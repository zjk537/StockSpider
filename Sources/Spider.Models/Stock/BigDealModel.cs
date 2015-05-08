using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spider.Models.Stock
{
    public class BigDealModel
    {
        public int Id { get; set; }
        [JsonProperty("symbol")]
        public string StockCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long Volume { get; set; }
        [JsonProperty("prev_price")]
        public decimal PrevPrice { get; set; }
        [JsonProperty("kind")]
        public string DealType { get; set; }
        public string TickTime { get; set; }
        public DateTime DealDate { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
