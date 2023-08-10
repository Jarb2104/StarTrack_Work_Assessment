using SearchStatisticsDB.Entities;

namespace SearchStatisticsDB.Repositories
{
    public interface IStackExchangeCallRepository
    {
        public ValueTask<StackExchangeCall?> FindStackExchangeCall(int stackExchangeId);
    }
}
