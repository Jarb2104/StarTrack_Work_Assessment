using System.ComponentModel.DataAnnotations;

namespace SearchStatisticsDB.Entities
{
    //Parameters used to call the StackExchange API
    public class StackExchangeCall
    {
        [Key]
        public Guid QueryID { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        [Required]
        public string InTitle { get; set; } = string.Empty;
        [Required] 
        public string Site { get; set; } = string.Empty;

        [Required]
        public ICollection<QueryResult> Results { get; set; } = null!;
    }
}