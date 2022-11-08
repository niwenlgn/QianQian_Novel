using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QianQian_Novel.Entity.Entity
{
    [Table("base_user")]
    public class BaseUser
    {
        [Key]
        [Column("userid")]
        public long Userid { get; set; }

        [Column("username")]
        public string UserName { get; set; } = null!;

        [Column("age")]
        public int Age { get; set; }

        [Column("sex")]
        public bool Sex { get; set; }
    }
}
