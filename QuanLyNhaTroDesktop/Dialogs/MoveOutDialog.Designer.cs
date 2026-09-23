// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA MoveOutDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
//
//  ĐƠN VỊ Ở ĐÂY LÀ ĐƠN VỊ THIẾT KẾ 96 DPI — lúc chạy, app tự nhân kích thước theo
//  tỉ lệ màn hình (100% / 125% / 150% / 200%) qua Dpi.ScaleForm. Vì vậy:
//    • Sửa VỊ TRÍ / KÍCH THƯỚC / CHỮ / MÀU thoải mái — lúc chạy vẫn đúng.
//    • KHÔNG đổi AutoScaleMode (phải là None), nếu không sẽ bị nhân kích thước 2 lần.
//    • Cỡ chữ khai bằng point (pt) nên tự đúng ở mọi DPI — không cần tự nhân.
//    • Thêm control mới: kéo từ Toolbox thả vào đây là được, app tự nhân DPI lúc chạy.
// ------------------------------------------------------------------------------
namespace QuanLyNhaTroDesktop.Dialogs
{
    partial class MoveOutDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblHopDongCanTraPhong;
        private System.Windows.Forms.ComboBox cboContract;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabDienNuoc;
        private System.Windows.Forms.TableLayoutPanel layoutDienNuoc;
        private System.Windows.Forms.Label lblHuongDanChiSo;
        private System.Windows.Forms.Label lblChiSoDienMoiKw;
        private System.Windows.Forms.TextBox txtDienMoi;
        private System.Windows.Forms.Label lblChiSoNuocMoiM;
        private System.Windows.Forms.TextBox txtNuocMoi;
        private System.Windows.Forms.Label lblDonGiaDienD;
        private System.Windows.Forms.TextBox txtGiaDien;
        private System.Windows.Forms.Label lblDonGiaNuocD;
        private System.Windows.Forms.TextBox txtGiaNuoc;
        private System.Windows.Forms.FlowLayoutPanel buttonRowDienNuoc;
        private System.Windows.Forms.Button btnChotDienNuoc;
        private System.Windows.Forms.Label lblChotInfo;
        private System.Windows.Forms.TabPage tabDichVu;
        private System.Windows.Forms.DataGridView gridUsages;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDichVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonVi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.Panel panelDichVuBottom;
        private System.Windows.Forms.Label lblDichVuTong;
        private System.Windows.Forms.Panel panelDichVuTop;
        private System.Windows.Forms.Button btnThemDichVu;
        private System.Windows.Forms.TabPage tabQuyetToan;
        private System.Windows.Forms.TableLayoutPanel layoutQuyetToan;
        private System.Windows.Forms.Label lblNgayTraPhong;
        private System.Windows.Forms.DateTimePicker dtpNgayTra;
        private System.Windows.Forms.Label lblTrangThaiPhongSauTra;
        private System.Windows.Forms.ComboBox cboTrangThaiSau;
        private System.Windows.Forms.Label lblPhatSinhKhacD;
        private System.Windows.Forms.TextBox txtPhatSinhKhac;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.FlowLayoutPanel buttonRowPreview;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.FlowLayoutPanel buttonRowBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSettle;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveOutDialog));
            layout = new TableLayoutPanel();
            lblTieuDe = new Label();
            lblHopDongCanTraPhong = new Label();
            cboContract = new ComboBox();
            lblInfo = new Label();
            tabs = new TabControl();
            tabDienNuoc = new TabPage();
            layoutDienNuoc = new TableLayoutPanel();
            lblHuongDanChiSo = new Label();
            lblChiSoDienMoiKw = new Label();
            txtDienMoi = new TextBox();
            lblChiSoNuocMoiM = new Label();
            txtNuocMoi = new TextBox();
            lblDonGiaDienD = new Label();
            txtGiaDien = new TextBox();
            lblDonGiaNuocD = new Label();
            txtGiaNuoc = new TextBox();
            buttonRowDienNuoc = new FlowLayoutPanel();
            btnChotDienNuoc = new Button();
            lblChotInfo = new Label();
            tabDichVu = new TabPage();
            gridUsages = new DataGridView();
            colDichVu = new DataGridViewTextBoxColumn();
            colSoLuong = new DataGridViewTextBoxColumn();
            colDonVi = new DataGridViewTextBoxColumn();
            colDonGia = new DataGridViewTextBoxColumn();
            colThanhTien = new DataGridViewTextBoxColumn();
            panelDichVuBottom = new Panel();
            lblDichVuTong = new Label();
            panelDichVuTop = new Panel();
            btnThemDichVu = new Button();
            tabQuyetToan = new TabPage();
            layoutQuyetToan = new TableLayoutPanel();
            lblNgayTraPhong = new Label();
            dtpNgayTra = new DateTimePicker();
            lblTrangThaiPhongSauTra = new Label();
            cboTrangThaiSau = new ComboBox();
            lblPhatSinhKhacD = new Label();
            txtPhatSinhKhac = new TextBox();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            buttonRowPreview = new FlowLayoutPanel();
            btnPreview = new Button();
            txtPreview = new TextBox();
            buttonRowBottom = new FlowLayoutPanel();
            btnClose = new Button();
            btnSettle = new Button();
            layout.SuspendLayout();
            tabs.SuspendLayout();
            tabDienNuoc.SuspendLayout();
            layoutDienNuoc.SuspendLayout();
            buttonRowDienNuoc.SuspendLayout();
            tabDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridUsages).BeginInit();
            panelDichVuBottom.SuspendLayout();
            panelDichVuTop.SuspendLayout();
            tabQuyetToan.SuspendLayout();
            layoutQuyetToan.SuspendLayout();
            buttonRowPreview.SuspendLayout();
            buttonRowBottom.SuspendLayout();
            SuspendLayout();
            // 
            // layout
            // 
            layout.AutoScroll = true;
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            layout.Controls.Add(lblTieuDe, 0, 0);
            layout.Controls.Add(lblHopDongCanTraPhong, 0, 1);
            layout.Controls.Add(cboContract, 1, 1);
            layout.Controls.Add(lblInfo, 0, 2);
            layout.Controls.Add(tabs, 0, 3);
            layout.Controls.Add(buttonRowBottom, 0, 4);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.Padding = new Padding(7, 6, 7, 6);
            layout.RowCount = 5;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 107F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 470F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            layout.Size = new Size(900, 700);
            layout.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            layout.SetColumnSpan(lblTieuDe, 2);
            lblTieuDe.Dock = DockStyle.Fill;
            lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(37, 99, 235);
            lblTieuDe.Location = new Point(7, 6);
            lblTieuDe.Margin = new Padding(0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(886, 30);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUY TRÌNH TRẢ PHÒNG & QUYẾT TOÁN";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHopDongCanTraPhong
            // 
            lblHopDongCanTraPhong.Dock = DockStyle.Fill;
            lblHopDongCanTraPhong.Location = new Point(8, 36);
            lblHopDongCanTraPhong.Margin = new Padding(1, 0, 4, 0);
            lblHopDongCanTraPhong.Name = "lblHopDongCanTraPhong";
            lblHopDongCanTraPhong.Size = new Size(367, 24);
            lblHopDongCanTraPhong.TabIndex = 1;
            lblHopDongCanTraPhong.Text = "Hợp đồng cần trả phòng:";
            lblHopDongCanTraPhong.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboContract
            // 
            cboContract.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboContract.DropDownStyle = ComboBoxStyle.DropDownList;
            cboContract.Location = new Point(380, 38);
            cboContract.Margin = new Padding(1, 2, 1, 2);
            cboContract.Name = "cboContract";
            cboContract.Size = new Size(512, 28);
            cboContract.TabIndex = 2;
            // 
            // lblInfo
            // 
            layout.SetColumnSpan(lblInfo, 2);
            lblInfo.Dock = DockStyle.Fill;
            lblInfo.Font = new Font("Consolas", 10F);
            lblInfo.ForeColor = Color.FromArgb(31, 41, 55);
            lblInfo.Location = new Point(8, 62);
            lblInfo.Margin = new Padding(1, 2, 1, 2);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(884, 103);
            lblInfo.TabIndex = 3;
            lblInfo.Text = resources.GetString("lblInfo.Text");
            // 
            // tabs
            // 
            layout.SetColumnSpan(tabs, 2);
            tabs.Controls.Add(tabDienNuoc);
            tabs.Controls.Add(tabDichVu);
            tabs.Controls.Add(tabQuyetToan);
            tabs.Dock = DockStyle.Fill;
            tabs.Font = new Font("Segoe UI", 10.5F);
            tabs.Location = new Point(8, 169);
            tabs.Margin = new Padding(1, 2, 1, 2);
            tabs.Name = "tabs";
            tabs.Padding = new Point(2, 1);
            tabs.SelectedIndex = 0;
            tabs.Size = new Size(884, 466);
            tabs.TabIndex = 4;
            // 
            // tabDienNuoc
            // 
            tabDienNuoc.BackColor = Color.FromArgb(255, 255, 255);
            tabDienNuoc.Controls.Add(layoutDienNuoc);
            tabDienNuoc.Font = new Font("Segoe UI", 10.5F);
            tabDienNuoc.Location = new Point(4, 26);
            tabDienNuoc.Name = "tabDienNuoc";
            tabDienNuoc.Padding = new Padding(10);
            tabDienNuoc.Size = new Size(876, 436);
            tabDienNuoc.TabIndex = 0;
            tabDienNuoc.Text = "1. Chốt điện nước cuối kỳ";
            // 
            // layoutDienNuoc
            // 
            layoutDienNuoc.AutoScroll = true;
            layoutDienNuoc.ColumnCount = 2;
            layoutDienNuoc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            layoutDienNuoc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            layoutDienNuoc.Controls.Add(lblHuongDanChiSo, 0, 0);
            layoutDienNuoc.Controls.Add(lblChiSoDienMoiKw, 0, 1);
            layoutDienNuoc.Controls.Add(txtDienMoi, 1, 1);
            layoutDienNuoc.Controls.Add(lblChiSoNuocMoiM, 0, 2);
            layoutDienNuoc.Controls.Add(txtNuocMoi, 1, 2);
            layoutDienNuoc.Controls.Add(lblDonGiaDienD, 0, 3);
            layoutDienNuoc.Controls.Add(txtGiaDien, 1, 3);
            layoutDienNuoc.Controls.Add(lblDonGiaNuocD, 0, 4);
            layoutDienNuoc.Controls.Add(txtGiaNuoc, 1, 4);
            layoutDienNuoc.Controls.Add(buttonRowDienNuoc, 0, 5);
            layoutDienNuoc.Controls.Add(lblChotInfo, 0, 6);
            layoutDienNuoc.Dock = DockStyle.Fill;
            layoutDienNuoc.Font = new Font("Segoe UI", 10.5F);
            layoutDienNuoc.Location = new Point(10, 10);
            layoutDienNuoc.Name = "layoutDienNuoc";
            layoutDienNuoc.Padding = new Padding(7, 6, 7, 6);
            layoutDienNuoc.RowCount = 7;
            layoutDienNuoc.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutDienNuoc.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutDienNuoc.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutDienNuoc.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutDienNuoc.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutDienNuoc.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            layoutDienNuoc.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            layoutDienNuoc.Size = new Size(856, 416);
            layoutDienNuoc.TabIndex = 0;
            // 
            // lblHuongDanChiSo
            // 
            layoutDienNuoc.SetColumnSpan(lblHuongDanChiSo, 2);
            lblHuongDanChiSo.Dock = DockStyle.Fill;
            lblHuongDanChiSo.Font = new Font("Segoe UI", 10F);
            lblHuongDanChiSo.ForeColor = Color.FromArgb(107, 114, 128);
            lblHuongDanChiSo.Location = new Point(8, 8);
            lblHuongDanChiSo.Margin = new Padding(1, 2, 1, 2);
            lblHuongDanChiSo.Name = "lblHuongDanChiSo";
            lblHuongDanChiSo.Size = new Size(840, 40);
            lblHuongDanChiSo.TabIndex = 0;
            lblHuongDanChiSo.Text = "Nhập chỉ số mới trên đồng hồ tại thời điểm trả phòng. Chỉ số mới phải lớn hơn hoặc bằng chỉ số cũ.";
            lblHuongDanChiSo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblChiSoDienMoiKw
            // 
            lblChiSoDienMoiKw.Dock = DockStyle.Fill;
            lblChiSoDienMoiKw.Font = new Font("Segoe UI", 10.5F);
            lblChiSoDienMoiKw.Location = new Point(8, 50);
            lblChiSoDienMoiKw.Margin = new Padding(1, 0, 4, 0);
            lblChiSoDienMoiKw.Name = "lblChiSoDienMoiKw";
            lblChiSoDienMoiKw.Size = new Size(348, 24);
            lblChiSoDienMoiKw.TabIndex = 1;
            lblChiSoDienMoiKw.Text = "Chỉ số điện mới (kW):";
            lblChiSoDienMoiKw.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDienMoi
            // 
            txtDienMoi.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtDienMoi.Font = new Font("Segoe UI", 10.5F);
            txtDienMoi.Location = new Point(361, 52);
            txtDienMoi.Margin = new Padding(1, 2, 1, 2);
            txtDienMoi.Name = "txtDienMoi";
            txtDienMoi.Size = new Size(487, 26);
            txtDienMoi.TabIndex = 2;
            txtDienMoi.Text = "621";
            // 
            // lblChiSoNuocMoiM
            // 
            lblChiSoNuocMoiM.Dock = DockStyle.Fill;
            lblChiSoNuocMoiM.Font = new Font("Segoe UI", 10.5F);
            lblChiSoNuocMoiM.Location = new Point(8, 74);
            lblChiSoNuocMoiM.Margin = new Padding(1, 0, 4, 0);
            lblChiSoNuocMoiM.Name = "lblChiSoNuocMoiM";
            lblChiSoNuocMoiM.Size = new Size(348, 24);
            lblChiSoNuocMoiM.TabIndex = 3;
            lblChiSoNuocMoiM.Text = "Chỉ số nước mới (m³):";
            lblChiSoNuocMoiM.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNuocMoi
            // 
            txtNuocMoi.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtNuocMoi.Font = new Font("Segoe UI", 10.5F);
            txtNuocMoi.Location = new Point(361, 76);
            txtNuocMoi.Margin = new Padding(1, 2, 1, 2);
            txtNuocMoi.Name = "txtNuocMoi";
            txtNuocMoi.Size = new Size(487, 26);
            txtNuocMoi.TabIndex = 4;
            txtNuocMoi.Text = "28";
            // 
            // lblDonGiaDienD
            // 
            lblDonGiaDienD.Dock = DockStyle.Fill;
            lblDonGiaDienD.Font = new Font("Segoe UI", 10.5F);
            lblDonGiaDienD.Location = new Point(8, 98);
            lblDonGiaDienD.Margin = new Padding(1, 0, 4, 0);
            lblDonGiaDienD.Name = "lblDonGiaDienD";
            lblDonGiaDienD.Size = new Size(348, 24);
            lblDonGiaDienD.TabIndex = 5;
            lblDonGiaDienD.Text = "Đơn giá điện (đ):";
            lblDonGiaDienD.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGiaDien
            // 
            txtGiaDien.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtGiaDien.Font = new Font("Segoe UI", 10.5F);
            txtGiaDien.Location = new Point(361, 100);
            txtGiaDien.Margin = new Padding(1, 2, 1, 2);
            txtGiaDien.Name = "txtGiaDien";
            txtGiaDien.Size = new Size(487, 26);
            txtGiaDien.TabIndex = 6;
            txtGiaDien.Text = "3500";
            // 
            // lblDonGiaNuocD
            // 
            lblDonGiaNuocD.Dock = DockStyle.Fill;
            lblDonGiaNuocD.Font = new Font("Segoe UI", 10.5F);
            lblDonGiaNuocD.Location = new Point(8, 122);
            lblDonGiaNuocD.Margin = new Padding(1, 0, 4, 0);
            lblDonGiaNuocD.Name = "lblDonGiaNuocD";
            lblDonGiaNuocD.Size = new Size(348, 24);
            lblDonGiaNuocD.TabIndex = 7;
            lblDonGiaNuocD.Text = "Đơn giá nước (đ):";
            lblDonGiaNuocD.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGiaNuoc
            // 
            txtGiaNuoc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtGiaNuoc.Font = new Font("Segoe UI", 10.5F);
            txtGiaNuoc.Location = new Point(361, 124);
            txtGiaNuoc.Margin = new Padding(1, 2, 1, 2);
            txtGiaNuoc.Name = "txtGiaNuoc";
            txtGiaNuoc.Size = new Size(487, 26);
            txtGiaNuoc.TabIndex = 8;
            txtGiaNuoc.Text = "15000";
            // 
            // buttonRowDienNuoc
            // 
            buttonRowDienNuoc.AutoSize = true;
            layoutDienNuoc.SetColumnSpan(buttonRowDienNuoc, 2);
            buttonRowDienNuoc.Controls.Add(btnChotDienNuoc);
            buttonRowDienNuoc.Dock = DockStyle.Fill;
            buttonRowDienNuoc.FlowDirection = FlowDirection.RightToLeft;
            buttonRowDienNuoc.Font = new Font("Segoe UI", 10.5F);
            buttonRowDienNuoc.Location = new Point(8, 148);
            buttonRowDienNuoc.Margin = new Padding(1, 2, 1, 2);
            buttonRowDienNuoc.Name = "buttonRowDienNuoc";
            buttonRowDienNuoc.Size = new Size(840, 52);
            buttonRowDienNuoc.TabIndex = 9;
            // 
            // btnChotDienNuoc
            // 
            btnChotDienNuoc.BackColor = Color.FromArgb(37, 99, 235);
            btnChotDienNuoc.Cursor = Cursors.Hand;
            btnChotDienNuoc.FlatAppearance.BorderSize = 0;
            btnChotDienNuoc.FlatStyle = FlatStyle.Flat;
            btnChotDienNuoc.Font = new Font("Segoe UI", 10.5F);
            btnChotDienNuoc.ForeColor = Color.FromArgb(255, 255, 255);
            btnChotDienNuoc.Location = new Point(653, 0);
            btnChotDienNuoc.Margin = new Padding(8, 0, 0, 0);
            btnChotDienNuoc.Name = "btnChotDienNuoc";
            btnChotDienNuoc.Size = new Size(187, 40);
            btnChotDienNuoc.TabIndex = 0;
            btnChotDienNuoc.Text = "💾 Chốt chỉ số điện nước";
            btnChotDienNuoc.UseVisualStyleBackColor = false;
            // 
            // lblChotInfo
            // 
            layoutDienNuoc.SetColumnSpan(lblChotInfo, 2);
            lblChotInfo.Dock = DockStyle.Fill;
            lblChotInfo.Font = new Font("Segoe UI", 10F);
            lblChotInfo.ForeColor = Color.FromArgb(107, 114, 128);
            lblChotInfo.Location = new Point(8, 204);
            lblChotInfo.Margin = new Padding(1, 2, 1, 2);
            lblChotInfo.Name = "lblChotInfo";
            lblChotInfo.Size = new Size(840, 204);
            lblChotInfo.TabIndex = 10;
            lblChotInfo.Text = "Chỉ số cũ: điện 621 kW — nước 28 m³.\nNhập chỉ số mới rồi bấm 'Chốt chỉ số điện nước'.";
            // 
            // tabDichVu
            // 
            tabDichVu.BackColor = Color.FromArgb(255, 255, 255);
            tabDichVu.Controls.Add(gridUsages);
            tabDichVu.Controls.Add(panelDichVuBottom);
            tabDichVu.Controls.Add(panelDichVuTop);
            tabDichVu.Font = new Font("Segoe UI", 10.5F);
            tabDichVu.Location = new Point(4, 26);
            tabDichVu.Name = "tabDichVu";
            tabDichVu.Padding = new Padding(10);
            tabDichVu.Size = new Size(876, 436);
            tabDichVu.TabIndex = 1;
            tabDichVu.Text = "2. Chốt dịch vụ cuối kỳ";
            // 
            // gridUsages
            // 
            gridUsages.AllowUserToAddRows = false;
            gridUsages.AllowUserToResizeRows = false;
            gridUsages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridUsages.BackgroundColor = Color.FromArgb(255, 255, 255);
            gridUsages.BorderStyle = BorderStyle.None;
            gridUsages.Columns.AddRange(new DataGridViewColumn[] { colDichVu, colSoLuong, colDonVi, colDonGia, colThanhTien });
            gridUsages.Dock = DockStyle.Fill;
            gridUsages.Font = new Font("Segoe UI", 10.5F);
            gridUsages.Location = new Point(10, 74);
            gridUsages.Name = "gridUsages";
            gridUsages.ReadOnly = true;
            gridUsages.RowHeadersVisible = false;
            gridUsages.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridUsages.Size = new Size(856, 288);
            gridUsages.TabIndex = 0;
            // 
            // colDichVu
            // 
            colDichVu.FillWeight = 34F;
            colDichVu.HeaderText = "Dịch vụ";
            colDichVu.Name = "colDichVu";
            colDichVu.ReadOnly = true;
            // 
            // colSoLuong
            // 
            colSoLuong.FillWeight = 16F;
            colSoLuong.HeaderText = "Số lượng";
            colSoLuong.Name = "colSoLuong";
            colSoLuong.ReadOnly = true;
            // 
            // colDonVi
            // 
            colDonVi.FillWeight = 12F;
            colDonVi.HeaderText = "Đơn vị";
            colDonVi.Name = "colDonVi";
            colDonVi.ReadOnly = true;
            // 
            // colDonGia
            // 
            colDonGia.FillWeight = 19F;
            colDonGia.HeaderText = "Đơn giá áp dụng";
            colDonGia.Name = "colDonGia";
            colDonGia.ReadOnly = true;
            // 
            // colThanhTien
            // 
            colThanhTien.FillWeight = 19F;
            colThanhTien.HeaderText = "Thành tiền";
            colThanhTien.Name = "colThanhTien";
            colThanhTien.ReadOnly = true;
            // 
            // panelDichVuBottom
            // 
            panelDichVuBottom.Controls.Add(lblDichVuTong);
            panelDichVuBottom.Dock = DockStyle.Bottom;
            panelDichVuBottom.Font = new Font("Segoe UI", 10.5F);
            panelDichVuBottom.Location = new Point(10, 362);
            panelDichVuBottom.Name = "panelDichVuBottom";
            panelDichVuBottom.Size = new Size(856, 64);
            panelDichVuBottom.TabIndex = 1;
            // 
            // lblDichVuTong
            // 
            lblDichVuTong.Dock = DockStyle.Fill;
            lblDichVuTong.Font = new Font("Segoe UI", 10F);
            lblDichVuTong.ForeColor = Color.FromArgb(31, 41, 55);
            lblDichVuTong.Location = new Point(0, 0);
            lblDichVuTong.Margin = new Padding(2, 0, 2, 0);
            lblDichVuTong.Name = "lblDichVuTong";
            lblDichVuTong.Padding = new Padding(0, 10, 0, 0);
            lblDichVuTong.Size = new Size(856, 64);
            lblDichVuTong.TabIndex = 0;
            lblDichVuTong.Text = "Chưa có dịch vụ nào ghi cho kỳ chốt TRAPHONG-1.\nBấm '➕ Thêm dịch vụ cuối kỳ' nếu còn dịch vụ chưa thu tiền.";
            // 
            // panelDichVuTop
            // 
            panelDichVuTop.Controls.Add(btnThemDichVu);
            panelDichVuTop.Dock = DockStyle.Top;
            panelDichVuTop.Font = new Font("Segoe UI", 10.5F);
            panelDichVuTop.Location = new Point(10, 10);
            panelDichVuTop.Name = "panelDichVuTop";
            panelDichVuTop.Size = new Size(856, 64);
            panelDichVuTop.TabIndex = 2;
            // 
            // btnThemDichVu
            // 
            btnThemDichVu.BackColor = Color.FromArgb(37, 99, 235);
            btnThemDichVu.Cursor = Cursors.Hand;
            btnThemDichVu.Dock = DockStyle.Left;
            btnThemDichVu.FlatAppearance.BorderSize = 0;
            btnThemDichVu.FlatStyle = FlatStyle.Flat;
            btnThemDichVu.Font = new Font("Segoe UI", 10.5F);
            btnThemDichVu.ForeColor = Color.FromArgb(255, 255, 255);
            btnThemDichVu.Location = new Point(0, 0);
            btnThemDichVu.Name = "btnThemDichVu";
            btnThemDichVu.Size = new Size(184, 64);
            btnThemDichVu.TabIndex = 0;
            btnThemDichVu.Text = "➕ Thêm dịch vụ cuối kỳ";
            btnThemDichVu.UseVisualStyleBackColor = false;
            // 
            // tabQuyetToan
            // 
            tabQuyetToan.BackColor = Color.FromArgb(255, 255, 255);
            tabQuyetToan.Controls.Add(layoutQuyetToan);
            tabQuyetToan.Font = new Font("Segoe UI", 10.5F);
            tabQuyetToan.Location = new Point(4, 26);
            tabQuyetToan.Name = "tabQuyetToan";
            tabQuyetToan.Padding = new Padding(10);
            tabQuyetToan.Size = new Size(876, 436);
            tabQuyetToan.TabIndex = 2;
            tabQuyetToan.Text = "3. Xem trước & hoàn tất";
            // 
            // layoutQuyetToan
            // 
            layoutQuyetToan.AutoScroll = true;
            layoutQuyetToan.ColumnCount = 2;
            layoutQuyetToan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            layoutQuyetToan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            layoutQuyetToan.Controls.Add(lblNgayTraPhong, 0, 0);
            layoutQuyetToan.Controls.Add(dtpNgayTra, 1, 0);
            layoutQuyetToan.Controls.Add(lblTrangThaiPhongSauTra, 0, 1);
            layoutQuyetToan.Controls.Add(cboTrangThaiSau, 1, 1);
            layoutQuyetToan.Controls.Add(lblPhatSinhKhacD, 0, 2);
            layoutQuyetToan.Controls.Add(txtPhatSinhKhac, 1, 2);
            layoutQuyetToan.Controls.Add(lblGhiChu, 0, 3);
            layoutQuyetToan.Controls.Add(txtGhiChu, 1, 3);
            layoutQuyetToan.Controls.Add(buttonRowPreview, 0, 4);
            layoutQuyetToan.Controls.Add(txtPreview, 0, 5);
            layoutQuyetToan.Dock = DockStyle.Fill;
            layoutQuyetToan.Font = new Font("Segoe UI", 10.5F);
            layoutQuyetToan.Location = new Point(10, 10);
            layoutQuyetToan.Name = "layoutQuyetToan";
            layoutQuyetToan.Padding = new Padding(7, 6, 7, 6);
            layoutQuyetToan.RowCount = 6;
            layoutQuyetToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutQuyetToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutQuyetToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutQuyetToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutQuyetToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            layoutQuyetToan.RowStyles.Add(new RowStyle(SizeType.Absolute, 235F));
            layoutQuyetToan.Size = new Size(856, 416);
            layoutQuyetToan.TabIndex = 0;
            // 
            // lblNgayTraPhong
            // 
            lblNgayTraPhong.Dock = DockStyle.Fill;
            lblNgayTraPhong.Font = new Font("Segoe UI", 10.5F);
            lblNgayTraPhong.Location = new Point(8, 6);
            lblNgayTraPhong.Margin = new Padding(1, 0, 4, 0);
            lblNgayTraPhong.Name = "lblNgayTraPhong";
            lblNgayTraPhong.Size = new Size(348, 24);
            lblNgayTraPhong.TabIndex = 0;
            lblNgayTraPhong.Text = "Ngày trả phòng:";
            lblNgayTraPhong.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpNgayTra
            // 
            dtpNgayTra.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpNgayTra.Font = new Font("Segoe UI", 10.5F);
            dtpNgayTra.Format = DateTimePickerFormat.Short;
            dtpNgayTra.Location = new Point(361, 8);
            dtpNgayTra.Margin = new Padding(1, 2, 1, 2);
            dtpNgayTra.Name = "dtpNgayTra";
            dtpNgayTra.Size = new Size(487, 26);
            dtpNgayTra.TabIndex = 1;
            // 
            // lblTrangThaiPhongSauTra
            // 
            lblTrangThaiPhongSauTra.Dock = DockStyle.Fill;
            lblTrangThaiPhongSauTra.Font = new Font("Segoe UI", 10.5F);
            lblTrangThaiPhongSauTra.Location = new Point(8, 30);
            lblTrangThaiPhongSauTra.Margin = new Padding(1, 0, 4, 0);
            lblTrangThaiPhongSauTra.Name = "lblTrangThaiPhongSauTra";
            lblTrangThaiPhongSauTra.Size = new Size(348, 24);
            lblTrangThaiPhongSauTra.TabIndex = 2;
            lblTrangThaiPhongSauTra.Text = "Trạng thái phòng sau trả:";
            lblTrangThaiPhongSauTra.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboTrangThaiSau
            // 
            cboTrangThaiSau.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboTrangThaiSau.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThaiSau.Font = new Font("Segoe UI", 10.5F);
            cboTrangThaiSau.Items.AddRange(new object[] { "Trống", "Đang sửa chữa" });
            cboTrangThaiSau.Location = new Point(361, 32);
            cboTrangThaiSau.Margin = new Padding(1, 2, 1, 2);
            cboTrangThaiSau.Name = "cboTrangThaiSau";
            cboTrangThaiSau.Size = new Size(487, 27);
            cboTrangThaiSau.TabIndex = 3;
            // 
            // lblPhatSinhKhacD
            // 
            lblPhatSinhKhacD.Dock = DockStyle.Fill;
            lblPhatSinhKhacD.Font = new Font("Segoe UI", 10.5F);
            lblPhatSinhKhacD.Location = new Point(8, 54);
            lblPhatSinhKhacD.Margin = new Padding(1, 0, 4, 0);
            lblPhatSinhKhacD.Name = "lblPhatSinhKhacD";
            lblPhatSinhKhacD.Size = new Size(348, 24);
            lblPhatSinhKhacD.TabIndex = 4;
            lblPhatSinhKhacD.Text = "Phát sinh khác (đ):";
            lblPhatSinhKhacD.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPhatSinhKhac
            // 
            txtPhatSinhKhac.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPhatSinhKhac.Font = new Font("Segoe UI", 10.5F);
            txtPhatSinhKhac.Location = new Point(361, 56);
            txtPhatSinhKhac.Margin = new Padding(1, 2, 1, 2);
            txtPhatSinhKhac.Name = "txtPhatSinhKhac";
            txtPhatSinhKhac.Size = new Size(487, 26);
            txtPhatSinhKhac.TabIndex = 5;
            txtPhatSinhKhac.Text = "0";
            // 
            // lblGhiChu
            // 
            lblGhiChu.Dock = DockStyle.Fill;
            lblGhiChu.Font = new Font("Segoe UI", 10.5F);
            lblGhiChu.Location = new Point(8, 78);
            lblGhiChu.Margin = new Padding(1, 0, 4, 0);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(348, 24);
            lblGhiChu.TabIndex = 6;
            lblGhiChu.Text = "Ghi chú:";
            lblGhiChu.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGhiChu
            // 
            txtGhiChu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtGhiChu.Font = new Font("Segoe UI", 10.5F);
            txtGhiChu.Location = new Point(361, 80);
            txtGhiChu.Margin = new Padding(1, 2, 1, 2);
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(487, 26);
            txtGhiChu.TabIndex = 7;
            // 
            // buttonRowPreview
            // 
            buttonRowPreview.AutoSize = true;
            layoutQuyetToan.SetColumnSpan(buttonRowPreview, 2);
            buttonRowPreview.Controls.Add(btnPreview);
            buttonRowPreview.Dock = DockStyle.Fill;
            buttonRowPreview.FlowDirection = FlowDirection.RightToLeft;
            buttonRowPreview.Font = new Font("Segoe UI", 10.5F);
            buttonRowPreview.Location = new Point(8, 104);
            buttonRowPreview.Margin = new Padding(1, 2, 1, 2);
            buttonRowPreview.Name = "buttonRowPreview";
            buttonRowPreview.Size = new Size(840, 52);
            buttonRowPreview.TabIndex = 8;
            // 
            // btnPreview
            // 
            btnPreview.BackColor = Color.FromArgb(37, 99, 235);
            btnPreview.Cursor = Cursors.Hand;
            btnPreview.FlatAppearance.BorderSize = 0;
            btnPreview.FlatStyle = FlatStyle.Flat;
            btnPreview.Font = new Font("Segoe UI", 10.5F);
            btnPreview.ForeColor = Color.FromArgb(255, 255, 255);
            btnPreview.Location = new Point(654, 0);
            btnPreview.Margin = new Padding(8, 0, 0, 0);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(186, 40);
            btnPreview.TabIndex = 0;
            btnPreview.Text = "👁 Xem trước quyết toán";
            btnPreview.UseVisualStyleBackColor = false;
            // 
            // txtPreview
            // 
            txtPreview.BackColor = Color.FromArgb(249, 250, 251);
            txtPreview.BorderStyle = BorderStyle.FixedSingle;
            layoutQuyetToan.SetColumnSpan(txtPreview, 2);
            txtPreview.Dock = DockStyle.Fill;
            txtPreview.Font = new Font("Consolas", 10F);
            txtPreview.Location = new Point(8, 160);
            txtPreview.Margin = new Padding(1, 2, 1, 2);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.ScrollBars = ScrollBars.Vertical;
            txtPreview.Size = new Size(840, 248);
            txtPreview.TabIndex = 9;
            txtPreview.Text = "Chưa xem trước. Bấm '👁 Xem trước quyết toán'.";
            txtPreview.WordWrap = false;
            // 
            // buttonRowBottom
            // 
            buttonRowBottom.AutoSize = true;
            layout.SetColumnSpan(buttonRowBottom, 2);
            buttonRowBottom.Controls.Add(btnClose);
            buttonRowBottom.Controls.Add(btnSettle);
            buttonRowBottom.Dock = DockStyle.Fill;
            buttonRowBottom.FlowDirection = FlowDirection.RightToLeft;
            buttonRowBottom.Location = new Point(8, 639);
            buttonRowBottom.Margin = new Padding(1, 2, 1, 2);
            buttonRowBottom.Name = "buttonRowBottom";
            buttonRowBottom.Size = new Size(884, 53);
            buttonRowBottom.TabIndex = 5;
            // 
            // btnClose
            // 
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.FromArgb(31, 41, 55);
            btnClose.Location = new Point(754, 0);
            btnClose.Margin = new Padding(8, 0, 0, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(130, 40);
            btnClose.TabIndex = 0;
            btnClose.Text = "Hủy";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnSettle
            // 
            btnSettle.BackColor = Color.FromArgb(22, 163, 74);
            btnSettle.Cursor = Cursors.Hand;
            btnSettle.Enabled = false;
            btnSettle.FlatAppearance.BorderSize = 0;
            btnSettle.FlatStyle = FlatStyle.Flat;
            btnSettle.ForeColor = Color.FromArgb(255, 255, 255);
            btnSettle.Location = new Point(568, 0);
            btnSettle.Margin = new Padding(8, 0, 0, 0);
            btnSettle.Name = "btnSettle";
            btnSettle.Size = new Size(178, 40);
            btnSettle.TabIndex = 1;
            btnSettle.Text = "✅ Hoàn tất quyết toán";
            btnSettle.UseVisualStyleBackColor = false;
            // 
            // MoveOutDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(255, 255, 255);
            ClientSize = new Size(900, 700);
            Controls.Add(layout);
            Font = new Font("Segoe UI", 11F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MoveOutDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Trả phòng - Quyết toán";
            layout.ResumeLayout(false);
            layout.PerformLayout();
            tabs.ResumeLayout(false);
            tabDienNuoc.ResumeLayout(false);
            layoutDienNuoc.ResumeLayout(false);
            layoutDienNuoc.PerformLayout();
            buttonRowDienNuoc.ResumeLayout(false);
            tabDichVu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridUsages).EndInit();
            panelDichVuBottom.ResumeLayout(false);
            panelDichVuTop.ResumeLayout(false);
            tabQuyetToan.ResumeLayout(false);
            layoutQuyetToan.ResumeLayout(false);
            layoutQuyetToan.PerformLayout();
            buttonRowPreview.ResumeLayout(false);
            buttonRowBottom.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
