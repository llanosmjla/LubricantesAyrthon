using LubricantesAyrthonAPI.Dtos;
using LubricantesAyrthonAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LubricantesAyrthonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sellers = await _sellerService.GetAllAsync();
            return Ok(sellers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seller = await _sellerService.GetByIdAsync(id);
            if (seller == null)
                return NotFound();

            return Ok(seller);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SellerCreateDto seller)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSeller = await _sellerService.CreateAsync(seller);
            return CreatedAtAction(nameof(GetById), new { id = createdSeller.Id }, createdSeller);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SellerUpdateDto seller)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _sellerService.UpdateAsync(id, seller);
            if (!updated)
                return NotFound($"Vendedor con ID {id} no encontrado.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _sellerService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Vendedor con ID {id} no encontrado.");

            return NoContent();
        }
    }
}