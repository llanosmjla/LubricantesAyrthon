
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Models;

namespace LubricantesAyrthonAPI.Services.Interfaces
{
    public interface ISaleService : IBaseService<SaleCreateDto, SaleReadDto, SaleUpdateDto>
    {
    }


    /*public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<Sale> GetByIdAsync(int id);
        Task<Sale> CreateAsync(Sale sale);
        Task<bool> UpdateAsync(int id, Sale sale);
        Task<bool> DeleteAsync(int id);
    }*/
}