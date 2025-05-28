using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Sach
{
    public int MaSach { get; set; }

    public string? TenSach { get; set; }

    public string? HinhAnh { get; set; }

    public decimal? GiaBan { get; set; }

    public bool? TrangThai { get; set; }

    public string? MoTa { get; set; }

    public int? MaNxbSach { get; set; }

    public int? MaTgSach { get; set; }

    public int? MaLoaiSachSach { get; set; }

    public int? MaNccSach { get; set; }

    public int? MaCateSach { get; set; }

    public int? MaDiscSach { get; set; }

    public virtual ICollection<ChitietDonhang> ChitietDonhangs { get; set; } = new List<ChitietDonhang>();

    public virtual ICollection<ChitietGiohang> ChitietGiohangs { get; set; } = new List<ChitietGiohang>();

    public virtual ICollection<ChitietPhieunhap> ChitietPhieunhaps { get; set; } = new List<ChitietPhieunhap>();

    public virtual Category? MaCateSachNavigation { get; set; }

    public virtual Discount? MaDiscSachNavigation { get; set; }

    public virtual Loaisach? MaLoaiSachSachNavigation { get; set; }

    public virtual Ncc? MaNccSachNavigation { get; set; }

    public virtual Nxb? MaNxbSachNavigation { get; set; }

    public virtual Tacgia? MaTgSachNavigation { get; set; }
}
