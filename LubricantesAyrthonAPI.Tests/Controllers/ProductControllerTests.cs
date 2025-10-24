


using LubricantesAyrthonAPI.Controllers;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Services.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Controllers
{
    public class ProductControllerTests
    {

        // TC_PRODUCTOS_003 – Validar que GetAll del ProductController retorne Ok con lista de productos existentes
        [Fact]
        public async Task GetAll_ReturnsOkWithProducts()
        {
            // Arrange
            var mockService = new Mock<IProductService>();

            var productsMock = new List<ProductReadDto>
            {
                new ProductReadDto { Id = 1, Name = "Aceite A" },
                new ProductReadDto { Id = 2, Name = "Filtro B" }
            };

            mockService.Setup(s => s.GetAllAsync())
                       .ReturnsAsync(productsMock);

            var controller = new ProductController(mockService.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<ProductReadDto>>(okResult.Value);

            Assert.Equal(2, returnedProducts.Count());
            Assert.Contains(returnedProducts, p => p.Id == 1 && p.Name == "Aceite A");
            Assert.Contains(returnedProducts, p => p.Id == 2 && p.Name == "Filtro B");

            // Verificar que se llamó una vez al servicio
            mockService.Verify(s => s.GetAllAsync(), Times.Once);
        }

        // TC_PRODUCTOS_004 – Validar que GetAll del ProductController retorne Ok con lista vacía si no hay productos
        [Fact]
        public async Task GetAll_ReturnsOkWithEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            // Configurar el mock para retornar una lista vacía
            mockService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<ProductReadDto>());
            var controller = new ProductController(mockService.Object);
            // Act
            var result = await controller.GetAll();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<ProductReadDto>>(okResult.Value);
            Assert.Empty(returnedProducts);
            // Verificar que se llamó una vez al servicio
            mockService.Verify(s => s.GetAllAsync(), Times.Once);
        }

        // TC_PRODUCTOS_007 – Validar que GetById del ProductController retorne Ok con producto existente
        [Fact]
        public async Task GetById_ReturnsOkWhenProductExists()
        {
            // Arrange
            var mockService = new Mock<IProductService>();

            var productId = 1;
            var productMock = new ProductReadDto
            {
                Id = productId,
                Name = "Aceite Premium",
                Price = 45.0m
            };

            mockService.Setup(s => s.GetByIdAsync(productId))
                       .ReturnsAsync(productMock);

            var controller = new ProductController(mockService.Object);

            // Act
            var result = await controller.GetById(5);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ProductReadDto>(okResult.Value);

            Assert.Equal(productId, returnedProduct.Id);
            Assert.Equal("Aceite Premium", returnedProduct.Name);
            Assert.Equal(45.0m, returnedProduct.Price);

            // Verificar que se llamó exactamente una vez al servicio
            mockService.Verify(s => s.GetByIdAsync(productId), Times.Once);
        }

        // TC_PRODUCTOS_008 – Validar que GetById del ProductController retorne BadRequest si el ID es inválido
        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            int nonExistentId = 9999;

            productServiceMock
                .Setup(s => s.GetByIdAsync(nonExistentId))
                .ReturnsAsync((ProductReadDto?)null);

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.GetById(nonExistentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            productServiceMock.Verify(s => s.GetByIdAsync(nonExistentId), Times.Once);
        }

        // TC_PRODUCTOS_011 – Validar que Create del ProductController retorne Created con producto válido
        [Fact]
        public async Task Create_ReturnsCreatedAtActionForValidDto()
        {
            // Arrange
            var mockService = new Mock<IProductService>();

            var productCreateDto = new ProductCreateDto
            {
                Name = "Aceite X",
                Price = 30.0m
            };

            var createdProduct = new ProductReadDto
            {
                Id = 11,
                Name = "Aceite X",
                Price = 30.0m
            };

            mockService.Setup(s => s.CreateAsync(productCreateDto))
                       .ReturnsAsync(createdProduct);

            var controller = new ProductController(mockService.Object);

            // Act
            var result = await controller.Create(productCreateDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(ProductController.GetById), createdAtActionResult.ActionName);

            var returnedProduct = Assert.IsType<ProductReadDto>(createdAtActionResult.Value);
            Assert.Equal(11, returnedProduct.Id);
            Assert.Equal("Aceite X", returnedProduct.Name);
            Assert.Equal(30.0m, returnedProduct.Price);

            // Verificamos que el servicio fue llamado exactamente una vez
            mockService.Verify(s => s.CreateAsync(productCreateDto), Times.Once);
        }

        // TC_PRODUCTOS_012 – Validar que Create del ProductController retorne BadRequest si faltan datos obligatorios
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

        // TC_PRODUCTOS_015 – Validar que Update del ProductController retorne Ok al modificar producto existente
        [Fact]
        public async Task Update_ReturnsOkWithUpdatedProduct()
        {
            // Arrange
            var mockService = new Mock<IProductService>();

            var productUpdateDto = new ProductUpdateDto
            {
                Name = "Filtro Nuevo",
                Price = 12.5m,
                Stock = 50
            };

            var updatedProduct = new ProductReadDto
            {
                Id = 7,
                Name = "Filtro Nuevo",
                Price = 12.5m
            };

            mockService.Setup(s => s.UpdateAsync(7, productUpdateDto))
                       .ReturnsAsync(updatedProduct);

            var controller = new ProductController(mockService.Object);

            // Act
            var result = await controller.Update(7, productUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ProductReadDto>(okResult.Value);

            Assert.Equal(7, returnedProduct.Id);
            Assert.Equal("Filtro Nuevo", returnedProduct.Name);
            Assert.Equal(12.5m, returnedProduct.Price);

            mockService.Verify(s => s.UpdateAsync(7, productUpdateDto), Times.Once);
        }

        // TC_PRODUCTOS_016 – Validar que Update del ProductController retorne BadRequest si los datos son inválidos
        [Fact]
        public async Task Update_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int invalidId = 99;
            var updateDto = new ProductUpdateDto
            {
                Name = "Producto Falso",
                Price = 50.0m,
                Stock = 10
            };

            var productServiceMock = new Mock<IProductService>();
            productServiceMock
                .Setup(s => s.UpdateAsync(invalidId, updateDto))
                .ReturnsAsync((ProductReadDto?)null); // Simula que no existe

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.Update(invalidId, updateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            productServiceMock.Verify(s => s.UpdateAsync(invalidId, updateDto), Times.Once);
        }

        // TC_PRODUCTOS_019 – Validar que Delete del ProductController retorne NoContent al eliminar producto existente
        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            // Arrange
            var mockService = new Mock<IProductService>();

            mockService.Setup(s => s.DeleteAsync(4))
                       .ReturnsAsync(true);

            var controller = new ProductController(mockService.Object);

            // Act
            var result = await controller.Delete(4);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockService.Verify(s => s.DeleteAsync(4), Times.Once);
        }

        // TC_PRODUCTOS_020 – Validar que Delete del ProductController retorne NotFound si el producto no existe
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IProductService>();

            mockService.Setup(s => s.DeleteAsync(9999))
                       .ReturnsAsync(false);

            var controller = new ProductController(mockService.Object);

            // Act
            var result = await controller.Delete(9999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            mockService.Verify(s => s.DeleteAsync(9999), Times.Once);
        }   
    }
}