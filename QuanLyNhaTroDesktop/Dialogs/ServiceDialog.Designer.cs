// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA ServiceDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class ServiceDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTenDichVu;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.TextBox txtDonVi;
        private System.Windows.Forms.Label lblDonGiaD;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label lblSpacer;
        private System.Windows.Forms.CheckBox chkApDung;
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
            this.ClientSize = new System.Drawing.Size(455, 206);
            this.Text = "Thêm dịch vụ mới";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "ServiceDialog";
            this.SuspendLayout();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Size = new System.Drawing.Size(455, 206);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 5;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.lblTenDichVu = new System.Windows.Forms.Label();
            this.lblTenDichVu.Name = "lblTenDichVu";
            this.lblTenDichVu.Location = new System.Drawing.Point(8, 6);
            this.lblTenDichVu.Size = new System.Drawing.Size(154, 24);
            this.lblTenDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTenDichVu.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblTenDichVu.AutoSize = false;
            this.lblTenDichVu.Text = "Tên dịch vụ:";
            this.lblTenDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtTen.Name = "txtTen";
            this.txtTen.Location = new System.Drawing.Point(166, 8);
            this.txtTen.Size = new System.Drawing.Size(280, 20);
            this.txtTen.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtTen.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblDonViTinh = new System.Windows.Forms.Label();
            this.lblDonViTinh.Name = "lblDonViTinh";
            this.lblDonViTinh.Location = new System.Drawing.Point(8, 30);
            this.lblDonViTinh.Size = new System.Drawing.Size(154, 24);
            this.lblDonViTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDonViTinh.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblDonViTinh.AutoSize = false;
            this.lblDonViTinh.Text = "Đơn vị tính:";
            this.lblDonViTinh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDonVi = new System.Windows.Forms.TextBox();
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.Location = new System.Drawing.Point(166, 32);
            this.txtDonVi.Size = new System.Drawing.Size(280, 20);
            this.txtDonVi.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDonVi.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblDonGiaD = new System.Windows.Forms.Label();
            this.lblDonGiaD.Name = "lblDonGiaD";
            this.lblDonGiaD.Location = new System.Drawing.Point(8, 54);
            this.lblDonGiaD.Size = new System.Drawing.Size(154, 24);
            this.lblDonGiaD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDonGiaD.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblDonGiaD.AutoSize = false;
            this.lblDonGiaD.Text = "Đơn giá (đ):";
            this.lblDonGiaD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Location = new System.Drawing.Point(166, 56);
            this.txtDonGia.Size = new System.Drawing.Size(280, 20);
            this.txtDonGia.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDonGia.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblSpacer = new System.Windows.Forms.Label();
            this.lblSpacer.Name = "lblSpacer";
            this.lblSpacer.Location = new System.Drawing.Point(8, 78);
            this.lblSpacer.Size = new System.Drawing.Size(154, 24);
            this.lblSpacer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSpacer.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblSpacer.AutoSize = false;
            this.lblSpacer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkApDung = new System.Windows.Forms.CheckBox();
            this.chkApDung.Name = "chkApDung";
            this.chkApDung.Location = new System.Drawing.Point(166, 84);
            this.chkApDung.Size = new System.Drawing.Size(280, 12);
            this.chkApDung.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.chkApDung.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.chkApDung.Text = "Đang áp dụng";
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Location = new System.Drawing.Point(8, 104);
            this.pnlButtons.Size = new System.Drawing.Size(439, 94);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButtons.WrapContents = true;
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOk.Name = "btnOk";
            this.btnOk.Location = new System.Drawing.Point(309, 0);
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
            this.btnCancel.Location = new System.Drawing.Point(171, 0);
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.Controls.Add(this.layout);
            this.layout.Controls.Add(this.lblTenDichVu, 0, 0);
            this.layout.Controls.Add(this.txtTen, 1, 0);
            this.layout.Controls.Add(this.lblDonViTinh, 0, 1);
            this.layout.Controls.Add(this.txtDonVi, 1, 1);
            this.layout.Controls.Add(this.lblDonGiaD, 0, 2);
            this.layout.Controls.Add(this.txtDonGia, 1, 2);
            this.layout.Controls.Add(this.lblSpacer, 0, 3);
            this.layout.Controls.Add(this.chkApDung, 1, 3);
            this.layout.Controls.Add(this.pnlButtons, 0, 4);
            this.layout.SetColumnSpan(this.pnlButtons, 2);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
