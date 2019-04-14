using webapp.Models;
using System;
using System.Linq;
using System.Web.Security;

namespace webapp.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public CustomMembershipUser(Users user)
            : base("CustomMembershipProvider", user.Login, user.Id, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            var db = new WebappContext();
            UserId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserRoleId = user.RoleId;
            UserRoleName = db.Roles.Single(r => r.Id == user.RoleId).Name;
            UserName = user.Login;
            PhoneNumber = user.PhoneNumber;
        }
    }
}