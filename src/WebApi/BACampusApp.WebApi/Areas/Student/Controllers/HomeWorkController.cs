using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Educations;
using BACampusApp.Dtos.HomeWork;
using BACampusApp.Dtos.Students;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Student.Controllers
{
    public class HomeWorkController : StudentBaseController
    {
        private readonly IHomeWorkService _homeWorkService;
        private readonly IMapper _mapper;

        public HomeWorkController(IHomeWorkService homeWorkService, IMapper mapper)
        {
            _homeWorkService = homeWorkService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _homeWorkService.ListAsync();

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid guid)
        {
            var result = await _homeWorkService.GetByIdAsync(guid);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]/{studentId}")]
        public async Task<IActionResult> ListAssignments(Guid studentId)
        {
            var result = await _homeWorkService.GetAllHomeworkByStudentId(studentId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}

