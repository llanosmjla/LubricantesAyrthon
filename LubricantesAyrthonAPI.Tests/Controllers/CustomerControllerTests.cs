using System.ComponentModel.DataAnnotations;


using LubricantesAyrthonAPI.Controllers;
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LubricantesAyrthonAPI.Tests.Controllers
{

    public class CustomerControllerTests
    {

         //TC_CLIENTES_003 - Obtener todos los clientes
        [Fact]
        public async Task GetAll_ReturnsListOfCustomers()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();

            var customers = new List<CustomerReadDto>
            {
                new CustomerReadDto { Id = 1, Ci = "3040011", Name = "Juan Pérez" },
                new CustomerReadDto { Id = 2, Ci = "3040022", Name = "Ana Torres" }
            };

            mockService.Setup(s => s.GetAllAsync())
                       .ReturnsAsync(customers);

            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<CustomerReadDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Equal("Juan Pérez", returnValue[0].Name);
            Assert.Equal("Ana Torres", returnValue[1].Name);

            mockService.Verify(s => s.GetAllAsync(), Times.Once);
        }

        //TC_011 - Obtener todos los clientes
        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoCustomersExist()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();

            mockService.Setup(s => s.GetAllAsync())
                       .ReturnsAsync(new List<CustomerReadDto>()); // Lista vacía

            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<CustomerReadDto>>(okResult.Value);
            Assert.Empty(returnValue); // lista vacía

            mockService.Verify(s => s.GetAllAsync(), Times.Once);
        }

         //TC_012 - Obtener un cliente por ID
        [Fact]
        public async Task GetById_ReturnsCustomer_WhenExists()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var expectedCustomer = new CustomerReadDto
            {
                Id = 1,
                Ci = "3040011",
                Name = "Juan Pérez"
            };

            mockService.Setup(s => s.GetByIdAsync(1))
                       .ReturnsAsync(expectedCustomer);

            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CustomerReadDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Juan Pérez", returnValue.Name);

            mockService.Verify(s => s.GetByIdAsync(1), Times.Once);
        }

        // TC_013: Validar un cliente inexistente, el controlador debe devolver un NotFound
        [Fact]
        public async Task GetById_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            int nonExistentId = 999;

            mockService.Setup(s => s.GetByIdAsync(nonExistentId))
                       .ReturnsAsync((CustomerReadDto)null);

            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.GetById(nonExistentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            mockService.Verify(s => s.GetByIdAsync(nonExistentId), Times.Once);
        }

        // TC_002 - Rechazar registro de cliente con datos obligatorios faltantes

        [Fact]
        public async Task Create_RegisterCustomer_MissingRequiredData_ReturnsBadRequest()
        {
            // Arrange
            var serviceMock = new Mock<ICustomerService>();

            var controller = new CustomerController(serviceMock.Object);

            // Simular un DTO
            var customerDto = new CustomerCreateDto
            {
                Ci = "3040011",
                Name = ""
            };

            // Simular la validacion del modelo
            var validationContext = new ValidationContext(customerDto); // Aqui va el objeto a validar
            var validationResults = new List<ValidationResult>(); // Aqui se guardan los resultados de la validacion
            Validator.TryValidateObject(customerDto, validationContext, validationResults, true); // El true indica que se validen todas las propiedades

            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    // Agregar el error de validacion al ModelState del controlador
                    controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage!);
                }
            }

            // Act
            var result = await controller.Create(customerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            // Verificar que el servicio no fue llamado
            serviceMock.Verify(service => service.CreateAsync(It.IsAny<CustomerCreateDto>()), Times.Never);
        }

        // TC_014: Validar la creacion de un cliente
        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenCustomerIsCreatedSuccessfully()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();

            var customerCreateDto = new CustomerCreateDto
            {
                Ci = "12345678",
                Name = "Pedro Gómez",
                Email = "pedro@example.com"
            };

            var createdCustomer = new CustomerReadDto
            {
                Id = 10,
                Ci = "12345678",
                Name = "Pedro Gómez",
                Email = "pedro@example.com"
            };

            mockService.Setup(s => s.CreateAsync(customerCreateDto))
                       .ReturnsAsync(createdCustomer);

            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.Create(customerCreateDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<CustomerReadDto>(createdResult.Value);

            Assert.Equal(10, returnValue.Id);
            Assert.Equal("Pedro Gómez", returnValue.Name);
            Assert.Equal("12345678", returnValue.Ci);
            Assert.Equal("pedro@example.com", returnValue.Email);

            Assert.Equal(nameof(CustomerController.GetById), createdResult.ActionName);

            mockService.Verify(s => s.CreateAsync(customerCreateDto), Times.Once);
        }

        //TC_CLIENTES_015 – Validar que Update del CustomerController retorne Ok al modificar cliente existente
        [Fact]
        public async Task Update_ValidData_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var controller = new CustomerController(mockService.Object);

            var customerUpdateDto = new CustomerUpdateDto
            {
                Ci = "12345678",
                Name = "José Luis Llanos",
                Email = "joseluis.llanos@email.com",
                Phone = "+591 71234567",
                Address = "Av. Bolívar 123"
            };

            var updatedCustomer = new CustomerReadDto
            {
                Id = 1,
                Ci = customerUpdateDto.Ci,
                Name = customerUpdateDto.Name,
                Email = customerUpdateDto.Email,
                Phone = customerUpdateDto.Phone,
                Address = customerUpdateDto.Address
            };

            mockService
                .Setup(s => s.UpdateAsync(1, customerUpdateDto))
                .ReturnsAsync(updatedCustomer);

            // Act
            var result = await controller.Update(1, customerUpdateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var returnedCustomer = Assert.IsType<CustomerReadDto>(okResult.Value);
            Assert.Equal(customerUpdateDto.Ci, returnedCustomer.Ci);
            Assert.Equal(customerUpdateDto.Name, returnedCustomer.Name);
            Assert.Equal(customerUpdateDto.Email, returnedCustomer.Email);

            mockService.Verify(s => s.UpdateAsync(1, customerUpdateDto), Times.Once);
        }

        // TC_CLIENTES_016 – Validar que Update del CustomerController retorne BadRequest si los datos son inválidos
        [Fact]
        public async Task Update_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();

            var controller = new CustomerController(mockService.Object);

            var customerUpdateDto = new CustomerUpdateDto
            {
                Ci = "", // Dato inválido
                Name = "Nuevo Nombre"
            };

            // Simular la validacion del modelo
            var validationContext = new ValidationContext(customerUpdateDto);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(customerUpdateDto, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage!);
                }
            }

            // Act
            var result = await controller.Update(1, customerUpdateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);

            mockService.Verify(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CustomerUpdateDto>()), Times.Never);
        }

        // TC_CLIENTES_019 – Validar que Delete del CustomerController retorne NoContent al eliminar cliente existente
        [Fact]
        public async Task Delete_ExistingCustomer_ReturnsNoContent()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var controller = new CustomerController(mockService.Object);

            int existingId = 1;

            mockService
                .Setup(s => s.DeleteAsync(existingId))
                .ReturnsAsync(true);

            // Act
            var result = await controller.Delete(existingId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);

            mockService.Verify(s => s.DeleteAsync(existingId), Times.Once);
        }
        
        // TC_CLIENTES_020 – Validar que Delete del CustomerController retorne NotFound si el cliente no existe
        [Fact]
        public async Task Delete_NonExistingCustomer_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var controller = new CustomerController(mockService.Object);

            int nonExistingId = 999;

            mockService
                .Setup(s => s.DeleteAsync(nonExistingId))
                .ReturnsAsync(false);

            // Act
            var result = await controller.Delete(nonExistingId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);

            mockService.Verify(s => s.DeleteAsync(nonExistingId), Times.Once);
        }
    }
}