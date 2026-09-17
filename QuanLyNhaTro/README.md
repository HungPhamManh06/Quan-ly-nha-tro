# Quản lý nhà trọ (QuanLyNhaTro)

Website quản lý nhà trọ cho **Chủ trọ** — quản lý **một nhà trọ duy nhất**, xây dựng bằng:

- **C# / ASP.NET Core MVC 8** (Razor Views)
- **Entity Framework Core 8 + Microsoft SQL Server**
- **ASP.NET Core Identity** (tài khoản Chủ trọ, mật khẩu hash)
- **Bootstrap 5 + Bootstrap Icons + jQuery** (giao diện tiếng Việt, responsive)
- **Chart.js** (biểu đồ dashboard), **ClosedXML** (xuất Excel)

## 1. Chuẩn bị

1. **Cài .NET 8 SDK** (hoặc mới hơn): https://dotnet.microsoft.com/download
2. **Cài Microsoft SQL Server** — chọn một trong:
   - SQL Server Express (miễn phí): https://www.microsoft.com/sql-server/sql-server-downloads → chọn *Basic*
   - SQL Server Developer/Full + SSMS: https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms
   - LocalDB (đi kèm Visual Studio)
3. *(Khuyến nghị)* Cài **SSMS** để xem/quản lý database `QuanLyNhaTroDB`.

## 2. Cấu hình connection string

Mở `QuanLyNhaTro/appsettings.json` và sửa `Server=.` cho đúng máy của bạn:

| Trường hợp | Connection string |
|---|---|
| SQL Server mặc định | `Server=.;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |
| SQL Server Express | `Server=.\\SQLEXPRESS;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |
| LocalDB (Visual Studio) | `Server=(localdb)\\MSSQLLocalDB;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |

Mặc định project đã dùng `.\SQLEXPRESS`.

## 3. Tạo database bằng Migration

```bash
cd QuanLyNhaTro

# Cài công cụ EF nếu chưa có
dotnet tool install --global dotnet-ef

# Tạo database + toàn bộ bảng
dotnet ef database update
```

Hoặc trong **Visual Studio** (Package Manager Console):

```powershell
Add-Migration InitialCreate
Update-Database
```

> Lưu ý: nếu bạn **không** chạy migration trước, lần chạy đầu website vẫn sẽ tự động `MigrateAsync()` và seed dữ liệu mẫu (xem `Data/DbInitializer.cs`).

## 4. Chạy website

```bash
dotnet run
```

hoặc mở `QuanLyNhaTro.sln`/thư mục bằng **Visual Studio 2022** rồi nhấn **F5**.

Truy cập: `https://localhost:xxxx` → tự chuyển tới trang **Đăng nhập**.

### Tài khoản mẫu

| Tên đăng nhập | Mật khẩu |
|---|---|
| `chutro` | `Chutro@123` |

## 5. Dữ liệu mẫu (seed tự động lần đầu chạy)

- **20 phòng** P101–P208: 12 đang thuê, 3 đã cọc, 2 đang sửa chữa, 3 trống
- **19 người thuê** (CCCD, SĐT, quê quán)
- **17 hợp đồng**: 15 hiệu lực (12 đang thuê + 3 đã cọc), 2 đã thanh lý (1 có quyết toán trả phòng đầy đủ)
- **Điện nước** 3 kỳ gần nhất cho các phòng đang thuê + dữ liệu hợp đồng cũ
- **Dịch vụ**: Internet (70k/tháng), Gửi xe (60k/chiếc), Vệ sinh (30k/người) + bản ghi sử dụng
- **Hóa đơn**: 2 kỳ đã thanh toán + kỳ hiện tại chưa thanh toán (công nợ) cho từng phòng đang thuê
- **Thanh toán**: phương thức Tiền mặt / Chuyển khoản cho các hóa đơn đã thanh toán
- **MoveOut** mẫu: hợp đồng P110 đã quyết toán trả phòng (hoàn cọc), phòng chuyển sang sửa chữa

## 6. Chức năng chính

1. **Đăng nhập** — chỉ Chủ trọ (`/Account/Login`), lockout khi sai 5 lần
2. **Dashboard** — thống kê phòng/người thuê/hóa đơn/tài chính/hợp đồng + 4 biểu đồ
3. **Phòng** — CRUD, tìm kiếm, lọc trạng thái; phòng đã phát sinh dữ liệu → *Ngừng sử dụng* thay vì xóa
4. **Người thuê** — CRUD, CCCD duy nhất, phòng hiện tại suy ra từ hợp đồng hiệu lực
5. **Hợp đồng** — lập/xem/gia hạn/thanh lý, chặn chồng lấn thời gian cùng phòng, cảnh báo sắp hết hạn 30 ngày, lịch sử hợp đồng
6. **Điện nước** — ghi chỉ số theo phòng+kỳ (duy nhất), validate chỉ số mới ≥ chỉ số cũ
7. **Dịch vụ** — quản lý dịch vụ + ghi sử dụng (lưu đơn giá áp dụng tại thời điểm ghi)
8. **Hóa đơn** — tự gộp tiền phòng + điện + nước + dịch vụ, 1 phòng chỉ 1 hóa đơn/kỳ
9. **Thanh toán** — một lần duy nhất: thiếu → từ chối, thừa → hoàn lại, dùng transaction
10. **Trả phòng** — wizard 7 bước: chốt điện nước → dịch vụ → công nợ → cọc → quyết toán (MIN/MAX theo đặc tả) → thanh lý → cập nhật phòng, transaction nguyên vẹn
11. **Thống kê & báo cáo** — phòng, người thuê, hợp đồng, hóa đơn, doanh thu/công nợ, điện nước; **xuất Excel/CSV**

## 7. Business rules quan trọng (không được thay đổi)

- Trạng thái phòng chỉ gồm: **Trống / Đã cọc / Đang thuê / Đang sửa chữa**
- Hợp đồng: `NgàyKếtThúc > NgàyBắtĐau`, `GiáThuê > 0`, `TiềnCọc ≥ 0`; không chồng lấn cùng phòng
- Điện nước: `ChỉSốMới ≥ ChỉSốCũ`; unique (Phòng + Kỳ)
- Dịch vụ: unique (Phòng + Dịch vụ + Kỳ), lưu `DonGiaApDung`
- Hóa đơn: unique (Phòng + Kỳ); `TổngTiền = Phòng + Điện + Nước + Dịch vụ`
- Thanh toán: **một lần duy nhất** cho mỗi hóa đơn
- Trả phòng: **quyết toán một lần duy nhất**
- Xóa dữ liệu: không xóa cứng dữ liệu đã có lịch sử nghiệp vụ

## 8. Cấu trúc project

```
QuanLyNhaTro/
├── Controllers/        # Account, Dashboard, Rooms, Tenants, Contracts,
│                       # Utilities, Services, Invoices, Payments, MoveOut, Reports, Home
├── Models/             # EF entities + enums
├── Data/               # ApplicationDbContext, DbInitializer (seed)
├── Services/           # IRoomService, ITenantService, IContractService, IUtilityService,
│                       # IInvoiceService, IPaymentService, IMoveOutService, IReportService,
│                       # IDashboardService, IExportService
├── ViewModels/         # Form + màn hình tổng hợp
├── Views/              # Razor views (Bootstrap 5, tiếng Việt)
├── Migrations/         # EF Core migrations
└── wwwroot/            # css, js, lib
```
