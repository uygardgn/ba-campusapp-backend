using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using Microsoft.AspNetCore.Mvc;
using BACampusApp.Dtos.ClassroomTrainers;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BACampusApp.Dtos.ClassroomStudent;

namespace BACampusApp.WebApi.Areas.Student.Controllers
{
    public class ClassroomTrainersController : StudentBaseController
    {
        private readonly IClassroomTrainersService _classroomTrainerService;

        public ClassroomTrainersController(IClassroomTrainersService classroomTrainerService)
        {
            _classroomTrainerService = classroomTrainerService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> TrainersByClassroomIdAsync(Guid id)
        {
            var result = await _classroomTrainerService.TrainersByClassroomIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}