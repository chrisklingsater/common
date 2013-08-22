using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chris.Membership
{
    using Chris.Common.Helpers;
    using Chris.Membership.Interfaces;

    public class MembershipService
    {
        private IMembershipUnitOfWork unitOfWork;

        public MembershipService(IMembershipUnitOfWork membershipUnitOfWork)
        {
            unitOfWork = membershipUnitOfWork;
        }

        public UserAccount CreateUser(string userName, out string password)
        {
            if (userName == null)
                throw new ArgumentNullException("userName");

            password = EncryptionHelper.GeneratePassword();

            return CreateUser(userName, password);
        }

        public UserAccount CreateUser(string userName, string password)
        {
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (password == null)
                throw new ArgumentNullException("password");

            var user = unitOfWork.Users.Create();
            user.Email = userName;
            user.Password = EncryptionHelper.EncryptPassword(password);
            user.Roles = new List<Role> { new Role { Name = "Administrator" } };

            try
            {
                unitOfWork.Users.Add(user);
            }
            catch (Exception e)
            {
                throw;
            }

            return user;
        }

        public bool ValidateUser(string userName, string password)
        {
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (password == null)
                throw new ArgumentNullException("password");

            var user =
                unitOfWork.Users.FindAll(
                    u => u.Email == userName).SingleOrDefault();

            if (user != null)
            {
                if (EncryptionHelper.PasswordIsVerified(user.Password, password))
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public UserAccount GetUser(string userName)
        {
            var user = unitOfWork.Users.FindAll(u => u.Email == userName).SingleOrDefault();

            if (user != null) 
                return user;

            return null;
        }
    }
}
