using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentLogTable;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class StudentLogTableController : AdminBaseController
    {
        private readonly IStudentLogTableService _studentLogTableService;

        public StudentLogTableController(IStudentLogTableService studentLogTableService)
        {
            _studentLogTableService = studentLogTableService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateAsync(StudentLogTableCreateDto newStudentLog)
        {
            if (!ModelState.IsValid)  return BadRequest(ModelState);

            var studentLog = await _studentLogTableService.CreateAsync(newStudentLog);
            return studentLog.IsSuccess ? Ok(studentLog) : BadRequest(studentLog);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _studentLogTableService.ListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _studentLogTableService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
