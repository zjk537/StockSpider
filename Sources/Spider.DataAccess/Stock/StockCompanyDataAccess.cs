using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spider.Models.Stock;

namespace Spider.DataAccess.Stock
{
    public class StockCompanyDataAccess
    {

        public void AddCompanies(IEnumerable<StockCompanyModel> companyList)
        {
            string sql = @"insert into StockCompany (
                                StockCode,
                                StockBourse,
                                CompanyName,
                                CompanyNature,
                                CreatedDate
                            ) values (
                                @StockCode,
                                @StockBourse,
                                @CompanyName,
                                @CompanyNature,
                                @CreatedDate
                            )";
            SQLiteHelper.Instance.AppendTransaction();

            foreach (StockCompanyModel company in companyList)
            {
                object[] sqlParams = new object[]
                {
                   company.StockCode,
                   (int)company.StockBourse,
                   company.CompanyName,
                   company.CompanyNature,
                   company.CreatedDate.ToString("yyyy-MM-dd")
                };
                SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
            }

            SQLiteHelper.Instance.CommitTransaction();
        }

        public string[] FilterExistCodes(string[] stockCodes)
        {
            string sql = @"select DISTINCT(StockCode) from StockCompany 
                            where StockCode in (" + string.Format("'{0}'", string.Join("','", stockCodes)) + ")";
            List<string> resultCodes = new List<string>();
            using (var reader = SQLiteHelper.Instance.ExecuteReader(sql))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string stockCode = reader["StockCode"].ToString();
                        if (!stockCodes.Contains(stockCode))
                        {
                            resultCodes.Add(stockCode);
                        }
                    }
                }
                else
                {
                    resultCodes = stockCodes.ToList();
                }

            }
            return resultCodes.ToArray();
        }

        /// <summary>
        /// 判断单个StockCode 是否已存在数据库中
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public bool IsExist(string stockCode)
        {
            string sql = @"select StockCode from StockCompany 
                            where StockCode = '@StockCode'";
            object[] sqlParams = new object[]
            {
                stockCode
            };
            var result = SQLiteHelper.Instance.ExecuteScalar(sql, sqlParams);
            if (result == null || result is DBNull)
            {
                return false;
            }
            return true;
        }

        public List<StockCompanyModel> GetAllCompanies()
        {
            string sql = @"select CompanyId, 
                                StockCode,
                                StockBourse,
                                CompanyName,
                                CompanyNature,
                                CreatedDate
                            from StockCompany";

            List<StockCompanyModel> companies = new List<StockCompanyModel>();
            using (var reader = SQLiteHelper.Instance.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    companies.Add(new StockCompanyModel()
                    {
                        CompanyId = Convert.ToInt32(reader["CompanyId"].ToString()),
                        StockCode = reader["StockCode"].ToString(),
                        StockBourse = (BourseType)Convert.ToInt32(reader["StockBourse"]),
                        CompanyName = reader["CompanyName"].ToString(),
                        CompanyNature = reader["CompanyNature"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                    });
                }
            }
            return companies;
        }
    }
}
