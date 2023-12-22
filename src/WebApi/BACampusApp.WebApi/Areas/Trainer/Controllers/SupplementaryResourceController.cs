using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Core.Enums;
using BACampusApp.Core.Utilities.Results;
using BACampusApp.Dtos.SupplementaryResources;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class SupplementaryResourceController : TrainerBaseController
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
            supplementaryResourceCreateDto.ResourcesTypeStatus = Core.Enums.ResourcesTypeStatus.Requested;
            var result = await _supplementaryResourceService.AddAsync(supplementaryResourceCreateDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromForm] SupplementaryResourceUpdateDto supplementaryResourceUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            supplementaryResourceUpdateDto.ResourcesTypeStatus = Core.Enums.ResourcesTypeStatus.Requested;
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
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListsAllByTagId(Guid tagId)
        {
            var result = await _supplementaryResourceService.GetAllListsByTagIdAsync(tagId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListsAllSupplementaryResourceByTrainersEducations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _supplementaryResourceService.ListsAllSupplementaryResourceByTrainersEducationsAsync(userId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}