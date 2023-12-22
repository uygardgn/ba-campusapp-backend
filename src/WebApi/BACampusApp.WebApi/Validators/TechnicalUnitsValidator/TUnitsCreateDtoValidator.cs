using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.TechnicalUnits;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.TechnicalUnitsValidator
{
    public class TUnitsCreateDtoValidator:AbstractValidator<TUnitCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public TUnitsCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer) 
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.TUnitsNameNotEmpty]}").Length(2, 35).WithMessage($"{_stringLocalizer[Messages.TUnitsNameLength]}");
           // RuleFor(x => x.Name).Matches("^[a-zA-Z0-9ğüşıöçĞÜŞİÖÇ ]*$").WithMessage("Lütfen özel karakterler kullanmayınız.");
           
        }
    }
}
