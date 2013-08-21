using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chris.Membership.Interfaces
{
    using Chris.Common.Interfaces;

    public interface IMembershipUnitOfWork : IUnitOfWork
    {
        IRepository<Role> Roles { get; }

        IRepository<UserAccount> Users { get; }
    }
}
