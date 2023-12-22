using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Admin;
using BACampusApp.Dtos.Students;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BACampusApp.WebApi.Areas.Student.Controllers
{
    public class StudentController : StudentBaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCurrentStudentDetails()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var result = await _studentService.GetCurrentStudentDetailsAsync(userId);
            if (result is null || !result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateCurrentStudent([FromForm]StudentCurrentUserUpdateDto updateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateDto.IdentityId = userId;
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _studentService.UpdateCurrentStudentAsync(updateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetDetailsByStudentId(Guid studentId)
        {
            var result = await _studentService.GetDetailsByStudentIdAsync(studentId);
            if (result is null || !result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
