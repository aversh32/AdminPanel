using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("domains")]
    public class Domain : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public ICollection<UserDomain> UserDomains { get; set; }
    }
}
