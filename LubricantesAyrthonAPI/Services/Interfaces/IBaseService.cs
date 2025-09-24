namespace LubricantesAyrthonAPI.Services.Interfaces
{
    public interface IBaseService<TCreate, TRead, TUpdate>
    {
        Task<IEnumerable<TRead>> GetAllAsync(); // read
        Task<TRead> GetByIdAsync(int id); // read by id
        Task<TRead> CreateAsync(TCreate entity); // create
        Task<TRead> UpdateAsync(int id, TUpdate entity); // update
        Task<bool> DeleteAsync(int id); // delete
    }
}