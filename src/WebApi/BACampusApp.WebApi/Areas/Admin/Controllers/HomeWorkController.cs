using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Educations;
using BACampusApp.Dtos.HomeWork;
using BACampusApp.Dtos.Students;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class HomeWorkController : AdminBaseController
    {
        private readonly IHomeWorkService _homeWorkService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeWorkController(IHomeWorkService homeWorkService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _homeWorkService = homeWorkService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromForm] HomeWorkCreateDto newHomeWorkCreate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _homeWorkService.AddAsync(newHomeWorkCreate);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> PermanentlyDocumentDelete([FromBody] HomeWorkDeleteDto homeWorkDeleteDto)
        {
            var deleteResult = await _homeWorkService.PermanentlyDocumentDeleteAsync(homeWorkDeleteDto);

            if (!deleteResult.IsSuccess)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(deleteResult);
            }
        }

		[HttpDelete]
		[Route("[action]")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleteResult = await _homeWorkService.DeleteAsync(id);
			return deleteResult.IsSuccess == true ? Ok(deleteResult) : BadRequest(ModelState);

		}



		[HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromForm] HomeWorkUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _homeWorkService.UpdateAsync(model);
            return updateResult.IsSuccess == true ? Ok(updateResult) : BadRequest(updateResult);
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
        [Route("[action]")]
        [Authorize(Roles = Roles.AdminRole)] // Bu, sadece "Admin" rolüne sahip kullanıcıların erişimine izin verir
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _homeWorkService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public FileContentResult DownloadDocumentHomework(string filePath,Guid homeworkId)
        {
            var result = _homeWorkService.DownloadDocumentHomework(filePath, homeworkId);
            return result;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult DeletedFiles()
        {
            var result = _homeWorkService.GetAllDeletedFiles();
            return result.Any() ? Ok(result) : NotFound(result);
        }
    }
}

