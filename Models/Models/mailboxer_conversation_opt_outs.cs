using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diss.Core.Models
{
    [Table("mailboxer_conversation_opt_outs")]
    public partial class mailboxer_conversation_opt_outs
    {
        public int id { get; set; }

        [StringLength(8000)]
        public string unsubscriber_type { get; set; }

        public int? unsubscriber_id { get; set; }

        public int? conversation_id { get; set; }

        public virtual mailboxer_conversations mailboxer_conversations { get; set; }
    }
}
