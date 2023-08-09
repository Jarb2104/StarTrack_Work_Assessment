using System.ComponentModel.DataAnnotations;

namespace Search_Statistics.Entities
{
    public class SiteQuery
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