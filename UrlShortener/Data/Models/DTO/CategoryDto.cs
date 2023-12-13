using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Data.Models.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string NameCategory { get; set; }
    }
}
