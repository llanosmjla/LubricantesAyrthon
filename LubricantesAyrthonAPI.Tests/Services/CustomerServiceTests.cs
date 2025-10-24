
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Exceptions;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Implementations;
using LubricantesAyrthonAPI.Tests.TestDoubles.Stubs;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Services
{
    public class CustomerServiceTests
    {
        // TC_CLIENTES_001 - Validar que GetAllAsync del CustomerService retorne todos los clientes desde el stub
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

        // TC_CLIENTES_002 - VAalidar que GetAllAsync del CustomerService retorne lista vacía cuando no hay clientes
        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoCustomersExist()
        {
            // Arrange
            var mockRepo = new Mock<IBaseRepository<Customer>>();

            // Configurar el mock para retornar una lista vacía
            mockRepo
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Customer>());

            var service = new CustomerService(mockRepo.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        // TC_CLIENTES_005 - Validar que GetByIdAsync del CustomerService retorne cliente existente

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenIdIsValid()
        {
            // Arrange
            var mockCustomerRepository = new Mock<IBaseRepository<Customer>>();

            var expectedCustomer = new Customer
            {
                Id = 1,
                Ci = "3040011",
                Name = "Juan Perez",
                Email = "juan.perez@example.com",
                Phone = "12345678",
                Address = "Calle Falsa 123"
            };
            // Configurar mock para retornar el cliente esperado
            mockCustomerRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(expectedCustomer);
            var customerService = new CustomerService(mockCustomerRepository.Object);
            // Act
            var result = await customerService.GetByIdAsync(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCustomer.Id, result.Id);
            Assert.Equal(expectedCustomer.Ci, result.Ci);
            Assert.Equal(expectedCustomer.Name, result.Name);
            Assert.Equal(expectedCustomer.Email, result.Email);
            Assert.Equal(expectedCustomer.Phone, result.Phone);
            Assert.Equal(expectedCustomer.Address, result.Address);
        }

        // TC_CLIENTES_006 - Validar que GetByIdAsync del CustomerService retorne excepción con ID inválido
        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenIdIsInvalid()
        {
            // Arrange
            var mockCustomerRepository = new Mock<IBaseRepository<Customer>>();

            // Configurar mock para retornar null (cliente no encontrado)
            mockCustomerRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Customer)null);

            var customerService = new CustomerService(mockCustomerRepository.Object);

            int invalidCustomerId = 999;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => customerService.GetByIdAsync(invalidCustomerId)
            );

            Assert.Contains($"Cliente con Id {invalidCustomerId} no encontrado.", exception.Message);
        }

        // TC_CLIENTES_009 – Validar que CreateAsync del CustomerService agregue un cliente correctamente
        [Fact]
        public async Task CreateAsync_ShouldAddCustomerSuccessfully()
        {
            // Arrange
            var newCustomerDto = new CustomerCreateDto
            {
                Ci = "3040011",
                Name = "Juan Perez",
                Email = "juan.perez@email.com",
                Phone = "7551234"
            };

            var mockRepository = new Mock<IBaseRepository<Customer>>();
            mockRepository.Setup(r => r.AddAsync(It.IsAny<Customer>()))
                          .ReturnsAsync((Customer customer) =>
                          {
                              customer.Id = 1; // Simula que el repositorio asigna un Id
                              return customer;
                          });

            var customerService = new CustomerService(mockRepository.Object);

            // Act
            var result = await customerService.CreateAsync(newCustomerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(newCustomerDto.Ci, result.Ci);
            Assert.Equal(newCustomerDto.Name, result.Name);
            Assert.Equal(newCustomerDto.Email, result.Email);
            Assert.Equal(newCustomerDto.Phone, result.Phone);

            mockRepository.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        // TC_CLIENTES_010 – Validar que CreateAsync del CustomerService lance excepción si se intenta crear cliente con email duplicado
        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenEmailIsDuplicate()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                Id = 1,
                Ci = "3040011",
                Name = "Juan Perez",
                Email = "juan.perez@email.com",
                Phone = "78762636"
            };

            var mockRepository = new Mock<IBaseRepository<Customer>>();
            mockRepository.Setup(r => r.GetByIdAsync(existingCustomer.Id))
                          .ReturnsAsync(existingCustomer); // Simula que el email ya existe

            var customerService = new CustomerService(mockRepository.Object);

            var newCustomerDto = new CustomerCreateDto
            {
                Ci = "3040022",
                Name = "Pedro Gomez",
                Email = "pedrogomez@gmail.com",
                Phone = "7551234"
            };

            // Act & Assert
            await Assert.ThrowsAsync<DuplicateEmailException>(() => customerService.CreateAsync(newCustomerDto));
        }


        // TC_CLIENTES_013 – Validar que UpdateAsync del CustomerService modifique datos de cliente existente correctamente
        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingCustomerSuccessfully()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                Id = 1,
                Ci = "3040011",
                Name = "Juan Pérez",
                Email = "juan.perez@email.com",
                Phone = "78762636"
            };

            var updateDto = new CustomerUpdateDto
            {
                Ci = "3040011",
                Name = "Juan Carlos Pérez",
                Email = "juan.perez@email.com",
                Phone = "555-5678"
            };

            var mockRepository = new Mock<IBaseRepository<Customer>>();
            mockRepository.Setup(r => r.GetByIdAsync(existingCustomer.Id))
                          .ReturnsAsync(existingCustomer); // Cliente existente

            mockRepository.Setup(r => r.UpdateAsync(existingCustomer.Id, It.IsAny<Customer>()))
                          .ReturnsAsync((int id, Customer customer) => customer); // Retorna el cliente actualizado

            var customerService = new CustomerService(mockRepository.Object);

            // Act
            var result = await customerService.UpdateAsync(existingCustomer.Id, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Email, result.Email);
            Assert.Equal(updateDto.Phone, result.Phone);

            mockRepository.Verify(r => r.UpdateAsync(existingCustomer.Id, It.IsAny<Customer>()), Times.Once);
        }

        // TC_CLIENTES_014 – Validar que UpdateAsync del CustomerService lance CustomerNotFoundException si el cliente no existe
        [Fact]
        public async Task UpdateAsync_ShouldThrowCustomerNotFoundException_WhenCustomerDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Customer>>();
            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                          .ReturnsAsync((Customer)null); // Cliente no existe

            var customerService = new CustomerService(mockRepository.Object);

            var updateDto = new CustomerUpdateDto
            {
                Ci = "3040022",
                Name = "Carlos Gómez",
                Email = "carlos.gomez@email.com",
                Phone = "78762636"
            };

            // Act & Assert
            await Assert.ThrowsAsync<CustomerNotFoundException>(() => customerService.UpdateAsync(1, updateDto));
        }


        // TC_CLIENTES_017 – Validar que DeleteAsync del CustomerService elimine cliente existente correctamente
        [Fact]
        public async Task DeleteAsync_ShouldRemoveExistingCustomerSuccessfully()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                Id = 1,
                Ci = "12345678",
                Name = "Juan Pérez",
                Email = "juan.perez@email.com",
                Phone = "555-1234",
                Address = "Av. Siempre Viva 123"
            };

            var mockRepository = new Mock<IBaseRepository<Customer>>();
            mockRepository.Setup(r => r.GetByIdAsync(existingCustomer.Id))
                          .ReturnsAsync(existingCustomer); // Cliente existente

            mockRepository.Setup(r => r.DeleteAsync(existingCustomer.Id))
                          .ReturnsAsync(true); // Simula eliminación exitosa

            var customerService = new CustomerService(mockRepository.Object);

            // Act
            var result = await customerService.DeleteAsync(existingCustomer.Id);

            // Assert
            Assert.True(result);
            mockRepository.Verify(r => r.DeleteAsync(existingCustomer.Id), Times.Once);
        }

        // TC_CLIENTES_018 – Validar que DeleteAsync del CustomerService lance KeyNotFoundException si el cliente no existe
        [Fact]
        public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenCustomerDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IBaseRepository<Customer>>();
            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                          .ReturnsAsync((Customer)null); // Cliente no encontrado

            var customerService = new CustomerService(mockRepository.Object);

            int nonExistentCustomerId = 999;

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => customerService.DeleteAsync(nonExistentCustomerId));

            // Verificación: no se intenta eliminar nada si el cliente no existe
            mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}