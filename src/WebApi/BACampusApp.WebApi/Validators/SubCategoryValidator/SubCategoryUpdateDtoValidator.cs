using BACampusApp.Dtos.SubCategory;
using FluentValidation;
using Microsoft.Extensions.Localization;
using BACampusApp.Business;
using BACampusApp.Business.Constants;

namespace BACampusApp.WebApi.Validators.SubCategoryValidator
{
    public class SubCategoryUpdateDtoValidator : AbstractValidator<SubCategoryUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SubCategoryUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SubcategoryEmpty]}").Length(2, 35).WithMessage($"{_stringLocalizer[Messages.SubcategoryLength]}");
            RuleFor(x => x.ParentCategoryId).NotEmpty().WithMessage($"{_stringLocalizer[Messages.ParentCategoryIdNotFound]}");
            RuleFor(x => x.Name).Matches("^[a-zA-Z0-9ğüşıöçĞÜŞİÖÇ ]*$").WithMessage($"{_stringLocalizer[Messages.SubCategoryMatches]}");
        }
    }
}
