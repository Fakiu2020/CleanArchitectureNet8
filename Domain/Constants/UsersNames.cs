using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public abstract class UsersNames(int id, string userName, string firstName, string lastName, string email)
    {
       
        public string Email { get; private set; } = email;

        public string UserName { get; private set; } = userName;
        public string FirstName { get; private set; } = firstName;
        public string LastName { get; private set; } = lastName;
        public int Id { get; private set; } = id;


        public static UsersNames Admin = new AdminType();
        public static UsersNames SuperAdmin = new SuperAdminType();
        public static UsersNames User = new UserType();


        private class SuperAdminType : UsersNames
        {
            public SuperAdminType() : base(1, "superAdminTest", "Test", "SuperAdmin", "superAdmin@test.com")
            { }
        }

        private class AdminType : UsersNames
        {
            public AdminType() : base(2, "adminTest", "Test", "Admin", "admin@test.com")
            { }
        }

        private class UserType : UsersNames
        {
            public UserType() : base(3, "UserTest", "01", "User", "user@test.com")
            { }
        }

    }

}
