using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using SearchStatisticsDB.Entities;
using SearchStatisticsDB.Repositories;
using SearchStatisticsDB.ModelToEntity;
using StackExchangeQueryTracker.Models;
using Microsoft.Extensions.Caching.Memory;

namespace StackExchangeQueryTracker.Controllers
{
    public class StatisticsController : BaseApiController
    {
        private readonly ISearchStatisticsDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public StatisticsController(ISearchStatisticsDBRepository repository, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _repository = repository;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        [HttpGet("{fromDate:datetime}&{toDate:datetime}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(DateTime fromDate, DateTime toDate)
        {
            string site = _configuration.GetValue<string>("StackExchange:Query:Site") ?? "";
            string cacheKey = fromDate.ToString() + toDate.ToString() + site;
            List<StackExchangeCall> searchResult;

            if (!_memoryCache.TryGetValue(cacheKey, out searchResult!))
            {
                searchResult = await _repository.StackExchangeCall.GetStackExchangeCalls(site, fromDate, toDate);
            }

            if (searchResult.Count() == 0)
            {
                return NoContent();
            }

            SearchStatisticsModel response = StackExchangeCallToEntity.StackExchangeCallEntityToModel(searchResult);
            response.FromDate = fromDate;
            response.ToDate = toDate;

            return Ok(response);
        }
    }
}
