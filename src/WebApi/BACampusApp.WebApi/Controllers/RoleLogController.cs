using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.RoleLog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleLogController : ControllerBase
    {
        private readonly IRoleLogService _roleLogService;

        public RoleLogController(IRoleLogService roleLogService)
        {
            _roleLogService = roleLogService;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleLogCreateDto roleLogCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _roleLogService.CreateAsync(roleLogCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetLastRoleLogByUserId(Guid id)
        {
            var result = await _roleLogService.GetLastRoleLogByUserIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
