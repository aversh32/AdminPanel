using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("task_types")]
    public partial class task_types : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(8000)]
        public string key { get; set; }

        [Required]
        [StringLength(8000)]
        public string title { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }
    }
}
