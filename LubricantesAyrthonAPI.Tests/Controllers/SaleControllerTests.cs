using LubricantesAyrthonAPI.Controllers;
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Controllers
{
    public class SaleControllerTests
    {
        // TC_VENTAS_003 – Validar GetAll del SaleController retorna lista de ventas
        [Fact]
        public async Task GetAll_ReturnsOk_WithSalesList()
        {
            // Arrange
            var mockService = new Mock<ISaleService>();
            var sales = new List<SaleReadDto>
            {
                new SaleReadDto
                {
                    Id = 1,
                    IdCustomer = 10,
                    IdSeller = 20,
                    TotalPrice = 100.0m,
                    SaleDate = new DateTime(2025, 9, 1),
                    SaleDetails = new List<SaleDetailReadDto>
                    {
                        new SaleDetailReadDto { Id = 1, IdProduct = 100, Quantity = 2, UnitPrice = 50.0m }
                    }
                },
                new SaleReadDto
                {
                    Id = 2,
                    IdCustomer = 11,
                    IdSeller = 21,
                    TotalPrice = 250.5m,
                    SaleDate = new DateTime(2025, 9, 2),
                    SaleDetails = new List<SaleDetailReadDto>
                    {
                        new SaleDetailReadDto { Id = 2, IdProduct = 200, Quantity = 5, UnitPrice = 50.1m }
                    }
                }
            };

            mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(sales);

            var controller = new SaleController(mockService.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnSales = Assert.IsAssignableFrom<IEnumerable<SaleReadDto>>(okResult.Value);

            Assert.Equal(2, returnSales.Count());
            Assert.Equal(sales[0].Id, returnSales.First().Id);
            Assert.Equal(sales[^1].TotalPrice, returnSales.Last().TotalPrice);
        }

        //TC_VENTAS_004 – Validar GetAll del SaleController retorna lista vacía cuando no hay ventas
        [Fact]
        public async Task GetAll_NoSales_ReturnsOkWithEmptyList()
        {
            // Arrange
            var mockService = new Mock<ISaleService>();
            mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<SaleReadDto>());

            var controller = new SaleController(mockService.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnSales = Assert.IsAssignableFrom<IEnumerable<SaleReadDto>>(okResult.Value);

            Assert.Empty(returnSales);
        }

        // TC_VENTAS_007 - Validar GetById del SaleController retorna la venta con sus detalles cuando existe
        [Fact]
        public async Task GetById_ReturnsOk_WithSaleAndDetails_WhenSaleExists()
        {
            // Arrange
            var saleServiceMock = new Mock<ISaleService>();
            int saleId = 5;

            var saleDto = new SaleReadDto
            {
                Id = saleId,
                IdCustomer = 10,
                IdSeller = 3,
                TotalPrice = 100.0m,
                SaleDate = new DateTime(2025, 9, 28),
                SaleDetails = new List<SaleDetailReadDto>
            {
                new SaleDetailReadDto
                {
                    IdProduct = 1,
                    Quantity = 2,
                    UnitPrice = 50.0m
                }
            }
            };

            saleServiceMock
                .Setup(s => s.GetByIdAsync(saleId))
                .ReturnsAsync(saleDto);

            var controller = new SaleController(saleServiceMock.Object);

            // Act
            var result = await controller.GetById(saleId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedSale = Assert.IsType<SaleReadDto>(okResult.Value);

            Assert.Equal(saleId, returnedSale.Id);
            Assert.Equal(10, returnedSale.IdCustomer);
            Assert.Equal(3, returnedSale.IdSeller);
            Assert.Equal(100.0m, returnedSale.TotalPrice);
            Assert.Equal(new DateTime(2025, 9, 28), returnedSale.SaleDate);

            // Verificar detalles de venta
            Assert.Single(returnedSale.SaleDetails);
            var detail = returnedSale.SaleDetails[0];
            Assert.Equal(1, detail.IdProduct);
            Assert.Equal(2, detail.Quantity);
            Assert.Equal(50.0m, detail.UnitPrice);

            // Verificar que se llamó exactamente una vez al servicio
            saleServiceMock.Verify(s => s.GetByIdAsync(saleId), Times.Once);
        }

        //TC_VENTAS_008 - Validar GetById del SaleController retorna NotFound cuando la venta no existe
        [Fact]
        public async Task GetById_VentaNoExiste_RetornaNotFound()
        {
            // Arrange
            var mockService = new Mock<ISaleService>();
            mockService.Setup(s => s.GetByIdAsync(9999))
                       .ReturnsAsync((SaleReadDto?)null); // simula que no se encuentra la venta

            var controller = new SaleController(mockService.Object);

            // Act
            var result = await controller.GetById(9999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        // TC_VENTAS_011 – Validar Create del SaleController crea una venta correctamente
        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenSaleIsValid()
        {
            // Arrange
            var saleServiceMock = new Mock<ISaleService>();

            var saleCreateDto = new SaleCreateDto
            {
                IdCustomer = 1,
                IdSeller = 1,
                TotalPrice = 100m,
                SaleDate = DateTime.Today,
                SaleDetails = new List<SaleDetailCreateDto>
            {
                new SaleDetailCreateDto { IdProduct = 1, Quantity = 2, UnitPrice = 50m }
            }
            };

            var saleReadDto = new SaleReadDto
            {
                Id = 11,
                IdCustomer = saleCreateDto.IdCustomer,
                IdSeller = saleCreateDto.IdSeller,
                TotalPrice = saleCreateDto.TotalPrice,
                SaleDate = saleCreateDto.SaleDate,
                SaleDetails = new List<SaleDetailReadDto>
            {
                new SaleDetailReadDto { IdProduct = 1, Quantity = 2, UnitPrice = 50m }
            }
            };

            saleServiceMock
                .Setup(s => s.CreateAsync(saleCreateDto))
                .ReturnsAsync(saleReadDto);

            var controller = new SaleController(saleServiceMock.Object);

            // Act
            var result = await controller.Create(saleCreateDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(SaleController.GetById), createdResult.ActionName);

            var returnedSale = Assert.IsType<SaleReadDto>(createdResult.Value);
            Assert.Equal(11, returnedSale.Id);

            saleServiceMock.Verify(s => s.CreateAsync(saleCreateDto), Times.Once);
        }



        // TC_VENTAS_012 – Validar Create del SaleController retorna BadRequest cuando la cantidad es negativa
        [Fact]
        public async Task CreateSale_WithNegativeQuantity_ReturnsBadRequest()
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

        // TC_VENTAS_015 - Validar que Update del SaleController retorne Ok cuando la venta se actualiza correctamente
        [Fact]
        public async Task Update_ReturnsOk_WithUpdatedSale()
        {
            // Arrange
            var mockService = new Mock<ISaleService>();

            var dtoUpdate = new SaleUpdateDto
            {
                IdCustomer = 2,
                IdSeller = 3,
                TotalPrice = 180.0m,
                SaleDate = DateTime.UtcNow,
                SaleDetails = new List<SaleDetailUpdateDto>
                {
                    new SaleDetailUpdateDto { IdProduct = 10, Quantity = 2, UnitPrice = 90.0m }
                }
            };

            var updatedSale = new SaleReadDto
            {
                Id = 5,
                IdCustomer = 2,
                IdSeller = 3,
                TotalPrice = 180.0m,
                SaleDate = DateTime.UtcNow,
                SaleDetails = new List<SaleDetailReadDto>
                {
                    new SaleDetailReadDto { Id = 1, IdProduct = 10, Quantity = 2, UnitPrice = 90.0m }
                }
            };

            mockService
                .Setup(s => s.UpdateAsync(5, dtoUpdate))
                .ReturnsAsync(updatedSale);

            var controller = new SaleController(mockService.Object);

            // Act
            var result = await controller.Update(5, dtoUpdate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<SaleReadDto>(okResult.Value);

            Assert.Equal(5, value.Id);
            Assert.Equal(2, value.IdCustomer);
            Assert.Equal(3, value.IdSeller);
            Assert.Equal(180.0m, value.TotalPrice);

            mockService.Verify(s => s.UpdateAsync(5, dtoUpdate), Times.Once);
        }

        // TC_VENTAS_016 - Validar que Update del VentasController retorne NotFound al actualizar una venta inexistente
        [Fact]
        public async Task Update_ReturnsNotFound_WhenSaleDoesNotExist()
        {
            // Arrange
            var saleId = 9999;

            var mockService = new Mock<ISaleService>();

            var dtoUpdate = new SaleUpdateDto
            {
                IdCustomer = 2,
                IdSeller = 3,
                TotalPrice = 150.0m,
                SaleDate = DateTime.UtcNow,
                SaleDetails = new List<SaleDetailUpdateDto>
                {
                    new SaleDetailUpdateDto { IdProduct = 7, Quantity = 1, UnitPrice = 150.0m }
                }
            };

            mockService
                .Setup(s => s.UpdateAsync(saleId, dtoUpdate))
                .ReturnsAsync((SaleReadDto?)null);

            var controller = new SaleController(mockService.Object);

            // Act
            var result = await controller.Update(saleId, dtoUpdate);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            // Assert
            Assert.Equal($"Venta con ID {saleId} no encontrada.", notFoundResult.Value);
            mockService.Verify(s => s.UpdateAsync(saleId, dtoUpdate), Times.Once);
        }

        //TC_VENTAS_019 - Validar eliminación de venta existente retorna NoContent
        [Fact]
        public async Task Delete_ExistingSale_ReturnsNoContent()
        {
            // Arrange
            int saleId = 1;

            var saleServiceMock = new Mock<ISaleService>();
            saleServiceMock
                .Setup(s => s.DeleteAsync(saleId))
                .ReturnsAsync(true);

            var controller = new SaleController(saleServiceMock.Object);

            // Act
            var result = await controller.Delete(saleId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            saleServiceMock.Verify(s => s.DeleteAsync(saleId), Times.Once);
        }

        // TC_VENTAS_020 - Validar eliminación de venta inexistente retorna NotFound
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenDeleteFails()
        {
            // Arrange
            var mockService = new Mock<ISaleService>();

            mockService
                .Setup(s => s.DeleteAsync(20))
                .ReturnsAsync(false);

            var controller = new SaleController(mockService.Object);

            // Act
            var result = await controller.Delete(20);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            mockService.Verify(s => s.DeleteAsync(20), Times.Once);
        }


    }
}