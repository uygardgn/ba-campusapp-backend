using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResourceTags;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SupplementaryResourceTagValidator
{
    public class SupplementaryResourceTagUpdateDtoValidator:AbstractValidator<SupplementaryResourceTagUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SupplementaryResourceTagUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(dto => dto.Id).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagIdNotEmpty]}");

            RuleFor(dto => dto.SupplementaryResourceId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagSupplementaryResourceIdNotEmpty]}");

            RuleFor(dto => dto.TagId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagTagIdNotEmpty]}");
        }
    }
}
