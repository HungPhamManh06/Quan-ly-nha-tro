namespace QuanLyNhaTroDesktop.Forms;

/// <summary>
/// Một tab dạng danh sách: bảng dữ liệu (ListView) + thanh nút Thêm / Sửa / Xóa / Chi tiết / Làm mới.
///
/// Dùng cho 9 tab nghiệp vụ của cửa sổ chính (Phòng, Người thuê, Hợp đồng, Điện nước, Dịch vụ,
/// Sử dụng dịch vụ, Hóa đơn, Thanh toán, Trả phòng) — mỗi tab là MỘT instance của UserControl này,
/// nên chỉ có một nơi định nghĩa giao diện (ListTabPanel.Designer.cs) và sửa một lần là đổi cả 9 tab.
///
/// GIAO DIỆN: ListTabPanel.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class ListTabPanel : UserControl
{
    public ListTabPanel()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Tên nghiệp vụ của tab ("Phòng", "Hợp đồng"...). Cửa sổ chính dựa vào giá trị này để biết
    /// cần nạp dữ liệu gì. Đặt được ngay trong Visual Studio Designer (cửa sổ Properties).
    /// </summary>
    public string TabKey { get; set; } = "";

    /// <summary>Bảng dữ liệu để cửa sổ chính đổ dữ liệu vào.</summary>
    public ListView List => list;

    /// <summary>Nút Thêm (dùng khi tab cho phép thêm mới).</summary>
    public Button AddButton => btnAdd;
    public Button EditButton => btnEdit;
    public Button DeleteButton => btnDelete;
    public Button DetailButton => btnDetail;
    public Button RefreshButton => btnRefresh;

    /// <summary>Ẩn/hiện nhóm nút theo nghiệp vụ của tab (Thanh toán chỉ xem, Trả phòng không sửa/xóa...).</summary>
    public void ShowActions(bool canAdd = true, bool canEdit = true, bool canDelete = true, bool canDetail = true)
    {
        btnAdd.Visible = canAdd;
        btnEdit.Visible = canEdit;
        btnDelete.Visible = canDelete;
        btnDetail.Visible = canDetail;
    }
}
