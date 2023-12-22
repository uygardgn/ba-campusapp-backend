using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.Business.Abstracts
{
    public interface IEducationSubjectService
    {
        /// <summary>
        /// Bu metot eğitim için konu ekleme işlemini yapar.
        /// </summary>
        /// <param name="educationSubjectDto"></param>
        Task<IResult> AddAsync(EducationSubjectCreateDto educationSubjectDto);

        /// <summary>
        /// Bu metot EduıcationSubject nesnesini silme işlemini yapar.
        /// </summary>
        /// <param name="id"></param>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        /// Bu metot EducationSubject nesnesinin güncelleme işlemini yapar.
        /// </summary>
        /// <param name="educationSubjectUpdateDto"></param>
        Task<IResult> UpdateAsync(EducationSubjectUpdateDto educationSubjectUpdateDto);

        /// <summary>
        /// Bu metot EducationSubject nesnelerini listelemeyi sağlar.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetListAsync();

        /// <summary>
        /// Bu metot içerisine gönderilen id'ye ait eğitim ve konu  getirir.
        /// </summary>
        /// <param name="id"></param>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metot Yöneticinin, tüm silinen EducationSubject nesneleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

        /// <summary>
        ///  Bu metot EducationId'ye göre listeleme yapar.
        /// </summary>        
        /// <returns></returns>  
        Task<IResult> GetSubjectsByEducationIdAsync(Guid? educationId);

		Task<IResult> GetResourceSubjectsListAsync(Guid? educationId);

        Task<IResult> CreateWithListAsync(EducationSubjectListCreateDto createListDto);

        /// <summary>
        /// Bu metot içerisine gönderilen eğitim idlerine ait konuları listeler.
        /// </summary>
        /// <param name="educationIds"></param>
        Task<IResult> GetSubjectsByEducationIdsAsync(List<Guid> educationIds);


    }
}
