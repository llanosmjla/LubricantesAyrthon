using LubricantesAyrthonAPI.Configuration;
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Implementations;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LubricantesAyrthonAPI.Services.Implementations
{


    public class CustomerService : ICustomerService
    {
        // private readonly AppDbContext _customerRepository;
        private readonly IBaseRepository<Customer> _customerRepository;

        public CustomerService(IBaseRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task<IEnumerable<CustomerReadDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerReadDto
            {
                Id = c.Id,
                Ci = c.Ci,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address
            });
        }

        public async Task<CustomerReadDto> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) throw new KeyNotFoundException($"Cliente con {id} no encontrado");

            return new CustomerReadDto
            {
                Id = customer.Id,
                Ci = customer.Ci,
                Name = customer.Name,
                Email = customer.Email
            };
        }

        public async Task<CustomerReadDto> CreateAsync(CustomerCreateDto customerCreateDto)
        {
            var newCustomer = new Customer
            {
                Ci = customerCreateDto.Ci,
                Name = customerCreateDto.Name,
                Email = customerCreateDto.Email,
                Phone = customerCreateDto.Phone,
                Address = customerCreateDto.Address

            };

            var createdCustomer = await _customerRepository.AddAsync(newCustomer);
            /*
            return new CustomerReadDto
            {
                Id = newCustomer.Id,
                Ci = newCustomer.Ci,
                Name = newCustomer.Name,
                Email = newCustomer.Email,
                Phone = newCustomer.Phone,
                Address = newCustomer.Address
            };
            */

            return new CustomerReadDto
            {
                Id = createdCustomer.Id,
                Ci = createdCustomer.Ci,
                Name = createdCustomer.Name,
                Email = createdCustomer.Email,
                Phone = createdCustomer.Phone,
                Address = createdCustomer.Address
            };
        }

        public async Task<CustomerReadDto> UpdateAsync(int id, CustomerUpdateDto customer)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null) return null;

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;

            var customerUpdated = await _customerRepository.UpdateAsync(id, existingCustomer);

            if (customerUpdated == null) return null;

            return new CustomerReadDto
            {
                Id = customerUpdated.Id,
                Ci = customerUpdated.Ci,
                Name = customerUpdated.Name,
                Email = customerUpdated.Email,
                Phone = customerUpdated.Phone,
                Address = customerUpdated.Address
            };
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return false;

            await _customerRepository.DeleteAsync(id);
            return true;
        }
    }
}