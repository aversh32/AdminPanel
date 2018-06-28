using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("alternatives")]
    public partial class alternatives : IDbEntity
    {

        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(8000)]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        public int? task_id { get; set; }

        public int category { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public virtual Task tasks { get; set; }
    }
}
