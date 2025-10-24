using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Models;
using LubricantesAyrthonAPI.Repositories.Implementations;
using LubricantesAyrthonAPI.Repositories.Interfaces;
using LubricantesAyrthonAPI.Services.Interfaces;

namespace LubricantesAyrthonAPI.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly IBaseRepository<Sale> _saleRepository;
        private readonly IProductService _productService;


        public SaleService(IBaseRepository<Sale> saleRepository, IProductService productService)
        {
            _saleRepository = saleRepository;
            _productService = productService;
        }

        public async Task<IEnumerable<SaleReadDto>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync() ?? Enumerable.Empty<Sale>();
            return sales.Select(s => new SaleReadDto
            {
                Id = s.Id,
                IdCustomer = s.IdCustomer,
                IdSeller = s.IdSeller,
                TotalPrice = s.TotalPrice,
                SaleDate = s.SaleDate,
                SaleDetails = s.SaleDetails.Select(sd => new SaleDetailReadDto
                {
                    Id = sd.Id,
                    IdProduct = sd.IdProduct,
                    Quantity = sd.Quantity,
                    UnitPrice = sd.UnitPrice
                }).ToList()
            });
        }

        public async Task<SaleReadDto> GetByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null) return null;

            return new SaleReadDto
            {
                Id = sale.Id,
                IdCustomer = sale.IdCustomer,
                IdSeller = sale.IdSeller,
                TotalPrice = sale.TotalPrice,
                SaleDate = sale.SaleDate,
                SaleDetails = sale.SaleDetails.Select(sd => new SaleDetailReadDto
                {
                    Id = sd.Id,
                    IdProduct = sd.IdProduct,
                    Quantity = sd.Quantity,
                    UnitPrice = sd.UnitPrice
                }).ToList()
            };
        }

        public async Task<SaleReadDto> CreateAsync(SaleCreateDto entity)
        {
            foreach (var detail in entity.SaleDetails)
            {
                if (!await _productService.IsStockAvailable(detail.IdProduct, detail.Quantity))
                {
                    return null;
                }
            }

            var sale = new Sale
            {
                IdCustomer = entity.IdCustomer,
                IdSeller = entity.IdSeller,
                TotalPrice = CalculateTotalPrice(entity.SaleDetails!),
                SaleDate = entity.SaleDate,
                SaleDetails = entity.SaleDetails.Select(sd => new SaleDetail
                {
                    IdProduct = sd.IdProduct,
                    Quantity = sd.Quantity,
                    UnitPrice = sd.UnitPrice
                }).ToList()
            };

            var saleCreated = await _saleRepository.AddAsync(sale);

            // Actualizar el stock de los productos vendidos
            foreach (var detail in entity.SaleDetails)
            {
                await _productService.UpdateStockAfterSaleAsync(detail.IdProduct, detail.Quantity);
            }

            return new SaleReadDto
            {
                Id = saleCreated.Id,
                IdCustomer = saleCreated.IdCustomer,
                IdSeller = saleCreated.IdSeller,
                TotalPrice = saleCreated.TotalPrice,
                SaleDate = saleCreated.SaleDate,
                SaleDetails = saleCreated.SaleDetails.Select(sd => new SaleDetailReadDto
                {
                    Id = sd.Id,
                    IdProduct = sd.IdProduct,
                    Quantity = sd.Quantity,
                    UnitPrice = sd.UnitPrice
                }).ToList()
            };
        }

        public async Task<SaleReadDto> UpdateAsync(int id, SaleUpdateDto entity)
        {
            foreach (var detail in entity.SaleDetails)
            {
                if (! await _productService.IsStockAvailable(detail.IdProduct, detail.Quantity))
                {
                    return null;
                }
            }

            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null) return null;

            sale.IdCustomer = entity.IdCustomer;
            sale.IdSeller = entity.IdSeller;
            sale.TotalPrice = entity.TotalPrice;
            sale.SaleDate = entity.SaleDate;
            sale.SaleDetails = entity.SaleDetails.Select(sd => new SaleDetail
            {
                IdProduct = sd.IdProduct,
                Quantity = sd.Quantity,
                UnitPrice = sd.UnitPrice
            }).ToList();

            var saleUpdated = await _saleRepository.UpdateAsync(id, sale);

            if (saleUpdated == null) return null;

            // Actualizar el stock de los productos vendidos
            foreach (var detail in entity.SaleDetails)
            {
                await _productService.UpdateStockAfterSaleAsync(detail.IdProduct, detail.Quantity);
            }

            return new SaleReadDto
            {
                Id = id,
                IdCustomer = saleUpdated.IdCustomer,
                IdSeller = saleUpdated.IdSeller,
                TotalPrice = saleUpdated.TotalPrice,
                SaleDate = saleUpdated.SaleDate,
                SaleDetails = saleUpdated.SaleDetails.Select(sd => new SaleDetailReadDto
                {
                    Id = sd.Id,
                    IdProduct = sd.IdProduct,
                    Quantity = sd.Quantity,
                    UnitPrice = sd.UnitPrice
                }).ToList()
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null) return false;

            await _saleRepository.DeleteAsync(id);
            return true;
        }

        // Logica de negocio adicional
        public decimal CalculateTotalPrice(IEnumerable<SaleDetailCreateDto> saleDetails)
        {
            return saleDetails.Sum(sd => sd.Quantity * sd.UnitPrice);
        }

        
    }
}