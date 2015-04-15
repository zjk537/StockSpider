using Spider.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.DataAccess.Stock
{
    public class DealRecordDataAccess
    {
        public void AddDealRecord(List<DealModel> models)
        {
            string sqlAdd = @"insert into DealRecord (
                                StockCode,
                                Price,
                                Volume,
                                PrevPrice,
                                DealType,
                                TickTime,
                                DealDate
                            ) values (
                                @StockCode,
                                @Price,
                                @Volume,
                                @PrevPrice,
                                @DealType,
                                @TickTime,
                                @DealDate
                            )";
            SQLiteHelper.Instance.AppendTransaction();

            foreach (DealModel model in models)
            {
                object[] sqlParams = new object[]
                {
                    model.StockCode,
                    model.Price,
                    model.Volume,
                    model.PrevPrice,
                    model.DealType,
                    model.TickTime,
                    model.DealDate.ToString("yyyy-MM-dd")
                };
                SQLiteHelper.Instance.ExecuteNonQuery(sqlAdd, sqlParams);
            }
            SQLiteHelper.Instance.CommitTransaction();
        }
    }
}
