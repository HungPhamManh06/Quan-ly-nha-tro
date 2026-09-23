// ------------------------------------------------------------------------------
//  GIAO DIỆN CỦA TenantDialog (mở bằng Visual Studio Designer: nhấp đúp file hoặc Shift+F7).
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
    partial class TenantDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblQueQuan;
        private System.Windows.Forms.TextBox txtQueQuan;
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
            this.ClientSize = new System.Drawing.Size(450, 214);
            this.Text = "Thêm người thuê mới";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            this.Name = "TenantDialog";
            this.SuspendLayout();
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Size = new System.Drawing.Size(450, 214);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.layout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 6;
            this.layout.AutoScroll = true;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Location = new System.Drawing.Point(8, 6);
            this.lblHoTen.Size = new System.Drawing.Size(152, 24);
            this.lblHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHoTen.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblHoTen.AutoSize = false;
            this.lblHoTen.Text = "Họ tên:";
            this.lblHoTen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Location = new System.Drawing.Point(164, 8);
            this.txtHoTen.Size = new System.Drawing.Size(278, 20);
            this.txtHoTen.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Location = new System.Drawing.Point(8, 30);
            this.lblCCCD.Size = new System.Drawing.Size(152, 24);
            this.lblCCCD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCCCD.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblCCCD.AutoSize = false;
            this.lblCCCD.Text = "CCCD:";
            this.lblCCCD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Location = new System.Drawing.Point(164, 32);
            this.txtCCCD.Size = new System.Drawing.Size(278, 20);
            this.txtCCCD.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtCCCD.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Location = new System.Drawing.Point(8, 54);
            this.lblSoDienThoai.Size = new System.Drawing.Size(152, 24);
            this.lblSoDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoDienThoai.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblSoDienThoai.AutoSize = false;
            this.lblSoDienThoai.Text = "Số điện thoại:";
            this.lblSoDienThoai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Location = new System.Drawing.Point(164, 56);
            this.txtDienThoai.Size = new System.Drawing.Size(278, 20);
            this.txtDienThoai.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtDienThoai.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblQueQuan = new System.Windows.Forms.Label();
            this.lblQueQuan.Name = "lblQueQuan";
            this.lblQueQuan.Location = new System.Drawing.Point(8, 78);
            this.lblQueQuan.Size = new System.Drawing.Size(152, 24);
            this.lblQueQuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQueQuan.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblQueQuan.AutoSize = false;
            this.lblQueQuan.Text = "Quê quán:";
            this.lblQueQuan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtQueQuan = new System.Windows.Forms.TextBox();
            this.txtQueQuan.Name = "txtQueQuan";
            this.txtQueQuan.Location = new System.Drawing.Point(164, 80);
            this.txtQueQuan.Size = new System.Drawing.Size(278, 20);
            this.txtQueQuan.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtQueQuan.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Location = new System.Drawing.Point(8, 102);
            this.lblGhiChu.Size = new System.Drawing.Size(152, 24);
            this.lblGhiChu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(1, 0, 4, 0);
            this.lblGhiChu.AutoSize = false;
            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Location = new System.Drawing.Point(164, 104);
            this.txtGhiChu.Size = new System.Drawing.Size(278, 20);
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Location = new System.Drawing.Point(8, 128);
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
            this.layout.Controls.Add(this.lblHoTen, 0, 0);
            this.layout.Controls.Add(this.txtHoTen, 1, 0);
            this.layout.Controls.Add(this.lblCCCD, 0, 1);
            this.layout.Controls.Add(this.txtCCCD, 1, 1);
            this.layout.Controls.Add(this.lblSoDienThoai, 0, 2);
            this.layout.Controls.Add(this.txtDienThoai, 1, 2);
            this.layout.Controls.Add(this.lblQueQuan, 0, 3);
            this.layout.Controls.Add(this.txtQueQuan, 1, 3);
            this.layout.Controls.Add(this.lblGhiChu, 0, 4);
            this.layout.Controls.Add(this.txtGhiChu, 1, 4);
            this.layout.Controls.Add(this.pnlButtons, 0, 5);
            this.layout.SetColumnSpan(this.pnlButtons, 2);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
