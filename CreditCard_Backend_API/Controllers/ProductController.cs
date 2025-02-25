using CreditCard_Backend_API.Models.Domain;
using CreditCard_Backend_API.Models.DTO;
using CreditCard_Backend_API.Repositories.Implementation;
using CreditCard_Backend_API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCard_Backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        //Create
        [HttpPost]
        [Route("/Product/addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductsRequestDto productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Invalid data.");
            }
            var product = new Products
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                //Image = productDTO.Image,
                ImageUrl=productDTO.ImageUrl
            };

            await _productRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            
        }

        //this is referenced in post method once the addition is done to return response
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Products product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product Id Mismatched");
            }

            var updateSuccess = await _productRepository.UpdateProductAsync(product);

            if (!updateSuccess)
            {
                return NotFound("Product not found");
            }
            //await _productRepository.UpdateProductAsync(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteProductAsync(id);            
            return Ok(new { deletedProduct = new[] { product } });
        }
    }
}
