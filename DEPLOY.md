# 🚀 Hướng dẫn đưa web QuanLyNhaTro lên mạng (Render + Microsoft SQL Server)

## Kiến trúc tổng quan

```
┌─────────────────────┐         ┌──────────────────────────┐
│  Người dùng (trình   │  HTTPS  │  Render Web Service      │
│  duyệt)             │ ──────► │  (ASP.NET Core 8, Docker)│
└─────────────────────┘         │  FREE ~ $0/tháng         │
                                └───────────┬──────────────┘
                                            │ TCP 1433 (mã hóa)
                                            ▼
                                ┌──────────────────────────┐
                                │  Azure SQL Database      │
                                │  (Microsoft SQL Server)  │
                                └──────────────────────────┘
```

- **Render**: chạy ứng dụng web (miễn phí, tự deploy mỗi khi bạn push code lên GitHub).
- **Azure SQL Database**: lưu dữ liệu — đây là Microsoft SQL Server "trên mây", **dữ liệu không bao giờ mất khi web ngủ**.
- ❓ **Vì sao không dùng Vercel?** Vercel chỉ chạy web làm bằng Node.js/Next.js/Python…, **không chạy được ứng dụng .NET**. Web .NET cho lên Render. Sau này nếu muốn, bạn vẫn có thể đặt 1 trang giới thiệu tĩnh trên Vercel trỏ về app Render.

---

## Bước 0: Chuẩn bị

1. Tài khoản **GitHub** (push code lên repo).
2. Tài khoản **Render**: đăng ký tại <https://dashboard.render.com> (đăng nhập bằng GitHub là nhanh nhất).
3. Tài khoản **Azure**: đăng ký tại <https://azure.microsoft.com/free> — miễn phí, được tặng $200 tín dụng trong 30 ngày. *(Sinh viên có thể dùng Azure for Students: <https://azure.microsoft.com/free/students> — không cần thẻ tín dụng.)*

---

## Bước 1: Tạo database trên Azure SQL (Microsoft SQL Server)

### 1.1. Tạo SQL Database

1. Đăng nhập <https://portal.azure.com> → ô tìm kiếm gõ **SQL databases** → **+ Create**.
2. Điền thông tin:
   - **Database name**: `QuanLyNhaTroDB`
   - **Server**: bấm *Create new* →
     - **Server name**: đặt tên duy nhất, ví dụ `nhatro-server-2026` (sau này dùng `nhatro-server-2026.database.windows.net`)
     - **Location**: `(Asia Pacific) Southeast Asia` (Singapore — gần Việt Nam nhất)
     - **Authentication method**: *Use SQL authentication*
     - **Server admin login**: ví dụ `nhatroadmin`
     - **Password**: đặt mật khẩu MẠNH (chữ hoa + thường + số + ký tự đặc biệt, ≥ 12 ký tự) — **ghi nhớ mật khẩu này!**
   - **Want to use SQL Elastic Pool?** = No
   - **Compute + storage**: bấm *Configure database* →
     - Chọn tab **Serverless** (tiết kiệm nhất — tự tạm dừng khi không ai dùng, tự bật khi có truy cập)
     - Chọn cấu hình **General Purpose – Serverless, 1 vCore** (thấp nhất). Nếu thấy gói **Free** (free offer) thì chọn luôn — mỗi tháng được miễn phí một lượng giờ tính toán đủ cho mục đích cá nhân.
3. Bấm **Review + create** → **Create** → chờ 2–5 phút.

### 1.2. Mở tường lửa (RẤT QUAN TRỌNG)

Azure SQL mặc định **chặn mọi kết nối từ ngoài** — bao gồm cả server của Render. Cần mở:

1. Vào database vừa tạo → mục **Networking** (menu trái) → tab **Public access**:
   - **Public network access** = *Selected networks* (hoặc Public nếu không thấy)
   - Bấm **+ Add your client IPv4** (để sau này bạn chạy tool từ máy mình được).
   - ✅ Tick **"Allow Azure services and resources to access this server"**.
