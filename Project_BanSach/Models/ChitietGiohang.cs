using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class ChitietGiohang
{
    public int MaGhCtgh { get; set; }

    public int MaSachCtgh { get; set; }

    public int? SoLuong { get; set; }

    public virtual Giohang MaGhCtghNavigation { get; set; } = null!;

    public virtual Sach MaSachCtghNavigation { get; set; } = null!;
}
