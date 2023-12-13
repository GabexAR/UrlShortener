using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Data.Models.DTO
{
    public class UserCreationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public int CantConversiones { get; set; } = 10;
    }
}
