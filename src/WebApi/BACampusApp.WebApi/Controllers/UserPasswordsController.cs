using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.UserPasswords;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminRole + "," + Roles.TrainerRole + "," + Roles.StudentRole)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPasswordsController : ControllerBase
    {
        private readonly IUserPasswordsService _userPasswordsService;

        public UserPasswordsController(IUserPasswordsService userPasswordsService)
        {
            _userPasswordsService = userPasswordsService;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserPasswordsCreateDto userPasswordsCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userPasswordsService.CreateAsync(userPasswordsCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllByUserId(Guid id)
        {
            var result = await _userPasswordsService.GetAllByUserIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userPasswordsService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
