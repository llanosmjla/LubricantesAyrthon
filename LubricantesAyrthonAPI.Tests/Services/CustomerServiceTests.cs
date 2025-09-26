
using System.Data;
using LubricanteAyrthon.Tests.TestDoubles.Fakes;
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Implementations;
using LubricantesAyrthonAPI.Tests.TestDoubles.Stubs;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Services
{
    public class CustomerServiceTests
    {
        // TC_001: Registrar un cliente con datos válidos
        [Fact]
        public async Task RegisterCustomer_ValidData_ReturnsSuccess()
        {
            // Arrange
            var customerDto = new CustomerCreateDto
            {
                Ci = "3040011",
                Name = "Mariano Messi",
            };

            var expectedCustomer = new Customer
            {
                Id = 1,
                Ci = customerDto.Ci,
                Name = customerDto.Name,
            };

            var repositoryMock = new Mock<IBaseRepository<Customer>>();

            repositoryMock
                .Setup(repository => repository.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(expectedCustomer);


            var service = new CustomerService(repositoryMock.Object);

            // Act
            var result = await service.CreateAsync(customerDto);

            // Assert
            Assert.Equal(expectedCustomer.Id, result.Id);
            Assert.Equal(expectedCustomer.Ci, result.Ci);
            Assert.Equal(expectedCustomer.Name, result.Name);

            // Verificar que el método AddAsync fue llamado una vez
            repositoryMock.Verify(repository => repository.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        // TC_003: Consultar un cliente existente por ID
        [Fact]
        public async Task GetCustomerById_ExistingId_ReturnsCustomer()
        {
            // Arrange
            var repositoryFake = new CustomerRepositoryFake();
            var service = new CustomerService(repositoryFake);

            var newCustomer = new Customer
            {
                Ci = "9876543",
                Name = "Jose Luis Garcia"
            };

            var createdCustomer = await repositoryFake.AddAsync(newCustomer);

            // Act
            var result = await service.GetByIdAsync(createdCustomer.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdCustomer.Id, result.Id);
            Assert.Equal(createdCustomer.Ci, result.Ci);
            Assert.Equal(createdCustomer.Name, result.Name);
        }

        // TC_004: Consultar Cliente inexistente
        [Fact]
        public async Task GetCustomerById_NonExistentCustomer_ThrowsException()
        {
            // Arrange
            var repositoryFake = new CustomerRepositoryFake();
            var service = new CustomerService(repositoryFake);

            var nonExistentId = 999;

            // Act
            Task act() => service.GetByIdAsync(nonExistentId);

            // Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(act);
            Assert.Equal($"Cliente con {nonExistentId} no encontrado", exception.Message);
        }

        // TC_005: Validar obtención de todos los clientes
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCustomers_FromStub()
        {
            // Arrange
            var stubRepo = new CustomerRepositoryStub();
            var service = new CustomerService(stubRepo);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());

            Assert.Collection(result,
                c => Assert.Equal("Eva Mesa", c.Name),
                c => Assert.Equal("Roso Perez", c.Name),
                c => Assert.Equal("Sara Mendez", c.Name)
            );
        }

        //TC_007: Validar actualización de cliente existente
        [Fact]
        public async Task UpdateAsync_ClienteExistente_RetornaClienteActualizado()
        {
            // Arrange
            var repository = new CustomerRepositoryFake();
            var service = new CustomerService(repository);

            var newCustomer = new Customer
            {
                Ci = "3040011",
                Name = "Juan Miranda",
                Email = "juan.miranda@example.com",
                Phone = "44456789",
                Address = "Calle Bolivar 123"
            };

            await repository.AddAsync(newCustomer);

            var updateDto = new CustomerUpdateDto
            {
                Ci = "3040011",
                Name = "Juan Miranda",
                Email = "juan.miranda@example.com",
                Phone = "44456789",
                Address = "Calle Bolivar 123"
            };

            // Act
            var result = await service.UpdateAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Ci, result.Ci);
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Email, result.Email);
        }

        //TC_008: Validar actualización de cliente inexistente
        [Fact]
        public async Task UpdateAsync_ClienteInexistente_RetornaNull()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Customer>>();

            // Configuramos el mock para que GetByIdAsync devuelva null para cualquier ID
            mockRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Customer?)null);

            var service = new CustomerService(mockRepository.Object);

            var updateDto = new CustomerUpdateDto
            {
                Ci = "3040011",
                Name = "Pedro Gómez",
                Email = "pedro.gomez@example.com",
                Phone = "987654321",
                Address = "Avenida Siempre Viva 742"
            };

            // Act
            var result = await service.UpdateAsync(99, updateDto); // ID inexistente

            // Assert
            Assert.Null(result);
            mockRepository.Verify(repo => repo.GetByIdAsync(99), Times.Once);
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Customer>()), Times.Never);
        }

        //TC_009: Validar actualización de cliente con repositorio fallante

        [Fact]
        public async Task UpdateAsync_ErrorEnRepositorio_RetornaNull()
        {
            // Arrange

            var existingCustomer = new Customer
            {
                Id = 1,
                Ci = "3040011",
                Name = "Juan Pérez",
                Email = "juan.perez@example.com",
                Phone = "111222333",
                Address = "Calle Falsa 123"
            };

            var updateDto = new CustomerUpdateDto
            {
                Ci = "3040011",
                Name = "Juan Pérez Modificado",
                Email = "juan.perez@example.com",
                Phone = "111222333",
                Address = "Calle Falsa 123"
            };

            var mockRepository = new Mock<IBaseRepository<Customer>>();
            // Configurar mock para GetByIdAsync (cliente existente)
            mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(existingCustomer);

            // Configurar mock para UpdateAsync (simula fallo, retorna null)
            mockRepository
                .Setup(r => r.UpdateAsync(1, It.IsAny<Customer>()))
                .ReturnsAsync((Customer?)null);

            var service = new CustomerService(mockRepository.Object);

            // Act
            var result = await service.UpdateAsync(1, updateDto);

            // Assert
            Assert.Null(result);
            mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
            mockRepository.Verify(r => r.UpdateAsync(1, It.IsAny<Customer>()), Times.Once);

        }

        //TC_010: Validar eliminación de cliente existente
        [Fact]
        public async Task DeleteAsync_ClienteExistente_RetornaTrue()
        {
            // Arrange
            var repository = new CustomerRepositoryStub();
            var service = new CustomerService(repository);

            // Act
            var result = await service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ClienteNoExistente_RetornaFalse()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Customer>>();
            var service = new CustomerService(mockRepository.Object);

            // Configurar GetByIdAsync para devolver null (no existe cliente)
            mockRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Customer)null);

            // Act
            var result = await service.DeleteAsync(99);

            // Assert
            Assert.False(result);

            // Verificar que GetByIdAsync fue llamado con el ID correcto
            mockRepository.Verify(r => r.GetByIdAsync(99), Times.Once);

            // Verificar que DeleteAsync nunca fue llamado
            mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }




    }
}
