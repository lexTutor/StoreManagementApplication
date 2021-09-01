using NLog;
using StoreApp.NetFramework.Helpers;
using System;
using System.Web.Security;
using System.Web.UI;

namespace StoreApp.NetFramework.Authentication
{
    public partial class Authenticate : Page
    {
        private const string Question = "No Question";
        private const string Answer = "No Answer";
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.HttpMethod == HTTPMethods.POST.ToString())
                {
                    if (SignIn.Checked)
                    {
                        string validationResult = ServerValidation.ValidateLoginForServer(UserName.Value, Password.Value);

                        if (!string.IsNullOrWhiteSpace(validationResult))
                            return;

                        if (Membership.ValidateUser(UserName.Value, Password.Value))
                        {
                            FormsAuthentication.SetAuthCookie(UserName.Value, true);
                            Response.Redirect("~/");
                            return;
                        }
                        else
                        {
                            Response.Write(StaticMarkup.GetSignInErrorMarkup());
                            return;
                        }
                    }

                    else
                    {
                        string validationResult = ServerValidation.ValidateRegisterationForServer(NewUsername.Value, EmailAddress.Value, NewPassword.Value, Confirm.Value);
                        MembershipCreateStatus status;

                        if (!string.IsNullOrWhiteSpace(validationResult))
                            return;

                        Membership.CreateUser(NewUsername.Value, NewPassword.Value, EmailAddress.Value, Question,
                             Answer, true, string.Empty, out status);

                        if (status == MembershipCreateStatus.Success)
                        {
                            FormsAuthentication.SetAuthCookie(UserName.Value, true);
                            return;
                        }
                        else
                        {
                            Response.Write(StaticMarkup.GetSignUpErrorMarkup());
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return;
            }
        }
    }
}