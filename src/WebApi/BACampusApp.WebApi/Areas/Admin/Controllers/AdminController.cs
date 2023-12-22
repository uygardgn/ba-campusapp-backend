using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Concretes;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Admin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class AdminController : AdminBaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _adminService.GetAllAsync();
            return Ok(result);
        }
        
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _adminService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateDto adminCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _adminService.CreateAsync(adminCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpDelete()]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _adminService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> Update(AdminUpdateDto adminUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _adminService.UpdateAsync(adminUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult>GetByIdentityId(string identityId)
        {
            var result = await _adminService.GetByIdentityId(identityId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult>GetCurrentAdminDetails()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var result = await _adminService.GetCurrentAdminDetailsAsync(userId);
            if (result is null || !result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateCurrentAdmin([FromForm]AdminCurrentUserUpdateDto updateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateDto.IdentityId = userId;
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _adminService.UpdateCurrentAdminAsync(updateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateLoggedInAdmin([FromForm] AdminLoggedInUserUpdateDto updateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateDto.IdentityId = userId;
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _adminService.UpdateLoggedInAdminAsync(updateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
