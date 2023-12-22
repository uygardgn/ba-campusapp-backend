using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.Core.Utilities.Results;
using BACampusApp.Dtos.HomeWork;
using BACampusApp.Dtos.StudentHomework;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class StudentHomeworkController : AdminBaseController
    {
        private readonly IStudentHomeworkService _studentHomeworkService;

        public StudentHomeworkController(IStudentHomeworkService studentHomeworkService)
        {
            _studentHomeworkService = studentHomeworkService;
        }


        /// <summary>
        /// Bu metot içerisine gönderilen Liste StudentHomeworkCreateDto nesnesi ile öğrenci için ödev ataması yapar.
        /// </summary>
        /// <param name="StudentHomeworkCreateDto"></param>
        /// <returns>Durum mesajı döner.</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(List<StudentHomeworkCreateDto> studentHomeworkCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.AddFail);
            }

            var result = await _studentHomeworkService.CreateAsync(studentHomeworkCreateDto);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Bu metot içerisine gönderilen id'ye ait ödevi günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Durum mesajı döner.</returns>
        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] StudentHomeworkTrainerUpdateDto studentHomeworkUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _studentHomeworkService.TrainerHomeworkUpdateAsync(studentHomeworkUpdateDto);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
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
        /// Bu metot içerisine gönderilen studentHomeworkUpdateDto nesnesi ile öğrenci için ödev güncellemesi yapar.
        /// </summary>
        /// <param name="studentHomeworkUpdateDto"></param>
        /// <returns>Güncellenen öğrenci bilgilerini ve durum mesajı döner.</returns>
        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] StudentHomeworkDeleteDto studentHomeworkDeleteDto)
        {
            var result = await _studentHomeworkService.DeleteAsync(studentHomeworkDeleteDto);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Bu metot içerisine gönderilen id'ye ait ödevi getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>İlgili ödev bilgisini döner</returns>
        [Route("[action]")]
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
        /// Bu metot içerisine gönderilen studentHomeworkPointDto ile öğrencinin ilgili ödevi için puan verilebilmektedir.
        /// </summary>
        /// <param name="studentHomeworkPointDto"></param>
        /// <returns>İlgili ödev bilgilerini döner.</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> GivePoint(StudentHomeworkPointDto studentHomeworkPointDto)
        {
            var point = await _studentHomeworkService.GivePoint(studentHomeworkPointDto);
            return point.IsSuccess ? Ok(point) : BadRequest(point);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _studentHomeworkService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> StudentsByHomeworkId(Guid id)
        {
            var result = await _studentHomeworkService.StudentsByHomeworkIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public FileContentResult DownloadDocumentStudentHomework(string filePath, Guid studentHomeworkId)
        {
            var result = _studentHomeworkService.DownloadDocumentStudentHomework(filePath, studentHomeworkId);
            return result;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult DeletedFiles()
        {
            var result = _studentHomeworkService.GetAllDeletedFiles();
            return result.Any() ? Ok(result) : NotFound(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> GiveFeedback(StudentHomeworkFeedbackDto studentHomeworkFeedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _studentHomeworkService.GiveFeedback(studentHomeworkFeedbackDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
