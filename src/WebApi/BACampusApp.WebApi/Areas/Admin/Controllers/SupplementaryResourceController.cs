using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Core.Enums;
using BACampusApp.Dtos.StudentHomework;
using BACampusApp.Dtos.SupplementaryResources;
using BACampusApp.Entities.DbSets;
using BACampusApp.Entities.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography.Pkcs;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class SupplementaryResourceController : AdminBaseController
    {
        private readonly ISupplementaryResourceService _supplementaryResourceService;

        public SupplementaryResourceController(ISupplementaryResourceService supplementaryResourceService)
        {
            _supplementaryResourceService = supplementaryResourceService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListsAll()
        {
            var result = await _supplementaryResourceService.GetAllListsAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListsAllForResourceTypeStatus(ResourcesTypeStatus status)
        {
            var result = await _supplementaryResourceService.GetAllListsForResourceTypeStatusAsync(status);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _supplementaryResourceService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
            //return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromForm] SupplementaryResourceCreateDto supplementaryResourceCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            supplementaryResourceCreateDto.ResourcesTypeStatus = Core.Enums.ResourcesTypeStatus.Approved;
            var result = await _supplementaryResourceService.AddAsync(supplementaryResourceCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromForm] SupplementaryResourceUpdateDto supplementaryResourceUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            supplementaryResourceUpdateDto.ResourcesTypeStatus = Core.Enums.ResourcesTypeStatus.Approved;
            var result = await _supplementaryResourceService.UpdateAsync(supplementaryResourceUpdateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> PermanentlyDocumentDelete([FromForm] SupplementaryResourcesDeleteDto supplementaryResourcesDeleteDto)
        {
            var deleteResult = await _supplementaryResourceService.PermanentlyDocumentDeleteAsync(supplementaryResourcesDeleteDto);

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
            var deleteResult = await _supplementaryResourceService.DeleteAsync(id);
            return deleteResult.IsSuccess == true ? Ok(deleteResult) : BadRequest(ModelState);

        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> DeletedResources()
        {
            var result = await _supplementaryResourceService.GetDeletedResources();
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> DeletedResource(Guid id)
        {
            var result = await _supplementaryResourceService.GetDeletedResourceById(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> PermanentlyDeleteDeledResource(Guid id)
        {
            var result = await _supplementaryResourceService.PermanentlyDeleteDeletedResource(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Recover([FromForm] SupplementaryResourceRecoverDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _supplementaryResourceService.Recover(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        //[HttpGet]
        //[Route("[action]")]
        //public async Task<IActionResult> ListsAllBySubjectId(Guid subjectId)
        //{
        //    var result = await _supplementaryResourceService.GetAllListsBySubjectIdAsync(subjectId);
        //    return result.IsSuccess ? Ok(result) : BadRequest(result);
        //}
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListsAllByTagId(Guid tagId)
        {
            var result = await _supplementaryResourceService.GetAllListsByTagIdAsync(tagId);
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
        public  async Task<FileContentResult> DownloadVideoSupplementaryResource(string filePath, Guid supplementaryResourceId, Quality quality)
        {
            var result =await _supplementaryResourceService.DownloadMp4SupplementaryResource(filePath, supplementaryResourceId, quality);
            return result;

        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateStatus(Guid supplementaryResourceId, ResourcesTypeStatus status)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _supplementaryResourceService.UpdateStatusAsync(supplementaryResourceId, status);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> GiveFeedback(SupplementaryResourceFeedBackDto supplementaryResourceFeedBackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _supplementaryResourceService.GiveFeedback(supplementaryResourceFeedBackDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}