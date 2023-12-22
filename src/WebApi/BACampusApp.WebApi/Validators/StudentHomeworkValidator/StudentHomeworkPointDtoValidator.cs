using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentHomework;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentHomeworkValidator
{
    public class StudentHomeworkPointDtoValidator : AbstractValidator<StudentHomeworkPointDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentHomeworkPointDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkPointIdNotNull]}")
                .NotNull().WithErrorCode($"{_stringLocalizer[Messages.StudentHomeWorkPointIdNotNull]}");

            //Yalnızca Id ile çekme işlemi yapıldığından satırlar yoruma alınmıştır, nihai sonuç sonrası duruma göre kaldırılacaklardır.

            //RuleFor(x => x.HomeWorkId)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty().WithErrorCode("Bir ödev numarası girmelisiniz.")
            //    .NotNull().WithErrorCode("Bir ödev numarası girmelisiniz.");

            //Yalnızca Id ile çekme işlemi yapıldığından satırlar yoruma alınmıştır, nihai sonuç sonrası duruma göre kaldırılacaklardır.

            //RuleFor(x => x.StudentId)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty().WithErrorCode("Bir öğrenci numarası girmelisiniz.")
            //    .NotNull().WithErrorCode("Bir öğrenci numarası girmelisiniz.");

            RuleFor(x => x.Point)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Ödev puanı 0'dan büyük olmalıdır!")
                .LessThanOrEqualTo(100).WithMessage("Ödev puanı 100'den büyük olamaz!");

        }
    }
}