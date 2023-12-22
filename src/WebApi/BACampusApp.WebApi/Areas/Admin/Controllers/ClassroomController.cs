using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Classroom;
using BACampusApp.Dtos.Students;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class ClassroomController : AdminBaseController
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _classroomService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ActiveListAll()
        {
            var result = await _classroomService.ActiveListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _classroomService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(ClassroomCreateDto classroomCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _classroomService.CreateAsync(classroomCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _classroomService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(ClassroomUpdateDto classroomUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _classroomService.UpdateAsync(classroomUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> TechnicDetails(Guid id)
        {
            var result = await _classroomService.GetDetails(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateActive([FromBody] ClassroomActiveUpdateDto classroomActiveUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                classroomActiveUpdateDto.UserId = userId;
                var result = await _classroomService.UpdateActiveAsync(classroomActiveUpdateDto);
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
            var result = await _classroomService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetStudentsCountByClassroomId(Guid id)
        {
            var result = await _classroomService.GetStudentsCountByClassroomId(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetTrainersByClassroomId(Guid id)
        {
            var result = await _classroomService.GetTrainersByClassroomId(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetActiveClassroomsByEducationId(Guid id)
        {
            var result = await _classroomService.GetActiveClassroomsByEducationId(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
