using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Models.Stock
{
    public class DealModel
    {
        public int Id { get; set; }
        public string StockCode { get; set; }
        public float Price { get; set; }
        public int Volume { get; set; }
        public float PrevPrice { get; set; }
        public string DealType { get; set; }
        public string TickTime { get; set; }
        public DateTime DealDate { get; set; }

    }
}
