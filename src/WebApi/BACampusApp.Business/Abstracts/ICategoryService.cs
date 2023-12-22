namespace BACampusApp.Business.Abstracts
{
    public interface ICategoryService
    {
		/// <summary>
		///  Bu metot Category nesnesi oluşturma işlemini yapmaktadır.
		/// </summary>
		/// <param name="newCategory"></param>
		///  <returns>SuccessDataResult<CategoryCreateDto>, ErrorResult</returns>
		Task<IResult> AddAsync(CategoryCreateDto newCategory);

        /// <summary>
        ///  CategoryDto ve Category nesnelerini listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Category nesnesinin Guid tipinde Id si</param>
        /// <returns>SuccessDataResult<CategoryDto>, ErrorResult</returns>
        Task<IResult> GetByIdAsync(Guid id);

		/// <summary>
		///  Bu metot Category nesnesi silme işlemini yapacaktır.
		/// </summary>
		/// <param name="id">silinmek  istenen category nesnesinin Guid tipinde Id si </param>  
		/// <returns>SuccessResult, ErrorResult</returns>
		Task<IResult> DeleteAsync(Guid id);

		/// <summary>
		/// Bu metot Category nesnesinin güncelleme işlemini yapacaktır.
		/// </summary>
		/// <param name="entity">güncellenmek istenen category nesnesinin CategoryUpdateDto tipinde entity'si</param>
		/// <returns>SuccessDataResult<CategoryDto>, ErrorResult</returns>
		Task<IResult> UpdateAsync(CategoryUpdateDto entity);

		/// <summary>
		///  Bu metot Adminin,tüm categoryleri listelemesini sağlamaktadır.
		/// </summary>        
		/// <returns>SuccessDataResult<CategoryListDto>, ErrorResult</returns>       
		Task<IResult> GetListAsync();

        /// <summary>
        ///  Bu metot Yöneticinin,tüm categoryleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();


		Task<IResult> GetCategoriesByParentIdAsync(Guid? parentCategoryId);

        Task<IResult> GetSubCategoriesByIdAsync(Guid? subCategoryId);
    }
}
