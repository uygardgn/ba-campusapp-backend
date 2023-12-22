using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Students;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BACampusApp.WebApi.Validators.StudentValidator
{
    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.FirstName).NotEmpty().WithMessage($"{_stringLocalizer[Messages.StudentFirstNameNotEmpty]}");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage($"{_stringLocalizer[Messages.StudentFirstNameMaxLength]}");
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.StudentFirstNameMinLength]}");
            RuleFor(x => x.FirstName).Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ\\s]+$").WithMessage($"{_stringLocalizer[Messages.StudentFirstNameMatches]}");

            RuleFor(x => x.LastName).NotEmpty().WithMessage($"{_stringLocalizer[Messages.StudentLastNameNotEmpty]}");
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage($"{_stringLocalizer[Messages.StudentLastNameMaxLength]}");
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.StudentLastNameMinLength]}");
            RuleFor(x => x.LastName).Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ\\s]+$").WithMessage($"{_stringLocalizer[Messages.StudentLastNameMatches]}");

            RuleFor(x => x.Email)
               .Cascade(CascadeMode.Stop) 
               .NotEmpty()
               .WithMessage($"{_stringLocalizer[Messages.StudentEmailNotEmpty]}")
               .Must(IsValidEmail) 
               .WithMessage($"{_stringLocalizer[Messages.StudentEmailControl]}");


            RuleFor(x => x.PhoneNumber)
            .Matches(@"^+?\d[\d\s]*$").WithMessage($"{_stringLocalizer[Messages.StudentPhoneNumberMatches]}")
            .MinimumLength(7).WithMessage($"{_stringLocalizer[Messages.StudentPhoneNumberLength]}");

            RuleFor(x => x.DateOfBirth).Must(BeValidAge).WithMessage($"{_stringLocalizer[Messages.StudentDateOfBirthControl]}");

            RuleFor(x => x.Country)
                .MaximumLength(2).WithMessage($"{_stringLocalizer[Messages.StudentCountryLength]}")
                .Must(IsValidCountry).WithMessage($"{_stringLocalizer[Messages.StudentCountryControl]}");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.StudentAddressNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.StudentAddressNotNull]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.StudentAddressLength]}");

        }

        //CountryCode yardımcı metod
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
        private bool BeValidAge(DateTime? birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate?.Year;

            if (birthDate > today.AddYears((int)-age))
            {
                age--;
            }

            return age >= 18 && age <= 65;
        }       

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@(bilgeadam\.com|bilgeadamakademi\.com|bilgeadamboost\.com)$";

            Regex rg = new Regex(pattern);

            email = email.Trim();

            if (!email.IsNullOrEmpty() && rg.IsMatch(email))
            {
                return true;
            }
            return false;
        }


    }
}
