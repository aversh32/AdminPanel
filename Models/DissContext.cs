using Diss.Core.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace Diss.Core
{
    public class DissContext : DbContext
    {
        public DissContext(DbContextOptions<DissContext> options) : base(options)
        {

        }

        public virtual DbSet<alternatives> Alternatives { get; set; }
        public virtual DbSet<ar_internal_metadata> ar_internal_metadata { get; set; }
        public virtual DbSet<change_requests> Change_requests { get; set; }
        public virtual DbSet<ckeditor_assets> ckeditor_assets { get; set; }
        public virtual DbSet<Domain> Domains { get; set; }
        public virtual DbSet<mailboxer_conversation_opt_outs> mailboxer_conversation_opt_outs { get; set; }
        public virtual DbSet<mailboxer_conversations> mailboxer_conversations { get; set; }
        public virtual DbSet<mailboxer_notifications> mailboxer_notifications {  get; set; }
        public virtual DbSet<mailboxer_receipts> mailboxer_receipts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<schema_migrations> schema_migrations { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<task_types> Task_types { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<UserDomain> User_domains { get; set; }
        public virtual DbSet<user_ratings> User_ratings { get; set; }
        public virtual DbSet<UserRole> User_roles { get; set; }
        public virtual DbSet<UserTask> User_tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            //modelBuilder.Entity<UserDomain>().HasKey(ud => new { ud.UserId, ud.DomainId });
          //  modelBuilder.Entity<UserTask>().HasKey(ut => new { ut.UserId, ut.TaskId });
        }
    }
}
