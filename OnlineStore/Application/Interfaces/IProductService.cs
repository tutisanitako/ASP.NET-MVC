using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto dto);
        Task<ProductDto?> UpdateProductAsync(int id, CreateProductDto dto);
        Task<bool> DeleteProductAsync(int id);
    }
}