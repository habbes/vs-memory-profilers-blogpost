using OrderCachedStatsSample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IDataStore, DataStore>();
builder.Services.AddSingleton<IOrderStatsCache, OrderStatsCache>();
builder.Services.AddSingleton<IStatsService, StatsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/orders", (IDataStore store) => store.GetOrders());

app.MapGet("/stats/{region}/{category}",
    (IStatsService statsService, string region, string category) =>
        statsService.GetOrderStatsByRegionAndCategory(region, category));

app.Run();
