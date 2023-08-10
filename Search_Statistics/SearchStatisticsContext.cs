using Microsoft.EntityFrameworkCore;
using SearchStatisticsDB.Entities;

namespace SearchStatisticsDB
{
    public class SearchStatisticsContext : DbContext
    {
        public DbSet<QueryResult> QueryResults { get; set; }
        public DbSet<StackExchangeCall> StackExchangeCalls { get; set; }

        public SearchStatisticsContext(DbContextOptions<SearchStatisticsContext> options) : base(options) 
        {
        }
    }
}
