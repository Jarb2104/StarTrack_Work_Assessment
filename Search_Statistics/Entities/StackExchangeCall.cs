using System.ComponentModel.DataAnnotations;

namespace SearchStatisticsDB.Entities
{
    public class StackExchangeCall
    {
        public int QueryID { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        [Required]
        public string QueryText { get; set; } = string.Empty;

        [Required]
        public ICollection<QueryResult> Results { get; set; } = null!;
    }
}