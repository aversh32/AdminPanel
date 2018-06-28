using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diss.Core.Models
{
    [Table("mailboxer_receipts")]
    public partial class mailboxer_receipts
    {
        public int id { get; set; }

        [StringLength(8000)]
        public string receiver_type { get; set; }

        public int? receiver_id { get; set; }

        public int notification_id { get; set; }

        public bool? is_read { get; set; }

        public bool? trashed { get; set; }

        public bool? deleted { get; set; }

        [StringLength(25)]
        public string mailbox_type { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public bool? is_delivered { get; set; }

        [StringLength(8000)]
        public string delivery_method { get; set; }

        [StringLength(8000)]
        public string message_id { get; set; }

        public virtual mailboxer_notifications mailboxer_notifications { get; set; }
    }
}
