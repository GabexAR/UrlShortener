using UrlShortener.Data.Models.Enum;

namespace UrlShortener.Data.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public RoleEnum Role { get; set; } = RoleEnum.User;

        public int CantConversiones { get; set; }
    }
}
