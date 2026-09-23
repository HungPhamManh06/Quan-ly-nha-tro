using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Hộp thoại "xem chi tiết" chỉ đọc — dùng cho các màn Details của bản web
/// (chi tiết phòng, người thuê, hóa đơn, thanh toán, trả phòng...).
/// GIAO DIỆN: InfoDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// </summary>
public sealed partial class InfoDialog : Form
{
    /// <summary>
    /// Constructor KHÔNG THAM SỐ: bắt buộc phải có để Visual Studio Designer mở được form này
    /// (Designer tạo form trong tiến trình riêng, không có database).
    /// </summary>
    public InfoDialog()
        : this("Chi tiết", "Nội dung chi tiết sẽ được điền khi chạy.") { }

    public InfoDialog(string title, string content)
    {
        InitializeComponent();
        Dpi.ScaleForm(this);

        Text = title;
        txtContent.Text = content;
        AcceptButton = btnClose;
        CancelButton = btnClose;
    }

    /// <summary>Mở hộp thoại xem chi tiết.</summary>
    public static void Open(IWin32Window? owner, string title, string content)
    {
        using var dlg = new InfoDialog(title, content);
        dlg.ShowDialog(owner);
    }
}
