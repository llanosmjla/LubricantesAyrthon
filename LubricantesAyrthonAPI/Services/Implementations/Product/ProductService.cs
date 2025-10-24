using System.Data;
using LubricantesAyrthonAPI.Configuration;
using LubricantesAyrthonAPI.Exceptions;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LubricantesAyrthonAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _repository;

        public ProductService(IBaseRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            });
        }

        public async Task<ProductReadDto> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Producto con Id {id} no encontrado.");
            }

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
            var allProduct = await _repository.GetAllAsync();
            if (allProduct.Any(p => p.Name.ToLower() == product.Name.ToLower()))
            {
                throw new DuplicateProductNameException($"El producto con nombre: '{product.Name}' ya existe.");
            }


            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };

            var createdProduct = await _repository.AddAsync(newProduct);

            return new ProductReadDto
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Description = createdProduct.Description,
                Price = createdProduct.Price,
                Stock = createdProduct.Stock
            };

        }

        public async Task<ProductReadDto> UpdateAsync(int id, ProductUpdateDto product)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Producto con Id {id} no encontrado.");
            } 

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            var updatedProduct = await _repository.UpdateAsync(id, existingProduct);

            if (updatedProduct == null)
            {
                throw new DataException("Error al actualizar el producto.");
            }
            
            return new ProductReadDto
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                Stock = updatedProduct.Stock
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return false;

            await _repository.DeleteAsync(id);

            return true;
        }

        // Logica de negocio adicional
        public async Task<bool> IsStockAvailable(int productId, int quantity)
        {
            var product = await _repository.GetByIdAsync(productId);
            if (product == null) return false;

            return product.Stock >= quantity;
        }

        public async Task<bool> UpdateStockAfterSaleAsync(int productId, int quantitySold)
        {
            var product = await _repository.GetByIdAsync(productId);
            if (product == null) return false;

            if (product.Stock < quantitySold) return false;

            product.Stock -= quantitySold;
            await _repository.UpdateAsync(productId, product);
            return true;
        }
    }
}