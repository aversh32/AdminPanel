using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diss.Core.Models
{
    [Table("ar_internal_metadata")]
    public partial class ar_internal_metadata
    {
        [Key]
        [StringLength(8000)]
        public string key { get; set; }

        [StringLength(8000)]
        public string value { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }
    }
}
