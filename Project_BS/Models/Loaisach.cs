using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Loaisach
{
    public int MaLoaiSach { get; set; }

    public string? TenLoaiSach { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
