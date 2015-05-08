using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spider.Models.Stock;

namespace Spider.DataAccess.Stock
{
    public class BaseDataAccess
    {
        public void TruncateTables(string[] tableNames)
        {
            string sqlTmpl = @"delete from {0};
                        update sqlite_sequence set seq=0 where name='{0}'";
            SQLiteHelper.Instance.AppendTransaction();

            foreach (string tableName in tableNames)
            {
                string sql = string.Format(sqlTmpl, tableName);
                SQLiteHelper.Instance.ExecuteNonQuery(sql);
            }

            SQLiteHelper.Instance.CommitTransaction();
        }
    }
}
