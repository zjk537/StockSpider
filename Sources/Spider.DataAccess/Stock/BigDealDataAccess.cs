using Spider.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.DataAccess.Stock
{
    public class BigDealDataAccess
    {
        public void AddBigDealRecord(List<BigDealModel> models, string dealDate)
        {
            string sqlAdd = @"insert into BigDealRecord (
                                StockCode,
                                Name,
                                Price,
                                Volume,
                                PrevPrice,
                                DealType,
                                TickTime,
                                DealDate,
                                CreatedDate
                            ) values (
                                @StockCode,
                                @Name,
                                @Price,
                                @Volume,
                                @PrevPrice,
                                @DealType,
                                @TickTime,
                                @DealDate,
                                @CreatedDate
                            )";
            SQLiteHelper.Instance.AppendTransaction();

            foreach (BigDealModel model in models)
            {
                object[] sqlParams = new object[]
                {
                    model.StockCode.Substring(2),
                    model.Name,
                    model.Price,
                    model.Volume,
                    model.PrevPrice,
                    model.DealType.ToUpper(),
                    model.TickTime,
                    dealDate,
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                };
                SQLiteHelper.Instance.ExecuteNonQuery(sqlAdd, sqlParams);
            }
            SQLiteHelper.Instance.CommitTransaction();
        }

        public BigDealModel GetMaxTickTime(string stockCode, string dealDate)
        {
            string sql = @"select 
                            Volume,                           
                           max(TickTime) TickTime
                            from BigDealRecord 
                            where StockCode = @StockCode and DealDate = @DealDate";
            object[] sqlParams = new object[]
            {
                stockCode,
                dealDate
            };
            BigDealModel model = null;
            using (var reader = SQLiteHelper.Instance.ExecuteReader(sql, sqlParams))
            {
                reader.Read();
                if (reader["TickTime"] is DBNull)
                {
                    model = null;
                }
                else
                {
                    model = new BigDealModel()
                    {
                        Volume = Convert.ToInt32(reader["Volume"]),
                        TickTime = reader["TickTime"].ToString()
                    };
                }
            }

            return model;
        }
    }
}
