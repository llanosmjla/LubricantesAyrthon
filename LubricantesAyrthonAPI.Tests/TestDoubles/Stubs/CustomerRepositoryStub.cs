using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace LubricantesAyrthonAPI.Tests.TestDoubles.Stubs
{

    public class CustomerRepositoryStub : IBaseRepository<Customer>
    {
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var customers = new List<Customer>
            {
                new Customer { Ci = "1", Name = "Eva Mesa" },
                new Customer { Ci = "2", Name = "Roso Perez" },
                new Customer { Ci = "3", Name = "Sara Mendez" }
            };

            return await Task.FromResult(customers);
        }

        // Métodos no usados en este caso de prueba
        public async Task<Customer> GetByIdAsync(int id) => throw new System.NotImplementedException();
        public Task<Customer> AddAsync(Customer customer) => throw new System.NotImplementedException();
        public Task<Customer?> UpdateAsync(int id, Customer customer) => throw new System.NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new System.NotImplementedException();
    }

}