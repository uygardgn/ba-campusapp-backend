using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.HomeWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.HomeWorkValidator
{
    public class HomeWorkUpdateDtoValidator : AbstractValidator<HomeWorkUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public HomeWorkUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.HomeWorkTitleNotEmpty]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.HomeWorkTitleLength]}");


            RuleFor(dto => dto.StartDate)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.HomeWorkStartDateNotEmpty]}")
				 .Must((dto, startDate) => startDate <= dto.EndDate).WithMessage($"{_stringLocalizer[Messages.HomeWorkStartDateSecondControl]}");

			RuleFor(dto => dto.EndDate)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.HomeWorkEndDateNotEmpty]}")
                .Must(dto => dto > DateTime.Today).WithMessage($"{_stringLocalizer[Messages.HomeWorkEndDateControl]}");

			RuleFor(dto => dto.ReferanceFile).Must(HaveValidFileSize).WithMessage($"{_stringLocalizer[Messages.HomeWorkReferanceFileSizeControl]}");
			RuleFor(dto => dto.IsLateTurnedIn).NotNull().WithMessage($"{_stringLocalizer[Messages.HomeWorkIsLateTurnedInNotNull]}");
			RuleFor(dto => dto.IsHasPoint).NotNull().WithMessage($"{_stringLocalizer[Messages.HomeWorkIsHasPointNotNull]}");
		}

		private bool HaveValidFileSize(IFormFile value)
		{
			if (value == null) return true;
			return value.Length <= 1024 * 1024 * 10; //10MB
		}
	}
}
