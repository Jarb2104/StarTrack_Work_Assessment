using API.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchStatisticsDB;
using SearchStatisticsDB.Entities;
using SearchStatisticsDB.Repositories;
using StackExchangeQueryTracker.Models;
using StackExchangeQueryTracker.Utilities;

namespace StackExchangeQueryTracker.Controllers
{
    public class QueryController : BaseApiController
    {
        private readonly IStackExchangeCallRepository _repository;

        public QueryController(IStackExchangeCallRepository repository) {
            _repository = repository;
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
            

            return Ok("Okay!");
        }
    }
}
