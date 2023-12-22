using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SupplementaryResourcesEducationSubjectsValidator
{
    public class SupplementaryResourcesEducationSubjectUpdateDtoValidator : AbstractValidator<SupplementaryResourceEducationSubjectUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SupplementaryResourcesEducationSubjectUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

           RuleFor(dto => dto.SupplementaryResourceId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagSupplementaryResourceIdNotEmpty]}");

            RuleFor(dto => dto.EducationId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagTagIdNotEmpty]}");

            RuleFor(dto => dto.SubjectId).
               NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagTagIdNotEmpty]}");
        }
    }
}
