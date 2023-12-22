using BACampusApp.Business.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Student.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.StudentRole)]
    [Area("Student")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class StudentBaseController : ControllerBase
    {

    }
}
