using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentHomework;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentHomeworkValidator
{
    public class StudentHomeworkFeedbackDtoValidator : AbstractValidator<StudentHomeworkFeedbackDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentHomeworkFeedbackDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeworkFeedbackIdNotNull]}")
                .NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeworkFeedbackIdNotNull]}");
        }
    }
}
