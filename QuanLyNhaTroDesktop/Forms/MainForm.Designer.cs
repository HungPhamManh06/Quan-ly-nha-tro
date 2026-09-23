// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA MainForm (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
//
//  ĐƠN VỊ Ở ĐÂY LÀ ĐƠN VỊ THIẾT KẾ 96 DPI — lúc chạy, app tự nhân kích thước theo
//  tỉ lệ màn hình (100% / 125% / 150% / 200%) qua Dpi.ScaleForm. Vì vậy:
//    • Sửa VỊ TRÍ / KÍCH THƯỚC / CHỮ / MÀU thoải mái — lúc chạy vẫn đúng.
//    • KHÔNG đổi AutoScaleMode (phải là None), nếu không sẽ bị nhân kích thước 2 lần.
//    • Cỡ chữ khai bằng point (pt) nên tự đúng ở mọi DPI — không cần tự nhân.
//    • Thêm control mới: kéo từ Toolbox thả vào đây là được, app tự nhân DPI lúc chạy.
// ------------------------------------------------------------------------------
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // ===== Menu =====
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuLamMoi;
        private System.Windows.Forms.ToolStripSeparator mnuSepHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuNghiepVu;
        private System.Windows.Forms.ToolStripMenuItem mnuLapHopDong;
        private System.Windows.Forms.ToolStripMenuItem mnuGhiDienNuoc;
        private System.Windows.Forms.ToolStripMenuItem mnuLapHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuGhiSuDungDichVu;
        private System.Windows.Forms.ToolStripSeparator mnuSepNghiepVu;
        private System.Windows.Forms.ToolStripMenuItem mnuTraPhong;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuGioiThieu;
        private System.Windows.Forms.ToolStripMenuItem mnuTaiKhoan;

        // ===== Thanh trạng thái =====
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;

        // ===== Khung tab =====
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabTongQuan;
        private System.Windows.Forms.TabPage tabPhong;
        private System.Windows.Forms.TabPage tabNguoiThue;
        private System.Windows.Forms.TabPage tabHopDong;
        private System.Windows.Forms.TabPage tabDienNuoc;
        private System.Windows.Forms.TabPage tabDichVu;
        private System.Windows.Forms.TabPage tabSuDungDichVu;
        private System.Windows.Forms.TabPage tabHoaDon;
        private System.Windows.Forms.TabPage tabThanhToan;
        private System.Windows.Forms.TabPage tabTraPhong;
        private System.Windows.Forms.TabPage tabBaoCao;

        // ===== Nội dung từng tab =====
        private DashboardPanel panelTongQuan;
        private ListTabPanel panelPhong;
        private ListTabPanel panelNguoiThue;
        private ListTabPanel panelHopDong;
        private ListTabPanel panelDienNuoc;
        private ListTabPanel panelDichVu;
        private ListTabPanel panelSuDungDichVu;
        private ListTabPanel panelHoaDon;
        private ListTabPanel panelThanhToan;
        private ListTabPanel panelTraPhong;
        private ReportPanel panelBaoCao;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLamMoi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSepHeThong = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNghiepVu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLapHopDong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGhiDienNuoc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLapHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGhiSuDungDichVu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSepNghiepVu = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTraPhong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGioiThieu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.status = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabTongQuan = new System.Windows.Forms.TabPage();
            this.panelTongQuan = new QuanLyNhaTroDesktop.Forms.DashboardPanel();
            this.tabPhong = new System.Windows.Forms.TabPage();
            this.panelPhong = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabNguoiThue = new System.Windows.Forms.TabPage();
            this.panelNguoiThue = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabHopDong = new System.Windows.Forms.TabPage();
            this.panelHopDong = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabDienNuoc = new System.Windows.Forms.TabPage();
            this.panelDienNuoc = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabDichVu = new System.Windows.Forms.TabPage();
            this.panelDichVu = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabSuDungDichVu = new System.Windows.Forms.TabPage();
            this.panelSuDungDichVu = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabHoaDon = new System.Windows.Forms.TabPage();
            this.panelHoaDon = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabThanhToan = new System.Windows.Forms.TabPage();
            this.panelThanhToan = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabTraPhong = new System.Windows.Forms.TabPage();
            this.panelTraPhong = new QuanLyNhaTroDesktop.Forms.ListTabPanel();
            this.tabBaoCao = new System.Windows.Forms.TabPage();
            this.panelBaoCao = new QuanLyNhaTroDesktop.Forms.ReportPanel();
            this.menu.SuspendLayout();
            this.status.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabTongQuan.SuspendLayout();
            this.tabPhong.SuspendLayout();
            this.tabNguoiThue.SuspendLayout();
            this.tabHopDong.SuspendLayout();
            this.tabDienNuoc.SuspendLayout();
            this.tabDichVu.SuspendLayout();
            this.tabSuDungDichVu.SuspendLayout();
            this.tabHoaDon.SuspendLayout();
            this.tabThanhToan.SuspendLayout();
            this.tabTraPhong.SuspendLayout();
            this.tabBaoCao.SuspendLayout();
            this.SuspendLayout();
            //
            // mnuHeThong
            //
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLamMoi,
            this.mnuSepHeThong,
            this.mnuDangXuat,
            this.mnuThoat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(80, 26);
            this.mnuHeThong.Text = "Hệ thống";
            //
            // mnuLamMoi
            //
            this.mnuLamMoi.Name = "mnuLamMoi";
            this.mnuLamMoi.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuLamMoi.Size = new System.Drawing.Size(200, 26);
            this.mnuLamMoi.Text = "🔄 Làm mới màn hình";
            //
            // mnuSepHeThong
            //
            this.mnuSepHeThong.Name = "mnuSepHeThong";
            this.mnuSepHeThong.Size = new System.Drawing.Size(197, 6);
            //
            // mnuDangXuat
            //
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(200, 26);
            this.mnuDangXuat.Text = "🚪 Đăng xuất";
            //
            // mnuThoat
            //
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(200, 26);
            this.mnuThoat.Text = "❌ Thoát";
            //
            // mnuNghiepVu
            //
            this.mnuNghiepVu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLapHopDong,
            this.mnuGhiDienNuoc,
            this.mnuLapHoaDon,
            this.mnuGhiSuDungDichVu,
            this.mnuSepNghiepVu,
            this.mnuTraPhong});
            this.mnuNghiepVu.Name = "mnuNghiepVu";
            this.mnuNghiepVu.Size = new System.Drawing.Size(88, 26);
            this.mnuNghiepVu.Text = "Nghiệp vụ";
            //
            // mnuLapHopDong
            //
            this.mnuLapHopDong.Name = "mnuLapHopDong";
            this.mnuLapHopDong.Size = new System.Drawing.Size(260, 26);
            this.mnuLapHopDong.Text = "Lập hợp đồng mới";
            //
            // mnuGhiDienNuoc
            //
            this.mnuGhiDienNuoc.Name = "mnuGhiDienNuoc";
            this.mnuGhiDienNuoc.Size = new System.Drawing.Size(260, 26);
            this.mnuGhiDienNuoc.Text = "Ghi chỉ số điện nước";
            //
            // mnuLapHoaDon
            //
            this.mnuLapHoaDon.Name = "mnuLapHoaDon";
            this.mnuLapHoaDon.Size = new System.Drawing.Size(260, 26);
            this.mnuLapHoaDon.Text = "Lập hóa đơn tháng";
            //
            // mnuGhiSuDungDichVu
            //
            this.mnuGhiSuDungDichVu.Name = "mnuGhiSuDungDichVu";
            this.mnuGhiSuDungDichVu.Size = new System.Drawing.Size(260, 26);
            this.mnuGhiSuDungDichVu.Text = "Ghi nhận sử dụng dịch vụ";
            //
            // mnuSepNghiepVu
            //
            this.mnuSepNghiepVu.Name = "mnuSepNghiepVu";
            this.mnuSepNghiepVu.Size = new System.Drawing.Size(257, 6);
            //
            // mnuTraPhong
            //
            this.mnuTraPhong.Name = "mnuTraPhong";
            this.mnuTraPhong.Size = new System.Drawing.Size(260, 26);
            this.mnuTraPhong.Text = "Trả phòng & quyết toán";
            //
            // mnuTroGiup
            //
            this.mnuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGioiThieu,
            this.mnuTaiKhoan});
            this.mnuTroGiup.Name = "mnuTroGiup";
            this.mnuTroGiup.Size = new System.Drawing.Size(74, 26);
            this.mnuTroGiup.Text = "Trợ giúp";
            //
            // mnuGioiThieu
            //
            this.mnuGioiThieu.Name = "mnuGioiThieu";
            this.mnuGioiThieu.Size = new System.Drawing.Size(220, 26);
            this.mnuGioiThieu.Text = "Giới thiệu";
            //
            // mnuTaiKhoan
            //
            this.mnuTaiKhoan.Name = "mnuTaiKhoan";
            this.mnuTaiKhoan.Size = new System.Drawing.Size(220, 26);
            this.mnuTaiKhoan.Text = "Tài khoản đang dùng";
            //
            // menu
            //
            this.menu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuNghiepVu,
            this.mnuTroGiup});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(1280, 30);
            this.menu.TabIndex = 0;
            //
            // lblStatus
            //
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(70, 20);
            this.lblStatus.Text = "Sẵn sàng";
            //
            // status
            //
            this.status.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.status.Location = new System.Drawing.Point(0, 738);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1280, 22);
            this.status.TabIndex = 2;
            //
            // panelTongQuan — màn Tổng quan (thẻ số liệu + biểu đồ + việc cần xử lý)
            //
            this.panelTongQuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTongQuan.Location = new System.Drawing.Point(3, 3);
            this.panelTongQuan.Name = "panelTongQuan";
            this.panelTongQuan.Size = new System.Drawing.Size(1266, 700);
            this.panelTongQuan.TabIndex = 0;
            //
            // panelPhong
            //
            this.panelPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPhong.Location = new System.Drawing.Point(6, 6);
            this.panelPhong.Name = "panelPhong";
            this.panelPhong.Size = new System.Drawing.Size(1260, 694);
            this.panelPhong.TabIndex = 0;
            this.panelPhong.TabKey = "Phòng";
            //
            // panelNguoiThue
            //
            this.panelNguoiThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNguoiThue.Location = new System.Drawing.Point(6, 6);
            this.panelNguoiThue.Name = "panelNguoiThue";
            this.panelNguoiThue.Size = new System.Drawing.Size(1260, 694);
            this.panelNguoiThue.TabIndex = 0;
            this.panelNguoiThue.TabKey = "Người thuê";
            //
            // panelHopDong
            //
            this.panelHopDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHopDong.Location = new System.Drawing.Point(6, 6);
            this.panelHopDong.Name = "panelHopDong";
            this.panelHopDong.Size = new System.Drawing.Size(1260, 694);
            this.panelHopDong.TabIndex = 0;
            this.panelHopDong.TabKey = "Hợp đồng";
            //
            // panelDienNuoc
            //
            this.panelDienNuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDienNuoc.Location = new System.Drawing.Point(6, 6);
            this.panelDienNuoc.Name = "panelDienNuoc";
            this.panelDienNuoc.Size = new System.Drawing.Size(1260, 694);
            this.panelDienNuoc.TabIndex = 0;
            this.panelDienNuoc.TabKey = "Điện nước";
            //
            // panelDichVu
            //
            this.panelDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDichVu.Location = new System.Drawing.Point(6, 6);
            this.panelDichVu.Name = "panelDichVu";
            this.panelDichVu.Size = new System.Drawing.Size(1260, 694);
            this.panelDichVu.TabIndex = 0;
            this.panelDichVu.TabKey = "Dịch vụ";
            //
            // panelSuDungDichVu
            //
            this.panelSuDungDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSuDungDichVu.Location = new System.Drawing.Point(6, 6);
            this.panelSuDungDichVu.Name = "panelSuDungDichVu";
            this.panelSuDungDichVu.Size = new System.Drawing.Size(1260, 694);
            this.panelSuDungDichVu.TabIndex = 0;
            this.panelSuDungDichVu.TabKey = "Sử dụng dịch vụ";
            //
            // panelHoaDon
            //
            this.panelHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHoaDon.Location = new System.Drawing.Point(6, 6);
            this.panelHoaDon.Name = "panelHoaDon";
            this.panelHoaDon.Size = new System.Drawing.Size(1260, 694);
            this.panelHoaDon.TabIndex = 0;
            this.panelHoaDon.TabKey = "Hóa đơn";
            //
            // panelThanhToan
            //
            this.panelThanhToan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelThanhToan.Location = new System.Drawing.Point(6, 6);
            this.panelThanhToan.Name = "panelThanhToan";
            this.panelThanhToan.Size = new System.Drawing.Size(1260, 694);
            this.panelThanhToan.TabIndex = 0;
            this.panelThanhToan.TabKey = "Thanh toán";
            //
            // panelTraPhong
            //
            this.panelTraPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTraPhong.Location = new System.Drawing.Point(6, 6);
            this.panelTraPhong.Name = "panelTraPhong";
            this.panelTraPhong.Size = new System.Drawing.Size(1260, 694);
            this.panelTraPhong.TabIndex = 0;
            this.panelTraPhong.TabKey = "Trả phòng";
            //
            // panelBaoCao — màn Báo cáo (7 loại báo cáo + xuất Excel/CSV)
            //
            this.panelBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBaoCao.Location = new System.Drawing.Point(3, 3);
            this.panelBaoCao.Name = "panelBaoCao";
            this.panelBaoCao.Size = new System.Drawing.Size(1266, 700);
            this.panelBaoCao.TabIndex = 0;
            //
            // tabTongQuan
            //
            this.tabTongQuan.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.tabTongQuan.Controls.Add(this.panelTongQuan);
            this.tabTongQuan.Location = new System.Drawing.Point(4, 32);
            this.tabTongQuan.Name = "tabTongQuan";
            this.tabTongQuan.Padding = new System.Windows.Forms.Padding(3);
            this.tabTongQuan.Size = new System.Drawing.Size(1272, 706);
            this.tabTongQuan.TabIndex = 0;
            this.tabTongQuan.Text = "📊 Tổng quan";
            //
            // tabPhong
            //
            this.tabPhong.BackColor = System.Drawing.Color.White;
            this.tabPhong.Controls.Add(this.panelPhong);
            this.tabPhong.Location = new System.Drawing.Point(4, 32);
            this.tabPhong.Name = "tabPhong";
            this.tabPhong.Padding = new System.Windows.Forms.Padding(6);
            this.tabPhong.Size = new System.Drawing.Size(1272, 706);
            this.tabPhong.TabIndex = 1;
            this.tabPhong.Text = "Phòng";
            //
            // tabNguoiThue
            //
            this.tabNguoiThue.BackColor = System.Drawing.Color.White;
            this.tabNguoiThue.Controls.Add(this.panelNguoiThue);
            this.tabNguoiThue.Location = new System.Drawing.Point(4, 32);
            this.tabNguoiThue.Name = "tabNguoiThue";
            this.tabNguoiThue.Padding = new System.Windows.Forms.Padding(6);
            this.tabNguoiThue.Size = new System.Drawing.Size(1272, 706);
            this.tabNguoiThue.TabIndex = 2;
            this.tabNguoiThue.Text = "Người thuê";
            //
            // tabHopDong
            //
            this.tabHopDong.BackColor = System.Drawing.Color.White;
            this.tabHopDong.Controls.Add(this.panelHopDong);
            this.tabHopDong.Location = new System.Drawing.Point(4, 32);
            this.tabHopDong.Name = "tabHopDong";
            this.tabHopDong.Padding = new System.Windows.Forms.Padding(6);
            this.tabHopDong.Size = new System.Drawing.Size(1272, 706);
            this.tabHopDong.TabIndex = 3;
            this.tabHopDong.Text = "Hợp đồng";
            //
            // tabDienNuoc
            //
            this.tabDienNuoc.BackColor = System.Drawing.Color.White;
            this.tabDienNuoc.Controls.Add(this.panelDienNuoc);
            this.tabDienNuoc.Location = new System.Drawing.Point(4, 32);
            this.tabDienNuoc.Name = "tabDienNuoc";
            this.tabDienNuoc.Padding = new System.Windows.Forms.Padding(6);
            this.tabDienNuoc.Size = new System.Drawing.Size(1272, 706);
            this.tabDienNuoc.TabIndex = 4;
            this.tabDienNuoc.Text = "Điện nước";
            //
            // tabDichVu
            //
            this.tabDichVu.BackColor = System.Drawing.Color.White;
            this.tabDichVu.Controls.Add(this.panelDichVu);
            this.tabDichVu.Location = new System.Drawing.Point(4, 32);
            this.tabDichVu.Name = "tabDichVu";
            this.tabDichVu.Padding = new System.Windows.Forms.Padding(6);
            this.tabDichVu.Size = new System.Drawing.Size(1272, 706);
            this.tabDichVu.TabIndex = 5;
            this.tabDichVu.Text = "Dịch vụ";
            //
            // tabSuDungDichVu
            //
            this.tabSuDungDichVu.BackColor = System.Drawing.Color.White;
            this.tabSuDungDichVu.Controls.Add(this.panelSuDungDichVu);
            this.tabSuDungDichVu.Location = new System.Drawing.Point(4, 32);
            this.tabSuDungDichVu.Name = "tabSuDungDichVu";
            this.tabSuDungDichVu.Padding = new System.Windows.Forms.Padding(6);
            this.tabSuDungDichVu.Size = new System.Drawing.Size(1272, 706);
            this.tabSuDungDichVu.TabIndex = 6;
            this.tabSuDungDichVu.Text = "Sử dụng dịch vụ";
            //
            // tabHoaDon
            //
            this.tabHoaDon.BackColor = System.Drawing.Color.White;
            this.tabHoaDon.Controls.Add(this.panelHoaDon);
            this.tabHoaDon.Location = new System.Drawing.Point(4, 32);
            this.tabHoaDon.Name = "tabHoaDon";
            this.tabHoaDon.Padding = new System.Windows.Forms.Padding(6);
            this.tabHoaDon.Size = new System.Drawing.Size(1272, 706);
            this.tabHoaDon.TabIndex = 7;
            this.tabHoaDon.Text = "Hóa đơn";
            //
            // tabThanhToan
            //
            this.tabThanhToan.BackColor = System.Drawing.Color.White;
            this.tabThanhToan.Controls.Add(this.panelThanhToan);
            this.tabThanhToan.Location = new System.Drawing.Point(4, 32);
            this.tabThanhToan.Name = "tabThanhToan";
            this.tabThanhToan.Padding = new System.Windows.Forms.Padding(6);
            this.tabThanhToan.Size = new System.Drawing.Size(1272, 706);
            this.tabThanhToan.TabIndex = 8;
            this.tabThanhToan.Text = "Thanh toán";
            //
            // tabTraPhong
            //
            this.tabTraPhong.BackColor = System.Drawing.Color.White;
            this.tabTraPhong.Controls.Add(this.panelTraPhong);
            this.tabTraPhong.Location = new System.Drawing.Point(4, 32);
            this.tabTraPhong.Name = "tabTraPhong";
            this.tabTraPhong.Padding = new System.Windows.Forms.Padding(6);
            this.tabTraPhong.Size = new System.Drawing.Size(1272, 706);
            this.tabTraPhong.TabIndex = 9;
            this.tabTraPhong.Text = "Trả phòng";
            //
            // tabBaoCao
            //
            this.tabBaoCao.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.tabBaoCao.Controls.Add(this.panelBaoCao);
            this.tabBaoCao.Location = new System.Drawing.Point(4, 32);
            this.tabBaoCao.Name = "tabBaoCao";
            this.tabBaoCao.Padding = new System.Windows.Forms.Padding(3);
            this.tabBaoCao.Size = new System.Drawing.Size(1272, 706);
            this.tabBaoCao.TabIndex = 10;
            this.tabBaoCao.Text = "📈 Báo cáo";
            //
            // tabs
            //
            this.tabs.Controls.Add(this.tabTongQuan);
            this.tabs.Controls.Add(this.tabPhong);
            this.tabs.Controls.Add(this.tabNguoiThue);
            this.tabs.Controls.Add(this.tabHopDong);
            this.tabs.Controls.Add(this.tabDienNuoc);
            this.tabs.Controls.Add(this.tabDichVu);
            this.tabs.Controls.Add(this.tabSuDungDichVu);
            this.tabs.Controls.Add(this.tabHoaDon);
            this.tabs.Controls.Add(this.tabThanhToan);
            this.tabs.Controls.Add(this.tabTraPhong);
            this.tabs.Controls.Add(this.tabBaoCao);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.tabs.Location = new System.Drawing.Point(0, 30);
            this.tabs.Name = "tabs";
            this.tabs.Padding = new System.Drawing.Point(14, 6);
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1280, 708);
            this.tabs.TabIndex = 1;
            //
            // MainForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.status);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý nhà trọ - Bản desktop (Windows Forms)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabTongQuan.ResumeLayout(false);
            this.tabPhong.ResumeLayout(false);
            this.tabNguoiThue.ResumeLayout(false);
            this.tabHopDong.ResumeLayout(false);
            this.tabDienNuoc.ResumeLayout(false);
            this.tabDichVu.ResumeLayout(false);
            this.tabSuDungDichVu.ResumeLayout(false);
            this.tabHoaDon.ResumeLayout(false);
            this.tabThanhToan.ResumeLayout(false);
            this.tabTraPhong.ResumeLayout(false);
            this.tabBaoCao.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
