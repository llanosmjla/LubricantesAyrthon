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

        [Range(1.00, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public List<SaleDetail>? SaleDetails { get; set; } = new();
    }
    
}