using CreditCard_Backend_API.Models.Domain;

namespace CreditCard_Backend_API.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllProductsAsync();
        Task<Products> GetProductByIdAsync(int id);
        Task AddProductAsync(Products product);
        Task UpdateProductAsync(Products product);
        Task DeleteProductAsync(int id);
    }
}
