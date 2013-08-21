namespace Chris.Membership.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using Chris.Common.Interfaces;

    public class MembershipContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }

        public DbSet<UserAccount> Users { get; set; } 

        public override int SaveChanges()
        {
            SetAuditInfo();

            return base.SaveChanges();
        }

        private void SetAuditInfo()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is IAudit && 
                (e.State == EntityState.Added || e.State == EntityState.Modified)))
            {
                var now = DateTime.Now;

                var audit = (IAudit)entry.Entity;

                if (entry.State == EntityState.Added)
                    audit.Created = now;

                audit.Modified = now;
            }
        }
    }
}
