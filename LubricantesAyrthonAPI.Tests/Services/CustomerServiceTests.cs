
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
    }
}
