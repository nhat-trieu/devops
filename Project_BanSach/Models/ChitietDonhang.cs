using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class ChitietDonhang
{
    public int MaDhCtdh { get; set; }

    public int MaSachCtdh { get; set; }

    public int? SoLuong { get; set; }

    public decimal? ThanhTien { get; set; }

    public bool? TinhTrangThanhToan { get; set; }

    public virtual Donhang MaDhCtdhNavigation { get; set; } = null!;

    public virtual Sach MaSachCtdhNavigation { get; set; } = null!;
}
