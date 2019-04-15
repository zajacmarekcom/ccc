using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Features.Agent.Home
{
    public class UserViewModel
    {
        public UserViewModel(
            string username,
            string firstName,
            string lastName,
            string email,
            string phoneNumber)
        {
            Username = username;
            Fullname = string.Format("{0} {1}", firstName, lastName);
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string Username { get; }
        public string Fullname { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
    }
}