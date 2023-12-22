using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Subjects;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.SubjectCreate
{
    public class SubjectCreateValidator : AbstractValidator<SubjectCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public SubjectCreateValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Name).NotEmpty().WithMessage($"{_stringLocalizer[Messages.SubjectNameNotEmpty]}").Length(2, 128).WithMessage($"{_stringLocalizer[Messages.SubjectNameLength]}");
            
        }
    }
}
