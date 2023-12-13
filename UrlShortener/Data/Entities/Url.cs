using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Data.Entities
{
    public class Url
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string LargeUrl { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortUrl { get; set; }

        public int Count { get; set; } = 0;

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }



    }
}
