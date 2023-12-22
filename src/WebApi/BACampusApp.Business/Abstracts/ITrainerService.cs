namespace BACampusApp.Business.Abstracts
{
    public interface ITrainerService
    {
        /// <summary>
        ///  Bu metot Trainer nesnesi oluşturma işlemini yapmaktadır
        /// </summary>
        /// <param name="trainerCreateDto"></param>
        ///  <returns>SuccessDataResult<TrainerCreateDto>, ErrorResult</returns>
        Task<IResult> AddAsync(TrainerCreateDto trainerCreateDto);

        /// <summary>
        /// Bu metot mevcut Trainer nesnesini silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<Result> DeleteAsync(Guid id);

        /// <summary>
        ///  Bu metot Adminin,tüm Trainer nesnelerini listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<List<TrainerListDto>>, ErrorResult</returns>        
        Task<IResult> ListAsync();

        /// <summary>
        /// Bu metot Trainer nesnesini güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="trainerUpdateDto"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<IResult> UpdateAsync(TrainerUpdateDto trainerUpdateDto);

        /// <summary>
        ///  Trainer nesnesinin listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Trainer nesnesinin Guid tipinde Id si</param>
        /// <returns>SuccessDataResult<TrainerDto>></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metot Yöneticinin,tüm eğitmenleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

        /// <summary>
        ///  Bu metot,tüm aktif eğitmenlerin listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<TrainerListDto>, ErrorDataResult<TrainerListDto></returns>    
        Task<IResult> ActiveListAsync();

        /// <summary>
        /// Bu metot Trainer nesnesinin aktiflik durumunu güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="trainerActiveUpdateDto"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<IResult> UpdateActiveAsync(TrainerActiveUpdateDto trainerActiveUpdateDto);
        /// <summary>
        /// Bu metot verilen identityid'ye göre trainer döndürür
        /// </summary>
        /// <param name="identityId"></param>
        /// <returns></returns>
        Task<IResult> GetByIdentityId(string identityId);

        /// <summary>
        /// Bu metot sisteme giriş yapan trainer rolündeki kullanıcı nesnesini getirmek için kullanılır.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>IResult</returns>

        Task<IResult> GetCurrentTrainerAsync(string currentUserId);

        /// <summary>
        /// Bu metot giriş yapan trainer rolündeki kullanıcının kendi bilgilerini güncellemesi için kullanılır.
        /// </summary>
        /// <param name="trainerUpdateDto">Güncellemesi yapılacak olan verileri içeren TrainerCurrentUserUpdateDto nesnesi</param>
        /// <returns>IResult</returns>
        Task<IResult> UpdateCurrentTrainerAsync(TrainerCurrentUserUpdateDto trainerUpdateDto);
    }
}
