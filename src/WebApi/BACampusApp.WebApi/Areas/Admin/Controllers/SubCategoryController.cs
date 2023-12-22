using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Categorys;
using BACampusApp.Dtos.SubCategory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class SubCategoryController : AdminBaseController
    {
        private readonly ISubCategoryService _subCategoryService;
       

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
           
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] SubCategoryCreateDto newSubCategoryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _subCategoryService.AddAsync(newSubCategoryCreateDto);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _subCategoryService.DeleteAsync(id);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _subCategoryService.GetListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetbyParentId(Guid parentId)
        {
            var result = await _subCategoryService.GetListByParentId(parentId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] SubCategoryUpdateDto newsubCategoryUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _subCategoryService.UpdateAsync(newsubCategoryUpdateDto);
            return updateResult.IsSuccess == true ? Ok(updateResult) : BadRequest(updateResult);

        }

    }
}
