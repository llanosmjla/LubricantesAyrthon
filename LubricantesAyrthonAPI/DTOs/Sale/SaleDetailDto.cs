
using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Dtos
{
    public class SaleDetailCreateDto
    {
        
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required int IdProduct { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1.00, double.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public decimal UnitPrice { get; set; }
    }

    public class SaleDetailUpdateDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required int IdProduct { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1.00, double.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public decimal UnitPrice { get; set; }
    }

    public class SaleDetailReadDto
    {
        public int Id { get; set; }
        public required int IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}