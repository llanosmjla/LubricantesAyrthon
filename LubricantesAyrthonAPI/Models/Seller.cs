
using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(20)]
        public required string Ci { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [Range(18, 65)]
        public required int Age { get; set; }

        [EmailAddress()]
        public string? Email { get; set; }

        [Required]
        [Phone()]
        public required string Phone { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Address { get; set; }

        [Required]
        [Range(1.0, double.MaxValue)]
        public decimal Salary { get; set; }

        public List<Sale>? Sales { get; set; } = new();
    }
}