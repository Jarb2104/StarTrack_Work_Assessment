using Microsoft.EntityFrameworkCore.ChangeTracking;
using SearchStatisticsDB.Entities;
using StackExchangeQueryTracker.Models;

namespace SearchStatisticsDB.Repositories
{
    public interface IStackExchangeCallRepository
    {
        public Task<StackExchangeCall?> FindStackExchangeCall(QueryStackExchangeModel stackExchangeId);
        public Task<List<StackExchangeCall>> GetStackExchangeCalls(string Site, DateTime fromDate, DateTime toDate);
        public ValueTask<EntityEntry<StackExchangeCall>> AddStackExchangeCall(StackExchangeCall stackExchangeCall);
    }
}
