using SearchStatisticsDB.Entities;

namespace SearchStatisticsDB.Repositories
{
    internal class StackExchangeCallRepository : IStackExchangeCallRepository
    {
        private readonly SearchStatisticsContext _dbContext;

        public StackExchangeCallRepository(SearchStatisticsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ValueTask<StackExchangeCall?> FindStackExchangeCall(int stackExchangeId)
        {
            return _dbContext.StackExchangeCalls.FindAsync(stackExchangeId);
        }
    }
}
