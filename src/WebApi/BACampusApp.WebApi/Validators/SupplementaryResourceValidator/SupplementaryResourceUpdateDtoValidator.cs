using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SupplementaryResourceValidator
{
    public class SupplementaryResourceUpdateDtoValidator : AbstractValidator<SupplementaryResourceUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SupplementaryResourceUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceNameNotEmpty]}").MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceNameLength]}").MaximumLength(128).WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceNameLength]}");
            RuleFor(x => x.Tags).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagsNotNull]}").NotNull().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagsNotNull]}");
            RuleFor(x => x.Educations).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceEducationsNotNull]}").NotNull().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceEducationsNotNull]}");
            RuleFor(x => x.Subjects).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceSubjectsNotNull]}").NotNull().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceSubjectsNotNull]}");

        }
    }
}