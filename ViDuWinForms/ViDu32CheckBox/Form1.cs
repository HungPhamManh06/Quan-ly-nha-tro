using System.Text;

namespace ViDu32CheckBox;

/// <summary>
/// Ví dụ 3.2 (slide 97): Sử dụng CheckBox để hiển thị thông báo danh sách
/// môn học đăng ký khi ấn vào nút "Đăng ký".
/// Nếu chưa chọn học phần nào thì thông báo "Chưa đăng ký học phần nào!".
/// </summary>
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        btnDangKy.Click += BtnDangKy_Click;
    }

    private void BtnDangKy_Click(object? sender, EventArgs e)
    {
        // Gom danh sách học phần đang được chọn (Checked = true)
        var daChon = new List<string>();
        if (chkLapTrinhCSharp.Checked) daChon.Add(chkLapTrinhCSharp.Text);
        if (chkLapTrinhWeb.Checked) daChon.Add(chkLapTrinhWeb.Text);
        if (chkCCNA.Checked) daChon.Add(chkCCNA.Text);
        if (chkPhanTichThietKe.Checked) daChon.Add(chkPhanTichThietKe.Text);
        if (chkHeQuanTriCSDL.Checked) daChon.Add(chkHeQuanTriCSDL.Text);

        if (daChon.Count == 0)
        {
            // Chưa tick học phần nào
            MessageBox.Show("Chưa đăng ký học phần nào!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Hiển thị danh sách học phần đã đăng ký
        var thongBao = new StringBuilder("Các học phần đăng ký:").AppendLine();
        foreach (var mon in daChon)
            thongBao.AppendLine("- " + mon);

        MessageBox.Show(thongBao.ToString(),
            "Đăng ký thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
