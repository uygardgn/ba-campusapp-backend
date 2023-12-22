using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Students;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentValidator
{
    public class StudentActiveUpdateDtoValidator: AbstractValidator<StudentActiveUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentActiveUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Id).NotEmpty().WithMessage($"{_stringLocalizer[Messages.StudentActiveIdNotEmpty]}");
            RuleFor(x => x.Description).NotEmpty().WithMessage($"{_stringLocalizer[Messages.StudentActiveDescriptionNotEmpty]}");
            RuleFor(x => x.Description).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.StudentActiveDescriptionMinLength]}");
            RuleFor(x => x.Description).MaximumLength(256).WithMessage($"{_stringLocalizer[Messages.StudentActiveDescriptionMaxLength]}");


        }
    }
}
