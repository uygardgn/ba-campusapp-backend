using BACampusApp.Dtos.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class ReportManager : IReportService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly ITrainerRepository _trainerRepo;
        private readonly IAdminRepository _adminRepo;
        public ReportManager(IStudentRepository studentRepo, ITrainerRepository trainerRepo, IAdminRepository adminRepo, IStringLocalizer<Resource> stringLocalizer)
        {
            _studentRepo = studentRepo;
            _trainerRepo = trainerRepo;
            _stringLocalizer = stringLocalizer;
            _adminRepo = adminRepo;
        }


        /// <summary>
        /// Aktif olan tüm kullanıcı tiplerinin sayısının bilgisini verir.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> ActiveAllUserCount()
        {
            var activeAdminCount =await _adminRepo.GetAllAsync(x=>x.Status == Status.Active);
            var activeTrainerCount = await _trainerRepo.GetAllAsync(x => x.Status == Status.Active);
            var activeStudentCount = await _studentRepo.GetAllAsync(x => x.Status == Status.Active);

            var allActiveUsersDto = new AllActiveUsersCountDto
            {
                ActiveAdminsCount = activeAdminCount.Count(),
                ActiveStudentsCount = activeTrainerCount.Count(),
                ActiveTrainerssCount = activeTrainerCount.Count(),
            };

            return new SuccessDataResult<AllActiveUsersCountDto>(allActiveUsersDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot tüm aktif öğrencilerin sayısının dönmesini sağlamaktadır.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> ActiveStudentCountAsync()
        {
            var students = await _studentRepo.GetAllAsync(x => x.Status == Status.Active);
            var studentCount = students.Count();

            if (studentCount <= 0)
                return new ErrorResult(_stringLocalizer[Messages.StudentNotActive]);

            return new SuccessDataResult<int>(studentCount, _stringLocalizer[Messages.FoundSuccess]);
        }
        /// <summary>
        /// Bu metot tüm aktif eğitmenlerin sayısının dönmesini sağlamaktadır.
        /// </summary>
        /// <returns>Başarılıysa ActiveTrainerCountDto tipinde başarılı mesajını, başarılı değilse hata mesajını döner. </returns>
        public async Task<IResult> ActiveTrainerCountAsync()
        {
            var activeTrainersCount = (await _trainerRepo.GetAllAsync(x => x.Status == Status.Active)).Count();
            var activeTrainerDto = new ActiveTrainerCountDto
            {
                ActiveTrainersCount = activeTrainersCount,
            };

            if (activeTrainerDto.ActiveTrainersCount == 0)
                return new ErrorResult(_stringLocalizer[Messages.TrainerNotActive]);

            return new SuccessDataResult<ActiveTrainerCountDto>(activeTrainerDto, _stringLocalizer[Messages.FoundSuccess]);
        }
    }
}
