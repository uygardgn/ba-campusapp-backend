using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.RoleLog;
using BACampusApp.Dtos.Students;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminRole + "," + Roles.TrainerRole + "," + Roles.StudentRole)]
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class RoleLogController : ControllerBase
    {
        private readonly IRoleLogService _roleLogService;

        public RoleLogController(IRoleLogService roleLogService)
        {
            _roleLogService = roleLogService;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleLogService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _roleLogService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllByUserId(Guid id)
        {
            var result = await _roleLogService.GetAllByUserIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
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
