using BACampusApp.Business.Abstracts;
using BACampusApp.Dtos.Educations;
using BACampusApp.Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BACampusApp.Business.Constants;
using BACampusApp.WebApi.Areas.Trainer.Controllers;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class EducationController : TrainerBaseController
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] EducationCreateDto newEducationCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _educationService.AddAsync(newEducationCreate);
            return result.IsSuccess? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Details(Guid guid)
        {
            var result = await _educationService.GetDetailsAsync(guid);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result=await _educationService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
 
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] EducationUpdateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _educationService.UpdateAsync(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _educationService.GetListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllThatHaveEducationSubject()
        {
            var result = await _educationService.GetEducationListThatHaveEducationSubjectAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
