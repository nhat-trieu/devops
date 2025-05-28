using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project_BanSach.Models;

public partial class WebBanSachSqlContext : DbContext
{
    public WebBanSachSqlContext()
    {
    }

    public WebBanSachSqlContext(DbContextOptions<WebBanSachSqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChitietDonhang> ChitietDonhangs { get; set; }

    public virtual DbSet<ChitietGiohang> ChitietGiohangs { get; set; }

    public virtual DbSet<ChitietPhieunhap> ChitietPhieunhaps { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Donhang> Donhangs { get; set; }

    public virtual DbSet<Giohang> Giohangs { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaisach> Loaisaches { get; set; }

    public virtual DbSet<Ncc> Nccs { get; set; }

    public virtual DbSet<Nxb> Nxbs { get; set; }

    public virtual DbSet<Phieunhaphang> Phieunhaphangs { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<Tacgia> Tacgia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.MaCate).HasName("CATEGORY_MaCate_PK");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.TenDanhMuc).HasMaxLength(250);
        });

        modelBuilder.Entity<ChitietDonhang>(entity =>
        {
            entity.HasKey(e => new { e.MaDhCtdh, e.MaSachCtdh }).HasName("CTDH_MaDH_MaSach_PK");

            entity.ToTable("CHITIET_DONHANG");

            entity.Property(e => e.MaDhCtdh).HasColumnName("MaDH_CTDH");
            entity.Property(e => e.MaSachCtdh).HasColumnName("MaSach_CTDH");
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaDhCtdhNavigation).WithMany(p => p.ChitietDonhangs)
                .HasForeignKey(d => d.MaDhCtdh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CTDH_MaDH_FK");

            entity.HasOne(d => d.MaSachCtdhNavigation).WithMany(p => p.ChitietDonhangs)
                .HasForeignKey(d => d.MaSachCtdh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CTDH_MaSach_FK");
        });

        modelBuilder.Entity<ChitietGiohang>(entity =>
        {
            entity.HasKey(e => new { e.MaGhCtgh, e.MaSachCtgh }).HasName("CTGH_MaGH_MaSach_PK");

            entity.ToTable("CHITIET_GIOHANG");

            entity.Property(e => e.MaGhCtgh).HasColumnName("MaGH_CTGH");
            entity.Property(e => e.MaSachCtgh).HasColumnName("MaSach_CTGH");

            entity.HasOne(d => d.MaGhCtghNavigation).WithMany(p => p.ChitietGiohangs)
                .HasForeignKey(d => d.MaGhCtgh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CTGH_MaGH_FK");

            entity.HasOne(d => d.MaSachCtghNavigation).WithMany(p => p.ChitietGiohangs)
                .HasForeignKey(d => d.MaSachCtgh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CTGH_MaSach_FK");
        });

        modelBuilder.Entity<ChitietPhieunhap>(entity =>
        {
            entity.HasKey(e => new { e.MaPnCtpn, e.MaSachCtpn }).HasName("CTPN_MaPN_MaSach_PK");

            entity.ToTable("CHITIET_PHIEUNHAP");

            entity.Property(e => e.MaPnCtpn).HasColumnName("MaPN_CTPN");
            entity.Property(e => e.MaSachCtpn).HasColumnName("MaSach_CTPN");
            entity.Property(e => e.GiaNhap).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ThanhTien).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaPnCtpnNavigation).WithMany(p => p.ChitietPhieunhaps)
                .HasForeignKey(d => d.MaPnCtpn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CTPN_MaPN_FK");

            entity.HasOne(d => d.MaSachCtpnNavigation).WithMany(p => p.ChitietPhieunhaps)
                .HasForeignKey(d => d.MaSachCtpn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CTPN_MaSach_FK");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.MaDisc).HasName("DISCOUNT_MaDisc_PK");

            entity.ToTable("DISCOUNT");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.TenDisc).HasMaxLength(250);
        });

        modelBuilder.Entity<Donhang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("DONHANG_MaDH_PK");

            entity.ToTable("DONHANG");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.DiaChiGiaoHang)
                .HasMaxLength(250)
                .HasColumnName("DiaChi_GiaoHang");
            entity.Property(e => e.MaKhDh).HasColumnName("MaKH_DH");
            entity.Property(e => e.NgayDat).HasColumnType("date");
            entity.Property(e => e.NgayGiao).HasColumnType("date");

            entity.HasOne(d => d.MaKhDhNavigation).WithMany(p => p.Donhangs)
                .HasForeignKey(d => d.MaKhDh)
                .HasConstraintName("DONHANG_MaKH_FK");
        });

        modelBuilder.Entity<Giohang>(entity =>
        {
            entity.HasKey(e => e.MaGh).HasName("GIOHANG_MaGH_PK");

            entity.ToTable("GIOHANG");

            entity.Property(e => e.MaGh).HasColumnName("MaGH");
            entity.Property(e => e.MaKhGh).HasColumnName("MaKH_GH");

            entity.HasOne(d => d.MaKhGhNavigation).WithMany(p => p.Giohangs)
                .HasForeignKey(d => d.MaKhGh)
                .HasConstraintName("GIOHANG_MaKH_FK");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("KHACHHANG_MaKH_PK");

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenKh)
                .HasMaxLength(250)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<Loaisach>(entity =>
        {
            entity.HasKey(e => e.MaLoaiSach).HasName("LOAISACH_MaLoaiSach_PK");

            entity.ToTable("LOAISACH");

            entity.Property(e => e.TenLoaiSach).HasMaxLength(250);
        });

        modelBuilder.Entity<Ncc>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("NCC_MaNCC_PK");

            entity.ToTable("NCC");

            entity.Property(e => e.MaNcc).HasColumnName("MaNCC");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(250)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<Nxb>(entity =>
        {
            entity.HasKey(e => e.MaNxb).HasName("NXB_MaNXB_PK");

            entity.ToTable("NXB");

            entity.Property(e => e.MaNxb).HasColumnName("MaNXB");
            entity.Property(e => e.TenNxb)
                .HasMaxLength(250)
                .HasColumnName("TenNXB");
        });

        modelBuilder.Entity<Phieunhaphang>(entity =>
        {
            entity.HasKey(e => e.MaPn).HasName("PHIEUNHAPHANG_MaPN_PK");

            entity.ToTable("PHIEUNHAPHANG");

            entity.Property(e => e.MaPn).HasColumnName("MaPN");
            entity.Property(e => e.MaNccPn).HasColumnName("MaNCC_PN");
            entity.Property(e => e.NgayNhap).HasColumnType("date");

            entity.HasOne(d => d.MaNccPnNavigation).WithMany(p => p.Phieunhaphangs)
                .HasForeignKey(d => d.MaNccPn)
                .HasConstraintName("PNH_MaNCC_FK");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.MaSach).HasName("SACH_MaSach_PK");

            entity.ToTable("SACH");

            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MaCateSach).HasColumnName("MaCate_SACH");
            entity.Property(e => e.MaDiscSach).HasColumnName("MaDisc_SACH");
            entity.Property(e => e.MaLoaiSachSach).HasColumnName("MaLoaiSach_SACH");
            entity.Property(e => e.MaNccSach).HasColumnName("MaNCC_SACH");
            entity.Property(e => e.MaNxbSach).HasColumnName("MaNXB_SACH");
            entity.Property(e => e.MaTgSach).HasColumnName("MaTG_SACH");
            entity.Property(e => e.MoTa).HasColumnType("text");
            entity.Property(e => e.TenSach).HasColumnType("text");

            entity.HasOne(d => d.MaCateSachNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaCateSach)
                .HasConstraintName("SACH_MaCate_FK");

            entity.HasOne(d => d.MaDiscSachNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaDiscSach)
                .HasConstraintName("SACH_MaDisc_FK");

            entity.HasOne(d => d.MaLoaiSachSachNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaLoaiSachSach)
                .HasConstraintName("SACH_MaLoaiSach_FK");

            entity.HasOne(d => d.MaNccSachNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaNccSach)
                .HasConstraintName("SACH_MaNCC_FK");

            entity.HasOne(d => d.MaNxbSachNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaNxbSach)
                .HasConstraintName("SACH_MaNXB_FK");

            entity.HasOne(d => d.MaTgSachNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaTgSach)
                .HasConstraintName("SACH_MaTG_FK");
        });

        modelBuilder.Entity<Tacgia>(entity =>
        {
            entity.HasKey(e => e.MaTg).HasName("TACGIA_MaTG_PK");

            entity.ToTable("TACGIA");

            entity.Property(e => e.MaTg).HasColumnName("MaTG");
            entity.Property(e => e.TenTacGia).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
