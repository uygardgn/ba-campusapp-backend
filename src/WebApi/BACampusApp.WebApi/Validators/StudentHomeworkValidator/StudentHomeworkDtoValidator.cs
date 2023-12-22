using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentHomework;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentHomeworkValidator
{
    public class StudentHomeworkDtoValidator : AbstractValidator<StudentHomeworkDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentHomeworkDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkIdNotNull]}")
                .NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkIdNotNull]}");

            RuleFor(x => x.HomeWorkId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkHomeWorkIdNotNull]}")
                .NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkHomeWorkIdNotNull]}");

            RuleFor(x => x.StudentId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkStudentIdNotEmpty]}")
                .NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkStudentIdNotEmpty]}");

            RuleFor(x => x.Point)
                .Cascade(CascadeMode.Stop)
                .LessThanOrEqualTo(100).GreaterThanOrEqualTo(0).WithMessage($"{_stringLocalizer[Messages.StudentHomeWorkPointControl]}");

            RuleFor(x => x.HomeworkState)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkHomeworkStateNotNull]}")
                .NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkHomeworkStateNotNull]}");
        }
        
    }
}