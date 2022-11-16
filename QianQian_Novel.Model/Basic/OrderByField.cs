using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Model.Basic
{
    /// <summary>
    /// 排序
    /// </summary>
    public class OrderByField
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; } = null!;

        /// <summary>
        /// 排序方式
        /// </summary>
        public OrderType OrderBy { get; set; }

        /// <summary>
        /// 排序类别
        /// </summary>
        public enum OrderType
        {
            Default = 0,
            Asc = 1,
            Desc = 2,
        }
    }
}
