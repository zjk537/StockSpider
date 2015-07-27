using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spider.Models.Stock;

namespace Spider.DataAccess.Stock
{
    public class NoticeDataAccess
    {
        public void AddNotices(List<NoticeModel> noticeModels)
        {
            string sqlAdd = @"insert into Notice (
                                Id,
                                StockCode,
                                Title,
                                Date
                            ) values (
                                @Id,
                                @StockCode,
                                @Title,
                                @Date
                            )";
            string sqlSelect = @"select NoticeId from Notice where Id = @Id";
            SQLiteHelper.Instance.AppendTransaction();


            foreach (NoticeModel notice in noticeModels)
            {
                int noticeId = Convert.ToInt32(SQLiteHelper.Instance.ExecuteScalar(sqlSelect, new object[] { notice.Id }));
                if (noticeId == 0)
                {
                    object[] sqlParams = new object[]
                    {
                       notice.Id,
                       notice.StockCode,
                       notice.Title,
                       notice.Date
                    };
                    SQLiteHelper.Instance.ExecuteNonQuery(sqlAdd, sqlParams);
                }
            }
            SQLiteHelper.Instance.CommitTransaction();
        }

        public List<NoticeModel> GetNotices()
        {
            string sql = @"select NoticeId
                                ,Id
                                ,StockCode
                                ,Title
                                ,Date
                            from Notice";
            List<NoticeModel> notices = new List<NoticeModel>();
            var reader = SQLiteHelper.Instance.ExecuteReader(sql);
            while (reader.Read())
            {
                notices.Add(new NoticeModel()
                {
                    NoticeId = Convert.ToInt32(reader["NoticeId"]),
                    Id = Convert.ToInt32(reader["Id"]),
                    StockCode = reader["StockCode"].ToString(),
                    Title = reader["Title"].ToString(),
                    Date = reader["Date"].ToString()
                });
            }

            return notices;
        }

        public List<NoticeModel> GetLastNotices()
        {
            string sql = @"select NoticeId
                                ,Id
                                ,StockCode
                                ,Title
                                ,Date
                            from Notice
                            where Date >= date()
                            order by Date desc
                        ";
            List<NoticeModel> notices = new List<NoticeModel>();
            var reader = SQLiteHelper.Instance.ExecuteReader(sql);
            while (reader.Read())
            {
                notices.Add(new NoticeModel()
                {
                    NoticeId = Convert.ToInt32(reader["NoticeId"]),
                    Id = Convert.ToInt32(reader["Id"]),
                    StockCode = reader["StockCode"].ToString(),
                    Title = reader["Title"].ToString(),
                    Date = reader["Date"].ToString()
                });
            }

            return notices;
        }
    }
}
