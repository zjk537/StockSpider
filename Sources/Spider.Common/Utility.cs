using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spider.Common
{
    public static class Utility
    {
        public static string GetQueryStringValue(string url, string argName)
        {
            var paraReg = "(^|&|\\?)" + argName + "=([^&]*)(&|$)";
            Regex reg = new Regex(paraReg);
            var match = reg.Match(url);
            if (match.Success)
            {
                var str = match.Groups[2].Value;
                return str;
            }
            return "";
        }
    }
}
