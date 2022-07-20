namespace OrderCachedStatsSample;

public interface IDataStore
{
    IEnumerable<Order> GetOrders();
}
