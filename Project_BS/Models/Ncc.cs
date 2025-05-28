using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Ncc
{
    public int MaNcc { get; set; }

    public string? TenNcc { get; set; }

    public virtual ICollection<Phieunhaphang> Phieunhaphangs { get; set; } = new List<Phieunhaphang>();

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
