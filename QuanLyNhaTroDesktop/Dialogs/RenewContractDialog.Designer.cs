// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA RenewContractDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class RenewContractDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblContractInfo;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private System.Windows.Forms.DateTimePicker dtpNgayKetThucMoi;
        private System.Windows.Forms.Label lblGiaThue;
        private System.Windows.Forms.TextBox txtGiaThueMoi;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.TextBox txtTienCocMoi;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGoiY;
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
            this.ClientSize = new System.Drawing.Size(580, 296);
            this.Text = "Gia hạn hợp đồng";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "RenewContractDialog";
            this.SuspendLayout();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Size = new System.Drawing.Size(580, 296);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 8;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Location = new System.Drawing.Point(7, 6);
            this.lblTitle.Size = new System.Drawing.Size(566, 28);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lblTitle.AutoSize = false;
            this.lblTitle.Text = "GIA HẠN HỢP ĐỒNG HD0013";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblContractInfo = new System.Windows.Forms.Label();
            this.lblContractInfo.Name = "lblContractInfo";
            this.lblContractInfo.Location = new System.Drawing.Point(8, 36);
            this.lblContractInfo.Size = new System.Drawing.Size(564, 60);
            this.lblContractInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContractInfo.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblContractInfo.AutoSize = false;
            // Nội dung do RenewContractDialog.cs điền lúc chạy (đây là chữ mẫu cho Designer xem bố cục)
            this.lblContractInfo.Text = "Phòng: P101 — Người thuê: Nguyễn Văn An\nThời hạn hiện tại: 01/01/2026 → 31/12/2026\nGiá thuê hiện tại: 1,500,000 đ/tháng — Tiền cọc: 3,000,000 đ";
            this.lblContractInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblContractInfo.ForeColor = System.Drawing.Color.FromArgb(255, 107, 114, 128);
            this.lblContractInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Location = new System.Drawing.Point(8, 98);
            this.lblNgayKetThuc.Size = new System.Drawing.Size(232, 24);
            this.lblNgayKetThuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgayKetThuc.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblNgayKetThuc.AutoSize = false;
            this.lblNgayKetThuc.Text = "Ngày kết thúc mới:";
            this.lblNgayKetThuc.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.dtpNgayKetThucMoi = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayKetThucMoi.Name = "dtpNgayKetThucMoi";
            this.dtpNgayKetThucMoi.Location = new System.Drawing.Point(246, 100);
            this.dtpNgayKetThucMoi.Size = new System.Drawing.Size(326, 24);
            this.dtpNgayKetThucMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dtpNgayKetThucMoi.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.dtpNgayKetThucMoi.Text = "9/30/2028";
            this.dtpNgayKetThucMoi.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.lblGiaThue = new System.Windows.Forms.Label();
            this.lblGiaThue.Name = "lblGiaThue";
            this.lblGiaThue.Location = new System.Drawing.Point(8, 122);
            this.lblGiaThue.Size = new System.Drawing.Size(232, 24);
            this.lblGiaThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGiaThue.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblGiaThue.AutoSize = false;
            this.lblGiaThue.Text = "Giá thuê mới (đ/tháng):";
            this.lblGiaThue.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.txtGiaThueMoi = new System.Windows.Forms.TextBox();
            this.txtGiaThueMoi.Name = "txtGiaThueMoi";
            this.txtGiaThueMoi.Location = new System.Drawing.Point(246, 124);
            this.txtGiaThueMoi.Size = new System.Drawing.Size(326, 20);
            this.txtGiaThueMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGiaThueMoi.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtGiaThueMoi.Text = "2000000";
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.lblTienCoc.Name = "lblTienCoc";
            this.lblTienCoc.Location = new System.Drawing.Point(8, 146);
            this.lblTienCoc.Size = new System.Drawing.Size(232, 24);
            this.lblTienCoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTienCoc.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblTienCoc.AutoSize = false;
            this.lblTienCoc.Text = "Tiền cọc mới (đ):";
            this.lblTienCoc.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.txtTienCocMoi = new System.Windows.Forms.TextBox();
            this.txtTienCocMoi.Name = "txtTienCocMoi";
            this.txtTienCocMoi.Location = new System.Drawing.Point(246, 148);
            this.txtTienCocMoi.Size = new System.Drawing.Size(326, 20);
            this.txtTienCocMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtTienCocMoi.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtTienCocMoi.Text = "2000000";
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Location = new System.Drawing.Point(8, 170);
            this.lblGhiChu.Size = new System.Drawing.Size(232, 24);
            this.lblGhiChu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Location = new System.Drawing.Point(246, 172);
            this.txtGhiChu.Size = new System.Drawing.Size(326, 20);
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblGoiY = new System.Windows.Forms.Label();
            this.lblGoiY.Name = "lblGoiY";
            this.lblGoiY.Location = new System.Drawing.Point(8, 196);
            this.lblGoiY.Size = new System.Drawing.Size(564, 40);
            this.lblGoiY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGoiY.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblGoiY.AutoSize = false;
            this.lblGoiY.Text = "Ghi chú: hợp đồng chỉ được gia hạn dài hơn ngày kết thúc hiện tại.";
            this.lblGoiY.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGoiY.ForeColor = System.Drawing.Color.FromArgb(255, 107, 114, 128);
            this.lblGoiY.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.buttonRow = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRow.Name = "buttonRow";
            this.buttonRow.Location = new System.Drawing.Point(8, 240);
            this.buttonRow.Size = new System.Drawing.Size(564, 48);
            this.buttonRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRow.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.buttonRow.AutoSize = true;
            this.buttonRow.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonRow.WrapContents = true;
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOk.Name = "btnOk";
            this.btnOk.Location = new System.Drawing.Point(434, 0);
            this.btnOk.Size = new System.Drawing.Size(130, 40);
            this.btnOk.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this.btnOk.Text = "Gia hạn";
            this.btnOk.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Location = new System.Drawing.Point(296, 0);
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
            this.layout.Controls.Add(this.lblContractInfo, 0, 1);
            this.layout.SetColumnSpan(this.lblContractInfo, 2);
            this.layout.Controls.Add(this.lblNgayKetThuc, 0, 2);
            this.layout.Controls.Add(this.dtpNgayKetThucMoi, 1, 2);
            this.layout.Controls.Add(this.lblGiaThue, 0, 3);
            this.layout.Controls.Add(this.txtGiaThueMoi, 1, 3);
            this.layout.Controls.Add(this.lblTienCoc, 0, 4);
            this.layout.Controls.Add(this.txtTienCocMoi, 1, 4);
            this.layout.Controls.Add(this.lblGhiChu, 0, 5);
            this.layout.Controls.Add(this.txtGhiChu, 1, 5);
            this.layout.Controls.Add(this.lblGoiY, 0, 6);
            this.layout.SetColumnSpan(this.lblGoiY, 2);
            this.layout.Controls.Add(this.buttonRow, 0, 7);
            this.layout.SetColumnSpan(this.buttonRow, 2);
            this.buttonRow.Controls.Add(this.btnOk);
            this.buttonRow.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
