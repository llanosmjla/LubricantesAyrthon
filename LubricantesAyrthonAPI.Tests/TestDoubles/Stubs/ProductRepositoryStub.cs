

using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;

namespace LubricantesAyrthonAPI.Tests.TestDoubles.Stubs
{
    public class ProductRepositoryStub : IBaseRepository<Product>
    {
        public ProductRepositoryStub()
        {
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 10,
                    Stock = 100
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 20,
                    Stock = 200
                }
            };
            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            var product = new Product
            {
                Id = id,
                Name = $"Product {id}",
                Price = id * 10,
                Stock = id * 100
            };
            return Task.FromResult<Product?>(product);
        }

        public Task<Product> AddAsync(Product entity)
        {
            entity.Id = 3; // Simulate auto-generated ID
            return Task.FromResult(entity);
        }

        public Task<Product?> UpdateAsync(int id, Product entity)
        {
            entity.Id = id;
            return Task.FromResult<Product?>(entity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(true);
        }
    }
}