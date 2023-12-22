using BACampusApp.Dtos.TrainerLogTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface ITrainerLogTableService
    {
        /// <summary>
        ///  Bu metot Trainer nesnesi oluşturma işlemini yapmaktadır
        /// </summary>
        /// <param name="trainerLogTableCreateDto"></param>
        ///  <returns>SuccessDataResult<TrainerCreateDto>, ErrorResult</returns>
        Task<IResult> CreateAsync(TrainerLogTableCreateDto trainerLogTableCreateDto);
        /// <summary>
        ///  Bu metot Adminin,tüm TrainerLogTable nesnelerini listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<List<TrainerListDto>>, ErrorResult</returns>        
        Task<IResult> ListAsync();
        /// <summary>
        ///  TrainerLogTable nesnesinin listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Trainer nesnesinin Guid tipinde Id si</param>
        /// <returns>SuccessDataResult<TrainerDto>></returns>
        Task<IResult> GetByIdAsync(Guid id);
        /// <summary>
        ///  Bu metot Yöneticinin,tüm eğitmenleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        //Task<IResult> DeletedListAsync();
        /// <summary>
        ///  Bu metot,tüm aktif eğitmenlerin listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<TrainerListDto>, ErrorDataResult<TrainerListDto></returns>    
        //Task<IResult> ActiveListAsync();
        /// <summary>
        /// Bu metot TrainerLogTable nesnesinin aktiflik durumunu güncelleme işlemini yapmaktadır.
        /// </summary>
        //// <param name="trainerActiveUpdateDto"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        /// Task<IResult> UpdateActiveAsync(TrainerActiveUpdateDto trainerActiveUpdateDto);
    }
}
