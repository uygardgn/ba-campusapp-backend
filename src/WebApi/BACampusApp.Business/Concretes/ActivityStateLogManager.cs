using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class ActivityStateLogManager : IActivityStateLogSevices
    {
        private readonly IActivityStateLogRepository _activityStateLogRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ActivityStateLogManager(IActivityStateLogRepository activityStateLogRepo, IMapper mapper, UserManager<IdentityUser> userManager, IStringLocalizer<Resource> stringLocalizer)
        {
            _activityStateLogRepo = activityStateLogRepo;
            _mapper = mapper;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<IResult> CreateAsync(ActivityStateLogCreateDto activityStateLogCreateDto)
        {
            if (activityStateLogCreateDto == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.FailedAddActivityState]);
            }
            var activityStateLog = _mapper.Map<ActivityStateLog>(activityStateLogCreateDto);
            await _activityStateLogRepo.AddAsync(activityStateLog);
            await _activityStateLogRepo.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        ///  Bu metot veritabanındaki tüm logları çeker ve bu log listesini ActivityStateLogListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult< ActivityStateLogListDto>, ErrorDataResult< ActivityStateLogListDto></returns>

        public async Task<IResult> GetAllAsync()
        {
            var activityStateLog = await _activityStateLogRepo.GetAllAsync();
            if (activityStateLog.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<ActivityStateLogListDto>>(_mapper.Map<List<ActivityStateLogListDto>>(activityStateLog), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot verilen id ile eşleşen ActivityStateLog nesnesinin getirilmesi için kullanılmaktadır.
        /// </summary>
        /// <returns>ErrorResult, SuccessDataResult<<ActivityStateLogDto></returns>

        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var activityStateLog = await _activityStateLogRepo.GetByIdAsync(id);
            if (activityStateLog == null)
                return new ErrorResult(_stringLocalizer[Messages.ActivityStateLogNotFound]);
            var ActivityStateLogDto = _mapper.Map<ActivityStateLogDto>(activityStateLog);
            return new SuccessDataResult<ActivityStateLogDto>(ActivityStateLogDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot belirtilen role'deki kullanıcıların ItemId'lerine göre ActivityStateLog nesnesinin gösterilmesi için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>

        public async Task<IResult> GetAllAsync(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);

            List<ActivityStateLog> activityLogs = new List<ActivityStateLog>();

            foreach (var user in users)
            {
                var userActivityStates=await _activityStateLogRepo.GetAllAsync(x => x.ItemId == user.Id);
                activityLogs.AddRange(userActivityStates);
            }

            if (activityLogs.Count<=0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);

            var activityStateLogListDtos = _mapper.Map<List<ActivityStateLogListDto>>(activityLogs);
            return new SuccessDataResult<List<ActivityStateLogListDto>>(activityStateLogListDtos,_stringLocalizer[Messages.ListedSuccess]);
        }

    }
}
