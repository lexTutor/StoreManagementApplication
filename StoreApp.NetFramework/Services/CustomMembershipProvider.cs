using StoreApp.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace StoreApp.NetFramework.AppModels
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private readonly IUserRepository _userRepository;
        public CustomMembershipProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public override bool EnablePasswordRetrieval { get; }

        public override bool EnablePasswordReset { get; }

        public override bool RequiresQuestionAndAnswer { get; } = false;

        public override string ApplicationName { get ; set ; }

        public override int MaxInvalidPasswordAttempts { get; }

        public override int PasswordAttemptWindow { get; }

        public override bool RequiresUniqueEmail { get; } = true;

        public override MembershipPasswordFormat PasswordFormat { get; }

        public override int MinRequiredPasswordLength { get; }

        public override int MinRequiredNonAlphanumericCharacters { get; }

        public override string PasswordStrengthRegularExpression { get; }

        #region
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }


        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        #endregion
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, 
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args =
            new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            var result = _userRepository.Add(username, password, email);
            status = MembershipCreateStatus.Success;

            return result;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {

            return _userRepository.DeleteUser(username, deleteAllRelatedData);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var users = _userRepository.GetAllUsers(pageIndex, pageSize, out totalRecords);

            MembershipUserCollection membershipUserCollection = new MembershipUserCollection();
            foreach (var user in users)
            {
                membershipUserCollection.Add(user);
            }

            return membershipUserCollection;
        }


        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return _userRepository.GetUser(username, userIsOnline);
        }

        public override string GetUserNameByEmail(string email)
        {
            return _userRepository.GetUserNameByEmail(email);
        }


        public override void UpdateUser(MembershipUser user)
        {
            _userRepository.UpdateUser(user);
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                return _userRepository.Login(username, password);

            }
            catch (Exception)
            {
                //Log error
            }

            return false;
        }
    }
}