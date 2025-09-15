
using LubricantesAyrthonAPI.Configuration;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace LubricantesAyrthonAPI.Repositories.Implementations
{
    public class SellerRepository : IBaseRepository<Seller>
    {
        private readonly AppDbContext _context;

        public SellerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seller>> GetAllAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task<Seller?> GetByIdAsync(int id)
        {
            return await _context.Sellers.FindAsync(id);
        }

        public async  Task<Seller> AddAsync(Seller entity)
        {
            await _context.Sellers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Seller?> UpdateAsync(int id, Seller entity)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null) return null;

            seller.Name = entity.Name;
            seller.Email = entity.Email;
            seller.Phone = entity.Phone;

            await _context.SaveChangesAsync();
            return seller;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null) return false;

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}