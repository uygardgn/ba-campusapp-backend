namespace BACampusApp.Business.Abstracts
{
    public interface IClassroomService
    {
        /// <summary>
        /// Bu metot Classroom nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="classroomCreateDto"></param>
        /// <returns></returns>
        Task<IResult> CreateAsync(ClassroomCreateDto classroomCreateDto);
        /// <summary>
        /// Bu metot Classroom nesnesi silme işlemini yapmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Bu metot Classroom nesnesi güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="classroomUpdateDto"></param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(ClassroomUpdateDto classroomUpdateDto);
        /// <summary>
        /// Bu metot tüm classroom listesini getirme işlemi yapmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetAllAsync();
        /// <summary>
        /// Bu metot girilen id ye göre classroom nesnesi getirme işlemi yapmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);
        /// <summary>
        /// Bu metot girilen id ye göre classroom nesnesinin teknik detaylarını getirme işlemi yapmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetDetails(Guid id);
        /// <summary>
        /// Bu metot tüm aktif olan classroomların listesini getirme işlemi yapmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> ActiveListAsync();
        /// <summary>
        ///  Bu metot Id ye bağlı classroom un aktiflik durumunun değüiştirlmesini sağlamkatadır.
        /// </summary>        
        /// <returns>IResult</returns>  
        Task<IResult> UpdateActiveAsync(ClassroomActiveUpdateDto classroomActiveUpdateDto);

        /// <summary>
        ///  Bu metot Yöneticinin,tüm classroomların listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

        /// <summary>
        /// Bu metot classroom ıd ye göre öğrenci sayısını getirmektedir.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetStudentsCountByClassroomId(Guid id);

        /// <summary>
        /// Bu metot classroom ıd ye göre trainer adı ve soyadını getirmektedir.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetTrainersByClassroomId(Guid id);

        /// <summary>
        /// Bu metot educationId ye göre aktif sınıf gruplarının listelenmesini sağlamaktadır
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetActiveClassroomsByEducationId(Guid educationId);
    }
}
