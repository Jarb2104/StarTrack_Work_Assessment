namespace StarTrack_Work_Assessment.Models
{
    public class QueryStackExchangeModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string InTitle { get; set; } = string.Empty;
        public string Site { get; set; } = string.Empty;
    }
}
