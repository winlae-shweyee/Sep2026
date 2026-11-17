
namespace Sep2026.MyanmarHistory.App.Dtos
{
    public class KingDto
    {
        public int KingId { get; set; }
        public string KingName { get; set; }
        public int EraId { get; set; }
        public int? ReignStartYear { get; set; }
        public int? ReignEndYear { get; set; }
        public string? Description { get; set; }
    }
}
