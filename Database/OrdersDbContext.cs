using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class OrdersDbContext: DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        OnCommonModelCreating(builder);
    }

    protected void OnCommonModelCreating(ModelBuilder modelBuilder)
    {
        var orders = modelBuilder.Entity<OrderEntity>().ToTable("orders");
    }
    
    public IQueryable<OrderEntity?> Orders => Set<OrderEntity>();
}