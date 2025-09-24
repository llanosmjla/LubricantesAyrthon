using System.ComponentModel.DataAnnotations;

using LubricantesAyrthonAPI.Dtos;

namespace LubricantesAyrthonAPI.Tests.Dtos
{
    public class CustomerDtoTests
    {
        // TC_006: Validar que el DTO de creación de cliente requiere el campo "Name"
        [Fact]
        public void CustomerCreateDto_NameIsRequired_ValidationFails()
        {
            // Arrange
            var dto = new CustomerCreateDto
            {
                Ci = "22233345",
                Name = "" // vacío
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto);

            // Act
            var isValid = Validator.TryValidateObject(
                dto,
                validationContext,
                validationResults,
                validateAllProperties: true
            );

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage?.Contains("required") == true);
        }
    }

}

