using System.ComponentModel.DataAnnotations;

namespace SearchStatisticsDB.Entities
{
    //The results from calling the StackExchange API
    public class QueryResult
    {
        [Key]
        public Guid ResultID { get; set; }
        public string Tittle { get; set; } = null!;
        public int AnswerCount { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PicURL { get; set; } = string.Empty;
    }
}
