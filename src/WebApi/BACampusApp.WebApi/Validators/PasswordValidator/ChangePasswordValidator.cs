using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Account;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BACampusApp.WebApi.Validators.PasswordValidator
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ChangePasswordValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Email)
               .Cascade(CascadeMode.Stop)
               .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.ChangePasswordEmailNotEmpty]}")
               .Must(IsValidEmail)
               .WithMessage($"{_stringLocalizer[Messages.ChangePasswordEmailControl]}");

            RuleFor(x => x.OldPassword)
              .Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage($"{_stringLocalizer[Messages.ChangePasswordOldPasswordNotEmpty]}");

            RuleFor(x => x.NewPassword)
               .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage($"{_stringLocalizer[Messages.ChangePasswordNewPasswordNotEmpty]}");

            RuleFor(x => x.NewPasswordAgain)
              .Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage($"{_stringLocalizer[Messages.ChangePasswordNewPasswordAgainNotEmpty]}")
              .Equal(x => x.NewPassword).WithMessage($"{_stringLocalizer[Messages.ChangePasswordNewPasswordAgainEqual]}");
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

