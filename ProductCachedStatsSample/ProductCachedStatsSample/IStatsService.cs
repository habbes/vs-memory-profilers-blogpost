namespace OrderCachedStatsSample;

public interface IStatsService
{
    OrderStats GetOrderStatsByRegionAndCategory(string region, string category);
}
