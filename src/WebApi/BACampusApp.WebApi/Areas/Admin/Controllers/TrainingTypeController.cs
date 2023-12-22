using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Branch;
using BACampusApp.Dtos.TrainingType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class TrainingTypeController : AdminBaseController
    {
        private readonly ITrainingTypeService _trainingTypeService;
        public TrainingTypeController (ITrainingTypeService trainingTypeService)
        {
            _trainingTypeService = trainingTypeService;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _trainingTypeService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _trainingTypeService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(TrainingTypeCreateDto trainingTypeCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _trainingTypeService.CreateAsync(trainingTypeCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _trainingTypeService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(TrainingTypeUpdateDto trainingTypeUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _trainingTypeService.UpdateAsync(trainingTypeUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = Roles.AdminRole)] // Bu, sadece "Admin" rolüne sahip kullanıcıların erişimine izin verir
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _trainingTypeService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