2. Để server Render kết nối được, thêm rule mở rộng:
   - Bấm **+ Add a firewall rule**:
     - Rule name: `AllowAllForRender`
     - Start IP: `0.0.0.0`
     - End IP: `255.255.255.255`
   - ⚠️ Rule này mở cổng 1433 cho mọi IP — ai biết user/password mới vào được DB. Đây là cách đơn giản nhất cho Render (IP của Render thay đổi liên tục). Hãy dùng mật khẩu DB thật mạnh.
   - Bấm **Save**.

### 1.3. Lấy Connection String

1. Vào database → menu **Overview** → nút **Connection strings** (hoặc menu *Settings → Connection strings*).
2. Chọn tab **ADO.NET (SQL authentication)** — bạn sẽ thấy chuỗi dạng:

```
Server=tcp:nhatro-server-2026.database.windows.net,1433;Initial Catalog=QuanLyNhaTroDB;Persist Security Info=False;User ID=nhatroadmin;Password=<MẬT KHẨU_CỦA_BẠN>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

3. **Copy chuỗi này** và thay `<MẬT KHẨU_CỦA_BẠN>` bằng mật khẩu thật. Sửa thêm: `MultipleActiveResultSets=False` thành `MultipleActiveResultSets=True` (app dùng nhiều truy vấn song song). Giữ nguyên `Encrypt=True`.
4. Lưu chuỗi đã sửa vào nơi an toàn — Bước 3 sẽ dán vào Render.

> 💡 Tên đăng nhập **không được là** `admin`/`sa`/`root` và mật khẩu không được chứa tên đăng nhập — Azure sẽ báo lỗi nếu vi phạm.

---

## Bước 2: Đưa code lên GitHub

```bash
# Trong thư mục dự án (thư mục chứa .gitignore)
git add .
git commit -m "Thêm cấu hình deploy Render + Azure SQL"
git push origin main
```

Kiểm tra: file `QuanLyNhaTro/Dockerfile`, `QuanLyNhaTro/render.yaml` và `DEPLOY.md` phải có trên GitHub. Các file `*.db`, `*.log` đã bị .gitignore loại ra — đúng như vậy.

---

## Bước 3: Tạo Web Service trên Render

1. Đăng nhập <https://dashboard.render.com> → **New +** → **Web Service**.
2. **Connect** repository GitHub `QuanLyNhaTro` (lần đầu bấm *Connect GitHub* để cấp quyền).
3. Điền cấu hình:

| Mục | Giá trị |
|---|---|
| **Name** | `quanlynhatro` (web sẽ có địa chỉ `https://quanlynhatro.onrender.com`) |
| **Language / Runtime** | `Docker` |
| **Root Directory** | `QuanLyNhaTro` |
| **Instance Type** | `Free` |
| **Health Check Path** | `/` |

4. Mở rộng **Advanced → Add Environment Variable**, thêm 2 biến:

| Key | Value |
|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `ConnectionStrings__DefaultConnection` | *(dán Connection String đã chuẩn bị ở Bước 1.3)* |

   > Render sẽ dùng biến `ConnectionStrings__DefaultConnection` ghi đè lên `appsettings.json` — app đọc được ngay, không cần sửa code.

5. Bấm **Create Web Service** → Render tự build Docker image (lần đầu ~3–5 phút) → tự deploy.

### Cách thay thế (dùng Blueprint, ít bấm hơn)

Nếu đã có file `render.yaml` (trong repo này có sẵn): **New + → Blueprint → chọn repo → Apply**. Render tự tạo service với đúng cấu hình; sau đó bạn chỉ cần vào **Environment** của service dán `ConnectionStrings__DefaultConnection` rồi **Save & Deploy**.

---

## Bước 4: Kiểm tra

