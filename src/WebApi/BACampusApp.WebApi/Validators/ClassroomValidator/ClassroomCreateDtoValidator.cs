using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Classroom;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.ClassroomValidator
{
    public class ClassroomCreateDtoValidator:AbstractValidator<ClassroomCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ClassroomCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(dto => dto.Name).Cascade(CascadeMode.StopOnFirstFailure).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomNameNotEmpty]}").
                MaximumLength(50).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.ClassroomNameLength]}");

            RuleFor(x => x.EducationId)
             .Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage($"{_stringLocalizer[Messages.EducationNotFound]}")
             .NotNull().WithMessage($"{_stringLocalizer[Messages.EducationNotFound]}");

            RuleFor(dto => dto.OpenDate).Cascade(CascadeMode.StopOnFirstFailure).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomOpenDateNotEmpty]}")
                .Must((dto, openDate) => openDate <= dto.ClosedDate).WithMessage($"{_stringLocalizer[Messages.ClassroomOpenDateSecondControl]}");

            RuleFor(dto => dto.ClosedDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomClosedDateNotEmpty]}")
                .Must(dto => dto >= DateTime.Today).WithMessage($"{_stringLocalizer[Messages.ClassroomClosedDateControl]}");
        }
    }
}
