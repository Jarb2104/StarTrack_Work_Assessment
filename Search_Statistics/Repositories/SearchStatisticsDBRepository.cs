namespace SearchStatisticsDB.Repositories
{
    public class SearchStatisticsDBRepository : ISearchStatisticsDBRepository
    {
        private readonly SearchStatisticsContext _context;
        private StackExchangeCallRepository? _callRepository;

        public SearchStatisticsDBRepository(SearchStatisticsContext context)
        {
            _context = context;
        }

        public IStackExchangeCallRepository StackExchangeCall
        {
            get { return _callRepository ??= new StackExchangeCallRepository(_context); }
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
