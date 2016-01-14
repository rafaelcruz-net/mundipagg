using MundiPagg.Domain.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using WebMatrix.WebData;

namespace MundiPagg.Domain.Service.Membership
{
    public class MembershipProvider : ExtendedMembershipProvider
    {
        private const int TokenSizeInBytes = 16;
        private MachineKeySection _machineKey;

        private ICustomerService customerService
        {
            get;
            set;
        }


        private string applicationName;

        private bool enablePasswordRetrieval;
        private bool enablePasswordReset;

        private int maxInvalidPasswordAttempts;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private string passwordStrengthRegularExpression;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;


        #region Propriedades
        public override bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }
        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get { return enablePasswordReset; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return requiresUniqueEmail; }
        }

        #endregion

      
        public MembershipProvider()
        {
           
        }
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            // Initialize the abstract base class.
            base.Initialize(name, config);

            this.applicationName = GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
            this.maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            this.passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            this.minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            this.minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            this.passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            this.enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            this.enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            this.requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            this.requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));

            string tempFormat = config["passwordFormat"] ?? "Hashed";

            switch (tempFormat)
            {
                case "Hashed":
                    this.passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    this.passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    this.passwordFormat = MembershipPasswordFormat.Clear;
                    break;
            }

            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
                System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
                this._machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

                if (_machineKey.ValidationKey.Contains("AutoGenerate"))
                {
                    if (PasswordFormat == MembershipPasswordFormat.Encrypted)
                        throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
                }
            }

            this.customerService = Infra.IoC.Kernel.ResolveService<ICustomerService>();

        }


        private static string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }
        
        public override bool ValidateUser(string username, string password)
        {
            var user = this.customerService.GetCustomerByEmailAndPassword(username, password);

            if (user == null)
                return false;

            return true;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = this.customerService.GetCustomerById((Guid)providerUserKey);
            return LoadMembershipUserFromUser(user);
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = this.customerService.GetCustomerByEmail(username);
            return LoadMembershipUserFromUser(user);
        }


        private MembershipUser LoadMembershipUserFromUser(Customer user)
        {
            
            var result = new MembershipUser(this.Name,
                                            user.Name,
                                            user.Id,
                                            user.Email,
                                            "",
                                            "",
                                            true,
                                            false,
                                            DateTime.Now,
                                            DateTime.Now,
                                            DateTime.Now,
                                            DateTime.MinValue,
                                            DateTime.MinValue);
            return result;
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            throw new NotImplementedException();
        }

        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            throw new NotImplementedException();
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            throw new NotImplementedException();
        }

        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteAccount(string userName)
        {
            throw new NotImplementedException();
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            throw new NotImplementedException();
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            throw new NotImplementedException();
        }

        public override bool IsConfirmed(string userName)
        {
            throw new NotImplementedException();
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetCreateDate(string userName)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
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

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
    }
}
