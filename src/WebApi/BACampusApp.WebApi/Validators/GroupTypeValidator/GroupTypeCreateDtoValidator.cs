using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.GroupType;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.GroupTypeValidator
{
    public class GroupTypeCreateDtoValidator : AbstractValidator<GroupTypeCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public GroupTypeCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name)
              .NotEmpty().WithMessage($"{_stringLocalizer[Messages.GroupTypeNameNotEmpty]}")
              .Length(2, 35).WithMessage($"{_stringLocalizer[Messages.GroupTypeNameLength]}")
              .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ0-9 ]+$").WithMessage($"{_stringLocalizer[Messages.GroupTypeNameInvalid]}");

        }
    }
}