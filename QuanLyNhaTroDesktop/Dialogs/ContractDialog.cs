using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog lập hợp đồng thuê phòng mới (chỉ tạo mới).
///
/// GIAO DIỆN: ContractDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class ContractDialog : Form
{
    private readonly IContractService? _contractService;
    private readonly IRoomService? _roomService;
    private readonly ITenantService? _tenantService;

    private List<Room> _rooms = new();
    private List<Tenant> _tenants = new();

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public ContractDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public ContractDialog(Contract? existing, IContractService contractService,
        IRoomService roomService, ITenantService tenantService) : this()
    {
        _contractService = contractService;
        _roomService = roomService;
        _tenantService = tenantService;

        AcceptButton = btnOk;
        CancelButton = btnCancel;
        btnOk.Click += BtnOk_Click;
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };

        dtpStart.Value = DateTime.Today;
        dtpEnd.Value = DateTime.Today.AddYears(1).AddDays(-1);

        Load += async (_, _) => await LoadAsync();
    }

    private async Task LoadAsync()
    {
        try
        {
            _rooms = await _roomService!.GetAllAsync();
            cboRoom.DisplayMember = nameof(Room.MaPhongKyHieu);
            cboRoom.DataSource = _rooms;

            var tenantItems = await _tenantService!.GetAllAsync();
            _tenants = tenantItems.Select(t => t.Tenant).ToList();
            cboTenant.DisplayMember = nameof(Tenant.HoTen);
            cboTenant.DataSource = _tenants;

            cboRoom.SelectedIndexChanged += (_, _) =>
            {
                if (cboRoom.SelectedItem is Room r) txtGia.Text = r.GiaPhong.ToString();
            };
            if (cboRoom.SelectedItem is Room first) txtGia.Text = first.GiaPhong.ToString();
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi tải dữ liệu: " + ex.Message);
        }
    }

    private async void BtnOk_Click(object? sender, EventArgs e)
    {
        if (cboRoom.SelectedItem is not Room room || cboTenant.SelectedItem is not Tenant tenant)
        {
            Msg.Warn(this, "Vui lòng chọn phòng và người thuê.", "Thiếu dữ liệu");
            return;
        }
        if (!decimal.TryParse(txtGia.Text, out var gia) || gia <= 0)
        {
            Msg.Warn(this, "Giá thuê phải là số lớn hơn 0.", "Sai định dạng");
            return;
        }
        if (!decimal.TryParse(txtCoc.Text, out var coc) || coc < 0)
        {
            Msg.Warn(this, "Tiền cọc phải là số lớn hơn hoặc bằng 0.", "Sai định dạng");
            return;
        }

        var contract = new Contract
        {
            MaPhong = room.MaPhong,
            MaNguoiThue = tenant.MaNguoiThue,
            NgayBatDau = dtpStart.Value.Date,
            NgayKetThuc = dtpEnd.Value.Date,
            GiaThue = gia,
            TienCoc = coc,
        };

        try
        {
            var (ok, message) = await _contractService!.CreateAsync(contract);
            Msg.Info(this, message, ok ? "Thành công" : "Không thể lập");
            if (ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
