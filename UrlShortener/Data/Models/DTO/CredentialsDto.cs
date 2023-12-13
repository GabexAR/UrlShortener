using System.ComponentModel.DataAnnotations;
namespace UrlShortener.Data.Models.DTO
{
    public class CredentialsDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
