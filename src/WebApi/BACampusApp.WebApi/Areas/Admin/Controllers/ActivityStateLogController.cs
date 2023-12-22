using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class ActivityStateLogController : AdminBaseController
    {
        private readonly IActivityStateLogSevices _activityStateLogService;

        public ActivityStateLogController(IActivityStateLogSevices activityStateLogService)
        {
            _activityStateLogService = activityStateLogService;

        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _activityStateLogService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _activityStateLogService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ListByItemId(string role)
        {
            var result = await _activityStateLogService.GetAllAsync(role);
            return Ok(result);
        }
    }
}
