using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.ClassroomStudent;
using BACampusApp.Dtos.Trainers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class ClassroomStudentController : TrainerBaseController
    {
        private readonly IClassroomStudentService _classroomStudentService;
        

        public ClassroomStudentController(IClassroomStudentService classroomStudentService, IActivityStateLogSevices activityStateLogSevices)
        {
            _classroomStudentService = classroomStudentService;
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
