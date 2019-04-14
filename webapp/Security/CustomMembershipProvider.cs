using webapp.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;

namespace webapp.Security
{
    public class CustomMembershipProvider : MembershipProvider
    {
        int _cacheTimeoutInMinutes = 180;

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public CustomMembershipUser CreateUser(Register register)
        {
            using (var context = new WebappContext())
            {
                var user = new Users();
                user.Login = register.UserName;
                user.Email = register.Email;
                user.Password = Utils.Password.EncodePassword(register.Password);
                user.FirstName = register.FirstName;
                user.LastName = register.LastName;
                user.PhoneNumber = register.PhoneNumber;
                user.RoleId = 2;

                context.Users.Add(user);
                context.SaveChanges();

                return new CustomMembershipUser(user);
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var cacheKey = string.Format("UserData_{0}", username);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                CustomMembershipUser user = null;
                user = (CustomMembershipUser)HttpRuntime.Cache[cacheKey];
                Users user2 = null;
                using (var context = new WebappContext())
                {
                   user2 = (from u in context.Users where String.Compare(u.Login, username, StringComparison.OrdinalIgnoreCase) == 0 select u).FirstOrDefault();
                }
                if(user2 != null)
                {
                    if (user.UserRoleId != user2.RoleId)
                    {
                        HttpRuntime.Cache.Insert(cacheKey, new CustomMembershipUser(user2), null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);
                        return new CustomMembershipUser(user2);
                    }
                    else
                    {
                        return user;
                    }
                }
            }

            using (var context = new WebappContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == username);

                if (user == null)
                    return null;

                var membershipUser = new CustomMembershipUser(user);

                HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

                return membershipUser;
            }
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            using (var context = new WebappContext())
            {
                password = Utils.Password.EncodePassword(password);
                var user = (from u in context.Users
                            where String.Compare(u.Login, username, StringComparison.OrdinalIgnoreCase) == 0 &&
                                String.Compare(u.Password, password, StringComparison.OrdinalIgnoreCase) == 0
                            select u).FirstOrDefault();

                return user != null;
            }
        }
    }
}