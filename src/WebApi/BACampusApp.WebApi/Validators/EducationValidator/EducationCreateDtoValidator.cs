using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Educations;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.EducationValidator
{
    public class EducationCreateDtoValidator:AbstractValidator<EducationCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public EducationCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(dto => dto.Name)
                  .NotEmpty().WithMessage($"{_stringLocalizer[Messages.EducationNameNotEmpty]}")
                  .MaximumLength(50).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.EducationNameLength]}");

            RuleFor(dto => dto.Description)
                 .NotEmpty().WithMessage($"{_stringLocalizer[Messages.EducationDescriptionNotEmpty]}")
                 .MaximumLength(1200).MinimumLength(1).WithMessage($"{_stringLocalizer[Messages.EducationDescriptionLength]}");


            RuleFor(dto => dto.CourseHours)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.EducationCourseHoursNotEmpty]}")
                .InclusiveBetween(1, 1000).WithMessage($"{_stringLocalizer[Messages.EducationCourseHoursControl]}");

            RuleFor(dto => dto.SubCategoryId)
                .NotNull().WithMessage($"{_stringLocalizer[Messages.EducationCategoryIdNotNull]}");
        }


    }
}
