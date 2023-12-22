using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.ClassroomTrainers;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.ClassroomTrainerValidator
{
    public class ClassroomTrainerCreateDtoValidator : AbstractValidator<ClassroomTrainersCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ClassroomTrainerCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.ClassroomId).NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomTrainerClassroomIdNotEmpty]}");

            RuleFor(x => x.TrainerId).NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomTrainerTrainerIdNotEmpty]}");

            RuleFor(x => x.StartDate).Cascade(CascadeMode.StopOnFirstFailure).
               NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomTrainerStartDateNotEmpty]}"); 

            RuleFor(x => x.EndDate).Cascade(CascadeMode.StopOnFirstFailure);
        }
    }
}
