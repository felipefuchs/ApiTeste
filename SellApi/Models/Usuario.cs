using System.ComponentModel.DataAnnotations;

namespace SellApi.Models
{
    public class Usuario
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        [StringLength(200, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Required]
        public bool Active { get; set; }
    }
}
