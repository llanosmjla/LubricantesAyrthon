using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;


namespace LubricantesAyrthonAPI.Tests.TestDoubles.Stubs;

public class SellerRepositoryStub : IBaseRepository<Seller>
{
    private readonly List<Seller> _sellers;

    public SellerRepositoryStub()
    {
        _sellers = new List<Seller>
        {
            new Seller
            {
                Id = 1,
                Ci = "10101010",
                Name = "Carlos Pérez",
                Age = 30,
                Email = "carlos.perez@example.com",
                Phone = "777888999",
                Address = "Av. Central 123",
                Salary = 2500
            }
        };
    }

    public Task<Seller> AddAsync(Seller entity)
    {
        entity.Id = _sellers.Max(s => s.Id) + 1;
        _sellers.Add(entity);
        return Task.FromResult(entity);
    }


    public Task<IEnumerable<Seller>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Seller>>(_sellers);
    }

    public Task<Seller> GetByIdAsync(int id)
    {
        var seller = _sellers.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(seller);
    }

    public Task<Seller> UpdateAsync(int id, Seller entity)
    {
        var seller = _sellers.FirstOrDefault(s => s.Id == id);
        if (seller == null) return Task.FromResult<Seller>(null);

        seller.Ci = entity.Ci;
        seller.Name = entity.Name;
        seller.Age = entity.Age;
        seller.Email = entity.Email;
        seller.Phone = entity.Phone;
        seller.Address = entity.Address;
        seller.Salary = entity.Salary;

        return Task.FromResult(seller);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var seller = _sellers.FirstOrDefault(s => s.Id == id);
        if (seller != null)
            _sellers.Remove(seller);
        return Task.FromResult(true);
    }

}
