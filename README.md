# 🏠 Quản Lý Nhà Trọ

> Hệ thống quản lý nhà trọ dành riêng cho **Chủ trọ** — quản lý **một nhà trọ duy nhất**: phòng, người thuê, hợp đồng, điện nước, dịch vụ, hóa đơn, thanh toán, trả phòng và báo cáo.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/Database-Microsoft%20SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![EF Core](https://img.shields.io/badge/ORM-Entity%20Framework%20Core%208-512BD4)
![Bootstrap](https://img.shields.io/badge/UI-Bootstrap%205.3-7952B3?logo=bootstrap&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Desktop-Windows%20Forms-012169?logo=windows&logoColor=white)

---

## 📋 Mục lục

- [Tính năng](#-tính-năng)
- [Công nghệ](#-công-nghệ)
- [Giao diện](#-giao-diện)
- [Cài đặt & chạy](#-cài-đặt--chạy)
- [💻 Ứng dụng desktop & sửa giao diện trong Visual Studio](#-ứng-đụng-desktop--sửa-giao-diện-trong-visual-studio)
- [🗄️ Bản desktop kết nối SQL Server](#-bản-desktop-kết-nối-sql-server-từng-bước)
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

Mở `QuanLyNhaTro/appsettings.json` (bản web) **và** `QuanLyNhaTroDesktop/appsettings.json`
(bản desktop) — hai file phải trỏ cùng một database. Chọn đúng instance SQL Server đang cài trên máy:

| Trường hợp | Connection string |
|---|---|
| **SQL Server bản mặc định** ⭐ | `Server=.;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |
| SQL Server Express | `Server=.\SQLEXPRESS;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |
| LocalDB (đi kèm Visual Studio) | `Server=(localdb)\MSSQLLocalDB;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true` |

**Máy bạn đang cài bản SQL Server nào?** Mở **PowerShell** và chạy 2 lệnh:

```powershell
Get-Service | Where-Object { $_.Name -like 'MSSQL*' } | Select-Object Name, Status
Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL'
```

| Kết quả | Kết luận |
|---|---|
| Service tên **`MSSQLSERVER`** (không có dấu `$`) | Bản **mặc định** → dùng `Server=.` |
| Service tên **`MSSQL$SQLEXPRESS`** | Bản **Express** → dùng `Server=.\SQLEXPRESS` |
| Không có service nào, nhưng có Visual Studio | Dùng **LocalDB**: `Server=(localdb)\MSSQLLocalDB` |

Thử kết nối trước khi mở app:

```powershell
sqlcmd -S . -E -C -Q "SELECT @@VERSION"
```

`-E` = đăng nhập bằng tài khoản Windows (không cần user/password). Nếu báo lỗi
*"A network-related or instance-specific error"* hoặc *"Error Locating Server/Instance Specified"* thì
`Server=` đang sai instance — sửa lại theo bảng trên.

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

## 💻 Ứng dụng desktop & sửa giao diện trong Visual Studio

Bản desktop **Windows Forms** dùng chung toàn bộ `Models / Services / Data` với bản web
(thông qua tham chiếu project `QuanLyNhaTro`), nên nghiệp vụ, dữ liệu và tài khoản đăng nhập
**giống hệt** bản web. Giao diện của bản này được thiết kế ngay trong Visual Studio Designer.

### A. Mở solution

1. Bấm đúp **`QuanLyNhaTro.slnx`** ở thư mục gốc dự án (hoặc File → Open → Project/Solution…).
   > Cần Visual Studio 2022 **17.13+** hoặc **Visual Studio 2026** để mở định dạng `.slnx`.
2. Chưa thấy cây file? Bật **View → Solution Explorer** (phím tắt `Ctrl+Alt+L`).
3. Solution gồm 2 project: `QuanLyNhaTro` (bản web) và `QuanLyNhaTroDesktop` (bản desktop).

### B. Chạy bản desktop

1. Chuột phải project **`QuanLyNhaTroDesktop`** → **Set as Startup Project**.
2. Nhấn **F5** → cửa sổ Đăng nhập hiện ra → đăng nhập `chutro` / `Chutro@123`.
3. Lần chạy đầu tiên tự **seed** dữ liệu mẫu (20 phòng, 19 người thuê, 17 hợp đồng, 38 hóa đơn…).

Chạy bằng lệnh thay vì VS:

```bash
dotnet run --project QuanLyNhaTroDesktop/QuanLyNhaTroDesktop.csproj
```

> 💡 Bản desktop mặc định dùng **SQLite** (file `QuanLyNhaTroDesktop.db`) — **không cần cài SQL Server**.
> Muốn dùng SQL Server: sửa `"Database:Provider": "sqlserver"` trong `QuanLyNhaTroDesktop/appsettings.json`.
> Muốn reset dữ liệu demo: đóng app → xóa file `.db` → chạy lại là seed tự tạo mới.

### C. Mở form ở chế độ Design (xem giao diện)

1. Trong Solution Explorer mở rộng **`QuanLyNhaTroDesktop`** → thư mục `Forms` hoặc `Dialogs`.
2. **Bấm đúp file `.cs`** → tab **`[Design]`** hiện ra với giao diện form.
   - Lỡ mở nhầm màn hình code: bấm **`Shift+F7`** (hoặc chuột phải → *View Designer*).
   - Muốn quay lại màn hình code: bấm **`F7`**.

| Thư mục | Các form (nhấp đúp để mở) |
|---|---|
| `Forms/` | `LoginForm` — Đăng nhập · `MainForm` — Cửa sổ chính (menu + 11 tab) · `DashboardPanel` — Tổng quan · `ReportPanel` — Báo cáo · `ListTabPanel` — khung tab dùng chung |
| `Dialogs/` | `RoomDialog` Thêm/sửa phòng · `TenantDialog` Người thuê · `ContractDialog` Lập hợp đồng · `ContractDetailDialog` Chi tiết hợp đồng · `RenewContractDialog` Gia hạn · `UtilityDialog` Điện nước · `ServiceDialog` Dịch vụ · `ServiceUsageDialog` Ghi sử dụng dịch vụ · `InvoiceDialog` Lập hóa đơn · `PaymentDialog` Thanh toán · `MoveOutDialog` Trả phòng · `NoteDialog` Ghi chú · `InfoDialog` Xem chi tiết |

Mỗi form gồm **2 file** — hiểu điểm này là hiểu cách Visual Studio làm việc:

| File | Vai trò | Sửa ở đâu |
|---|---|---|
| `Xxx.Designer.cs` | **Giao diện**: vị trí, kích thước, chữ, màu, (Name) của từng control | Sửa trong tab **`[Design]`** — VS tự ghi lại file này |
| `Xxx.cs` | **Xử lý**: sự kiện nút bấm, gọi service, nạp dữ liệu | Sửa trong màn hình code (`F7`) |

### D. Sửa giao diện trong Designer

| Thao tác | Cách làm |
|---|---|
| Xem/sửa thuộc tính | Chọn control → **`F4`** (cửa sổ Properties): `Text`, `Font`, `ForeColor`, `BackColor`, `Size`, `(Name)`… |
| Xem toàn bộ control theo tên | **`Ctrl+Alt+0`** (Document Outline) — tên có nghĩa như `lblTenPhong`, `btnOk`, `pnlButtons` |
| Thêm control mới | Kéo từ **Toolbox** (cột trái) thả vào form |
| Thêm nút + viết sự kiện | **Nhấp đúp nút** trong Designer → VS tự sinh hàm `btnXxx_Click` trong file `.cs` → viết code xử lý vào đó |
| Xoá control | Chọn rồi nhấn `Delete` |
| Chạy thử | `F5` — app tự nhân kích thước theo tỉ lệ màn hình của bạn |

### E. ⚠️ 3 quy tắc DPI khi sửa (quan trọng)

1. **Kích thước trong Designer là đơn vị thiết kế 96 DPI.** Lúc chạy, app tự nhân theo tỉ lệ
   màn hình (100% / 125% / 150% / 200%) qua `Dpi.ScaleForm` — bạn thiết kế **một lần**, đúng trên mọi máy.
2. **Không đổi `AutoScaleMode`** của form (phải giữ `None`) — đổi là bị nhân kích thước 2 lần, giao diện vỡ.
3. **Cỡ chữ để theo `pt`** — font khai bằng point nên WinForms tự đúng theo DPI, bạn **không** tự nhân.

### F. Công cụ kiểm tra (chạy terminal, không cần mở VS)

```bash
dotnet build QuanLyNhaTro.slnx                       # build trước
cd QuanLyNhaTroDesktop/bin/Debug/net8.0-windows

.\QuanLyNhaTroDesktop.exe --designer-check           # → designer-check.txt: 18 form có mở được trong Designer
.\QuanLyNhaTroDesktop.exe --dump-layout              # → layout-dump.txt: toạ độ/kích thước mọi control
.\QuanLyNhaTroDesktop.exe --preview                  # → preview/index.html: ảnh xem trước 13 cửa sổ
```

Mỗi file `.Designer.cs` đều có chú thích đầu file nhắc lại 3 quy tắc ở mục E.

Khi chạy app mà có lỗi không lường trước, app ghi file **`loi-ung-dung.log`** cùng thư mục file
`.exe` (kèm stack trace đầy đủ) — đây là nơi đầu tiên cần xem khi tìm nguyên nhân.

### G. Chạy THỬ riêng 1 cửa sổ (bỏ qua Đăng nhập)

Khi muốn test riêng đúng 1 chức năng (VD cửa sổ **Trả phòng**) thay vì phải Đăng nhập rồi
mở tab trong Cửa sổ chính:

```bash
cd QuanLyNhaTroDesktop/bin/Debug/net8.0-windows
.\QuanLyNhaTroDesktop.exe --form traphong     # mở thẳng cửa sổ Trả phòng
.\QuanLyNhaTroDesktop.exe --form list         # liệt kê toàn bộ tên cửa sổ test được
```

**Trong Visual Studio — cách nhanh nhất (không cần sửa gì):**

1. Ở thanh công cụ trên, cạnh nút **▶** có dropdown tên profile (mặc định là
   *`QuanLyNhaTroDesktop`*). Bấm dropdown đó.
2. Chọn **`Trả phòng (test riêng)`** → bấm **F5**.

App mở thẳng cửa sổ Trả phòng, **không** hiện Đăng nhập và Cửa sổ chính. Đóng cửa sổ đó = app thoát.
Các profile có sẵn nằm trong `QuanLyNhaTroDesktop/Properties/launchSettings.json`
(`Trả phòng`, `Phòng`, `Hợp đồng`, `Danh sách cửa sổ test được`).
Muốn chạy bình thường trở lại thì chọn lại profile *`QuanLyNhaTroDesktop`*.

Cách khác (không dùng profile): chuột phải project `QuanLyNhaTroDesktop` → **Properties** →
tab **Debug** (mục *General* → **Open debug launch profiles UI**) → điền
*Command line arguments* = `--form traphong` → **F5**.

> 💾 Chế độ chạy thử dùng **đúng database demo** (`QuanLyNhaTroDesktop.db`), nên nếu bạn bấm
> nút xác nhận (VD *Xác nhận trả phòng*) thì dữ liệu mẫu thay đổi thật. Muốn về ban đầu:
> đóng app → xoá file `QuanLyNhaTroDesktop.db` trong thư mục `QuanLyNhaTroDesktop/` → **F5**
> (app tự tạo lại + seed dữ liệu mẫu).

| Tên (viết hoa/thường đều được) | Cửa sổ mở ra |
|---|---|
| `traphong` / `MoveOutDialog` | Trả phòng — quyết toán |
| `phong` / `hopdong` / `diennuoc` / `dichvu` / `hoadon` / `thanhtoan`… | Cửa sổ tương ứng |
| `dangnhap` / `cuanhachinh` | Đăng nhập / Cửa sổ chính |
| `list` | In danh sách đầy đủ ra console |

## 🗄️ Bản desktop kết nối SQL Server (từng bước)

Bản desktop dùng **chung database với bản web** (cùng `Models`, `Data`, `Services`).

### Bước 1 — Chọn nhà cung cấp database

Mở `QuanLyNhaTroDesktop/appsettings.json`:

```json
// Dùng SQL Server (đúng yêu cầu đồ án)
"Database:Provider": "sqlserver",

// Hoặc chạy nhanh mà KHÔNG cần cài SQL Server (dữ liệu lưu trong file QuanLyNhaTroDesktop.db)
// "Database:Provider": "sqlite",
```

### Bước 2 — Trỏ đúng instance (xem bảng ở mục *Cài đặt & chạy → 2. Cấu hình connection string*)

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=QuanLyNhaTroDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

Nếu dùng tài khoản SQL (`sa`) thay vì tài khoản Windows, thay `Trusted_Connection=True` bằng
`User Id=sa;Password=mật_khẩu_của_bạn;`.

### Bước 3 — Chạy app lần đầu: database TỰ TẠO, không phải viết script SQL

Nhấn **F5** trong Visual Studio (hoặc `dotnet run --project QuanLyNhaTroDesktop`). Lần chạy đầu,
`QuanLyNhaTro/Data/DbInitializer.cs` sẽ tự làm hết:

1. Chạy **migration** (`MigrateAsync`) → tạo database `QuanLyNhaTroDB` + **18 bảng** + khoá ngoại + index.
2. **Seed dữ liệu mẫu**: 20 phòng, 19 người thuê, 17 hợp đồng, 38 hoá đơn, 26 phiếu thu,
   1 phiếu quyết toán trả phòng, 39 bản ghi điện nước, 74 lượt dùng dịch vụ, tài khoản `chutro`.

Muốn tạo database trước bằng dòng lệnh:

```bash
dotnet ef database update --project QuanLyNhaTro --startup-project QuanLyNhaTro
```

### Bước 4 — Kiểm tra dữ liệu đã vào SQL Server

| Cách | Thao tác |
|---|---|
| **sqlcmd** | `sqlcmd -S . -E -C -d QuanLyNhaTroDB -Q "SELECT COUNT(*) AS SoPhong FROM Rooms"` |
| **SSMS** | Server name `localhost` (hoặc `.\SQLEXPRESS`) → Windows Authentication → `QuanLyNhaTroDB` → Tables |
| **Ngay trong Visual Studio** | Menu **View → SQL Server Object Explorer** → mở rộng `SQL Server` → `QuanLyNhaTroDB` → chuột phải bảng → *View Data* |

### Bước 5 — Sửa model hoặc xoá dữ liệu

| Muốn gì | Làm thế nào |
|---|---|
| Đổi model (thêm cột/bảng) | `dotnet ef migrations add TenThayDoi --project QuanLyNhaTro --startup-project QuanLyNhaTro` → **F5**, app tự áp dụng migration mới |
| Xoá sạch rồi tạo lại dữ liệu mẫu | `sqlcmd -S . -E -C -Q "ALTER DATABASE QuanLyNhaTroDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE QuanLyNhaTroDB"` → **F5** lại app |
| Xem app đang ghi câu SQL nào | Thêm `--verbose-sql` sau khi chạy: `QuanLyNhaTroDesktop.exe --dump-layout --verbose-sql` |

> 💡 Bản **web** cũng đọc chung chuỗi kết nối này, nhưng khi chạy ở môi trường **Development** thì
> `QuanLyNhaTro/appsettings.Development.json` chuyển sang SQLite. Muốn bản web dùng SQL Server khi
> đang phát triển thì sửa file đó thành `"Database:Provider": "sqlserver"`.

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

```
QuanLyNhaTroDesktop/        # 🖥️ Bản desktop Windows Forms — dùng chung nghiệp vụ bản web
├── Forms/                  # LoginForm, MainForm (kèm *.Designer.cs), DashboardPanel,
│                           # ReportPanel, ListTabPanel
├── Dialogs/                # 13 cửa sổ con: phòng, người thuê, hợp đồng, điện nước,
│                           # dịch vụ, hóa đơn, thanh toán, trả phòng, ghi chú, xem chi tiết
├── Controls/               # BarChart — tự vẽ biểu đồ không cần thư viện ngoài
├── Helpers/                # Dpi (nhân kích thước theo màn hình), Ui/Tlp (control dùng chung),
│                           # DesignerCheck / LayoutDump / FormPreview (3 công cụ kiểm tra)
├── Properties/             # launchSettings.json
├── appsettings.json        # Database:Provider = sqlite | sqlserver
└── QuanLyNhaTroDesktop.csproj   # SubType=Form + ForceDesignerDpiUnaware cho VS Designer
```

## 🔧 Xử lý sự cố

| Vấn đề | Cách xử lý |
|---|---|
| `Cannot connect to SQL Server` | Kiểm tra service SQL Server đang chạy (`services.msc`); sửa `Server=.` trong `appsettings.json` (thử `.\SQLEXPRESS` hoặc `(localdb)\MSSQLLocalDB`) |
| `dotnet-ef: command not found` | `dotnet tool install --global dotnet-ef` rồi mở lại terminal |
| Login failed cho user Windows | Thêm `TrustServerCertificate=True;Trusted_Connection=True` vào connection string (đã có sẵn mặc định) |
| Muốn reset dữ liệu demo | Xóa file `QuanLyNhaTroDemo.db` (SQLite) hoặc drop database `QuanLyNhaTroDB` (SQL Server) rồi chạy lại — seed tự tạo mới |
| Port bị chiếm | `dotnet run --urls http://localhost:5099` để chọn port khác |
| `String or binary data would be truncated` | Ghi chuỗi dài hơn độ dài cột (SQLite bỏ qua nhưng SQL Server chặn). Các cột kỳ (`UtilityReadings.KyGhi`, `ServiceUsages.KySuDung`, `Invoices.KyHoaDon`) đã được mở thành `nvarchar(20)` để chứa kỳ chốt `TRAPHONG-{mã HĐ}` — migration `MoRongKyTraPhong`. Nếu tự thêm cột chuỗi mới, nhớ đặt `[StringLength]` đủ dài |
| `A second operation was started on this context instance` | Hai thao tác cùng dùng **một** `DbContext` chạy chồng nhau (SQLite nhanh nên ít khi lộ, SQL Server thì lộ ngay). Bản desktop đã xử lý: mỗi service/cửa sổ có DbContext riêng (`AddTransient` trong `QuanLyNhaTroDesktop/Program.cs`) + khoá xếp hàng `SemaphoreSlim` trong `MainForm`, `ReportPanel`, `UtilityDialog`, `InvoiceDialog`, `MoveOutDialog`. Khi tự thêm màn hình mới có 2 luồng truy vấn (VD: đổi combo làm nạp lại dữ liệu trong lúc form đang nạp lần đầu), nhớ xếp hàng tương tự |
| Mở cửa sổ con thấy báo lỗi database ngay | Tab danh sách đang nạp dở thì cửa sổ con mở lên cũng truy vấn — `MainForm.WaitForIdleAsync()` đã chờ sẵn; nếu tự viết thêm nút mở cửa sổ, gọi hàm này trước khi `ShowDialog` |
| Hộp thoại .NET *"Object reference not set to an instance of an object"* (chỉ có nút **Continue**) | Đây là lỗi **không bắt được** ở luồng giao diện. Từ giờ app tự **ghi đầy đủ stack trace** ra file `loi-ung-dung.log` **cạnh file `.exe`** (`bin\Debug\net8.0-windows\`) và hiện đường dẫn đó trong hộp thoại — mở file để biết chính xác dòng gây lỗi thay vì phải đoán |
| Lỗi trong `DataGridViewCell.PaintWork` / `DataGridView.UpdateColumnsDisplayedState` | Do **xoá/tạo lại cột** của `DataGridView` đúng lúc WinForms đang vẽ hoặc đang layout. Cách phòng đã áp dụng ở tab Báo cáo (`ReportPanel.ResetGrid`): **ẩn lưới → `SuspendLayout()` → tắt `AutoSizeColumnsMode` → `Rows.Clear()` → `Columns.Clear()` → `ResumeLayout(true)`**. Khi tự thêm lưới mới, dùng lại mẫu này thay vì gọi `Columns.Clear()` trực tiếp |
| **Tab `[Design]` của bản desktop hiện form trống** (khung vuông, không có control) | Đóng hẳn Visual Studio (File → Exit) → xóa thư mục `bin`, `obj`, `.vs` trong repo **và** `%LocalAppData%\Microsoft\VisualStudio\18.0_*\WinFormsDesigner` → mở lại solution → **Build → Rebuild Solution** |
| Dòng vàng *"Scaling on your main display is set to 200%..."* trong Designer | Thuộc tính `ForceDesignerDpiUnaware` đã có sẵn trong `.csproj` — chỉ cần **đóng và mở lại Visual Studio** là tab Design hiển thị đúng đơn vị thiết kế |
| Sửa trong Designer rồi app chạy sai kích thước | Quét lại Properties của form: `AutoScaleMode` phải là **None** (xem mục *3 quy tắc DPI* ở trên) |

---

<p align="center">
  Dự án học tập — ASP.NET Core MVC + EF Core + SQL Server<br/>
  <sub>Dành riêng cho một chủ trọ · một nhà trọ</sub>
</p>
