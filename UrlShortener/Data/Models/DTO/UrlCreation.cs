using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Data.Models.DTO
{
    public class UrlCreation
    {
        [Required]
        [Url]
        public string Url { get; set; }

        public string CategoryName { get; set; }

    }
}
