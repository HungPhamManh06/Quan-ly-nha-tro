// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA UtilityDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class UtilityDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.ComboBox cboRoom;
        private System.Windows.Forms.Label lblKyYyyyMM;
        private System.Windows.Forms.TextBox txtKy;
        private System.Windows.Forms.Label lblDienCuKw;
        private System.Windows.Forms.TextBox txtDienCu;
        private System.Windows.Forms.Label lblDienMoiKw;
        private System.Windows.Forms.TextBox txtDienMoi;
        private System.Windows.Forms.Label lblDonGiaDienD;
        private System.Windows.Forms.TextBox txtGiaDien;
        private System.Windows.Forms.Label lblNuocCuM;
        private System.Windows.Forms.TextBox txtNuocCu;
        private System.Windows.Forms.Label lblNuocMoiM;
        private System.Windows.Forms.TextBox txtNuocMoi;
        private System.Windows.Forms.Label lblDonGiaNuocD;
        private System.Windows.Forms.TextBox txtGiaNuoc;
        private System.Windows.Forms.Label lblTien;
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
            this.ClientSize = new System.Drawing.Size(475, 294);
            this.Text = "Ghi chỉ số điện nước";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "UtilityDialog";
            this.SuspendLayout();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Size = new System.Drawing.Size(475, 294);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 10;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.lblPhong = new System.Windows.Forms.Label();
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.Location = new System.Drawing.Point(8, 6);
            this.lblPhong.Size = new System.Drawing.Size(179, 24);
            this.lblPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhong.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblPhong.AutoSize = false;
            this.lblPhong.Text = "Phòng:";
            this.lblPhong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboRoom = new System.Windows.Forms.ComboBox();
            this.cboRoom.Name = "cboRoom";
            this.cboRoom.Location = new System.Drawing.Point(192, 8);
            this.cboRoom.Size = new System.Drawing.Size(275, 24);
            this.cboRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.cboRoom.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblKyYyyyMM = new System.Windows.Forms.Label();
            this.lblKyYyyyMM.Name = "lblKyYyyyMM";
            this.lblKyYyyyMM.Location = new System.Drawing.Point(8, 30);
            this.lblKyYyyyMM.Size = new System.Drawing.Size(179, 24);
            this.lblKyYyyyMM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKyYyyyMM.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblKyYyyyMM.AutoSize = false;
            this.lblKyYyyyMM.Text = "Kỳ (yyyy-MM):";
            this.lblKyYyyyMM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtKy = new System.Windows.Forms.TextBox();
            this.txtKy.Name = "txtKy";
            this.txtKy.Location = new System.Drawing.Point(192, 32);
            this.txtKy.Size = new System.Drawing.Size(275, 20);
            this.txtKy.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtKy.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtKy.Text = "2026-09";
            this.lblDienCuKw = new System.Windows.Forms.Label();
            this.lblDienCuKw.Name = "lblDienCuKw";
            this.lblDienCuKw.Location = new System.Drawing.Point(8, 54);
            this.lblDienCuKw.Size = new System.Drawing.Size(179, 24);
            this.lblDienCuKw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDienCuKw.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblDienCuKw.AutoSize = false;
            this.lblDienCuKw.Text = "Điện cũ (kW):";
            this.lblDienCuKw.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDienCu = new System.Windows.Forms.TextBox();
            this.txtDienCu.Name = "txtDienCu";
            this.txtDienCu.Location = new System.Drawing.Point(192, 56);
            this.txtDienCu.Size = new System.Drawing.Size(275, 20);
            this.txtDienCu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDienCu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtDienCu.Text = "0";
            this.lblDienMoiKw = new System.Windows.Forms.Label();
            this.lblDienMoiKw.Name = "lblDienMoiKw";
            this.lblDienMoiKw.Location = new System.Drawing.Point(8, 78);
            this.lblDienMoiKw.Size = new System.Drawing.Size(179, 24);
            this.lblDienMoiKw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDienMoiKw.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblDienMoiKw.AutoSize = false;
            this.lblDienMoiKw.Text = "Điện mới (kW):";
            this.lblDienMoiKw.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDienMoi = new System.Windows.Forms.TextBox();
            this.txtDienMoi.Name = "txtDienMoi";
            this.txtDienMoi.Location = new System.Drawing.Point(192, 80);
            this.txtDienMoi.Size = new System.Drawing.Size(275, 20);
            this.txtDienMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDienMoi.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblDonGiaDienD = new System.Windows.Forms.Label();
            this.lblDonGiaDienD.Name = "lblDonGiaDienD";
            this.lblDonGiaDienD.Location = new System.Drawing.Point(8, 102);
            this.lblDonGiaDienD.Size = new System.Drawing.Size(179, 24);
            this.lblDonGiaDienD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDonGiaDienD.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblDonGiaDienD.AutoSize = false;
            this.lblDonGiaDienD.Text = "Đơn giá điện (đ):";
            this.lblDonGiaDienD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtGiaDien = new System.Windows.Forms.TextBox();
            this.txtGiaDien.Name = "txtGiaDien";
            this.txtGiaDien.Location = new System.Drawing.Point(192, 104);
            this.txtGiaDien.Size = new System.Drawing.Size(275, 20);
            this.txtGiaDien.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGiaDien.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtGiaDien.Text = "3500";
            this.lblNuocCuM = new System.Windows.Forms.Label();
            this.lblNuocCuM.Name = "lblNuocCuM";
            this.lblNuocCuM.Location = new System.Drawing.Point(8, 126);
            this.lblNuocCuM.Size = new System.Drawing.Size(179, 24);
            this.lblNuocCuM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNuocCuM.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblNuocCuM.AutoSize = false;
            this.lblNuocCuM.Text = "Nước cũ (m³):";
            this.lblNuocCuM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtNuocCu = new System.Windows.Forms.TextBox();
            this.txtNuocCu.Name = "txtNuocCu";
            this.txtNuocCu.Location = new System.Drawing.Point(192, 128);
            this.txtNuocCu.Size = new System.Drawing.Size(275, 20);
            this.txtNuocCu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtNuocCu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtNuocCu.Text = "0";
            this.lblNuocMoiM = new System.Windows.Forms.Label();
            this.lblNuocMoiM.Name = "lblNuocMoiM";
            this.lblNuocMoiM.Location = new System.Drawing.Point(8, 150);
            this.lblNuocMoiM.Size = new System.Drawing.Size(179, 24);
            this.lblNuocMoiM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNuocMoiM.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblNuocMoiM.AutoSize = false;
            this.lblNuocMoiM.Text = "Nước mới (m³):";
            this.lblNuocMoiM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtNuocMoi = new System.Windows.Forms.TextBox();
            this.txtNuocMoi.Name = "txtNuocMoi";
            this.txtNuocMoi.Location = new System.Drawing.Point(192, 152);
            this.txtNuocMoi.Size = new System.Drawing.Size(275, 20);
            this.txtNuocMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtNuocMoi.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblDonGiaNuocD = new System.Windows.Forms.Label();
            this.lblDonGiaNuocD.Name = "lblDonGiaNuocD";
            this.lblDonGiaNuocD.Location = new System.Drawing.Point(8, 174);
            this.lblDonGiaNuocD.Size = new System.Drawing.Size(179, 24);
            this.lblDonGiaNuocD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDonGiaNuocD.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblDonGiaNuocD.AutoSize = false;
            this.lblDonGiaNuocD.Text = "Đơn giá nước (đ):";
            this.lblDonGiaNuocD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtGiaNuoc = new System.Windows.Forms.TextBox();
            this.txtGiaNuoc.Name = "txtGiaNuoc";
            this.txtGiaNuoc.Location = new System.Drawing.Point(192, 176);
            this.txtGiaNuoc.Size = new System.Drawing.Size(275, 20);
            this.txtGiaNuoc.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGiaNuoc.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtGiaNuoc.Text = "15000";
            this.lblTien = new System.Windows.Forms.Label();
            this.lblTien.Name = "lblTien";
            this.lblTien.Location = new System.Drawing.Point(8, 200);
            this.lblTien.Size = new System.Drawing.Size(459, 20);
            this.lblTien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTien.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblTien.AutoSize = false;
            this.lblTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTien.ForeColor = System.Drawing.Color.FromArgb(255, 37, 99, 235);
            this.lblTien.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Location = new System.Drawing.Point(8, 224);
            this.pnlButtons.Size = new System.Drawing.Size(459, 62);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButtons.WrapContents = true;
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOk.Name = "btnOk";
            this.btnOk.Location = new System.Drawing.Point(329, 0);
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
            this.btnCancel.Location = new System.Drawing.Point(191, 0);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.Controls.Add(this.layout);
            this.layout.Controls.Add(this.lblPhong, 0, 0);
            this.layout.Controls.Add(this.cboRoom, 1, 0);
            this.layout.Controls.Add(this.lblKyYyyyMM, 0, 1);
            this.layout.Controls.Add(this.txtKy, 1, 1);
            this.layout.Controls.Add(this.lblDienCuKw, 0, 2);
            this.layout.Controls.Add(this.txtDienCu, 1, 2);
            this.layout.Controls.Add(this.lblDienMoiKw, 0, 3);
            this.layout.Controls.Add(this.txtDienMoi, 1, 3);
            this.layout.Controls.Add(this.lblDonGiaDienD, 0, 4);
            this.layout.Controls.Add(this.txtGiaDien, 1, 4);
            this.layout.Controls.Add(this.lblNuocCuM, 0, 5);
            this.layout.Controls.Add(this.txtNuocCu, 1, 5);
            this.layout.Controls.Add(this.lblNuocMoiM, 0, 6);
            this.layout.Controls.Add(this.txtNuocMoi, 1, 6);
            this.layout.Controls.Add(this.lblDonGiaNuocD, 0, 7);
            this.layout.Controls.Add(this.txtGiaNuoc, 1, 7);
            this.layout.Controls.Add(this.lblTien, 0, 8);
            this.layout.SetColumnSpan(this.lblTien, 2);
            this.layout.Controls.Add(this.pnlButtons, 0, 9);
            this.layout.SetColumnSpan(this.pnlButtons, 2);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
