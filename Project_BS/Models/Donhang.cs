using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Donhang
{
    public int MaDh { get; set; }

    public DateTime? NgayDat { get; set; }

    public DateTime? NgayGiao { get; set; }

    public bool? TinhTrang { get; set; }

    public string? DiaChiGiaoHang { get; set; }

    public int? MaKhDh { get; set; }

    public virtual ICollection<ChitietDonhang> ChitietDonhangs { get; set; } = new List<ChitietDonhang>();

    public virtual Khachhang? MaKhDhNavigation { get; set; }
}
