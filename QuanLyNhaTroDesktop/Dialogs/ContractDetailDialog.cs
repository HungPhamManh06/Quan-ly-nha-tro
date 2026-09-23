using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog chi tiết hợp đồng (tương ứng Contracts/Details của bản web) kèm các thao tác
/// nghiệp vụ: GIA HẠN · THANH LÝ · TRẢ PHÒNG (quyết toán). Dữ liệu lấy từ
/// IContractService.GetDetailAsync (đã gồm hóa đơn, lịch sử, phiếu trả phòng).
///
/// GIAO DIỆN: ContractDetailDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class ContractDetailDialog : Form
{
    private readonly IContractService? _contracts;
    private readonly IMoveOutService? _moveOuts;
    private readonly IServiceService? _services;
    private readonly int _id;

    private Contract? _contract;

    /// <summary>True khi có thao tác làm thay đổi dữ liệu (để màn chính tải lại danh sách).</summary>
    public bool Changed { get; private set; }

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public ContractDetailDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public ContractDetailDialog(int contractId, IContractService contractService, IMoveOutService moveOutService) : this()
    {
        _contracts = contractService;
        _moveOuts = moveOutService;
        _services = Program.Services.GetRequiredService<IServiceService>();
        _id = contractId;

        btnClose.DialogResult = DialogResult.Cancel;
        CancelButton = btnClose;

        Load += async (_, _) => await LoadAsync();
        _btnRenew.Click += async (_, _) => await RenewAsync();
        _btnLiquidate.Click += async (_, _) => await LiquidateAsync();
        _btnMoveOut.Click += async (_, _) => await MoveOutAsync();
    }

    private async Task LoadAsync()
    {
        try
        {
            _contract = await _contracts!.GetDetailAsync(_id);
            if (_contract == null)
            {
                Msg.Warn(this, "Không tìm thấy hợp đồng.");
                Close();
                return;
            }

            var c = _contract;
            Text = $"Chi tiết hợp đồng {c.MaHopDongKyHieu}";

            var conNgay = (c.NgayKetThuc.Date - DateTime.Today).Days;
            _summary.Text =
                $"Mã hợp đồng : {c.MaHopDongKyHieu}                 Trạng thái: {Ui.ContractStatusText(c)}\n" +
                $"Phòng       : {c.Room?.MaPhongKyHieu} - {c.Room?.TenPhong}\n" +
                $"Người thuê  : {c.Tenant?.HoTen} ({c.Tenant?.SoDienThoai})\n" +
                $"Thời hạn    : {c.NgayBatDau:dd/MM/yyyy} → {c.NgayKetThuc:dd/MM/yyyy}" +
                (c.TrangThai != ContractStatus.DaThanhLy ? $"  (còn {conNgay} ngày)" : "") + "\n" +
                $"Giá thuê    : {Ui.Money(c.GiaThue)}/tháng          Tiền cọc: {Ui.Money(c.TienCoc)}\n" +
                $"Ghi chú     : {c.GhiChu ?? "(không có)"}";

            // Hóa đơn của phòng
            _invoices.Columns.Clear();
            _invoices.Rows.Clear();
            foreach (var (name, header, weight) in new[]
            {
                ("MaHD", "Mã hóa đơn", 14), ("Ky", "Kỳ", 8), ("Phong", "Tiền phòng", 12),
                ("Dien", "Tiền điện", 11), ("Nuoc", "Tiền nước", 11), ("DV", "Dịch vụ", 11),
                ("Tong", "Tổng tiền", 13), ("Han", "Hạn TT", 10), ("TrangThai", "Trạng thái", 12),
            })
            {
                _invoices.Columns.Add(new DataGridViewTextBoxColumn { Name = name, HeaderText = header, FillWeight = weight });
            }
            _invoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (var i in c.Invoices.OrderByDescending(i => i.KyHoaDon))
            {
                _invoices.Rows.Add(i.MaHoaDonKyHieu, i.KyHoaDon, Ui.Money(i.TienPhong), Ui.Money(i.TienDien),
                    Ui.Money(i.TienNuoc), Ui.Money(i.TienDichVu), Ui.Money(i.TongTien), Ui.Date(i.HanThanhToan),
                    Ui.InvoiceStatusText(i));
            }

            // Lịch sử
            _histories.Columns.Clear();
            _histories.Rows.Clear();
            _histories.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thời gian", FillWeight = 18 });
            _histories.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hành động", FillWeight = 22 });
            _histories.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nội dung", FillWeight = 60 });
            _histories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (var h in c.Histories.OrderByDescending(h => h.ThoiGian))
                _histories.Rows.Add(h.ThoiGian.ToString("dd/MM/yyyy HH:mm"), h.HanhDong, h.NoiDung);

            // Quyết toán trả phòng
            var m = c.MoveOut;
            _moveOutInfo.Text = m == null
                ? "Hợp đồng chưa trả phòng.\n\nBấm nút \"🚪 Trả phòng\" để chốt điện nước, xem trước quyết toán và hoàn tất."
                : $"Ngày trả phòng           : {m.NgayTraPhong:dd/MM/yyyy}\n" +
                  $"Công nợ trước trả phòng : {Ui.Money(m.CongNoTruocTraPhong)}\n" +
                  $"Phát sinh cuối kỳ        : {Ui.Money(m.TienPhatSinhCuoiKy)}\n" +
                  $"Khoản phải thu cuối      : {Ui.Money(m.KhoanPhaiThuCuoiCung)}\n" +
                  $"Tiền cọc ban đầu         : {Ui.Money(m.TienCocBanDau)}\n" +
                  $"Khấu trừ cọc             : {Ui.Money(m.TienKhauTruCoc)}\n" +
                  (m.TienHoanCoc > 0 ? $"HOÀN LẠI CỌC            : {Ui.Money(m.TienHoanCoc)}"
                                     : $"KHÁCH TRẢ THÊM          : {Ui.Money(m.TienPhaiTraThem)}") + "\n" +
                  $"Trạng thái phòng sau trả : {Ui.RoomStatusText(m.TrangThaiPhongSauTra)}\n" +
                  $"Đã quyết toán            : {(m.DaQuyetToan ? "Rồi" : "Chưa")}\n" +
                  $"Ghi chú                  : {m.GhiChu ?? "(không có)"}";

            // Quyền thao tác
            _btnRenew.Enabled = c.TrangThai != ContractStatus.DaThanhLy;
            _btnLiquidate.Enabled = c.TrangThai != ContractStatus.DaThanhLy;
            _btnMoveOut.Enabled = c.TrangThai != ContractStatus.DaThanhLy && c.MoveOut == null;
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi tải chi tiết hợp đồng: " + ex.Message);
        }
    }

    private async Task RenewAsync()
    {
        if (_contract == null) return;
        using var dlg = new RenewContractDialog(_contract, _contracts!);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            Changed = true;
            await LoadAsync();
        }
    }

    private async Task LiquidateAsync()
    {
        if (_contract == null) return;

        var reason = NoteDialog.Open(this, "Thanh lý hợp đồng",
            $"Lý do thanh lý hợp đồng {_contract.MaHopDongKyHieu} (phòng {_contract.Room?.MaPhongKyHieu}):",
            "Hai bên thống nhất chấm dứt hợp đồng.");
        if (reason == null) return;

        try
        {
            var result = await _contracts!.LiquidateAsync(_id, reason);
            Msg.Result(this, result);
            if (result.Ok)
            {
                Changed = true;
                await LoadAsync();
            }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }

    private async Task MoveOutAsync()
    {
        if (_contract == null) return;
        using var dlg = new MoveOutDialog(_contract.MaHopDong, _moveOuts!, _contracts!, _services!);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            Changed = true;
            await LoadAsync();
        }
    }
}