1. Mở `https://<tên-service>.onrender.com`.
2. Lần đầu chạy, app **tự động tạo bảng** (chạy migration) và **tự seed dữ liệu mẫu** — xem log tại tab **Logs** của Render, sẽ thấy dòng `Seed hoàn tất...`.
3. Đăng nhập thử: `chutro` / `Chutro@123` → **đổi mật khẩu ngay** sau khi đăng nhập.
4. Kiểm tra dữ liệu có vào SQL thật không: portal Azure → database → **Query editor (preview)** → đăng nhập user DB → chạy `SELECT COUNT(*) FROM Rooms;` (phải thấy ~20 phòng).

---

## Những điều cần biết khi chạy miễn phí

| Vấn đề | Chi tiết |
|---|---|
| **Web "ngủ" trên Render Free** | Sau **15 phút** không có ai truy cập, web tạm dừng. Lượt truy cập sau sẽ **chậm 30–60 giây** để web tự bật lại. **Dữ liệu KHÔNG mất** vì nó nằm ở Azure SQL. Muốn web luôn tỉnh: nâng cấp Instance `$7/tháng`. |
| **Chi phí Azure SQL** | Gói Serverless 1 vCore tự tạm dừng khi không dùng; nếu đăng ký Azure mới/bản sinh viên, tín dụng miễn phí ($200 hoặc hạn mức sinh viên) dùng được nhiều tháng. Khi hết, bảng giá thấp nhất của Serverless 1 vCore khoảng **~$15/tháng** nếu chạy 24/7 (chỉ tính lúc DB "tỉnh"). Theo dõi **Cost Management** trong portal để không bị bất ngờ. |
| **Sao lưu** | Azure SQL tự backup hàng ngày (giữ 7 ngày trở lên) — yên tâm hơn hẳn file .db cục bộ. |
| **HTTPS** | Render tự cấp chứng chỉ SSL cho `*.onrender.com` — trình duyệt sẽ hiện ổ khóa 🔒. |
| **Cập nhật sau này** | Mỗi lần `git push` lên `main`, Render **tự build và deploy lại** — không cần làm gì thêm. |

---

## Xử lý sự cố thường gặp

| Triệu chứng | Nguyên nhân & cách sửa |
|---|---|
| Log Render: `Cannot connect to Server ...` | Quên rule tường lửa `0.0.0.0 – 255.255.255.255` (Bước 1.2), hoặc connection string sai mật khẩu/không có `Encrypt=True`. |
| Lỗi `Login failed for user` | Sai `User ID`/`Password` trong connection string (dấu `<...>` chưa thay, mật khẩu có ký tự `;` phải bọc trong `'...'`). |
| Lỗi `Win32Exception: The certificate chain was not able to be built` | Thêm `TrustServerCertificate=True` vào cuối connection string. |
| Web bị 404 sau khi đổi tên service | Cập nhật lại URL mới trong ý tưởng liên kết ngoài (nếu có). |
| Muốn seed lại dữ liệu mẫu từ đầu | Portal Azure → database → xóa database và tạo lại (app tự seed lần chạy đầu). Hoặc chạy lệnh SQL `DELETE FROM __EFMigrationsHistory; DROP DATABASE...` nếu am hiểu. |
| Build Docker thất bại | Kiểm tra tab **Events/Logs** — thường do quên `git push` file `Dockerfile`. |

---

## Tóm tắt nhanh (dành cho người bận rộn)

1. **Azure**: tạo SQL Database (Serverless 1 vCore) → mở firewall `0.0.0.0–255.255.255.255` → copy ADO.NET connection string (sửa mật khẩu + `MultipleActiveResultSets=True`).
2. **GitHub**: push toàn bộ code.
3. **Render**: New Web Service → Docker → Root Directory `QuanLyNhaTro` → thêm biến môi trường `ASPNETCORE_ENVIRONMENT=Production` và `ConnectionStrings__DefaultConnection=<chuỗi ở trên>` → Deploy.
4. Mở web, đăng nhập `chutro` / `Chutro@123`, đổi mật khẩu. Xong! 🎉
