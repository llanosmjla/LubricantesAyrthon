
using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Dtos
{
    public class SellerCreateDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(7, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Ci { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(2, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(18, 65, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public required int Age { get; set; }

        [EmailAddress(ErrorMessage = "El campo {0} debe ser una dirección de correo electrónico válida.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "El campo {0} debe ser un número de teléfono válido.")]
        public string? Phone { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1.0, double.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public decimal Salary { get; set; }

    }

    public class SellerUpdateDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(2, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(18, 65, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        public required int Age { get; set; }

        [EmailAddress(ErrorMessage = "El campo {0} debe ser una dirección de correo electrónico válida.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "El campo {0} debe ser un número de teléfono válido.")]
        public string? Phone { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1.0, double.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public decimal Salary { get; set; }
    }

    public class SellerReadDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Age { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
    }
}