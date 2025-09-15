
using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(7)]
        public required string Ci { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [EmailAddress()]
        public string? Email { get; set; }
        [Phone()]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        public List<Sale>? Sales { get; set; } = new();
    }
    
}