using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("user_ratings")]
    public partial class user_ratings : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }

        public int? task_id { get; set; }

        public int? user_id { get; set; }

        [StringLength(8000)]
        public string comment { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public int count_of_changes { get; set; }

        public virtual Task tasks { get; set; }

        public virtual User users { get; set; }
    }
}
