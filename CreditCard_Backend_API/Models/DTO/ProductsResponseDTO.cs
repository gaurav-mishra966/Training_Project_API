namespace CreditCard_Backend_API.Models.DTO
{
    public class ProductsResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public object Image { get; internal set; }
    }
}
