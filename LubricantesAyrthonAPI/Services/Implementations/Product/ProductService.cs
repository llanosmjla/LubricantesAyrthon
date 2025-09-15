using LubricantesAyrthonAPI.Configuration;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Services.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LubricantesAyrthonAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            return await _context.Products
                .Select(p => new ProductReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock
                })
                .ToListAsync();
        }

        public async Task<ProductReadDto> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }
        public async Task<ProductReadDto> CreateAsync(ProductCreateDto product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return new ProductReadDto
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                Stock = newProduct.Stock
            };

        }

        public async Task<bool> UpdateAsync(int id, ProductUpdateDto product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return false;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            return true;
            
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