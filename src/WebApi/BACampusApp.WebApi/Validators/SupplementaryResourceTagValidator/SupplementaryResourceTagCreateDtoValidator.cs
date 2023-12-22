using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResourceTags;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SupplementaryResourceTagValidator
{
    public class SupplementaryResourceTagCreateDtoValidator:AbstractValidator<SupplementaryResourceTagCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SupplementaryResourceTagCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(dto => dto.SupplementaryResourceId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagSupplementaryResourceIdNotEmpty]}");

            RuleFor(dto => dto.TagId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagTagIdNotEmpty]}");
        }
    }
}
