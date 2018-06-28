using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("user_roles")]
    public class UserRole : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
