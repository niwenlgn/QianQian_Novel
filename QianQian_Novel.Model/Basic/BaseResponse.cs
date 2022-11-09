using QianQian_Novel.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Model.Basic
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public BaseStatusCode Code { get; set; } = BaseStatusCode.Success;

        /// <summary>
        /// 执行结果
        /// </summary>
        public string Result
        {
            get {
                return Code.ToString("g");
            }
        }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; } = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T> : BaseResponse
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public long Count { get; set; } = 0;
    }

}
