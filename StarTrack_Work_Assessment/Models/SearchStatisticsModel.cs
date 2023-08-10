namespace StackExchangeQueryTracker.Models
{
    public class SearchStatisticsModel
    {
        public int SiteID { get; set; }
        public string SiteURL { get; set; } = string.Empty;
        public DateTime LastQuery { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int SearchesDone { get; set; }
    }
}
