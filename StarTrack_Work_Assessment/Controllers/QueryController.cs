using Microsoft.AspNetCore.Mvc;
using Search_Statistics;

namespace StarTrack_Work_Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : Controller
    {
        private readonly Search_Statistics_Context _dbContext;

        public QueryController(Search_Statistics_Context dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet(Name = "QueryStackExchange")]
        public IActionResult Get()
        {
            return Ok("Okay!");
        }
    }
}
