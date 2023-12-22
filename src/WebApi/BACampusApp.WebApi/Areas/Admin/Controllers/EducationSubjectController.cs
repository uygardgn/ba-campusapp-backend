using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.EducationSubject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class EducationSubjectController : AdminBaseController
    {
        private readonly IEducationSubjectService _educationSubjectService;

        public EducationSubjectController(IEducationSubjectService educationSubjectService)
        {
            _educationSubjectService = educationSubjectService;
        }
       
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] EducationSubjectCreateDto educationSubjectCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _educationSubjectService.AddAsync(educationSubjectCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(EducationSubjectUpdateDto educationSubjectUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _educationSubjectService.UpdateAsync(educationSubjectUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
           
            var result = await _educationSubjectService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

 
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _educationSubjectService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

      
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _educationSubjectService.GetListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = Roles.AdminRole)] // Bu, sadece "Admin" rolüne sahip kullanıcıların erişimine izin verir
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _educationSubjectService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListByEducationtId(Guid? educationId)
        {
            var result = await _educationSubjectService.GetSubjectsByEducationIdAsync(educationId.Value);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> ResourceSubjectsList(Guid? educationId)
		{
			var result = await _educationSubjectService.GetResourceSubjectsListAsync(educationId.Value);

			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> CreateWithList([FromBody] EducationSubjectListCreateDto createListDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _educationSubjectService.CreateWithListAsync(createListDto);
			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}




        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSubjectsByEducationIds([FromForm] List<Guid> educationIds)
        {
            if (educationIds == null || educationIds.Count == 0)
            {
                return BadRequest();
            }

            var result = await _educationSubjectService.GetSubjectsByEducationIdsAsync(educationIds);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
