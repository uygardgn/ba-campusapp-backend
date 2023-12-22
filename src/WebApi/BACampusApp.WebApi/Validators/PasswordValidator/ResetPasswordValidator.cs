using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Account;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BACampusApp.WebApi.Validators.PasswordValidator
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ResetPasswordValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.ResetPasswordEmailNotEmpty]}")
                .Must(IsValidEmail).WithMessage($"{_stringLocalizer[Messages.ResetPasswordEmailControl]}");
        }
        
        private bool IsValidEmail(string email)
        {            
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            Regex rg = new Regex(pattern);

            email = email.Trim();

            if (!email.IsNullOrEmpty() && rg.IsMatch(email))            
                return true;
            
            return false;
        }
    }
}
