using Microsoft.EntityFrameworkCore;

public class StocksDbContext : DbContext
{
    public StocksDbContext(DbContextOptions<StocksDbContext> options) : base(options)
    {
    }

    public DbSet<Exchange> StockDataExchanges { get; set; }
    public DbSet<Fund> StockDataFunds { get; set; }
}
