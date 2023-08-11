using API.Controllers;
using Microsoft.AspNetCore.Mvc;
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

        //Initializing repository for DB connection.
        public QueryController(ISearchStatisticsDBRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
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
            bool newQuery = false;

            //Server error if configuration not found.
            if (string.IsNullOrWhiteSpace(endPoint) || string.IsNullOrWhiteSpace(URL))
            {
                throw new Exception("Unable to get configuration");
            }

            //Making the call to the endpoint of StackExchange.
            StackExchangeResponseModel stackExchangeResponse = await StackExchangeAPICalls.callSearchEndPoint(endPoint, URL, query);
            //Checking if a previous call with the same parameters exists.
            StackExchangeCall? seachQueryStatisticsCall = await _repository.StackExchangeCall.FindStackExchangeCall(query);

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
                StackExchangeCallToEntity.StackExchangeResponseModelToEntity(stackExchangeResponse, seachQueryStatisticsCall);

                newQuery = true;
            }
            
            seachQueryStatisticsCall.TimesRequested++;
            seachQueryStatisticsCall.LastTimeRequested = DateTime.Now;

            try
            {
                if (newQuery) await _repository.StackExchangeCall.AddStackExchangeCall(seachQueryStatisticsCall);
                await _repository.Save();
            }
            catch (Exception)
            {
                return NoContent();
            }
            
            return Ok(seachQueryStatisticsCall);
        }
    }
}
