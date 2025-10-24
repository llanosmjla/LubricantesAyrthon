using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;

namespace LubricantesAyrthonAPI.Tests.TestDoubles.Stubs
{
    public class SaleRepositoryStub : IBaseRepository<Sale>
    {
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
                        },
                        new SaleDetail
                        {
                            Id = 2,
                            IdProduct = 2,
                            Quantity = 2,
                            UnitPrice = 2
                        }
                    }
                }
            };
            return await Task.FromResult(sales);
        }

        public Task<Sale> AddAsync(Sale entity)
        {
            return Task.FromResult(entity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(false);
        }

        public Task<Sale> GetByIdAsync(int id)
        {
            if (id == 1)
            {
                return Task.FromResult(new Sale
                {
                    Id = 1,
                    IdCustomer = 101,
                    IdSeller = 201,
                    TotalPrice = 150.50m,
                    SaleDate = DateTime.Now,
                    SaleDetails = new List<SaleDetail>
                    {
                        new SaleDetail
                        {
                            Id = 1,
                            IdProduct = 301,
                            Quantity = 2,
                            UnitPrice = 50.25m
                        },
                        new SaleDetail
                        {
                            Id = 2,
                            IdProduct = 302,
                            Quantity = 1,
                            UnitPrice = 50.00m
                        }
                    }
                });
            }
            return Task.FromResult<Sale>(null);
        }

        public Task<Sale?> UpdateAsync(int id, Sale entity)
        {
            throw new NotImplementedException();
        }
    }
}