using System;
using System.Security.Principal;

namespace webapp.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return Identity is CustomIdentity && string.Compare(role, ((CustomIdentity)Identity).UserRoleName, StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        public CustomIdentity CustomIdentity { get { return (CustomIdentity)Identity; } set { Identity = value; } }

        public CustomPrincipal(CustomIdentity identity)
        {
            Identity = identity;
        }

        public CustomPrincipal(string name)
        {
            Identity = new CustomIdentity(new GenericIdentity(name));
        }
    }
}