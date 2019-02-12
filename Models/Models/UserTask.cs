using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Core;

namespace Diss.Core.Models
{
    [Table("user_tasks")]
    public class UserTask: IDbEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("task_id")]
        public int TaskId { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("status")]
        public int StatusId { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public Task Task { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
        public Status Status { get; set; }
    }
}
