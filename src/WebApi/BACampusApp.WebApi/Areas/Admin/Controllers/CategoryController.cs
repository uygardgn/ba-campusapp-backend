using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Core.Utilities.Results;
using BACampusApp.Dtos.Categorys;
using BACampusApp.Dtos.Educations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper map, ICategoryService categoryService)
        {
            _mapper = map;
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto newCategoryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.AddAsync(newCategoryCreateDto);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Details(Guid guid)
        {

            var result = await _categoryService.GetByIdAsync(guid);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _categoryService.UpdateAsync(model);
            return updateResult.IsSuccess == true ? Ok(updateResult) : BadRequest(updateResult);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _categoryService.GetListAsync();

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
         
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = Roles.AdminRole)] // Bu, sadece "Admin" rolüne sahip kullanıcıların erişimine izin verir
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _categoryService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListByParentId(Guid? parentCategoryId)
        {
            var result = await _categoryService.GetCategoriesByParentIdAsync(parentCategoryId);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSubCategoriesByIdAsync(Guid? subCategoryId)
        {
            var result = await _categoryService.GetCategoriesByParentIdAsync(subCategoryId);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
