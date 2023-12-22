namespace BACampusApp.Business.Abstracts
{
    public interface IEducationService
    {
        /// <summary>
        ///  Bu metot Education nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="educationCreateDto"></param>
        ///  <returns>SuccessDataResult<EducationCreateDto>, ErrorDataResult<EducationCreateDto></returns>
        Task<IResult> AddAsync(EducationCreateDto educationCreateDto);

        /// <summary>
        /// Bu metodu database de kayıtlı id si verilen Educationu çeker ve gösterir.
        /// </summary>
        /// /// <param name="id">detayları getirilmek istenen education nesnesinin Guid tipinde Id si </param>
        /// <returns>SuccessDataResult<EducationDetailsDto>,ErrorResult</returns>
        Task<IResult> GetDetailsAsync(Guid id);

        /// <summary>
        /// Bu metod database de kayıtlı id si verilen Educationu siler.
        /// </summary>
        /// <param name="id">silinmek istenen education nesnesinin Guid tipinde Id si </param>
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        ///  Bu metot Education nesnesini güncelleme işlemini  yapmaktadır.
        /// </summary>
        /// <param name="educationUpdateDto">update edilecek nesne</param>
        ///  <returns>SuccessResult, ErrorDataResult<EducationUpdateDto></returns>
        Task<IResult> UpdateAsync(EducationUpdateDto educationUpdateDto);

        /// <summary>
        ///  Bu metot veritabanındaki tüm eğitimleri çeker ve bu eğitim listesini EducationListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<EducationListDto>, ErrorResult</returns>       
        Task<IResult> GetListAsync();

        /// <summary>
        ///  Bu metot veritabanındaki educationsubject içeren tüm eğitimleri çeker ve bu eğitim listesini EducationListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<EducationListDto>, ErrorResult</returns>
        Task<IResult> GetEducationListThatHaveEducationSubjectAsync();

        /// <summary>
        ///  Bu metot Yöneticinin,tüm eğitimleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();
    }
}
