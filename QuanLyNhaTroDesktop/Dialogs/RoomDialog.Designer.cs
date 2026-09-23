// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA RoomDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class RoomDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblMaPhongP101;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label lblTenPhong;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblLoaiPhong;
        private System.Windows.Forms.TextBox txtLoai;
        private System.Windows.Forms.Label lblDienTichM;
        private System.Windows.Forms.TextBox txtDienTich;
        private System.Windows.Forms.Label lblGiaPhongD;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
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
            this.ClientSize = new System.Drawing.Size(450, 238);
            this.Text = "Thêm phòng mới";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "RoomDialog";
            this.SuspendLayout();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Size = new System.Drawing.Size(450, 238);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 7;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.lblMaPhongP101 = new System.Windows.Forms.Label();
            this.lblMaPhongP101.Name = "lblMaPhongP101";
            this.lblMaPhongP101.Location = new System.Drawing.Point(8, 6);
            this.lblMaPhongP101.Size = new System.Drawing.Size(152, 24);
            this.lblMaPhongP101.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaPhongP101.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblMaPhongP101.AutoSize = false;
            this.lblMaPhongP101.Text = "Mã phòng (P101):";
            this.lblMaPhongP101.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMa = new System.Windows.Forms.TextBox();
            this.txtMa.Name = "txtMa";
            this.txtMa.Location = new System.Drawing.Point(164, 8);
            this.txtMa.Size = new System.Drawing.Size(278, 20);
            this.txtMa.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtMa.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtMa.Text = "P209";
            this.lblTenPhong = new System.Windows.Forms.Label();
            this.lblTenPhong.Name = "lblTenPhong";
            this.lblTenPhong.Location = new System.Drawing.Point(8, 30);
            this.lblTenPhong.Size = new System.Drawing.Size(152, 24);
            this.lblTenPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTenPhong.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblTenPhong.AutoSize = false;
            this.lblTenPhong.Text = "Tên phòng:";
            this.lblTenPhong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtTen.Name = "txtTen";
            this.txtTen.Location = new System.Drawing.Point(164, 32);
            this.txtTen.Size = new System.Drawing.Size(278, 20);
            this.txtTen.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtTen.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblLoaiPhong = new System.Windows.Forms.Label();
            this.lblLoaiPhong.Name = "lblLoaiPhong";
            this.lblLoaiPhong.Location = new System.Drawing.Point(8, 54);
            this.lblLoaiPhong.Size = new System.Drawing.Size(152, 24);
            this.lblLoaiPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoaiPhong.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblLoaiPhong.AutoSize = false;
            this.lblLoaiPhong.Text = "Loại phòng:";
            this.lblLoaiPhong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtLoai = new System.Windows.Forms.TextBox();
            this.txtLoai.Name = "txtLoai";
            this.txtLoai.Location = new System.Drawing.Point(164, 56);
            this.txtLoai.Size = new System.Drawing.Size(278, 20);
            this.txtLoai.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtLoai.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblDienTichM = new System.Windows.Forms.Label();
            this.lblDienTichM.Name = "lblDienTichM";
            this.lblDienTichM.Location = new System.Drawing.Point(8, 78);
            this.lblDienTichM.Size = new System.Drawing.Size(152, 24);
            this.lblDienTichM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDienTichM.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblDienTichM.AutoSize = false;
            this.lblDienTichM.Text = "Diện tích (m²):";
            this.lblDienTichM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDienTich = new System.Windows.Forms.TextBox();
            this.txtDienTich.Name = "txtDienTich";
            this.txtDienTich.Location = new System.Drawing.Point(164, 80);
            this.txtDienTich.Size = new System.Drawing.Size(278, 20);
            this.txtDienTich.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDienTich.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblGiaPhongD = new System.Windows.Forms.Label();
            this.lblGiaPhongD.Name = "lblGiaPhongD";
            this.lblGiaPhongD.Location = new System.Drawing.Point(8, 102);
            this.lblGiaPhongD.Size = new System.Drawing.Size(152, 24);
            this.lblGiaPhongD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGiaPhongD.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblGiaPhongD.AutoSize = false;
            this.lblGiaPhongD.Text = "Giá phòng (đ):";
            this.lblGiaPhongD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtGia = new System.Windows.Forms.TextBox();
            this.txtGia.Name = "txtGia";
            this.txtGia.Location = new System.Drawing.Point(164, 104);
            this.txtGia.Size = new System.Drawing.Size(278, 20);
            this.txtGia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGia.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Location = new System.Drawing.Point(8, 126);
            this.lblGhiChu.Size = new System.Drawing.Size(152, 24);
            this.lblGhiChu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Location = new System.Drawing.Point(164, 128);
            this.txtGhiChu.Size = new System.Drawing.Size(278, 20);
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Location = new System.Drawing.Point(8, 152);
            this.pnlButtons.Size = new System.Drawing.Size(434, 78);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButtons.WrapContents = true;
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOk.Name = "btnOk";
            this.btnOk.Location = new System.Drawing.Point(304, 0);
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
            this.btnCancel.Location = new System.Drawing.Point(166, 0);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.Controls.Add(this.layout);
            this.layout.Controls.Add(this.lblMaPhongP101, 0, 0);
            this.layout.Controls.Add(this.txtMa, 1, 0);
            this.layout.Controls.Add(this.lblTenPhong, 0, 1);
            this.layout.Controls.Add(this.txtTen, 1, 1);
            this.layout.Controls.Add(this.lblLoaiPhong, 0, 2);
            this.layout.Controls.Add(this.txtLoai, 1, 2);
            this.layout.Controls.Add(this.lblDienTichM, 0, 3);
            this.layout.Controls.Add(this.txtDienTich, 1, 3);
            this.layout.Controls.Add(this.lblGiaPhongD, 0, 4);
            this.layout.Controls.Add(this.txtGia, 1, 4);
            this.layout.Controls.Add(this.lblGhiChu, 0, 5);
            this.layout.Controls.Add(this.txtGhiChu, 1, 5);
            this.layout.Controls.Add(this.pnlButtons, 0, 6);
            this.layout.SetColumnSpan(this.pnlButtons, 2);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
