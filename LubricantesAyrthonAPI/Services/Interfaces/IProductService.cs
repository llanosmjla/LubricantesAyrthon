using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Services.Dtos;

namespace LubricantesAyrthonAPI.Services.Interfaces
{
    public interface IProductService : IBaseService<ProductCreateDto, ProductReadDto, ProductUpdateDto>
    {
    }

    /*public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllAsync();
        Task<ProductReadDto> GetByIdAsync(int id);
        Task<ProductReadDto> CreateAsync(ProductCreateDto product);
        Task<bool> UpdateAsync(int id, ProductUpdateDto product);
        Task<bool> DeleteAsync(int id);
    }*/
}