using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Models
{
    public class SaleDetail
    {
        [Key]
        public int Id { get; set; }
        public int IdSale { get; set; }
        public Sale Sale { get; set; } = null!;

        [Required]
        public required int IdProduct { get; set; }
        public Product Product { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
    }
}