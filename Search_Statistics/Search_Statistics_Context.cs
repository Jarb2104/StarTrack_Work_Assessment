using Microsoft.EntityFrameworkCore;
using Search_Statistics.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search_Statistics
{
    public class Search_Statistics_Context : DbContext
    {
        public DbSet<QueryResult> QueryResults { get; set; }
        public DbSet<SiteQuery> SiteQueries { get; set; }

        public Search_Statistics_Context(DbContextOptions<Search_Statistics_Context> options) : base(options) 
        {
        }
    }
}
