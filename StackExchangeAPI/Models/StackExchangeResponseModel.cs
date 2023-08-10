namespace StackExchangeQueryTracker.Models
{
    //Full StackExchange return data model
    public class StackExchangeResponseModel
    {
        public StackOverflowPost[] items { get; set; } = null!;
        public bool has_more { get; set; }
	    public int quota_max { get; set; }
	    public int quota_remaining { get; set; }
    }
}
