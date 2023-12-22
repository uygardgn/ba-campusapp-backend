using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Core.Utilities.Results;
using BACampusApp.Dtos.Students;
using BACampusApp.Dtos.Subjects;
using BACampusApp.Dtos.Tag;
using BACampusApp.WebApi.Areas.Trainer.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class TagController : TrainerBaseController
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }



        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _tagService.GetListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid guid)
        {
            var result = await _tagService.GetByIdAsync(guid);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

      
    }
}