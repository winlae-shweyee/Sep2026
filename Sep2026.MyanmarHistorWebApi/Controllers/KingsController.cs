
using Microsoft.AspNetCore.Mvc;
using Sep2026.MyanmarHistory.App.Dtos;
using Sep2026.MyanmarHistory.App.Interfaces;

namespace Sep2026.MyanmarHistorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KingsController : ControllerBase
    {
        private readonly IKingService _service;

        public KingsController(IKingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<KingDto>>> GetAll()
        {
            var kings = await _service.GetAllAsync();
            return Ok(kings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KingDto>> GetById(int id)
        {
            var king = await _service.GetByIdAsync(id);

            if (king == null)
            {
                return NotFound();
            }

            return Ok(king);
        }

        [HttpPost]
        public async Task<ActionResult<KingDto>> Create(KingDto kingDto)
        {
            var createdKing = await _service.CreateAsync(kingDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdKing.KingId },
                createdKing
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, KingDto kingDto)
        {
            var result = await _service.UpdateAsync(id, kingDto);

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
