using Microsoft.EntityFrameworkCore;
using SearchStatisticsDB.Entities;
using StackExchangeQueryTracker.Models;

namespace SearchStatisticsDB.Repositories
{
    internal class StackExchangeCallRepository : IStackExchangeCallRepository
    {
        private readonly SearchStatisticsContext _dbContext;

        public StackExchangeCallRepository(SearchStatisticsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<StackExchangeCall?> FindStackExchangeCall(QueryStackExchangeModel query)
        {
            return _dbContext.StackExchangeCalls
                .Where(sec => sec.Page == query.Page && sec.PageSize == query.PageSize && sec.InTitle.Equals(query.InTitle) && sec.Site.Equals(query.Site))
                .FirstOrDefaultAsync();
        }
    }
}
