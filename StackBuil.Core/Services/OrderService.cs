using Microsoft.EntityFrameworkCore;
using StackBuildApi.Core.DTO;
using StackBuildApi.Core.Interface.irepositories;
using StackBuildApi.Domain.Entities;
using StackBuildApi.Model;
using StackBuildApi.Model.Entities;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _uow;

    public OrderService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<OrderResponseDto>> PlaceOrderAsync(PlaceOrderDto dto)
    {
        await using var transaction = await _uow.BeginTransactionAsync();
        try
        {
            var productIds = dto.Items.Select(i => i.ProductId).ToList();

            var products = await _uow.ProductRepository.GetByIdsForUpdateAsync(productIds);

            if (products.Count != productIds.Count)
            {
                await transaction.RollbackAsync();
                return ApiResponse<OrderResponseDto>.Failed("One or more products not found.", 400);
            }

            foreach (var item in dto.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);
                if (product.StockQuantity < item.Quantity)
                {
                    await transaction.RollbackAsync();
                    return ApiResponse<OrderResponseDto>.Failed($"Insufficient stock for {product.Name}.", 400);
                }
            }

            var order = new Order();
            foreach (var item in dto.Items)
            {
                var product = products.First(p => p.Id == item.ProductId);

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                product.StockQuantity -= item.Quantity; 
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

            return ApiResponse<OrderResponseDto>.Success(response, "Order placed successfully.", 201);
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaction.RollbackAsync();
            return ApiResponse<OrderResponseDto>.Failed("Stock changed. Please try again.", 409);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return ApiResponse<OrderResponseDto>.Failed(ex.Message, 500);
        }
    }
}
