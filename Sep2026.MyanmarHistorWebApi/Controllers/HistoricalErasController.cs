
using Microsoft.AspNetCore.Mvc;
using Sep2026.MyanmarHistory.App.Dtos;
using Sep2026.MyanmarHistory.App.Interfaces;

namespace Sep2026.MyanmarHistorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalErasController : ControllerBase
    {
        private readonly IHistoricalEraService _service;

        public HistoricalErasController(IHistoricalEraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<HistoricalEraDto>>> GetAll()
        {
            var eras = await _service.GetAllAsync();
            return Ok(eras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricalEraDto>> GetById(int id)
        {
            var era = await _service.GetByIdAsync(id);

            if (era == null)
            {
                return NotFound();
            }

            return Ok(era);
        }

        [HttpPost]
        public async Task<ActionResult<HistoricalEraDto>> Create(HistoricalEraDto eraDto)
        {
            var createdEra = await _service.CreateAsync(eraDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdEra.EraId },
                createdEra
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HistoricalEraDto eraDto)
        {
            var result = await _service.UpdateAsync(id, eraDto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
