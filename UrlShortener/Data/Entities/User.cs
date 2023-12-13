using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UrlShortener.Data.Models.Enum;

namespace UrlShortener.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public RoleEnum Role { get; set; } = RoleEnum.User;

        public int CantConversiones { get; set; } = 10;

        public ICollection<Url> Urls { get; set; }

    }
}
