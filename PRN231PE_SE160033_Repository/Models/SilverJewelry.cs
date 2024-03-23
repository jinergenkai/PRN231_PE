using System;
using System.Collections.Generic;

namespace PRN231PE_SE160033_Repository.Models;

public partial class SilverJewelry
{
    public string SilverJewelryId { get; set; } = null!;

    public string SilverJewelryName { get; set; } = null!;

    public string? SilverJewelryDescription { get; set; }

    public decimal? MetalWeight { get; set; }

    public decimal? Price { get; set; }

    public int? ProductionYear { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
