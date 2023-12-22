using BACampusApp.Dtos.StudentLogTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IStudentLogTableService
    {
        /// <summary>
        ///  Bu metot StudentLogTable nesnesi oluşturma işlemini yapmaktadır
        /// </summary>
        /// <param name="studentLogTableCreateDto"></param>
        ///  <returns>SuccessDataResult<TrainerCreateDto>, ErrorResult</returns>
        Task<IResult> CreateAsync(StudentLogTableCreateDto studentLogTableCreateDto);
        /// <summary>
        ///  Bu metot Adminin,tüm StudentLogTable nesnelerini listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<List<TrainerListDto>>, ErrorResult</returns>        
        Task<IResult> ListAsync();
        /// <summary>
        ///  StudentLogTable nesnesinin listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Trainer nesnesinin Guid tipinde Id si</param>
        /// <returns>SuccessDataResult<TrainerDto>></returns>
        Task<IResult> GetByIdAsync(Guid id);
    }
}
