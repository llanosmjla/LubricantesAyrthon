using System.ComponentModel.DataAnnotations;
using LubricantesAyrthonAPI.Controllers;
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Controllers
{
    public class SellerControllerTests
    {
        // TC_VENDEDORES_003 – Validar que GetAll del VendedorController retorne Ok con lista de vendedores existentes
        [Fact]
        public async Task GetAll_ReturnsOkWithListOfSellers()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();

            var sellersMock = new List<SellerReadDto>
            {
                new SellerReadDto { Id = 1, Ci = "3040011", Name = "Juan Pérez", Age = 30, Salary = 1500.00m },
                new SellerReadDto { Id = 2, Ci = "3040012", Name = "Ana Torres", Age = 28, Salary = 1700.00m }
            };

            sellerServiceMock.Setup(s => s.GetAllAsync())
                             .ReturnsAsync(sellersMock);

            var controller = new SellerController(sellerServiceMock.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedSellers = Assert.IsAssignableFrom<IEnumerable<SellerReadDto>>(okResult.Value);
            Assert.Equal(2, ((List<SellerReadDto>)returnedSellers).Count);
            Assert.Contains(returnedSellers, s => s.Id == 1 && s.Name == "Juan Pérez" && s.Salary == 1500.00m);
            Assert.Contains(returnedSellers, s => s.Id == 2 && s.Name == "Ana Torres" && s.Salary == 1700.00m);
        }

        // TC_VENDEDORES_005 – Validar que GetAll del VendedorController retorne Ok con lista vacía si no hay vendedores
        [Fact]
        public async Task GetAll_ReturnsOkWithEmptyList_WhenNoSellersExist()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();
            sellerServiceMock.Setup(s => s.GetAllAsync())
                             .ReturnsAsync(new List<SellerReadDto>());

            var controller = new SellerController(sellerServiceMock.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedSellers = Assert.IsAssignableFrom<IEnumerable<SellerReadDto>>(okResult.Value);
            Assert.Empty(returnedSellers);
        }

        // TC_VENDEDORES_007 – Validar que GetById del VendedorController retorne Ok con vendedor existente
        [Fact]
        public async Task GetById_ReturnsOk_WhenSellerExists()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();

            var sellerMock = new SellerReadDto
            {
                Id = 5,
                Ci = "1234567",
                Name = "Carlos López",
                Age = 30,
                Salary = 2000.00m
            };

            sellerServiceMock.Setup(s => s.GetByIdAsync(5))
                             .ReturnsAsync(sellerMock);

            var controller = new SellerController(sellerServiceMock.Object);

            // Act
            var result = await controller.GetById(5);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedSeller = Assert.IsType<SellerReadDto>(okResult.Value);

            Assert.Equal(5, returnedSeller.Id);
            Assert.Equal("1234567", returnedSeller.Ci);
            Assert.Equal("Carlos López", returnedSeller.Name);
            Assert.Equal(30, returnedSeller.Age);
            Assert.Equal(2000.00m, returnedSeller.Salary);
        }

        // TC_VENDEDORES_008 – Validar que GetById del VendedorController retorne BadRequest si el ID es inválido
        [Fact]
        public async Task GetById_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();
            var controller = new SellerController(sellerServiceMock.Object);

            // Act
            var result = await controller.GetById(-1);
            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);

            // Assert
            Assert.Equal("El ID del vendedor debe ser un número positivo.", notFoundResult.Value);
        }

        // TC_VENDEDORES_011 – Validar que Create del VendedorController retorne Created con vendedor válido
        [Fact]
        public async Task Create_ReturnsCreated_WhenSellerIsValid()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();
            var controller = new SellerController(sellerServiceMock.Object);
            var newSellerDto = new SellerCreateDto
            {
                Ci = "7654321",
                Name = "María Gómez",
                Age = 29,
                Salary = 1800.00m
            };

            // Act
            var result = await controller.Create(newSellerDto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var returnedSeller = Assert.IsType<SellerReadDto>(createdResult.Value);

            Assert.Equal("7654321", returnedSeller.Ci);
            Assert.Equal("María Gómez", returnedSeller.Name);
            Assert.Equal(29, returnedSeller.Age);
            Assert.Equal(1800.00m, returnedSeller.Salary);
        }

        // TC_VENDEDORES_012 – Validar que Create del VendedorController retorne BadRequest si faltan datos obligatorios
        [Fact]
        public async Task Create_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<ISellerService>();
            var controller = new SellerController(mockService.Object);

            var sellerCreateDto = new SellerCreateDto
            {
                Ci = "", // Campo obligatorio faltante
                Name = "", // Campo obligatorio faltante
                Email = "carlos.gomez@email.com",
                Phone = "555-8888",
                Age = 25,
                Salary = 2500
            };

            // Simular la validación del modelo
            var validationContext = new ValidationContext(sellerCreateDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(sellerCreateDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage!);
                }
            }

            // Act
            var result = await controller.Create(sellerCreateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);

            // Verificar que el servicio no fue invocado
            mockService.Verify(s => s.CreateAsync(It.IsAny<SellerCreateDto>()), Times.Never);

            // Opcional: verificar que el mensaje de error contiene los campos obligatorios
            var errorDict = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.True(errorDict.ContainsKey(nameof(SellerCreateDto.Ci)));
            Assert.True(errorDict.ContainsKey(nameof(SellerCreateDto.Name)));
        }

        // TC_VENDEDORES_015 – Validar que Update del VendedorController retorne Ok al modificar vendedor existente
        [Fact]
        public async Task Update_ReturnsOk_WhenSellerIsUpdated()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();
            var controller = new SellerController(sellerServiceMock.Object);
            var updateDto = new SellerUpdateDto
            {
                Ci = "9876543",
                Name = "Luis Martínez",
                Age = 35,
                Salary = 2200.00m
            };

            // Act
            var result = await controller.Update(1, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedSeller = Assert.IsType<SellerReadDto>(okResult.Value);

            Assert.Equal(1, returnedSeller.Id);
            Assert.Equal("9876543", returnedSeller.Ci);
            Assert.Equal("Luis Martínez", returnedSeller.Name);
            Assert.Equal(35, returnedSeller.Age);
            Assert.Equal(2200.00m, returnedSeller.Salary);
        }

        // TC_VENDEDORES_016 – Validar que Update del VendedorController retorne BadRequest si los datos son inválidos
        [Fact]
        public async Task Update_ReturnsBadRequest_WhenDataIsInvalid()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();
            var controller = new SellerController(sellerServiceMock.Object);
            var updateDto = new SellerUpdateDto
            {
                Ci = "", // Ci inválido
                Name = "Luis Martínez",
                Age = 35,
                Salary = 2200.00m
            };

            // Act
            var result = await controller.Update(1, updateDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        // TC_VENDEDORES_019 – Validar que Delete del VendedorController retorne NoContent al eliminar vendedor existente
        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSellerIsDeleted()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();
            sellerServiceMock.Setup(s => s.DeleteAsync(1))
                             .ReturnsAsync(true);

            var controller = new SellerController(sellerServiceMock.Object);

            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        // TC_VENDEDORES_020 – Validar que Delete del VendedorController retorne NotFound si el vendedor no existe
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenSellerDoesNotExist()
        {
            // Arrange
            var sellerServiceMock = new Mock<ISellerService>();
            sellerServiceMock.Setup(s => s.DeleteAsync(1))
                             .ReturnsAsync(false);

            var controller = new SellerController(sellerServiceMock.Object);

            // Act
            var result = await controller.Delete(1);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

            // Assert
            Assert.Equal("Vendedor con ID 1 no encontrado.", notFoundResult.Value);

        }
    }
}