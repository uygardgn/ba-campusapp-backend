using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentHomework;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentHomeworkValidator
{
	public class StudentHomeworkTrainerUpdateDtoValidator : AbstractValidator<StudentHomeworkTrainerUpdateDto>
	{
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentHomeworkTrainerUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
		{
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Id)
			   .Cascade(CascadeMode.Stop)
			   .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkTrainerIdNotNull]}")
			   .NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkTrainerIdNotNull]}");

			RuleFor(x => x.HomeWorkId)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkTrainerHomeWorkIdNotNull]}")
				.NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkTrainerHomeWorkIdNotNull]}");

			RuleFor(x => x.StudentId)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkTrainerStudentIdNotNull]}")
				.NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkTrainerStudentIdNotNull]}");

			RuleFor(x => x.Point)
				.Cascade(CascadeMode.Stop)
				.LessThanOrEqualTo(100).GreaterThanOrEqualTo(0).WithMessage($"{_stringLocalizer[Messages.StudentHomeWorkTrainerPointControl]}");

			RuleFor(dto => dto.AttachedFile).Must(HaveValidFileSize).WithMessage($"{_stringLocalizer[Messages.StudentHomeWorkTrainerAttachedFileSizeControl]}");

			//RuleFor(dto => dto.IsFileChanged).Equal(x => x.IsFileChanged == true && x.AttachedFile == null).WithMessage("Belge yüklemediniz!");
		}
		private bool HaveValidFileSize(IFormFile value)
		{
			if (value == null) return true;
			return value.Length <= 1024 * 1024 * 10; //10MB
		}
		
	}
}

