using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Discount
{
    public int MaDisc { get; set; }

    public string? TenDisc { get; set; }

    public double? PhanTram { get; set; }

    public string? MoTa { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
