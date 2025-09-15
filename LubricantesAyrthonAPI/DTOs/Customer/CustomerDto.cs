using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Dtos
{
    public class CustomerCreateDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(7, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Ci { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(2, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Name { get; set; }

        [EmailAddress(ErrorMessage = "El campo {0} debe ser una dirección de correo electrónico válida.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "El campo {0} debe ser un número de teléfono válido.")]
        public string? Phone { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Address { get; set; }
    }

    public class CustomerUpdateDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(7, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Ci { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(2, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Name { get; set; }

        [EmailAddress(ErrorMessage = "El campo {0} debe ser una dirección de correo electrónico válida.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "El campo {0} debe ser un número de teléfono válido.")]
        public string? Phone { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Address { get; set; }
    }

    public class CustomerReadDto
    {
        public int Id { get; set; }
        public required string Ci { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}