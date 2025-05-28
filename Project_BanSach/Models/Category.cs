using System;
using System.Collections.Generic;

namespace Project_BanSach.Models;

public partial class Category
{
    public int MaCate { get; set; }

    public string? TenDanhMuc { get; set; }

    public bool? TrangThai { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
