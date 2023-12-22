using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.TrainingType;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.TrainingTypeValidator
{
    public class TrainingTypeUpdateDtoValidator : AbstractValidator<TrainingTypeUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public TrainingTypeUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.TrainingTypeNameNotEmpty]}").Length(2, 35)
                .WithMessage($"{_stringLocalizer[Messages.TrainingTypeNameLength]}")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ0-9 ]+$").WithMessage($"{_stringLocalizer[Messages.TrainingTypeNameInvalid]}");
        }
    }
}
