namespace StarTrack_Work_Assessment.Models
{
    public class StackOverflowPost
    {
        public string[] tags { get; set; } = null!;
        public PostOwner owner { get; set; } = null!;
		public bool is_answered { get; set; }
		public int view_count { get; set; }
		public int answer_count { get; set; }
		public int score { get; set; }
		public int last_activity_date { get; set; }
		public int creation_date { get; set; }
		public int last_edit_date { get; set; }
		public int question_id { get; set; }
		public string content_license { get; set; } = null!;
		public string link { get; set; } = null!;
		public string title { get; set; } = null!;
    }
}
