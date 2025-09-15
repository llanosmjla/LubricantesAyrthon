using System.Collections.Generic;
using System.Threading.Tasks;
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Models;

namespace LubricantesAyrthonAPI.Services.Interfaces
{
    public interface ICustomerService : IBaseService<CustomerCreateDto, CustomerReadDto, CustomerUpdateDto>
    {
    }
    /*public interface ICustomerService
    {
        Task<IEnumerable<CustomerReadDto>> GetAllAsync(); // read
        Task<CustomerReadDto> GetByIdAsync(int id); // read by id
        Task<CustomerReadDto> CreateAsync(CustomerCreateDto customer); // create
        Task<bool> UpdateAsync(int id, CustomerUpdateDto customer); // update
        Task<bool> DeleteAsync(int id); // delete
    }*/
}