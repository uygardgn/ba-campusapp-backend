using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SupplementaryResourceValidator
{
    public class SupplementaryResourceRecoverDtoValidator : AbstractValidator<SupplementaryResourceRecoverDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public SupplementaryResourceRecoverDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceNameNotEmpty]}");
            RuleFor(x => x.Tags).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagsNotNull]}");
            RuleFor(x => x.Tags).NotNull().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagsNotNull]}");
        }
    }
}
