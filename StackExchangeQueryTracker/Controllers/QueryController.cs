using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using SearchStatisticsDB.Entities;
using SearchStatisticsDB.Repositories;
using StackExchangeQueryTracker.Models;
using StackExchangeQueryTracker.Utilities;
using StackExchangeQueryTracker.StackExchangeAPI;

namespace StackExchangeQueryTracker.Controllers
{
    //Controller to call the StackExchange API.
    public class QueryController : BaseApiController
    {
        private readonly ISearchStatisticsDBRepository _repository;
        private readonly IConfiguration _configuration;

        //Initializing repository for DB connection.
        public QueryController(ISearchStatisticsDBRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(QueryStackExchangeModel query)
        {
            int hashSearch;
            string endPoint = _configuration.GetValue<string>("StackExchange:Query:EndPoint") ?? "";
            string URL = _configuration.GetValue<string>("StackExchange:Query:URL") ?? "";
            query.Site = _configuration.GetValue<string>("StackExchange:Query:Site") ?? "";

            if (string.IsNullOrWhiteSpace(endPoint) || string.IsNullOrWhiteSpace(URL))
            {
                throw new Exception("Unable to get configuration");
            }

            //Making the call to the endpoint of StackExchange.
            StackExchangeResponseModel stackExchangeResponse = await StackExchangeAPICalls.callSearchEndPoint(endPoint, URL, query);

            //Doing a hash for each call to be able to find old calls.
            hashSearch = Tools.GetQueryHash(query.Page, query.PageSize, query.InTitle, query.Site);

            //Checking if a previous call with the same parameters exists.
            StackExchangeCall? seachQueryStatisticsCall = await _repository.StackExchangeCall.FindStackExchangeCall(hashSearch);

            return Ok(seachQueryStatisticsCall);
        }
    }
}
