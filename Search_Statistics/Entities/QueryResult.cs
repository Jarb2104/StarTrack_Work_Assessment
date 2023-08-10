namespace Search_Statistics.Entities
{
    public class QueryResult
    {
        public int ResultID { get; set; }
        public string Tittle { get; set; } = null!;
        public int AnswerCount { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PicURL { get; set; } = string.Empty;
        public DateTime QueryDate { get; set; }
    }
}
