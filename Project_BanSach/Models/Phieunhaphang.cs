using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Phieunhaphang
{
    public int MaPn { get; set; }

    public DateTime? NgayNhap { get; set; }

    public int? MaNccPn { get; set; }

    public virtual ICollection<ChitietPhieunhap> ChitietPhieunhaps { get; set; } = new List<ChitietPhieunhap>();

    public virtual Ncc? MaNccPnNavigation { get; set; }
}
