using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Nxb
{
    public int MaNxb { get; set; }

    public string? TenNxb { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
