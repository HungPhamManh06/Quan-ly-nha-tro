using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog nhỏ nhập một đoạn ghi chú (lý do thanh lý, ghi chú trả phòng...).
/// GIAO DIỆN: NoteDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// </summary>
public sealed partial class NoteDialog : Form
{
    private readonly bool _required;

    /// <summary>
    /// Constructor KHÔNG THAM SỐ: bắt buộc phải có để Visual Studio Designer mở được form này
    /// (Designer tạo form trong tiến trình riêng, không có database).
    /// </summary>
    public NoteDialog()
        : this("Ghi chú", "Nhập nội dung ghi chú:", "", required: false) { }

    private NoteDialog(string title, string prompt, string defaultText, bool required)
    {
        _required = required;
        InitializeComponent();
        Dpi.ScaleForm(this);

        Text = title;
        lblPrompt.Text = prompt;
        _text.Text = defaultText;

        AcceptButton = btnOk;
        CancelButton = btnCancel;

        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        btnOk.Click += (_, _) =>
        {
            if (_required && string.IsNullOrWhiteSpace(_text.Text))
            {
                Msg.Warn(this, "Vui lòng nhập nội dung.");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        };
    }

    /// <summary>Mở dialog nhập ghi chú. Trả về null nếu người dùng hủy.</summary>
    public static string? Open(IWin32Window? owner, string title, string prompt,
        string defaultText = "", bool required = false)
    {
        using var dlg = new NoteDialog(title, prompt, defaultText, required);
        return dlg.ShowDialog(owner) == DialogResult.OK ? dlg._text.Text.Trim() : null;
    }
}
