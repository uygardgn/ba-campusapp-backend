using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.ClassroomTrainers;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.ClassroomTrainerValidator
{
    public class ClassroomTrainerUpdateDtoValidator : AbstractValidator<ClassroomTrainersUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ClassroomTrainerUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.Id).
               NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomTrainerIdNotEmpty]}");

            RuleFor(x => x.ClassroomId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomTrainerClassroomIdNotEmpty]}");

            RuleFor(x => x.TrainerId).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomTrainerTrainerIdNotEmpty]}");

            RuleFor(x => x.StartDate).
                NotEmpty().WithMessage($"{_stringLocalizer[Messages.ClassroomTrainerStartDateControl]}");

           
        }
    }
}
