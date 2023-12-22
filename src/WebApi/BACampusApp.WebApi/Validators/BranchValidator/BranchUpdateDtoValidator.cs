using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Branch;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.BranchValidator
{
    public class BranchUpdateDtoValidator : AbstractValidator<BranchUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public BranchUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.BranchNameNotEmpty]}").Length(2, 35)
                .WithMessage($"{_stringLocalizer[Messages.BranchNameLength]}")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ0-9 ]+$").WithMessage($"{_stringLocalizer[Messages.BranchNameInvalid]}");
        }
    }
}
