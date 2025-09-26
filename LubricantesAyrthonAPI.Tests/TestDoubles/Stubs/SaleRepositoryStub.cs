using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;

namespace LubricantesAyrthonAPI.Tests.TestDoubles.Stubs
{
    public class SaleRepositoryStub : IBaseRepository<Sale>
    {

        public Task<Sale> AddAsync(Sale entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            var sales = new List<Sale>()
            {
                new Sale
                {
                    Id = 1,
                    IdCustomer = 1,
                    IdSeller = 1,
                    TotalPrice = 100,
                    SaleDate = DateTime.Now,
                    SaleDetails = new List<SaleDetail>(){
                        new SaleDetail
                        {
                            Id = 1,
                            IdProduct = 1,
                            Quantity = 1,
                            UnitPrice = 1
                        }
                    }
                }
            };
            return await Task.FromResult(sales);
        }

        public Task<Sale?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Sale?> UpdateAsync(int id, Sale entity)
        {
            throw new NotImplementedException();
        }
    }
}