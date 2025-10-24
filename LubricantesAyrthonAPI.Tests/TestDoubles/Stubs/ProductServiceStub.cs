
using LubricantesAyrthonAPI.Services.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;

namespace LubricantesAyrthonAPI.Tests.TestDoubles.Stubs
{
    public class ProductServiceStub : IProductService
    {
        private readonly ProductRepositoryStub _productRepositoryStub;

        public ProductServiceStub()
        {
            _productRepositoryStub = new ProductRepositoryStub();
        }

        public Task<ProductReadDto> CreateAsync(ProductCreateDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductReadDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductReadDto> UpdateAsync(int id, ProductUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsStockAvailable(int productId, int quantity)
        {
            return false;
        }

        public async Task<bool> UpdateStockAfterSaleAsync(int productId, int quantitySold)
        {
            var product = await _productRepositoryStub.GetByIdAsync(productId);
            if (product == null) return false;

            product.Stock -= quantitySold;
            var updatedProduct = await _productRepositoryStub.UpdateAsync(productId, product);
            return updatedProduct != null;
        }
    }
}