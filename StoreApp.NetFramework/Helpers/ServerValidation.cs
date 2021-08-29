using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreApp.NetFramework.Helpers
{
    public class ServerValidation
    {
        private class UserReisteration
        {
            [Required]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Email Address is required")]
            [EmailAddress(ErrorMessage = "Email Address is invalid")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm Password is required")]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }

        private class UserLogin
        {
            [Required]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public static string ValidateRegisterationForServer(string userName, string email, string password, string confirmPassword)
        {
            UserReisteration user = new UserReisteration
            {
                UserName = userName,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, true);

            string message = string.Empty;
            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    message += validationResult.ErrorMessage.ToString() + Environment.NewLine;
                }

                return message;
            }

            return message;
        }
        public static string ValidateLoginForServer(string userName, string password)
        {
            UserLogin user = new UserLogin
            {
                UserName = userName,
                Password = password
            };

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, true);

            string message = string.Empty;
            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    message += validationResult.ErrorMessage.ToString() + Environment.NewLine;
                }

                return message;
            }

            return message;
        }
    }
}