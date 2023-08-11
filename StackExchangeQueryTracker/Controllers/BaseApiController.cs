using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Base api controller to have a controller based name.
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    { }
}

