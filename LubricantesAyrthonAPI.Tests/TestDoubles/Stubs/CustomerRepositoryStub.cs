using System.Runtime.CompilerServices;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace LubricantesAyrthonAPI.Tests.TestDoubles.Stubs
{

    public class CustomerRepositoryStub : IBaseRepository<Customer>
    {
        private readonly List<Customer> _customers = new();

        public CustomerRepositoryStub()
        {
            _customers.Add(
                new Customer
                {
                    Id = 1,
                    Ci = "12345678",
                    Name = "Eva Mesa",
                    Email = "bZ2Og@example.com",
                    Phone = "1234567890",
                    Address = "123 Main St"
                }
            );

            _customers.Add(
                new Customer
                {
                    Id = 2,
                    Ci = "87654321",
                    Name = "Roso Perez",
                    Email = "qyH2j@example.com",
                    Phone = "9876543210",
                    Address = "456 Elm St"
                }
            );
            _customers.Add(
                new Customer
                {
                    Id = 3,
                    Ci = "87654321",
                    Name = "Sara Mendez",
                    Email = "qyH2j@example.com",
                    Phone = "9876543210",
                    Address = "456 Elm St"
                }
            );
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await Task.FromResult(_customers);
        }

        // Métodos no usados en este caso de prueba
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await Task.FromResult(_customers.FirstOrDefault(c => c.Id == id));
        }
        public Task<Customer> AddAsync(Customer customer)
        {
            customer.Id = _customers.Count + 1;
            _customers.Add(customer);
            return Task.FromResult(customer);
        }
        public Task<Customer?> UpdateAsync(int id, Customer customer)
        {
            var customerFound = _customers.FirstOrDefault(c => c.Id == id);
            if (customerFound == null) return Task.FromResult<Customer?>(null);

            customerFound.Ci = customer.Ci;
            customerFound.Name = customer.Name;
            customerFound.Email = customer.Email;
            customerFound.Phone = customer.Phone;
            customerFound.Address = customer.Address;

            return Task.FromResult<Customer?>(customerFound);
        }
        public Task<bool> DeleteAsync(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return Task.FromResult(false);

            _customers.Remove(customer);
            return Task.FromResult(true);
        }
    }

}