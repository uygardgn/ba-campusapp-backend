using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.Business.Abstracts
{
    public interface IStudentHomeworkService
    {
        /// <summary>
        /// Bu metot içerisine gönderilen Liste StudentHomeworkCreateDto nesnesi ile öğrenci için ödev ataması yapar.
        /// </summary>
        /// <param name="StudentHomeworkCreateDto"></param>
        /// <returns>Öğrenciye ödev atama işleminin başarılı/başarısız olma durumu mesajı döner.</returns>
        Task<IResult> CreateAsync(List<StudentHomeworkCreateDto> studentHomeworkCreateDto);
        /// <summary>
        /// Bu metot içerisine gönderilen id'ye ait ödevi siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Silme işlemine ilişkin durum mesajı döner.</returns>
        Task<IResult> DeleteAsync(StudentHomeworkDeleteDto studentHomeworkDeleteDto);
        /// <summary>
        /// Bu metot içerisine gönderilen studentHomeworkUpdateDto nesnesi ile eğitmen/admin için ödev güncellemesi yapar.
        /// </summary>
        /// <param name="studentHomeworkUpdateDto"></param>
        /// <returns>Öğrenci ödev atama güncelleme işleminin durum mesajı döner.</returns>
        Task<IResult> TrainerHomeworkUpdateAsync(StudentHomeworkTrainerUpdateDto studentHomeworkTrainerUpdateDto);
        /// <summary>
        /// Bu metot içerisine gönderilen studentHomeworkUpdateDto nesnesi ile öğrenci için ödev güncellemesi yapar.
        /// </summary>
        /// <param name="studentHomeworkUpdateDto"></param>
        /// <returns>Öğrenci ödev atama güncelleme işleminin durum mesajı döner.</returns>
        Task<IResult> StudentHomeworkUpdateAsync(StudentHomeworkStudentUpdateDto studentHomeworkStudentUpdateDto);
        /// <summary>
        /// Bu metot öğrencilere atanan tüm ödevleri getirir.
        /// </summary>
        /// <returns>Ödev atama tablosu verilerinin listenip listelenmediği ile ilgili durum mesajı döner </returns>
        Task<IResult> GetAllListAsync();
        /// <summary>
        /// Bu metot içerisine gönderilen id'ye ait ödevi getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>İlgili ödev bilgisini döner.</returns>
        Task<IResult> GetByIdAsync(Guid id);
        /// <summary>
        /// Bu metot içerisine gönderilen studentId ve homeworkId ile öğrencinin ilgili ödevden aldığı puanı getirir.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>Öğrenciye atanmış ödev puanını döner.</returns>
        Task<IResult> GetPoint(Guid studentId, Guid homeworkId);
        /// <summary>
        /// Bu metot içerisine gönderilen studentHomeworkPointDto ile öğrencinin ilgili ödevi için puan verilebilmektedir.
        /// </summary>
        /// <param name="studentHomeworkPointDto"></param>
        /// <returns>İlgili ödev bilgilerini döner.</returns>
        Task<IResult> GivePoint(StudentHomeworkPointDto studentHomeworkPointDto);

        /// <summary>
        ///  Bu metot Yöneticinin, silinen öğrencilere atanan tüm ödevleri nesneleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

        /// <summary>
        /// Bu metot StudentHomework tablosunda secili ödeve ait öğrenci listesini dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> StudentsByHomeworkIdAsync(Guid? homeworkId);

        /// <summary>
        /// StudentHomework tablosundaki belirli bir eklenmiş ödeve ait dosyanın indirilmesi için gerekli FileContentResult döner.
        /// </summary>
        /// <param name="filePath">dosyanın dosya yolu</param>
        /// <param name="studentHomeworkId">dosyanın dosya yolu</param>
        /// <returns>FileContentResult</returns>
        /// 
        FileContentResult DownloadDocumentStudentHomework(string filePath, Guid studentHomeworkId);

        /// <summary>
        /// Bu method wwwroot>documenta>StudentHomework>DeletedStudentHomework içerisindeki dosyaların linklerini döndürür
        /// </summary>
        /// <returns></returns>
        List<FileContentResult> GetAllDeletedFiles();

        /// <summary>
        /// Bu metot öğrencinin studenthomeworkId sini bulmamızı sağlar.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        Task<IResult> GetByStudentIdAndHomeworkId(Guid studentId, Guid homeworkId);

        /// <summary>
        /// Bu metot öğrencinin ilgili ödevi için bir geri bildirim eklenebilmesini sağlar.
        /// </summary>
        /// <param name="studentHomeworkFeedbackDto"></param>
        /// <returns></returns>
        Task<IResult> GiveFeedback(StudentHomeworkFeedbackDto studentHomeworkFeedbackDto);
    }
}
