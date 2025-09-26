using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Implementations;
using Microsoft.AspNetCore.Connections;
using Moq;

namespace LubricantesAyrthonAPI.Tests.TestDoubles.Stubs
{
    public class SellerServiceTests
    {

        // TC_001: Validar creación de vendedor con datos válidos
        [Fact]
        public async Task CreateSeller_WithValidData_Success()
        {
            // Arrange
            var sellerDto = new SellerCreateDto
            {
                Ci = "2002444",
                Name = "Roberto Murillo",
                Age = 20,
                Email = "robertomurillo@gmail.com",
                Phone = "77766656",
                Address = "Av. Murillo",
                Salary = 2750
            };

            var expectedSeller = new Seller
            {
                Id = 1,
                Ci = "2002444",
                Name = "Roberto Murillo",
                Age = 20,
                Email = "robertomurillo@gmail.com",
                Phone = "77766656",
                Address = "Av. Murillo",
                Salary = 2750
            };

            var sellerRepositoryMock = new Mock<IBaseRepository<Seller>>();

            sellerRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Seller>()))
                          .ReturnsAsync(expectedSeller);


            var sellerService = new SellerService(sellerRepositoryMock.Object);

            // Act
            var result = await sellerService.CreateAsync(sellerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedSeller.Ci, result.Ci);
            Assert.Equal(expectedSeller.Name, result.Name);
            Assert.Equal(expectedSeller.Salary, result.Salary);
            // Verifica que se llamó al repositorio
            sellerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Seller>()), Times.Once);
        }

        //TC_007: Validar creación de vendedor con repositorio fallante
        [Fact]
        public async Task CreateAsync_RepositorioFalla_RetornaNull()
        {
            // Arrange
            var sellerDto = new SellerCreateDto
            {
                Ci = "2002444",
                Name = "Roberto Murillo",
                Age = 20,
                Email = "robertomurillo@gmail.com",
                Phone = "77766656",
                Address = "Av. Murillo",
                Salary = 2750
            };

            var mockRepository = new Mock<IBaseRepository<Seller>>();

            mockRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Seller)null);
            // Configuramos el mock para que AddAsync devuelva null
            mockRepository.Setup(r => r.AddAsync(It.IsAny<Seller>()))
                          .ReturnsAsync((Seller)null);

            var service = new SellerService(mockRepository.Object);


            // Act
            var result = await service.CreateAsync(sellerDto);

            // Assert
            Assert.Null(result);


            // Verificar que AddAsync fue llamado con los datos correctos
            mockRepository.Verify(r => r.AddAsync(It.IsAny<Seller>()), Times.Once);
        }

        //TC_002: Validar obtención de todos los vendedores
        [Fact]
        public async Task GetAllAsync_RepositorioContieneRegistros_RetornaListaDeSellerReadDto()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();
            var sellersList = new List<Seller>
        {
            new Seller
            {
                Id = 1,
                Ci = "123",
                Name = "Juan Pérez",
                Age = 30,
                Email = "juan@test.com",
                Phone = "77777777",
                Address = "Av. Siempre Viva",
                Salary = 3500
            }
        };

            mockRepository.Setup(r => r.GetAllAsync())
                          .ReturnsAsync(sellersList);

            var service = new SellerService(mockRepository.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result); // La lista no debe ser nula
            Assert.Equal(sellersList.Count, result.Count()); // Debe contener la misma cantidad de elementos
            Assert.Equal(sellersList[0].Id, result.First().Id); // Verifica mapeo correcto
            Assert.Equal(sellersList[0].Ci, result.First().Ci);
            Assert.Equal(sellersList[0].Name, result.First().Name);
            Assert.Equal(sellersList[0].Age, result.First().Age);
            Assert.Equal(sellersList[0].Email, result.First().Email);
            Assert.Equal(sellersList[0].Phone, result.First().Phone);
            Assert.Equal(sellersList[0].Address, result.First().Address);
            Assert.Equal(sellersList[0].Salary, result.First().Salary);
        }


        //TC_003: Validar obtención de todos los vendedores sin registros
        [Fact]
        public async Task GetAllAsync_RepositorioSinRegistros_RetornaListaVacia()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();
            var emptyList = new List<Seller>();

            mockRepository.Setup(r => r.GetAllAsync())
                          .ReturnsAsync(emptyList);

            var service = new SellerService(mockRepository.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);          // La lista no debe ser nula
            Assert.Empty(result);            // La lista debe estar vacía
            Assert.Equal(emptyList.Count, result.Count());

        }

        //TC_004: Validar obtención de vendedor por Id
        [Fact]
        public async Task GetByIdAsync_IdExistente_RetornaVendedor()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();
            var seller = new Seller
            {
                Id = 1,
                Ci = "123",
                Name = "Juan Pérez",
                Age = 30,
                Email = "juan@test.com",
                Phone = "77777777",
                Address = "Av. Siempre Viva",
                Salary = 3500
            };

            mockRepository.Setup(r => r.GetByIdAsync(1))
                          .ReturnsAsync(seller);

            var service = new SellerService(mockRepository.Object);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);              // El resultado no debe ser nulo
            Assert.Equal(seller.Id, result.Id);
            Assert.Equal(seller.Ci, result.Ci);
            Assert.Equal(seller.Name, result.Name);
            Assert.Equal(seller.Age, result.Age);
            Assert.Equal(seller.Email, result.Email);
            Assert.Equal(seller.Phone, result.Phone);
            Assert.Equal(seller.Address, result.Address);
            Assert.Equal(seller.Salary, result.Salary);
        }


        //TC_005: Validar obtención de vendedor por Id inexistente
        [Fact]
        public async Task GetByIdAsync_IdNoExistente_RetornaNull()
        {
            // Arrange
            var id = 99;
            var mockRepository = new Mock<IBaseRepository<Seller>>();
            mockRepository.Setup(r => r.GetByIdAsync(id))
                          .ReturnsAsync((Seller)null); // No existe vendedor con este ID

            var service = new SellerService(mockRepository.Object);

            // Act
            var result = await service.GetByIdAsync(id);

            // Assert
            Assert.Null(result); // Se espera null
            mockRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        //TC_008: Validar actualización de vendedor existente
        [Fact]
        public async Task UpdateAsync_ExistingSeller_ReturnsUpdatedSeller()
        {
            // Arrange
            var existingSeller = new Seller
            {
                Id = 1,
                Ci = "1234567",
                Name = "Carlos López",
                Age = 29,
                Email = "carlos.lopez@test.com",
                Phone = "77712345",
                Address = "Calle Falsa 123",
                Salary = 4000
            };

            var mockRepository = new Mock<IBaseRepository<Seller>>();
            mockRepository.Setup(r => r.GetByIdAsync(1))
                          .ReturnsAsync(existingSeller);

            mockRepository.Setup(r => r.UpdateAsync(1, It.IsAny<Seller>()))
                          .ReturnsAsync((int id, Seller s) => s);

            var service = new SellerService(mockRepository.Object);

            var updateDto = new SellerUpdateDto
            {
                Ci = "1234567",
                Name = "Carlos López Actualizado",
                Age = 29,
                Email = "carlos.lopez.update@test.com",
                Phone = "77712346",
                Address = "Calle Falsa 124",
                Salary = 4200
            };

            // Act
            var result = await service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Email, result.Email);
            Assert.Equal(updateDto.Phone, result.Phone);
            Assert.Equal(updateDto.Address, result.Address);
            Assert.Equal(updateDto.Salary, result.Salary);
        }

        //TC_009: Validar actualización de vendedor inexistente
        [Fact]
        public async Task UpdateAsync_NonExistentSeller_ReturnsNull()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();
            mockRepository.Setup(r => r.GetByIdAsync(99))
                          .ReturnsAsync((Seller?)null);

            var service = new SellerService(mockRepository.Object);

            var updateDto = new SellerUpdateDto
            {
                Ci = "7654321",
                Name = "Ana Torres",
                Age = 35,
                Email = "ana.torres@test.com",
                Phone = "77798765",
                Address = "Av. Central 456",
                Salary = 4500
            };

            // Act
            var result = await service.UpdateAsync(99, updateDto);

            // Assert
            Assert.Null(result);
            // Validar que se haya llamado al repositorio con el ID correcto
            mockRepository.Verify(r => r.GetByIdAsync(99), Times.Once);
            // Validar que no se haya llamado al repositorio para actualizar
            mockRepository.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Seller>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_RepositoryUpdateFails_ReturnsNull()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();


            // Vendedor existente
            var seller = new Seller
            {
                Id = 1,
                Ci = "7654321",
                Name = "Ana Torres",
                Age = 35,
                Email = "ana.torres@test.com",
                Phone = "77798765",
                Address = "Av. Central 456",
                Salary = 4500
            };

            // Mock de GetByIdAsync devuelve el vendedor
            mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(seller);

            // Mock de UpdateAsync devuelve null, simulando fallo en el repositorio
            mockRepository
                .Setup(r => r.UpdateAsync(1, It.IsAny<Seller>()))
                .ReturnsAsync((Seller?)null);

            var service = new SellerService(mockRepository.Object);

            // Datos de prueba
            var updateDto = new SellerUpdateDto
            {

                Ci = "76543214",
                Name = "Ana Torres Modificada",
                Age = 36,
                Email = "ana.torres.mod@test.com",
                Phone = "77798766",
                Address = "Av. Central 457",
                Salary = 4600
            };

            // Act
            var result = await service.UpdateAsync(1, updateDto);

            // Assert
            Assert.Null(result); // Debe retornar null

            mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);

            mockRepository.Verify(r => r.UpdateAsync(1, It.IsAny<Seller>()), Times.Once); // Verifica que se llamó una vez
        }

        //TC_010: Validar eliminación de vendedor existente
        [Fact]
        public async Task DeleteAsync_VendedorExistente_RetornaTrue()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();

            var seller = new Seller
            {
                Id = 1,
                Ci = "1234567",
                Name = "Carlos López",
                Age = 29,
                Email = "carlos.lopez@test.com",
                Phone = "77712345",
                Address = "Calle Falsa 124",
                Salary = 4200
            };

            // Configurar GetByIdAsync para retornar el vendedor existente
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(seller);
            // Configurar DeleteAsync como tarea completada
            mockRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var service = new SellerService(mockRepository.Object);

            // Act
            var result = await service.DeleteAsync(1);

            // Assert
            Assert.True(result); // El método debe retornar true

            // Validar que se haya llamado al repositorio con el ID correcto
            mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);

            // Validar que se haya llamado al repositorio para eliminar
            mockRepository.Verify(r => r.DeleteAsync(1), Times.Once); // Verificar que DeleteAsync fue llamado
        }


        //TC_011: Validar eliminación de vendedor inexistente
        [Fact]
        public async Task DeleteAsync_WhenSellerDoesNotExist_ReturnsFalse()
        {
            // Arrange
            int id = 99;
            var mockRepository = new Mock<IBaseRepository<Seller>>();

            mockRepository
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Seller?)null);

            var service = new SellerService(mockRepository.Object);

            // Act
            var result = await service.DeleteAsync(99);

            // Assert
            Assert.False(result);
            // Validar que se haya llamado al repositorio con el ID correcto
            mockRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
            // Validar que no se haya llamado al repositorio para eliminar
            mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_WhenRepositoryDeleteFails_ReturnsFalse()
        {
            // Arrange
            int sellerId = 1;

            var existingSeller = new Seller
            {
                Id = sellerId,
                Ci = "123456",
                Name = "Juan Pérez",
                Age = 30,
                Email = "juan.perez@example.com",
                Phone = "111222333",
                Address = "Calle Falsa 123",
                Salary = 5000
            };
            
            var _mockRepository = new Mock<IBaseRepository<Seller>>();
            // Simula que el vendedor sí existe
            _mockRepository.Setup(r => r.GetByIdAsync(sellerId))
                           .ReturnsAsync(existingSeller);

            // Simula que la eliminación falla en el repositorio
            _mockRepository.Setup(r => r.DeleteAsync(sellerId))
                           .ReturnsAsync(false);

            var _service = new SellerService(_mockRepository.Object);

            // Act
            var result = await _service.DeleteAsync(sellerId);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(r => r.GetByIdAsync(sellerId), Times.Once);
            _mockRepository.Verify(r => r.DeleteAsync(sellerId), Times.Once);
        }

    }
}