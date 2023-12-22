using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using Microsoft.AspNetCore.Mvc;
using BACampusApp.Dtos.ClassroomTrainers;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BACampusApp.Dtos.ClassroomStudent;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class ClassroomTrainersController : TrainerBaseController
    {
        private readonly IClassroomTrainersService _classroomTrainerService;

        public ClassroomTrainersController(IClassroomTrainersService classroomTrainerService)
        {
            _classroomTrainerService = classroomTrainerService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ClassroomsByTrainerIdAsync(Guid id)
        {
            var result = await _classroomTrainerService.ClassroomsByTrainerIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


    }
}