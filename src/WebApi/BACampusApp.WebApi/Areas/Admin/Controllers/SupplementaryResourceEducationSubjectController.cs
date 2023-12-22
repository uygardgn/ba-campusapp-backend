using BACampusApp.Business.Abstracts;
using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using BACampusApp.Dtos.SupplementaryResourceTags;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class SupplementaryResourceEducationSubjectController : AdminBaseController
    {

        private readonly ISupplementaryResourceEducationSubjectService _service;
        public SupplementaryResourceEducationSubjectController(ISupplementaryResourceEducationSubjectService service)
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
        public async Task<IActionResult> Create(SupplementaryResourceEducationSubjectCreateDto supplementaryResourceEducationSubjectCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _service.CreateAsync(supplementaryResourceEducationSubjectCreateDto);
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
        public async Task<IActionResult> Update(SupplementaryResourceEducationSubjectUpdateDto supplementaryResourceEducationSubjectUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _service.UpdateAsync(supplementaryResourceEducationSubjectUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


    }
}
