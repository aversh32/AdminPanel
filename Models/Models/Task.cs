using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("tasks")]
    public class Task : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("alternatives")]
        public string Alternatives { get; set; }
        [Column("math_data")]
        public string MathData { get; set; }

        [Column("created_at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [Column("must_completed_at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpirationDate { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }
        [Column("domain_id")]
        public int DomainId { get; set; }

        public Status Status { get; set; }
        public Domain Domain { get; set; }

        public ICollection<Query> Queries { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
    }
}
