using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Dialogs;
using QuanLyNhaTroDesktop.Forms;

namespace QuanLyNhaTroDesktop.Helpers;

/// <summary>
/// Chạy THỬ RIÊNG một cửa sổ — bỏ qua màn hình Đăng nhập và Cửa sổ chính.
/// Dùng khi muốn test giao diện/nghiệp vụ của đúng 1 chức năng.
///
/// Chạy:
///   QuanLyNhaTroDesktop.exe --form traphong         (mở thẳng cửa sổ Trả phòng)
///   QuanLyNhaTroDesktop.exe --form MoveOutDialog    (tên class cũng được)
///   QuanLyNhaTroDesktop.exe --form list             (liệt kê tên các cửa sổ)
///
/// Trong Visual Studio: chuột phải project → Properties → Debug →
/// Command line arguments: ghi `--form traphong` → F5.
///
/// Trả về true nếu lệnh có --form (đã xử lý xong), false nếu không có.
/// </summary>
public static class TestForm
{
    public static bool Run(string[] args)
    {
        var i = Array.IndexOf(args, "--form");
        if (i < 0) return false;

        var name = i + 1 < args.Length ? args[i + 1] : "";

        // Danh sách cửa sổ test được: (tên gọi tắt + tên class, tiêu đề, cách dựng)
        var rooms = Program.Services.GetRequiredService<IRoomService>();
        var tenants = Program.Services.GetRequiredService<ITenantService>();
        var contracts = Program.Services.GetRequiredService<IContractService>();
        var utilities = Program.Services.GetRequiredService<IUtilityService>();
        var services = Program.Services.GetRequiredService<IServiceService>();
        var invoices = Program.Services.GetRequiredService<IInvoiceService>();
        var payments = Program.Services.GetRequiredService<IPaymentService>();
        var moveOuts = Program.Services.GetRequiredService<IMoveOutService>();

        Contract? FirstContract() => contracts.GetAllAsync().GetAwaiter().GetResult().FirstOrDefault();

        var demoInvoice = new Invoice
        {
            MaHoaDon = 0,
            MaHoaDonKyHieu = "HD-THU-NGHIEM",
            KyHoaDon = DateTime.Today.ToString("yyyy-MM"),
            TongTien = 1_500_000,
        };

        var entries = new List<(string[] Keys, string Title, Func<Form> Make)>
        {
            (new[] { "traphong", "moveout", "moveoutdialog" }, "Trả phòng (quyết toán)",
                () => new MoveOutDialog(null, moveOuts, contracts, services)),
            (new[] { "phong", "room", "roomdialog" }, "Thêm / sửa phòng",
                () => new RoomDialog(null)),
            (new[] { "nguoithue", "tenant", "tenantdialog" }, "Thêm người thuê",
                () => new TenantDialog(null)),
            (new[] { "hopdong", "contract", "contractdialog" }, "Lập hợp đồng",
                () => new ContractDialog(null, contracts, rooms, tenants)),
            (new[] { "chitiethopdong", "contractdetail", "contractdetaildialog" }, "Chi tiết hợp đồng",
                () => FirstContract() is { } c
                    ? new ContractDetailDialog(c.MaHopDong, contracts, moveOuts)
                    : new InfoDialog("Thông báo", "Chưa có hợp đồng nào để xem chi tiết.")),
            (new[] { "giahan", "renew", "renewcontractdialog" }, "Gia hạn hợp đồng",
                () => FirstContract() is { } c
                    ? new RenewContractDialog(c, contracts)
                    : new InfoDialog("Thông báo", "Chưa có hợp đồng nào để gia hạn.")),
            (new[] { "diennuoc", "utility", "utilitydialog" }, "Ghi điện nước",
                () => new UtilityDialog(null, utilities, rooms)),
            (new[] { "dichvu", "service", "servicedialog" }, "Thêm dịch vụ",
                () => new ServiceDialog(null, services)),
            (new[] { "sudungdichvu", "usage", "serviceusagedialog" }, "Ghi sử dụng dịch vụ",
                () => new ServiceUsageDialog(null, services)),
            (new[] { "hoadon", "invoice", "invoicedialog" }, "Lập hóa đơn",
                () => new InvoiceDialog(null, invoices)),
            (new[] { "thanhtoan", "payment", "paymentdialog" }, "Thanh toán hóa đơn",
                () => new PaymentDialog(payments, demoInvoice)),
            (new[] { "ghichu", "note", "notedialog" }, "Hộp thoại ghi chú",
                () => new NoteDialog()),
            (new[] { "xemchitiet", "info", "infodialog" }, "Hộp thoại xem chi tiết",
                () => new InfoDialog()),
            (new[] { "dangnhap", "login", "loginform" }, "Đăng nhập",
                () => new LoginForm()),
            (new[] { "cuanhachinh", "main", "mainform" }, "Cửa sổ chính (11 tab)",
                () => new MainForm()),
        };

        // --form list  →  liệt kê
        if (string.IsNullOrWhiteSpace(name) ||
            name.Equals("list", StringComparison.OrdinalIgnoreCase) ||
            name.Equals("?", StringComparison.OrdinalIgnoreCase))
        {
            PrintList(entries);
            return true;
        }

        var found = entries.FirstOrDefault(e =>
            e.Keys.Any(k => k.Equals(name, StringComparison.OrdinalIgnoreCase)));

        if (found.Make == null)
        {
            Console.WriteLine($"Không tìm thấy cửa sổ \"{name}\". Danh sách có sẵn:");
            PrintList(entries);
            return true;
        }

        try
        {
            Console.WriteLine($"Mở thử riêng: {found.Title}");
            using var form = found.Make();
            form.ShowDialog();          // đóng cửa sổ này = app thoát
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LỖI khi mở {found.Title}: {ex.Message}");
            if (ex.InnerException != null) Console.WriteLine("  " + ex.InnerException.Message);
        }
        return true;
    }

    private static void PrintList(List<(string[] Keys, string Title, Func<Form> Make)> entries)
    {
        Console.WriteLine();
        Console.WriteLine("  {0,-22} {1}", "TÊN GỌI (--form ...)", "CỬA SỐ");
        Console.WriteLine("  " + new string('-', 60));
        foreach (var (keys, title, _) in entries)
            Console.WriteLine("  {0,-22} {1}", string.Join(" / ", keys.Take(2)), title);
        Console.WriteLine();
        Console.WriteLine("  Ví dụ: QuanLyNhaTroDesktop.exe --form traphong");
    }
}
