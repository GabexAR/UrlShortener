using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Data.Models.DTO
{
    public class UrlDto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        public string LargeUrl { get; set; }

        public string ShortUrl { get; set; }

        public int CategoryId { get; set; }

    }
}
