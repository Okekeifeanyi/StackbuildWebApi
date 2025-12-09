using StackBuildApi.Core.DTO;

public interface IOrderService
{
    Task<(bool Success, string? Error, OrderResponseDto? Order)>
        PlaceOrderAsync(PlaceOrderDto dto);
}
