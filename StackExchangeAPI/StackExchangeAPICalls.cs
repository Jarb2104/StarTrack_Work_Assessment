using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using StackExchangeQueryTracker.Models;
using StackExchangeQueryTracker.Utilities;
using System.Text.Json;

namespace StackExchangeQueryTracker.StackExchangeAPI
{
    public static class StackExchangeAPICalls
    {
        public static async Task<StackExchangeResponseModel> callSearchEndPoint(string endPoint, string URL, QueryStackExchangeModel query)
        {
            //Initializing variables
            StackExchangeResponseModel responseModel;

            //Setting up the client for the API call
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "page", query.Page.ToString() },
                { "pagesize", query.PageSize.ToString() },
                { "intitle", query.InTitle },
                { "site", query.Site }
            };

            //Doing te API call
            HttpResponseMessage response = await client.GetAsync(QueryHelpers.AddQueryString(endPoint, parameters));

            //Checking if the response was succesfull
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