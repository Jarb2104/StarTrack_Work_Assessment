using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
                .Include(sec => sec.Results)
                .Where(sec => sec.Page == query.Page && sec.PageSize == query.PageSize && sec.InTitle.Equals(query.InTitle) && sec.Site.Equals(query.Site))
                .SingleOrDefaultAsync();
        }

        public Task<List<StackExchangeCall>> GetStackExchangeCalls(string site, DateTime fromDate, DateTime toDate)
        {
            return _dbContext.StackExchangeCalls
                .Include(sec => sec.Results)
                .Where(sec => sec.LastTimeRequested > fromDate && sec.LastTimeRequested < toDate && sec.Site.Equals(site))
                .ToListAsync();
        }

        public ValueTask<EntityEntry<StackExchangeCall>> AddStackExchangeCall(StackExchangeCall stackExchangeCall)
        {
            return _dbContext.StackExchangeCalls.AddAsync(stackExchangeCall);
        }
    }
}
