using BACampusApp.Dtos.RoleLog;
using FluentValidation;

namespace BACampusApp.WebApi.Validators.RoleLogValidator
{
    public class RoleLogCreateValidator : AbstractValidator<RoleLogCreateDto>
    {
        public RoleLogCreateValidator()
        {
            RuleFor(dto => dto.ActiveRole)
                .NotEmpty().WithMessage("Aktif rol boş olamaz.");

            RuleFor(dto => dto.ActionType)
                .NotEmpty().WithMessage("İşlem tipi boş olamaz.");
        }
    }
}
