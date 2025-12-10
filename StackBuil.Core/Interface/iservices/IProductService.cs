using StackBuildApi.Core.DTO;
using StackBuildApi.Model;
using StackBuildApi.Model.Entities;

namespace StackBuilApi.Core.Interface.iservices
{
    public interface IProductService
    {
        Task<ApiResponse<IEnumerable<ProductDto>>> GetAllAsync();
        Task<ApiResponse<ProductDto>> CreateAsync(CreateProductDto dto);
        Task<ApiResponse<ProductDto?>> UpdateAsync(UpdateProductDto dto);
        Task<ApiResponse<string>> DeleteAsync(Guid id);
        Task<ApiResponse<ProductDto?>> GetByIdAsync(Guid id);



    }
}
