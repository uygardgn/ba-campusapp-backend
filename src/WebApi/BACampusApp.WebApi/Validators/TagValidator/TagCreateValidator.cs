using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Subjects;
using BACampusApp.Dtos.Tag;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.TagValidator
{
    public class TagCreateValidator : AbstractValidator<TagCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public TagCreateValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.TagNameNotEmpty]}").Length(1, 128).WithMessage($"{_stringLocalizer[Messages.TagNameLength]}");
            RuleFor(x => x.Name).Matches("^[a-zA-Z0-9ğüşıöçĞÜŞİÖÇ ]*$").WithMessage($"{_stringLocalizer[Messages.TagNameNotMatches]}");
            // Yukarıdaki kod, Name alanında yalnızca harfleri, rakamları, boşluk karakterini ve Türkçe karakterleri (ğ, ü, ş, ı, ö, ç, Ğ, Ü, Ş, İ, Ö, Ç) kabul eder.
        }
    }
}