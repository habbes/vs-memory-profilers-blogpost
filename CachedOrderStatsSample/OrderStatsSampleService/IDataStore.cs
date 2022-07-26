namespace OrderStatsSampleService;

public interface IDataStore
{
    IEnumerable<Order> GetOrders();
}
