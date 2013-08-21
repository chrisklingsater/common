namespace Chris.Membership
{
    using System;

    using Chris.Common.Interfaces;

    public class Role : IEntity<Role>
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        #region Implementation of IAudit

        public virtual DateTime Created { get; set; }

        public virtual DateTime Modified { get; set; }

        #endregion
    }
}
