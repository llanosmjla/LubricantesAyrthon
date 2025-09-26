using LubricantesAyrthonAPI.Controllers;
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Controllers
{
    public class SaleControllerTests
    {
        // TC_006: Validar creación de venta con cantidad negativa
        [Fact]
        public async Task TC_006_CreateSale_WithNegativeQuantity_ReturnsBadRequest()
        {
            // Arrange
            var saleDto = new SaleCreateDto
            {
                IdCustomer = 1,
                IdSeller = 1,
                SaleDate = DateTime.Now,
                SaleDetails = new List<SaleDetailCreateDto>
                {
                    new SaleDetailCreateDto
                    {
                        IdProduct = 1,
                        Quantity = -2,
                        UnitPrice = 50.00m
                    }
                }
            };

            var saleServiceMock = new Mock<ISaleService>();

            var controller = new SaleController(saleServiceMock.Object);
            controller.ModelState.AddModelError("Quantity", "La cantidad debe ser mayor a 0.");

            // Act
            var result = await controller.Create(saleDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = badRequestResult.Value as SerializableError;

            Assert.NotNull(response);
            Assert.True(response.ContainsKey("Quantity"));
            saleServiceMock.Verify(s => s.CreateAsync(It.IsAny<SaleCreateDto>()), Times.Never);
        }
    }
}