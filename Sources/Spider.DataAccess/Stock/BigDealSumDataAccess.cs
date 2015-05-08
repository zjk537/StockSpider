using Spider.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.DataAccess.Stock
{
    public class BigDealSumDataAccess
    {
        public void AddBigDealSumRecord(BigDealSumModel model)
        {

            string sql = @"INSERT INTO BigDealSum (
                                    StockCode,
                                    Name,
                                    TotalVol,
                                    TotalVolPCT,
                                    TotalAmt,
                                    TotalAmtPCT,
                                    AvgPrice,
                                    KuVolume,
                                    KuAmount,
                                    KeVolume,
                                    KeAmount,
                                    KdVolume,
                                    KdAmount,
                                    StockVol,
                                    StockAmt,
                                    DealDate,
                                    CreatedDate
                                ) VALUES (
                                    @StockCode,
                                    @Name,
                                    @TotalVol,
                                    @TotalVolPCT,
                                    @TotalAmt,
                                    @TotalAmtPCT,
                                    @AvgPrice,
                                    @KuVolume,
                                    @KuAmount,
                                    @KeVolume,
                                    @KeAmount,
                                    @KdVolume,
                                    @KdAmount,
                                    @StockVol,
                                    @StockAmt,
                                    @DealDate,
                                    @CreatedDate)";

            object[] sqlParams = new object[]
                {
                    model.StockCode.Substring(2),
                    model.Name,
                    model.TotalVol,
                    model.TotalVolPCT,
                    model.TotalAmt,
                    model.TotalAmtPCT,
                    model.AvgPrice,
                    model.KuVolume,
                    model.KuAmount,
                    model.KeVolume,
                    model.KeAmount,
                    model.KdVolume,
                    model.KdAmount,
                    model.StockVol,
                    model.StockAmt,
                    model.DealDate,
                    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                };
            SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
        }

        /// <summary>
        /// 是否已同步过当天的数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool HasExist(BigDealSumModel model)
        {
            string sql = @"SELECT count(0) 
                            FROM BigDealSum
                            WHERE StockCode = @StockCode 
                            AND DealDate = @DealDate";
            object[] sqlParams = new object[]
                {
                    model.StockCode.Substring(2),
                    model.DealDate
                };
            var reslt = SQLiteHelper.Instance.ExecuteScalar(sql, sqlParams);
            if (reslt == null || reslt is DBNull)
            {
                return false;
            }

            return Convert.ToInt32(reslt) > 0;
        }

        public void UpdateBigDealSumRecord(BigDealSumModel model)
        {
            string sql = @"UPDATE BigDealSum SET 
                TotalVol = @TotalVol,
                TotalVolPCT  = @TotalVolPCT,
                TotalAmt  = @TotalAmt,
                TotalAmtPCT  = @TotalAmtPCT,
                AvgPrice  = @AvgPrice,
                KuVolume  = @KuVolume,
                KuAmount = @KuAmount,
                KeVolume = @KeVolume,
                KeAmount = @KeAmount,
                KdVolume = @KdVolume,
                KdAmount = @KdAmount,
                StockVol = @StockVol,
                StockAmt = @StockAmt
                WHERE 
                StockCode = @StockCode
                AND DealDate = @DealDate";
            object[] sqlParams = new object[]
                {
                    model.Name,
                    model.TotalVol,
                    model.TotalVolPCT,
                    model.TotalAmt,
                    model.TotalAmtPCT,
                    model.AvgPrice,
                    model.KuVolume,
                    model.KuAmount,
                    model.KeVolume,
                    model.KeAmount,
                    model.KdVolume,
                    model.KdAmount,
                    model.StockVol,
                    model.StockAmt,
                    model.StockCode.Substring(2),
                    model.DealDate
                };

            SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
        }
    }
}
