using System;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("change_requests")]
    public partial class change_requests : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }

        public int? user_id { get; set; }

        public int? task_id { get; set; }

        public int status { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public virtual User users { get; set; }

        public virtual Diss.Core.Models.Task tasks { get; set; }
    }
}
