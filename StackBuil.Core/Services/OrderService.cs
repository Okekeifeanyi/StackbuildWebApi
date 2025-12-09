using StackBuilApi.Core.Interface.iservices;
using StackBuildApi.Core.DTO;
using StackBuildApi.Core.Interface.irepositories;
using StackBuildApi.Domain.Entities;
using StackBuildApi.Model.Entities;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _uow;

    public OrderService(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<(bool Success, string? Error, OrderResponseDto? Order)>
        PlaceOrderAsync(PlaceOrderDto dto)
    {
        await using var transaction = await _uow.BeginTransactionAsync();
        try
        {
            var productIds = dto.Items.Select(i => i.ProductId).ToList();
            var products = await _uow.ProductRepository.GetByIdsAsync(productIds);

            if (products.Count != productIds.Count)
                return (false, "One or more products not found.", null);
            
            // Validate stock
            foreach (var item in dto.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);
                if (product.StockQuantity < item.Quantity)
                    return (false, $"Insufficient stock for {product.Name}.", null);
            }

            // Create order
            var order = new Order();
            foreach (var item in dto.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                    // SubTotal calculated by property
                });

                product.StockQuantity -= item.Quantity;
                _uow.ProductRepository.Update(product);
            }

            order.Total = order.Items.Sum(i => i.SubTotal);

            await _uow.OrderRepository.AddAsync(order);
            await _uow.SaveChangesAsync();
            await transaction.CommitAsync();

            var response = new OrderResponseDto(
                order.Id,
                order.CreatedAt,
                order.Total,
                order.Items.Select(i =>
                {
                    var prod = products.First(p => p.Id == i.ProductId);
                    return new OrderItemDto(prod.Id, prod.Name, i.Quantity, i.UnitPrice, i.SubTotal);
                }).ToList()
            );

            return (true, null, response);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw; // or return (false, ex.Message, null);
        }
    }
}

