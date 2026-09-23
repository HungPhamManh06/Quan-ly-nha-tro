using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Dialogs;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Forms;

/// <summary>
/// Cửa sổ chính: menu + TabControl gồm toàn bộ màn hình nghiệp vụ của bản web
/// (Tổng quan, Phòng, Người thuê, Hợp đồng, Điện nước, Dịch vụ, Sử dụng dịch vụ,
///  Hóa đơn, Thanh toán, Trả phòng, Báo cáo).
/// Nghiệp vụ tái sử dụng nguyên vẹn từ bản web qua tiêm phụ thuộc (DI).
///
/// GIAO DIỆN: MainForm.Designer.cs (menu, 11 tab, thanh trạng thái) — mở bằng
/// Visual Studio Designer để chỉnh sửa. Mỗi tab danh sách là một ListTabPanel.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class MainForm : Form
{
    // Gán trong constructor. Không dùng khởi tạo field vì ở chế độ Designer (VS tạo form
    // trong tiến trình riêng, không có database) thì Program.Services chưa tồn tại.
    private readonly IRoomService _roomService = null!;
    private readonly ITenantService _tenantService = null!;
    private readonly IContractService _contractService = null!;
    private readonly IUtilityService _utilityService = null!;
    private readonly IServiceService _serviceService = null!;
    private readonly IInvoiceService _invoiceService = null!;
    private readonly IPaymentService _paymentService = null!;
    private readonly IMoveOutService _moveOutService = null!;
    private readonly ApplicationDbContext _db = null!;

    public MainForm()
    {
        InitializeComponent();

        // Nhân kích thước theo tỉ lệ màn hình (100% / 125% / 150% / 200%)
        Dpi.ScaleForm(this);

        // Ở Visual Studio Designer: dừng tại đây. Designer không có database, nếu truy cập
        // Program.Services thì Designer báo lỗi và không hiện được giao diện.
        if (Dpi.IsDesignTime) return;

        // Lấy các service từ DI (giống inject trong Controller của bản web)
        _roomService = Program.Services.GetRequiredService<IRoomService>();
        _tenantService = Program.Services.GetRequiredService<ITenantService>();
        _contractService = Program.Services.GetRequiredService<IContractService>();
        _utilityService = Program.Services.GetRequiredService<IUtilityService>();
        _serviceService = Program.Services.GetRequiredService<IServiceService>();
        _invoiceService = Program.Services.GetRequiredService<IInvoiceService>();
        _paymentService = Program.Services.GetRequiredService<IPaymentService>();
        _moveOutService = Program.Services.GetRequiredService<IMoveOutService>();
        _db = Program.Services.GetRequiredService<ApplicationDbContext>();

        WireEvents();

        Load += async (_, _) => await LoadCurrentTabAsync();
        tabs.SelectedIndexChanged += async (_, _) => await LoadCurrentTabAsync();
    }

    // ================== Gắn sự kiện ==================
    //
    // Giao diện nằm trong MainForm.Designer.cs và ListTabPanel.Designer.cs; ở đây chỉ nối
    // sự kiện. Vì 9 tab danh sách dùng CHUNG một UserControl (ListTabPanel) nên chỉ cần
    // một vòng lặp: muốn đổi nút bấm thì sửa ListTabPanel.Designer.cs (một chỗ).

    private void WireEvents()
    {
        mnuLamMoi.Click += async (_, _) => await LoadCurrentTabAsync();
        mnuDangXuat.Click += (_, _) => DangXuat();
        mnuThoat.Click += (_, _) => Close();

        mnuLapHopDong.Click += async (_, _) => await OnAddAsync("Hợp đồng");
        mnuGhiDienNuoc.Click += async (_, _) => await OnAddAsync("Điện nước");
        mnuLapHoaDon.Click += async (_, _) => await OnAddAsync("Hóa đơn");
        mnuGhiSuDungDichVu.Click += async (_, _) => await OnAddAsync("Sử dụng dịch vụ");
        mnuTraPhong.Click += async (_, _) => await OnAddAsync("Trả phòng");

        mnuGioiThieu.Click += (_, _) => InfoDialog.Open(this, "Giới thiệu",
            "ỨNG DỤNG QUẢN LÝ NHÀ TRỌ (BẢN DESKTOP - WINDOWS FORMS)\n\n" +
            "· Phần nghiệp vụ dùng lại 100% từ bản web (Models, Services, EF Core, seed dữ liệu).\n" +
            "· Giao diện desktop: 11 màn hình tương ứng các màn của bản web.\n" +
            "· Database: SQL Server (đúng đề tài) hoặc SQLite (chạy nhanh, không cần cài\n" +
            "  SQL Server) — chọn trong QuanLyNhaTroDesktop/appsettings.json.\n\n" +
            "Tài khoản mẫu: chutro / Chutro@123");
        mnuTaiKhoan.Click += (_, _) => InfoDialog.Open(this, "Tài khoản",
            "Ứng dụng desktop dùng chung bảng tài khoản (Identity) với bản web.\n\n" +
            "Tài khoản seed sẵn: chutro / Chutro@123");

        // 9 tab danh sách — cùng cấu trúc nên gắn sự kiện trong một vòng lặp
        foreach (var panel in ListPanels)
        {
            var tab = panel.TabKey;
            var list = panel.List;
            list.DoubleClick += async (_, _) => await OnDetailAsync(tab);
            panel.AddButton.Click += async (_, _) => await OnAddAsync(tab);
            panel.EditButton.Click += async (_, _) => await OnEditAsync(tab, list);
            panel.DeleteButton.Click += async (_, _) => await OnDeleteAsync(tab, list);
            panel.DetailButton.Click += async (_, _) => await OnDetailAsync(tab);
            panel.RefreshButton.Click += async (_, _) => await LoadCurrentTabAsync();
        }

        // Quyền thao tác theo nghiệp vụ (giống bản web): Thanh toán chỉ xem,
        // Trả phòng chỉ thêm mới + xem chi tiết.
        panelThanhToan.ShowActions(canAdd: false, canEdit: false, canDelete: false);
        panelTraPhong.ShowActions(canEdit: false, canDelete: false);
    }

    /// <summary>Các tab dạng danh sách (đều là ListTabPanel nên gắn sự kiện giống nhau).</summary>
    private IEnumerable<ListTabPanel> ListPanels => new[]
    {
        panelPhong, panelNguoiThue, panelHopDong, panelDienNuoc, panelDichVu,
        panelSuDungDichVu, panelHoaDon, panelThanhToan, panelTraPhong,
    };

    private ListTabPanel PanelOf(string tab) => ListPanels.First(p => p.TabKey == tab);

    private ListView ListOf(string tab) => PanelOf(tab).List;

    // ---- Dùng cho công cụ kiểm tra tự động (--dump-layout) ----
    internal int TabCount => tabs.TabPages.Count;
    internal string TabTitleAt(int index) => tabs.TabPages[index].Text;
    internal void SelectTab(int index) => tabs.SelectedIndex = index;

    /// <summary>Số dòng đang có ở tab hiện tại (-1 nếu tab không phải dạng danh sách).</summary>
    internal int CurrentTabRowCount()
    {
        var page = tabs.SelectedTab;
        if (page == null) return -1;
        var list = page.Controls.OfType<ListTabPanel>().Select(p => p.List).FirstOrDefault()
                   ?? page.Controls.OfType<ListView>().FirstOrDefault();
        return list?.Items.Count ?? -1;
    }

    internal string TabSummary()
    {
        var page = tabs.SelectedTab;
        if (page == null) return "(không có tab)";
        var list = page.Controls.OfType<ListView>().FirstOrDefault()
                   ?? page.Controls.OfType<ListTabPanel>().Select(p => p.List).FirstOrDefault();
        if (list != null) return $"{list.Items.Count} dòng";
        if (page.Text.Contains("Tổng quan")) return "bảng tổng quan";
        if (page.Text.Contains("Báo cáo")) return "bảng báo cáo";
        return "?";
    }

    private void SetStatus(string message)
    {
        lblStatus.Text = message;
        status.Refresh();
    }

    private void DangXuat()
    {
        Hide();
        using var login = new LoginForm();
        if (login.ShowDialog() == DialogResult.OK) Show();
        else Close();
    }

    // ================== Nạp dữ liệu ==================
    //
    // Cả app dùng CHUNG một DbContext (lấy từ DI), nên hai lần nạp dữ liệu KHÔNG được chạy
    // chồng lên nhau. Sự kiện đổi tab là `async void` — bấm tab nhanh (hoặc mở cửa sổ rồi
    // bấm ngay) thì lần nạp trước chưa xong đã có lần nạp sau, SQL Server báo:
    //   "A second operation was started on this context instance..."
    // (SQLite nhanh nên trước đây ít khi lộ ra). Khóa dưới đây xếp hàng các lần nạp lại.
    private readonly SemaphoreSlim _loadLock = new(1, 1);

    private async Task LoadCurrentTabAsync()
    {
        await _loadLock.WaitAsync();
        try
        {
            var page = tabs.SelectedTab;
            if (page == null) return;

            if (page.Text == "📊 Tổng quan") { await panelTongQuan.LoadDataAsync(); return; }
            if (page.Text == "📈 Báo cáo") { await panelBaoCao.LoadReportAsync(); return; }

            await ReloadListAsync(page.Text, ListOf(page.Text));
        }
        finally
        {
            _loadLock.Release();
        }
    }

    /// <summary>
    /// Chờ lần nạp dữ liệu đang chạy xong (nếu có) rồi mới mở cửa sổ con. Cửa sổ con cũng
    /// dùng chung DbContext với danh sách, nên mở ngay lúc danh sách đang tải sẽ bị lỗi
    /// "A second operation was started on this context instance". Hàm này nhả khóa ngay
    /// (không giữ) nên không gây treo.
    /// </summary>
    private async Task WaitForIdleAsync()
    {
        await _loadLock.WaitAsync();
        _loadLock.Release();
    }

    private async Task ReloadCurrentAsync() => await LoadCurrentTabAsync();

    private async Task ReloadListAsync(string tab, ListView list)
    {
        try
        {
            SetStatus("Đang tải...");
            list.BeginUpdate();
            list.Columns.Clear();
            list.Items.Clear();
            list.Groups.Clear();

            switch (tab)
            {
                case "Phòng": await BuildRoomsTab(list); break;
                case "Người thuê": await BuildTenantsTab(list); break;
                case "Hợp đồng": await BuildContractsTab(list); break;
                case "Điện nước": await BuildUtilitiesTab(list); break;
                case "Dịch vụ": await BuildServicesTab(list); break;
                case "Sử dụng dịch vụ": await BuildServiceUsagesTab(list); break;
                case "Hóa đơn": await BuildInvoicesTab(list); break;
                case "Thanh toán": await BuildPaymentsTab(list); break;
                case "Trả phòng": await BuildMoveOutTab(list); break;
            }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi tải dữ liệu: " + ex.Message);
        }
        finally
        {
            list.EndUpdate();
            SetStatus($"{tab}: {list.Items.Count} dòng — cập nhật lúc {DateTime.Now:HH:mm:ss}");
        }
    }

    private async Task BuildRoomsTab(ListView list)
    {
        var rooms = await _roomService.GetAllAsync();
        list.Columns.Add("Mã phòng", 110);
        list.Columns.Add("Tên phòng", 150);
        list.Columns.Add("Loại", 130);
        list.Columns.Add("Diện tích", 100);
        list.Columns.Add("Giá phòng (đ/tháng)", 160);
        list.Columns.Add("Trạng thái", 140);
        list.Columns.Add("Người thuê hiện tại", 180);
        list.Columns.Add("Ghi chú", 200);

        foreach (var r in rooms)
        {
            var tenantActive = r.Contracts?
                .Where(c => c.TrangThai != ContractStatus.DaThanhLy && c.ConHieuLuc(DateTime.Today))
                .Select(c => c.Tenant?.HoTen).FirstOrDefault();

            var item = new ListViewItem(r.MaPhongKyHieu) { Tag = r };
            item.SubItems.Add(r.TenPhong);
            item.SubItems.Add(r.LoaiPhong ?? "");
            item.SubItems.Add(r.DienTich?.ToString("N1") ?? "");
            item.SubItems.Add(Ui.Money(r.GiaPhong));
            item.SubItems.Add(Ui.RoomStatusText(r.TrangThai));
            item.SubItems.Add(tenantActive ?? "—");
            item.SubItems.Add(r.GhiChu ?? "");
            item.ForeColor = Ui.RoomStatusColor(r.TrangThai);
            list.Items.Add(item);
        }
        SetStatus($"Có {rooms.Count} phòng.");
    }

    private async Task BuildTenantsTab(ListView list)
    {
        var tenants = await _tenantService.GetAllAsync();
        list.Columns.Add("Họ tên", 190);
        list.Columns.Add("CCCD", 140);
        list.Columns.Add("Điện thoại", 120);
        list.Columns.Add("Quê quán", 150);
        list.Columns.Add("Phòng hiện tại", 120);
        list.Columns.Add("Trạng thái", 120);

        foreach (var t in tenants)
        {
            var item = new ListViewItem(t.Tenant.HoTen) { Tag = t.Tenant };
            item.SubItems.Add(t.Tenant.CCCD);
            item.SubItems.Add(t.Tenant.SoDienThoai ?? "");
            item.SubItems.Add(t.Tenant.QueQuan ?? "");
            item.SubItems.Add(t.PhongHienTai ?? "—");
            item.SubItems.Add(t.Tenant.TrangThai);
            list.Items.Add(item);
        }
        SetStatus($"Có {tenants.Count} người thuê.");
    }

    private async Task BuildContractsTab(ListView list)
    {
        var contracts = await _contractService.GetAllAsync();
        list.Columns.Add("Mã HĐ", 100);
        list.Columns.Add("Phòng", 90);
        list.Columns.Add("Người thuê", 170);
        list.Columns.Add("Bắt đầu", 100);
        list.Columns.Add("Kết thúc", 100);
        list.Columns.Add("Còn lại", 90);
        list.Columns.Add("Giá thuê (đ)", 120);
        list.Columns.Add("Tiền cọc (đ)", 110);
        list.Columns.Add("Trạng thái", 150);

        foreach (var c in contracts)
        {
            var item = new ListViewItem(c.MaHopDongKyHieu) { Tag = c };
            item.SubItems.Add(c.Room?.MaPhongKyHieu ?? "");
            item.SubItems.Add(c.Tenant?.HoTen ?? "");
            item.SubItems.Add(Ui.Date(c.NgayBatDau));
            item.SubItems.Add(Ui.Date(c.NgayKetThuc));
            item.SubItems.Add(((c.NgayKetThuc.Date - DateTime.Today).Days is var d && d >= 0) ? $"{d} ngày" : "—");
            item.SubItems.Add(Ui.Money(c.GiaThue));
            item.SubItems.Add(Ui.Money(c.TienCoc));
            item.SubItems.Add(Ui.ContractStatusText(c));
            if (c.TrangThai == ContractStatus.DaThanhLy) item.ForeColor = Ui.Muted;
            else if (Ui.ContractStatusText(c).StartsWith("Sắp hết hạn")) item.ForeColor = Ui.Warning;
            list.Items.Add(item);
        }
        SetStatus($"Có {contracts.Count} hợp đồng.");
    }

    private async Task BuildUtilitiesTab(ListView list)
    {
        var readings = await _utilityService.GetAllAsync();
        list.Columns.Add("Phòng", 90);
        list.Columns.Add("Kỳ", 100);
        list.Columns.Add("Điện cũ", 90);
        list.Columns.Add("Điện mới", 90);
        list.Columns.Add("Số điện", 90);
        list.Columns.Add("Tiền điện (đ)", 120);
        list.Columns.Add("Nước cũ", 90);
        list.Columns.Add("Nước mới", 90);
        list.Columns.Add("Số nước", 90);
        list.Columns.Add("Tiền nước (đ)", 120);

        foreach (var u in readings)
        {
            var item = new ListViewItem(u.Room?.MaPhongKyHieu ?? "") { Tag = u };
            item.SubItems.Add(u.KyGhi);
            item.SubItems.Add(u.ChiSoDienCu.ToString("N0"));
            item.SubItems.Add(u.ChiSoDienMoi.ToString("N0"));
            item.SubItems.Add(u.SoDienSuDung.ToString("N0"));
            item.SubItems.Add(Ui.Money(u.TienDien));
            item.SubItems.Add(u.ChiSoNuocCu.ToString("N0"));
            item.SubItems.Add(u.ChiSoNuocMoi.ToString("N0"));
            item.SubItems.Add(u.SoNuocSuDung.ToString("N0"));
            item.SubItems.Add(Ui.Money(u.TienNuoc));
            list.Items.Add(item);
        }
        SetStatus($"Có {readings.Count} bản ghi điện nước.");
    }

    private async Task BuildServicesTab(ListView list)
    {
        var services = await _serviceService.GetServicesAsync();
        list.Columns.Add("Tên dịch vụ", 200);
        list.Columns.Add("Đơn vị tính", 120);
        list.Columns.Add("Đơn giá (đ)", 150);
        list.Columns.Add("Trạng thái", 130);

        foreach (var s in services)
        {
            var item = new ListViewItem(s.TenDichVu) { Tag = s };
            item.SubItems.Add(s.DonViTinh);
            item.SubItems.Add(Ui.Money(s.DonGia));
            item.SubItems.Add(s.TrangThai ? "Đang áp dụng" : "Ngừng");
            if (!s.TrangThai) item.ForeColor = Ui.Muted;
            list.Items.Add(item);
        }
        SetStatus($"Có {services.Count} dịch vụ.");
    }

    private async Task BuildServiceUsagesTab(ListView list)
    {
        var usages = await _serviceService.GetUsagesAsync();
        list.Columns.Add("Phòng", 90);
        list.Columns.Add("Dịch vụ", 180);
        list.Columns.Add("Kỳ", 110);
        list.Columns.Add("Số lượng", 90);
        list.Columns.Add("Đơn vị", 90);
        list.Columns.Add("Đơn giá áp dụng (đ)", 150);
        list.Columns.Add("Thành tiền (đ)", 140);
        list.Columns.Add("Ghi chú", 180);

        foreach (var u in usages)
        {
            var item = new ListViewItem(u.Room?.MaPhongKyHieu ?? "") { Tag = u };
            item.SubItems.Add(u.Service?.TenDichVu ?? "");
            item.SubItems.Add(u.KySuDung);
            item.SubItems.Add(u.SoLuong.ToString("0.##"));
            item.SubItems.Add(u.Service?.DonViTinh ?? "");
            item.SubItems.Add(Ui.Money(u.DonGiaApDung));
            item.SubItems.Add(Ui.Money(u.ThanhTien));
            item.SubItems.Add(u.GhiChu ?? "");
            if (u.KySuDung.StartsWith("TRAPHONG-", StringComparison.OrdinalIgnoreCase)) item.ForeColor = Ui.Warning;
            list.Items.Add(item);
        }
        SetStatus($"Có {usages.Count} bản ghi sử dụng dịch vụ.");
    }

    private async Task BuildInvoicesTab(ListView list)
    {
        var invoices = await _invoiceService.GetAllAsync();
        list.Columns.Add("Mã hóa đơn", 150);
        list.Columns.Add("Phòng", 80);
        list.Columns.Add("Kỳ", 90);
        list.Columns.Add("Tiền phòng", 110);
        list.Columns.Add("Tiền điện", 100);
        list.Columns.Add("Tiền nước", 100);
        list.Columns.Add("Dịch vụ", 100);
        list.Columns.Add("Tổng tiền (đ)", 130);
        list.Columns.Add("Hạn TT", 100);
        list.Columns.Add("Trạng thái", 130);

        foreach (var i in invoices)
        {
            var item = new ListViewItem(i.MaHoaDonKyHieu) { Tag = i };
            item.SubItems.Add(i.Room?.MaPhongKyHieu ?? "");
            item.SubItems.Add(i.KyHoaDon);
            item.SubItems.Add(Ui.Money(i.TienPhong));
            item.SubItems.Add(Ui.Money(i.TienDien));
            item.SubItems.Add(Ui.Money(i.TienNuoc));
            item.SubItems.Add(Ui.Money(i.TienDichVu));
            item.SubItems.Add(Ui.Money(i.TongTien));
            item.SubItems.Add(Ui.Date(i.HanThanhToan));
            item.SubItems.Add(Ui.InvoiceStatusText(i));
            item.ForeColor = i.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan ? Ui.Success : Ui.Danger;
            list.Items.Add(item);
        }
        SetStatus($"Có {invoices.Count} hóa đơn — chưa thu {Ui.Money(invoices.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan).Sum(i => i.TongTien))}.");
    }

    private async Task BuildPaymentsTab(ListView list)
    {
        var payments = await _paymentService.GetAllAsync();
        list.Columns.Add("Hóa đơn", 150);
        list.Columns.Add("Phòng", 80);
        list.Columns.Add("Kỳ", 90);
        list.Columns.Add("Số tiền (đ)", 130);
        list.Columns.Add("Ngày thanh toán", 140);
        list.Columns.Add("Phương thức", 120);
        list.Columns.Add("Ghi chú", 240);

        foreach (var p in payments)
        {
            var item = new ListViewItem(p.Invoice?.MaHoaDonKyHieu ?? "") { Tag = p };
            item.SubItems.Add(p.Invoice?.Room?.MaPhongKyHieu ?? "");
            item.SubItems.Add(p.Invoice?.KyHoaDon ?? "");
            item.SubItems.Add(Ui.Money(p.SoTien));
            item.SubItems.Add(p.NgayThanhToan.ToString("dd/MM/yyyy HH:mm"));
            item.SubItems.Add(Ui.PaymentMethodText(p.PhuongThuc));
            item.SubItems.Add(p.GhiChu ?? "");
            list.Items.Add(item);
        }
        SetStatus($"Có {payments.Count} phiếu thu — tổng {Ui.Money(payments.Sum(p => p.SoTien))}.");
    }

    private async Task BuildMoveOutTab(ListView list)
    {
        var moveOuts = await _db.MoveOuts.AsNoTracking()
            .Include(m => m.Contract).ThenInclude(c => c!.Room)
            .Include(m => m.Contract).ThenInclude(c => c!.Tenant)
            .OrderByDescending(m => m.NgayTraPhong).ToListAsync();

        list.Columns.Add("Phòng", 80);
        list.Columns.Add("Người thuê", 160);
        list.Columns.Add("Ngày trả phòng", 120);
        list.Columns.Add("Công nợ trước", 120);
        list.Columns.Add("Phát sinh cuối kỳ", 130);
        list.Columns.Add("Phải thu cuối", 130);
        list.Columns.Add("Cọc ban đầu", 120);
        list.Columns.Add("Khấu trừ cọc", 120);
        list.Columns.Add("Hoàn cọc", 120);
        list.Columns.Add("Trả thêm", 110);
        list.Columns.Add("Đã quyết toán", 110);

        foreach (var m in moveOuts)
        {
            var item = new ListViewItem(m.Contract?.Room?.MaPhongKyHieu ?? "") { Tag = m };
            item.SubItems.Add(m.Contract?.Tenant?.HoTen ?? "");
            item.SubItems.Add(Ui.Date(m.NgayTraPhong));
            item.SubItems.Add(Ui.Money(m.CongNoTruocTraPhong));
            item.SubItems.Add(Ui.Money(m.TienPhatSinhCuoiKy));
            item.SubItems.Add(Ui.Money(m.KhoanPhaiThuCuoiCung));
            item.SubItems.Add(Ui.Money(m.TienCocBanDau));
            item.SubItems.Add(Ui.Money(m.TienKhauTruCoc));
            item.SubItems.Add(Ui.Money(m.TienHoanCoc));
            item.SubItems.Add(Ui.Money(m.TienPhaiTraThem));
            item.SubItems.Add(m.DaQuyetToan ? "Rồi" : "Chưa");
            list.Items.Add(item);
        }
        SetStatus($"Có {moveOuts.Count} lần trả phòng — hoàn cọc {Ui.Money(moveOuts.Sum(m => m.TienHoanCoc))}.");
    }

    // ================== Thêm / Sửa / Xóa / Chi tiết ==================

    private async Task OnAddAsync(string tab)
    {
        await WaitForIdleAsync();

        switch (tab)
        {
            case "Phòng":
                using (var dlg = new RoomDialog(null))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Người thuê":
                using (var dlg = new TenantDialog(null))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Hợp đồng":
                if (await HasNoRoomOrTenantAsync()) return;
                using (var dlg = new ContractDialog(null, _contractService, _roomService, _tenantService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Điện nước":
                using (var dlg = new UtilityDialog(null, _utilityService, _roomService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Dịch vụ":
                using (var dlg = new ServiceDialog(null, _serviceService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Sử dụng dịch vụ":
                using (var dlg = new ServiceUsageDialog(null, _serviceService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Hóa đơn":
                using (var dlg = new InvoiceDialog(null, _invoiceService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Thanh toán":
                Msg.Info(this, "Để ghi nhận thanh toán: mở tab 'Hóa đơn' → chọn hóa đơn chưa thanh toán → bấm '✏ Sửa'.");
                break;

            case "Trả phòng":
                using (var dlg = new MoveOutDialog(null, _moveOutService, _contractService, _serviceService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;
        }
    }

    private async Task<bool> HasNoRoomOrTenantAsync()
    {
        if (!await _db.Rooms.AnyAsync())
        {
            Msg.Warn(this, "Chưa có phòng nào. Hãy thêm phòng trước khi lập hợp đồng.");
            return true;
        }
        if (!await _db.Tenants.AnyAsync())
        {
            Msg.Warn(this, "Chưa có người thuê nào. Hãy thêm người thuê trước khi lập hợp đồng.");
            return true;
        }
        return false;
    }

    private async Task OnEditAsync(string tab, ListView list)
    {
        await WaitForIdleAsync();

        if (!TryGetSelected(list, out var tag)) return;

        switch (tab)
        {
            case "Phòng":
                using (var dlg = new RoomDialog((Room)tag!))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Người thuê":
                using (var dlg = new TenantDialog((Tenant)tag!))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Hợp đồng":
                await ShowContractDetailAsync(((Contract)tag!).MaHopDong);
                break;

            case "Điện nước":
                using (var dlg = new UtilityDialog((UtilityReading)tag!, _utilityService, _roomService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Dịch vụ":
                using (var dlg = new ServiceDialog((Service)tag!, _serviceService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Sử dụng dịch vụ":
                using (var dlg = new ServiceUsageDialog((ServiceUsage)tag!, _serviceService))
                    if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
                break;

            case "Hóa đơn":
                await PayInvoiceAsync((Invoice)tag!);
                break;

            case "Thanh toán":
                await OnDetailAsync("Thanh toán", tag);
                break;

            default:
                Msg.Info(this, "Màn này chỉ xem dữ liệu. Dùng nút '🔍 Chi tiết'.");
                break;
        }
    }

    private bool TryGetSelected(ListView list, out object? tag)
    {
        tag = null;
        if (list.SelectedItems.Count == 0)
        {
            Msg.Info(this, "Hãy chọn một dòng trong bảng trước.");
            return false;
        }
        tag = list.SelectedItems[0].Tag;
        return true;
    }

    private async Task OnDeleteAsync(string tab, ListView list)
    {
        await WaitForIdleAsync();

        if (!TryGetSelected(list, out var tag)) return;

        var confirm = tab switch
        {
            "Hợp đồng" => "Hợp đồng không xóa trực tiếp. Dùng chức năng 'Thanh lý' hoặc 'Trả phòng & quyết toán'.\n\nMở màn chi tiết hợp đồng ngay bây giờ?",
            "Trả phòng" => "Phiếu trả phòng đã quyết toán không thể xóa (dữ liệu lịch sử tài chính).\n\nMở màn xem chi tiết?",
            _ => $"Bạn có chắc muốn xóa mục đã chọn ở màn '{tab}'?",
        };

        if (tab is "Hợp đồng" or "Trả phòng")
        {
            if (Msg.Confirm(this, confirm, "Không thể xóa")) await OnDetailAsync(tab, tag);
            return;
        }

        if (!Msg.Confirm(this, confirm, "Xác nhận xóa")) return;

        try
        {
            var result = tab switch
            {
                "Phòng" => await _roomService.DeleteAsync(((Room)tag!).MaPhong),
                "Người thuê" => await _tenantService.DeleteAsync(((Tenant)tag!).MaNguoiThue),
                "Điện nước" => await _utilityService.DeleteAsync(((UtilityReading)tag!).MaGhiChiSo),
                "Dịch vụ" => await _serviceService.DeleteServiceAsync(((Service)tag!).MaDichVu),
                "Sử dụng dịch vụ" => await _serviceService.DeleteUsageAsync(((ServiceUsage)tag!).MaSuDung),
                "Hóa đơn" => await _invoiceService.DeleteAsync(((Invoice)tag!).MaHoaDon),
                _ => (false, "Không thể xóa dữ liệu này."),
            };

            Msg.Result(this, result);
            await ReloadCurrentAsync();
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }

    private async Task OnDetailAsync(string tab)
    {
        await WaitForIdleAsync();

        var list = ListOf(tab);
        if (!TryGetSelected(list, out var tag)) return;
        await OnDetailAsync(tab, tag);
    }

    /// <summary>Xem chi tiết — tương ứng các màn Details/ của bản web.</summary>
    private async Task OnDetailAsync(string tab, object? tag)
    {
        try
        {
            switch (tab)
            {
                case "Phòng": await ShowRoomDetailAsync((Room)tag!); break;
                case "Người thuê": await ShowTenantDetailAsync((Tenant)tag!); break;
                case "Hợp đồng": await ShowContractDetailAsync(((Contract)tag!).MaHopDong); break;
                case "Điện nước": ShowUtilityDetail((UtilityReading)tag!); break;
                case "Dịch vụ": ShowServiceDetail((Service)tag!); break;
                case "Sử dụng dịch vụ": ShowUsageDetail((ServiceUsage)tag!); break;
                case "Hóa đơn": await ShowInvoiceDetailAsync((Invoice)tag!); break;
                case "Thanh toán": await ShowPaymentDetailAsync(((Payment)tag!).MaThanhToan); break;
                case "Trả phòng": await ShowMoveOutDetailAsync((MoveOut)tag!); break;
            }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi xem chi tiết: " + ex.Message);
        }
    }

    // ================== Các màn xem chi tiết ==================

    private async Task ShowRoomDetailAsync(Room room)
    {
        var detail = await _roomService.GetDetailAsync(room.MaPhong);
        if (detail == null) return;

        var sb = new StringBuilder();
        sb.AppendLine($"PHÒNG {detail.MaPhongKyHieu} — {detail.TenPhong}");
        sb.AppendLine(new string('-', 92));
        sb.AppendLine($"Loại phòng      : {detail.LoaiPhong ?? "(chưa khai)"}");
        sb.AppendLine($"Diện tích       : {(detail.DienTich.HasValue ? detail.DienTich + " m²" : "(chưa khai)")}");
        sb.AppendLine($"Giá phòng       : {Ui.Money(detail.GiaPhong)}/tháng");
        sb.AppendLine($"Trạng thái      : {Ui.RoomStatusText(detail.TrangThai)}");
        sb.AppendLine($"Ghi chú         : {detail.GhiChu ?? "(không có)"}");
        sb.AppendLine();
        sb.AppendLine("HỢP ĐỒNG CỦA PHÒNG");
        sb.AppendLine(string.Format("{0,-10} {1,-24} {2,-12} {3,-12} {4}", "Mã HĐ", "Người thuê", "Bắt đầu", "Kết thúc", "Trạng thái"));
        foreach (var c in detail.Contracts.OrderByDescending(c => c.NgayBatDau))
            sb.AppendLine(string.Format("{0,-10} {1,-24} {2,-12} {3,-12} {4}", c.MaHopDongKyHieu,
                Cut(c.Tenant?.HoTen ?? "", 24), c.NgayBatDau.ToString("dd/MM/yyyy"),
                c.NgayKetThuc.ToString("dd/MM/yyyy"), Ui.ContractStatusText(c)));
        sb.AppendLine();
        sb.AppendLine("CHỈ SỐ ĐIỆN NƯỚC GẦN NHẤT");
        foreach (var u in detail.UtilityReadings.OrderByDescending(u => u.KyGhi).Take(6))
            sb.AppendLine($"  {u.KyGhi}: điện {u.ChiSoDienCu:N0}→{u.ChiSoDienMoi:N0} ({u.TienDien:N0} đ) | " +
                          $"nước {u.ChiSoNuocCu:N0}→{u.ChiSoNuocMoi:N0} ({u.TienNuoc:N0} đ)");
        sb.AppendLine();
        sb.AppendLine("HÓA ĐƠN");
        foreach (var i in detail.Invoices.OrderByDescending(i => i.KyHoaDon).Take(12))
            sb.AppendLine($"  {i.KyHoaDon}: {Ui.Money(i.TongTien)} — {Ui.InvoiceStatusText(i)}");

        var chuaThu = detail.Invoices.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan).Sum(i => i.TongTien);
        sb.AppendLine();
        sb.AppendLine($"Tổng công nợ chưa thu của phòng: {Ui.Money(chuaThu)}");

        InfoDialog.Open(this, $"Chi tiết phòng {detail.MaPhongKyHieu}", sb.ToString());
    }

    private async Task ShowTenantDetailAsync(Tenant tenant)
    {
        var detail = await _tenantService.GetDetailAsync(tenant.MaNguoiThue);
        var active = await _tenantService.GetActiveContractAsync(tenant.MaNguoiThue);
        var t = detail ?? tenant;

        var sb = new StringBuilder();
        sb.AppendLine($"NGƯỜI THUÊ: {t.HoTen}");
        sb.AppendLine(new string('-', 92));
        sb.AppendLine($"CCCD            : {t.CCCD}");
        sb.AppendLine($"Điện thoại      : {t.SoDienThoai ?? "(chưa có)"}");
        sb.AppendLine($"Quê quán        : {t.QueQuan ?? "(chưa có)"}");
        sb.AppendLine($"Trạng thái      : {t.TrangThai}");
        sb.AppendLine($"Ghi chú         : {t.GhiChu ?? "(không có)"}");
        sb.AppendLine();
        sb.AppendLine("HỢP ĐỒNG ĐANG HIỆU LỰC");
        sb.AppendLine(active == null
            ? "  (không có hợp đồng đang hiệu lực)"
            : $"  {active.MaHopDongKyHieu} — phòng {active.Room?.MaPhongKyHieu}\n" +
              $"  Thời hạn: {active.NgayBatDau:dd/MM/yyyy} → {active.NgayKetThuc:dd/MM/yyyy}\n" +
              $"  Giá thuê: {Ui.Money(active.GiaThue)}/tháng — Tiền cọc: {Ui.Money(active.TienCoc)}");

        InfoDialog.Open(this, $"Chi tiết người thuê {t.HoTen}", sb.ToString());
    }

    private async Task ShowContractDetailAsync(int contractId)
    {
        using var dlg = new ContractDetailDialog(contractId, _contractService, _moveOutService);
        if (dlg.ShowDialog(this) == DialogResult.OK || dlg.Changed)
            await ReloadCurrentAsync();
    }

    private void ShowUtilityDetail(UtilityReading u)
    {
        InfoDialog.Open(this, "Chi tiết chỉ số điện nước",
            $"Phòng   : {u.Room?.MaPhongKyHieu} - {u.Room?.TenPhong}\n" +
            $"Kỳ ghi  : {u.KyGhi}\n\n" +
            $"ĐIỆN: cũ {u.ChiSoDienCu:N0} → mới {u.ChiSoDienMoi:N0} = {u.SoDienSuDung:N0} kW\n" +
            $"      đơn giá {u.DonGiaDien:N0} đ → {Ui.Money(u.TienDien)}\n\n" +
            $"NƯỚC: cũ {u.ChiSoNuocCu:N0} → mới {u.ChiSoNuocMoi:N0} = {u.SoNuocSuDung:N0} m³\n" +
            $"      đơn giá {u.DonGiaNuoc:N0} đ → {Ui.Money(u.TienNuoc)}\n\n" +
            $"TỔNG: {Ui.Money(u.TienDien + u.TienNuoc)}\n" +
            $"Ghi chú: {u.GhiChu ?? "(không có)"}");
    }

    private void ShowServiceDetail(Service s)
    {
        InfoDialog.Open(this, "Chi tiết dịch vụ",
            $"Tên dịch vụ : {s.TenDichVu}\n" +
            $"Đơn vị tính : {s.DonViTinh}\n" +
            $"Đơn giá     : {Ui.Money(s.DonGia)}\n" +
            $"Trạng thái  : {(s.TrangThai ? "Đang áp dụng" : "Ngừng áp dụng")}");
    }

    private void ShowUsageDetail(ServiceUsage u)
    {
        InfoDialog.Open(this, "Chi tiết sử dụng dịch vụ",
            $"Phòng           : {u.Room?.MaPhongKyHieu} - {u.Room?.TenPhong}\n" +
            $"Dịch vụ         : {u.Service?.TenDichVu} ({u.Service?.DonViTinh})\n" +
            $"Kỳ sử dụng      : {u.KySuDung}\n" +
            $"Số lượng        : {u.SoLuong:N2}\n" +
            $"Đơn giá áp dụng : {Ui.Money(u.DonGiaApDung)}\n" +
            $"Thành tiền      : {Ui.Money(u.ThanhTien)}\n" +
            $"Ghi chú         : {u.GhiChu ?? "(không có)"}");
    }

    private async Task ShowInvoiceDetailAsync(Invoice invoice)
    {
        var detail = await _invoiceService.GetDetailAsync(invoice.MaHoaDon) ?? invoice;
        var sb = new StringBuilder();
        sb.AppendLine($"HÓA ĐƠN {detail.MaHoaDonKyHieu}");
        sb.AppendLine(new string('-', 92));
        sb.AppendLine($"Phòng           : {detail.Room?.MaPhongKyHieu} - {detail.Room?.TenPhong}");
        sb.AppendLine($"Kỳ hóa đơn      : {detail.KyHoaDon}");
        sb.AppendLine($"Ngày lập        : {detail.NgayLap:dd/MM/yyyy HH:mm}");
        sb.AppendLine($"Hạn thanh toán  : {Ui.Date(detail.HanThanhToan)}");
        sb.AppendLine();
        sb.AppendLine($"Tiền phòng      : {detail.TienPhong,14:N0} đ");
        sb.AppendLine($"Tiền điện       : {detail.TienDien,14:N0} đ");
        sb.AppendLine($"Tiền nước       : {detail.TienNuoc,14:N0} đ");
        sb.AppendLine($"Tiền dịch vụ    : {detail.TienDichVu,14:N0} đ");
        sb.AppendLine($"TỔNG TIỀN       : {detail.TongTien,14:N0} đ");
        sb.AppendLine();
        sb.AppendLine($"Trạng thái      : {Ui.InvoiceStatusText(detail)}");
        sb.AppendLine($"Ngày thanh toán : {(detail.NgayThanhToan.HasValue ? detail.NgayThanhToan.Value.ToString("dd/MM/yyyy HH:mm") : "(chưa thanh toán)")}");
        if (detail.Payment != null)
        {
            sb.AppendLine($"Phiếu thu       : {Ui.Money(detail.Payment.SoTien)} — {Ui.PaymentMethodText(detail.Payment.PhuongThuc)}");
            sb.AppendLine($"Ghi chú thu     : {detail.Payment.GhiChu ?? "(không có)"}");
        }
        sb.AppendLine($"Ghi chú hóa đơn : {detail.GhiChu ?? "(không có)"}");

        InfoDialog.Open(this, $"Chi tiết hóa đơn {detail.MaHoaDonKyHieu}", sb.ToString());
    }

    private async Task ShowPaymentDetailAsync(int paymentId)
    {
        var p = await _paymentService.GetDetailAsync(paymentId);
        if (p == null) return;
        InfoDialog.Open(this, "Chi tiết phiếu thu",
            $"Phiếu thu       : #{p.MaThanhToan}\n" +
            $"Hóa đơn         : {p.Invoice?.MaHoaDonKyHieu} (kỳ {p.Invoice?.KyHoaDon})\n" +
            $"Phòng           : {p.Invoice?.Room?.MaPhongKyHieu} - {p.Invoice?.Room?.TenPhong}\n" +
            $"Tổng hóa đơn    : {Ui.Money(p.Invoice?.TongTien ?? 0)}\n" +
            $"Số tiền đã thu  : {Ui.Money(p.SoTien)}\n" +
            $"Ngày thanh toán : {p.NgayThanhToan:dd/MM/yyyy HH:mm}\n" +
            $"Phương thức     : {Ui.PaymentMethodText(p.PhuongThuc)}\n" +
            $"Ghi chú         : {p.GhiChu ?? "(không có)"}");
    }

    private async Task ShowMoveOutDetailAsync(MoveOut m)
    {
        if (m.Contract == null)
            m.Contract = await _db.Contracts.Include(c => c.Room).Include(c => c.Tenant)
                .FirstOrDefaultAsync(c => c.MaHopDong == m.MaHopDong);

        InfoDialog.Open(this, "Chi tiết quyết toán trả phòng",
            $"Phòng                 : {m.Contract?.Room?.MaPhongKyHieu}\n" +
            $"Người thuê            : {m.Contract?.Tenant?.HoTen}\n" +
            $"Ngày trả phòng        : {Ui.Date(m.NgayTraPhong)}\n\n" +
            $"Công nợ trước trả     : {m.CongNoTruocTraPhong,14:N0} đ\n" +
            $"Phát sinh cuối kỳ      : {m.TienPhatSinhCuoiKy,14:N0} đ\n" +
            $"KHOẢN PHẢI THU CUỐI   : {m.KhoanPhaiThuCuoiCung,14:N0} đ\n\n" +
            $"Tiền cọc ban đầu      : {m.TienCocBanDau,14:N0} đ\n" +
            $"Khấu trừ cọc          : {m.TienKhauTruCoc,14:N0} đ\n" +
            $"Hoàn lại cọc          : {m.TienHoanCoc,14:N0} đ\n" +
            $"Khách trả thêm        : {m.TienPhaiTraThem,14:N0} đ\n\n" +
            $"Trạng thái phòng sau  : {Ui.RoomStatusText(m.TrangThaiPhongSauTra)}\n" +
            $"Đã quyết toán         : {(m.DaQuyetToan ? "Rồi" : "Chưa")}\n" +
            $"Ghi chú               : {m.GhiChu ?? "(không có)"}");
    }

    private async Task PayInvoiceAsync(Invoice invoice)
    {
        var detail = await _invoiceService.GetDetailAsync(invoice.MaHoaDon) ?? invoice;
        if (detail.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan)
        {
            Msg.Info(this, $"Hóa đơn {detail.MaHoaDonKyHieu} đã thanh toán — không thể thu thêm.\n\n" +
                           "Bấm '🔍 Chi tiết' để xem phiếu thu.");
            return;
        }

        using var dlg = new PaymentDialog(_paymentService, detail);
        if (dlg.ShowDialog(this) == DialogResult.OK) await ReloadCurrentAsync();
    }

    private static string Cut(string text, int max) => text.Length <= max ? text : text[..(max - 1)] + "…";
}
