namespace StackExchangeQueryTracker.Models
{
    public class PostOwner
    {
        public int account_id { get; set; }
		public int reputation { get; set; }
		public int user_id { get; set; }
		public string user_type { get; set; } = null!;
		public string profile_image { get; set; } = null!;
		public string display_name { get; set; } = null!;
		public string link { get; set; } = null!;
    }
}
