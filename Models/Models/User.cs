using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("users")]
    public class User : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("first_name")]
        public string Name { get; set; }
        [Required]
        [Column("last_name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        [DataType(DataType.Password)]
        [Column("encrypted_password")]
        public string Password { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserDomain> UserDomains { get; set; }
    }
}
