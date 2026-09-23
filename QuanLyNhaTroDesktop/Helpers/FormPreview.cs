using System.Text;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Dialogs;
using QuanLyNhaTroDesktop.Forms;

namespace QuanLyNhaTroDesktop.Helpers;

/// <summary>
/// Xuất ẢNH XEM TRƯỚC của mọi cửa sổ (đúng như lúc chạy) ra thư mục preview/ kèm trang
/// index.html để mở bằng trình duyệt.
///
/// Dùng khi muốn kiểm tra nhanh giao diện đã thiết kế mà không cần mở từng tab [Design]
/// trong Visual Studio.
///
/// Chạy: QuanLyNhaTroDesktop.exe --preview
/// Kết quả: bin/.../preview/&lt;tên form&gt;.png  và  bin/.../preview/index.html
/// </summary>
public static class FormPreview
{
    public static void RunAll()
    {
        Msg.Silent = true;

        var dir = Path.Combine(AppContext.BaseDirectory, "preview");
        Directory.CreateDirectory(dir);

        // Mỗi cửa sổ được cấp service MỚI (mỗi service có DbContext riêng) để cửa sổ trước còn
        // đang nạp dữ liệu mà cửa sổ sau đã mở cũng không va chạm DbContext.
        T Svc<T>() where T : notnull => Program.Services.GetRequiredService<T>();

        var firstContract = Svc<IContractService>().GetAllAsync().GetAwaiter().GetResult().FirstOrDefault();

        var shots = new List<(string Title, Func<Form> Factory)>
        {
            ("Đăng nhập", () => new LoginForm()),
            ("Thêm / sửa phòng", () => new RoomDialog(null)),
            ("Thêm / sửa người thuê", () => new TenantDialog(null)),
            ("Lập hợp đồng", () => new ContractDialog(null, Svc<IContractService>(), Svc<IRoomService>(), Svc<ITenantService>())),
            ("Ghi điện nước", () => new UtilityDialog(null, Svc<IUtilityService>(), Svc<IRoomService>())),
            ("Thêm dịch vụ", () => new ServiceDialog(null, Svc<IServiceService>())),
            ("Ghi sử dụng dịch vụ", () => new ServiceUsageDialog(null, Svc<IServiceService>())),
            ("Lập hóa đơn", () => new InvoiceDialog(null, Svc<IInvoiceService>())),
            ("Thanh toán", () => new PaymentDialog(Svc<IPaymentService>(), new Invoice
            {
                MaHoaDon = 0,
                MaHoaDonKyHieu = "HD-XEM-TRUOC",
                KyHoaDon = DateTime.Today.ToString("yyyy-MM"),
                TongTien = 1_500_000,
            })),
            ("Trả phòng", () => new MoveOutDialog(null, Svc<IMoveOutService>(), Svc<IContractService>(), Svc<IServiceService>())),
        };

        if (firstContract != null)
        {
            shots.Add(("Chi tiết hợp đồng", () => new ContractDetailDialog(firstContract.MaHopDong, Svc<IContractService>(), Svc<IMoveOutService>())));
            shots.Add(("Gia hạn hợp đồng", () => new RenewContractDialog(firstContract, Svc<IContractService>())));
        }

        shots.Add(("Cửa sổ chính", () => new MainForm()));

        var made = new List<(string Title, string File, int W, int H)>();
        foreach (var (title, factory) in shots)
        {
            var png = Shot(dir, factory, out var w, out var h);
            if (png != null) made.Add((title, png, w, h));
        }

        WriteGallery(dir, made);
        Console.WriteLine($"Đã xuất {made.Count} ảnh xem trước → {Path.Combine(dir, "index.html")}");
    }

    private static string? Shot(string dir, Func<Form> factory, out int w, out int h)
    {
        w = h = 0;
        Form? form = null;
        try
        {
            form = factory();
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(0, 0);
            form.ShowInTaskbar = false;
            form.Show();                 // cần hiện để mọi control vẽ ra được
            for (var elapsed = 0; elapsed < 1200; elapsed += 50)   // chờ nạp xong dữ liệu cho lưới
            {
                Application.DoEvents();  // (SQL Server cần lâu hơn SQLite, 250ms là chưa đủ)
                Thread.Sleep(50);
            }
            Application.DoEvents();
            form.PerformLayout();

            using var bmp = new Bitmap(form.Width, form.Height);
            form.DrawToBitmap(bmp, new Rectangle(0, 0, form.Width, form.Height));

            var file = SafeName(form.Text) + ".png";
            bmp.Save(Path.Combine(dir, file), System.Drawing.Imaging.ImageFormat.Png);

            w = form.Width;
            h = form.Height;
            return file;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ! Không xuất được ảnh: {ex.Message}");
            return null;
        }
        finally
        {
            form?.Hide();
            form?.Dispose();
        }
    }

    private static void WriteGallery(string dir, List<(string Title, string File, int W, int H)> shots)
    {
        var html = new StringBuilder();
        html.AppendLine("<!doctype html><html lang=\"vi\"><head><meta charset=\"utf-8\">");
        html.AppendLine("<title>Xem trước giao diện — Quản lý nhà trọ</title>");
        html.AppendLine("<style>");
        html.AppendLine(" body{font-family:'Segoe UI',sans-serif;background:#f3f4f6;margin:0;padding:24px;color:#1f2937}");
        html.AppendLine(" h1{font-size:20px;margin:0 0 4px}");
        html.AppendLine(" p.sub{color:#6b7280;margin:0 0 20px;font-size:14px}");
        html.AppendLine(" h2{font-size:15px;margin:0 0 6px}");
        html.AppendLine(" figure{background:#fff;border:1px solid #e5e7eb;border-radius:10px;padding:12px;margin:0 0 24px;box-shadow:0 1px 2px rgba(0,0,0,.05)}");
        html.AppendLine(" img{max-width:100%;height:auto;display:block;border:1px solid #e5e7eb;border-radius:6px;background:#fff}");
        html.AppendLine(" figcaption{font-size:12px;color:#6b7280;margin-top:8px}");
        html.AppendLine("</style></head><body>");
        html.AppendLine("<h1>Các giao diện đã thiết kế</h1>");
        html.AppendLine("<p class=\"sub\">Ảnh chụp từ chính ứng dụng lúc chạy (đã nhân theo tỉ lệ màn hình). " +
                        "Ảnh càng lớn = form càng nhiều nội dung.</p>");
        foreach (var (title, file, w, h) in shots)
        {
            html.AppendLine("<figure>");
            html.AppendLine($"  <h2>{title}</h2>");
            html.AppendLine($"  <img src=\"{Uri.EscapeDataString(file)}\" alt=\"{title}\">");
            html.AppendLine($"  <figcaption>{file} — {w} x {h} pixel</figcaption>");
            html.AppendLine("</figure>");
        }
        html.AppendLine("</body></html>");
        File.WriteAllText(Path.Combine(dir, "index.html"), html.ToString(), Encoding.UTF8);
    }

    private static string SafeName(string text)
    {
        var name = new string(text.Select(ch => Path.GetInvalidFileNameChars().Contains(ch) ? '-' : ch).ToArray());
        return name.Trim();
    }
}
