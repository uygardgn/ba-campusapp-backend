using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Categorys;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.CategoryValidator
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public CategoryUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.CategoryNotEmpty]}").Length(2, 35).WithMessage($"{_stringLocalizer[Messages.CategoryLength]}");
            RuleFor(x => x.Name).Matches("^[a-zA-Z0-9ğüşıöçĞÜŞİÖÇ ]*$").WithMessage($"{_stringLocalizer[Messages.CategoryMatches]}");          
        }
    }
}
