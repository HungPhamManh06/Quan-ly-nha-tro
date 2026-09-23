using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.ViewModels;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Quy trình trả phòng đầy đủ (tương ứng MoveOut/Index → Details → Settle của bản web),
/// chia thành 3 bước trên 3 tab:
///   Tab 1 — Chốt chỉ số điện nước cuối kỳ (kỳ "TRAPHONG-{mã HĐ}")
///   Tab 2 — Chốt dịch vụ cuối kỳ (ghi nhận dịch vụ đã dùng của kỳ chốt)
///   Tab 3 — Xem trước quyết toán (công nợ + phát sinh → khấu trừ cọc → hoàn cọc/trả thêm)
///           rồi HOÀN TẤT (thanh lý hợp đồng + cập nhật trạng thái phòng).
/// Toàn bộ số tiền được MoveOutService.SettleAsync tính lại từ database, không tin client.
///
/// GIAO DIỆN: MoveOutDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class MoveOutDialog : Form
{
    private readonly IMoveOutService? _moveOuts;
    private readonly IContractService? _contracts;
    private readonly IServiceService? _services;

    private List<Contract> _liquidatable = new();
    private List<ServiceUsage> _usages = new();
    private MoveOutViewModel? _vm;
    private MoveOutPreviewViewModel? _preview;

    private string KyChot => _vm == null ? "" : $"TRAPHONG-{_vm.MaHopDong}";

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public MoveOutDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public MoveOutDialog(int? contractId, IMoveOutService moveOutService,
        IContractService contractService, IServiceService serviceService) : this()
    {
        _moveOuts = moveOutService;
        _contracts = contractService;
        _services = serviceService;

        btnSettle.Enabled = false;   // chỉ bật sau khi đã xem trước quyết toán
        btnClose.DialogResult = DialogResult.Cancel;
        CancelButton = btnClose;

        // Hai luồng này chạy tự động, không do người dùng bấm: khi nạp danh sách hợp đồng thì
        // gán cboContract.DataSource làm sự kiện đổi hợp đồng chạy NGAY → hai truy vấn cùng lúc
        // trên một DbContext. Xếp hàng lại để SQL Server không báo
        // "A second operation was started on this context instance".
        Load += async (_, _) => await RunDbAsync(() => LoadContractsAsync(contractId));
        cboContract.SelectedIndexChanged += async (_, _) => await RunDbAsync(ReloadAsync);
        btnChotDienNuoc.Click += async (_, _) => await ChotDienNuocAsync();
        btnThemDichVu.Click += async (_, _) => await ThemDichVuAsync();
        btnPreview.Click += async (_, _) => await PreviewAsync();
        btnSettle.Click += async (_, _) => await SettleAsync();
    }

    // ================== Nạp dữ liệu ==================

    private readonly SemaphoreSlim _dbLock = new(1, 1);

    /// <summary>Chạy một thao tác database của dialog, không cho chồng lên thao tác khác.</summary>
    private async Task RunDbAsync(Func<Task> action)
    {
        await _dbLock.WaitAsync();
        try { await action(); }
        finally { _dbLock.Release(); }
    }

    private async Task LoadContractsAsync(int? contractId)
    {
        try
        {
            _liquidatable = await _moveOuts!.GetLiquidatableContractsAsync();
            if (_liquidatable.Count == 0)
            {
                Msg.Info(this, "Không có hợp đồng nào cần trả phòng (mọi hợp đồng đã thanh lý hoặc đã quyết toán).");
                Close();
                return;
            }

            cboContract.Items.Clear();
            foreach (var c in _liquidatable)
                cboContract.Items.Add($"{c.MaHopDongKyHieu} — {c.Room?.MaPhongKyHieu} — {c.Tenant?.HoTen}");

            var index = contractId.HasValue ? _liquidatable.FindIndex(c => c.MaHopDong == contractId.Value) : 0;
            cboContract.SelectedIndex = index < 0 ? 0 : index;
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi tải danh sách hợp đồng: " + ex.Message);
        }
    }

    private async Task ReloadAsync()
    {
        if (cboContract.SelectedIndex < 0) return;
        try
        {
            var contract = _liquidatable[cboContract.SelectedIndex];
            _vm = await _moveOuts!.BuildWizardAsync(contract.MaHopDong);
            if (_vm == null)
            {
                Msg.Warn(this, "Không dựng được phiếu trả phòng cho hợp đồng này.");
                return;
            }

            lblInfo.Text =
                $"Phòng           : {_vm.TenPhong}\n" +
                $"Người thuê      : {_vm.TenNguoiThue}\n" +
                $"Thời hạn HĐ     : {_vm.NgayBatDau:dd/MM/yyyy} → {_vm.NgayKetThuc:dd/MM/yyyy}\n" +
                $"Giá thuê / cọc  : {Ui.Money(_vm.GiaThue)}/tháng  —  {Ui.Money(_vm.TienCoc)}\n" +
                $"Công nợ chưa thu: {Ui.Money(_vm.CongNoTruocTraPhong)}\n" +
                (_vm.HoaDonChuaThanhToan.Count > 0
                    ? "  hóa đơn chưa trả: " + string.Join(", ", _vm.HoaDonChuaThanhToan.Select(i => $"{i.KyHoaDon} ({i.TongTien:N0}đ)"))
                    : "  (không có hóa đơn chưa thanh toán)");

            txtDienMoi.Text = _vm.ChiSoDienCu.ToString("0.##");
            txtNuocMoi.Text = _vm.ChiSoNuocCu.ToString("0.##");
            txtGiaDien.Text = _vm.DonGiaDien.ToString("0.##");
            txtGiaNuoc.Text = _vm.DonGiaNuoc.ToString("0.##");
            lblChotInfo.ForeColor = Ui.Muted;
            lblChotInfo.Text = $"Chỉ số cũ: điện {_vm.ChiSoDienCu:N0} kW — nước {_vm.ChiSoNuocCu:N0} m³.\n" +
                               "Nhập chỉ số mới rồi bấm 'Chốt chỉ số điện nước'.";

            txtPhatSinhKhac.Text = "0";
            txtGhiChu.Text = "";
            _preview = null;
            txtPreview.Text = "Chưa xem trước. Bấm '👁 Xem trước quyết toán'.";
            btnSettle.Enabled = false;

            await LoadUsagesAsync(contract.MaPhong);
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi dựng phiếu trả phòng: " + ex.Message);
        }
    }

    private async Task LoadUsagesAsync(int roomId)
    {
        _usages = await _services!.GetUsagesAsync(KyChot, roomId);
        gridUsages.Rows.Clear();
        foreach (var u in _usages)
        {
            gridUsages.Rows.Add(u.Service?.TenDichVu ?? "", u.SoLuong.ToString("0.##"),
                u.Service?.DonViTinh ?? "", Ui.Money(u.DonGiaApDung), Ui.Money(u.ThanhTien));
        }

        lblDichVuTong.Text = _usages.Count == 0
            ? $"Chưa có dịch vụ nào ghi cho kỳ chốt {KyChot}.\nBấm '➕ Thêm dịch vụ cuối kỳ' nếu còn dịch vụ chưa thu tiền."
            : $"Tổng dịch vụ kỳ {KyChot}: {Ui.Money(_usages.Sum(u => u.ThanhTien))} ({_usages.Count} dịch vụ)";
    }

    // ================== Các bước nghiệp vụ ==================

    private async Task ChotDienNuocAsync()
    {
        if (_vm == null) return;
        if (!decimal.TryParse(txtDienMoi.Text, out var dienMoi) || !decimal.TryParse(txtNuocMoi.Text, out var nuocMoi) ||
            !decimal.TryParse(txtGiaDien.Text, out var giaDien) || !decimal.TryParse(txtGiaNuoc.Text, out var giaNuoc))
        {
            Msg.Warn(this, "Chỉ số và đơn giá phải là số.");
            return;
        }
        if (dienMoi < _vm.ChiSoDienCu || nuocMoi < _vm.ChiSoNuocCu)
        {
            Msg.Warn(this, "Chỉ số mới phải lớn hơn hoặc bằng chỉ số cũ.");
            return;
        }

        try
        {
            var result = await _moveOuts!.ChotDienNuocCuoiKyAsync(_vm.MaHopDong, dienMoi, nuocMoi, giaDien, giaNuoc);
            Msg.Result(this, result);
            if (!result.Ok) return;

            _vm.ChiSoDienMoi = dienMoi;
            _vm.ChiSoNuocMoi = nuocMoi;
            _vm.DonGiaDien = giaDien;
            _vm.DonGiaNuoc = giaNuoc;

            var tienDien = Math.Max(0, dienMoi - _vm.ChiSoDienCu) * giaDien;
            var tienNuoc = Math.Max(0, nuocMoi - _vm.ChiSoNuocCu) * giaNuoc;
            lblChotInfo.ForeColor = Ui.Success;
            lblChotInfo.Text = $"✔ Đã chốt kỳ {KyChot}:\n" +
                               $"   điện {_vm.ChiSoDienCu:N0} → {dienMoi:N0} kW = {Ui.Money(tienDien)}\n" +
                               $"   nước {_vm.ChiSoNuocCu:N0} → {nuocMoi:N0} m³ = {Ui.Money(tienNuoc)}";
            _preview = null;
            btnSettle.Enabled = false;
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }

    private async Task ThemDichVuAsync()
    {
        if (_vm == null) return;
        var roomId = _liquidatable[cboContract.SelectedIndex].MaPhong;
        using var dlg = new ServiceUsageDialog(null, _services!, roomId, KyChot);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            await LoadUsagesAsync(roomId);
            _preview = null;
            btnSettle.Enabled = false;
        }
    }

    private async Task PreviewAsync()
    {
        if (_vm == null) return;
        if (!decimal.TryParse(txtDienMoi.Text, out var dienMoi) || !decimal.TryParse(txtNuocMoi.Text, out var nuocMoi) ||
            !decimal.TryParse(txtGiaDien.Text, out var giaDien) || !decimal.TryParse(txtGiaNuoc.Text, out var giaNuoc))
        {
            Msg.Warn(this, "Chỉ số và đơn giá phải là số.");
            return;
        }
        if (dienMoi < _vm.ChiSoDienCu || nuocMoi < _vm.ChiSoNuocCu)
        {
            Msg.Warn(this, "Chỉ số mới phải lớn hơn hoặc bằng chỉ số cũ.");
            return;
        }
        var phatSinhKhac = decimal.TryParse(txtPhatSinhKhac.Text, out var pk) ? pk : 0;

        try
        {
            _vm.ChiSoDienMoi = dienMoi;
            _vm.ChiSoNuocMoi = nuocMoi;
            _vm.DonGiaDien = giaDien;
            _vm.DonGiaNuoc = giaNuoc;
            _vm.NgayTraPhong = dtpNgayTra.Value.Date;
            _vm.TrangThaiPhongSauTra = cboTrangThaiSau.SelectedIndex == 1 ? RoomStatus.DangSuaChua : RoomStatus.Trong;
            _vm.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

            var tienDien = Math.Max(0, dienMoi - _vm.ChiSoDienCu) * giaDien;
            var tienNuoc = Math.Max(0, nuocMoi - _vm.ChiSoNuocCu) * giaNuoc;
            var tienDichVu = _usages.Sum(u => u.ThanhTien);
            _vm.TienPhatSinhCuoiKy = tienDien + tienNuoc + tienDichVu + phatSinhKhac;

            _preview = await _moveOuts!.PreviewAsync(_vm);
            var p = _preview;
            txtPreview.Text =
                $"BẢNG QUYẾT TOÁN TRẢ PHÒNG — kỳ chốt {KyChot}\n" +
                new string('-', 78) + "\n" +
                $"Công nợ trước trả phòng (hóa đơn chưa thu) : {p.CongNoTruocTraPhong,14:N0} đ\n" +
                $"Phát sinh cuối kỳ                           : {p.TienPhatSinhCuoiKy,14:N0} đ\n" +
                $"   · tiền điện (số mới - số cũ)             : {tienDien,14:N0} đ\n" +
                $"   · tiền nước (số mới - số cũ)             : {tienNuoc,14:N0} đ\n" +
                $"   · dịch vụ cuối kỳ ({_usages.Count} dịch vụ)              : {tienDichVu,14:N0} đ\n" +
                $"   · phát sinh khác                          : {phatSinhKhac,14:N0} đ\n" +
                $"KHOẢN PHẢI THU CUỐI CÙNG                    : {p.KhoanPhaiThuCuoiCung,14:N0} đ\n" +
                new string('-', 78) + "\n" +
                $"Tiền cọc ban đầu                            : {p.TienCocBanDau,14:N0} đ\n" +
                $"Khấu trừ cọc (min(cọc, phải thu))           : {p.TienKhauTruCoc,14:N0} đ\n" +
                (p.TienHoanCoc > 0 ? $"HOÀN LẠI CỌC CHO NGƯỜI THUÊ                : {p.TienHoanCoc,14:N0} đ"
                 : p.TienPhaiTraThem > 0 ? $"NGƯỜI THUÊ PHẢI TRẢ THÊM                   : {p.TienPhaiTraThem,14:N0} đ"
                 : "Tiền cọc vừa đủ khấu trừ — hai bên không còn liên quan tài chính.") + "\n" +
                new string('-', 78) + "\n" +
                $"Ngày trả phòng: {_vm.NgayTraPhong:dd/MM/yyyy}   Trạng thái phòng sau trả: {Ui.RoomStatusText(_vm.TrangThaiPhongSauTra)}\n\n" +
                "Bấm '✅ Hoàn tất quyết toán' để thanh lý hợp đồng và cập nhật trạng thái phòng.";

            btnSettle.Enabled = true;
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi xem trước quyết toán: " + ex.Message);
        }
    }

    private async Task SettleAsync()
    {
        if (_vm == null || _preview == null)
        {
            Msg.Info(this, "Hãy bấm '👁 Xem trước quyết toán' trước khi hoàn tất.");
            return;
        }

        var confirm = Msg.Confirm(this,
            $"Xác nhận TRẢ PHÒNG cho phòng {_vm.TenPhong} — {_vm.TenNguoiThue}?\n\n" +
            $"Khoản phải thu cuối cùng: {Ui.Money(_preview.KhoanPhaiThuCuoiCung)}\n" +
            (_preview.TienHoanCoc > 0 ? $"Hoàn lại cọc: {Ui.Money(_preview.TienHoanCoc)}"
                                      : $"Khách trả thêm: {Ui.Money(_preview.TienPhaiTraThem)}") + "\n\n" +
            "Thao tác này sẽ thanh lý hợp đồng và cập nhật trạng thái phòng.",
            "Xác nhận trả phòng");
        if (!confirm) return;

        try
        {
            var result = await _moveOuts!.SettleAsync(_vm);
            Msg.Result(this, result);
            if (result.Ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
