using System.ComponentModel.DataAnnotations;


using LubricantesAyrthonAPI.Controllers;
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Controllers
{

    public class CustomerControllerTests
    {
        // TC_002 - Rechazar registro de cliente con datos obligatorios faltantes

        [Fact]
        public async Task RegisterCustomer_MissingRequiredData_ReturnsBadRequest()
        {
            // Arrange
            var serviceMock = new Mock<ICustomerService>();

            var controller = new CustomerController(serviceMock.Object);

            // Simular un DTO
            var customerDto = new CustomerCreateDto
            {
                Ci = "3040011",
                Name = ""
            };

            // Simular la validacion del modelo
            var validationContext = new ValidationContext(customerDto); // Aqui va el objeto a validar
            var validationResults = new List<ValidationResult>(); // Aqui se guardan los resultados de la validacion
            Validator.TryValidateObject(customerDto, validationContext, validationResults, true); // El true indica que se validen todas las propiedades

            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    // Agregar el error de validacion al ModelState del controlador
                    controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage!);
                }
            }

            // Act
            var result = await controller.Create(customerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            // Verificar que el servicio no fue llamado
            serviceMock.Verify(service => service.CreateAsync(It.IsAny<CustomerCreateDto>()), Times.Never);
        }
    }
}