using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Common.Enums
{
    public enum ProcessState
    {
        /// <summary>
        /// 开始
        /// </summary>
        Start = 1,

        /// <summary>
        /// 采集中...
        /// </summary>
        Processing = 2,

        /// <summary>
        /// 中止
        /// </summary>
        Abort = 3,

        /// <summary>
        /// 完成
        /// </summary>
        Complete = 4,

        /// <summary>
        /// 数据错误
        /// </summary>
        Error = 5
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDescAttribute : Attribute
    {
        public string Description { get; set; }

        public EnumDescAttribute(string desc)
        {
            this.Description = desc;
        }
    }
}
