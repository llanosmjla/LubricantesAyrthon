

using LubricantesAyrthonAPI.Configuration;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LubricantesAyrthonAPI.Repositories.Implementations
{
    public class CustomerRepository : IBaseRepository<Customer>
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            var newCustomer = new Customer
            {
                Ci = entity.Ci,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                Address = entity.Address
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;

        }

        public async Task<Customer?> UpdateAsync(int id, Customer entity)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            customer.Ci = entity.Ci;
            customer.Name = entity.Name;
            customer.Email = entity.Email;
            customer.Phone = entity.Phone;
            customer.Address = entity.Address;

            await _context.SaveChangesAsync();
            return customer;
        }
        
         public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}