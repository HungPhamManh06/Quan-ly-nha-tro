// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA ReportPanel (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
//
//  ĐƠN VỊ Ở ĐÂY LÀ ĐƠN VỊ THIẾT KẾ 96 DPI — lúc chạy, app tự nhân kích thước theo
//  tỉ lệ màn hình (100% / 125% / 150% / 200%) qua Dpi.ScaleForm. Vì vậy:
//    • Sửa VỊ TRÍ / KÍCH THƯỚC / CHỮ / MÀU thoải mái — lúc chạy vẫn đúng.
//    • KHÔNG đổi AutoScaleMode (phải là None), nếu không sẽ bị nhân kích thước 2 lần.
//    • Cỡ chữ khai bằng point (pt) nên tự đúng ở mọi DPI — không cần tự nhân.
//    • Thêm control mới: kéo từ Toolbox thả vào đây là được, app tự nhân DPI lúc chạy.
// ------------------------------------------------------------------------------
namespace QuanLyNhaTroDesktop.Forms
{
    partial class ReportPanel
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Label lblPick;
        private System.Windows.Forms.ListBox listReports;
        private System.Windows.Forms.Panel right;
        private System.Windows.Forms.FlowLayoutPanel filters;
        private System.Windows.Forms.Label lblTuKy;
        private System.Windows.Forms.TextBox txtTuKy;
        private System.Windows.Forms.Label lblDenKy;
        private System.Windows.Forms.TextBox txtDenKy;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnCsv;
        private System.Windows.Forms.TableLayoutPanel grids;
        private System.Windows.Forms.DataGridView gridSummary;
        private System.Windows.Forms.DataGridView gridReport;
        private System.Windows.Forms.Panel statusBar;
        private System.Windows.Forms.Label lblReportStatus;

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
            this.leftPanel = new System.Windows.Forms.Panel();
            this.lblPick = new System.Windows.Forms.Label();
            this.listReports = new System.Windows.Forms.ListBox();
            this.right = new System.Windows.Forms.Panel();
            this.grids = new System.Windows.Forms.TableLayoutPanel();
            this.gridSummary = new System.Windows.Forms.DataGridView();
            this.gridReport = new System.Windows.Forms.DataGridView();
            this.filters = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTuKy = new System.Windows.Forms.Label();
            this.txtTuKy = new System.Windows.Forms.TextBox();
            this.lblDenKy = new System.Windows.Forms.Label();
            this.txtDenKy = new System.Windows.Forms.TextBox();
            this.lblNam = new System.Windows.Forms.Label();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnCsv = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.Panel();
            this.lblReportStatus = new System.Windows.Forms.Label();
            this.leftPanel.SuspendLayout();
            this.right.SuspendLayout();
            this.grids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).BeginInit();
            this.filters.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            //
            // lblPick
            //
            this.lblPick.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPick.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPick.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblPick.Location = new System.Drawing.Point(5, 5);
            this.lblPick.Name = "lblPick";
            this.lblPick.Size = new System.Drawing.Size(240, 30);
            this.lblPick.TabIndex = 0;
            this.lblPick.Text = "Chọn loại báo cáo";
            //
            // listReports
            //
            this.listReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listReports.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.listReports.IntegralHeight = false;
            this.listReports.ItemHeight = 15;
            this.listReports.Items.AddRange(new object[] {
            "Danh sách phòng",
            "Người thuê",
            "Hợp đồng",
            "Hóa đơn",
            "Tài chính (doanh thu - công nợ)",
            "Điện nước",
            "Doanh thu theo tháng"});
            this.listReports.Location = new System.Drawing.Point(10, 40);
            this.listReports.Name = "listReports";
            this.listReports.SelectedIndex = 0;
            this.listReports.Size = new System.Drawing.Size(230, 235);
            this.listReports.TabIndex = 1;
            //
            // leftPanel
            //
            this.leftPanel.BackColor = System.Drawing.Color.White;
            this.leftPanel.Controls.Add(this.listReports);
            this.leftPanel.Controls.Add(this.lblPick);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Padding = new System.Windows.Forms.Padding(10);
            this.leftPanel.Size = new System.Drawing.Size(250, 522);
            this.leftPanel.TabIndex = 1;
            //
            // txtTuKy
            //
            this.txtTuKy.Location = new System.Drawing.Point(76, 10);
            this.txtTuKy.Margin = new System.Windows.Forms.Padding(6, 2, 12, 0);
            this.txtTuKy.Name = "txtTuKy";
            this.txtTuKy.Size = new System.Drawing.Size(110, 40);
            this.txtTuKy.TabIndex = 1;
            this.txtTuKy.Text = "2026-01";
            //
            // lblTuKy
            //
            this.lblTuKy.AutoSize = true;
            this.lblTuKy.Location = new System.Drawing.Point(0, 14);
            this.lblTuKy.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblTuKy.Name = "lblTuKy";
            this.lblTuKy.Size = new System.Drawing.Size(66, 26);
            this.lblTuKy.TabIndex = 0;
            this.lblTuKy.Text = "Từ kỳ:";
            //
            // txtDenKy
            //
            this.txtDenKy.Location = new System.Drawing.Point(308, 10);
            this.txtDenKy.Margin = new System.Windows.Forms.Padding(6, 2, 12, 0);
            this.txtDenKy.Name = "txtDenKy";
            this.txtDenKy.Size = new System.Drawing.Size(110, 40);
            this.txtDenKy.TabIndex = 3;
            this.txtDenKy.Text = "2026-12";
            //
            // lblDenKy
            //
            this.lblDenKy.AutoSize = true;
            this.lblDenKy.Location = new System.Drawing.Point(222, 14);
            this.lblDenKy.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblDenKy.Name = "lblDenKy";
            this.lblDenKy.Size = new System.Drawing.Size(78, 26);
            this.lblDenKy.TabIndex = 2;
            this.lblDenKy.Text = "Đến kỳ:";
            //
            // cboNam
            //
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNam.Location = new System.Drawing.Point(488, 10);
            this.cboNam.Margin = new System.Windows.Forms.Padding(6, 2, 12, 0);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(90, 42);
            this.cboNam.TabIndex = 5;
            //
            // lblNam
            //
            this.lblNam.AutoSize = true;
            this.lblNam.Location = new System.Drawing.Point(436, 14);
            this.lblNam.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(42, 26);
            this.lblNam.TabIndex = 4;
            this.lblNam.Text = "Năm:";
            //
            // cboTrangThai
            //
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Items.AddRange(new object[] {
            "(tất cả)",
            "Chưa thanh toán",
            "Đã thanh toán",
            "Đang hiệu lực",
            "Sắp hết hạn",
            "Đã thanh lý"});
            this.cboTrangThai.Location = new System.Drawing.Point(718, 10);
            this.cboTrangThai.Margin = new System.Windows.Forms.Padding(6, 2, 12, 0);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(170, 42);
            this.cboTrangThai.TabIndex = 7;
            //
            // lblTrangThai
            //
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(590, 14);
            this.lblTrangThai.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(118, 26);
            this.lblTrangThai.TabIndex = 6;
            this.lblTrangThai.Text = "Trạng thái:";
            //
            // btnView
            //
            this.btnView.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnView.FlatAppearance.BorderSize = 0;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location = new System.Drawing.Point(900, 10);
            this.btnView.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(124, 38);
            this.btnView.TabIndex = 8;
            this.btnView.Text = "🔍 Xem báo cáo";
            this.btnView.UseVisualStyleBackColor = false;
            //
            // btnExcel
            //
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1013, 10);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(105, 38);
            this.btnExcel.TabIndex = 9;
            this.btnExcel.Text = "⤓ Xuất Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            //
            // btnCsv
            //
            this.btnCsv.BackColor = System.Drawing.Color.White;
            this.btnCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCsv.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCsv.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCsv.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnCsv.Location = new System.Drawing.Point(1126, 10);
            this.btnCsv.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.btnCsv.Name = "btnCsv";
            this.btnCsv.Size = new System.Drawing.Size(105, 38);
            this.btnCsv.TabIndex = 10;
            this.btnCsv.Text = "⤓ Xuất CSV";
            this.btnCsv.UseVisualStyleBackColor = false;
            //
            // filters
            //
            this.filters.AutoScroll = true;
            this.filters.BackColor = System.Drawing.Color.White;
            this.filters.Controls.Add(this.lblTuKy);
            this.filters.Controls.Add(this.txtTuKy);
            this.filters.Controls.Add(this.lblDenKy);
            this.filters.Controls.Add(this.txtDenKy);
            this.filters.Controls.Add(this.lblNam);
            this.filters.Controls.Add(this.cboNam);
            this.filters.Controls.Add(this.lblTrangThai);
            this.filters.Controls.Add(this.cboTrangThai);
            this.filters.Controls.Add(this.btnView);
            this.filters.Controls.Add(this.btnExcel);
            this.filters.Controls.Add(this.btnCsv);
            this.filters.Dock = System.Windows.Forms.DockStyle.Top;
            this.filters.Location = new System.Drawing.Point(10, 10);
            this.filters.Name = "filters";
            this.filters.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.filters.Size = new System.Drawing.Size(1196, 48);
            this.filters.TabIndex = 0;
            this.filters.WrapContents = false;
            //
            // gridSummary
            //
            this.gridSummary.AllowUserToAddRows = false;
            this.gridSummary.AllowUserToDeleteRows = false;
            this.gridSummary.AllowUserToResizeRows = false;
            this.gridSummary.BackgroundColor = System.Drawing.Color.White;
            this.gridSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSummary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gridSummary.Location = new System.Drawing.Point(3, 3);
            this.gridSummary.Name = "gridSummary";
            this.gridSummary.ReadOnly = true;
            this.gridSummary.RowHeadersVisible = false;
            this.gridSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSummary.Size = new System.Drawing.Size(1190, 89);
            this.gridSummary.TabIndex = 0;
            this.gridSummary.Visible = false;
            //
            // gridReport
            //
            this.gridReport.AllowUserToAddRows = false;
            this.gridReport.AllowUserToDeleteRows = false;
            this.gridReport.AllowUserToResizeRows = false;
            this.gridReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridReport.BackgroundColor = System.Drawing.Color.White;
            this.gridReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridReport.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gridReport.Location = new System.Drawing.Point(3, 98);
            this.gridReport.Name = "gridReport";
            this.gridReport.ReadOnly = true;
            this.gridReport.RowHeadersVisible = false;
            this.gridReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridReport.Size = new System.Drawing.Size(1190, 140);
            this.gridReport.TabIndex = 1;
            //
            // grids
            //
            this.grids.ColumnCount = 1;
            this.grids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.grids.Controls.Add(this.gridSummary, 0, 0);
            this.grids.Controls.Add(this.gridReport, 0, 1);
            this.grids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grids.Location = new System.Drawing.Point(10, 58);
            this.grids.Name = "grids";
            this.grids.RowCount = 2;
            this.grids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.grids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.grids.Size = new System.Drawing.Size(1196, 241);
            this.grids.TabIndex = 1;
            //
            // lblReportStatus
            //
            this.lblReportStatus.AutoSize = true;
            this.lblReportStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReportStatus.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblReportStatus.Location = new System.Drawing.Point(10, 4);
            this.lblReportStatus.Name = "lblReportStatus";
            this.lblReportStatus.Size = new System.Drawing.Size(200, 17);
            this.lblReportStatus.TabIndex = 0;
            this.lblReportStatus.Text = "Sẵn sàng";
            //
            // statusBar
            //
            this.statusBar.BackColor = System.Drawing.Color.White;
            this.statusBar.Controls.Add(this.lblReportStatus);
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Location = new System.Drawing.Point(10, 540);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(10, 4, 0, 0);
            this.statusBar.Size = new System.Drawing.Size(1196, 30);
            this.statusBar.TabIndex = 2;
            //
            // right
            //
            this.right.BackColor = System.Drawing.Color.White;
            this.right.Controls.Add(this.grids);
            this.right.Controls.Add(this.filters);
            this.right.Controls.Add(this.statusBar);
            this.right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.right.Location = new System.Drawing.Point(250, 0);
            this.right.Name = "right";
            this.right.Padding = new System.Windows.Forms.Padding(10);
            this.right.Size = new System.Drawing.Size(1206, 580);
            this.right.TabIndex = 0;
            //
            // ReportPanel
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.right);
            this.Controls.Add(this.leftPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ReportPanel";
            this.Size = new System.Drawing.Size(1331, 580);
            this.leftPanel.ResumeLayout(false);
            this.right.ResumeLayout(false);
            this.grids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).EndInit();
            this.filters.ResumeLayout(false);
            this.filters.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
