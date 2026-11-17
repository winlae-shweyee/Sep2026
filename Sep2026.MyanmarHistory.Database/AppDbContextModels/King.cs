using System;
using System.Collections.Generic;

namespace Sep2026.MyanmarHistory.Database.AppDbContextModels;

public partial class King
{
    public int KingId { get; set; }

    public string KingName { get; set; } = null!;

    public int EraId { get; set; }

    public int? ReignStartYear { get; set; }

    public int? ReignEndYear { get; set; }

    public string? Description { get; set; }

    public virtual HistoricalEra Era { get; set; } = null!;
}
