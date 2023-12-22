using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Trainers;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BACampusApp.WebApi.Validators.TrainerValidator
{
    public class TrainerCreateDtoValidator : AbstractValidator<TrainerCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public TrainerCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.FirstName).NotEmpty().WithMessage($"{_stringLocalizer[Messages.TrainerFirstNameNotEmpty]}");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage($"{_stringLocalizer[Messages.TrainerFirstNameMaxLength]}");
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.TrainerFirstNameMinLength]}");
            RuleFor(x => x.FirstName).Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage($"{_stringLocalizer[Messages.TrainerFirstNameMatches]}");

            RuleFor(x => x.LastName).NotEmpty().WithMessage($"{_stringLocalizer[Messages.TrainerLastNameNotEmpty]}");
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage($"{_stringLocalizer[Messages.TrainerLastNameMaxLength]}");
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.TrainerLastNameMinLength]}");
            RuleFor(x => x.LastName).Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage($"{_stringLocalizer[Messages.TrainerFirstNameMatches]}");

            RuleFor(x => x.Email)
              .Cascade(CascadeMode.Stop) //NotEmpty hatasını alınca bir alt şarta geçmeyi engeller.
              .NotEmpty()
              .WithMessage($"{_stringLocalizer[Messages.TrainerEmailNotEmpty]}")
              .Must(IsValidEmail) //yardımcı metot
              .WithMessage($"{_stringLocalizer[Messages.TrainerEmailControl]}");


            RuleFor(x => x.PhoneNumber)
                .Matches(@"^+?\d[\d\s]*$").WithMessage($"{_stringLocalizer[Messages.TrainerPhoneNumberMatches]}")
                .MinimumLength(7).WithMessage($"{_stringLocalizer[Messages.TrainerPhoneNumberMinLength]}");

            RuleFor(x => x.DateOfBirth).Must(BeValidAge).WithMessage($"{_stringLocalizer[Messages.TrainerDateOfBirthControl]}");

            RuleFor(x => x.Country)
                .MaximumLength(2).WithMessage($"{_stringLocalizer[Messages.TrainerCountryMaxLength]}")
                .Must(IsValidCountry).WithMessage($"{_stringLocalizer[Messages.TrainerCountryControl]}");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.TrainerAddressNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.TrainerAddressNotNull]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.TrainerAddressLength]}");

        }

        //CountryCode yardımcı metod
        private static bool IsValidCountry(string Country)
        {
            if (Country == null || Country == "")
                return true;
            else if (Country.Length < 2)
                return false;

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
            //email şartına uygun olup olmadığını kontrol eden regex yapısı:
            string pattern = @"^[a-zA-Z0-9_.+-]+@(bilgeadam\.com|bilgeadamakademi\.com|bilgeadamboost\.com)$";

            //Regex yaratır:
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

