namespace StackExchangeQueryTracker.Utilities
{
    public static class Tools
    {
        public static int GetQueryHash(int Page, int PageSize, string InTitle, string Site) {
            HashCode createHash = new HashCode();
            createHash.Add(Page);
            createHash.Add(PageSize);
            createHash.Add(InTitle);
            createHash.Add(Site);
            return createHash.ToHashCode();
        }
    }
}
