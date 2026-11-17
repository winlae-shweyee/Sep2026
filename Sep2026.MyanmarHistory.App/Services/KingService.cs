
using Microsoft.EntityFrameworkCore;
using Sep2026.MyanmarHistory.App.Dtos;
using Sep2026.MyanmarHistory.App.Interfaces;
using Sep2026.MyanmarHistory.Database.Data;
using Sep2026.MyanmarHistory.Database.AppDbContextModels;

namespace Sep2026.MyanmarHistory.App.Services
{
    public class KingService : IKingService
    {
        private readonly AppDbContext _context;

        public KingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<KingDto>> GetAllAsync()
        {
            var kings = await _context.Kings.ToListAsync();

            return kings.Select(king => new KingDto
            {
                KingId = king.KingId,
                KingName = king.KingName,
                EraId = king.EraId,
                ReignStartYear = king.ReignStartYear,
                ReignEndYear = king.ReignEndYear,
                Description = king.Description
            }).ToList();
        }

        public async Task<KingDto?> GetByIdAsync(int kingId)
        {
            var king = await _context.Kings
                .FirstOrDefaultAsync(k => k.KingId == kingId);

            if (king == null)
            {
                return null;
            }

            return new KingDto
            {
                KingId = king.KingId,
                KingName = king.KingName,
                EraId = king.EraId,
                ReignStartYear = king.ReignStartYear,
                ReignEndYear = king.ReignEndYear,
                Description = king.Description
            };
        }

        public async Task<KingDto> CreateAsync(KingDto kingDto)
        {
            var king = new King
            {
                KingName = kingDto.KingName,
                EraId = kingDto.EraId,
                ReignStartYear = kingDto.ReignStartYear,
                ReignEndYear = kingDto.ReignEndYear,
                Description = kingDto.Description
            };

            _context.Kings.Add(king);

            await _context.SaveChangesAsync();

            kingDto.KingId = king.KingId;

            return kingDto;
        }

        public async Task<bool> UpdateAsync(int kingId, KingDto kingDto)
        {
            var king = await _context.Kings
                .FirstOrDefaultAsync(k => k.KingId == kingId);

            if (king == null)
            {
                return false;
            }

            king.KingName = kingDto.KingName;
            king.EraId = kingDto.EraId;
            king.ReignStartYear = kingDto.ReignStartYear;
            king.ReignEndYear = kingDto.ReignEndYear;
            king.Description = kingDto.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int kingId)
        {
            var king = await _context.Kings
                .FirstOrDefaultAsync(k => k.KingId == kingId);

            if (king == null)
            {
                return false;
            }

            _context.Kings.Remove(king);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
