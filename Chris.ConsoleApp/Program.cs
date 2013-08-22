using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chris.ConsoleApp
{
    using System.Data.EntityClient;
    using System.Data.SqlClient;

    using Chris.Common.EF.Tracing;
    using Chris.Membership;
    using Chris.Membership.EF;

    using Clutch.Diagnostics.EntityFramework;

    class Program
    {
        static void Main(string[] args)
        {
            //TestMembership();



            Console.ReadKey();
        }

        private static void TestMembership()
        {
            EnableTracing();

            var context = new MembershipContext("Claims");

            var unitOfWork = new MembershipUnitOfWork(context);

            var role = unitOfWork.Roles.Create();

            unitOfWork.Roles.Add(role);

            role.Name = "Admin";

            unitOfWork.Commit();

            var roles = unitOfWork.Roles.FindAll(r => r.Name == "Test");

            roles.ToList().ForEach(r => Console.WriteLine(r.Name));

            role.Name = "Test";

            unitOfWork.Commit();

            Console.WriteLine("Done");

            var provider = new MembershipService(unitOfWork);

            string password;
            var user = provider.CreateUser("christopher.nystrom", out password);

            unitOfWork.Commit();

            Console.WriteLine(password);

            Console.WriteLine("User created");

            var validate = provider.ValidateUser(user.Email, password);

            Console.WriteLine(validate);

            var foundUser = provider.GetUser("christopher.nystrom");

            Console.WriteLine(foundUser.Email);

            foundUser = provider.GetUser("john.larsson");

            if (foundUser != null)
            {
                Console.WriteLine(foundUser.Email);
            }
            else
            {
                Console.WriteLine("John didn't exist");
            }
        }

        private static void EnableTracing()
        {
            DbTracing.Enable();
            DbTracing.AddListener(new EFTraceListener());
        }
    }
}
