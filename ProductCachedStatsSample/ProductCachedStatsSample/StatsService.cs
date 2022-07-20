namespace OrderCachedStatsSample;

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
            cache.SetStats(region, category, stats);
        }

        return stats!;
    }

    private OrderStats ComputeStats(string region, string category)
    {
        IEnumerable<Order> orders = dataStore.GetOrders();
        int counter = 0;
        var finalStats = orders.Where(order => order.Region == region && order.Category == category)
            .Aggregate(new OrderStats
            {
                Region = region,
                Category = category,
                AverageSales = 0,
                TotalSales = 0
            },
            (stats, order) =>
            {
                stats.TotalSales += order.Price;
                counter++;
                return stats;
            });

        finalStats.AverageSales = finalStats.TotalSales / counter;

        return finalStats;
    }
}
