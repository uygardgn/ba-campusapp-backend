using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Admin;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.AdminValidator
{
    public class AdminUpdateDtoValidator: AbstractValidator<AdminUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public AdminUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminFirstNameNotNull]}")
               .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminFirstNameNotNull]}")
               .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminFirstNameLength]}")
               .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage($"{_stringLocalizer[Messages.AdminFirstNameMatches]}");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminLastNameNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminLastNameNotNull]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminLastNameLength]}")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage($"{_stringLocalizer[Messages.AdminLastNameMatches]}");
            RuleFor(x => x.PhoneNumber)
               .Matches(@"^+?\d[\d\s]*$").WithMessage($"{_stringLocalizer[Messages.AdminPhoneNumberMatches]}")
               .MinimumLength(7).WithMessage($"{_stringLocalizer[Messages.AdminPhoneNumberLength]}");
            RuleFor(x => x.Country)
                .MaximumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminCountryCodeLength]}")
                .Must(IsValidCountry).WithMessage($"{_stringLocalizer[Messages.AdminCountryCodeControl]}");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminAddressNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminAddressNotNull]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminAddressLength]}");
        }
        private static bool IsValidCountry(string Country)
        {
            if (Country == null || Country == "")
            {
                return true;
            }
            else if (Country.Length < 2)
            {
                return false;
            }
            return true;

        }
    }
}
