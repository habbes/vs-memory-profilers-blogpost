namespace OrderStatsSampleService;

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
            Price = 10m,
            Category = "Stationery",
            Product = "Pen",
            Region = "Redmond"
        };

        yield return new Order
        {
            Id = 3,
            Price = 50m,
            Category = "Clothing",
            Product = "Shoes",
            Region = "Nairobi",
        };

        yield return new Order
        {
            Id = 4,
            Price = 20m,
            Category = "Cosmetics",
            Product = "Lipstick",
            Region = "Oslo"
        };
    }
}
