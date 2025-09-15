using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required int IdCustomer { get; set; }
        public Customer Customer { get; set; } = null!;

        [Required]
        public required int IdSeller { get; set; }
        public Seller Seller { get; set; } = null!;

        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; }
        public List<SaleDetail>? SaleDetails { get; set; } = new();
    }
    
}