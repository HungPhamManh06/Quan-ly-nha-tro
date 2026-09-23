// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA ContractDetailDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class ContractDetailDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label _summary;
        private System.Windows.Forms.TabControl _tabs;
        private System.Windows.Forms.TabPage tabHoaDonCuaPhong;
        private System.Windows.Forms.DataGridView _invoices;
        private System.Windows.Forms.TabPage tabLichSuThaoTac;
        private System.Windows.Forms.DataGridView _histories;
        private System.Windows.Forms.TabPage tabQuyetToanTraPhong;
        private System.Windows.Forms.Label _moveOutInfo;
        private System.Windows.Forms.FlowLayoutPanel buttonRow;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button _btnMoveOut;
        private System.Windows.Forms.Button _btnLiquidate;
        private System.Windows.Forms.Button _btnRenew;

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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.ClientSize = new System.Drawing.Size(940, 542);
            this.Text = "Chi tiết hợp đồng";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "ContractDetailDialog";
            this.SuspendLayout();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Size = new System.Drawing.Size(940, 542);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 4;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(7, 6);
            this.lblTitle.Size = new System.Drawing.Size(926, 30);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblTitle.AutoSize = false;
            this.lblTitle.Text = "CHI TIẾT HỢP ĐỒNG";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._summary = new System.Windows.Forms.Label();
            this._summary.Name = "_summary";
            this._summary.Location = new System.Drawing.Point(8, 38);
            this._summary.Size = new System.Drawing.Size(924, 124);
            this._summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summary.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this._summary.AutoSize = false;
            // Nội dung tóm tắt do ContractDetailDialog.cs điền lúc chạy (đây là chữ mẫu cho Designer xem bố cục)
            this._summary.Text = "Mã hợp đồng : HD0001                 Trạng thái: Đang hiệu lực\nPhòng       : P101 - Phòng 101\nNgười thuê  : Nguyễn Văn An (0901234567)\nThời hạn    : 01/01/2026 → 31/12/2026\nGiá thuê    : 1,500,000 đ/tháng          Tiền cọc: 3,000,000 đ\nGhi chú     : (không có)";
            this._summary.Font = new System.Drawing.Font("Consolas", 10.5F);
            this._summary.ForeColor = System.Drawing.Color.FromArgb(255, 31, 41, 55);
            this._summary.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this._tabs = new System.Windows.Forms.TabControl();
            this._tabs.Name = "_tabs";
            this._tabs.Location = new System.Drawing.Point(8, 166);
            this._tabs.Size = new System.Drawing.Size(924, 316);
            this._tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabs.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this._tabs.BackColor = System.Drawing.Color.FromArgb(255, 240, 240, 240);
            this._tabs.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._tabs.Padding = new System.Drawing.Point(2, 1);
            this.tabHoaDonCuaPhong = new System.Windows.Forms.TabPage();
            this.tabHoaDonCuaPhong.Name = "tabHoaDonCuaPhong";
            this.tabHoaDonCuaPhong.Location = new System.Drawing.Point(4, 26);
            this.tabHoaDonCuaPhong.Size = new System.Drawing.Size(916, 286);
            this.tabHoaDonCuaPhong.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabHoaDonCuaPhong.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.tabHoaDonCuaPhong.Text = "Hóa đơn của phòng";
            this.tabHoaDonCuaPhong.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._invoices = new System.Windows.Forms.DataGridView();
            this._invoices.Name = "_invoices";
            this._invoices.Location = new System.Drawing.Point(0, 0);
            this._invoices.Size = new System.Drawing.Size(916, 286);
            this._invoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoices.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this._invoices.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._invoices.AllowUserToAddRows = false;
            this._invoices.AllowUserToDeleteRows = true;
            this._invoices.AllowUserToResizeRows = false;
            this._invoices.RowHeadersVisible = false;
            this._invoices.ReadOnly = true;
            this._invoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._invoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._invoices.BackgroundColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this._invoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabLichSuThaoTac = new System.Windows.Forms.TabPage();
            this.tabLichSuThaoTac.Name = "tabLichSuThaoTac";
            this.tabLichSuThaoTac.Location = new System.Drawing.Point(4, 26);
            this.tabLichSuThaoTac.Size = new System.Drawing.Size(916, 286);
            this.tabLichSuThaoTac.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabLichSuThaoTac.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.tabLichSuThaoTac.Text = "Lịch sử thao tác";
            this.tabLichSuThaoTac.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._histories = new System.Windows.Forms.DataGridView();
            this._histories.Name = "_histories";
            this._histories.Location = new System.Drawing.Point(0, 0);
            this._histories.Size = new System.Drawing.Size(916, 286);
            this._histories.Dock = System.Windows.Forms.DockStyle.Fill;
            this._histories.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this._histories.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._histories.AllowUserToAddRows = false;
            this._histories.AllowUserToDeleteRows = true;
            this._histories.AllowUserToResizeRows = false;
            this._histories.RowHeadersVisible = false;
            this._histories.ReadOnly = true;
            this._histories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._histories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._histories.BackgroundColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this._histories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabQuyetToanTraPhong = new System.Windows.Forms.TabPage();
            this.tabQuyetToanTraPhong.Name = "tabQuyetToanTraPhong";
            this.tabQuyetToanTraPhong.Location = new System.Drawing.Point(4, 26);
            this.tabQuyetToanTraPhong.Size = new System.Drawing.Size(916, 286);
            this.tabQuyetToanTraPhong.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabQuyetToanTraPhong.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.tabQuyetToanTraPhong.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.tabQuyetToanTraPhong.Text = "Quyết toán trả phòng";
            this.tabQuyetToanTraPhong.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.tabQuyetToanTraPhong.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this._moveOutInfo = new System.Windows.Forms.Label();
            this._moveOutInfo.Name = "_moveOutInfo";
            this._moveOutInfo.Location = new System.Drawing.Point(10, 10);
            this._moveOutInfo.Size = new System.Drawing.Size(571, 50);
            this._moveOutInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this._moveOutInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._moveOutInfo.AutoSize = true;
            this._moveOutInfo.Text = "Hợp đồng chưa trả phòng.\n\nBấm nút \"🚪 Trả phòng\" để chốt điện nước, xem trước quyết toán và hoàn tất.";
            this._moveOutInfo.Font = new System.Drawing.Font("Consolas", 10.5F);
            this._moveOutInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.buttonRow = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRow.Name = "buttonRow";
            this.buttonRow.Location = new System.Drawing.Point(8, 486);
            this.buttonRow.Size = new System.Drawing.Size(924, 48);
            this.buttonRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRow.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonRow.AutoSize = true;
            this.buttonRow.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonRow.WrapContents = true;
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClose.Name = "btnClose";
            this.btnClose.Location = new System.Drawing.Point(794, 0);
            this.btnClose.Size = new System.Drawing.Size(130, 40);
            this.btnClose.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnClose.Text = "Đóng";
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(255, 31, 41, 55);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.UseVisualStyleBackColor = false;
            this._btnMoveOut = new System.Windows.Forms.Button();
            this._btnMoveOut.Name = "_btnMoveOut";
            this._btnMoveOut.Location = new System.Drawing.Point(656, 0);
            this._btnMoveOut.Size = new System.Drawing.Size(130, 40);
            this._btnMoveOut.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._btnMoveOut.BackColor = System.Drawing.Color.FromArgb(255, 22, 163, 74);
            this._btnMoveOut.Text = "🚪 Trả phòng";
            this._btnMoveOut.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this._btnMoveOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnMoveOut.FlatAppearance.BorderSize = 0;
            this._btnMoveOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnMoveOut.UseVisualStyleBackColor = false;
            this._btnLiquidate = new System.Windows.Forms.Button();
            this._btnLiquidate.Name = "_btnLiquidate";
            this._btnLiquidate.Location = new System.Drawing.Point(518, 0);
            this._btnLiquidate.Size = new System.Drawing.Size(130, 40);
            this._btnLiquidate.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._btnLiquidate.BackColor = System.Drawing.Color.FromArgb(255, 220, 38, 38);
            this._btnLiquidate.Text = "🚫 Thanh lý";
            this._btnLiquidate.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this._btnLiquidate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLiquidate.FlatAppearance.BorderSize = 0;
            this._btnLiquidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnLiquidate.UseVisualStyleBackColor = false;
            this._btnRenew = new System.Windows.Forms.Button();
            this._btnRenew.Name = "_btnRenew";
            this._btnRenew.Location = new System.Drawing.Point(380, 0);
            this._btnRenew.Size = new System.Drawing.Size(130, 40);
            this._btnRenew.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._btnRenew.BackColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this._btnRenew.Text = "📆 Gia hạn";
            this._btnRenew.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this._btnRenew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnRenew.FlatAppearance.BorderSize = 0;
            this._btnRenew.Cursor = System.Windows.Forms.Cursors.Hand;
            this._btnRenew.UseVisualStyleBackColor = false;
            this.Controls.Add(this.layout);
            this.layout.Controls.Add(this.lblTitle, 0, 0);
            this.layout.SetColumnSpan(this.lblTitle, 2);
            this.layout.Controls.Add(this._summary, 0, 1);
            this.layout.SetColumnSpan(this._summary, 2);
            this.layout.Controls.Add(this._tabs, 0, 2);
            this.layout.SetColumnSpan(this._tabs, 2);
            this._tabs.Controls.Add(this.tabHoaDonCuaPhong);
            this.tabHoaDonCuaPhong.Controls.Add(this._invoices);
            this._tabs.Controls.Add(this.tabLichSuThaoTac);
            this.tabLichSuThaoTac.Controls.Add(this._histories);
            this._tabs.Controls.Add(this.tabQuyetToanTraPhong);
            this.tabQuyetToanTraPhong.Controls.Add(this._moveOutInfo);
            this.layout.Controls.Add(this.buttonRow, 0, 3);
            this.layout.SetColumnSpan(this.buttonRow, 2);
            this.buttonRow.Controls.Add(this.btnClose);
            this.buttonRow.Controls.Add(this._btnMoveOut);
            this.buttonRow.Controls.Add(this._btnLiquidate);
            this.buttonRow.Controls.Add(this._btnRenew);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
