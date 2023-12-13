using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Data.Models.DTO
{
    public class UserDeletionDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
