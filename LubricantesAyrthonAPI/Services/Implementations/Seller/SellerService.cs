
using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Interfaces;

namespace LubricantesAyrthonAPI.Services.Implementations
{
    public class SellerService : ISellerService
    {
        
        private readonly IBaseRepository<Seller> _sellerRepository;

        public SellerService(IBaseRepository<Seller> sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        public async Task<IEnumerable<SellerReadDto>> GetAllAsync()
        {
            var sellers = await _sellerRepository.GetAllAsync();
            return sellers.Select(s => new SellerReadDto
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                Salary = s.Salary
            });
        }

        public async Task<SellerReadDto> GetByIdAsync(int id)
        {
            var seller = await _sellerRepository.GetByIdAsync(id);
            if (seller == null) return null;

            return new SellerReadDto
            {
                Id = seller.Id,
                Name = seller.Name,
                Age = seller.Age,
                Email = seller.Email,
                Phone = seller.Phone,
                Address = seller.Address,
                Salary = seller.Salary
            };
        }

        public async Task<SellerReadDto> CreateAsync(SellerCreateDto entity)
        {
            var seller = new Seller
            {
                Ci = entity.Ci,
                Name = entity.Name,
                Age = entity.Age,
                Email = entity.Email,
                Phone = entity.Phone,
                Address = entity.Address,
                Salary = entity.Salary
            };
            // Aquí deberías llamar al repositorio para guardar el vendedor en la base de datos
            await _sellerRepository.AddAsync(seller);

            return new SellerReadDto
            {
                Id = seller.Id,
                Name = seller.Name,
                Age = seller.Age,
                Email = seller.Email,
                Phone = seller.Phone,
                Address = seller.Address,
                Salary = seller.Salary
            };


        }

        public async Task<SellerReadDto> UpdateAsync(int id, SellerUpdateDto entity)
        {
            var Seller = await _sellerRepository.GetByIdAsync(id);
            if (Seller == null) return null;

            Seller.Name = entity.Name;
            Seller.Age = entity.Age;
            Seller.Email = entity.Email;
            Seller.Phone = entity.Phone;
            Seller.Address = entity.Address;
            Seller.Salary = entity.Salary;

            var updatedSeller = await _sellerRepository.UpdateAsync(id, Seller);
            return new SellerReadDto
            {
                Id = updatedSeller.Id,
                Name = updatedSeller.Name,
                Age = updatedSeller.Age,
                Email = updatedSeller.Email,
                Phone = updatedSeller.Phone,
                Address = updatedSeller.Address,
                Salary = updatedSeller.Salary
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Seller = await _sellerRepository.GetByIdAsync(id);
            if (Seller == null) return false;

            await _sellerRepository.DeleteAsync(id);
            return true;
        }

        

        
    }
}