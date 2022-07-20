namespace OrderCachedStatsSample;

public interface IOrderStatsCache
{
    public void SetStats(string region, string category, OrderStats stats);
    public bool TryGetStats(string region, string category, out OrderStats? stats);
}
