﻿using System.Collections.Concurrent;

namespace OrderCachedStatsSample;

public class OrderStatsCache : IOrderStatsCache
{
    private ConcurrentDictionary<CacheKey, OrderStats> cache = new();
    public void SetStats(string region, string category, OrderStats stats)
    {
        cache.AddOrUpdate(new CacheKey(region, category), stats, (key, existingStats) => stats);
    }

    public bool TryGetStats(string region, string category, out OrderStats? stats)
    {
        return cache.TryGetValue(new CacheKey(region, category), out stats);
    }

    private class CacheKey
    {
        public CacheKey(string region, string category)
        {
            Region = region;
            Category = category;
        }
        public string Region { get; }
        public string Category { get; }
    }
}
