using System.ComponentModel.DataAnnotations;

namespace CrudBack.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(300)]
        public string Password { get; set; }
    }
}
