using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.Business.Abstracts
{
    public interface IHomeWorkService
    { /// <summary>
      /// Bu metot Homework nesnesini oluşturma işlemini yapmaktadır.
      /// </summary>
      /// <param name="newHomeWork"></param>
      /// <returns></returns>
        Task<IResult> AddAsync(HomeWorkCreateDto newHomeWork);

		/// <summary>
		///  Bu metot HomeWork nesnesi silme işlemini yapacaktır.
		/// </summary>
		/// <param name="id">silinmek  istenen HomeWork nesnesinin Guid tipinde Id si </param>
		/// 
		///  
		Task<IResult> DeleteAsync(Guid id);

		/// <summary>
		/// Bu metot belge yüklenen ödevler için belgeyi saklama/kalıcı olarak silme işlevini yerine getirir.
		/// </summary>
		/// <param name="homeWorkDeleteDto"> metoda hardDelete=true/false ve id parametreleri gönderilir.</param>
		/// <returns></returns>
		Task<IResult> PermanentlyDocumentDeleteAsync(HomeWorkDeleteDto homeWorkDeleteDto);

		/// <summary>
		///  Bu metot tüm ödevlerin listelemesini sağlamaktadır.
		/// </summary>        
		/// <returns>SuccessDataResult<HomeWorkListDto>, ErrorDataResult<HomeWorkListDto></returns>       
		Task<IResult> ListAsync();

        /// <summary>
        /// Bu metot Student nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen student nesnesinin StudentUpdateDto tipinde entity'si</param>
        /// <returns>ErrorDataResult<StudentUpdateDto>, SuccessDataResult<StudentUpdateDto></returns>
        Task<IResult> UpdateAsync(HomeWorkUpdateDto updateHomework);

        /// <summary>
        ///  HomeWorkGetDto ve HomeWork nesnelerini listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen HomeWork nesnesinin Guid tipinde Id si</param>
        /// <returns>SuccessDataResult<HomeWorkDto>, ErrorDataResult<HomeWorkDto></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Belirli bir ödeve ait dosyanın indirilmesi için gerekli FileContentResult döner.
        /// </summary>
        /// <param name="filePath">dosyanın dosya yolu</param>
        /// <param name="homeworkId">dosyanın dosya yolu</param>
        /// <returns>FileContentResult</returns>
        /// 
        FileContentResult DownloadDocumentHomework(string filePath,Guid homeworkId);

        /// <summary>
        /// Bu method wwwroot>documenta>Homework>DeletedHomework içerisindeki dosyaların linklerini döndürür
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="homeworkId"></param>
        /// <returns></returns>
        List<FileContentResult> GetAllDeletedFiles();

        /// <summary>
        ///  Bu metot Yöneticinin, tüm silinen HomeWork nesneleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

        /// <summary>
        /// Bu method uygulamaya giriş yapan trainer'ın id'si ile sadece kendisi tarafından oluşturulmuş ödev listesini görüntüler
        /// </summary>
        /// <param name="trainerId"></param>
        /// <returns></returns>
        Task<IResult> GetHomeworkByTrainer(Guid trainerId);

        /// <summary>
        /// Bu method uygulamaya giriş yapan öğrenci'nın id'si ile sadece kendisine atanmış ödevleri listeler
        /// </summary>
        /// <param name="trainerId"></param>
        /// <returns></returns>
        Task<IResult> GetAllHomeworkByStudentId(Guid studentId);
    }
}
