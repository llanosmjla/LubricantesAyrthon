using Microsoft.AspNetCore.Mvc;
using LubricantesAyrthonAPI.Services.Interfaces;
using LubricantesAyrthonAPI.Dtos;

namespace LubricantesAyrthonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null)
                return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleCreateDto sale)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSale = await _saleService.CreateAsync(sale);
            return CreatedAtAction(nameof(GetById), new { id = createdSale.Id }, createdSale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaleUpdateDto sale)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedSale = await _saleService.UpdateAsync(id, sale);
            if (updatedSale == false)
                return NotFound($"Venta con ID {id} no encontrada.");

            return Ok(updatedSale);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _saleService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}