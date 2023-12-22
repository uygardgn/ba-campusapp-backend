using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.TechnicalUnits;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class TechnicalUnitsController : AdminBaseController
    {
        private readonly IMapper _mapper;
        private readonly ITechnicalUnitsService _technicalUnitsService;

        public TechnicalUnitsController(IMapper mapper, ITechnicalUnitsService technicalUnitsService)
        {
            _mapper = mapper;
            _technicalUnitsService = technicalUnitsService;
        }
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] TUnitCreateDto newUnitsCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _technicalUnitsService.AddAsync(newUnitsCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Details(Guid guid)
        {

            var result = await _technicalUnitsService.GetByIdAsync(guid);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _technicalUnitsService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] TUnitUpdateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _technicalUnitsService.UpdateAsync(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _technicalUnitsService.GetListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = Roles.AdminRole)] // Bu, sadece "Admin" rolüne sahip kullanıcıların erişimine izin verir
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _technicalUnitsService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
