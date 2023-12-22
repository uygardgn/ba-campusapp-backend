using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Classroom;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.ClassroomValidator
{
    public class ClassroomUpdateDtoValidator:AbstractValidator<ClassroomUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ClassroomUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(dto => dto.Id).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomIdNotEmpty]}");

            RuleFor(dto => dto.Name).Cascade(CascadeMode.StopOnFirstFailure).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomNameNotEmpty]}").
                MaximumLength(50).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.ClassroomNameLength]}");

            RuleFor(dto => dto.EducationId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomEducationIdNotEmpty]}");

            RuleFor(dto => dto.OpenDate).Cascade(CascadeMode.StopOnFirstFailure).
               NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomOpenDateNotEmpty]}")
               .Must((dto, openDate) => openDate <= dto.ClosedDate).WithMessage($"{_stringLocalizer[Messages.ClassroomOpenDateSecondControl]}");

            RuleFor(dto => dto.ClosedDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomClosedDateNotEmpty]}")
                .Must(dto => dto >= DateTime.Today).WithMessage($"{_stringLocalizer[Messages.ClassroomClosedDateControl]}");
        }
    }
}
