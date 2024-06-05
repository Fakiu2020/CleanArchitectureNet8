using Microsoft.AspNetCore.Identity;
using System.Security.Principal;


namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public User(string userName) : base(userName)
        {
           
        }
        public User(string userName, string firstName, string lastName, string email) : base(userName)
        {
            CreationDate = DateTime.Now;
            IsEnabled = true;
            MustChangePasswordOnNextLogIn = false;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            NormalizedEmail = email.ToUpper();
            NormalizedUserName = userName.ToUpper();
        }

        public User(int userId, string userName, string firstName, string lastName, string email) : base(userName)
        {
            CreationDate = DateTime.Now;
            Id = userId;
            IsEnabled = true;
            MustChangePasswordOnNextLogIn = false;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            NormalizedEmail = email.ToUpper();
            NormalizedUserName = userName.ToUpper();
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public bool IsEnabled { get; set; }
        public bool MustChangePasswordOnNextLogIn { get; set; }
        // public virtual DeviceToken DeviceToken { get; set; }
        //public virtual ICollection<UserClaim> Claims { get; private set; }
        //public virtual ICollection<UserLogin> Logins { get; private set; }
        //public virtual ICollection<UserToken> Tokens { get; private set; }
        //public virtual ICollection<UserRole> Roles { get; private set; }

    }
}
