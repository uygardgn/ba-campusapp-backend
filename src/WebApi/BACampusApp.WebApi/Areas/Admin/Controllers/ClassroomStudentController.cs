using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.ClassroomStudent;
using BACampusApp.Dtos.Trainers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class ClassroomStudentController : AdminBaseController
    {
        private readonly IClassroomStudentService _classroomStudentService;
        

        public ClassroomStudentController(IClassroomStudentService classroomStudentService, IActivityStateLogSevices activityStateLogSevices)
        {
            _classroomStudentService = classroomStudentService;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _classroomStudentService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ActiveListAll()
        {
            var result = await _classroomStudentService.ActiveListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _classroomStudentService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(ClassroomStudentCreateDto classroomStudentCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _classroomStudentService.CreateAsync(classroomStudentCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _classroomStudentService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> Update(ClassroomStudentUpdateDto classroomStudentUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _classroomStudentService.UpdateAsync(classroomStudentUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateActive([FromBody] ClassroomStudentActiveUpdateDto classroomStudentActiveUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                classroomStudentActiveUpdateDto.UserId = userId;
                var result = await _classroomStudentService.UpdateActiveAsync(classroomStudentActiveUpdateDto);
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
            var result = await _classroomStudentService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> StudentsByClassroomId(Guid id)
        {
            var result = await _classroomStudentService.StudentsByClassroomIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ClasslessStudentList(Guid id)
        {
            var result = await _classroomStudentService.ClasslessStudentList(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


    }
}
