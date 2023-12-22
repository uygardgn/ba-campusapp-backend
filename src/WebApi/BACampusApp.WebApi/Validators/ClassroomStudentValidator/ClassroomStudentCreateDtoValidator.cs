using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.ClassroomStudent;
using FluentValidation;
using Humanizer;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.ClassroomStudentValidator
{
    public class ClassroomStudentCreateDtoValidator : AbstractValidator<ClassroomStudentCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public ClassroomStudentCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(dto => dto.ClassroomId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomStudentClassroomIdNotEmpty]}");

            RuleFor(dto => dto.StudentId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomStudentStudentIdNotEmpty]}");

            RuleFor(dto => dto.StartDate).Cascade(CascadeMode.StopOnFirstFailure).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomStudentStartDateNotEmpty]}");

            RuleFor(dto => dto.EndDate).Cascade(CascadeMode.StopOnFirstFailure);

        }
    }
}
