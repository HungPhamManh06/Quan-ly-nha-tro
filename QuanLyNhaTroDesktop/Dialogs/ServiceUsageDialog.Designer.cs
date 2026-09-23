// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA ServiceUsageDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class ServiceUsageDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.ComboBox cboRoom;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.ComboBox cboService;
        private System.Windows.Forms.Label lblKy;
        private System.Windows.Forms.TextBox txtKy;
        private System.Windows.Forms.Label lblSoLuongLabel;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.FlowLayoutPanel buttonRow;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

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
            this.ClientSize = new System.Drawing.Size(560, 263);
            this.Text = "Ghi nhận sử dụng dịch vụ";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "ServiceUsageDialog";
            this.SuspendLayout();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Size = new System.Drawing.Size(560, 263);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 9;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(7, 6);
            this.lblTitle.Size = new System.Drawing.Size(546, 28);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblTitle.AutoSize = false;
            this.lblTitle.Text = "GHI NHẬN DỊCH VỤ ĐÃ DÙNG";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRoom = new System.Windows.Forms.Label();
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Location = new System.Drawing.Point(8, 34);
            this.lblRoom.Size = new System.Drawing.Size(213, 24);
            this.lblRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoom.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblRoom.AutoSize = false;
            this.lblRoom.Text = "Phòng:";
            this.lblRoom.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboRoom = new System.Windows.Forms.ComboBox();
            this.cboRoom.Name = "cboRoom";
            this.cboRoom.Location = new System.Drawing.Point(226, 36);
            this.cboRoom.Size = new System.Drawing.Size(326, 24);
            this.cboRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.cboRoom.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lblService = new System.Windows.Forms.Label();
            this.lblService.Name = "lblService";
            this.lblService.Location = new System.Drawing.Point(8, 58);
            this.lblService.Size = new System.Drawing.Size(213, 24);
            this.lblService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblService.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblService.AutoSize = false;
            this.lblService.Text = "Dịch vụ:";
            this.lblService.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboService = new System.Windows.Forms.ComboBox();
            this.cboService.Name = "cboService";
            this.cboService.Location = new System.Drawing.Point(226, 60);
            this.cboService.Size = new System.Drawing.Size(326, 24);
            this.cboService.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.cboService.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.cboService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lblKy = new System.Windows.Forms.Label();
            this.lblKy.Name = "lblKy";
            this.lblKy.Location = new System.Drawing.Point(8, 82);
            this.lblKy.Size = new System.Drawing.Size(213, 24);
            this.lblKy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKy.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblKy.AutoSize = false;
            this.lblKy.Text = "Kỳ sử dụng (yyyy-MM):";
            this.lblKy.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.txtKy = new System.Windows.Forms.TextBox();
            this.txtKy.Name = "txtKy";
            this.txtKy.Location = new System.Drawing.Point(226, 84);
            this.txtKy.Size = new System.Drawing.Size(326, 20);
            this.txtKy.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtKy.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtKy.Text = "2026-09";
            this.lblSoLuongLabel = new System.Windows.Forms.Label();
            this.lblSoLuongLabel.Name = "lblSoLuongLabel";
            this.lblSoLuongLabel.Location = new System.Drawing.Point(8, 106);
            this.lblSoLuongLabel.Size = new System.Drawing.Size(213, 24);
            this.lblSoLuongLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoLuongLabel.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblSoLuongLabel.AutoSize = false;
            this.lblSoLuongLabel.Text = "Số lượng:";
            this.lblSoLuongLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Location = new System.Drawing.Point(226, 108);
            this.txtSoLuong.Size = new System.Drawing.Size(326, 20);
            this.txtSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtSoLuong.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtSoLuong.Text = "1";
            this.lblDonGia = new System.Windows.Forms.Label();
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Location = new System.Drawing.Point(8, 130);
            this.lblDonGia.Size = new System.Drawing.Size(213, 24);
            this.lblDonGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDonGia.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblDonGia.AutoSize = false;
            this.lblDonGia.Text = "Đơn giá áp dụng (đ):";
            this.lblDonGia.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Location = new System.Drawing.Point(226, 132);
            this.txtDonGia.Size = new System.Drawing.Size(326, 20);
            this.txtDonGia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDonGia.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtDonGia.Text = "60000";
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Location = new System.Drawing.Point(8, 154);
            this.lblGhiChu.Size = new System.Drawing.Size(213, 24);
            this.lblGhiChu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Location = new System.Drawing.Point(226, 156);
            this.txtGhiChu.Size = new System.Drawing.Size(326, 20);
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Location = new System.Drawing.Point(8, 180);
            this.lblThanhTien.Size = new System.Drawing.Size(544, 22);
            this.lblThanhTien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblThanhTien.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblThanhTien.AutoSize = false;
            this.lblThanhTien.Text = "THÀNH TIỀN: 60,000 đ";
            this.lblThanhTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblThanhTien.ForeColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this.lblThanhTien.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.buttonRow = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRow.Name = "buttonRow";
            this.buttonRow.Location = new System.Drawing.Point(8, 206);
            this.buttonRow.Size = new System.Drawing.Size(544, 49);
            this.buttonRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRow.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonRow.AutoSize = true;
            this.buttonRow.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonRow.WrapContents = true;
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOk.Name = "btnOk";
            this.btnOk.Location = new System.Drawing.Point(414, 0);
            this.btnOk.Size = new System.Drawing.Size(130, 40);
            this.btnOk.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this.btnOk.Text = "Lưu";
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Location = new System.Drawing.Point(276, 0);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(255, 31, 41, 55);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.Controls.Add(this.layout);
            this.layout.Controls.Add(this.lblTitle, 0, 0);
            this.layout.SetColumnSpan(this.lblTitle, 2);
            this.layout.Controls.Add(this.lblRoom, 0, 1);
            this.layout.Controls.Add(this.cboRoom, 1, 1);
            this.layout.Controls.Add(this.lblService, 0, 2);
            this.layout.Controls.Add(this.cboService, 1, 2);
            this.layout.Controls.Add(this.lblKy, 0, 3);
            this.layout.Controls.Add(this.txtKy, 1, 3);
            this.layout.Controls.Add(this.lblSoLuongLabel, 0, 4);
            this.layout.Controls.Add(this.txtSoLuong, 1, 4);
            this.layout.Controls.Add(this.lblDonGia, 0, 5);
            this.layout.Controls.Add(this.txtDonGia, 1, 5);
            this.layout.Controls.Add(this.lblGhiChu, 0, 6);
            this.layout.Controls.Add(this.txtGhiChu, 1, 6);
            this.layout.Controls.Add(this.lblThanhTien, 0, 7);
            this.layout.SetColumnSpan(this.lblThanhTien, 2);
            this.layout.Controls.Add(this.buttonRow, 0, 8);
            this.layout.SetColumnSpan(this.buttonRow, 2);
            this.buttonRow.Controls.Add(this.btnOk);
            this.buttonRow.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
