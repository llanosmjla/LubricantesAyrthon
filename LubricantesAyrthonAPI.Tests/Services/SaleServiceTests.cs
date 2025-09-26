

using LubricantesAyrthonAPI.Dtos;
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

        //TC_001 – Validar creación de venta con datos válidos
        [Fact]
        public async Task CreateSale_WithValidData_Success()
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
                        Quantity = 2,
                        UnitPrice = 50.00m
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

            saleRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Sale>()))
                          .ReturnsAsync(expectedSale);


            var saleService = new SaleService(saleRepositoryMock.Object);

            // Act
            var result = await saleService.CreateAsync(saleDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(saleDto.IdCustomer, result.IdCustomer);
            Assert.Equal(saleDto.IdSeller, result.IdSeller);
            Assert.Equal(expectedTotalPrice, result.TotalPrice);
            Assert.Equal(saleDto.SaleDetails!.Count, result.SaleDetails!.Count);
            // Verifica que se llamó al repositorio
            saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        }

        // TC_010: Validar creación de venta con repositorio fallante
        [Fact]
        public async Task CreateSale_WhenRepositoryFails_ThrowsException()
        {
            // Arrange
            var saleDto = new SaleCreateDto
            {
                IdCustomer = 1,
                IdSeller = 1,
                SaleDate = DateTime.Now,
                SaleDetails = new List<SaleDetailCreateDto>
                {
                    new SaleDetailCreateDto { IdProduct = 1, Quantity = 2, UnitPrice = 50 }
                }
            };

            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();
            saleRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Sale>()))
                .ThrowsAsync(new Exception("Repositorio falló"));

            var saleService = new SaleService(saleRepositoryMock.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => saleService.CreateAsync(saleDto));
            Assert.Equal("Repositorio falló", ex.Message);

            // Verifica que se llamó al repositorio
            saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);

        }

        // TC_015: Validar creación de venta con repositorio fallante
        [Fact]
        public async Task TC_015_CreateSale_AddAsyncReturnsNull_ReturnsNull()
        {
            // Arrange
            var saleDto = new SaleCreateDto
            {
                IdCustomer = 1,
                IdSeller = 1,
                SaleDate = DateTime.Now,
                SaleDetails = new List<SaleDetailCreateDto>
                {
                    new SaleDetailCreateDto { IdProduct = 1, Quantity = 2, UnitPrice = 50 }
                }
            };

            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();
            saleRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Sale>()))
                .ReturnsAsync((Sale?)null); // simulamos que el repositorio falla

            var saleService = new SaleService(saleRepositoryMock.Object);

            // Act
            var result = await saleService.CreateAsync(saleDto);

            // Assert
            Assert.Null(result); // validamos que se retorne null

            // Verifica que se llamó al repositorio
            saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        }


        // TC_005: Validar obtención de venta por ID    
        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectSale_WhenIdExists()
        {
            // Arrange
            var mockRepo = new Mock<IBaseRepository<Sale>>();
            var expectedSale = new Sale
            {
                Id = 1,
                IdCustomer = 1,
                IdSeller = 1,
                TotalPrice = 100,
                SaleDate = DateTime.Today,
                SaleDetails = new List<SaleDetail>
                {
                    new SaleDetail { Id = 1, IdSale = 1, IdProduct = 1, Quantity = 2, UnitPrice = 50 }
                }
            };

            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expectedSale);

            var service = new SaleService(mockRepo.Object);

            // Act
            var result = await service.GetByIdAsync(1);
 
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedSale.Id, result.Id);
            Assert.Equal(expectedSale.IdCustomer, result.IdCustomer);
            Assert.Equal(expectedSale.IdSeller, result.IdSeller);
            Assert.Equal(expectedSale.TotalPrice, result.TotalPrice);
            Assert.Equal(expectedSale.SaleDate, result.SaleDate);
            Assert.NotEmpty(result.SaleDetails);
            Assert.Equal(expectedSale.SaleDetails[0].IdProduct, result.SaleDetails[0].IdProduct);

            mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Sale>()), Times.Never);
        }

        // TC_007: Validar obtención de venta inexistente por ID
        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenSaleDoesNotExist()
        {
            // Arrange
            var repoMock = new Mock<IBaseRepository<Sale>>();
            repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Sale)null);

            var service = new SaleService(repoMock.Object);

            // Act
            var result = await service.GetByIdAsync(99);

            // Assert
            Assert.Null(result);
            repoMock.Verify(r => r.GetByIdAsync(99), Times.Once);
            repoMock.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Sale>()), Times.Never);
        }


        // TC_013: Validar obtención de todas las ventas
        [Fact]
        public async Task TC_013_GetAllSales_WithStub_ReturnsAllSales()
        {
            // Arrange
            var stubRepo = new SaleRepositoryStub();
            var saleService = new SaleService(stubRepo);

            // Act
            var result = (await saleService.GetAllAsync()).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Solo hay una venta en el stub
            Assert.Equal(1, result[0].Id);
            Assert.Equal(1, result[0].IdCustomer);
            Assert.Equal(1, result[0].IdSeller);
            Assert.Equal(100m, result[0].TotalPrice);
            Assert.Single(result[0].SaleDetails);
            Assert.Equal(1, result[0].SaleDetails[0].IdProduct);
            Assert.Equal(1, result[0].SaleDetails[0].Quantity);
            Assert.Equal(1m, result[0].SaleDetails[0].UnitPrice);
        }

        // TC_014: Validar obtención de todas las ventas con repositorio fallante
        [Fact]
        public async Task GetAllAsync_RepositoryReturnsNull_ReturnsEmptyList()
        {
            // Arrange
            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();

            // Mockeamos que el repositorio devuelve null
            saleRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync((IEnumerable<Sale>?)null);

            var saleService = new SaleService(saleRepositoryMock.Object);

            // Act
            var result = await saleService.GetAllAsync();

            // Assert
            Assert.NotNull(result); // El método no debe retornar null
            Assert.Empty(result);   // Debe retornar lista vacía
            saleRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }


        // TC_008: Validar eliminación de venta
        [Fact]
        public async Task DeleteSale_WithStub_RemovesSale()
        {
            // Arrange
            var existingSale = new Sale
            {
                Id = 1,
                IdCustomer = 1,
                IdSeller = 1,
                TotalPrice = 100,
                SaleDate = DateTime.Today,
                SaleDetails = new List<SaleDetail>
                {
                    new SaleDetail { Id = 1, IdSale = 1, IdProduct = 1, Quantity = 2, UnitPrice = 50 }
                }
            };

            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();

            saleRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(existingSale);

            saleRepositoryMock
                .Setup(r => r.DeleteAsync(1))
                .ReturnsAsync(true);

            var saleService = new SaleService(saleRepositoryMock.Object);

            // Act
            var result = await saleService.DeleteAsync(1);

            // Assert
            Assert.True(result);
            saleRepositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            saleRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        // TC_007: Calcular el precio total de la venta
        [Fact]
        public void CalculateTotalPrice_WithValidDetails_ReturnsCorrectSum()
        {
            // Arrange
            var saleDetails = new List<SaleDetailCreateDto>
            {
                new SaleDetailCreateDto { IdProduct = 1, Quantity = 2, UnitPrice = 50.0m },
                new SaleDetailCreateDto { IdProduct = 2, Quantity = 1, UnitPrice = 30.0m }
            };

            var saleService = new SaleService(null!); // repositorio no necesario para este test

            // Act
            var result = saleService.CalculateTotalPrice(saleDetails);

            // Assert
            Assert.Equal(130.0m, result);
        }


        // TC_003: Validar la no eliminación de una venta inexistente
        [Fact]
        public async Task DeleteAsync_WhenSaleDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();

            saleRepositoryMock
                .Setup(r => r.GetByIdAsync(99))
                .ReturnsAsync((Sale?)null);

            var saleService = new SaleService(saleRepositoryMock.Object);

            // Act
            var result = await saleService.DeleteAsync(99);

            // Assert
            Assert.False(result);
            saleRepositoryMock.Verify(r => r.GetByIdAsync(99), Times.Once);
            saleRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        // TC_004: Validar actualización de venta existente
        [Fact]
        public async Task UpdateAsync_ShouldUpdateSale_WhenSaleExists()
        {
            // Arrange
            var existingSale = new Sale
            {
                Id = 1,
                IdCustomer = 1,
                IdSeller = 1,
                TotalPrice = 100,
                SaleDate = DateTime.Today,
                SaleDetails = new List<SaleDetail>()
            };

            var updateDto = new SaleUpdateDto
            {
                IdCustomer = 2,
                IdSeller = 2,
                TotalPrice = 200,
                SaleDate = DateTime.Today,
                SaleDetails = new List<SaleDetailUpdateDto>
                {
                    new SaleDetailUpdateDto { IdProduct = 5, Quantity = 4, UnitPrice = 50 }
                }
            };

            var updatedSale = new Sale
            {
                Id = 1,
                IdCustomer = updateDto.IdCustomer,
                IdSeller = updateDto.IdSeller,
                TotalPrice = updateDto.TotalPrice,
                SaleDate = updateDto.SaleDate,
                SaleDetails = updateDto.SaleDetails.Select(sd => new SaleDetail
                {
                    IdProduct = sd.IdProduct,
                    Quantity = sd.Quantity,
                    UnitPrice = sd.UnitPrice
                }).ToList()
            };

            var repoMock = new Mock<IBaseRepository<Sale>>();
            repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingSale);
            repoMock.Setup(r => r.UpdateAsync(1, It.IsAny<Sale>())).ReturnsAsync(updatedSale);

            var service = new SaleService(repoMock.Object);

            // Act
            var result = await service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.IdCustomer);
            Assert.Equal(200, result.TotalPrice);
            Assert.Single(result.SaleDetails);

            repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            repoMock.Verify(r => r.UpdateAsync(1, It.IsAny<Sale>()), Times.Once);
        }

        // TC_006: Validar actualización de venta inexistente
        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenSaleDoesNotExist()
        {
            // Arrange
            var updateDto = new SaleUpdateDto
            {
                IdCustomer = 2,
                IdSeller = 2,
                TotalPrice = 200,
                SaleDate = DateTime.Today,
                SaleDetails = new List<SaleDetailUpdateDto>
                {
                    new SaleDetailUpdateDto { IdProduct = 5, Quantity = 4, UnitPrice = 50 }
                }
            };

            var repoMock = new Mock<IBaseRepository<Sale>>();
            repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Sale)null);

            var service = new SaleService(repoMock.Object);

            // Act
            var result = await service.UpdateAsync(1, updateDto);

            // Assert
            Assert.Null(result);
            repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            repoMock.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Sale>()), Times.Never);
        }

        // TC_009: Validar actualización de venta con repositorio fallante
        [Fact]
        public async Task UpdateSale_RepositoryFails_ReturnsNull()
        {
            // Arrange
            var existingSale = new Sale
            {
                Id = 1,
                IdCustomer = 1,
                IdSeller = 1,
                TotalPrice = 100,
                SaleDate = DateTime.Today,
                SaleDetails = new List<SaleDetail>
                {
                    new SaleDetail { Id = 1, IdSale = 1, IdProduct = 1, Quantity = 2, UnitPrice = 50 }
                }
            };

            var saleUpdateDto = new SaleUpdateDto
            {
                IdCustomer = 2,
                IdSeller = 3,
                TotalPrice = 200,
                SaleDate = DateTime.Today,
                SaleDetails = new List<SaleDetailUpdateDto>
                {
                    new SaleDetailUpdateDto { IdProduct = 1, Quantity = 2, UnitPrice = 100 }
                }
            };

            var saleRepositoryMock = new Mock<IBaseRepository<Sale>>();

            // Mockear GetByIdAsync para devolver venta existente
            saleRepositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(existingSale);

            // Mockear UpdateAsync para que falle (devuelva null)
            saleRepositoryMock
                .Setup(r => r.UpdateAsync(1, It.IsAny<Sale>()))
                .ReturnsAsync((Sale?)null);

            var saleService = new SaleService(saleRepositoryMock.Object);

            // Act
            var result = await saleService.UpdateAsync(1, saleUpdateDto);

            // Assert
            Assert.Null(result); // Camino no feliz: falla actualización
            saleRepositoryMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            saleRepositoryMock.Verify(r => r.UpdateAsync(1, It.IsAny<Sale>()), Times.Once);
        }

    }


}