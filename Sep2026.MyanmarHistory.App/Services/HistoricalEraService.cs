
using Microsoft.EntityFrameworkCore;
using Sep2026.MyanmarHistory.App.Dtos;
using Sep2026.MyanmarHistory.App.Interfaces;
using Sep2026.MyanmarHistory.Database.Data;
using Sep2026.MyanmarHistory.Database.AppDbContextModels;

namespace Sep2026.MyanmarHistory.App.Services
{
    public class HistoricalEraService : IHistoricalEraService
    {
        private readonly AppDbContext _context;

        public HistoricalEraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<HistoricalEraDto>> GetAllAsync()
        {
            var eras = await _context.HistoricalEras.ToListAsync();

            return eras.Select(era => new HistoricalEraDto
            {
                EraId = era.EraId,
                EraName = era.EraName,
                StartYear = era.StartYear,
                EndYear = era.EndYear,
                Description = era.Description
            }).ToList();
        }

        public async Task<HistoricalEraDto?> GetByIdAsync(int eraId)
        {
            var era = await _context.HistoricalEras
                .FirstOrDefaultAsync(e => e.EraId == eraId);

            if (era == null)
            {
                return null;
            }

            return new HistoricalEraDto
            {
                EraId = era.EraId,
                EraName = era.EraName,
                StartYear = era.StartYear,
                EndYear = era.EndYear,
                Description = era.Description
            };
        }

        public async Task<HistoricalEraDto> CreateAsync(HistoricalEraDto eraDto)
        {
            var era = new HistoricalEra
            {
                EraName = eraDto.EraName,
                StartYear = eraDto.StartYear,
                EndYear = eraDto.EndYear,
                Description = eraDto.Description
            };

            _context.HistoricalEras.Add(era);

            await _context.SaveChangesAsync();

            eraDto.EraId = era.EraId;

            return eraDto;
        }

        public async Task<bool> UpdateAsync(int eraId, HistoricalEraDto eraDto)
        {
            var era = await _context.HistoricalEras
                .FirstOrDefaultAsync(e => e.EraId == eraId);

            if (era == null)
            {
                return false;
            }

            era.EraName = eraDto.EraName;
            era.StartYear = eraDto.StartYear;
            era.EndYear = eraDto.EndYear;
            era.Description = eraDto.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int eraId)
        {
            var era = await _context.HistoricalEras
                .FirstOrDefaultAsync(e => e.EraId == eraId);

            if (era == null)
            {
                return false;
            }

            _context.HistoricalEras.Remove(era);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
