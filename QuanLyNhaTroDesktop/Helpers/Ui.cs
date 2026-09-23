using QuanLyNhaTro.Models;

namespace QuanLyNhaTroDesktop.Helpers;

/// <summary>Màu sắc, kiểu chữ, nút bấm và các control dùng chung cho toàn bộ màn hình.</summary>
public static class Ui
{
    public static readonly Color Primary = Color.FromArgb(37, 99, 235);
    public static readonly Color Success = Color.FromArgb(22, 163, 74);
    public static readonly Color Danger = Color.FromArgb(220, 38, 38);
    public static readonly Color Warning = Color.FromArgb(202, 138, 4);
    public static readonly Color Muted = Color.FromArgb(107, 114, 128);
    public static readonly Color Surface = Color.White;
    public static readonly Color HeaderText = Color.FromArgb(31, 41, 55);

    /// <summary>
    /// Chuẩn hoá một form/dialog: TẮT auto-scale (nguồn gốc lỗi cắt chữ ở DPI 125%),
    /// font Segoe UI, kích thước thiết kế (đã nhân theo DPI màn hình).
    /// </summary>
    public static void InitForm(Form f, int clientWidth, int clientHeight, string title)
    {
        f.AutoScaleMode = AutoScaleMode.None;
        f.Font = new Font("Segoe UI", 11F);
        f.Text = title;
        f.StartPosition = FormStartPosition.CenterParent;
        f.FormBorderStyle = FormBorderStyle.FixedDialog;
        f.MaximizeBox = false;
        f.MinimizeBox = false;
        f.ShowInTaskbar = false;
        f.BackColor = Surface;
        f.ClientSize = Dpi.S(clientWidth, clientHeight);
    }

    // ================== Nút bấm ==================

    public static Button Btn(string text) => new() { Text = text, AutoSize = false };

    public static void StylePrimary(Button b) => Style(b, Primary, Color.White);
    public static void StyleSuccess(Button b) => Style(b, Success, Color.White);
    public static void StyleDanger(Button b) => Style(b, Danger, Color.White);
    public static void StyleSecondary(Button b) => Style(b, Color.White, HeaderText);

    private static void Style(Button b, Color back, Color fore)
    {
        b.BackColor = back;
        b.ForeColor = fore;
        b.FlatStyle = FlatStyle.Flat;
        b.FlatAppearance.BorderSize = back == Color.White ? 1 : 0;
        b.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
        b.Cursor = Cursors.Hand;
        b.UseVisualStyleBackColor = false;
    }

    /// <summary>Nút nhỏ dùng trên thanh công cụ của mỗi tab.</summary>
    public static Button ToolButton(string text, Color back, Color fore)
    {
        var b = new Button
        {
            Text = text,
            AutoSize = false,
            Height = Math.Max(Tlp.ButtonHeight(new Font("Segoe UI", 10F)), Dpi.S(36)),
            Width = Tlp.TextWidth(text, new Font("Segoe UI", 10F)) + Dpi.S(34),
            Font = new Font("Segoe UI", 10F),
            BackColor = back,
            ForeColor = fore,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(0, 0, Dpi.S(8), 0),
            Cursor = Cursors.Hand,
            UseVisualStyleBackColor = false,
        };
        b.FlatAppearance.BorderSize = back == Color.White ? 1 : 0;
        b.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
        return b;
    }

    // ================== Lưới dữ liệu ==================

    /// <summary>DataGridView cấu hình sẵn: chỉ đọc, tự dãn cột, xen kẽ màu dòng.</summary>
    public static DataGridView Grid()
    {
        var g = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
            AutoGenerateColumns = true,
            BackgroundColor = Surface,
            BorderStyle = BorderStyle.None,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            EnableHeadersVisualStyles = false,
            AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(249, 250, 251) },
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(243, 244, 246),
                ForeColor = HeaderText,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(243, 244, 246),
            },
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            RowTemplate = { Height = Tlp.LineHeight(new Font("Segoe UI", 10F)) + 8 },
        };
        g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        g.DefaultCellStyle.SelectionForeColor = HeaderText;
        return g;
    }

    // ================== Nhãn / màu ==================

    public static Label SectionTitle(string text) => new()
    {
        Text = text,
        AutoSize = true,
        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
        ForeColor = HeaderText,
        Margin = new Padding(0, 0, 0, 6),
    };

    // ================== Định dạng hiển thị ==================

    public static string Money(decimal v) => v.ToString("N0") + " đ";
    public static string Number(decimal v) => v.ToString("N0");
    public static string Date(DateTime? d) => d?.ToString("dd/MM/yyyy") ?? "";

    public static string RoomStatusText(RoomStatus s) => s switch
    {
        RoomStatus.Trong => "Trống",
        RoomStatus.DaCoc => "Đã cọc",
        RoomStatus.DangThue => "Đang thuê",
        RoomStatus.DangSuaChua => "Đang sửa chữa",
        _ => s.ToString(),
    };

    /// <summary>Màu hiển thị theo trạng thái phòng (dùng cho ô trong lưới).</summary>
    public static Color RoomStatusColor(RoomStatus s) => s switch
    {
        RoomStatus.Trong => Success,
        RoomStatus.DaCoc => Warning,
        RoomStatus.DangThue => Primary,
        _ => Muted,
    };

    public static string InvoiceStatusText(Invoice i) =>
        i.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán";

    public static string ContractStatusText(Contract c)
    {
        if (c.TrangThai == ContractStatus.DaThanhLy) return "Đã thanh lý";
        var today = DateTime.Today;
        if (c.ConHieuLuc(today))
        {
            var days = (c.NgayKetThuc.Date - today).Days;
            return days <= 30 ? $"Sắp hết hạn ({days} ngày)" : "Đang hiệu lực";
        }
        return c.NgayKetThuc < today ? "Hết hạn" : "Chưa bắt đầu";
    }

    public static string PaymentMethodText(PaymentMethod m) => m == PaymentMethod.TienMat ? "Tiền mặt" : "Chuyển khoản";
}

/// <summary>Tiện ích hiển thị hộp thoại thông báo tiếng Việt.</summary>
public static class Msg
{
    /// <summary>Chế độ im lặng — dùng khi kiểm tra tự động (không hiện hộp thoại chặn tiến trình).</summary>
    public static bool Silent { get; set; }

    public static void Info(IWin32Window? owner, string text, string title = "Thông báo")
    {
        if (Silent) { Console.WriteLine($"[INFO] {title}: {text}"); return; }
        MessageBox.Show(owner, text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void Warn(IWin32Window? owner, string text, string title = "Không thể thực hiện")
    {
        if (Silent) { Console.WriteLine($"[WARN] {title}: {text}"); return; }
        MessageBox.Show(owner, text, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void Error(IWin32Window? owner, string text)
    {
        if (Silent) { Console.WriteLine($"[ERROR] {text}"); return; }
        MessageBox.Show(owner, text, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static bool Confirm(IWin32Window? owner, string text, string title = "Xác nhận")
    {
        if (Silent) { Console.WriteLine($"[CONFIRM] {title}: {text}"); return false; }
        return MessageBox.Show(owner, text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    /// <summary>Hiển thị kết quả (ok, message) trả về từ tầng nghiệp vụ.</summary>
    public static void Result(IWin32Window? owner, (bool Ok, string Message) result)
    {
        if (result.Ok) Info(owner, result.Message, "Thành công");
        else Warn(owner, result.Message);
    }
}
