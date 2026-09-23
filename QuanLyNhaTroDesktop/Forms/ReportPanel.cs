using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Forms;

/// <summary>
/// Tab "Báo cáo" — gom toàn bộ các màn Reports của bản web vào một chỗ:
/// Danh sách phòng · Người thuê · Hợp đồng · Hóa đơn · Tài chính (doanh thu - công nợ)
/// · Điện nước · Doanh thu theo tháng — kèm lọc theo kỳ/năm/trạng thái và XUẤT EXCEL/CSV.
///
/// GIAO DIỆN: ReportPanel.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// Danh mục báo cáo nằm trong Designer (thuộc tính Items của listReports).
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class ReportPanel : UserControl
{
    private readonly IReportService? _report;
    private readonly IExportService? _export;

    private string _currentReport = "";

    // Xem chú thích ở MainForm._loadLock: cả app dùng chung một DbContext nên hai lần nạp
    // dữ liệu không được chồng lên nhau (chọn báo cáo liên tục, hoặc bấm Xem khi đang tải).
    private readonly SemaphoreSlim _loadLock = new(1, 1);

    /// <summary>
    /// Constructor KHÔNG THAM SỐ: bắt buộc phải có để Visual Studio Designer mở được
    /// (Designer tạo điều khiển trong tiến trình riêng, không có database/DI).
    /// </summary>
    public ReportPanel()
    {
        InitializeComponent();

        if (Dpi.IsDesignTime) return;

        _report = Program.Services.GetRequiredService<IReportService>();
        _export = Program.Services.GetRequiredService<IExportService>();

        // Danh sách năm: sinh theo năm hiện tại nên tạo lúc chạy
        cboNam.Items.Add("(tất cả)");
        for (var y = DateTime.Today.Year; y >= DateTime.Today.Year - 5; y--) cboNam.Items.Add(y.ToString());
        cboNam.SelectedIndex = 0;
        cboTrangThai.SelectedIndex = 0;

        listReports.SelectedIndexChanged += async (_, _) => await LoadReportAsync();
        btnView.Click += async (_, _) => await LoadReportAsync();
        btnExcel.Click += (_, _) => Export("excel");
        btnCsv.Click += (_, _) => Export("csv");

        Load += async (_, _) => await LoadReportAsync();
    }

    private void ShowFilters(bool ky, bool nam, bool trangThai)
    {
        lblTuKy.Visible = txtTuKy.Visible = ky;
        lblDenKy.Visible = txtDenKy.Visible = ky;
        lblNam.Visible = cboNam.Visible = nam;
        lblTrangThai.Visible = cboTrangThai.Visible = trangThai;
    }

    // ---- Dùng cho công cụ kiểm tra tự động (--dump-layout) ----
    internal int ReportCount => listReports.Items.Count;
    internal string ReportName => _currentReport;
    internal int LoadedRows => gridReport.Rows.Count;
    internal string StatusText => lblReportStatus.Text;
    internal void SelectReport(int index) => listReports.SelectedIndex = index;

    /// <summary>Nạp lại báo cáo đang chọn (được MainForm gọi khi mở tab Báo cáo).</summary>
    public async Task LoadReportAsync()
    {
        if (_report == null) return;   // đang ở chế độ Designer

        await _loadLock.WaitAsync();
        try
        {
            _currentReport = listReports.SelectedItem as string ?? listReports.Items[0] as string ?? "";
            lblReportStatus.Text = "Đang tải báo cáo...";

            // Ẩn bảng tổng hợp TRƯỚC khi xoá cột/dòng của nó — xoá cột trong lúc lưới đang vẽ
            // là nguyên nhân lỗi NullReferenceException ở DataGridViewCell.PaintWork.
            gridSummary.Visible = false;
            ResetGrid(gridReport);
            ResetGrid(gridSummary);

            gridReport.SuspendLayout();
            try
            {
                switch (_currentReport)
                {
                    case "Danh sách phòng": await LoadRoomsAsync(); break;
                    case "Người thuê": await LoadTenantsAsync(); break;
                    case "Hợp đồng": await LoadContractsAsync(); break;
                    case "Hóa đơn": await LoadInvoicesAsync(); break;
                    case "Tài chính (doanh thu - công nợ)": await LoadFinancialAsync(); break;
                    case "Điện nước": await LoadUtilitiesAsync(); break;
                    default: await LoadRevenueAsync(); break;
                }
            }
            finally
            {
                gridReport.ResumeLayout(true);
            }
        }
        catch (Exception ex)
        {
            lblReportStatus.Text = "Lỗi tải báo cáo.";
            Msg.Error(this, "Lỗi tải báo cáo: " + ex.Message);
        }
        finally
        {
            _loadLock.Release();
        }
    }

    /// <summary>
    /// Xoá sạch cột + dòng của một lưới một cách AN TOÀN.
    ///
    /// DataGridView rất dễ ném lỗi khi cột bị xoá đúng lúc WinForms vẽ/layout:
    ///   · NullReferenceException tại DataGridViewCell.PaintWork / PaintColumnHeaders
    ///   · ArgumentOutOfRangeException tại DataGridView.UpdateColumnsDisplayedState
    /// Cách phòng: tắt tự động giãn cột, tạm dừng layout, xoá DÒNG trước rồi mới xoá CỘT,
    /// cuối cùng bật lại layout.
    /// </summary>
    private static void ResetGrid(DataGridView grid)
    {
        grid.SuspendLayout();
        try
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.Rows.Clear();
            grid.Columns.Clear();
        }
        finally
        {
            grid.ResumeLayout(true);
        }
    }

    private void Columns(params (string Name, string Header, int Weight)[] cols)
    {
        gridReport.SuspendLayout();
        try
        {
            foreach (var (name, header, weight) in cols)
            {
                gridReport.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = name,
                    HeaderText = header,
                    FillWeight = weight,
                });
            }
            gridReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        finally
        {
            gridReport.ResumeLayout(true);
        }
    }

    // ================== Từng báo cáo ==================

    private async Task LoadRoomsAsync()
    {
        ShowFilters(ky: false, nam: false, trangThai: false);
        Columns(("MaPhong", "Mã phòng", 12), ("TenPhong", "Tên phòng", 20), ("Loai", "Loại phòng", 16),
                ("DienTich", "Diện tích (m²)", 12), ("GiaPhong", "Giá phòng", 18), ("TrangThai", "Trạng thái", 22));

        var data = await _report!.GetRoomReportAsync();
        foreach (var r in data)
        {
            gridReport.Rows.Add(r.MaPhongKyHieu, r.TenPhong, r.LoaiPhong ?? "",
                r.DienTich?.ToString("N1") ?? "", Ui.Money(r.GiaPhong), Ui.RoomStatusText(r.TrangThai));
        }
        lblReportStatus.Text = $"Báo cáo phòng: {data.Count} phòng.";
    }

    private async Task LoadTenantsAsync()
    {
        ShowFilters(ky: false, nam: false, trangThai: false);
        Columns(("HoTen", "Họ tên", 24), ("CCCD", "CCCD", 16), ("SDT", "Điện thoại", 14),
                ("Phong", "Phòng hiện tại", 14), ("TrangThai", "Trạng thái", 16));

        var data = await _report!.GetTenantReportAsync();
        foreach (var t in data)
            gridReport.Rows.Add(t.HoTen, t.CCCD, t.SoDienThoai ?? "", t.PhongHienTai, t.TrangThai);
        lblReportStatus.Text = $"Báo cáo người thuê: {data.Count} người.";
    }

    private async Task LoadContractsAsync()
    {
        ShowFilters(ky: false, nam: false, trangThai: true);
        Columns(("MaHD", "Mã HĐ", 10), ("Phong", "Phòng", 8), ("NguoiThue", "Người thuê", 18),
                ("BatDau", "Bắt đầu", 11), ("KetThuc", "Kết thúc", 11), ("GiaThue", "Giá thuê", 14),
                ("TienCoc", "Tiền cọc", 13), ("TrangThai", "Trạng thái", 15));

        ContractStatus? status = null;
        bool? sapHetHan = null;
        switch (cboTrangThai.SelectedItem as string)
        {
            case "Đang hiệu lực": status = ContractStatus.HieuLuc; break;
            case "Sắp hết hạn": sapHetHan = true; break;
            case "Đã thanh lý": status = ContractStatus.DaThanhLy; break;
        }

        var data = await _report!.GetContractReportAsync(status, sapHetHan);
        foreach (var c in data)
        {
            gridReport.Rows.Add(c.MaHopDongKyHieu, c.Room?.MaPhongKyHieu ?? "", c.Tenant?.HoTen ?? "",
                Ui.Date(c.NgayBatDau), Ui.Date(c.NgayKetThuc), Ui.Money(c.GiaThue), Ui.Money(c.TienCoc),
                Ui.ContractStatusText(c));
        }
        lblReportStatus.Text = $"Báo cáo hợp đồng: {data.Count} hợp đồng.";
    }

    private async Task LoadInvoicesAsync()
    {
        ShowFilters(ky: true, nam: false, trangThai: true);
        Columns(("MaHD", "Mã hóa đơn", 12), ("Phong", "Phòng", 7), ("Ky", "Kỳ", 8),
                ("TienPhong", "Tiền phòng", 11), ("TienDien", "Tiền điện", 10), ("TienNuoc", "Tiền nước", 10),
                ("TienDV", "Dịch vụ", 10), ("Tong", "Tổng tiền", 12), ("Han", "Hạn TT", 10),
                ("TrangThai", "Trạng thái", 13));

        InvoicePaymentStatus? status = (cboTrangThai.SelectedItem as string) switch
        {
            "Chưa thanh toán" => InvoicePaymentStatus.ChuaThanhToan,
            "Đã thanh toán" => InvoicePaymentStatus.DaThanhToan,
            _ => null,
        };

        var data = await _report!.GetInvoiceReportAsync(txtTuKy.Text.Trim(), txtDenKy.Text.Trim(), status);
        foreach (var i in data)
        {
            gridReport.Rows.Add(i.MaHoaDonKyHieu, i.Room?.MaPhongKyHieu ?? "", i.KyHoaDon,
                Ui.Money(i.TienPhong), Ui.Money(i.TienDien), Ui.Money(i.TienNuoc), Ui.Money(i.TienDichVu),
                Ui.Money(i.TongTien), Ui.Date(i.HanThanhToan), Ui.InvoiceStatusText(i));
        }
        lblReportStatus.Text = $"Báo cáo hóa đơn: {data.Count} hóa đơn — tổng {Ui.Money(data.Sum(i => i.TongTien))}.";
    }

    private async Task LoadFinancialAsync()
    {
        ShowFilters(ky: true, nam: true, trangThai: false);
        var tuKy = txtTuKy.Text.Trim();
        var denKy = txtDenKy.Text.Trim();
        var nam = cboNam.SelectedItem as string;

        var summary = await _report!.GetFinancialReportAsync(tuKy, denKy);

        ResetGrid(gridSummary);   // xoá an toàn (lưới đang ẩn nên không bị vẽ giữa chừng)
        gridSummary.SuspendLayout();
        try
        {
            gridSummary.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chỉ tiêu", FillWeight = 60 });
            gridSummary.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giá trị", FillWeight = 40 });
            gridSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridSummary.Rows.Add("Khoảng kỳ", $"{summary.TuKy ?? "(đầu)"} → {summary.DenKy ?? "(cuối)"}");
            gridSummary.Rows.Add("Tổng doanh thu (đã thu)", Ui.Money(summary.TongDoanhThu));
            gridSummary.Rows.Add("Tổng công nợ (chưa thu)", Ui.Money(summary.TongCongNo));
            gridSummary.Rows.Add("Số hóa đơn đã thanh toán", summary.SoHoaDonDaTT.ToString());
            gridSummary.Rows.Add("Số hóa đơn chưa thanh toán", summary.SoHoaDonChuaTT.ToString());
        }
        finally
        {
            gridSummary.ResumeLayout(true);
        }
        gridSummary.Visible = true;

        Columns(("Thang", "Tháng", 20), ("DoanhThu", "Doanh thu", 30), ("CongNo", "Công nợ", 30),
                ("TyLe", "Tỷ lệ thu", 20));

        var data = await _report.GetDoanhThuTheoThangAsync(nam == "(tất cả)" ? null : nam);
        foreach (var d in data)
        {
            var tong = d.DoanhThu + d.CongNo;
            var tyLe = tong > 0 ? d.DoanhThu / tong * 100 : 0;
            gridReport.Rows.Add(d.Thang, Ui.Money(d.DoanhThu), Ui.Money(d.CongNo), $"{tyLe:N0}%");
        }
        lblReportStatus.Text = $"Báo cáo tài chính — {data.Count} tháng, doanh thu kỳ lọc {Ui.Money(summary.TongDoanhThu)}.";
    }

    private async Task LoadUtilitiesAsync()
    {
        ShowFilters(ky: true, nam: false, trangThai: false);
        Columns(("Ky", "Kỳ", 8), ("Phong", "Phòng", 7), ("DienCu", "Điện cũ", 9), ("DienMoi", "Điện mới", 9),
                ("SoDien", "Số điện", 8), ("TienDien", "Tiền điện", 12), ("NuocCu", "Nước cũ", 9),
                ("NuocMoi", "Nước mới", 9), ("SoNuoc", "Số nước", 8), ("TienNuoc", "Tiền nước", 12));

        var data = await _report!.GetUtilityReportAsync(txtTuKy.Text.Trim(), txtDenKy.Text.Trim());
        foreach (var u in data)
        {
            gridReport.Rows.Add(u.KyGhi, u.MaPhong, u.ChiSoDienCu.ToString("N0"), u.ChiSoDienMoi.ToString("N0"),
                u.SoDienSuDung.ToString("N0"), Ui.Money(u.TienDien), u.ChiSoNuocCu.ToString("N0"),
                u.ChiSoNuocMoi.ToString("N0"), u.SoNuocSuDung.ToString("N0"), Ui.Money(u.TienNuoc));
        }
        lblReportStatus.Text = $"Báo cáo điện nước: {data.Count} bản ghi — điện {Ui.Money(data.Sum(u => u.TienDien))}, " +
                       $"nước {Ui.Money(data.Sum(u => u.TienNuoc))}.";
    }

    private async Task LoadRevenueAsync()
    {
        ShowFilters(ky: false, nam: true, trangThai: false);
        Columns(("Thang", "Tháng", 20), ("DoanhThu", "Doanh thu", 30), ("CongNo", "Công nợ", 30),
                ("Tong", "Tổng phát sinh", 20));

        var nam = cboNam.SelectedItem as string;
        var data = await _report!.GetDoanhThuTheoThangAsync(nam == "(tất cả)" ? null : nam);
        foreach (var d in data)
            gridReport.Rows.Add(d.Thang, Ui.Money(d.DoanhThu), Ui.Money(d.CongNo), Ui.Money(d.DoanhThu + d.CongNo));
        lblReportStatus.Text = $"Doanh thu theo tháng: {data.Count} tháng, tổng đã thu {Ui.Money(data.Sum(d => d.DoanhThu))}.";
    }

    // ================== Xuất file ==================

    private void Export(string format)
    {
        if (gridReport.Rows.Count == 0)
        {
            Msg.Warn(this, "Chưa có dữ liệu để xuất. Hãy bấm 'Xem báo cáo' trước.");
            return;
        }

        var headers = gridReport.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText).ToList();
        var rows = gridReport.Rows.Cast<DataGridViewRow>()
            .Where(r => !r.IsNewRow)
            .Select(r => r.Cells.Cast<DataGridViewCell>()
                .Select(c => c.Value is null ? "" : (object)(c.Value.ToString() ?? ""))
                .ToList())
            .ToList();

        var name = "BaoCao_" + _currentReport.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        using var dialog = new SaveFileDialog
        {
            Title = "Lưu báo cáo",
            Filter = format == "csv" ? "Tệp CSV (*.csv)|*.csv" : "Tệp Excel (*.xlsx)|*.xlsx",
            FileName = $"{name}_{DateTime.Now:yyyyMMdd_HHmm}.{(format == "csv" ? "csv" : "xlsx")}",
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            var bytes = format == "csv"
                ? _export!.ExportCsv(_currentReport, headers, rows)
                : _export!.ExportExcel(_currentReport, headers, rows);
            File.WriteAllBytes(dialog.FileName, bytes);
            Msg.Info(this, "Đã xuất báo cáo:\n" + dialog.FileName, "Xuất thành công");
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi xuất báo cáo: " + ex.Message);
        }
    }
}
