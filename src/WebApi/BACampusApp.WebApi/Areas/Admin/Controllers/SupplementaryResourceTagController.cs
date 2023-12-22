using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResourceTags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class SupplementaryResourceTagController : AdminBaseController
    {
        private readonly ISupplementaryResourceTagService _service;
        public SupplementaryResourceTagController(ISupplementaryResourceTagService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(SupplementaryResourceTagCreateDto supplementaryResourceTagCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _service.CreateAsync(supplementaryResourceTagCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(SupplementaryResourceTagUpdateDto supplementaryResourceTagUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _service.UpdateAsync(supplementaryResourceTagUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        

    }
}
