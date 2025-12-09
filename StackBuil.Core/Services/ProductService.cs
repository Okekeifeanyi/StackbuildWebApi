using StackBuilApi.Core.Interface.iservices;
using StackBuildApi.Core.DTO;
using StackBuildApi.Core.Interface.irepositories;
using StackBuildApi.Model.Entities;

namespace StackBuildApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _uow.ProductRepository.GetAllAsync();
            return products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity));
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(id);
            return p == null ? null : new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var p = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            await _uow.ProductRepository.AddAsync(p);
            await _uow.SaveChangesAsync();

            return new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
        }

        public async Task<ProductDto?> UpdateAsync(UpdateProductDto dto)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(dto.Id);
            if (p == null) return null;

            p.Name = dto.Name;
            p.Description = dto.Description;
            p.Price = dto.Price;
            p.StockQuantity = dto.StockQuantity;

            _uow.ProductRepository.Update(p);
            await _uow.SaveChangesAsync();

            return new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(id);
            if (p == null) return false;

            _uow.ProductRepository.Delete(p);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
