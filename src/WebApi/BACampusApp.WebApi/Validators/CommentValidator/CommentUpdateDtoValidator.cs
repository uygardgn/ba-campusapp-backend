using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Comment;
using BACampusApp.Entities.Enums;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.CommentValidator
{
    public class CommentUpdateDtoValidator:AbstractValidator<CommentUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public CommentUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Content)
                  .NotEmpty().WithMessage($"{_stringLocalizer[Messages.CommentContentNotNull]}")
                  .NotNull().WithMessage($"{_stringLocalizer[Messages.CommentContentNotNull]}")
                  .MaximumLength(400).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.CommentContentLength]}");
            RuleFor(x => x.Title)
                  .NotEmpty().WithMessage($"{_stringLocalizer[Messages.CommentTitleNotNull]}")
                  .NotNull().WithMessage($"{_stringLocalizer[Messages.CommentTitleNotNull]}")
                  .MaximumLength(100).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.CommentTitleLength]}");
            RuleFor(x => x.ItemType)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.CommentItemTypeNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.CommentItemTypeNotNull]}")
                .Must(ItemTypeIsValid).WithMessage($"{_stringLocalizer[Messages.CommentItemTypeControl]}");
        }
        private static bool ItemTypeIsValid(ItemType ItemType)
        {
            if (ItemType == ItemType.Student || ItemType == ItemType.HomeWork || ItemType == ItemType.SupplementaryResource)
            {
                return true;
            }
            return false;
        }
    }
}
