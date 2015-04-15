using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spider.Models.Stock;

namespace Spider.DataAccess.Stock
{
    public class UrlDataAccess
    {
        public void AddUrl(SourceUrlModel urlModel)
        {
            string sql = @"insert into SourceUrl (
                                Url,
                                Type,
                                state,
                                Remark
                            ) values (
                                @Url,
                                @Type,
                                1,
                                @Remark
                            )";
            object[] sqlParams = new object[]
                {
                   urlModel.Url,
                   (int)urlModel.UrlType,
                   urlModel.Remark
                };
            SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
        }

        public void UpdateUrlState(int urlId, int state)
        {
            string sql = @"update SourceUrl 
                           set State = @State
                           where 
                            UrlId = @id;";
            object[] sqlParams = new object[]
                {
                   state,
                   urlId
                };
            SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
        }

        /// <summary>
        /// get urls byt url type
        /// </summary>
        /// <param name="urlType"></param>
        /// <returns></returns>
        public List<SourceUrlModel> GetSourceUrls(UrlType urlType)
        {
            string sql = @"select UrlId,
                                Url,
                                Type,
                                State,
                                Remark
                            from SourceUrl
                            where
                            Type = @Type";
            object[] sqlParams = new object[]
                {
                   (int)urlType
                };
            var reader = SQLiteHelper.Instance.ExecuteReader(sql, sqlParams);
            List<SourceUrlModel> sourceUrls = new List<SourceUrlModel>();
            while (reader.Read())
            {
                sourceUrls.Add(new SourceUrlModel()
                {
                    UrlId = Convert.ToInt32(reader["UrlId"]),
                    Url = reader["Url"].ToString(),
                    UrlType = (UrlType)Convert.ToInt32(reader["Type"]),
                    State = Convert.ToInt32(reader["State"]),
                    Remark = reader["Remark"].ToString()
                });
            }
            return sourceUrls;
        }

        /// <summary>
        ///  get all urls 
        /// </summary>
        /// <returns></returns>
        public List<SourceUrlModel> GetSourceUrls()
        {
            string sql = @"select UrlId,
                                Url,
                                Type,
                                State,
                                Remark
                            from SourceUrl";
            var reader = SQLiteHelper.Instance.ExecuteReader(sql);
            List<SourceUrlModel> sourceUrls = new List<SourceUrlModel>();
            while (reader.Read())
            {
                sourceUrls.Add(new SourceUrlModel()
                {
                    UrlId = Convert.ToInt32(reader["UrlId"]),
                    Url = reader["Url"].ToString(),
                    UrlType = (UrlType)Convert.ToInt32(reader["Type"]),
                    State = Convert.ToInt32(reader["State"]),
                    Remark = reader["Remark"].ToString()
                });
            }
            return sourceUrls;
        }

        public void UpdateWorkingUrlStatus()
        {
            string sql = @"update SourceUrl 
                           set State = 3
                           where 
                            State = 2;";
            
            SQLiteHelper.Instance.ExecuteNonQuery(sql);
        }
    }
}
