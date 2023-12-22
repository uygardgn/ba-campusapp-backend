using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Students;
using BACampusApp.Dtos.Tag;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.TagValidator
{
    public class TagUpdateValidator : AbstractValidator<TagUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public TagUpdateValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.TagNameNotEmpty]}")
                .Length(1, 128).WithMessage($"{_stringLocalizer[Messages.TagNameLength]}");
            RuleFor(x => x.Name).Matches("^[a-zA-Z0-9ğüşıöçĞÜŞİÖÇ ]*$").WithMessage($"{_stringLocalizer[Messages.TagNameNotMatches]}");
            _stringLocalizer = stringLocalizer;
            // Yukarıdaki kod, Name alanında yalnızca harfleri, rakamları, boşluk karakterini ve Türkçe karakterleri (ğ, ü, ş, ı, ö, ç, Ğ, Ü, Ş, İ, Ö, Ç) kabul eder.
        }
    }
}