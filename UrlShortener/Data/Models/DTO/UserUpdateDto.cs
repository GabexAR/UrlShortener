using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Data.Models.DTO
{
    public class UserUpdateDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
