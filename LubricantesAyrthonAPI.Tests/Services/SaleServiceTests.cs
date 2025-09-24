

using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Implementations;
using LubricantesAyrthonAPI.Services.Interfaces;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Services
{
    public class SaleServiceTests
    {

        //TC_001 – Validar creación de venta con datos válidos
        [Fact]
        public async Task TC_001_CreateSale_WithValidData_Success()
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

        //[Fact]

    }
    
}