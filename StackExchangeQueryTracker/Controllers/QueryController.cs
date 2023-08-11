using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SearchStatisticsDB.Entities;
using SearchStatisticsDB.ModelToEntity;
using SearchStatisticsDB.Repositories;
using StackExchangeQueryTracker.Models;
using StackExchangeQueryTracker.StackExchangeAPI;

namespace StackExchangeQueryTracker.Controllers
{
    //Controller to call the StackExchange API.
    public class QueryController : BaseApiController
    {
        private readonly ISearchStatisticsDBRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        //Initializing repository for DB connection.
        public QueryController(ISearchStatisticsDBRepository repository, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _repository = repository;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(QueryStackExchangeModel query)
        {
            if (query.Page <= 0)
            {
                return BadRequest($"Incorrect Page value: {query.Page}");
            }

            if (query.PageSize <= 0)
            {
                return BadRequest($"Incorrect PageSize value: {query.PageSize}");
            }

            if (string.IsNullOrEmpty(query.InTitle))
            {
                return BadRequest($"InTitle value is empty");
            }

            //Getting the configuration need from config file.
            string endPoint = _configuration.GetValue<string>("StackExchange:Query:EndPoint") ?? "";
            string URL = _configuration.GetValue<string>("StackExchange:Query:URL") ?? "";
            query.Site = _configuration.GetValue<string>("StackExchange:Query:Site") ?? "";
            string cacheKey = query.Page.ToString() + query.PageSize.ToString() + query.InTitle + query.Site;
            bool newQuery = false;
            StackExchangeResponseModel? stackExchangeResponse;
            StackExchangeCall? seachQueryStatisticsCall;

            //Server error if configuration not found.
            if (string.IsNullOrWhiteSpace(endPoint) || string.IsNullOrWhiteSpace(URL))
            {
                throw new Exception("Unable to get configuration");
            }

            //Checking cache for data
            if (!_memoryCache.TryGetValue(cacheKey, out stackExchangeResponse!))
            {
                //Making the call to the endpoint of StackExchange.
                stackExchangeResponse = await StackExchangeAPICalls.callSearchEndPoint(endPoint, URL, query);
                if (stackExchangeResponse != null)
                {
                    MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                        .SetPriority(CacheItemPriority.Normal);

                    _memoryCache.Set(cacheKey, stackExchangeResponse, cacheEntryOptions);
                }
            }


            //Checking if a previous call with the same parameters exists.
            seachQueryStatisticsCall = await _repository.StackExchangeCall.FindStackExchangeCall(query);
            if (seachQueryStatisticsCall == null)
            {
                //Create a new call object with the query data.
                seachQueryStatisticsCall = new StackExchangeCall();
                seachQueryStatisticsCall.QueryID = Guid.NewGuid();
                seachQueryStatisticsCall.Page = query.Page;
                seachQueryStatisticsCall.PageSize = query.PageSize;
                seachQueryStatisticsCall.InTitle = query.InTitle;
                seachQueryStatisticsCall.Site = query.Site;
                seachQueryStatisticsCall.FirstTimeRequested = DateTime.Now;

                if (stackExchangeResponse != null) StackExchangeCallToEntity.StackExchangeResponseModelToEntity(stackExchangeResponse, seachQueryStatisticsCall);
                newQuery = true;
            }

            //Since this data is updated with every call, cache for it loses meaning as it has to be updated with every call, hence why seachQueryStatisticsCall doesn't get stored in cache.
            seachQueryStatisticsCall.TimesRequested++;
            seachQueryStatisticsCall.LastTimeRequested = DateTime.Now;

            try
            {
                if (newQuery) await _repository.StackExchangeCall.AddStackExchangeCall(seachQueryStatisticsCall);
                await _repository.Save();
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to save new data: " + ex.Message);
            }

            return Ok(seachQueryStatisticsCall);
        }
    }
}
