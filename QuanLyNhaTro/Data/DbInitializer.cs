using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Data;

/// <summary>
/// Khởi tạo database: tự động áp dụng migration và tạo dữ liệu mẫu lần đầu chạy.
/// </summary>
public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services, ILogger logger)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();

        // Tự động tạo schema: SQLite (demo) dùng EnsureCreated, SQL Server dùng migration
        if (db.Database.IsSqlite())
            await db.Database.EnsureCreatedAsync();
        else
            await db.Database.MigrateAsync();

        // Đã có dữ liệu thì bỏ qua seed
        if (await db.Users.AnyAsync()) return;

        logger.LogInformation("Bắt đầu tạo dữ liệu mẫu cho QuanLyNhaTroDB...");

        var rnd = new Random(2026);
        var today = DateTime.Today;
        var firstOfMonth = new DateTime(today.Year, today.Month, 1);

        // ================= Tài khoản Chủ trọ =================
        var admin = new AppUser
        {
            UserName = "chutro",
            Email = "chutro@nhatro.vn",
            HoTen = "Chủ trọ",
            PhoneNumber = "0901234567",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(admin, "Chutro@123");
        if (!result.Succeeded)
        {
            logger.LogError("Không tạo được tài khoản chủ trọ: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            return;
        }

        // ================= Phòng =================
        var activeCodes = new[] { "P101", "P102", "P103", "P104", "P105", "P106", "P107", "P108", "P201", "P202", "P203", "P204" };
        var cocCodes = new[] { "P205", "P206", "P207" };
        var suaChuaCodes = new[] { "P110", "P208" };
        var trongCodes = new[] { "P109", "P111", "P112" };

        var rooms = new List<Room>();
        var allCodes = activeCodes.Concat(cocCodes).Concat(suaChuaCodes).Concat(trongCodes);
        foreach (var code in allCodes)
        {
            rooms.Add(new Room
            {
                MaPhongKyHieu = code,
                TenPhong = "Phòng " + code.Substring(1),
                LoaiPhong = code.StartsWith("P1") ? "Phòng đơn" : "Phòng đôi",
                DienTich = rnd.Next(15, 36),
                GiaPhong = rnd.Next(12, 26) * 100000m,
                TrangThai = RoomStatus.Trong,
                GhiChu = null
            });
        }
        db.Rooms.AddRange(rooms);
        await db.SaveChangesAsync();
        var roomByCode = rooms.ToDictionary(r => r.MaPhongKyHieu);

        // ================= Dịch vụ =================
        var dvInternet = new Service { TenDichVu = "Internet", DonViTinh = "tháng", DonGia = 70000m, TrangThai = true };
        var dvGuiXe = new Service { TenDichVu = "Gửi xe", DonViTinh = "chiếc", DonGia = 60000m, TrangThai = true };
        var dvVeSinh = new Service { TenDichVu = "Vệ sinh", DonViTinh = "người", DonGia = 30000m, TrangThai = true };
        db.Services.AddRange(dvInternet, dvGuiXe, dvVeSinh);
        await db.SaveChangesAsync();

        // ================= Người thuê =================
        var names = new[]
        {
            "Nguyễn Văn An", "Trần Thị Bích", "Lê Văn Cường", "Phạm Thị Dung", "Hoàng Văn Em",
            "Đỗ Thị Phương", "Vũ Văn Giang", "Bùi Thị Hạnh", "Đặng Văn Inh", "Ngô Thị Khanh",
            "Dương Văn Lâm", "Lý Thị Mai", "Phan Văn Nam", "Trịnh Thị Oanh", "Hồ Văn Phúc",
            "Chu Thị Quỳnh", "Trương Văn Sơn", "Tạ Thị Trang", "Cao Văn Út"
        };
        var queQuans = new[] { "Hà Tĩnh", "Nghệ An", "Thanh Hóa", "Quảng Ngãi", "Đồng Nai", "Bến Tre", "Vĩnh Long", "Cà Mau" };
        var tenants = names.Select((n, i) => new Tenant
        {
            HoTen = n,
            CCCD = $"037{ (100000000 + i * 7919).ToString().PadLeft(9, '0') }",
            SoDienThoai = $"09{rnd.Next(10, 99)}{rnd.Next(100000, 999999)}",
            QueQuan = queQuans[i % queQuans.Length],
            TrangThai = "Chưa thuê"
        }).ToList();
        db.Tenants.AddRange(tenants);
        await db.SaveChangesAsync();

        // ================= Hợp đồng =================
        var contracts = new List<Contract>();
        var contractNo = 1;
        Contract NewContract(Room room, Tenant tenant, DateTime start, DateTime end, decimal gia, decimal coc, ContractStatus status)
        {
            var c = new Contract
            {
                MaHopDongKyHieu = $"HD{contractNo++:0000}",
                MaPhong = room.MaPhong,
                MaNguoiThue = tenant.MaNguoiThue,
                NgayBatDau = start,
                NgayKetThuc = end,
                GiaThue = gia,
                TienCoc = coc,
                TrangThai = status
            };
            contracts.Add(c);
            return c;
        }

        // 12 hợp đồng đang hiệu lực (bắt đầu 3-8 tháng trước, hạn 12 tháng)
        for (var i = 0; i < activeCodes.Length; i++)
        {
            var room = roomByCode[activeCodes[i]];
            var tenant = tenants[i];
            var start = firstOfMonth.AddMonths(-rnd.Next(3, 9)).AddDays(rnd.Next(0, 5));
            var end = start.AddYears(1).AddDays(-1);
            room.TrangThai = RoomStatus.DangThue;
            tenant.TrangThai = "Đang thuê";
            NewContract(room, tenant, start, end, room.GiaPhong, room.GiaPhong * (rnd.Next(1, 3)), ContractStatus.HieuLuc);
        }

        // 3 hợp đồng đã cọc (bắt đầu tháng sau)
        for (var i = 0; i < cocCodes.Length; i++)
        {
            var room = roomByCode[cocCodes[i]];
            var tenant = tenants[activeCodes.Length + i];
            var start = firstOfMonth.AddMonths(1);
            var end = start.AddYears(1).AddDays(-1);
            room.TrangThai = RoomStatus.DaCoc;
            tenant.TrangThai = "Đã cọc";
            NewContract(room, tenant, start, end, room.GiaPhong, room.GiaPhong, ContractStatus.HieuLuc);
        }

        // Hợp đồng quá khứ P109 - đã thanh lý bình thường
        {
            var room = roomByCode["P109"];
            var tenant = tenants[15];
            var start = firstOfMonth.AddMonths(-13);
            var end = start.AddMonths(9).AddDays(-1);
            room.TrangThai = RoomStatus.Trong;
            tenant.TrangThai = "Đã trả phòng";
            NewContract(room, tenant, start, end, room.GiaPhong, room.GiaPhong, ContractStatus.DaThanhLy);
        }

        // Hợp đồng quá khứ P110 - đã trả phòng (quyết toán + MoveOut), phòng chuyển sửa chữa
        Contract? contractP110 = null;
        {
            var room = roomByCode["P110"];
            var tenant = tenants[16];
            var start = firstOfMonth.AddMonths(-14);
            var end = start.AddMonths(12).AddDays(-1);
            room.TrangThai = RoomStatus.DangSuaChua;
            room.GhiChu = "Đang sửa chữa sau khi khách trả phòng";
            tenant.TrangThai = "Đã trả phòng";
            contractP110 = NewContract(room, tenant, start, end, room.GiaPhong, room.GiaPhong * 2, ContractStatus.DaThanhLy);
        }

        db.Contracts.AddRange(contracts);
        await db.SaveChangesAsync();

        // ================= Điện nước (12 phòng đang thuê × 3 kỳ gần nhất) =================
        var dienGia = 3500m;
        var nuocGia = 15000m;
        var readings = new List<UtilityReading>();
        var kyM2 = firstOfMonth.AddMonths(-2).ToString("yyyy-MM");
        var kyM1 = firstOfMonth.AddMonths(-1).ToString("yyyy-MM");
        var kyM0 = firstOfMonth.ToString("yyyy-MM");
        var latestDien = new Dictionary<int, decimal>();
        var latestNuoc = new Dictionary<int, decimal>();

        foreach (var code in activeCodes)
        {
            var room = roomByCode[code];
            var dienCu = rnd.Next(120, 400);
            var nuocCu = rnd.Next(4, 15);
            foreach (var ky in new[] { kyM2, kyM1, kyM0 })
            {
                var dienMoi = dienCu + rnd.Next(60, 160);
                var nuocMoi = nuocCu + rnd.Next(3, 8);
                readings.Add(new UtilityReading
                {
                    MaPhong = room.MaPhong, KyGhi = ky,
                    ChiSoDienCu = dienCu, ChiSoDienMoi = dienMoi, DonGiaDien = dienGia,
                    ChiSoNuocCu = nuocCu, ChiSoNuocMoi = nuocMoi, DonGiaNuoc = nuocGia
                });
                dienCu = dienMoi; nuocCu = nuocMoi;
            }
            latestDien[room.MaPhong] = dienCu;
            latestNuoc[room.MaPhong] = nuocCu;
        }

        // P109: 1 kỳ ghi theo hợp đồng cũ
        {
            var room = roomByCode["P109"];
            readings.Add(new UtilityReading
            {
                MaPhong = room.MaPhong, KyGhi = firstOfMonth.AddMonths(-13).ToString("yyyy-MM"),
                ChiSoDienCu = 0, ChiSoDienMoi = 180, DonGiaDien = dienGia,
                ChiSoNuocCu = 0, ChiSoNuocMoi = 6, DonGiaNuoc = nuocGia
            });
        }
        // P110: kỳ ghi cuối trước trả phòng
        UtilityReading? readingP110 = null;
        {
            var room = roomByCode["P110"];
            readingP110 = new UtilityReading
            {
                MaPhong = room.MaPhong, KyGhi = firstOfMonth.AddMonths(-14).ToString("yyyy-MM"),
                ChiSoDienCu = 0, ChiSoDienMoi = 220, DonGiaDien = dienGia,
                ChiSoNuocCu = 0, ChiSoNuocMoi = 7, DonGiaNuoc = nuocGia
            };
            readings.Add(readingP110);
        }
        db.UtilityReadings.AddRange(readings);
        await db.SaveChangesAsync();

        // ================= Sử dụng dịch vụ =================
        var usages = new List<ServiceUsage>();
        foreach (var code in activeCodes)
        {
            var room = roomByCode[code];
            foreach (var ky in new[] { kyM2, kyM1, kyM0 })
            {
                usages.Add(new ServiceUsage { MaPhong = room.MaPhong, MaDichVu = dvInternet.MaDichVu, KySuDung = ky, SoLuong = 1, DonGiaApDung = dvInternet.DonGia, ThanhTien = dvInternet.DonGia });
                usages.Add(new ServiceUsage { MaPhong = room.MaPhong, MaDichVu = dvGuiXe.MaDichVu, KySuDung = ky, SoLuong = rnd.Next(1, 3), DonGiaApDung = dvGuiXe.DonGia, ThanhTien = 0 });
                usages.Last().ThanhTien = usages.Last().SoLuong * usages.Last().DonGiaApDung;
            }
        }
        {
            var room109 = roomByCode["P109"];
            usages.Add(new ServiceUsage { MaPhong = room109.MaPhong, MaDichVu = dvInternet.MaDichVu, KySuDung = firstOfMonth.AddMonths(-13).ToString("yyyy-MM"), SoLuong = 1, DonGiaApDung = dvInternet.DonGia, ThanhTien = dvInternet.DonGia });
            var room110 = roomByCode["P110"];
            usages.Add(new ServiceUsage { MaPhong = room110.MaPhong, MaDichVu = dvInternet.MaDichVu, KySuDung = firstOfMonth.AddMonths(-14).ToString("yyyy-MM"), SoLuong = 1, DonGiaApDung = dvInternet.DonGia, ThanhTien = dvInternet.DonGia });
        }
        db.ServiceUsages.AddRange(usages);
        await db.SaveChangesAsync();

        // ================= Hóa đơn + Thanh toán =================
        var invoices = new List<Invoice>();
        var payments = new List<Payment>();
        var invoiceCounter = new Dictionary<string, int>();
        Invoice NewInvoice(Contract c, string ky, DateTime ngayLap, bool paid, PaymentMethod method)
        {
            if (!invoiceCounter.TryGetValue(ky, out var n)) { n = 1; invoiceCounter[ky] = 1; }
            invoiceCounter[ky] = n + 1;

            var reading = readings.FirstOrDefault(u => u.MaPhong == c.MaPhong && u.KyGhi == ky);
            var tienDien = (reading?.SoDienSuDung ?? 0) * (reading?.DonGiaDien ?? 0);
            var tienNuoc = (reading?.SoNuocSuDung ?? 0) * (reading?.DonGiaNuoc ?? 0);
            var tienDV = usages.Where(su => su.MaPhong == c.MaPhong && su.KySuDung == ky).Sum(su => su.ThanhTien);

            var inv = new Invoice
            {
                MaHoaDonKyHieu = $"HD{ky.Replace("-", "")}-{n:000}",
                MaPhong = c.MaPhong,
                MaHopDong = c.MaHopDong,
                KyHoaDon = ky,
                NgayLap = ngayLap,
                TienPhong = c.GiaThue,
                TienDien = tienDien,
                TienNuoc = tienNuoc,
                TienDichVu = tienDV,
                HanThanhToan = ngayLap.AddDays(10),
                TrangThaiThanhToan = paid ? InvoicePaymentStatus.DaThanhToan : InvoicePaymentStatus.ChuaThanhToan,
                GhiChu = null
            };
            inv.TongTien = inv.TienPhong + inv.TienDien + inv.TienNuoc + inv.TienDichVu;
            invoices.Add(inv);
            if (paid)
            {
                payments.Add(new Payment
                {
                    MaHoaDon = 0, // gán sau khi lưu
                    Invoice = inv,
                    SoTien = inv.TongTien,
                    NgayThanhToan = ngayLap.AddDays(rnd.Next(3, 12)),
                    PhuongThuc = method
                });
            }
            return inv;
        }

        // Hóa đơn phòng đang thuê: 2 kỳ đã thanh toán + kỳ hiện tại chưa thanh toán (công nợ)
        foreach (var code in activeCodes)
        {
            var c = contracts.First(x => x.MaPhong == roomByCode[code].MaPhong && x.TrangThai == ContractStatus.HieuLuc);
            NewInvoice(c, kyM2, firstOfMonth.AddMonths(-2), true, rnd.Next(2) == 0 ? PaymentMethod.TienMat : PaymentMethod.ChuyenKhoan);
            NewInvoice(c, kyM1, firstOfMonth.AddMonths(-1), true, rnd.Next(2) == 0 ? PaymentMethod.TienMat : PaymentMethod.ChuyenKhoan);
            NewInvoice(c, kyM0, firstOfMonth, false, PaymentMethod.TienMat);
        }

        // Hóa đơn quá khứ của P109, P110 (đã thanh toán)
        foreach (var c in contracts.Where(x => x.TrangThai == ContractStatus.DaThanhLy))
        {
            var kyOld = c.NgayBatDau.ToString("yyyy-MM");
            var reading = readings.FirstOrDefault(u => u.MaPhong == c.MaPhong && u.KyGhi == kyOld);
            if (reading != null) NewInvoice(c, kyOld, c.NgayBatDau, true, PaymentMethod.TienMat);
        }

        db.Invoices.AddRange(invoices);
        await db.SaveChangesAsync();

        foreach (var p in payments) { p.MaHoaDon = p.Invoice!.MaHoaDon; }
        db.Payments.AddRange(payments);
        await db.SaveChangesAsync();

        // ================= Trả phòng (MoveOut) cho P110 =================
        if (contractP110 != null && readingP110 != null)
        {
            var kyChot = $"TRAPHONG-{contractP110.MaHopDong}";
            var chot = new UtilityReading
            {
                MaPhong = contractP110.MaPhong, KyGhi = kyChot,
                ChiSoDienCu = readingP110.ChiSoDienMoi, ChiSoDienMoi = readingP110.ChiSoDienMoi + 85, DonGiaDien = dienGia,
                ChiSoNuocCu = readingP110.ChiSoNuocMoi, ChiSoNuocMoi = readingP110.ChiSoNuocMoi + 4, DonGiaNuoc = nuocGia,
                GhiChu = "Chốt chỉ số khi trả phòng"
            };
            db.UtilityReadings.Add(chot);
            await db.SaveChangesAsync();

            var tienPhatSinh = ((chot.ChiSoDienMoi - chot.ChiSoDienCu) * chot.DonGiaDien) + ((chot.ChiSoNuocMoi - chot.ChiSoNuocCu) * chot.DonGiaNuoc);
            var congNoP110 = 0m; // các hóa đơn của P110 đã thanh toán hết
            var phaiThu = congNoP110 + tienPhatSinh;
            var coc = contractP110.TienCoc;
            var khauTru = Math.Min(coc, phaiThu);
            var hoanCoc = Math.Max(0, coc - phaiThu);
            var traThem = Math.Max(0, phaiThu - coc);

            db.MoveOuts.Add(new MoveOut
            {
                MaHopDong = contractP110.MaHopDong,
                NgayTraPhong = contractP110.NgayKetThuc,
                CongNoTruocTraPhong = congNoP110,
                TienPhatSinhCuoiKy = tienPhatSinh,
                KhoanPhaiThuCuoiCung = phaiThu,
                TienCocBanDau = coc,
                TienKhauTruCoc = khauTru,
                TienHoanCoc = hoanCoc,
                TienPhaiTraThem = traThem,
                DaQuyetToan = true,
                TrangThaiPhongSauTra = RoomStatus.DangSuaChua,
                GhiChu = "Khách trả phòng giữa hợp đồng, phòng chuyển sang sửa chữa"
            });

            db.ContractHistories.AddRange(new[]
            {
                new ContractHistory { MaHopDong = contractP110.MaHopDong, HanhDong = "Tạo mới", NoiDung = "Lập hợp đồng thuê phòng P110.", ThoiGian = contractP110.NgayBatDau },
                new ContractHistory { MaHopDong = contractP110.MaHopDong, HanhDong = "Thanh lý (trả phòng)", NoiDung = $"Quyết toán trả phòng ngày {contractP110.NgayKetThuc:dd/MM/yyyy}. Khoản phải thu cuối cùng {phaiThu:N0} đ.", ThoiGian = contractP110.NgayKetThuc }
            });
            await db.SaveChangesAsync();
        }

        // Lịch sử tạo hợp đồng cho các hợp đồng còn lại
        foreach (var c in contracts.Where(x => x.MaHopDong != contractP110?.MaHopDong))
        {
            db.ContractHistories.Add(new ContractHistory
            {
                MaHopDong = c.MaHopDong,
                HanhDong = "Tạo mới",
                NoiDung = $"Lập hợp đồng thuê phòng {c.Room?.MaPhongKyHieu}, thời hạn {c.NgayBatDau:dd/MM/yyyy} - {c.NgayKetThuc:dd/MM/yyyy}.",
                ThoiGian = c.NgayBatDau
            });
        }
        await db.SaveChangesAsync();

        var doanhThu = payments.Sum(p => p.SoTien);
        var congNo = invoices.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan).Sum(i => i.TongTien);
        logger.LogInformation("Seed hoàn tất: 20 phòng, {Tenants} người thuê, {Contracts} hợp đồng, {Invoices} hóa đơn. Doanh thu mẫu: {Revenue:N0} đ, công nợ: {CongNo:N0} đ",
            tenants.Count, contracts.Count, invoices.Count, doanhThu, congNo);
    }
}
