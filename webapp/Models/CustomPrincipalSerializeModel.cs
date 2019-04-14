using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool RememberMe { get; set; }
        public int UserRoleId { get; set; }
        public string UseRoleName { get; set; }
    }
}