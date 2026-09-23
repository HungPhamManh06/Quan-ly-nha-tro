using System.Reflection;
using System.Text;

namespace QuanLyNhaTroDesktop.Helpers;

/// <summary>
/// Công cụ kiểm tra "đường đi của Visual Studio Designer".
///
/// VÌ SAO CẦN: khi bạn mở một form ở chế độ [Design], Visual Studio KHÔNG chạy ứng dụng —
/// nó tạo form bằng constructor KHÔNG THAM SỐ (parameterless), trong tiến trình riêng
/// (DesignToolsServer.exe) nơi KHÔNG có database, KHÔNG có DI container.
/// Nếu constructor truy cập database → Designer báo lỗi và hiện form TRỐNG.
///
/// Công cụ này làm đúng việc Designer làm: tạo từng form/UserControl qua constructor
/// không tham số trong chế độ "giả lập thiết kế" (không đụng database), rồi:
///   1. kiểm tra cây control có đầy đủ và kích thước có bình thường không;
///   2. IN RA TOÀN BỘ cây giao diện (tên, loại, vị trí, kích thước, chữ, cỡ chữ) —
///      đây chính là những gì bạn phải nhìn thấy trong tab [Design].
///
/// Chạy: QuanLyNhaTroDesktop.exe --designer-check
/// Kết quả: file designer-check.txt cạnh file thực thi.
/// </summary>
public static class DesignerCheck
{
    public static void Run()
    {
        Dpi.ForceDesignTime = true;   // giả lập đúng môi trường Designer (bỏ qua nhân DPI)
        Msg.Silent = true;

        var path = Path.Combine(AppContext.BaseDirectory, "designer-check.txt");
        using var w = new StreamWriter(path, false, Encoding.UTF8);

        w.WriteLine("KIỂM TRA CÁC FORM MỞ ĐƯỢC BẰNG VISUAL STUDIO DESIGNER");
        w.WriteLine("Mỗi mục = 1 form/UserControl dựng bằng constructor KHÔNG THAM SỐ (đúng như Designer làm),");
        w.WriteLine("kèm TOÀN BỘ cây control mà tab [Design] phải hiện ra.");
        w.WriteLine("Đơn vị: đơn vị thiết kế 96 DPI (lúc chạy sẽ được nhân theo tỉ lệ màn hình).");
        w.WriteLine();

        var types = typeof(DesignerCheck).Assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.IsPublic && t.Namespace?.StartsWith("QuanLyNhaTroDesktop") == true)
            .Where(t => typeof(Form).IsAssignableFrom(t) || typeof(UserControl).IsAssignableFrom(t))
            .OrderBy(t => t.Name)
            .ToList();

        var ok = 0;
        var warnings = new List<string>();

        foreach (var type in types)
        {
            var kind = typeof(Form).IsAssignableFrom(type) ? "Form" : "UserControl";
            try
            {
                // Đúng cách Designer tạo đối tượng
                if (Activator.CreateInstance(type) is not Control control)
                {
                    w.WriteLine($"✘ {type.Name} [{kind}] không tạo được đối tượng.");
                    warnings.Add($"{type.Name}: không tạo được đối tượng");
                    continue;
                }

                using (control)
                {
                    // Cho control "hiện" ở chế độ vô hình (Opacity = 0) giống cách Designer dựng:
                    // nhờ vậy Visible của các control con là thật, không bị báo [ẩn] oan.
                    Form? host = null;
                    if (control is Form f)
                    {
                        f.ShowInTaskbar = false;
                        f.StartPosition = FormStartPosition.Manual;
                        f.Location = new Point(0, 0);   // KHÔNG đặt ra ngoài màn hình (sẽ bị lệch DPI)
                        f.Opacity = 0;
                        try { f.Show(); } catch { /* trong chế độ thiết kế có thể không hiện được */ }
                    }
                    else
                    {
                        host = new Form
                        {
                            ClientSize = new Size(Math.Max(control.Width, 400), Math.Max(control.Height, 300)),
                            ShowInTaskbar = false,
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(0, 0),
                            Opacity = 0,
                        };
                        host.Controls.Add(control);
                        control.Dock = DockStyle.Fill;
                        try { host.Show(); } catch { /* bỏ qua */ }
                    }

                    control.CreateControl();      // tạo handle để WinForms tính layout
                    control.PerformLayout();

                    var controls = Walk(control).ToList();
                    var problems = new List<string>();
                    foreach (var c in controls)
                    {
                        if (c == control) continue;
                        if (c.Width <= 1 || c.Height <= 1) problems.Add($"{Name(c)} {c.Width}x{c.Height}");

                        // Thanh cuộn nội bộ của DataGridView/TextBox: framework tự quản lý, bỏ qua
                        if (c is ScrollBar) continue;

                        // Control đang ẩn, hoặc kích thước do máy tự tính (AutoSize) thì
                        // không xét tràn khung: ở đơn vị thiết kế chúng luôn vừa khung.
                        if (!c.Visible || c.AutoSize) continue;

                        // Bỏ qua các khung tự cuộn / tự xuống dòng (AutoScroll, FlowLayoutPanel):
                        // nội dung cao hơn khung là chuyện bình thường, cuộn là xem được
                        var p = c.Parent;
                        var scrolls = p is ScrollableControl { AutoScroll: true };
                        if (p != null && p is not TabControl && !scrolls && p is not FlowLayoutPanel &&
                            (c.Bounds.Right > p.ClientSize.Width + 2 ||
                             c.Bounds.Bottom > p.ClientSize.Height + 2))
                            problems.Add($"{Name(c)} tràn khỏi {Name(p)}");

                        if (IsTextClipped(c, out var need))
                            problems.Add($"{Name(c)} chữ cần {need}px/{c.Width}px");
                    }

                    var state = problems.Count == 0 ? "✔" : "⚠";
                    if (problems.Count == 0) ok++;

                    w.WriteLine($"{state} {type.Name} [{kind}]  {controls.Count - 1} control  " +
                                $"{control.Width}x{control.Height}  \"{control.Text}\"");
                    DumpTree(w, control, 1);
                    w.WriteLine();

                    control.Hide();
                    host?.Hide();
                    host?.Dispose();

                    if (problems.Count > 0)
                    {
                        var list = string.Join(", ", problems.Take(10)) + (problems.Count > 10 ? $" … (+{problems.Count - 10})" : "");
                        w.WriteLine($"    → cần xem lại: {list}");
                        w.WriteLine();
                        warnings.Add($"{type.Name}: {list}");
                    }
                }
            }
            catch (Exception ex)
            {
                w.WriteLine($"✘ {type.Name} [{kind}] LỖI: {ex.GetType().Name}: {ex.Message}");
                w.WriteLine("    → Visual Studio sẽ hiện form TRỐNG. Nguyên nhân thường là constructor");
                w.WriteLine("      truy cập database/Program.Services mà không kiểm tra Dpi.IsDesignTime.");
                if (ex.InnerException != null) w.WriteLine($"      Chi tiết: {ex.InnerException.Message}");
                w.WriteLine();
                warnings.Add($"{type.Name}: {ex.Message}");
            }
        }

