using Spider.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.DataAccess.Stock
{
    public class DailyRecordDataAccess
    {
        public void AddDailyRecord(DailyRecordModel model)
        {
            string sql = @"INSERT INTO DailyRecord (
                            StockCode,
                            Name,
                            Now,
                            Open,
                            High,
                            Low,
                            PreClose,
                            Change,
                            ChangeP,
                            Volume,
                            Amount,
                            DealDate,
                            CreatedDate
                            ) VALUES (
                            @StockCode,
                            @Name,
                            @Now,
                            @Open,
                            @High,
                            @Low,
                            @PreClose,
                            @Change,
                            @ChangeP,
                            @Volume,
                            @Amount,
                            @DealDate,
                            @CreatedDate
                            )";
            object[] sqlParams = new object[]
                {
                    model.StockCode,
                    model.Name,
                    model.Now,
                    model.Open,
                    model.High,
                    model.Low,
                    model.PreClose,
                    model.Change,
                    model.ChangeP,
                    model.Volume,
                    model.Amount,
                    model.DealDate.ToString("yyyy-MM-dd"),
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                };
            SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
        }

        public bool HasExist(DailyRecordModel model)
        {
            string sql = @"SELECT count(0) 
                            FROM DailyRecord
                            WHERE 
                            StockCode = @StockCode
                            AND DealDate = @DealDate";
            object[] sqlParams = new object[]
                {
                    model.StockCode,
                    model.DealDate.ToString("yyyy-MM-dd")
                };
            var result = SQLiteHelper.Instance.ExecuteScalar(sql, sqlParams);
            if (result == null || result is DBNull)
            {
                return false;
            }
            return Convert.ToInt32(result) > 0;
        }

        public void UpdateDailyRecord(DailyRecordModel model)
        {
            string sql = @"UPDATE DailyRecord SET 
                            Now = @Now,
                            Open = @Open,
                            High = @High,
                            Low = @Low,
                            PreClose = @PreClose,
                            Change = @Change,
                            ChangeP = @ChangeP,
                            Volume = @Volume,
                            Amount = @Amount
                            WHERE
                            StockCode = @StockCode 
                            AND DealDate = @DealDate";
            object[] sqlParams = new object[]
                {
                    model.Now,
                    model.Open,
                    model.High,
                    model.Low,
                    model.PreClose,
                    model.Change,
                    model.ChangeP,
                    model.Volume,
                    model.Amount,
                    model.StockCode,
                    model.DealDate.ToString("yyyy-MM-dd")
                };
            SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
        }
    }
}
