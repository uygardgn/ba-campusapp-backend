using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Categorys;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.CategoryValidator
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public CategoryCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.CategoryNotEmpty]}").Length(2, 35).WithMessage($"{_stringLocalizer[Messages.CategoryLength]}");

        }
    }
}
