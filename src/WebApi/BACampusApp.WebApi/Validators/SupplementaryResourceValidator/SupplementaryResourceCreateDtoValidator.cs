using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace BACampusApp.WebApi.Validators.SupplementaryResourceCreate
{
    public class SupplementaryResourceCreateDtoValidator : AbstractValidator<SupplementaryResourceCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SupplementaryResourceCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceNameNotEmpty]}").MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceNameLength]}").MaximumLength(128).WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceNameLength]}");
            RuleFor(x => x.FileURL).NotEmpty().When(x => x.Link == null).WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceFileURLNotEmpty]}");
            RuleFor(x => x.Link).NotEmpty().When(x => x.FileURL == null).WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceFileURLNotEmpty]}");
            RuleFor(x => x.ResourceType)
                .InclusiveBetween(0, 12)
                .WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceResourceTypeControl]}");
            RuleFor(x => x.Tags).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagsNotNull]}").NotNull().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceTagsNotNull]}");
            RuleFor(x => x.Educations).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceEducationsNotNull]}").NotNull().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceEducationsNotNull]}");
            RuleFor(x => x.Subjects).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceSubjectsNotNull]}").NotNull().WithMessage($"{_stringLocalizer[Messages.SupplementaryResourceSubjectsNotNull]}");
        }
    }
}