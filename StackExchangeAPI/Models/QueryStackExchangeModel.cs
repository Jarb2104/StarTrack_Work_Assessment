namespace StackExchangeQueryTracker.Models
{
    //Model to call the endpoint for calling the StackExchange endpoint
    public class QueryStackExchangeModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string InTitle { get; set; } = string.Empty;
        public string Site { get; set; } = string.Empty;
    }
}
