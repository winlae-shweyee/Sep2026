
using Sep2026.MyanmarHistory.App.Dtos;

namespace Sep2026.MyanmarHistory.App.Interfaces
{
    public interface IHistoricalEraService
    {
        Task<List<HistoricalEraDto>> GetAllAsync();

        Task<HistoricalEraDto?> GetByIdAsync(int eraId);

        Task<HistoricalEraDto> CreateAsync(HistoricalEraDto eraDto);

        Task<bool> UpdateAsync(int eraId, HistoricalEraDto eraDto);

        Task<bool> DeleteAsync(int eraId);
    }
}
