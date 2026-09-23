using System.Text;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Dialogs;
using QuanLyNhaTroDesktop.Forms;

namespace QuanLyNhaTroDesktop.Helpers;

/// <summary>
/// Công cụ kiểm tra layout: dựng từng cửa sổ (ngoài màn hình) rồi ghi ra file toạ độ và
/// kích thước THẬT của mọi control. Dùng để xác minh không còn control nào bị cao/rộng 0
/// hay nằm ngoài vùng nhìn — nguyên nhân lỗi "mất ô nhập" của các phiên bản trước.
///
/// Chạy: QuanLyNhaTroDesktop.exe --dump-layout
/// Kết quả: file layout-dump.txt cạnh file thực thi.
/// </summary>
public static class LayoutDump
{
    public static void RunAll()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "layout-dump.txt");
        Msg.Silent = true;   // không hiện hộp thoại chặn tiến trình

        using var w = new StreamWriter(path, false, Encoding.UTF8);
        w.WriteLine($"DPI scale         : {Dpi.F:0.##}   (1.0 = 100%, 1.25 = 125%, 1.5 = 150%)");
        w.WriteLine($"Primary screen    : {Screen.PrimaryScreen?.Bounds.Width} x {Screen.PrimaryScreen?.Bounds.Height}");
        w.WriteLine();

        // Mỗi cửa sổ được cấp service MỚI (mỗi service có DbContext riêng). Nếu dùng chung một
        // instance cho nhiều cửa sổ thì cửa sổ trước còn đang nạp dữ liệu mà cửa sổ sau đã mở
        // sẽ va chạm DbContext:"A second operation was started on this context instance".
        T Svc<T>() where T : notnull => Program.Services.GetRequiredService<T>();

        DumpForm(w, () => new LoginForm(), "LoginForm (Đăng nhập)");

        DumpForm(w, () => new RoomDialog(null), "RoomDialog (Thêm phòng)");
        DumpForm(w, () => new TenantDialog(null), "TenantDialog (Thêm người thuê)");
        DumpForm(w, () => new ContractDialog(null, Svc<IContractService>(), Svc<IRoomService>(), Svc<ITenantService>()), "ContractDialog (Lập hợp đồng)");
        DumpForm(w, () => new UtilityDialog(null, Svc<IUtilityService>(), Svc<IRoomService>()), "UtilityDialog (Ghi điện nước)");
        DumpForm(w, () => new ServiceDialog(null, Svc<IServiceService>()), "ServiceDialog (Thêm dịch vụ)");
        DumpForm(w, () => new ServiceUsageDialog(null, Svc<IServiceService>()), "ServiceUsageDialog (Ghi dịch vụ)");
        DumpForm(w, () => new InvoiceDialog(null, Svc<IInvoiceService>()), "InvoiceDialog (Lập hóa đơn)");
        DumpForm(w, () => new PaymentDialog(Svc<IPaymentService>(), new Invoice
        {
            MaHoaDon = 0,
            MaHoaDonKyHieu = "HD-KIEM-TRA",
            KyHoaDon = DateTime.Today.ToString("yyyy-MM"),
            TongTien = 1_500_000,
        }), "PaymentDialog (Thanh toán)");
        DumpForm(w, () => new MoveOutDialog(null, Svc<IMoveOutService>(), Svc<IContractService>(), Svc<IServiceService>()), "MoveOutDialog (Trả phòng)");

        var firstContract = Svc<IContractService>().GetAllAsync().GetAwaiter().GetResult().FirstOrDefault();
        if (firstContract != null)
        {
            DumpForm(w, () => new ContractDetailDialog(firstContract.MaHopDong, Svc<IContractService>(), Svc<IMoveOutService>()),
                "ContractDetailDialog (Chi tiết hợp đồng)");
            DumpForm(w, () => new RenewContractDialog(firstContract, Svc<IContractService>()),
                "RenewContractDialog (Gia hạn hợp đồng)");
        }

        DumpReports(w);
        DumpForm(w, () => new MainForm(), "MainForm (Cửa sổ chính)", dumpTabs: true);

        w.Flush();
        Console.WriteLine("Đã ghi " + path);
    }

    /// <summary>Chạy qua cả 7 loại báo cáo để chắc chắn mỗi báo cáo đều nạp được dữ liệu.</summary>
    private static void DumpReports(TextWriter w)
    {
        Form? host = null;
        try
        {
            // LƯU Ý: KHÔNG được đặt form ra ngoài màn hình (ví dụ -6000) — Windows sẽ coi cửa sổ
            // thuộc màn hình 96 DPI và WinForms tự nhân lại kích thước/font, số đo sẽ sai.
            host = new Form { ClientSize = Dpi.S(1200, 800), StartPosition = FormStartPosition.Manual, Location = new Point(0, 0), ShowInTaskbar = false, Opacity = 0 };
            var panel = new ReportPanel { Dock = DockStyle.Fill };
            host.Controls.Add(panel);
            host.Show();
            Pump(800);

            w.WriteLine("===== Báo cáo (7 loại) =====");
            for (var i = 0; i < panel.ReportCount; i++)
            {
                panel.SelectReport(i);
                Pump(2000);   // đủ lâu cho SQL Server nạp xong trước khi chọn báo cáo kế tiếp
                w.WriteLine($"  [{i + 1}] {panel.ReportName,-38} → {panel.LoadedRows,5} dòng | {panel.StatusText}");
            }
            w.WriteLine();
        }
        catch (Exception ex)
        {
            w.WriteLine("  !! Lỗi kiểm tra báo cáo: " + ex.Message);
            w.WriteLine();
        }
        finally
        {
            host?.Hide();
            host?.Dispose();
        }
    }

    /// <summary>
    /// Chạy vòng lặp thông điệp trong <paramref name="ms"/> mili-giây để các tác vụ nạp dữ liệu
    /// (async) của cửa sổ kịp xong trước khi đo và dispose nó. Cần thiết vì cửa sổ dùng chung
    /// một DbContext: hai truy vấn chồng nhau sẽ báo lỗi "second operation was started ...".
    /// </summary>
    private static void Pump(int ms)
    {
        for (var elapsed = 0; elapsed < ms; elapsed += 50)
        {
            Application.DoEvents();
            Thread.Sleep(50);
        }
        Application.DoEvents();
    }

    private static void DumpForm(TextWriter w, Func<Form> factory, string name, bool dumpTabs = false)
    {
        Form? form = null;
        try
        {
            form = factory();
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(0, 0);   // trong màn hình, nhưng trong suốt (Opacity = 0)
            form.Opacity = 0;
            form.Show();
            Pump(1200);   // chờ tác vụ nạp dữ liệu async của cửa sổ xong (SQL Server chậm hơn SQLite)
            form.PerformLayout();
            Application.DoEvents();

            if (dumpTabs && form is MainForm main)
            {
                w.WriteLine($"===== {name}: nạp dữ liệu từng tab =====");
                for (var i = 0; i < main.TabCount; i++)
                {
                    main.SelectTab(i);
                    // Chờ tab nạp XONG ĐÚNG LÚC thay vì đoán thời gian: theo dõi số dòng, dừng khi
                    // ổn định 1,5 giây (tối đa 20 giây). Nhờ vậy kết quả đo là số dòng cuối cùng.
                    var last = -2;
                    var stableMs = 0;
                    for (var waited = 0; waited < 20000; waited += 250)
                    {
                        Pump(250);
                        var count = main.CurrentTabRowCount();
                        stableMs = count == last ? stableMs + 250 : 0;
                        last = count;
                        // Đã nạp xong khi số dòng không đổi 1,5 giây (và đã có dữ liệu),
                        // hoặc khi chờ quá 4 giây (tab rỗng / không phải danh sách).
                        if (stableMs >= 1500 && (count > 0 || waited >= 4000)) break;
                    }
                    w.WriteLine($"  [{i + 1,2}] {main.TabTitleAt(i),-22} → {main.TabSummary()}");
                }
                w.WriteLine();
            }

            Write(w, form, name);
        }
        catch (Exception ex)
        {
            w.WriteLine($"===== {name} =====");
            w.WriteLine("  !! Không dựng được cửa sổ: " + ex.Message);
            w.WriteLine();
        }
        finally
        {
            form?.Hide();
            form?.Dispose();
        }
    }

    private static void Write(TextWriter w, Form form, string name)
    {
        w.WriteLine($"===== {name} | ClientSize={form.ClientSize.Width}x{form.ClientSize.Height} | Text=\"{form.Text}\" =====");
        Dump(w, form, name, 0);
        w.WriteLine();
    }

    private static void Dump(TextWriter w, Control parent, string path, int depth)
    {
        foreach (Control c in parent.Controls)
        {
            var name = string.IsNullOrEmpty(c.Name) ? c.GetType().Name : c.Name;
            var b = c.Bounds;
            var flag = "";

            var visible = c.Visible;
            if (!visible) flag += "  [ẩn]";

            // Control đang ẩn (ví dụ thanh cuộn nội bộ của lưới, lưới phụ của Báo cáo) thì
            // kích thước/vị trí không ảnh hưởng hiển thị → bỏ qua khi xét cảnh báo.
            if (!visible) goto print;

            if (c is TextBox or ComboBox or DateTimePicker or Button or NumericUpDown)
            {
                if (b.Width < 12 || b.Height < 12) flag += "  <<< KÍCH THƯỚC BẤT THƯỜNG";
                else if (b.Height < 26 && c is TextBox) flag += "  <<< THẤP HƠN 1 DÒNG CHỮ";
            }

            // Khung tự cuộn / tự xuống dòng thì nội dung cao hơn khung là bình thường
            var scrolls = parent is ScrollableControl { AutoScroll: true };
            if (!scrolls && parent is not FlowLayoutPanel && parent is not TabControl &&
                (b.Right > parent.ClientSize.Width + 2 || b.Bottom > parent.ClientSize.Height + 2))
                flag += "  <<< TRÀN KHỎI KHUNG CHA";

        print:

            var text = (c.Text ?? "").Replace("\n", " / ").Replace("\r", "");
            if (text.Length > 30) text = text[..30] + "…";

            w.WriteLine($"{new string(' ', depth * 2)}{name} [{c.GetType().Name}] " +
                        $"@({b.X},{b.Y}) {b.Width}x{b.Height}" +
                        (text.Length > 0 ? $"  \"{text}\"" : "") + flag);

            if (c.HasChildren) Dump(w, c, path + "/" + name, depth + 1);
        }
    }
}
