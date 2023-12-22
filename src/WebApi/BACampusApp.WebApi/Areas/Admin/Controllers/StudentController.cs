using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Students;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    
    public class StudentController : AdminBaseController
    {
        private readonly IStudentService _studentService;
        private readonly ISupplementaryResourceService _supplementaryResourceService;

        public StudentController(IStudentService studentService,ISupplementaryResourceService supplementaryResourceService)
        {
            _studentService = studentService;
            _supplementaryResourceService = supplementaryResourceService;
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto newStudent)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _studentService.CreateAsync(newStudent);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllSupplementaryResources()
        {
            var result = await _supplementaryResourceService.GetAllListsAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        } 

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _studentService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] StudentUpdateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _studentService.UpdateAsync(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ActiveListAll()
        {
            var result = await _studentService.ActiveListAsync();

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _studentService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateActive([FromBody] StudentActiveUpdateDto studentActiveUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                studentActiveUpdateDto.UserId = userId;
                var result = await _studentService.UpdateActiveAsync(studentActiveUpdateDto);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            else
            {
                return BadRequest("Invalid or missing User ID claim.");
            }

        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = Roles.AdminRole)] // Bu, sadece "Admin" rolüne sahip kullanıcıların erişimine izin verir
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _studentService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
