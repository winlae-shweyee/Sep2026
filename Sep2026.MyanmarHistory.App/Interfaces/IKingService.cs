
using Sep2026.MyanmarHistory.App.Dtos;

namespace Sep2026.MyanmarHistory.App.Interfaces
{
    public interface IKingService
    {
        Task<List<KingDto>> GetAllAsync();

        Task<KingDto?> GetByIdAsync(int kingId);

        Task<KingDto> CreateAsync(KingDto kingDto);

        Task<bool> UpdateAsync(int kingId, KingDto kingDto);

        Task<bool> DeleteAsync(int kingId);
    }
}
