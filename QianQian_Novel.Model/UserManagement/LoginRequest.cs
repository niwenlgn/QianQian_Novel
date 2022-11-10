using QianQian_Novel.MyUtility.AttributeRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Model.UserManagement
{
    public class LoginRequest
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空")]
        public string? Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string? Password { get; set; }


        /// <summary>
        /// 验证码
        /// </summary>
        [HiddenField]
        public string? Code { get; set; }
    }
}
