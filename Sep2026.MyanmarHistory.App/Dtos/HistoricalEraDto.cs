
namespace Sep2026.MyanmarHistory.App.Dtos
{
    public class HistoricalEraDto
    {
        public int EraId { get; set; }
        public string EraName { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string? Description { get; set; }
    }
}
