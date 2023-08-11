using SearchStatisticsDB.Entities;
using StackExchangeQueryTracker.Models;

namespace SearchStatisticsDB.Repositories
{
    public interface IStackExchangeCallRepository
    {
        public Task<StackExchangeCall?> FindStackExchangeCall(QueryStackExchangeModel stackExchangeId);
    }
}
