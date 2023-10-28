using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Admin.Models
{
    public partial class QLBanDTContext : DbContext
    {
        public QLBanDTContext()
        {
        }

        public QLBanDTContext(DbContextOptions<QLBanDTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAdmin> TAdmins { get; set; } = null!;
        public virtual DbSet<TChiTietHdb> TChiTietHdbs { get; set; } = null!;
        public virtual DbSet<TChiTietHdn> TChiTietHdns { get; set; } = null!;
        public virtual DbSet<THang> THangs { get; set; } = null!;
        public virtual DbSet<THoaDonBan> THoaDonBans { get; set; } = null!;
        public virtual DbSet<THoaDonNhap> THoaDonNhaps { get; set; } = null!;
        public virtual DbSet<TKhachHang> TKhachHangs { get; set; } = null!;
        public virtual DbSet<TNhaCungCap> TNhaCungCaps { get; set; } = null!;
        public virtual DbSet<TSp> TSp { get; set; } = null!;
        public virtual DbSet<TTheLoai> TTheLoais { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source = DESKTOP-Q96SDP5\\SQLEXPRESS; Initial Catalog = QLBanDT; Persist Security Info = true; User ID = sa; Password = 123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAdmin>(entity =>
            {
                entity.HasKey(e => e.MaAdmin)
                    .HasName("PK_tNhanVien");

                entity.ToTable("tAdmin");

                entity.Property(e => e.MaAdmin).HasMaxLength(10);

                entity.Property(e => e.PassWord)
                    .HasMaxLength(50)
                    .HasColumnName("passWord");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");
            });

            modelBuilder.Entity<TChiTietHdb>(entity =>
            {
                entity.HasKey(e => new { e.SoHdb, e.MaSp });

                entity.ToTable("tChiTietHDB");

                entity.Property(e => e.SoHdb)
                    .HasMaxLength(10)
                    .HasColumnName("SoHDB");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .HasColumnName("MaSP");

                entity.Property(e => e.KhuyenMai).HasMaxLength(100);

                entity.Property(e => e.Slban).HasColumnName("SLBan");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.TChiTietHdbs)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tChiTietHDB_tSP");

                entity.HasOne(d => d.SoHdbNavigation)
                    .WithMany(p => p.TChiTietHdbs)
                    .HasForeignKey(d => d.SoHdb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tChiTietHDB_tHoaDonBan");
            });

            modelBuilder.Entity<TChiTietHdn>(entity =>
            {
                entity.HasKey(e => new { e.SoHdn, e.MaSp });

                entity.ToTable("tChiTietHDN");

                entity.Property(e => e.SoHdn)
                    .HasMaxLength(10)
                    .HasColumnName("SoHDN");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .HasColumnName("MaSP");

                entity.Property(e => e.KhuyenMai).HasMaxLength(100);

                entity.Property(e => e.Slnhap).HasColumnName("SLNhap");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.TChiTietHdns)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tChiTietHDN_tSP");

                entity.HasOne(d => d.SoHdnNavigation)
                    .WithMany(p => p.TChiTietHdns)
                    .HasForeignKey(d => d.SoHdn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tChiTietHDN_tHoaDonNhap");
            });

            modelBuilder.Entity<THang>(entity =>
            {
                entity.HasKey(e => e.MaHang)
                    .HasName("PK_tTheLoai");

                entity.ToTable("tHang");

                entity.Property(e => e.MaHang).HasMaxLength(10);

                entity.Property(e => e.TenHang).HasMaxLength(100);
            });

            modelBuilder.Entity<THoaDonBan>(entity =>
            {
                entity.HasKey(e => e.SoHdb);

                entity.ToTable("tHoaDonBan");

                entity.Property(e => e.SoHdb)
                    .HasMaxLength(10)
                    .HasColumnName("SoHDB");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH");

                entity.Property(e => e.NgayBan).HasColumnType("datetime");

                entity.Property(e => e.TongHdb)
                    .HasColumnType("money")
                    .HasColumnName("TongHDB");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.THoaDonBans)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_tHoaDonBan_tKhachHang");
            });

            modelBuilder.Entity<THoaDonNhap>(entity =>
            {
                entity.HasKey(e => e.SoHdn);

                entity.ToTable("tHoaDonNhap");

                entity.Property(e => e.SoHdn)
                    .HasMaxLength(10)
                    .HasColumnName("SoHDN");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.Property(e => e.TongHdn)
                    .HasColumnType("money")
                    .HasColumnName("TongHDN");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.THoaDonNhaps)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_tHoaDonNhap_tNhaCungCap");
            });

            modelBuilder.Entity<TKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("tKhachHang");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH");

                entity.Property(e => e.DiaChi).HasMaxLength(150);

                entity.Property(e => e.DienThoai).HasMaxLength(15);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<TNhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.ToTable("tNhaCungCap");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.TenNcc)
                    .HasMaxLength(200)
                    .HasColumnName("TenNCC");
            });

            modelBuilder.Entity<TSp>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK_tSach");

                entity.ToTable("tSP");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .HasColumnName("MaSP");

                entity.Property(e => e.Anh).HasColumnType("nvarchar(100)");

                entity.Property(e => e.DonGiaBan).HasColumnType("money");

                entity.Property(e => e.DonGiaNhap).HasColumnType("money");

                entity.Property(e => e.MaHang).HasMaxLength(10);

                entity.Property(e => e.MaTl)
                    .HasMaxLength(10)
                    .HasColumnName("MaTL");

                entity.Property(e => e.TenSp)
                    .HasMaxLength(200)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaHangNavigation)
                    .WithMany(p => p.TSp)
                    .HasForeignKey(d => d.MaHang)
                    .HasConstraintName("FK_tSP_tHang");

                entity.HasOne(d => d.MaTlNavigation)
                    .WithMany(p => p.TSp)
                    .HasForeignKey(d => d.MaTl)
                    .HasConstraintName("FK_tSP_tTheLoai");
            });

            modelBuilder.Entity<TTheLoai>(entity =>
            {
                entity.HasKey(e => e.MaTl)
                    .HasName("PK_tNhaXuatBan");

                entity.ToTable("tTheLoai");

                entity.Property(e => e.MaTl)
                    .HasMaxLength(10)
                    .HasColumnName("MaTL");

                entity.Property(e => e.TenTl)
                    .HasMaxLength(100)
                    .HasColumnName("TenTL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
