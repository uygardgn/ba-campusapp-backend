using BACampusApp.Dtos.GroupType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IGroupTypeService
    {
        /// <summary>
        /// Bu metot Branch nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="groupTypeCreateDto"></param>
        /// <returns></returns>
        Task<IResult> CreateAsync(GroupTypeCreateDto groupTypeCreateDto);
        /// <summary>
        /// Bu metot Branch nesnesi silme işlemini yapmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Bu metot branch nesnesi güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="branchUpdateDto"></param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(GroupTypeUpdateDto groupTypeUpdateDto);
        /// <summary>
        /// Bu metot tüm branch listesini getirme işlemi yapmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetAllAsync();
        /// <summary>
        /// Bu metot girilen id ye göre branch nesnesi getirme işlemi yapmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);
        /// <summary>
        ///  Bu metot Yöneticinin,tüm branchların listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();
    }
}
