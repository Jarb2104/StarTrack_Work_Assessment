﻿using StarTrack_Work_Assessment.Models;

namespace StarTrack_Work_Assessment.Utilities
{
    public static class Tools
    {
        public static int GetQueryHash(QueryStackExchangeModel query) {
            HashCode createHash = new HashCode();
            createHash.Add(query.Page);
            createHash.Add(query.PageSize);
            createHash.Add(query.InTitle);
            createHash.Add(query.Site);
            return createHash.ToHashCode();
        }
    }
}