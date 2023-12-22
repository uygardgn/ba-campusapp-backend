using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.AdminRole)]
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AdminBaseController : ControllerBase
    {  

    }
}
