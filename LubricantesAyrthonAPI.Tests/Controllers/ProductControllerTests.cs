


using LubricantesAyrthonAPI.Controllers;
using LubricantesAyrthonAPI.Services.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Controllers
{
    public class ProductControllerTests
    {

        // TC_002
        [Fact]
        public async Task CreateAsync_ShouldThrowArgumentException_WhenNameIsEmpty()
        {
            // Arrange
            var productDto = new ProductCreateDto
            {
                Name = "",  // inválido
                Price = 30.0m,
                Stock = 100
            };

            var productServiceMock = new Mock<IProductService>();
            var controller = new ProductController(productServiceMock.Object);

            // Forzar validación manual de atributos
            controller.ModelState.AddModelError("Name", "El campo Name es obligatorio.");

            // Act
            var result = await controller.Create(productDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = badRequestResult.Value as SerializableError;

            Assert.NotNull(errors);
            Assert.True(errors.ContainsKey("Name"));
            // Verificar que el servicio no fue llamado
            productServiceMock.Verify(service => service.CreateAsync(It.IsAny<ProductCreateDto>()), Times.Never);
        }

    }
}