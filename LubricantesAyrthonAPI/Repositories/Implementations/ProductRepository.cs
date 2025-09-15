
using LubricantesAyrthonAPI.Configuration;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LubricantesAyrthonAPI.Repositories.Implementations
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<Product?> UpdateAsync(int id, Product entity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Name = entity.Name;
            product.Price = entity.Price;
            product.Description = entity.Description;

            await _context.SaveChangesAsync();
            return product;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}