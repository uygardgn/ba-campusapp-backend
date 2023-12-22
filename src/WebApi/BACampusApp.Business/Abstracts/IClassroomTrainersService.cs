namespace BACampusApp.Business.Abstracts
{
    public interface IClassroomTrainersService
    {
        /// <summary>
        /// Bu metot yeni bir ClassroomTrainer nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="classroomTrainerCreateDto"></param>
        /// <returns>SuccessDataResult<ClassroomTrainerDto>, ErrorResult</returns>
        Task<IResult> CreateAsync(ClassroomTrainersCreateDto classroomTrainerCreateDto);
        /// <summary>
        /// Bu metot mevcut ClassroomTrainer nesnesini silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Bu metot ClassroomTrainer nesnesini güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="classTrainerUpdateDto"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<IResult> UpdateAsync(ClassroomTrainersUpdateDto classTrainerUpdateDto);
        /// <summary>
        /// Bu metot veritabanındaki tüm ClassroomTrainer nesnelerini liste şeklinde göstermek için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetAllAsync();
        /// <summary>
        /// Bu metot tüm ClassroomTrainer listesinin aktif olanlarını getirme işlemi yapmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> ActiveListAsync();
        /// <summary>
        /// Bu metot classroomTrainer nesnesinin aktiflik durumunu güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="classroomTrainerActiveUpdateDto"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<IResult> UpdateActiveAsync(ClassroomTrainerActiveUpdateDto classroomTrainerActiveUpdateDto);

        /// <summary>
        ///  Bu metot Yöneticinin,tüm ClassroomTrainer nesnelerini listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

        /// <summary>
        /// Bu metot ClassromTrainer tablosunda secili sınıfa ait eğitmen listesini dönmek için kullanılmaktadır.
        /// </summary>    
        /// <returns></returns>         
        Task<IResult> TrainersByClassroomIdAsync(Guid? clasroomId);
        /// <summary>
        /// Bu metot ClassromTrainer tablosunda secili sınıfa ait olayan eğitmenlerin listesini dönmek için kullanılmaktadır.
        /// </summary>     
        /// <returns></returns>         
        Task<IResult> ClasslessTrainerList(Guid? clasroomId);
        /// <summary>
        /// Bu method ClassroomTrainerId'sine göre ClassRoomTrainer öğesini dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetById(Guid id);

        /// <summary>
        /// Bu metot, belirli bir eğitmenin ait olduğu sınıf odalarını listelemek için kullanılır.
        /// </summary>
        /// <param name="trainerId"></param>
        /// <returns></returns>
        Task<IResult> ClassroomsByTrainerIdAsync(Guid trainerId);

    }
}
