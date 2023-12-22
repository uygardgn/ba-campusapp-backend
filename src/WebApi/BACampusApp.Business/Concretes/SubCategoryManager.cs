using BACampusApp.Core.Entities.Interfaces;
using BACampusApp.DataAccess.Interfaces.Repositories;
using BACampusApp.Dtos.SubCategory;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public readonly ITechnicalUnitsRepository _technicalUnitsRepository;
        public readonly ICategoryRepository _categoryRepository;

        public SubCategoryManager(IMapper map, ISubCategoryRepository subCategoryRepository, ITechnicalUnitsRepository technicalUnitsRepository, IStringLocalizer<Resource> stringLocalizer, ICategoryRepository categoryRepository)
        {
            _mapper = map;
            _subCategoryRepository = subCategoryRepository;
            _technicalUnitsRepository = technicalUnitsRepository;
            _stringLocalizer = stringLocalizer;
            _categoryRepository = categoryRepository;
        }

        public async Task<IResult> AddAsync(SubCategoryCreateDto newSubCategory)
        {
            bool hasSubCategory = await _subCategoryRepository.AnyAsync(s => s.Name.ToLower() == newSubCategory.Name.ToLower());
            if (hasSubCategory)
                return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);

            var technicalUnits = await _categoryRepository.GetAsync(x => x.TechnicalUnitId == newSubCategory.TechnicalUnitId);
            if (technicalUnits == null)
                return new ErrorResult(_stringLocalizer[Messages.TechnicalUnitsNotFound]);

            Category subCategory = await _subCategoryRepository.AddAsync(_mapper.Map<Category>(newSubCategory));
            await _subCategoryRepository.SaveChangesAsync();

            return new SuccessDataResult<SubCategoryDto>(_mapper.Map<SubCategoryDto>(subCategory), _stringLocalizer[Messages.AddSuccess]);
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingSubCategory = await _subCategoryRepository.GetByIdAsync(id);
            if (deletingSubCategory == null)
                return new ErrorResult(_stringLocalizer[Messages.CategoryNotFound]);

            if (deletingSubCategory.Educations.Any())
                return new ErrorResult(_stringLocalizer[Messages.SubCategoryCanNotDelete]);




            await _subCategoryRepository.DeleteAsync(deletingSubCategory);
            await _subCategoryRepository.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        public async Task<IResult> GetListByParentId(Guid parentId)
        {
            var subCategories = await _subCategoryRepository.GetAllAsync(sc => sc.ParentCategoryId == parentId);

            if (!subCategories.Any(sc => sc.Status == Status.Active))
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<List<SubCategoryDto>>(_mapper.Map<List<SubCategoryDto>>(subCategories), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetListAsync()
        {
            var subCategories = await _subCategoryRepository.GetAllAsync(x => x.ParentCategoryId != null);

            if (!subCategories.Any())
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<List<SubCategoryDto>>(_mapper.Map<List<SubCategoryDto>>(subCategories), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> UpdateAsync(SubCategoryUpdateDto entity)
        {
            bool hasSubCategory = await _subCategoryRepository.AnyAsync(s => s.Name.ToLower() == entity.Name.ToLower());
            var subcategory = await _subCategoryRepository.GetByIdAsync(entity.Id);
            if(subcategory.Name != entity.Name)
            if (hasSubCategory)
                return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);

            if (subcategory == null)
                return new ErrorResult(_stringLocalizer[Messages.SubcategoriesNotFound]);

            var parents = await _categoryRepository.GetAsync(x => x.ParentCategoryId == entity.ParentCategoryId);
            if (subcategory == null)
                return new ErrorResult(_stringLocalizer[Messages.ParentCategoryIdNotFound]);


            await _subCategoryRepository.UpdateAsync(_mapper.Map(entity, subcategory));
            await _subCategoryRepository.SaveChangesAsync();

            return new SuccessDataResult<SubCategoryDto>(_mapper.Map<SubCategoryDto>(subcategory), _stringLocalizer[Messages.UpdateSuccess]);
        }
    }
}
