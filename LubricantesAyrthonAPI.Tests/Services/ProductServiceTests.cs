

using LubricantesAyrthonAPI.Exceptions;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Dtos;
using LubricantesAyrthonAPI.Services.Implementations;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Services
{
    public class ProductServiceTests
    {
        // TC_PRODUCTOS_001 – Validar que GetAll del ProductService retorne lista de productos existentes
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

        // TC_PRODUCTOS_002 – Validar que GetAll del ProductService retorne lista vacía si no hay productos
        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Product>>();
            mockRepository.Setup(r => r.GetAllAsync())
                          .ReturnsAsync(new List<Product>()); // Simula lista vacía

            var productService = new ProductService(mockRepository.Object);

            // Act
            var result = await productService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);   // La lista debe estar vacía
        }

        // TC_PRODUCTOS_005 – Validar que GetByIdAsync del ProductService retorne producto existente por ID
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

        // TC_PRODUCTOS_006 – Validar que GetByIdAsync del ProductService lance ProductNotFoundException si el ID es inválido
        [Fact]
        public async Task GetByIdAsync_ThrowsProductNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange
            var repositoryMock = new Mock<IBaseRepository<Product>>();
            repositoryMock.Setup(r => r.GetByIdAsync(999))
                          .ReturnsAsync((Product?)null); // Mock devuelve null

            var productService = new ProductService(repositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => productService.GetByIdAsync(999));
            repositoryMock.Verify(r => r.GetByIdAsync(999), Times.Once); // Verifica que se llamó una vez
        }

        // TC_PRODUCTOS_009 – Validar que CreateAsync del ProductService agregue un producto correctamente
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

        // TC_PRODUCTOS_010 – Validar que CreateAsync del ProductService lance excepción si se intenta crear producto con nombre duplicado
        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenDuplicateName()
        {
            // Arrange
            var existingProduct = new Product { Id = 1, Name = "Aceite Motor 10W40", Stock = 50, Price = 25m };

            var mockRepository = new Mock<IBaseRepository<Product>>();

            // Mockear GetAllAsync para que devuelva el producto existente
            mockRepository.Setup(r => r.GetAllAsync())
                          .ReturnsAsync(new List<Product> { existingProduct });

            var productService = new ProductService(mockRepository.Object);

            var newProductDto = new ProductCreateDto
            {
                Name = "Aceite Motor 10W40",
                Price = 25.00m,
                Stock = 50
            };

            // Act & Assert
            await Assert.ThrowsAsync<DuplicateProductNameException>(() => productService.CreateAsync(newProductDto));

            // Verificar que no se llama a AddAsync
            mockRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }


        // TC_PRODUCTOS_013 – Validar que UpdateAsync del ProductService modifique producto existente correctamente
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


        // TC_PRODUCTOS_014 – Validar que UpdateAsync del ProductService lance ProductNotFoundException si el producto no existe
        [Fact]
        public async Task UpdateAsync_ShouldThrowKeyNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange
            int nonExistingId = 999;

            var mockRepository = new Mock<IBaseRepository<Product>>();
            mockRepository.Setup(r => r.GetByIdAsync(nonExistingId))
                          .ReturnsAsync((Product?)null); // Producto no existe

            var productService = new ProductService(mockRepository.Object);

            var updateDto = new ProductUpdateDto
            {
                Name = "Aceite Motor 15W40",
                Price = 35m,
                Stock = 20
            };

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => productService.UpdateAsync(nonExistingId, updateDto));

            // Verificar que GetByIdAsync fue llamado una vez
            mockRepository.Verify(r => r.GetByIdAsync(nonExistingId), Times.Once);

            // Verificar que UpdateAsync no fue llamado
            mockRepository.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Product>()), Times.Never);
        }




        // TC_PRODUCTOS_017 – Validar que DeleteAsync del ProductService elimine producto existente correctamente

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

        //TC_PRODUCTOS_018 – Validar que DeleteAsync del ProductService retorne false al intentar eliminar producto inexistente
        [Fact]
        public async Task DeleteProduct_NonExistent_ReturnsFalse()
        {
            // Arrange
            var productRepositoryMock = new Mock<IBaseRepository<Product>>();

            // Configurar el mock para que GetByIdAsync devuelva null
            productRepositoryMock.Setup(r => r.GetByIdAsync(999))
                                 .ReturnsAsync((Product?)null);

            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.DeleteAsync(999);

            // Assert
            Assert.False(result);
            productRepositoryMock.Verify(r => r.GetByIdAsync(999), Times.Once);
            productRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
        // TC_PRODUCTOS_021 – Validar que IsStockAvailable retorne true si el stock es suficiente
        [Fact]
        public async Task IsStockAvailable_ShouldReturnTrue_WhenStockIsSufficient()
        {
            // Arrange
            var existingProduct = new Product
            {
                Id = 1,
                Name = "Aceite Motor 10W40",
                Stock = 50,
                Price = 25m
            };

            var mockRepository = new Mock<IBaseRepository<Product>>();
            mockRepository.Setup(r => r.GetByIdAsync(existingProduct.Id))
                          .ReturnsAsync(existingProduct); // Simula producto con stock suficiente

            var productService = new ProductService(mockRepository.Object);

            int quantityRequested = 30; // Cantidad menor o igual al stock

            // Act
            var result = await productService.IsStockAvailable(existingProduct.Id, quantityRequested);

            // Assert
            Assert.True(result); // Debe retornar true
        }

        // TC_PRODUCTOS_022 – Validar que IsStockAvailable retorne false si el stock es insuficiente
        [Fact]
        public async Task IsStockAvailable_ShouldReturnFalse_WhenStockIsInsufficient()
        {
            // Arrange
            var existingProduct = new Product
            {
                Id = 1,
                Name = "Aceite Motor 10W40",
                Stock = 5,
                Price = 25m
            };

            var mockRepository = new Mock<IBaseRepository<Product>>();
            mockRepository.Setup(r => r.GetByIdAsync(existingProduct.Id))
                          .ReturnsAsync(existingProduct); // Stock insuficiente

            var productService = new ProductService(mockRepository.Object);

            int quantityRequested = 10; // Excede stock

            // Act
            var result = await productService.IsStockAvailable(existingProduct.Id, quantityRequested);

            // Assert
            Assert.False(result);
        }

        // TC_PRODUCTOS_023 – Validar que UpdateStockAfterSaleAsync actualice correctamente el stock si la cantidad vendida es menor o igual al stock disponible
        [Fact]
        public async Task UpdateStockAfterSaleAsync_ShouldReturnTrue_WhenStockIsSufficient()
        {
            // Arrange
            var existingProduct = new Product
            {
                Id = 1,
                Name = "Aceite Motor 10W40",
                Stock = 50,
                Price = 25m
            };

            var mockRepository = new Mock<IBaseRepository<Product>>();
            mockRepository.Setup(r => r.GetByIdAsync(existingProduct.Id))
                          .ReturnsAsync(existingProduct); // Producto existente

            mockRepository.Setup(r => r.UpdateAsync(existingProduct.Id, It.IsAny<Product>()))
                          .ReturnsAsync((int id, Product p) => p); // Retorna el producto actualizado

            var productService = new ProductService(mockRepository.Object);

            int quantitySold = 20; // Cantidad menor o igual al stock

            // Act
            var result = await productService.UpdateStockAfterSaleAsync(existingProduct.Id, quantitySold);

            // Assert
            Assert.True(result);
            Assert.Equal(30, existingProduct.Stock); // Stock actualizado correctamente
        }

        // TC_PRODUCTOS_024 – Validar que UpdateStockAfterSaleAsync retorne false si la cantidad vendida excede el stock disponible
        [Fact]
        public async Task UpdateStockAfterSaleAsync_ShouldReturnFalse_WhenQuantityExceedsStock()
        {
            // Arrange
            var existingProduct = new Product
            {
                Id = 1,
                Name = "Aceite Motor 10W40",
                Stock = 5, // Stock actual
                Price = 25m
            };

            var mockRepository = new Mock<IBaseRepository<Product>>();
            mockRepository.Setup(r => r.GetByIdAsync(existingProduct.Id))
                          .ReturnsAsync(existingProduct); // Producto existente con stock limitado

            var productService = new ProductService(mockRepository.Object);

            int quantitySold = 10; // Excede stock

            // Act
            var result = await productService.UpdateStockAfterSaleAsync(existingProduct.Id, quantitySold);

            // Assert
            Assert.False(result);
            Assert.Equal(5, existingProduct.Stock); // Stock no se modifica
        }

    }

}