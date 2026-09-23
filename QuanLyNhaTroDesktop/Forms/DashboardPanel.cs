using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.ViewModels;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Forms;

/// <summary>
/// Tab "Tổng quan" — tương đương Dashboard/Index của bản web:
/// thẻ số liệu (phòng, hợp đồng, doanh thu, công nợ) + biểu đồ doanh thu/công nợ 12 tháng
/// + biểu đồ trạng thái phòng + danh sách "Cần xử lý".
///
/// GIAO DIỆN: DashboardPanel.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ (nạp số liệu vào các thẻ/biểu đồ/bảng).
/// </summary>
public partial class DashboardPanel : UserControl
{
    private readonly IDashboardService? _dashboard;

    /// <summary>
    /// Constructor KHÔNG THAM SỐ: bắt buộc phải có để Visual Studio Designer mở được
    /// (Designer tạo điều khiển trong tiến trình riêng, không có database/DI).
    /// </summary>
    public DashboardPanel()
    {
        InitializeComponent();

        if (Dpi.IsDesignTime) return;

        _dashboard = Program.Services.GetRequiredService<IDashboardService>();
        btnRefresh.Click += async (_, _) => await LoadDataAsync();
    }

    public async Task LoadDataAsync()
    {
        if (_dashboard == null) return;   // đang ở chế độ Designer

        try
        {
            lblStatus.Text = "Đang tải số liệu...";
            var vm = await _dashboard.GetDashboardAsync();

            cards.Controls.Clear();
            cards.Controls.Add(Card("Tổng số phòng", vm.TongSoPhong.ToString(), Ui.Primary));
            cards.Controls.Add(Card("Đang thuê", vm.PhongDangThue.ToString(), Ui.Success));
            cards.Controls.Add(Card("Trống", vm.PhongTrong.ToString(), Ui.Muted));
            cards.Controls.Add(Card("Đã cọc", vm.PhongDaCoc.ToString(), Ui.Warning));
            cards.Controls.Add(Card("Hợp đồng hiệu lực", vm.HopDongHieuLuc.ToString(), Ui.Primary));
            cards.Controls.Add(Card("Sắp hết hạn (≤30 ngày)", vm.HopDongSapHetHan.ToString(), Ui.Warning));
            cards.Controls.Add(Card("Hóa đơn chưa thu", vm.HoaDonChuaThanhToan.ToString(), Ui.Danger));
            cards.Controls.Add(Card("Doanh thu đã thu", Ui.Money(vm.DoanhThu), Ui.Success));
            cards.Controls.Add(Card("Công nợ chưa thu", Ui.Money(vm.CongNo), Ui.Danger));

            chart.SetData(vm.DoanhThuChart
                .Select(d => new ChartItemViewModel(Thang(d.Label), d.GiaTri1,
                    vm.CongNoChart.FirstOrDefault(c => c.Label == d.Label)?.GiaTri1 ?? 0))
                .ToList());

            roomChart.SetData(vm.TrangThaiPhongChart);

            gridAttention.Rows.Clear();
            foreach (var item in vm.CanXuLy)
            {
                var loai = item.Loai switch
                {
                    "hoadon" => "Hóa đơn",
                    "hopdong" => "Hợp đồng",
                    _ => "Phòng",
                };
                gridAttention.Rows.Add(loai, item.TieuDe, item.MoTa,
                    item.SoTien > 0 ? Ui.Money(item.SoTien) : (item.TrangThai ?? ""));
            }

            lblStatus.Text = $"Cập nhật lúc {DateTime.Now:HH:mm:ss} — {vm.CanXuLy.Count} việc cần xử lý";
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Lỗi tải số liệu.";
            Msg.Error(this, "Lỗi tải tổng quan: " + ex.Message);
        }
    }

    private string Thang(string ky) =>
        DateTime.TryParse(ky + "-01", out var d) ? d.ToString("MM/yy") : ky;

    /// <summary>Một thẻ số liệu (tạo lúc chạy vì số thẻ phụ thuộc dữ liệu).</summary>
    private Panel Card(string title, string value, Color accent)
    {
        var card = new Panel
        {
            Width = Dpi.S(224),
            Height = Dpi.S(90),
            BackColor = Color.White,
            Margin = new Padding(0, 0, Dpi.S(10), 0),
            Padding = new Padding(Dpi.S(14), Dpi.S(10), Dpi.S(10), Dpi.S(8)),
        };
        card.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(229, 231, 235));
            e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            using var brush = new SolidBrush(accent);
            e.Graphics.FillRectangle(brush, 0, 0, Dpi.S(5), card.Height);
        };

        var lblTitle = new Label
        {
            Text = title,
            Dock = DockStyle.Top,
            Height = Dpi.S(24),
            ForeColor = Ui.Muted,
            Font = new Font("Segoe UI", 9.5F),
        };
        var lblValue = new Label
        {
            Text = value,
            Dock = DockStyle.Fill,
            ForeColor = accent,
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            AutoEllipsis = true,
        };
        card.Controls.Add(lblValue);
        card.Controls.Add(lblTitle);
        return card;
    }
}
