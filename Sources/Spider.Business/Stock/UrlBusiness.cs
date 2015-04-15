using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spider.DataAccess.Stock;
using Spider.Models.Stock;
using Spider.Common;
using Spider.Common.Enums;

namespace Spider.Business.Stock
{
    public class UrlBusiness
    {
        UrlDataAccess dataAccess = new UrlDataAccess();

        public void AddUrl(SourceUrlModel urlModel)
        {
            dataAccess.AddUrl(urlModel);
        }
        /// <summary>
        /// get all urls
        /// </summary>
        /// <returns></returns>
        public List<SourceUrlModel> GetSourceUrls()
        {
            return dataAccess.GetSourceUrls();
        }

        public void UpdateUrlStatus(int urlId, ProcessState state)
        {
            dataAccess.UpdateUrlState(urlId, (int)state);
        }

        /// <summary>
        /// update all processing and abort urls
        /// </summary>
        public void UpdateWorkingUrlStatus()
        {
            dataAccess.UpdateWorkingUrlStatus();
        }
    }
}
