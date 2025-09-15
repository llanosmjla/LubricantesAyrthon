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
            if (customer == null) throw new KeyNotFoundException("Cliente no encontrado");

            return new CustomerReadDto
            {
                Id = customer.Id,
                Ci = customer.Ci,
                Name = customer.Name,
                Email = customer.Email
            };
        }

        public async Task<CustomerReadDto> CreateAsync(CustomerCreateDto customer)
        {
            var newCustomer = new Customer
            {
                Ci = customer.Ci,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address

            };

            await _customerRepository.AddAsync(newCustomer);

            return new CustomerReadDto
            {
                Id = newCustomer.Id,
                Ci = newCustomer.Ci,
                Name = newCustomer.Name,
                Email = newCustomer.Email,
                Phone = newCustomer.Phone,
                Address = newCustomer.Address
            };
        }

        public async Task<bool> UpdateAsync(int id, CustomerUpdateDto customer)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null) return false;

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;

            await _customerRepository.UpdateAsync(id, existingCustomer);
            return true;
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