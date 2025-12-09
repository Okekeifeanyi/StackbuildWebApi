using StackBuildApi.Core.DTO;

namespace StackBuilApi.Core.Interface.iservices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto?> UpdateAsync(UpdateProductDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
