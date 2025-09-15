
using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Dtos
{
    public class SaleCreateDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required int IdCustomer { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required int IdSeller { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1.00, double.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date, ErrorMessage = "El campo {0} debe ser una fecha válida.")]
        public DateTime SaleDate { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(1, ErrorMessage = "Debe haber al menos {1} detalle de venta.")]
        public List<SaleDetailCreateDto>? SaleDetails { get; set; }
    }

    public class SaleUpdateDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required int IdCustomer { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required int IdSeller { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1.00, double.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date, ErrorMessage = "El campo {0} debe ser una fecha válida.")] 
        public DateTime SaleDate { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(1, ErrorMessage = "Debe haber al menos {1} detalle de venta.")]
        public List<SaleDetailUpdateDto>? SaleDetails { get; set; }
    }

    public class SaleReadDto
    {
        public int Id { get; set; }
        public required int IdCustomer { get; set; }
        public required int IdSeller { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleDetailReadDto>? SaleDetails { get; set; }
    }
}