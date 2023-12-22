using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Subjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class SubjectController : TrainerBaseController
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
   
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _subjectService.GetListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
