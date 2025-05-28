using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Tacgia
{
    public int MaTg { get; set; }

    public string? TenTacGia { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
