using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diss.Core.Models
{
    [Table("mailboxer_notifications")]
    public partial class mailboxer_notifications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mailboxer_notifications()
        {
            mailboxer_receipts = new HashSet<mailboxer_receipts>();
        }

        public int id { get; set; }

        [StringLength(8000)]
        public string type { get; set; }

        public string body { get; set; }

        [StringLength(8000)]
        public string subject { get; set; }

        [StringLength(8000)]
        public string sender_type { get; set; }

        public int? sender_id { get; set; }

        public int? conversation_id { get; set; }

        public bool? draft { get; set; }

        [StringLength(8000)]
        public string notification_code { get; set; }

        [StringLength(8000)]
        public string notified_object_type { get; set; }

        public int? notified_object_id { get; set; }

        [StringLength(8000)]
        public string attachment { get; set; }

        public DateTime updated_at { get; set; }

        public DateTime created_at { get; set; }

        public bool? global { get; set; }

        public DateTime? expires { get; set; }

        public virtual mailboxer_conversations mailboxer_conversations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mailboxer_receipts> mailboxer_receipts { get; set; }
    }
}
