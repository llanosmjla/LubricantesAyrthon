using System.ComponentModel.DataAnnotations;

namespace LubricantesAyrthonAPI.Services.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(2, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Name { get; set; }

        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Description { get; set; }

        [Range(1.00, double.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public required decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El campo {0} no puede ser negativo.")]
        public int Stock { get; set; }
    }

    public class ProductUpdateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [MinLength(2, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        public required string Name { get; set; }

        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Description { get; set; }

        [Required]
        [Range(1.00, double.MaxValue, ErrorMessage = "El campo {0} debe ser al menos {1}.")]
        public required decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El campo {0} no puede ser negativo.")]
        public required int Stock { get; set; }
    }

    public class ProductReadDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
}
}