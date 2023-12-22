using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SubCategory;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SubCategoryValidator
{
    public class SubCategoryCreateDtoValidator : AbstractValidator<SubCategoryCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SubCategoryCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SubcategoryEmpty]}").Length(2, 35).WithMessage($"{_stringLocalizer[Messages.SubcategoryLength]}");
            RuleFor(x => x.ParentCategoryId).NotEmpty().WithMessage($"{_stringLocalizer[Messages.ParentCategoryIdNotFound]}");
            RuleFor(x => x.TechnicalUnitId).NotEmpty().WithMessage($"{_stringLocalizer[Messages.TechnicalUnitsNotFound]}");
            RuleFor(x => x.Name).Matches("^[a-zA-Z0-9ğüşıöçĞÜŞİÖÇ ]*$").WithMessage($"{_stringLocalizer[Messages.CategoryMatches]}");

        }
    }
}
