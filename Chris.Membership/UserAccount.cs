namespace Chris.Membership
{
    using System;
    using System.Collections.Generic;

    using Chris.Common.Interfaces;
    using System.Linq;
    using Chris.Membership.Interfaces;

    public class UserAccount : IEntity<UserAccount>
    {
        public virtual int Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual IList<Role> Roles { get; set; }

        #region Implementation of IAudit

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        #endregion

        public bool IsInRole(string roleName)
        {
            return Roles.Any(r => r.Name == roleName);
        }
    }
}
