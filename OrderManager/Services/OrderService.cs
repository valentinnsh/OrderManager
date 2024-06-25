using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using OrderManager.Models;

namespace OrderManager.Services;

public interface IOrderService
{
    Task<OrderEntity> CreateOrderAsync(CreateOrderModel model, CancellationToken token = default);
    Task<List<OrderEntity?>> GetOrdersAsync(int pageNumber = 1, int pageSize = Int32.MaxValue);

    Task<OrderEntity?> GetOrderByIdAsync(Guid id);
}

public class OrderService : IOrderService
{
    private readonly OrdersDbContext _db;

    public OrderService(OrdersDbContext ctx)
    {
        _db = ctx;
    }
    
    public async Task<OrderEntity> CreateOrderAsync(CreateOrderModel model, CancellationToken token = default)
    {
        var newOrder = new OrderEntity
        {
            Weight = model.Weight,
            CollectionDate = model.CollectionDate.ToUniversalTime(),
            ConsigneeTown = model.ConsigneeTown,
            ConsigneeAddress = model.ConsigneeAddress,
            ShipperTown = model.ShipperTown,
            ShipperAddress = model.ShipperAddress,
            ExternalId = Guid.NewGuid()
        };
        
        var entry = _db.Add(newOrder);
        await _db.SaveChangesAsync(token);
        return entry.Entity;
    }

    public async Task<List<OrderEntity?>> GetOrdersAsync(int pageNumber = 1, int pageSize = Int32.MaxValue)
    {
        // TODO: Proper pagination
        return await _db.Orders
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<OrderEntity?> GetOrderByIdAsync(Guid id)
    {
        return await _db.Orders.Where(o => o.ExternalId == id).FirstOrDefaultAsync();
    }
}