using StackBuildApi.Core.DTO;

namespace StackBuilApi.Core.Interface.iservices
{
    public interface IOrderService
    {
        Task<(bool Success, string? Error, OrderResponseDto? Order)> PlaceOrderAsync(PlaceOrderDto dto, string? customerId = null);
    }
}
