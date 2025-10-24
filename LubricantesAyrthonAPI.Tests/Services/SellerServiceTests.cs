using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Exceptions;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Implementations;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Services
{
    public class SellerServiceTests
    {

        //TC_VENDEDORES_001 – Validar que GetAll del VendedorService retorne lista de vendedores existentes
        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfExistingSellers()
        {
            // Arrange
            var existingSeller = new Seller
            {
                Id = 1,
                Ci = "87654321",
                Name = "María López",
                Age = 25,
                Email = "maria.lopez@email.com",
                Phone = "555-4321",
                Address = "Calle Falsa 456",
                Salary = 3000
            };

            var mockRepository = new Mock<IBaseRepository<Seller>>();
            mockRepository.Setup(r => r.GetAllAsync())
                          .ReturnsAsync(new List<Seller> { existingSeller });

            var sellerService = new SellerService(mockRepository.Object);

            // Act
            var result = await sellerService.GetAllAsync();

            // Assert
            Assert.NotNull(result); // si result es IEnumerable
            Assert.NotEmpty(result);
            Assert.Single(result); // espera exactamente 1 elemento según el setup

            var seller = result.First();
            Assert.Equal(existingSeller.Id, seller.Id);
            Assert.Equal(existingSeller.Ci, seller.Ci);
            Assert.Equal(existingSeller.Name, seller.Name);
            Assert.Equal(existingSeller.Email, seller.Email);
            Assert.Equal(existingSeller.Phone, seller.Phone);
            Assert.Equal(existingSeller.Address, seller.Address);
            Assert.Equal(existingSeller.Salary, seller.Salary);

            mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        // TC_VENDEDORES_002 – Validar que GetAll del VendedorService retorne lista vacía si no hay vendedores
        [Fact]
        public async Task GetAllAsync_DebeRetornarListaVacia_SiNoHayVendedores()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();
            mockRepository.Setup(r => r.GetAllAsync())
                          .ReturnsAsync(new List<Seller>()); // Lista vacía

            var sellerService = new SellerService(mockRepository.Object);

            // Act
            var result = await sellerService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            var lista = result.ToList();
            Assert.Empty(lista);
            mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        // TC_VENDEDORES_005 – Validar que GetByIdAsync del VendedorService retorne vendedor existente por ID
        [Fact]
        public async Task GetByIdAsync_RepositorioConRegistro_RetornaVendedor()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();
            var existingSeller = new Seller
            {
                Id = 1,
                Ci = "87654321",
                Name = "María López",
                Age = 25,
                Email = "maria.lopez@email.com",
                Phone = "555-4321",
                Address = "Calle Falsa 456",
                Salary = 3000
            };

            mockRepository.Setup(r => r.GetByIdAsync(1))
                          .ReturnsAsync(existingSeller);

            var sellerService = new SellerService(mockRepository.Object);

            // Act
            var result = await sellerService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingSeller.Id, result.Id);
            Assert.Equal(existingSeller.Ci, result.Ci);
            Assert.Equal(existingSeller.Name, result.Name);
            Assert.Equal(existingSeller.Age, result.Age);
            Assert.Equal(existingSeller.Email, result.Email);
            Assert.Equal(existingSeller.Phone, result.Phone);
            Assert.Equal(existingSeller.Address, result.Address);
            Assert.Equal(existingSeller.Salary, result.Salary);
        }

        // TC_VENDEDORES_006 – Validar que GetByIdAsync del VendedorService lance KeyNotFoundException si el ID es inválido
        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenIdIsInvalid()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Seller>>();

            var sellerService = new SellerService(mockRepository.Object);

            int invalidId = -1; // Id inválido (<= 0)

            // Act & Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => sellerService.GetByIdAsync(invalidId)
            );

            // Opcional: validar mensaje que incluya el id
            Assert.Contains(invalidId.ToString(), ex.Message);

            // Verificar que no se haya consultado el repositorio cuando el id es inválido
            mockRepository.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }


        // TC_VENDEDORES_009 – Validar creación de vendedor con datos válidos
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

        //TC_VENDEDORES_010 – Validar que Create del VendedorService retorne null si el repositorio falla al crear el vendedor

        [Fact]
        public async Task CreateAsync_ShouldThrowDuplicateSellerEmailException_WhenEmailIsDuplicated()
        {
            // Datos de prueba
            var duplicateEmail = "carlos.mendoza@email.com";
            var existingSeller = new Seller
            {
                Id = 1,
                Name = "Ana Torres",
                Ci = "11223344",
                Age = 28,
                Email = duplicateEmail,
                Phone = "555-1111",
                Address = "Calle Sucre 456",
                Salary = 4000.00m
            };

            var newSellerDto = new SellerCreateDto
            {
                Name = "Carlos Mendoza",
                Ci = "76543210",
                Age = 30,
                Email = duplicateEmail,
                Phone = "555-6789",
                Address = "Av. Bolívar 123",
                Salary = 4500.00m
            };

            // Mock del repositorio
            var sellerRepoMock = new Mock<IBaseRepository<Seller>>();
            sellerRepoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(existingSeller);

            // Instancia del servicio
            var sellerService = new SellerService(sellerRepoMock.Object);

            // Ejecución y verificación
            var exception = await Assert.ThrowsAsync<DuplicateSellerEmailException>(() =>
                sellerService.CreateAsync(newSellerDto)
            );

            Assert.Contains(duplicateEmail, exception.Message);

            // Verificar que AddAsync no fue llamado
            sellerRepoMock.Verify(r => r.AddAsync(It.IsAny<Seller>()), Times.Never);
        }

        // TC_VENDEDORES_013 – Validar que UpdateAsync del VendedorService modifique datos de vendedor existente correctamente
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

       // TC_VENDEDORES_014 – Validar que UpdateAsync del VendedorService lance SellerNotFoundException si el vendedor no existe
        [Fact]
        public async Task UpdateAsync_NonExistentSeller_ThrowsSellerNotFoundException()
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

            // Act & Assert
            await Assert.ThrowsAsync<SellerNotFoundException>(() =>
                service.UpdateAsync(99, updateDto)
            );

            mockRepository.Verify(r => r.GetByIdAsync(99), Times.Once);
            mockRepository.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Seller>()), Times.Never);
        }

        // TC_VENDEDORES_017 – Validar que DeleteAsync del VendedorService elimine vendedor existente correctamente
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

        // TC_VENDEDORES_018 – Validar que DeleteAsync del VendedorService retorne false si el vendedor no existe
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
    }
}