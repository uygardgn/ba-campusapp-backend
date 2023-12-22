using BACampusApp.Business.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class AdminDashboardController : AdminBaseController
    {
        private readonly IReportService _reportService;
        public AdminDashboardController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ActiveStudentCount()
        {
            var result = await _reportService.ActiveStudentCountAsync();

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ActiveTrainerCount()
        {
            var result = await _reportService.ActiveTrainerCountAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> AllActiveUsersCount()
        {
            var result = await _reportService.ActiveAllUserCount();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
