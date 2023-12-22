using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentHomework;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentHomeworkValidator
{
	public class StudentHomeworkStudentUpdateDtoValidator : AbstractValidator<StudentHomeworkStudentUpdateDto>
	{
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentHomeworkStudentUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
		{
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Id)
			   .Cascade(CascadeMode.Stop)
			   .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkStudentIdNotEmpty]}")
			   .NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkStudentIdNotEmpty]}");

			RuleFor(dto => dto.AttachedFile).Must(HaveValidFileSize).WithMessage($"{_stringLocalizer[Messages.StudentHomeWorkStudentAttachedFileSizeControl]}").NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkStudentAttachedFileNotEmpty]}");
		}
		private bool HaveValidFileSize(IFormFile value)
		{
			if (value == null) return true;
			return value.Length <= 1024 * 1024 * 10; //10MB
		}
	}
}
