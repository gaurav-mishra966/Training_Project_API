namespace CreditCard_Backend_API.Models.DTO
{
    public class UserListResponseDTO
    {
        public int TotalCount { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
