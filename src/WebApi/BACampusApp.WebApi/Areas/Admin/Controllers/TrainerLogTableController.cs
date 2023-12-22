using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.TrainerLogTable;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class TrainerLogTableController : AdminBaseController
    {
        private readonly ITrainerLogTableService _trainerLogTableService;

        public TrainerLogTableController(ITrainerLogTableService trainerLogTableService)
        {
            _trainerLogTableService = trainerLogTableService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateAsync(TrainerLogTableCreateDto trainerLogTableCreateDto)
        {
            if (!ModelState.IsValid)   return BadRequest(ModelState);

            var trainerLogTable = await _trainerLogTableService.CreateAsync(trainerLogTableCreateDto);
            return trainerLogTable.IsSuccess ? Ok(trainerLogTable) : BadRequest(trainerLogTable);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAsync()
        {
            var trainerLogTable = await _trainerLogTableService.ListAsync();
            return trainerLogTable.IsSuccess ? Ok(trainerLogTable) : BadRequest(trainerLogTable);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var trainerLogTable = await _trainerLogTableService.GetByIdAsync(id);
            return trainerLogTable.IsSuccess ? Ok(trainerLogTable) : BadRequest(trainerLogTable);
        }
    }
}
