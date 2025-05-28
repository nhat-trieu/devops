using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class ChitietPhieunhap
{
    public int MaPnCtpn { get; set; }

    public int MaSachCtpn { get; set; }

    public decimal? GiaNhap { get; set; }

    public int? SoLuong { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual Phieunhaphang MaPnCtpnNavigation { get; set; } = null!;

    public virtual Sach MaSachCtpnNavigation { get; set; } = null!;
}
