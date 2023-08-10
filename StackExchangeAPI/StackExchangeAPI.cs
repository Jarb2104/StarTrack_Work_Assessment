using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using StackExchangeQueryTracker.Models;
using StackExchangeQueryTracker.Utilities;
using System.Text.Json;

namespace StackExchangeQueryTracker.StackExchangeAPI
{
    public static class StackExchangeAPI
    {
        public static async Task<StackExchangeResponseModel> callSearchEndPoint(QueryStackExchangeModel query)
        {
            StackExchangeResponseModel responseModel;
            string endPoint = SearchStatisticsConfigurations.configuration.GetValue<string>("StackExchange:Query:EndPoint") ?? "";

            if (string.IsNullOrWhiteSpace(query.Site))
            {
                query.Site = SearchStatisticsConfigurations.configuration.GetValue<string>("StackExchange:Query:Site") ?? "";
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(SearchStatisticsConfigurations.configuration.GetValue<string>("StackExchange:Query:URL") ?? "");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("page", query.Page.ToString());
            parameters.Add("pagesize", query.PageSize.ToString());
            parameters.Add("intitle", query.InTitle);
            parameters.Add("site", query.Site);

            HttpResponseMessage response = await client.GetAsync(QueryHelpers.AddQueryString(endPoint, parameters));

            if (response.IsSuccessStatusCode)
            {
                responseModel = await JsonSerializer.DeserializeAsync<StackExchangeResponseModel>(response.Content.ReadAsStream()) ?? new StackExchangeResponseModel();
            }else
            {
                responseModel = new StackExchangeResponseModel();
            }

            return responseModel;
        }
    }
}