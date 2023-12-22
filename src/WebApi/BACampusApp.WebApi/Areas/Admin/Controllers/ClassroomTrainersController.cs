using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using Microsoft.AspNetCore.Mvc;
using BACampusApp.Dtos.ClassroomTrainers;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BACampusApp.Dtos.ClassroomStudent;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class ClassroomTrainersController : AdminBaseController
    {
        private readonly IClassroomTrainersService _classroomTrainerService;

        public ClassroomTrainersController(IClassroomTrainersService classroomTrainerService)
        {
            _classroomTrainerService = classroomTrainerService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(ClassroomTrainersCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _classroomTrainerService.CreateAsync(createDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _classroomTrainerService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(ClassroomTrainersUpdateDto classroomTrainerUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _classroomTrainerService.UpdateAsync(classroomTrainerUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _classroomTrainerService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ActiveListAll()
        {
            var result = await _classroomTrainerService.ActiveListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateActive([FromBody] ClassroomTrainerActiveUpdateDto classroomTrainerActiveUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                classroomTrainerActiveUpdateDto.UserId = userId;
                var result = await _classroomTrainerService.UpdateActiveAsync(classroomTrainerActiveUpdateDto);
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
            var result = await _classroomTrainerService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> TrainersByClassroomIdAsync(Guid id)
        {
            var result = await _classroomTrainerService.TrainersByClassroomIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ClasslessTrainerList(Guid id)
        {
            var result = await _classroomTrainerService.ClasslessTrainerList(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result =await _classroomTrainerService.GetById(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}