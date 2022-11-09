using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Model.UserManagement
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Userid { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// 用户年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; } = null!;
    }
}
