using QianQian_Novel.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Model.Basic
{
    public class BaseResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public BaseStatusCode Code { get; set; }

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

    public class BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; }

        public long Count { get; set; }
    }

}
