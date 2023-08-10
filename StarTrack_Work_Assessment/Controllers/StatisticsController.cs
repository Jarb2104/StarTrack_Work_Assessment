using Microsoft.AspNetCore.Mvc;
using Search_Statistics;
using StarTrack_Work_Assessment.Models;

namespace StarTrack_Work_Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        private readonly Search_Statistics_Context _dbContext;

        public StatisticsController(Search_Statistics_Context dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetQueryStatistics")]
        public IActionResult Get(QueryStackExchangeModel query)
        {

            return Ok("Okay");
        }
    }
}
