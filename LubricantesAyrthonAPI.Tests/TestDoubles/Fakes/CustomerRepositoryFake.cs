

using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;

namespace LubricanteAyrthon.Tests.TestDoubles.Fakes
{
    public class CustomerRepositoryFake : IBaseRepository<Customer>
    {
        private readonly List<Customer> _customers = new();
        private int _nextId = 1;

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return Task.FromResult(_customers.AsEnumerable());
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(customer);
        }
        
        public Task<Customer> AddAsync(Customer entity)
        {
            entity.Id = _nextId++;
            _customers.Add(entity);
            return Task.FromResult(entity);
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
            if (customer == null) return Task.FromResult(false);

            _customers.Remove(customer);
            return Task.FromResult(true);
        }

        
    }
}
