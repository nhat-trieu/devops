using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Giohang
{
    public int MaGh { get; set; }

    public int? MaKhGh { get; set; }

    public virtual ICollection<ChitietGiohang> ChitietGiohangs { get; set; } = new List<ChitietGiohang>();

    public virtual Khachhang? MaKhGhNavigation { get; set; }
}
