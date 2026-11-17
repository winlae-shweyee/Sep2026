using System;
using System.Collections.Generic;

namespace Sep2026.MyanmarHistory.Database.AppDbContextModels;

public partial class HistoricalEra
{
    public int EraId { get; set; }

    public string EraName { get; set; } = null!;

    public int StartYear { get; set; }

    public int EndYear { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<King> Kings { get; set; } = new List<King>();
}
