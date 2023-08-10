using Microsoft.EntityFrameworkCore;
using SearchStatisticsDB.Entities;

namespace SearchStatisticsDB
{
    public class SearchStatisticsContext : DbContext
    {
        //Generating table relations with DB
        public DbSet<QueryResult> QueryResults { get; set; }
        public DbSet<StackExchangeCall> StackExchangeCalls { get; set; }

        //Generating context to connect to DB
        public SearchStatisticsContext(DbContextOptions<SearchStatisticsContext> options) : base(options) { }
    }
}
