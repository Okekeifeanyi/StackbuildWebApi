using StackBuildApi.Core.DTO;
using StackBuildApi.Model;

public interface IOrderService
{
    Task<ApiResponse<OrderResponseDto>> PlaceOrderAsync(PlaceOrderDto dto);
}
