using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("user_domains")]
    public class UserDomain: IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("domain_id")]
        public int DomainId { get; set; }

        [Column("competition_coef", TypeName = "decimal(4, 3)")]
        public decimal CompetitionCoef { get; set; }

        public User User { get; set; }
        public Domain Domain { get; set; }
    }
}
