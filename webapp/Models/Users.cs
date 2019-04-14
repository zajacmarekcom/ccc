using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Login { get; set; }
        public string Password { get; set; }
        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }
        [MaxLength(300)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }


        public static bool UserAlradyExists(string username, out string message)
        {
            message = String.Empty;
            using (var context = new WebappContext())
            {
                var result = context.Users.Any(u => string.Compare(u.Login, username, StringComparison.CurrentCultureIgnoreCase) == 0);
                if (result)
                {
                    message = "Użytkownik o podanej nazwie już istnieje";
                }

                return result;
            }
        }

        public static string GetUserName(int id)
        {
            string result;
            using (var context = new WebappContext())
            {
                var user = context.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                result = string.IsNullOrEmpty(user.FirstName) && string.IsNullOrEmpty(user.LastName) ? user.Login : user.FirstName + " " + user.LastName;
            }

            return result;
        }
    }
}