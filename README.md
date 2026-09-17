# 🏠 Quản Lý Nhà Trọ

> Hệ thống quản lý nhà trọ dành riêng cho **Chủ trọ** — quản lý **một nhà trọ duy nhất**: phòng, người thuê, hợp đồng, điện nước, dịch vụ, hóa đơn, thanh toán, trả phòng và báo cáo.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/Database-Microsoft%20SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![EF Core](https://img.shields.io/badge/ORM-Entity%20Framework%20Core%208-512BD4)
![Bootstrap](https://img.shields.io/badge/UI-Bootstrap%205.3-7952B3?logo=bootstrap&logoColor=white)

---

## 📋 Mục lục

- [Tính năng](#-tính-năng)
- [Công nghệ](#-công-nghệ)
- [Giao diện](#-giao-diện)
- [Cài đặt & chạy](#-cài-đặt--chạy)
- [Tài khoản mặc định](#-tài-khoản-mặc-định)
- [Dữ liệu mẫu](#-dữ-liệu-mẫu-seed-tự-động)
- [Chế độ demo không cần SQL Server](#-chế-độ-demo-không-cần-sql-server)
- [Nghiệp vụ cốt lõi](#-nghiệp-vụ-cốt-lõi)
- [Cấu trúc project](#-cấu-trúc-project)
- [Xử lý sự cố](#-xử-lý-sự-cố)

---

## ✨ Tính năng

| # | Nhóm chức năng | Chi tiết |
|---|---|---|
| 1 | **Đăng nhập** | Chỉ dành cho Chủ trọ · mật khẩu hash (ASP.NET Core Identity) · khóa tài khoản khi sai 5 lần |
| 2 | **Dashboard** | 4 thẻ KPI · thao tác nhanh · mục **Cần xử lý** (hóa đơn quá hạn, hợp đồng sắp hết hạn) · 4 biểu đồ (doanh thu 12 tháng, công nợ, điện nước, tình trạng phòng) |
| 3 | **Quản lý phòng** | CRUD · tìm kiếm · lọc theo trạng thái · thẻ tổng hợp click-để-lọc · phòng đã phát sinh dữ liệu → *Ngừng sử dụng* thay vì xóa cứng |
| 4 | **Quản lý người thuê** | CRUD · CCCD duy nhất · phòng hiện tại suy ra từ hợp đồng hiệu lực (không lưu trùng lặp) |
| 5 | **Quản lý hợp đồng** | Lập / xem / **gia hạn** (có lưu lịch sử) / **thanh lý** · chặn hợp đồng chồng lấn thời gian cùng phòng · cảnh báo sắp hết hạn 30 ngày |
| 6 | **Điện nước** | Ghi chỉ số theo phòng + kỳ (duy nhất) · validate chỉ số mới ≥ chỉ số cũ · tự tính tiền |
| 7 | **Dịch vụ** | Quản lý danh mục (Internet, gửi xe, vệ sinh…) · ghi sử dụng theo kỳ · **lưu đơn giá áp dụng** để hóa đơn lịch sử không bị thay đổi |
| 8 | **Hóa đơn** | Tự gộp tiền phòng + điện + nước + dịch vụ · mỗi phòng chỉ 1 hóa đơn/kỳ |
| 9 | **Thanh toán** | **Một lần duy nhất** mỗi hóa đơn: thiếu → từ chối · đủ → hoàn tất · thừa → hoàn lại phần thừa · ghi bằng transaction |
| 10 | **Trả phòng** | Wizard 7 bước: chốt điện nước → chốt dịch vụ → công nợ → tiền cọc → quyết toán (MIN/MAX) → thanh lý hợp đồng → cập nhật phòng — tất cả trong một transaction |
| 11 | **Thống kê & báo cáo** | 6 báo cáo (phòng, người thuê, hợp đồng, hóa đơn, doanh thu–công nợ, điện nước) · **xuất Excel (.xlsx) và CSV** |

## 🛠 Công nghệ

- **C# / ASP.NET Core MVC 8** — Razor Views
- **Entity Framework Core 8** + **Microsoft SQL Server**
- **ASP.NET Core Identity** — xác thực, hash mật khẩu
- **Bootstrap 5.3.3 + Bootstrap Icons + jQuery** — giao diện tiếng Việt, responsive
- **Chart.js** — biểu đồ dashboard
- **ClosedXML** — xuất Excel

## 🎨 Giao diện

Thiết kế theo phong cách **Modern SaaS Admin Dashboard** với design system thống nhất:

- Design tokens: nền `#F6F8FB`, primary `#2563EB`, font **Inter**
- Sidebar nhóm menu (Tổng quan / Phòng & người / Hợp đồng & tài chính / Báo cáo) với active accent
- **Mobile responsive**: sidebar dạng drawer + backdrop, đóng bằng ESC
- Badge trạng thái, empty state, toast, modal xác nhận, wizard stepper đồng nhất toàn hệ thống

## 🚀 Cài đặt & chạy

### 1. Chuẩn bị

1. **.NET 8 SDK** (hoặc mới hơn): https://dotnet.microsoft.com/download
2. **Microsoft SQL Server** — chọn một trong:
   - [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads) (miễn phí, chọn *Basic*)
   - SQL Server Developer/Full + [SSMS](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)
   - LocalDB (đi kèm Visual Studio)

### 2. Cấu hình connection string

Mở `QuanLyNhaTro/appsettings.json`, sửa `Server=.` cho đúng máy:

| Trường hợp | Connection string |
|---|---|
| SQL Server mặc định | `Server=.;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |
| SQL Server Express ⭐ mặc định | `Server=.\\SQLEXPRESS;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |
| LocalDB (Visual Studio) | `Server=(localdb)\\MSSQLLocalDB;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |

### 3. Tạo database bằng Migration

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

> 💡 Nếu bỏ qua bước này, lần chạy đầu website vẫn **tự động migrate + seed** dữ liệu mẫu (xem `Data/DbInitializer.cs`).

### 4. Chạy website

```bash
cd QuanLyNhaTro
dotnet run
```

Hoặc mở thư mục bằng **Visual Studio 2022** → nhấn **F5**. Truy cập địa chỉ hiển thị (ví dụ `https://localhost:xxxx`) → tự chuyển tới trang **Đăng nhập**.

## 🔑 Tài khoản mặc định

| Tên đăng nhập | Mật khẩu |
|:---:|:---:|
| `chutro` | `Chutro@123` |

> ⚠️ Đây là repo công khai — nếu chạy thật, hãy đổi mật khẩu trong `Data/DbInitializer.cs` trước khi tạo database.

## 🌱 Dữ liệu mẫu (seed tự động)

Lần chạy đầu tự tạo bộ dữ liệu demo liên kết chặt chẽ với nhau:

- **20 phòng** P101–P208: 12 đang thuê · 3 đã cọc · 2 sửa chữa · 3 trống
- **19 người thuê** (CCCD, SĐT, quê quán)
- **17 hợp đồng**: 15 hiệu lực, 2 đã thanh lý (1 có quyết toán trả phòng đầy đủ)
- **Điện nước** 3 kỳ gần nhất cho phòng đang thuê + dữ liệu hợp đồng cũ
- **Dịch vụ**: Internet 70.000đ/tháng · Gửi xe 60.000đ/chiếc · Vệ sinh 30.000đ/người + bản ghi sử dụng
- **Hóa đơn**: 2 kỳ đã thanh toán + kỳ hiện tại còn công nợ
- **Thanh toán**: tiền mặt / chuyển khoản cho các hóa đơn đã trả
- **Quyết toán trả phòng** mẫu (P110 — hoàn cọc, phòng chuyển sửa chữa)

## 🧪 Chế độ demo không cần SQL Server

Chạy ở môi trường **Development** (`dotnet run` mặc định):

```json
// appsettings.Development.json
"Database:Provider": "sqlite"
```

→ Hệ thống dùng **SQLite trong file** `QuanLyNhaTroDemo.db` thay cho SQL Server, tiện demo thử nhanh. Cấu hình mặc định (`appsettings.json`) **vẫn là SQL Server** — khi triển khai thật không cần sửa gì.

## 📐 Nghiệp vụ cốt lõi

Những quy tắc bất di bất dịch của hệ thống:

- **Trạng thái phòng** chỉ gồm: `Trống / Đã cọc / Đang thuê / Đang sửa chữa` — một phòng một trạng thái tại một thời điểm
- **Hợp đồng**: `NgàyKếtThúc > NgàyBắtĐầu` · `GiáThuê > 0` · `TiềnCọc ≥ 0` · không cho 2 hợp đồng hiệu lực chồng thời gian cùng phòng
- **Điện nước**: `ChỉSốMới ≥ ChỉSốCũ` · unique (Phòng + Kỳ)
- **Dịch vụ**: unique (Phòng + Dịch vụ + Kỳ) · lưu `DonGiaApDung` tại thời điểm ghi
- **Hóa đơn**: unique (Phòng + Kỳ) · `TổngTiền = Phòng + Điện + Nước + Dịch vụ`
- **Thanh toán**: mỗi hóa đơn **một lần duy nhất**, không thanh toán một phần
  ```
  Trả thiếu → từ chối, không tạo payment
  Trả đủ   → tạo payment, hóa đơn = Đã thanh toán
  Trả thừa → ghi nhận đúng tổng, hoàn lại phần thừa
  ```
- **Trả phòng**: một lần quyết toán duy nhất, dùng transaction nguyên vẹn:
  ```
  KhoảnPhảiThu = CôngNợ + TiềnPhátSinhCuốiKỳ
  TiềnKhấuTrừCọc = MIN(TiềnCọc, KhoảnPhảiThu)
  TiềnHoànCoc    = MAX(0, TiềnCọc − KhoảnPhảiThu)
  TiềnPhảiTrảThêm = MAX(0, KhoảnPhảiThu − TiềnCọc)
  ```
- **Xóa dữ liệu**: không xóa cứng dữ liệu đã có lịch sử nghiệp vụ

**Toàn vẹn dữ liệu** được bảo đảm ở 3 lớp: validation C# (Data Annotations) → kiểm tra nghiệp vụ trong Service → **unique/check constraints** trong SQL Server (Fluent API).

## 📂 Cấu trúc project

```
QuanLyNhaTro/
├── Controllers/        # Account, Dashboard, Rooms, Tenants, Contracts, Utilities,
│                       # Services, Invoices, Payments, MoveOut, Reports, Home
├── Models/             # EF entities + enums (Room, Tenant, Contract, Invoice, Payment,
│                       # MoveOut, UtilityReading, Service, ServiceUsage, ContractHistory…)
├── Data/               # ApplicationDbContext (Fluent API), DbInitializer (migrate + seed)
├── Services/           # Nghiệp vụ: Room, Tenant, Contract, Utility, Invoice,
│                       # Payment, MoveOut, Report, Dashboard, Export
├── ViewModels/         # Form + màn hình tổng hợp
├── Views/              # Razor views — Bootstrap 5, tiếng Việt, responsive
├── Migrations/         # EF Core migrations (InitialCreate)
└── wwwroot/            # css (design system), js (sidebar/toast/confirm)
```

## 🔧 Xử lý sự cố

| Vấn đề | Cách xử lý |
|---|---|
| `Cannot connect to SQL Server` | Kiểm tra service SQL Server đang chạy (`services.msc`); sửa `Server=.` trong `appsettings.json` (thử `.\SQLEXPRESS` hoặc `(localdb)\MSSQLLocalDB`) |
| `dotnet-ef: command not found` | `dotnet tool install --global dotnet-ef` rồi mở lại terminal |
| Login failed cho user Windows | Thêm `TrustServerCertificate=True;Trusted_Connection=True` vào connection string (đã có sẵn mặc định) |
| Muốn reset dữ liệu demo | Xóa file `QuanLyNhaTroDemo.db` (SQLite) hoặc drop database `QuanLyNhaTroDB` (SQL Server) rồi chạy lại — seed tự tạo mới |
| Port bị chiếm | `dotnet run --urls http://localhost:5099` để chọn port khác |

---

<p align="center">
  Dự án học tập — ASP.NET Core MVC + EF Core + SQL Server<br/>
  <sub>Dành riêng cho một chủ trọ · một nhà trọ</sub>
</p>
