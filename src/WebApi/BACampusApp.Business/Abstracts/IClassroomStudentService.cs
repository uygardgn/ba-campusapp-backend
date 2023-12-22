namespace BACampusApp.Business.Abstracts
{
    public interface IClassroomStudentService
    {
        /// <summary>
        /// Bu metot yeni bir ClassroomStudent nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="classroomStudentCreateDto">Oluşturulacak nesnenin verilerini içeren ClassroomStudentCreateDto nesnesi.</param>
        /// <returns>IResult</returns>
        Task<IResult> CreateAsync(ClassroomStudentCreateDto classroomStudentCreateDto);
        /// <summary>
        /// Bu metot verilen id'ye göre ClassroomStudent nesnesi silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id">Silinecek nesne için verilen id</param>
        /// <returns>IResult</returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Bu metot verilen ClassroomStudent nesnesini güncellemek için kullanılmaktadır.
        /// </summary>
        /// <param name="classroomStudentUpdateDto">Güncellenecek nesne için verilen güncel bilgilerin yer aldığı ClassroomStudentUpdateDto nesnesi</param>
        /// <returns>IResult</returns>
        Task<IResult> UpdateAsync(ClassroomStudentUpdateDto classroomStudentUpdateDto);
        /// <summary>
        /// Bu metot tüm ClassromStudent nesnlerini liste olarak dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>IResult</returns>
        Task<IResult> GetAllAsync();
        /// <summary>
        /// Bu metot verilen id'ye göre ClassroomStudent nesnesini geri göndürmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id">Veritabanından getirilecek olan ClassroomStudent nesnesi için verilen id</param>
        /// <returns>IResult</returns>
        Task<IResult> GetByIdAsync(Guid id);
        /// <summary>
        /// Bu metot tüm ClassromStudent nesnlerinin aktif olanlarını liste olarak dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>IResult</returns>
        Task<IResult> ActiveListAsync();

        /// <summary>
        /// Bu metot classroomStudent nesnesinin aktiflik durumunu güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="classroomStudentActiveUpdate"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<IResult> UpdateActiveAsync(ClassroomStudentActiveUpdateDto classroomStudentActiveUpdate);

        /// <summary>
        ///  Bu metot Yöneticinin,tüm ClassromStudent nesnlerini listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

        /// <summary>
        /// Bu metot ClassromStudent tablosunda secili sınıfa ait öğrenci listesini dönmek için kullanılmaktadır.
        /// </summary>    
        /// <returns></returns>         
        Task<IResult> StudentsByClassroomIdAsync(Guid? clasroomId);

        /// <summary>
        /// Bu metot ClassromStudent tablosunda secili sınıfa ait olayan öğrencilerin listesini dönmek için kullanılmaktadır.
        /// </summary>     
        /// <returns></returns>         
        Task<IResult> ClasslessStudentList(Guid? clasroomId);

        /// <summary>
        /// Bu metot ClassromStudent tablosunda secili öğrenciye ait tüm sınıf bilgisini dönmek için kullanılır
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<IResult> AllClassroomsByStudentIdAsync(Guid? studentId);

    }
}
