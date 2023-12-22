using BACampusApp.Dtos.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface ISubCategoryService
    {
        /// <summary>
		///  Bu metot yeni bir Alt Kategori için Category nesnesi oluşturma işlemini yapmaktadır.
		/// </summary>
		/// <param name="newSubCategory"></param>
		///  <returns>SuccessDataResult<CategoryCreateDto>, ErrorResult</returns>
		Task<IResult> AddAsync(SubCategoryCreateDto newSubCategory);

        /// <summary>
        ///  Bu metot Alt kategoriler için Category nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen category nesnesinin Guid tipinde Id si </param>  
        /// <returns>SuccessResult, ErrorResult</returns>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        ///  Bu metot Adminin, aktif durumdaki Alt Kategorileri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<CategoryListDto>, ErrorResult</returns>       
        Task<IResult> GetListAsync();

        /// <summary>
        /// Bu method ilgili kategorinin altındaki alt kategorilerin listesini döner.
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<IResult> GetListByParentId(Guid parentId);

        /// <summary>
		/// Bu metot SubCategory nesnesinin güncelleme işlemini yapacaktır.
		/// </summary>
		/// <param name="entity">güncellenmek istenen subCategory nesnesinin SubCategoryUpdateDto tipinde entity'si</param>
		/// <returns>SuccessDataResult<SubCategoryDto>, ErrorResult</returns>
        Task<IResult> UpdateAsync(SubCategoryUpdateDto entity);
    }
}
