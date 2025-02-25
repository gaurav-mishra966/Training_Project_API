using CreditCard_Backend_API.Models.Domain;
using CreditCard_Backend_API.Repositories.Interface;

namespace CreditCard_Backend_API.Repositories.Implementation
{
    public class ProductRepository:IProductRepository
    {
        private  List<Products> _products;

        public ProductRepository()
        {
            _products = new List<Products>
            {
                new Products { Id = 1, Name = "Visa Platinum", Description = "Enjoy exclusive benefits and rewards.", Price = 0, ImageUrl = "https://via.placeholder.com/300x180/4A90E2/FFFFFF?text=Visa",Image="Visa" },
                new Products { Id = 2, Name = "MasterCard Gold", Description = "Get the best cash back rewards.", Price = 95, ImageUrl = "https://via.placeholder.com/300x180/FFB848/FFFFFF?text=MasterCard",Image="MasterCard" },
                new Products { Id = 3, Name = "American Express Green", Description = "Earn points on every purchase.", Price = 150, ImageUrl = "https://via.placeholder.com/300x180/00A9E0/FFFFFF?text=American+Express",Image="American Express" }
            };
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            return await Task.FromResult(_products);
        }

        public async Task<Products> GetProductByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(product);
        }

        public async Task AddProductAsync(Products product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task<bool> UpdateProductAsync(Products product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return false;
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Image = product.Image;
            await Task.CompletedTask;
            return true;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            await Task.CompletedTask;
        }

    }
}
