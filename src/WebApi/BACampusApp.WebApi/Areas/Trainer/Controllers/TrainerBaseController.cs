using BACampusApp.Business.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.TrainerRole)]
    [Area("Trainer")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class TrainerBaseController : ControllerBase
    {

    }
}
