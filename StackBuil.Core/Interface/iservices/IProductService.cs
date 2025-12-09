using StackBuildApi.Core.DTO;
using StackBuildApi.Model.Entities;

namespace StackBuilApi.Core.Interface.iservices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<ProductDto?> UpdateAsync(UpdateProductDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<ProductDto?> GetByIdAsync(Guid id);



    }
}
