using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentHomework;
using BACampusApp.Entities.Enums;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace BACampusApp.WebApi.Validators.StudentHomeworkValidator
{
    public class StudentHomeworkCreateDtoValidator : AbstractValidator<StudentHomeworkCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentHomeworkCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.HomeworkId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.StudentHomeWorkHomeWorkIdNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.StudentHomeWorkHomeWorkIdNotNull]}");

            RuleFor(x => x.StudentId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.StudentHomeWorkStudentIdNotEmpty]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.StudentHomeWorkStudentIdNotEmpty]}");
        }
    }
}
