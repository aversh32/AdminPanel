using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("ckeditor_assets")]
    public partial class ckeditor_assets : IDbEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(8000)]
        public string data_file_name { get; set; }

        [StringLength(8000)]
        public string data_content_type { get; set; }

        public int? data_file_size { get; set; }

        [StringLength(8000)]
        public string data_fingerprint { get; set; }

        [StringLength(30)]
        public string type { get; set; }

        public int? width { get; set; }

        public int? height { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }
    }
}
