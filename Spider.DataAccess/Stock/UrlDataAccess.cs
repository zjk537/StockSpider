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
                                Remark,
                                Handle,
                                CreatedDate
                            ) values (
                                @Url,
                                @Type,
                                1,
                                @Remark,
                                @Handle,
                                @CreatedDate
                            )";
            object[] sqlParams = new object[]
                {
                   urlModel.Url,
                   (int)urlModel.UrlType,
                   urlModel.Remark,
                   urlModel.Handle,
                   DateTime.Now.ToString("yyyy-MM-ss hh:mm:ss")
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

        public void UpdateUrl(SourceUrlModel urlModel)
        {
            string sql = @"update SourceUrl 
                           set  Url = @Url,
                                Type = @UrlType,
                                Remark = @Remark,
                                Handle = @Handle
                           where 
                            UrlId = @id;";
            object[] sqlParams = new object[]
                {
                   urlModel.Url,
                   (int)urlModel.UrlType,
                   urlModel.Remark,
                   urlModel.Handle,
                   urlModel.UrlId
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
                                Remark,
                                Handle,
                                CreatedDate
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
                    Remark = reader["Remark"].ToString(),
                    Handle = reader["Handle"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
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
                                Remark,
                                Handle,
                                CreatedDate
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
                    Remark = reader["Remark"].ToString(),
                    Handle = reader["Handle"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                });
            }
            return sourceUrls;
        }

        public void UpdateWorkingUrlStatus(int state)
        {
            string sql = @"update SourceUrl 
                           set State = @State
                           where 
                            State = 2;";
            object[] sqlParams = new object[]
                {
                   state
                };
            SQLiteHelper.Instance.ExecuteNonQuery(sql, sqlParams);
        }
    }
}
