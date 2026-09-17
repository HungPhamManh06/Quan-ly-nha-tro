using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<ContractHistory> ContractHistories => Set<ContractHistory>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<ServiceUsage> ServiceUsages => Set<ServiceUsage>();
    public DbSet<UtilityReading> UtilityReadings => Set<UtilityReading>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<MoveOut> MoveOuts => Set<MoveOut>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ---- Khóa chính tường minh ----
        builder.Entity<Room>().HasKey(r => r.MaPhong);
        builder.Entity<Tenant>().HasKey(t => t.MaNguoiThue);
        builder.Entity<Contract>().HasKey(c => c.MaHopDong);
        builder.Entity<ContractHistory>().HasKey(h => h.MaLichSu);
        builder.Entity<Service>().HasKey(s => s.MaDichVu);
        builder.Entity<ServiceUsage>().HasKey(su => su.MaSuDung);
        builder.Entity<UtilityReading>().HasKey(u => u.MaGhiChiSo);
        builder.Entity<Invoice>().HasKey(i => i.MaHoaDon);
        builder.Entity<Payment>().HasKey(p => p.MaThanhToan);
        builder.Entity<MoveOut>().HasKey(m => m.MaTraPhong);

        // Bỏ tiền tố AspNet cho bảng Identity cho dễ đọc
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

        // ---- Unique constraints bắt buộc theo đặc tả ----
        builder.Entity<Room>(e =>
        {
            e.HasIndex(x => x.MaPhongKyHieu).IsUnique().HasFilter("[DaXoa] = 0");
            e.HasQueryFilter(x => !x.DaXoa); // phòng "ngừng sử dụng" vẫn còn trong DB nhưng ẩn khỏi truy vấn thường
        });

        builder.Entity<Tenant>()
            .HasIndex(x => x.CCCD)
            .IsUnique();

        builder.Entity<Contract>()
            .HasIndex(x => x.MaHopDongKyHieu)
            .IsUnique();

        builder.Entity<Invoice>(e =>
        {
            e.HasIndex(x => x.MaHoaDonKyHieu).IsUnique();
            // 1 phòng chỉ có 1 hóa đơn / 1 kỳ
            e.HasIndex(x => new { x.MaPhong, x.KyHoaDon }).IsUnique();
        });

        builder.Entity<Service>()
            .HasIndex(x => x.TenDichVu)
            .IsUnique();

        // 1 phòng + 1 dịch vụ + 1 kỳ chỉ có 1 bản ghi sử dụng
        builder.Entity<ServiceUsage>()
            .HasIndex(x => new { x.MaPhong, x.MaDichVu, x.KySuDung })
            .IsUnique();

        // 1 phòng + 1 kỳ chỉ có 1 bản ghi điện nước
        builder.Entity<UtilityReading>()
            .HasIndex(x => new { x.MaPhong, x.KyGhi })
            .IsUnique();

        // 1 hóa đơn chỉ được thanh toán 1 lần
        builder.Entity<Payment>()
            .HasIndex(x => x.MaHoaDon)
            .IsUnique();

        // ---- Quan hệ & hành vi xóa (giữ lịch sử, không cascade làm mất dữ liệu) ----
        builder.Entity<Contract>(e =>
        {
            e.HasOne(x => x.Room).WithMany(r => r.Contracts).HasForeignKey(x => x.MaPhong).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Tenant).WithMany(t => t.Contracts).HasForeignKey(x => x.MaNguoiThue).OnDelete(DeleteBehavior.Restrict);
            e.HasMany(x => x.Histories).WithOne(h => h.Contract!).HasForeignKey(h => h.MaHopDong).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Invoice>(e =>
        {
            e.HasOne(x => x.Room).WithMany(r => r.Invoices).HasForeignKey(x => x.MaPhong).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Contract).WithMany(c => c.Invoices).HasForeignKey(x => x.MaHopDong).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Payment>(e =>
        {
            e.HasOne(x => x.Invoice).WithOne(i => i.Payment).HasForeignKey<Payment>(x => x.MaHoaDon).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<UtilityReading>(e =>
        {
            e.HasOne(x => x.Room).WithMany(r => r.UtilityReadings).HasForeignKey(x => x.MaPhong).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ServiceUsage>(e =>
        {
            e.HasOne(x => x.Room).WithMany(r => r.ServiceUsages).HasForeignKey(x => x.MaPhong).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Service).WithMany(s => s.ServiceUsages).HasForeignKey(x => x.MaDichVu).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<MoveOut>(e =>
        {
            e.HasOne(x => x.Contract).WithOne(c => c.MoveOut).HasForeignKey<MoveOut>(x => x.MaHopDong).OnDelete(DeleteBehavior.Cascade);
        });

        // ---- Check constraint mức database ----
        builder.Entity<Contract>().ToTable(t => t.HasCheckConstraint("CK_Contract_Ngay", "[NgayKetThuc] > [NgayBatDau]"));
        builder.Entity<UtilityReading>().ToTable(t => t.HasCheckConstraint("CK_Utility_Dien", "[ChiSoDienMoi] >= [ChiSoDienCu]"));
        builder.Entity<UtilityReading>().ToTable(t => t.HasCheckConstraint("CK_Utility_Nuoc", "[ChiSoNuocMoi] >= [ChiSoNuocCu]"));
    }
}
