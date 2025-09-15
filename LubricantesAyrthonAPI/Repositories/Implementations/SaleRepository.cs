
using LubricantesAyrthonAPI.Configuration;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LubricantesAyrthonAPI.Repositories.Implementations
{

    public class SaleRepository : IBaseRepository<Sale>
    {
        private readonly AppDbContext _context;

            public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _context.Sales.FindAsync(id);
        }

        public async Task<Sale> AddAsync(Sale entity)
        {
            await _context.Sales.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }



        public async Task<Sale?> UpdateAsync(int id, Sale entity)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return null;

            sale.IdCustomer = entity.IdCustomer;
            sale.IdSeller = entity.IdSeller;
            sale.TotalPrice = entity.TotalPrice;
            sale.SaleDate = entity.SaleDate;
            sale.SaleDetails = entity.SaleDetails;

            await _context.SaveChangesAsync();
            return sale;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }

        
        
    }
}   