using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Educations;
using BACampusApp.Dtos.HomeWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.HomeWorkValidator
{
    public class HomeWorkCreateDtoValidator : AbstractValidator<HomeWorkCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public HomeWorkCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
		{
            _stringLocalizer = stringLocalizer;

            RuleFor(dto => dto.Title)
				.NotEmpty().WithMessage($"{_stringLocalizer[Messages.HomeWorkTitleNotEmpty]}")
				.MaximumLength(100).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.HomeWorkTitleLength]}");

			RuleFor(dto => dto.Intructions).MaximumLength(100).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.HomeWorkIntructionsLength]}");

			RuleFor(dto => dto.SubjectId)
			   .NotEmpty().WithMessage($"{_stringLocalizer[Messages.HomeWorkSubjectIdNotNull]}")
			   .NotNull().WithMessage($"{_stringLocalizer[Messages.HomeWorkSubjectIdNotNull]}")
			   .When(x => x.SubjectId != null);

			RuleFor(dto => dto.StartDate)
				.NotEmpty().WithMessage($"{_stringLocalizer[Messages.HomeWorkStartDateNotEmpty]}")
				 .Must(dto => dto >= DateTime.Today).WithMessage($"{_stringLocalizer[Messages.HomeWorkStartDateFirstControl]}").Must((dto, startDate) => startDate <= dto.EndDate).WithMessage($"{_stringLocalizer[Messages.HomeWorkStartDateSecondControl]}");

			RuleFor(dto => dto.EndDate)
				.NotEmpty().WithMessage($"{_stringLocalizer[Messages.HomeWorkEndDateNotEmpty]}")
				.Must(dto => dto > DateTime.Today).WithMessage($"{_stringLocalizer[Messages.HomeWorkEndDateControl]}");

			RuleFor(dto => dto.ReferanceFile).Must(HaveValidFileSize).WithMessage($"{_stringLocalizer[Messages.HomeWorkReferanceFileSizeControl]}");
            //RuleFor(dto => dto.IsLateTurnedIn).NotNull().WithMessage("Lütfen ödevin geç teslim edilme/edilmeme kuralını belirtiniz.");
            //RuleFor(dto=>dto.IsHasPoint).NotNull().WithMessage("Lütfen ödevin geç teslim edilme/edilmeme kuralını belirtiniz.");
        }

        private bool HaveValidFileSize(IFormFile value)
		{
			if (value == null) return true;
			return value.Length <= 1024 * 1024 * 10; //10MB
		}
	}
}
