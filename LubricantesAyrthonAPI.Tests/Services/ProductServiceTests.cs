

using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Dtos;
using LubricantesAyrthonAPI.Services.Implementations;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldCreateProduct_WhenDataIsValid()
        {
            // Arrange
            var productDto = new ProductCreateDto
            {
                Name = "Aceite 20W50",
                Price = 50.0m,
                Stock = 100,
                Description = "Aceite lubricante para motor" // opcional
            };

            var expectedProduct = new Product
            {
                Id = 1,
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock,
                Description = productDto.Description
            };

            var productoRepositoryMock = new Mock<IBaseRepository<Product>>();
            
            productoRepositoryMock
                .Setup(repository => repository.AddAsync(It.IsAny<Product>()))
                    .ReturnsAsync(expectedProduct);


            var productService = new ProductService(productoRepositoryMock.Object);

            // Act
            var result = await productService.CreateAsync(productDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
            Assert.Equal(expectedProduct.Price, result.Price);
            Assert.Equal(expectedProduct.Stock, result.Stock);
            Assert.Equal(expectedProduct.Description, result.Description);
            // Verificar si se llama a la función AddAsyn una sola vez
            productoRepositoryMock.Verify(repository => repository.AddAsync(It.IsAny<Product>()), Times.Once());
        }

        // TC_003 – Obtener todos los prouctos
        [Fact]

        public async Task GetAllAsync_ShouldReturnAllProducts_ReturnsSuccess()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Aceite 20W50", Price = 50.0m, Stock = 100 },
                new Product { Id = 2, Name = "Aceite 10W40", Price = 40.0m, Stock = 200 }
            };

            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
          
            productRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(expectedProducts);

            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProducts.Count, result.Count());
            Assert.Equal(expectedProducts[0].Name, result.ElementAt(0).Name);
            Assert.Equal(expectedProducts[1].Name, result.ElementAt(1).Name);
        }

        // TC_004 – Validar obtención de producto existente por Id
        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var expectedProduct = new Product
            {
                Id = 1,
                Name = "Aceite 20W50",
                Price = 50.0m,
                Stock = 100
            };

            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            productRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(expectedProduct);

            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
        }

        // TC_006 – Validar actualización de producto existente
        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedProduct_WhenProductExists()
        {
            // Arrange
            var updateDto = new ProductUpdateDto
            {
                Name = "Aceite 20W50 Premium",
                Price = 55.0m,
                Stock = 120
            };

            var existingProduct = new Product
            {
                Id = 1,
                Name = "Aceite 20W50",
                Price = 50.0m,
                Stock = 100
            };

            var updatedProduct = new Product
            {
                Id = 1,
                Name = updateDto.Name,
                Price = updateDto.Price,
                Stock = updateDto.Stock
            };

            // Configurar el GetByIdAsync para devolver el producto existente
            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
           
            productRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(existingProduct);

            //var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            productRepositoryMock
                .Setup(repo => repo.UpdateAsync(existingProduct.Id, It.IsAny<Product>()))
                .ReturnsAsync(updatedProduct);

            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.UpdateAsync(existingProduct.Id, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedProduct.Id, result.Id);
            Assert.Equal(updatedProduct.Name, result.Name);
            Assert.Equal(updatedProduct.Price, result.Price);
            Assert.Equal(updatedProduct.Stock, result.Stock);
        }

        // TC_008 – Validar eliminación de producto existente

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete_WhenProductExists()
        {
            // Arrange

            var existingProduct = new Product
            {
                Id = 1,
                Name = "Aceite 20W50",
                Price = 50.0m,
                Stock = 100
            };

            var productRepositoryMock = new Mock<IBaseRepository<Product>>();

            productRepositoryMock
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(existingProduct);

            productRepositoryMock
                .Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(true);

            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            await productService.DeleteAsync(1);

            // Assert
            productRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }


    }

}