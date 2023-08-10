using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using SearchStatisticsDB;
using StackExchangeQueryTracker.Models;

namespace StackExchangeQueryTracker.Controllers
{
    public class StatisticsController : BaseApiController
    {
        private readonly SearchStatisticsContext _dbContext;

        public StatisticsController(SearchStatisticsContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(QueryStackExchangeModel query)
        {

            return Ok("Okay");
        }
    }
}
