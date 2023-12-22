using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Students;
using BACampusApp.Dtos.Trainers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class TrainerController : TrainerBaseController
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCurrentTrainerDetails()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var result = await _trainerService.GetCurrentTrainerAsync(userId);
            if (result is null || !result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateCurrentTrainer([FromForm] TrainerCurrentUserUpdateDto updateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            updateDto.IdentityId = userId;
            if (string.IsNullOrEmpty(userId))
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _trainerService.UpdateCurrentTrainerAsync(updateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
