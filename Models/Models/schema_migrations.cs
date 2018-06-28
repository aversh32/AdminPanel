using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diss.Core.Models
{
    [Table("schema_migrations")]
    public partial class schema_migrations
    {
        [Key]
        [StringLength(8000)]
        public string version { get; set; }
    }
}
