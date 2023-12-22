using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.ClassroomStudent;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.ClassroomStudentValidator
{
    public class ClassroomStudentUpdateDtoValidator : AbstractValidator<ClassroomStudentUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ClassroomStudentUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(dto => dto.Id).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomStudentIdNotEmpty]}");

            RuleFor(dto => dto.ClassroomId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomStudentClassroomIdNotEmpty]}");

            RuleFor(dto => dto.StudentId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomStudentStudentIdNotEmpty]}");

            RuleFor(dto => dto.StartDate).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomStudentStartDateNotEmpty]}");
            //.Must((dto, startDate) => startDate <= dto.EndDate).WithMessage($"{_stringLocalizer[Messages.ClassroomStudentStartDateSecondControl]}");

            //RuleFor(dto => dto.EndDate).Cascade(CascadeMode.StopOnFirstFailure)
            //    .NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomStudentEndDateNotEmpty]}")
            //    .Must(dto => dto >= DateTime.Today).WithMessage($"{_stringLocalizer[Messages.ClassroomStudentEndDateControl]}");
        }
    }
}
