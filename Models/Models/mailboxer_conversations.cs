using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diss.Core.Models
{
    [Table("mailboxer_conversations")]
    public partial class mailboxer_conversations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mailboxer_conversations()
        {
            mailboxer_conversation_opt_outs = new HashSet<mailboxer_conversation_opt_outs>();
            mailboxer_notifications = new HashSet<mailboxer_notifications>();
        }

        public int id { get; set; }

        [StringLength(8000)]
        public string subject { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mailboxer_conversation_opt_outs> mailboxer_conversation_opt_outs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mailboxer_notifications> mailboxer_notifications { get; set; }
    }
}
