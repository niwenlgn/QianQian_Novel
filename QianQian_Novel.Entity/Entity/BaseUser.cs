using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Entity.Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("base_user")]
    public class BaseUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        [Column("userid")]
        public long Userid { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("username")]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// 用户年龄
        /// </summary>
        [Column("age")]
        public int Age { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        [Column("sex")]
        public bool Sex { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; } = null!;
    }
}
