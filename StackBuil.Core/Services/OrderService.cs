using Microsoft.EntityFrameworkCore;
using StackBuilApi.Core.Interface.iservices;
using StackBuildApi.Core.DTO;
using StackBuildApi.Core.Interface.irepositories;
using StackBuildApi.Model.Entities;

namespace StackBuildApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private const int MaxRetries = 3;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<(bool Success, string? Error, OrderResponseDto? Order)> PlaceOrderAsync(PlaceOrderDto dto, string? customerId = null)
        {
            if (dto.Items == null || !dto.Items.Any())
                return (false, "Order contains no items.", null);

            // normalize quantities (group by product)
            var requested = dto.Items
                .GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .ToList();

            int attempt = 0;
            while (true)
            {
                attempt++;
                using var tx = await _uow.BeginTransactionAsync();
                try
                {
                    // fetch products needed
                    var productIds = requested.Select(r => r.ProductId).ToList();
                    var products = await _uow.ProductRepository
                        .Where(p => productIds.Contains(p.Id))
                        .ToListAsync();

                    // ensure all products present
                    var missing = productIds.Except(products.Select(p => p.Id)).ToList();
                    if (missing.Any())
                    {
                        await tx.RollbackAsync();
                        return (false, $"Products not found: {string.Join(", ", missing)}", null);
                    }

                    // check stock
                    var insufficient = new List<string>();
                    foreach (var req in requested)
                    {
                        var product = products.Single(p => p.Id == req.ProductId);
                        if (product.StockQuantity < req.Quantity)
                            insufficient.Add($"{product.Name} (requested {req.Quantity}, available {product.StockQuantity})");
                    }

                    if (insufficient.Any())
                    {
                        await tx.RollbackAsync();
                        return (false, $"Insufficient stock for: {string.Join("; ", insufficient)}", null);
                    }

                    // reduce stock
                    foreach (var req in requested)
                    {
                        var product = products.Single(p => p.Id == req.ProductId);
                        product.StockQuantity -= req.Quantity;
                        _uow.ProductRepository.Update(product);
                    }

                    // create order
                    var order = new Order
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        CustomerId = customerId
                    };

                    decimal total = 0m;
                    foreach (var req in requested)
                    {
                        var prod = products.Single(p => p.Id == req.ProductId);
                        var item = new OrderItem
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order.Id,
                            ProductId = prod.Id,
                            Quantity = req.Quantity,
                            UnitPrice = prod.Price
                        };
                        order.Items.Add(item);
                        total += item.LineTotal;
                    }

                    order.Total = total;

                    await _uow.OrderRepository.AddAsync(order);

                    await _uow.SaveChangesAsync();
                    await tx.CommitAsync();

                    var response = new OrderResponseDto(order.Id, order.CreatedAt, order.Total,
                        order.Items.Select(i => new OrderItemDto(i.ProductId, products.Single(p => p.Id == i.ProductId).Name, i.Quantity, i.UnitPrice, i.LineTotal)));

                    return (true, null, response);
                }
                catch (DbUpdateConcurrencyException)
                {
                    // concurrency conflict when saving (RowVersion changed)
                    await tx.RollbackAsync();

                    if (attempt >= MaxRetries)
                        return (false, "Could not place order due to concurrent updates. Please try again.", null);

                    // else retry
                    await Task.Delay(50 * attempt); // small jitter
                    continue;
                }
                catch (Exception ex)
                {
                    // unexpected
                    await tx.RollbackAsync();
                    return (false, $"An error occurred: {ex.Message}", null);
                }
            }
        }
    }
}
