using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(1.00, double.MaxValue)]
        public required decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public required int Stock { get; set; }

        public List<SaleDetail>? SaleDetails { get; set; } = new();
    }
    
}