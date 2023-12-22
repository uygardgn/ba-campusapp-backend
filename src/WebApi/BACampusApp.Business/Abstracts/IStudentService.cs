namespace BACampusApp.Business.Abstracts
{
    public interface IStudentService
    {
        /// <summary>
        /// Bu metot Student nesnesinin veritabanında oluşturma işleminde kullanılmaktadır.
        /// </summary>
        /// <param name="studentCreateDto"></param>
        /// <returns>IResult</returns>
        Task<IResult> CreateAsync(StudentCreateDto studentCreateDto);
                
        /// <summary>
        ///  Bu metot Student nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen student nesnesinin Guid tipinde Id'si</param>
        /// <returns>IResult</returns>
        Task<Result> DeleteAsync(Guid id);

        /// <summary>
        /// Bu metot Student nesnesinin güncelleme işleminde kullanılmaktadır.
        /// </summary>
        /// <param name="studentUpdateDto">Güncellenmek istenen student nesnesinin StudentUpdateDto tipinde nesnesi</param>
        /// <returns>IResult</returns>
        Task<IResult> UpdateAsync(StudentUpdateDto studentUpdateDto);

        /// <summary>
        /// Bu metot tüm öğrencilerin listelenmesinde kullanılmaktadır.
        /// </summary>        
        /// <returns>IResult</returns>       
        Task<IResult> GetAllAsync();

        /// <summary>
        /// Bu metot verilen id ile eşleşen Student nesnesinin gösterilmesinde kullanılmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Student nesnesinin Guid tipinde Id si</param>
        /// <returns>SuccessDataResult<StudentDto>, ErrorDataResult<StudentDto></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metot Admin,tüm aktif öğrencilerin listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<StudentListDto>, ErrorDataResult<StudentListDto></returns>    
        Task<IResult> ActiveListAsync();
        /// <summary>
        ///  Bu metot Id ye bağlı öğrencinin aktflik durumunun değüiştirlmesini sağlamkatadır.
        /// </summary>        
        /// <returns>IResult</returns>  
        Task<IResult> UpdateActiveAsync(StudentActiveUpdateDto studentActiveUpdateDto);

        /// <summary>
        ///  Bu metot Yöneticinin,silinen tüm öğrencilerin listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

        /// <summary>
        /// Bu metot sisteme giriş yapan student rolündeki kullanıcı nesnesini getirmek için kullanılır.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>IResult</returns>
        Task<IResult> GetCurrentStudentDetailsAsync(string currentUserId);

        /// <summary>
        /// Bu metot giriş yapan student rolündeki kullanıcının kendi bilgilerini güncellemesi için kullanılır.
        /// </summary>
        /// <param name="studentUpdateDto">Güncellemesi yapılacak olan verileri içeren StudentCurrentUserUpdateDto nesnesi</param>
        /// <returns>ErrorResult,SuccessResult</returns>
        Task<IResult> UpdateCurrentStudentAsync(StudentCurrentUserUpdateDto studentUpdateDto);

        /// <summary>
        /// Bu metot studentId göre student bilgilerini getirmek için kullanılır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetDetailsByStudentIdAsync(Guid id);

    }
}
