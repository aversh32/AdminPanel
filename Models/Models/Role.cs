using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("roles")]
    public class Role : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
