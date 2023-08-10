namespace SearchStatisticsDB.Repositories
{
    public interface ISearchStatisticsDBRepository
    {
        IStackExchangeCallRepository StackExchangeCall { get; }
        Task<int> Save();
    }
}
