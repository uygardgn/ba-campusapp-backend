using BACampusApp.Entities.DbSets;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class CategoryManager : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITechnicalUnitsRepository _technicalUnitsRepository;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public CategoryManager(IMapper map, ICategoryRepository categoryRepository, ITechnicalUnitsRepository technicalUnitsRepository, IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = map;
            _categoryRepository = categoryRepository;
            _technicalUnitsRepository = technicalUnitsRepository;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        ///  Bu metot Category nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="newCategory"></param>
        ///  <returns>SuccessDataResult<CategoryCreateDto>, ErrorResult</returns>
        public async Task<IResult> AddAsync(CategoryCreateDto newCategory)
        {
            bool hasCategory = await _categoryRepository.AnyAsync(s => s.Name.ToLower() == newCategory.Name.ToLower());
            if (hasCategory)
                return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);

            var technicalUnits = await _technicalUnitsRepository.GetAsync(x => x.Id == newCategory.TechnicalUnitId);
            if (technicalUnits == null)
                return new ErrorResult(_stringLocalizer[Messages.TechnicalUnitsNotFound]);

            Category category = await _categoryRepository.AddAsync(_mapper.Map<Category>(newCategory));
            await _categoryRepository.SaveChangesAsync();

            return new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(category), _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        ///  Bu metot Category nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen category nesnesinin Guid tipinde Id si </param>  
        /// <returns>SuccessResult, ErrorResult</returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingCategory = await _categoryRepository.GetByIdAsync(id);
            if (deletingCategory == null)
                return new ErrorResult(_stringLocalizer[Messages.CategoryNotFound]);
            else if (deletingCategory.SubCategories != null && deletingCategory.SubCategories.Any())
                return new ErrorResult(_stringLocalizer[Messages.SubcategoriesFound]);

            await _categoryRepository.DeleteAsync(deletingCategory);
            await _categoryRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);

        }

        /// <summary>
        ///  CategoryDto ve Category nesnelerini listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Category nesnesinin Guid tipinde Id si</param>
        /// <returns>SuccessDataResult<CategoryDto>, ErrorResult</returns>
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return new ErrorResult(_stringLocalizer[Messages.CategoryNotFound]);

            return new SuccessDataResult<CategoryDetailsDto>(_mapper.Map<CategoryDetailsDto>(category), _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        ///  Bu metot Adminin,tüm categoryleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<CategoryListDto>, ErrorResult</returns>  
        public async Task<IResult> GetListAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(x => x.ParentCategoryId == null);
            if (categories.Count() <= 0)
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<List<CategoryListDto>>(_mapper.Map<List<CategoryListDto>>(categories), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot Category nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen category nesnesinin CategoryUpdateDto tipinde entity'si</param>
        /// <returns>ErrorDataResult<CategoryUpdateDto>, SuccessDataResult<CategoryUpdateDto></returns>
        public async Task<IResult> UpdateAsync(CategoryUpdateDto entity)
        {
            bool hasCategory = await _categoryRepository.AnyAsync(s => s.Name.ToLower() == entity.Name.ToLower());
            var category = await _categoryRepository.GetByIdAsync(entity.Id);
            if (category.Name != entity.Name)
            {
                if (hasCategory)
                    return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);
            }

            if (category == null)
                return new ErrorResult(_stringLocalizer[Messages.CategoryNotFound]);

            var units = await _technicalUnitsRepository.GetAsync(x => x.Id == entity.TechnicalUnitId);
            if (units == null)
                return new ErrorResult(_stringLocalizer[Messages.TechnicalUnitsNotFound]);

            await _categoryRepository.UpdateAsync(_mapper.Map(entity, category));
            await _categoryRepository.SaveChangesAsync();

            return new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(category), _stringLocalizer[Messages.UpdateSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var category = await _categoryRepository.GetAllDeletedAsync();
            if (category.Count() <= 0)
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);

            return new SuccessDataResult<List<CategoryDeletedListDto>>(_mapper.Map<List<CategoryDeletedListDto>>(category), _stringLocalizer[Messages.ListedSuccess]);
        }


        public async Task<IResult> GetCategoriesByParentIdAsync(Guid? parentCategoryId)
        {
            IEnumerable<Category> categories;

            if (parentCategoryId == null)
            {
                categories = await _categoryRepository.GetAllAsync(c => c.ParentCategoryId == null);
            }
            else
            {
                categories = await _categoryRepository.GetAllAsync(c => c.ParentCategoryId == parentCategoryId.Value);
            }

            // Bu kısımda categories null ise, bir hata durumu olduğunu belirleyebilirsiniz.
            if (categories == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.ParentCategoryIdNotFound]);
            }

            var list = categories.ToList();

            return new SuccessDataResult<List<CategoryListByParentIdDto>>(
                _mapper.Map<List<CategoryListByParentIdDto>>(list),
                _stringLocalizer[Messages.ListedSuccess]
            );
        }

        public async Task<IResult> GetSubCategoriesByIdAsync(Guid? subCategoryId)
        {
            IEnumerable<Category> subCategories;

            if (subCategoryId == null)
            {
                subCategories = await _categoryRepository.GetAllAsync(c => c.ParentCategoryId == null);
            }
            else
            {
                subCategories = await _categoryRepository.GetAllAsync(c => c.ParentCategoryId == subCategoryId.Value);
            }

            var subCategoryDtoList = subCategories
                .Select(c => new CategoryListBySubCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentCategoryId = c.ParentCategoryId,
                })
                .ToList();

            if (!subCategoryDtoList.Any())
            {
                // Alt kategori bulunamazsa, hata döndürün veya başka bir işlem yapın.
                return new ErrorResult(_stringLocalizer[Messages.ParentCategoryIdNotFound]);
            }

            return new SuccessDataResult<List<CategoryListBySubCategoryDto>>(
                subCategoryDtoList,
                _stringLocalizer[Messages.ListedSuccess]
            );
        }

    }
}
