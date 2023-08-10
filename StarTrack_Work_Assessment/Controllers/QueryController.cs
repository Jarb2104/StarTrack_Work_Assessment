using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Search_Statistics;
using Search_Statistics.Entities;
using StarTrack_Work_Assessment.Models;
using StarTrack_Work_Assessment.Utilities;

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
        public IActionResult Get(QueryStackExchangeModel query)
        {
            int hashSearch;

            if (string.IsNullOrWhiteSpace(query.Site))
            {
                query.Site = SearchStatisticsConfigurations.configuration.GetValue<string>("StackExchange:Query:Site");
            }

            hashSearch = Tools.GetQueryHash(query);
            
            IEnumerable<SiteQuery> result = 
                _dbContext.SiteQueries
                    .Include(sq => sq.Results)
                    .Where(sq => sq.QueryID == hashSearch)
                    .AsEnumerable();

            return Ok("Okay!");
        }
    }
}
