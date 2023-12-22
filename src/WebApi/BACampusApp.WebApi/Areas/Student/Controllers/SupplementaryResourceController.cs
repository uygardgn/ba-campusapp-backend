using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.SupplementaryResources;
using BACampusApp.Entities.DbSets;
using BACampusApp.Entities.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography.Pkcs;

namespace BACampusApp.WebApi.Areas.Student.Controllers
{
    public class SupplementaryResourceController : StudentBaseController
    {
        private readonly ISupplementaryResourceService _supplementaryResourceService;

        public SupplementaryResourceController(ISupplementaryResourceService supplementaryResourceService)
        {
            _supplementaryResourceService = supplementaryResourceService;
        }

        [HttpGet]
        [Route("[action]/{studentId}")]
        public async Task<IActionResult> ListsAll(Guid studentId)
        {
            var result = await _supplementaryResourceService.GetAllListByStudentId(studentId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _supplementaryResourceService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(SupplementaryResourceCreateDto supplementaryResourceCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _supplementaryResourceService.AddAsync(supplementaryResourceCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(SupplementaryResourceUpdateDto supplementaryResourceUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _supplementaryResourceService.UpdateAsync(supplementaryResourceUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromForm]SupplementaryResourcesDeleteDto supplementaryResourcesDeleteDto)
        {
            var result = await _supplementaryResourceService.DeleteAsync(supplementaryResourcesDeleteDto.Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

		[HttpGet]
		[Route("[action]")]
		public FileContentResult DownloadDocumentSupplementaryResource(string filePath, Guid supplementaryResourceId)
		{
			var result = _supplementaryResourceService.DownloadDocumentSupplementaryResource(filePath, supplementaryResourceId);
			return result;
		}

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetDocumentsOrVideosByEducationId(Guid educationId, ResourceType resourceType)
        {
            var result = await _supplementaryResourceService.GetDocumentsOrVideosByEducationId(educationId, resourceType);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<FileContentResult> DownloadVideoSupplementaryResource(string filePath, Guid supplementaryResourceId, Quality quality)
        {
            var result = await _supplementaryResourceService.DownloadMp4SupplementaryResource(filePath, supplementaryResourceId, quality);
            return result;

        }
    }
}