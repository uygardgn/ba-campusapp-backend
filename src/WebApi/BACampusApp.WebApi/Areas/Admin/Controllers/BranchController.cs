using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Branch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class BranchController : AdminBaseController
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _branchService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _branchService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BranchCreateDto branchCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _branchService.CreateAsync(branchCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _branchService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(BranchUpdateDto branchUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _branchService.UpdateAsync(branchUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = Roles.AdminRole)] // Bu, sadece "Admin" rolüne sahip kullanıcıların erişimine izin verir
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _branchService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
