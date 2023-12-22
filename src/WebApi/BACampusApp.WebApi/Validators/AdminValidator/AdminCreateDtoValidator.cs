using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Core.Utilities.Results;
using BACampusApp.Dtos.Admin;
using BACampusApp.Dtos.Educations;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BACampusApp.WebApi.Validators.AdminCreate
{
    public class AdminCreateDtoValidator : AbstractValidator<AdminCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public AdminCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminFirstNameNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminFirstNameNotNull]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminFirstNameLength]}")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage($"{_stringLocalizer[Messages.AdminFirstNameMatches]}");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminLastNameNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminLastNameNotNull]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminLastNameLength]}")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage($"{_stringLocalizer[Messages.AdminLastNameMatches]}");


            //Bu kod ile önce Email alanı boş bırakıldığı durumda mesaj dönmektedir, mail alanı doldurulduğu durumda ise girilen mail adresinin uygun formatta olup olmadığı kontrol edilip uygun değilse ilgili mesajı dönmektedir:
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop) //NotEmpty hatasını alınca bir alt şarta geçmeyi engeller.
                .NotEmpty()
                .WithMessage($"{_stringLocalizer[Messages.AdminEmailNotEmpty]}")
                .Must(IsValidEmail) //yardımcı metot
                .WithMessage($"{_stringLocalizer[Messages.AdminEmailControl]}");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^+?\d[\d\s]*$").WithMessage($"{_stringLocalizer[Messages.AdminPhoneNumberMatches]}")
                .MinimumLength(7).WithMessage($"{_stringLocalizer[Messages.AdminPhoneNumberLength]}");

            RuleFor(x => x.Country)
                .MaximumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminCountryCodeLength]}")
                .Must(IsValidCountry).WithMessage($"{_stringLocalizer[Messages.AdminCountryCodeControl]}");

            RuleFor(x => x.Gender)
                .Must(gender => gender == true || gender == false)
                .WithMessage($"{_stringLocalizer[Messages.AdminGenderControl]}");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now.AddYears(-18)).WithMessage($"{_stringLocalizer[Messages.AdminDateOfBirthLessThan]}")
                .GreaterThan(DateTime.Now.AddYears(-65)).WithMessage($"{_stringLocalizer[Messages.AdminDateOfBirthGreaterThan]}");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminAddressNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminAddressNotNull]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminAddressLength]}");
        }
        //Mail yardımcı metot:
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
        
        //CountryCode yardımcı metod
        private static bool IsValidCountry (string Country)
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