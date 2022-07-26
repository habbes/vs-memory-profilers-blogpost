namespace OrderStatsSampleService;

public class StatsService : IStatsService
{
    private readonly IDataStore dataStore;
    private readonly IOrderStatsCache cache;

    public StatsService(IDataStore dataStore, IOrderStatsCache cache)
    {
        this.dataStore = dataStore;
        this.cache = cache;
    }

    public OrderStats GetOrderStatsByRegionAndCategory(string region, string category)
    {
        OrderStats? stats = null;
        if (!cache.TryGetStats(region, category, out stats))
        {
            stats = ComputeStats(region, category);
            if (stats.Count > 0)
            {
                cache.SetStats(region, category, stats);
            }
        }

        return stats!;
    }

    private OrderStats ComputeStats(string region, string category)
    {
        IEnumerable<Order> orders = dataStore.GetOrders();
        var finalStats = orders.Where(order => order.Region == region && order.Category == category)
            .Aggregate(new OrderStats
            {
                Region = region,
                Category = category,
                AverageSales = 0,
                TotalSales = 0,
                Count = 0
            },
            (stats, order) =>
            {
                stats.TotalSales += order.Price;
                stats.Count++;
                return stats;
            });

        if (finalStats.Count > 0)
        {
            finalStats.AverageSales = finalStats.TotalSales / finalStats.Count;
        }

        return finalStats;
    }
}
