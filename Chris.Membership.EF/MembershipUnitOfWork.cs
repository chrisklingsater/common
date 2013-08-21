namespace Chris.Membership.EF
{
    using System;
    using System.Data.Entity;

    using Chris.Common.EF.Repositories;
    using Chris.Common.Interfaces;
    using Chris.Membership.Interfaces;

    public class MembershipUnitOfWork : IMembershipUnitOfWork
    {
        #region Private Variables

        private static DbContext context;

        private bool disposed;

        #endregion

        #region Implementation of IMembershipUnitOfWork

        private readonly Lazy<IRepository<Role>> roles = new Lazy<IRepository<Role>>(() => new SqlRepository<Role>(context));
        public IRepository<Role> Roles
        {
            get
            {
                return roles.Value;
            }
        }

        private readonly Lazy<IRepository<UserAccount>> users = new Lazy<IRepository<UserAccount>>(() => new SqlRepository<UserAccount>(context));
        public IRepository<UserAccount> Users
        {
            get
            {
                return users.Value;
            }
        }

        #endregion

        #region Ctors

        public MembershipUnitOfWork(DbContext dbContext)
        {
            context = dbContext;
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }
            }

            disposed = true;
        }

        #endregion

        #region Implementation of IUnitOfWork

        public void Commit()
        {
            context.SaveChanges();
        }

        #endregion
    }
}
