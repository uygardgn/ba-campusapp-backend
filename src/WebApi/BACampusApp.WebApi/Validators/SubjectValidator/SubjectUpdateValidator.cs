using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Subjects;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SubjectValidator
{
    public class SubjectUpdateValidator : AbstractValidator<SubjectUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SubjectUpdateValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SubjectNameNotEmpty]}").Length(2, 128).WithMessage($"{_stringLocalizer[Messages.SubjectNameLength]}");
           

        }
    }
}