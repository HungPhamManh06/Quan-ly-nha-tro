using System.ComponentModel;
using System.Reflection;

namespace QuanLyNhaTroDesktop.Helpers;

/// <summary>
/// Tự đo DPI màn hình và nhân kích thước pixel theo đúng tỷ lệ.
/// Dùng thay cho AutoScaleMode của WinForms (bị lỗi trên máy có DPI 125%/150%).
/// </summary>
public static class Dpi
{
    /// <summary>Hệ số scale, ví dụ 1.25 khi màn hình đặt 125%.</summary>
    public static float F { get; private set; } = 1f;

    /// <summary>
    /// TRUE khi form đang được Visual Studio Designer dựng (thiết kế), FALSE khi chạy thật.
    ///
    /// Vì sao cần: Designer KHÔNG chạy ứng dụng — nó dựng form trong tiến trình riêng
    /// (DesignToolsServer.exe). Ở đó không có database, không có DI container. Nếu constructor
    /// của form truy cập database thì Designer báo lỗi và hiện form TRỐNG.
    /// Mọi constructor đều phải kiểm tra cờ này trước khi làm việc gì cần dữ liệu.
    /// </summary>
    public static bool IsDesignTime => ForceDesignTime || DetectedDesignTime;

    /// <summary>Dùng cho công cụ kiểm tra --designer-check để giả lập môi trường Designer.</summary>
    public static bool ForceDesignTime { get; set; }

    private static readonly bool DetectedDesignTime = DetectDesignTime();

    private static bool DetectDesignTime()
    {
        // Cách 1: dịch vụ thiết kế chính thức của WinForms
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;

        // Cách 2: tiến trình chạy form là Visual Studio hoặc Designer out-of-process
        var entry = Assembly.GetEntryAssembly()?.GetName().Name;
        return entry is "devenv" or "DesignToolsServer" or "XDesProc" or "XDesProc64";
    }

    public static void Initialize()
    {
        if (IsDesignTime) return;   // ở Designer: giữ nguyên đơn vị thiết kế 96 DPI
        using var g = Graphics.FromHwnd(IntPtr.Zero);
        F = g.DpiX / 96f;
    }

    public static int S(int v) => (int)Math.Round(v * F);

    public static Size S(int w, int h) => new(S(w), S(h));

    public static Padding SP(int l, int t, int r, int b) => new(S(l), S(t), S(r), S(b));

    /// <summary>
    /// Nhân kích thước toàn bộ cây control theo tỉ lệ màn hình.
    ///
    /// Dùng cho các form có giao diện nằm trong file Designer (đơn vị thiết kế 96 DPI):
    /// thay vì để AutoScaleMode tự nhân (bị lệch trên màn hình 125%/150%/200%),
    /// form gọi Dpi.ScaleForm(this) ngay sau InitializeComponent() → kết quả chính xác,
    /// kiểm chứng được bằng công cụ --dump-layout. Font khai bằng point nên tự đúng theo DPI.
    /// </summary>
    public static void ScaleForm(Form form)
    {
        if (IsDesignTime) return;       // Designer hiển thị đúng đơn vị thiết kế 96 DPI
        if (Math.Abs(F - 1f) < 0.001f) return;
        form.ClientSize = new Size(S(form.ClientSize.Width), S(form.ClientSize.Height));
        ScaleChildren(form);
        form.PerformLayout();
    }

    private static void ScaleChildren(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            c.SuspendLayout();

            if (c is TableLayoutPanel tlp)
            {
                for (var i = 0; i < tlp.RowStyles.Count; i++)
                    if (tlp.RowStyles[i].SizeType == SizeType.Absolute)
                        tlp.RowStyles[i].Height = S((int)tlp.RowStyles[i].Height);
                for (var i = 0; i < tlp.ColumnStyles.Count; i++)
                    if (tlp.ColumnStyles[i].SizeType == SizeType.Absolute)
                        tlp.ColumnStyles[i].Width = S((int)tlp.ColumnStyles[i].Width);
            }

            // ComboBox / DateTimePicker có chiều cao do font quyết định → chỉ nhân bề rộng
            if (c is ComboBox or DateTimePicker)
                c.Width = S(c.Width);
            else if (c is TextBox or NumericUpDown)
            {
                // Ô nhập: nhân vị trí/bề rộng, còn CHIỀU CAO luôn đủ cho font hiện tại
                // (tránh trường hợp chữ bị cắt đáy ở màn hình tỉ lệ cao)
                c.Location = new Point(S(c.Location.X), S(c.Location.Y));
                c.Width = S(c.Width);
                c.AutoSize = false;
                c.Height = Math.Max(c.Font.Height + S(8), S(24));
            }
            else
            {
                c.Location = new Point(S(c.Location.X), S(c.Location.Y));
                c.Size = new Size(S(c.Size.Width), S(c.Size.Height));
            }

            c.Margin = SP(c.Margin.Left, c.Margin.Top, c.Margin.Right, c.Margin.Bottom);
            c.Padding = SP(c.Padding.Left, c.Padding.Top, c.Padding.Right, c.Padding.Bottom);
            if (c.MinimumSize != Size.Empty) c.MinimumSize = S(c.MinimumSize.Width, c.MinimumSize.Height);
            if (c.MaximumSize != Size.Empty) c.MaximumSize = S(c.MaximumSize.Width, c.MaximumSize.Height);

            ScaleChildren(c);
            c.ResumeLayout();
        }
    }
}