        w.WriteLine($"Tổng kết: {types.Count} form/UserControl — {ok} xem được trong Designer, {warnings.Count} có vấn đề.");
        w.Flush();

        Console.WriteLine($"Đã kiểm tra {types.Count} form → {path}");
        Console.WriteLine($"  {ok} xem được trong Designer, {warnings.Count} có vấn đề");
        foreach (var x in warnings) Console.WriteLine("  ! " + x);
    }

    /// <summary>In cây control đúng thứ tự lồng nhau (giống cây trong tab Design).</summary>
    private static void DumpTree(TextWriter w, Control parent, int depth)
    {
        foreach (Control c in parent.Controls)
        {
            var b = c.Bounds;
            var font = c.Font;
            var inherited = c.Parent != null && c.Font.Equals(c.Parent.Font);
            var text = (c.Text ?? "").Replace("\n", " / ").Replace("\r", "");
            if (text.Length > 34) text = text[..34] + "…";

            w.WriteLine($"{new string(' ', depth * 3)}{Name(c),-22} {c.GetType().Name,-20} " +
                        $"@({b.X,4},{b.Y,4}) {b.Width,4}x{b.Height,-4} " +
                        $"{font.Size:0.##}pt{(font.Bold ? " B" : "")}{(inherited ? " (thừa kế)" : "")}" +
                        (text.Length > 0 ? $"  \"{text}\"" : "") +
                        (c.Visible ? "" : "  [ẩn]"));

            if (c.HasChildren) DumpTree(w, c, depth + 1);
        }
    }

    /// <summary>
    /// Nhãn/nút có AutoSize=false mà chữ rộng hơn ô → Designer sẽ hiện chữ bị cắt.
    /// LƯU Ý ĐƠN VỊ: TextRenderer đo theo DPI thật của màn hình (ví dụ 200% → gấp đôi),
    /// còn kích thước control ở đây là đơn vị thiết kế 96 DPI → phải chia lại cho Dpi.F.
    /// </summary>
    private static bool IsTextClipped(Control c, out int need)
    {
        need = 0;
        if (c is not (Label or Button or CheckBox) || c.AutoSize) return false;
        if (string.IsNullOrEmpty(c.Text) || c.Text.Contains('\n')) return false;

        var scale = Dpi.F <= 0 ? 1f : Dpi.F;
        need = (int)Math.Ceiling((TextRenderer.MeasureText(c.Text, c.Font).Width + 4) / scale);
        return need > c.Width;
    }

    private static IEnumerable<Control> Walk(Control root)
    {
        yield return root;
        foreach (Control c in root.Controls)
            foreach (var x in Walk(c))
                yield return x;
    }

    private static string Name(Control c) => string.IsNullOrEmpty(c.Name) ? c.GetType().Name : c.Name;
}
