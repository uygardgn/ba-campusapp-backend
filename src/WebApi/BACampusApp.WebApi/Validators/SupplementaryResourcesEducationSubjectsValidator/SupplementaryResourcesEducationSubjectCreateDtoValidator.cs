using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SupplementaryResourcesEducationSubjectsValidator
{
    public class SupplementaryResourcesEducationSubjectCreateDtoValidator : AbstractValidator<SupplementaryResourceEducationSubjectCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SupplementaryResourcesEducationSubjectCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;


            RuleFor(dto => dto.EducationId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagTagIdNotEmpty]}");

            RuleFor(dto => dto.SubjectId).
               NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagTagIdNotEmpty]}");


        }
    }
}
