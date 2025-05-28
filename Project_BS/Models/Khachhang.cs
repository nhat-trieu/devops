using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Khachhang
{
    public int MaKh { get; set; }

    public string? TenKh { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();

    public virtual ICollection<Giohang> Giohangs { get; set; } = new List<Giohang>();
}
