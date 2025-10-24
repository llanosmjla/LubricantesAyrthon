

using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Exceptions;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Implementations;
using LubricantesAyrthonAPI.Services.Interfaces;
using LubricantesAyrthonAPI.Tests.TestDoubles.Stubs;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Services
{
    public class SaleServiceTests
    {
        // TC_VENTAS_001 - Validar que GetAllAsync del SaleService retorne todas las ventas registradas
        [Fact]
        public async Task GetAllAsync_ReturnsListOfSales_WhenSalesExists()
        {
            // Arrange
            var stubRepo = new SaleRepositoryStub();
            var productServiceStub = new ProductServiceStub();

            var saleService = new SaleService(stubRepo, productServiceStub);

            // Act
            var result = (await saleService.GetAllAsync()).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            // Verificar que hay dos productos en la venta 
            Assert.Equal(2, result[0].SaleDetails.Count);
        }

        // TC_VENTAS_002 - Validar que GetAllAsync del SaleService retorne una lista vacía cuando no hay ventas
        [Fact]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoSalesExist()
        {
            // Arrange
            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();
            var productServiceMock = new Mock<IProductService>();

            // Mockeamos que el repositorio devuelve una lista vacía
            saleRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Sale>());

            var saleService = new SaleService(saleRepositoryMock.Object, productServiceMock.Object);

            // Act
            var result = await saleService.GetAllAsync();

            // Assert
            Assert.NotNull(result); // El método no debe retornar null
            Assert.Empty(result);   // Debe retornar lista vacía
            saleRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        // TC_VENTAS_003 - Validar que GetByIdAsync del SaleService retorne la venta correcta cuando existe
        [Fact]
        public async Task GetByIdAsync_ReturnsExistingSale_WhenSaleExists()
        {
            // Arrange
            var stubRepository = new SaleRepositoryStub();
            var stubProductService = new ProductServiceStub();

            var service = new SaleService(stubRepository, stubProductService);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(101, result.IdCustomer);
            Assert.Equal(201, result.IdSeller);
            Assert.Equal(150.50m, result.TotalPrice);
            Assert.Equal(2, result.SaleDetails.Count);
        }

        // TC_VENTAS_004 - Validar que GetByIdAsync del SaleService retorne null cuando no existe
        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenSaleDoesNotExist()
        {
            // Arrange
            var expectedIdSale = 9999;
            var stubRepository = new SaleRepositoryStub();
            var stubProductService = new ProductServiceStub();

            var service = new SaleService(stubRepository, stubProductService);

            // Act
            var result = await service.GetByIdAsync(expectedIdSale);

            // Assert
            Assert.Null(result);
        }

        //TC_VENTAS_009 – Validar que CreateAsync del SaleService agregue una nueva venta correctamente

        [Fact]
        public async Task CreateSale_WithValidData_Success()
        {
            // Arrange
            var saleDto = new SaleCreateDto
            {
                IdCustomer = 101,
                IdSeller = 201,
                SaleDate = DateTime.Now,
                SaleDetails = new List<SaleDetailCreateDto>
                {
                    new SaleDetailCreateDto
                    {
                        IdProduct = 301,
                        Quantity = 2,
                        UnitPrice = 75.25m
                    }
                }
            };

            var expectedTotalPrice = saleDto.SaleDetails.Sum(sd => sd.Quantity * sd.UnitPrice);

            var expectedSale = new Sale
            {
                Id = 1,
                IdCustomer = saleDto.IdCustomer,
                IdSeller = saleDto.IdSeller,
                TotalPrice = expectedTotalPrice,
                SaleDate = saleDto.SaleDate,
                SaleDetails = saleDto.SaleDetails.Select(sd => new SaleDetail
                {
                    IdProduct = sd.IdProduct,
                    Quantity = sd.Quantity,
                    UnitPrice = sd.UnitPrice
                }).ToList()
            };

            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();
            var productServiceMock = new Mock<IProductService>();

            saleRepositoryMock
                        .Setup(r => r.AddAsync(It.IsAny<Sale>()))
                        .ReturnsAsync(expectedSale);

            productServiceMock
                        .Setup(ps => ps.IsStockAvailable(It.IsAny<int>(), It.IsAny<int>()))
                        .ReturnsAsync(true);

            var saleService = new SaleService(saleRepositoryMock.Object, productServiceMock.Object);

            // Act
            var result = await saleService.CreateAsync(saleDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedSale.Id, result.Id);
            Assert.Equal(saleDto.IdCustomer, result.IdCustomer);
            Assert.Equal(saleDto.IdSeller, result.IdSeller);
            Assert.Equal(expectedTotalPrice, result.TotalPrice);
            Assert.Equal(saleDto.SaleDetails!.Count, result.SaleDetails!.Count);
            // Verifica que se llamó al repositorio
            saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        }

        // TC_VENTAS_010 – Validar que CreateAsync del SaleService retorne null al intentar vender con stock insuficiente


        [Fact]
        public async Task CreateAsync_ShouldThrowInsufficientStockException_WhenStockIsInsufficient()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var mockSaleRepository = new Mock<IBaseRepository<Sale>>();

            // Configurar mock: stock insuficiente
            mockProductService
                .Setup(s => s.IsStockAvailable(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            // Crear instancia del servicio sin IMapper
            var saleService = new SaleService(
                mockSaleRepository.Object,
                mockProductService.Object
            );

            // Datos de entrada
            var saleDto = new SaleCreateDto
            {
                IdCustomer = 101,
                IdSeller = 201,
                SaleDate = DateTime.UtcNow,
                SaleDetails = new List<SaleDetailCreateDto>
            {
                new SaleDetailCreateDto
                {
                    IdProduct = 301,
                    Quantity = 10,
                    UnitPrice = 20.00m
                }
            }
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InsufficientStockException>(
                () => saleService.CreateAsync(saleDto)
            );

            // Validaciones
            Assert.Contains("Stock insuficiente", exception.Message);
            mockSaleRepository.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Never);
        }


        // TC_VENTAS_011 – Validar que UpdateAsync del SaleService retorne la venta actualizada cuando la venta existe
        [Fact]
        public async Task UpdateAsync_ShouldUpdateSale_WhenSaleExists()
        {
            // Arrange
            var saleId = 1;
            var saleUpdateDto = new SaleUpdateDto
            {
                IdCustomer = 101,
                IdSeller = 201,
                TotalPrice = 200.75m,
                SaleDate = DateTime.Now,
                SaleDetails = new List<SaleDetailUpdateDto>
                {
                    new SaleDetailUpdateDto
                    {
                        IdProduct = 301,
                        Quantity = 3,
                        UnitPrice = 66.92m
                    }
                }
            };

            // Venta existente simulada en el repositorio
            var existingSale = new Sale
            {
                Id = saleId,
                IdCustomer = 100,
                IdSeller = 200,
                TotalPrice = 150.50m,
                SaleDate = DateTime.Now.AddDays(-1),
                SaleDetails = new List<SaleDetail>
                {
                    new SaleDetail { IdProduct = 301, Quantity = 2, UnitPrice = 75.25m }
                }
            };

            // Mock del repositorio
            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();
            saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId))
                              .ReturnsAsync(existingSale);

            saleRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Sale>()))
                              .ReturnsAsync((int id, Sale s) => s);

            var productServiceMock = new Mock<IProductService>();
            // Verifica stock
            productServiceMock.Setup(p => p.IsStockAvailable(It.IsAny<int>(), It.IsAny<int>()))
                              .ReturnsAsync(true);

            var saleService = new SaleService(saleRepositoryMock.Object, productServiceMock.Object);

            // Act
            var result = await saleService.UpdateAsync(saleId, saleUpdateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(saleId, result.Id);
            Assert.Equal(saleUpdateDto.IdCustomer, result.IdCustomer);
            Assert.Equal(saleUpdateDto.IdSeller, result.IdSeller);
            Assert.Equal(saleUpdateDto.TotalPrice, result.TotalPrice);
            Assert.Equal(saleUpdateDto.SaleDetails.Count, result.SaleDetails.Count);

            // Verifica que el repositorio recibió la llamada a UpdateAsync
            saleRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Sale>()), Times.Once);
        }

        // TC_VENTAS_012 – Validar que UpdateAsync del SaleService retorne null al intentar actualizar una venta con stock insuficiente
        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenStockIsInsufficient()
        {
            // Arrange
            var saleId = 1;
            var saleUpdateDto = new SaleUpdateDto
            {
                IdCustomer = 101,
                IdSeller = 201,
                TotalPrice = 200.75m,
                SaleDate = DateTime.Now,
                SaleDetails = new List<SaleDetailUpdateDto>
                {
                    new SaleDetailUpdateDto
                    {
                        IdProduct = 301,
                        Quantity = 10, // excede stock
                        UnitPrice = 66.92m
                    }
                }
            };

            // Stub del ProductService que siempre devuelve false para stock
            var productServiceStub = new ProductServiceStub();
            // Stub del repositorio que devuelve la venta existente
            var saleRepositoryStub = new SaleRepositoryStub();

            var saleService = new SaleService(saleRepositoryStub, productServiceStub);

            // Act
            var result = await saleService.UpdateAsync(saleId, saleUpdateDto);

            // Assert
            Assert.Null(result);
        }

        // TC_VENTAS_017 – Validar que DeleteAsync del SaleService elimine correctamente una venta existente
        [Fact]
        public async Task DeleteAsync_ShouldDeleteSale_WhenSaleExists()
        {
            // Arrange
            var saleId = 1;

            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();
            var productServiceMock = new Mock<IProductService>();

            // Simula que existe la venta
            saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId))
                              .ReturnsAsync(new Sale
                              {
                                  Id = saleId,
                                  IdCustomer = 101,
                                  IdSeller = 201,
                                  TotalPrice = 100m,
                                  SaleDate = DateTime.Now,
                                  SaleDetails = new List<SaleDetail>() // 🔹 Inicializa la lista requerida
                              });

            // Simula DeleteAsync sin hacer nada
            saleRepositoryMock.Setup(r => r.DeleteAsync(saleId))
                              .ReturnsAsync(true);

            var saleService = new SaleService(saleRepositoryMock.Object, productServiceMock.Object);

            // Act
            await saleService.DeleteAsync(saleId);

            // Assert
            saleRepositoryMock.Verify(r => r.DeleteAsync(saleId), Times.Once);
        }

        //TC_VENTAS_018 – Validar que DeleteAsync del SaleService falle al intentar eliminar una venta inexistente
        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_WhenSaleDoesNotExist()
        {
            // Arrange
            var nonexistentSaleId = 9999;

            // Stub del repositorio que siempre retorna null al buscar cualquier venta
            var saleRepositoryStub = new SaleRepositoryStub();
            var productServiceStub = new ProductServiceStub();

            var saleService = new SaleService(saleRepositoryStub, productServiceStub);

            // Act
            var result = await saleService.DeleteAsync(nonexistentSaleId);

            // Assert
            Assert.False(result);
        }
    }
}