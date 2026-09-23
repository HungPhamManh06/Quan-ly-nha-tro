using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Forms;

/// <summary>
/// Cửa sổ đăng nhập (xác thực tài khoản chủ trọ — seed sẵn: chutro / Chutro@123).
///
/// GIAO DIỆN nằm trong LoginForm.Designer.cs → mở được bằng Visual Studio Designer
/// (bấm đúp file trong Solution Explorer hoặc Shift+F7) để xem/kéo thả/chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class LoginForm : Form
{
    private int _failedAttempts;

    public LoginForm()
    {
        InitializeComponent();

        // Nhân kích thước theo tỉ lệ màn hình (100% / 125% / 150% / 200%)
        Dpi.ScaleForm(this);

        AcceptButton = btnLogin;     // Enter = Đăng nhập
        CancelButton = btnCancel;    // Esc = Thoát

        btnLogin.Click += BtnLogin_Click;
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
    }

    private async void BtnLogin_Click(object? sender, EventArgs e)
    {
        var userName = txtUserName.Text.Trim();
        var password = txtPassword.Text;

        if (userName.Length == 0 || password.Length == 0)
        {
            lblError.Text = "Vui lòng nhập tên đăng nhập và mật khẩu.";
            return;
        }

        btnLogin.Enabled = false;
        lblError.ForeColor = Ui.Muted;
        lblError.Text = "Đang kiểm tra...";

        try
        {
            var ok = await PasswordHelper.CheckPasswordAsync(userName, password);
            if (ok) { DialogResult = DialogResult.OK; Close(); return; }

            _failedAttempts++;
            lblError.ForeColor = Ui.Danger;
            lblError.Text = $"Sai tên đăng nhập hoặc mật khẩu! (lần {_failedAttempts})";
            txtPassword.Clear();
            txtPassword.Focus();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Ui.Danger;
            lblError.Text = "Lỗi: " + ex.Message;
        }
        finally
        {
            btnLogin.Enabled = true;
        }
    }
}
