namespace OrderCachedStatsSample;

public class DataStore : IDataStore
{
    public IEnumerable<Order> GetOrders()
    {
        yield return new Order
        {
            Id = 1,
            Price = 100m,
            Category = "Clothing",
            Product = "T-Shirt",
            Region = "Nairobi",
        };

        yield return new Order
        {
            Id = 2,
            Price = 50m,
            Category = "Clothing",
            Product = "T-Shirt",
            Region = "Nairobi",
        };
    }
}
