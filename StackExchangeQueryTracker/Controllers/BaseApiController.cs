using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Base api controller to have a controller based name.
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    { }
}

