using System.Security.Principal;
using System.Web.Security;

namespace webapp.Security
{
    public class CustomIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }

        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;

            var customMembershipUser = (CustomMembershipUser)Membership.GetUser(identity.Name);
            if (customMembershipUser != null)
            {
                UserId = customMembershipUser.UserId;
                FirstName = customMembershipUser.FirstName;
                LastName = customMembershipUser.LastName;
                Email = customMembershipUser.Email;
                UserRoleId = customMembershipUser.UserRoleId;
                UserRoleName = customMembershipUser.UserRoleName;
                UserName = customMembershipUser.UserName;
                PhoneNumber = customMembershipUser.PhoneNumber;
            }
        }
    }
}