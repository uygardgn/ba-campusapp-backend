using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Core.Utilities.Results;
using BACampusApp.DataAccess.Interfaces.Repositories;
using BACampusApp.Dtos.HomeWork;
using BACampusApp.Dtos.StudentHomework;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Security.Claims;

namespace BACampusApp.WebApi.Areas.Student.Controllers
{
    public class StudentHomeworkController : StudentBaseController
    {
        private readonly IStudentHomeworkService _studentHomeworkService;
        private readonly IStudentRepository _studentRepository;
        private readonly IHomeWorkService _homeWorkService;

        public StudentHomeworkController(IStudentHomeworkService studentHomeworkService, IStudentRepository studentRepository, IHomeWorkService homeWorkService)
        {
            _studentHomeworkService = studentHomeworkService;
            _studentRepository = studentRepository;
            _homeWorkService = homeWorkService;
        }        

		/// <summary>
		/// Bu metot içerisine gönderilen id'ye ait ödevi günceller.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Durum mesajı döner.</returns>
		[Route("[action]")]
		[HttpPut]
		public async Task<IActionResult> StudentUpdate([FromForm] StudentHomeworkStudentUpdateDto studentHomeworkUpdateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _studentHomeworkService.StudentHomeworkUpdateAsync(studentHomeworkUpdateDto);
			return result.IsSuccess == true ? Ok(result) : BadRequest(result);
		}


        /// <summary>
        /// Bu metot içerisine gönderilen id'ye ait ödevi getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>İlgili ödev bilgisini döner</returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _studentHomeworkService.GetByIdAsync(id);
			return result.IsSuccess == true ? Ok(result) : BadRequest(result);
		}

        /// <summary>
        /// Bu metot öğrencilere atanan tüm ödevleri getirir.
        /// </summary>
        /// <returns>Öğrencilerin tüm ödevlerini döner.</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentHomeworkService.GetAllListAsync();
			return result.IsSuccess == true ? Ok(result) : BadRequest(result);
		}

        /// <summary>
        /// Bu metot içerisine gönderilen studentId ve homeworkId ile öğrencinin ilgili ödevden aldığı puanı getirir.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>Öğrenciye atanmış ödev puanını döner.</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetPoint(Guid studentId, Guid homeworkId)
        {
            var studentHomeworkPoint = await _studentHomeworkService.GetPoint(studentId, homeworkId);
            return studentHomeworkPoint.IsSuccess == true ? Ok(studentHomeworkPoint) : BadRequest(Messages.ListedFail);
        }

        /// <summary>
        /// Bu metot öğrencinin studenthomeworkId sini bulmamızı sağlar.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetStudentHomework(Guid studentId, Guid homeworkId)
        {
            var studentHomework = await _studentHomeworkService.GetByStudentIdAndHomeworkId(studentId, homeworkId);
            return studentHomework.IsSuccess == true ? Ok(studentHomework) : BadRequest(studentHomework);    
        }

        [HttpGet]
        [Route("[action]")]
        public FileContentResult DownloadDocumentHomework(string filePath, Guid homeworkId)
        {
            var result = _homeWorkService.DownloadDocumentHomework(filePath, homeworkId);
            return result;
        }
    }
}
