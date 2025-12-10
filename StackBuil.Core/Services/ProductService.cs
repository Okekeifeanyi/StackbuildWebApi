using Microsoft.EntityFrameworkCore;
using StackBuilApi.Core.Interface.iservices;
using StackBuildApi.Core.DTO;
using StackBuildApi.Core.Interface.irepositories;
using StackBuildApi.Model.Entities;
using StackBuildApi.Model;

namespace StackBuildApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ApiResponse<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var products = await _uow.ProductRepository.GetAllAsync();
            var data = products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity));
            return ApiResponse<IEnumerable<ProductDto>>.Success(data, "Products retrieved", 200);
        }

        public async Task<ApiResponse<ProductDto?>> GetByIdAsync(Guid id)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(id);
            if (p == null)
                return ApiResponse<ProductDto?>.Failed("Product not found", 404);

            var data = new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
            return ApiResponse<ProductDto?>.Success(data, "Product retrieved", 200);
        }

        public async Task<ApiResponse<ProductDto>> CreateAsync(CreateProductDto dto)
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

            var data = new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
            return ApiResponse<ProductDto>.Success(data, "Product created", 201);
        }

        public async Task<ApiResponse<ProductDto?>> UpdateAsync(UpdateProductDto dto)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(dto.Id);
            if (p == null)
                return ApiResponse<ProductDto?>.Failed("Product not found", 404);

            p.Name = dto.Name;
            p.Description = dto.Description;
            p.Price = dto.Price;
            p.StockQuantity = dto.StockQuantity;

            _uow.ProductRepository.Update(p);
            await _uow.SaveChangesAsync();

            var data = new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
            return ApiResponse<ProductDto?>.Success(data, "Product updated", 200);
        }

        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            var p = await _uow.ProductRepository.GetByIdAsync(id);
            if (p == null)
                return ApiResponse<string>.Failed("Product not found", 404);

            _uow.ProductRepository.Delete(p);
            await _uow.SaveChangesAsync();

            return ApiResponse<string>.Success("Product deleted", "Deleted", 200);
        }
    }
}
