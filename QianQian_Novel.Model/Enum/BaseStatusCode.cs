using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Model.Enum
{
    public enum BaseStatusCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 警告
        /// </summary>
        Warn,
        /// <summary>
        /// 无权限
        /// </summary>
        NoAccess,
        /// <summary>
        /// 密码过期
        /// </summary>
        PasswordAging,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 9,
        /// <summary>
        /// 未知
        /// </summary>
        Unknow
    }
}
