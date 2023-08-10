using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using SearchStatisticsDB.Entities;
using SearchStatisticsDB.Repositories;
using StackExchangeQueryTracker.Models;
using StackExchangeQueryTracker.Utilities;
using StackExchangeQueryTracker.StackExchangeAPI;

namespace StackExchangeQueryTracker.Controllers
{
    public class QueryController : BaseApiController
    {
        private readonly ISearchStatisticsDBRepository _repository;

        public QueryController(ISearchStatisticsDBRepository repository) {
            _repository = repository;
        }

        [HttpGet(Name = "QueryStackExchange")]
        public IActionResult Get(QueryStackExchangeModel query)
        {
            int hashSearch;
            
            StackExchangeAPI
            hashSearch = Tools.GetQueryHash(query.Page, query.PageSize, query.InTitle, query.Site);
            ValueTask<StackExchangeCall?> SeachQueryStatisticsCall = _repository.StackExchangeCall.FindStackExchangeCall(hashSearch);


            return Ok("Okay!");
        }
    }
}
