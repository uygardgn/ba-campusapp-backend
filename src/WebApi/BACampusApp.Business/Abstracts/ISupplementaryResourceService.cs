using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.Business.Abstracts
{
    public interface ISupplementaryResourceService
    {
        /// <summary>
        /// Bu metot SupplementaryResource nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="newSupplementaryResource"></param>
        /// <returns></returns>
        Task<IResult> AddAsync(SupplementaryResourceCreateDto newSupplementaryResource);

        /// <summary>
        ///  Bu metod SupplementaryResource nesnesini id'ye göre getirme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen SupplementaryResource nesnesinin Guid tipinde Id si</param>
        /// <returns>></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metod SupplementaryResource nesnelerinin tamamını listeleme işlemlerini yapmaktadır.
        /// </summary>        
        /// <returns></returns>       
        Task<IResult> GetAllListsAsync();
        /// bu metod SupplementaryResourcelarda ResourceTypeStatusu'ün statusüne göre SupplementaryResourceları getirir
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetAllListsForResourceTypeStatusAsync(ResourcesTypeStatus status);
        /// <summary>
        /// isHardDelete değerine göre silme işlemi yapan metot.
        /// </summary>
        /// <param name="supplementaryResourcesDeleteDto"></param>
        /// <returns></returns>
        Task<IResult> PermanentlyDocumentDeleteAsync(SupplementaryResourcesDeleteDto supplementaryResourcesDeleteDto);

        /// <summary>
        ///  Bu metot SupplemantaryResource nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen SupplemantaryResource nesnesinin Guid tipinde Id si </param>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        /// Bu metot SupplementaryResoutce nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="update">güncellenmek istenen SupplementaryResource nesnesinin SupplementaryResourceUpdateDto tipinde entity'si</param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(SupplementaryResourceUpdateDto update);

        /// <summary>
        /// Id'sine göre silimiş SupplemantaryResource nesnesini döndüren method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetDeletedResourceById(Guid id);

        /// <summary>
        ///  Bu metot Yöneticinin,silinen tüm SupplemantaryResource nesnelerini listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> GetDeletedResources();

        /// <summary>
        /// Soft silinmiş dosyayı kalıcı olarak siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> PermanentlyDeleteDeletedResource(Guid id);

        /// <summary>
        ///  Bu metod SupplementaryResource nesnelerinin subject Id'ye göre tamamını listeleme işlemlerini yapmaktadır.
        /// </summary>        
        /// <returns></returns>       
        //Task<IResult> GetAllListsBySubjectIdAsync(Guid subjectId);
        FileContentResult DownloadDocumentSupplementaryResource(string filePath, Guid studentHomeworkId);

        /// <summary>
        /// Bu metod seçilen tag'e göre yardımcı kaynakların listelenmesi işlevini görür. 
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
		Task<IResult> GetAllListsByTagIdAsync(Guid tagId);

        Task<IResult> GetAllListByStudentId(Guid studentId);

        Task<IResult> Recover(SupplementaryResourceRecoverDto recoverDto);

        ///// <summary>
        ///// Bu method, gelen IFormFile tipindeki dosyanın ilgili FileCategory'e göre hedef dizinde (Deleted klasörü) bulunup bulunmadığını kontrol eder.
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //bool CheckIfFileExists(IFormFile file);

        ///// <summary>
        ///// Bu method, bir kaynak oluşturulurken serverda var olan bir dosyanın (Deleted klasöründe) kullanılmak istenmesi durumda kullanılır.
        ///// </summary>
        ///// <param name="newSupplementaryResource"></param>
        ///// <returns></returns>
        //Task<IResult> AddWithExistingFileAsync(SupplementaryResourceCreateDto newSupplementaryResource);

        /// <summary>
        /// Bu method, eğitimin içinde bulunan resoruceType ve educationId'ye göre SupplementaryResource listesi döndürür. 
        /// </summary>
        /// <param name="educationId"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        Task<IResult> GetDocumentsOrVideosByEducationId(Guid educationId,ResourceType resourceType);
        /// <summary>
        /// Qualitye göre 480p ya da 360p gönderen metot
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="supplementaryResourceId"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        Task<FileContentResult> DownloadMp4SupplementaryResource(string filePath, Guid supplementaryResourceId, Quality quality);
        /// <summary>
        /// Admin yetkilisinin bekelemede ya da onaylı yardımcı kaynağın durumunu değiştirir
        /// </summary>
        /// <param name="supplementaryResourceId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<IResult> UpdateStatusAsync(Guid supplementaryResourceId, ResourcesTypeStatus status);

        Task<IResult> GiveFeedback(SupplementaryResourceFeedBackDto supplementaryResourceFeedBackDto);

        /// <summary>
        ///  Bu metod SupplementaryResource nesnelerini giriş yapan trainer a göre listeleme işlemlerini yapmaktadır.
        /// </summary>        
        /// <param name="id">giriş yapan trainer rolündeki kullanıcının identityId si</param>
        /// <returns></returns>   

        Task<IResult> ListsAllSupplementaryResourceByTrainersEducationsAsync(string id);

    }
}
